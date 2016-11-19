using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ToolKit {
    static partial class Tex {
        static public class Picture {

            struct TransPara {
                public string Inputfile;
                public string Outputfile;
                public TransPara(string infile, string outfile) {
                    Inputfile = infile;
                    Outputfile = outfile;
                }
            }

            static void DecodeAsync(object param) {
                TransPara para = (TransPara)param;
                string file = para.Inputfile;
                string outfile = para.Outputfile;
                //string outfile = outputfolder + "\\" + Path.GetFileNameWithoutExtension(file) + ".PNG";
                if (Path.GetFileName(file).StartsWith("BNR")) {
                    rgba2png(file, outfile);
                }
                else if (Path.GetFileName(file).StartsWith("IDX")) {
                    byte2png(file, outfile);
                }
                else {
                    throw new System.FormatException("输入既不是BNR，也不是IDX！");
                }
            }

            static void EncodeAsync(object param) {
                TransPara para = (TransPara)param;
                string file = para.Inputfile;
                string outfile = para.Outputfile;
                png2rgba(file, outfile);
            }

            static int readint16(ref FileStream f) {
                byte[] buf = new byte[4];
                f.Read(buf, 0, 2);
                buf[2] = 0;
                buf[3] = 0;
                return System.BitConverter.ToInt32(buf, 0);
            }
            static int readint(ref FileStream f) {
                byte[] buf = new byte[4];
                f.Read(buf, 0, 4);
                return System.BitConverter.ToInt32(buf, 0);
            }

            /// <summary>
            /// 把经过lzss解压后的RGBA图像文件转化成png文件，适用于BNR开头的封面图像
            /// </summary>        
            /// <param name="inputfile">输入文件</param>
            /// <param name="outputfile">输出文件</param>
            public static void rgba2png(string inputfile, string outputfile) {
                FileStream f = new FileStream(inputfile, FileMode.Open, FileAccess.ReadWrite);
                f.Seek(0x10, SeekOrigin.Begin);
                int width = readint16(ref f);
                int height = readint16(ref f);
                f.Seek(0x40, SeekOrigin.Begin);
                if (width == 0 || height == 0) return;
                Bitmap image = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++) {
                        byte[] buf = new byte[4];
                        f.Read(buf, 0, 4);
                        int r = (int)(buf[0]);
                        int g = (int)(buf[1]);
                        int b = (int)(buf[2]);
                        int a = (int)(buf[3]);
                        image.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                    }
                image.Save(outputfile, ImageFormat.Png);
                f.Close();
            }

            /// <summary>
            /// 把经过lzss解压后的单bit图像文件转化成png文件，适用于部分IDX开头的曲目标题图像
            /// </summary>
            /// <param name="inputfile">输入文件</param>
            /// <param name="outputfile">输出文件</param>
            public static void byte2png(string inputfile, string outputfile) {
                FileStream f = new FileStream(inputfile, FileMode.Open, FileAccess.ReadWrite);
                f.Seek(0x10, SeekOrigin.Begin);
                int width = readint16(ref f);
                int height = readint16(ref f);
                f.Seek(0x40, SeekOrigin.Begin);
                if (width == 0 || height == 0) return;
                Bitmap image = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                int i = 0;
                try {
                    for (int y = 0; y < height; y++)
                        for (int x = 0; x < width; x++) {
                            int r = 0xff;
                            int g = 0xff;
                            int b = 0xff;
                            int a = (int)f.ReadByte();
                            image.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                            i++;
                        }
                    //string outfile = outputfolder + "\\" + Path.GetFileName(inputfile) + ".PNG";
                    image.Save(outputfile, ImageFormat.Png);
                }
                catch(System.Exception e){
                    File.Create(outputfile + "_BROKEN");
                }
                f.Close();
            }

            /// <summary>
            /// 将整个文件夹中的konami图像转换成png。支持BNR和IDX。
            /// </summary>
            /// <param name="inputfolder">输入目录</param>
            /// <param name="outputfolder">输出目录</param>
            /// <exception cref="System.FormatException">输入文件名不是以BNR或IDX开头的</exception>
            public static void DecodeDir(string inputfolder, string outputfolder) {
                string[] files = Directory.GetFiles(inputfolder);
                foreach (string file in files) {
                    string outfile = outputfolder + "\\" + Path.GetFileNameWithoutExtension(file) + ".PNG";
                    if (Path.GetFileName(file).StartsWith("BNR")) {
                        rgba2png(file, outfile);
                    }
                    else if (Path.GetFileName(file).StartsWith("IDX")) {
                        byte2png(file, outfile);
                    }
                    else {
                        throw new System.FormatException("输入既不是BNR，也不是IDX！");
                    }

                }
            }

            /// <summary>
            /// 将整个文件夹中的konami图像转换成png。支持BNR和IDX。多线程运行。
            /// </summary>
            /// <param name="inputfolder">输入目录</param>
            /// <param name="outputfolder">输出目录</param>
            /// <param name="threadcount">线程数</param>
            /// <exception cref="System.FormatException">输入文件名不是以BNR或IDX开头的</exception>
            public static void DecodeDir(string inputfolder, string outputfolder, int threadcount) {
                ThreadPool.SetMaxThreads(threadcount, threadcount);
                string[] files = Directory.GetFiles(inputfolder);
                foreach (string file in files) {
                    string outfile = outputfolder + "\\" + Path.GetFileNameWithoutExtension(file) + ".PNG";
                    ThreadPool.QueueUserWorkItem(new WaitCallback(DecodeAsync), new TransPara(file, outfile));

                }
            }

            public static void png2rgba(string inputfile, string outputfile) {
                FileStream w = new FileStream(outputfile, FileMode.Create, FileAccess.Write);
                Bitmap image = new Bitmap(inputfile);
                short width = (short)image.Width;
                short height = (short)image.Height;
                int fsize = width * height * 4 + 64;
                //Header
                byte[] buf = new byte[] { 0x54, 0x44, 0x58, 0x54, 0x00, 0x00, 0x01, 0x00, 0x00, 0x01, 0x01, 0x00 };
                w.Write(buf, 0, buf.Length);
                w.Write(BitConverter.GetBytes(fsize), 0, 4);
                w.Write(BitConverter.GetBytes(width), 0, 2);
                w.Write(BitConverter.GetBytes(height), 0, 2);
                w.Write(new byte[] { 0x10, 0x10, 0x22, 0x11 }, 0, 4);
                buf = new byte[40];
                buf[20] = 0x83;
                w.Write(buf, 0, buf.Length);
                //文件头
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++) {
                        Color px = image.GetPixel(x, y);
                        w.WriteByte(px.R);
                        w.WriteByte(px.G);
                        w.WriteByte(px.B);
                        w.WriteByte(px.A);
                    }
                //image.GetPixel(0,0).
                w.Close();
            }

            public static void EncodeDir(string inputfolder, string outputfolder, int threadcount) {
                ThreadPool.SetMaxThreads(threadcount, threadcount);
                string[] files = Directory.GetFiles(inputfolder);
                foreach (string file in files) {
                    if (Path.GetExtension(file).ToLower().Contains("png")) {
                        string outfile = outputfolder + "\\" + Path.GetFileNameWithoutExtension(file);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(EncodeAsync), new TransPara(file, outfile));
                    }
                }
            }


            /// <summary>
            /// 已停用
            /// </summary>
            /// <param name="inputfile"></param>
            /// <param name="outputfolder"></param>
            public static void bit2png_old(string inputfile, string outputfolder) {
                FileStream f = new FileStream(inputfile, FileMode.Open, FileAccess.ReadWrite);
                f.Seek(0x10, SeekOrigin.Begin);
                int width = readint16(ref f);
                int height = readint16(ref f);
                f.Seek(0x40, SeekOrigin.Begin);
                int bytecount = width * height / 8;
                byte[] buf = new byte[bytecount];
                f.Read(buf, 0, bytecount);
                BitArray bit = new BitArray(buf);
                if (width == 0 || height == 0) return;
                Bitmap image = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                int i = 0;
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++) {
                        int r = 0xff;
                        int g = 0xff;
                        int b = 0xff;
                        int a = (bit[i] == true) ? 0xff : 0;
                        image.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                        i++;
                    }
                string outfile = outputfolder + "\\" + Path.GetFileName(inputfile) + ".PNG";
                image.Save(outfile, ImageFormat.Png);
            }

        }
    }
}

