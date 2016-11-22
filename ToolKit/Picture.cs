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
            public enum ConvertResult {
                FormatNotMatch,
                FileCorrupt,
                ZeroPixel,
                Success
            }

            static void DecodeOne(object param) {
                TransPara para = (TransPara)param;
                string file = para.Inputfile;
                string outfile = para.Outputfile;
                //string outfile = outputfolder + "\\" + Path.GetFileNameWithoutExtension(file) + ".PNG";
                if (rgba2png(file, outfile) != ConvertResult.Success) {
                    try {
                        byte2png(file,outfile);
                    }
                    catch (Exception e) {
                        //return false;
                    }
                }
                //return true;
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
            public static ConvertResult rgba2png(string inputfile, string outputfile) {
                FileStream f = new FileStream(inputfile, FileMode.Open, FileAccess.ReadWrite);
                f.Seek(0x14, SeekOrigin.Begin);
                if(readint(ref f)!=0x11221010)
                    return ConvertResult.FormatNotMatch;
                f.Seek(0x10, SeekOrigin.Begin);
                int width = readint16(ref f);
                int height = readint16(ref f);
                f.Seek(0x40, SeekOrigin.Begin);
                if (width == 0 || height == 0) 
                    return ConvertResult.ZeroPixel;
                try {
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
                }
                catch (System.Exception e) {
                    f.Close();
                    return ConvertResult.FileCorrupt;
                }
                f.Close();
                return ConvertResult.Success;
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
                catch (System.Exception e) {
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
            public static void DecodeDirSeq(string inputfolder, string outputfolder) {
                string[] files = Directory.GetFiles(inputfolder);
                foreach (string file in files) {
                    string outfile = outputfolder + "\\" + Path.GetFileNameWithoutExtension(file) + ".PNG";
                    DecodeOne(new TransPara(file, outfile));
                }
            }

            /// <summary>
            /// 将整个文件夹中的konami图像转换成png。支持BNR和IDX。多线程运行。
            /// </summary>
            /// <param name="inputfolder">输入目录</param>
            /// <param name="outputfolder">输出目录</param>
            /// <param name="threadcount">线程数</param>
            /// <exception cref="System.FormatException">输入文件名不是以BNR或IDX开头的</exception>
            /// <returns>表示每个文件执行过程的Task数组</returns>
            public static Task[] DecodeDir(string inputfolder, string outputfolder) {
                string[] files = Directory.GetFiles(inputfolder);
                Task[] tasks = new Task[files.Count()];
                int i = 0;
                foreach (string file in files) {
                    string outfile = outputfolder + "\\" + Path.GetFileNameWithoutExtension(file) + ".PNG";
                    tasks[i++] = Task.Factory.StartNew(DecodeOne, new TransPara(file, outfile), TaskCreationOptions.PreferFairness);
                }
                return tasks;
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
                buf[20] = 0x81;
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

            /// <summary>
            /// 
            /// </summary>
            /// <param name="inputfolder"></param>
            /// <param name="outputfolder"></param>
            public static Task[] EncodeDir(string inputfolder, string outputfolder) {
                string[] files = Directory.GetFiles(inputfolder);
                Task[] tasks = new Task[files.Count()];
                int i = 0;
                foreach (string file in files) {
                    if (Path.GetExtension(file).ToLower().Contains("png")) {
                        string outfile = outputfolder + "\\" + Path.GetFileNameWithoutExtension(file);
                        tasks[i++] = Task.Factory.StartNew(EncodeAsync, new TransPara(file, outfile), TaskCreationOptions.PreferFairness);
                    }
                }
                return tasks;
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

