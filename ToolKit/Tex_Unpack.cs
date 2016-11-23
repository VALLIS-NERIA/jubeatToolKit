using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace ToolKit {
    static partial class Tex {
        //public static void unpack(string inputfile, string outputfolder);
        //public static void repack(string originalfile, string sourcefolder, string outputfile);
        //public static void rgba2png(string inputfile, string outputfile);
        //public static void png2rgba(string outputfile, string intputfile);

        struct UnpackedFile {
            public string Name;
            public int Address;
            public int Length;
            public UnpackedFile(string name, int address, int length) {
                Name = name;
                Address = address;
                Length = length;
            }
        }

        struct LzssPara {
            public int Mode;
            public string Input;
            public string Output;
            public LzssPara(int mode, string input, string output) {
                Mode = mode;
                Input = input;
                Output = output;
            }
        }

        struct Para {
            public string Inputpath;
            public string Outputpath;
            public Para(string inpath, string outpath) {
                Inputpath = inpath;
                Outputpath = outpath;
            }
        }

        static int readint(ref FileStream f) {
            byte[] buf = new byte[4];
            f.Read(buf, 0, 4);
            return System.BitConverter.ToInt32(buf, 0);
        }

        static byte[] SwitchEndian(byte[] little) {
            int l = little.Length;
            int n;
            byte[] big = new byte[l];
            for (int i = 0; i < l; i++) {
                n = 3 + 8 * (int)(i / 4) - i;
                big[i] = little[n];
            }
            return big;
        }

        /// <summary>
        /// 调用C编写的lzss.dll使用lzss算法压缩/解压缩
        /// </summary>
        /// <param name="mode">0：压缩；非0：解压缩</param>
        /// <param name="inputfile">输入文件</param>
        /// <param name="outputfile">输出文件</param>
        /// <returns>是否成功执行</returns>
        [DllImport("lzss_dll.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int lzss(int mode, string inputfile, string outputfile);

        static void LzssAsync(object param) {
            LzssPara para = (LzssPara)param;
            lzss(para.Mode, para.Input, para.Output);
        }

        public static void UnpackFull_D(string inputfolder, string outputfolder) {
            string[] files = Directory.GetFiles(inputfolder);
            string name = Path.GetFileName(inputfolder);
            Directory.CreateDirectory(outputfolder);
            foreach (string file in files) {
                UnpackFull(file, outputfolder);
            }
        }

        public static void UnpackFull(string inputfile, string outputfolder) {
            string workingdir = outputfolder + "\\" + Path.GetFileNameWithoutExtension(inputfile) + "\\";
            Directory.CreateDirectory(workingdir);
            string unpackdir = workingdir + "\\unpack\\";
            string decompressdir = workingdir + "\\decompress\\";
            string pngdir = workingdir + "\\png\\";
            Directory.CreateDirectory(unpackdir);
            Directory.CreateDirectory(decompressdir);
            Directory.CreateDirectory(pngdir);
            Unpack(inputfile, unpackdir);
            Task.WaitAll(LzssDir(1, unpackdir, decompressdir));
            //LzssDir(1, unpackdir, decompressdir);
            Picture.DecodeDir(decompressdir, pngdir);
            string newfile = workingdir + "\\" + Path.GetFileName(inputfile);
            File.Copy(inputfile, newfile, true);
            File.SetAttributes(newfile, FileAttributes.Normal);
        }

        public static void Unpack(string inputfile, string outputfolder) {
            FileStream f = new FileStream(inputfile, FileMode.Open, FileAccess.Read);

            f.Seek(0x3c, SeekOrigin.Begin);
            int fileMapBegin = readint(ref f);
            f.Seek(0x50, SeekOrigin.Begin);
            int fileNum = readint(ref f);
            f.Seek(8 + 12 * fileNum, SeekOrigin.Current);
            List<string> fileNameList = new List<string>();
            string nameBuf = "";
            while (fileNum > 0) {
                byte b = (byte)f.ReadByte();
                if (b != '\0')
                    nameBuf += (char)b;
                else {
                    fileNameList.Add(nameBuf);
                    nameBuf = "";
                    fileNum--;
                }
            }
            f.Seek(fileMapBegin, SeekOrigin.Begin);
            List<UnpackedFile> fileList = new List<UnpackedFile>();
            foreach (string fileName in fileNameList) {
                readint(ref f);
                int length = readint(ref f);
                int address = readint(ref f);
                fileList.Add(new UnpackedFile(fileName, address, length));
            }
            foreach (UnpackedFile file in fileList) {
                f.Seek(file.Address, SeekOrigin.Begin);
                byte[] buf = new byte[5000000];
                f.Seek(8, SeekOrigin.Current);
                f.Read(buf, 0, file.Length - 8);
                if (!Directory.Exists(outputfolder)) {
                    Directory.CreateDirectory(outputfolder);
                }
                Directory.CreateDirectory(outputfolder);
                string outfile = outputfolder + file.Name;
                FileStream fwrite = new FileStream(outfile, FileMode.Create);
                fwrite.Write(buf, 0, file.Length - 8);
                fwrite.Close();
            }
            f.Close();
        }


        /// <summary>
        /// 异步、多线程地对文件夹中的所有文件进行lzss压缩/解压缩。
        /// </summary>
        /// <param name="mode">0：压缩；非0：解压缩</param>
        /// <param name="inputfolder">输入文件夹</param>
        /// <param name="outputfolder">输出文件夹</param>
        /// <param name="threadcount">最大线程数</param>
        /// <returns>表示每个文件执行过程的Task数组</returns>
        public static Task[] LzssDir(int mode, string inputfolder, string outputfolder) {
            string[] files = Directory.GetFiles(inputfolder);
            //ThreadPool.SetMaxThreads(threadcount, threadcount);
            string name = Path.GetFileName(inputfolder);
            Directory.CreateDirectory(outputfolder);
            Task[] tasks = new Task[files.Count()];
            int i = 0;
            foreach (string file in files) {
                string outputfile = outputfolder + Path.GetFileName(file);
                tasks[i++] = Task.Factory.StartNew(LzssAsync, new LzssPara(mode, file, outputfile), TaskCreationOptions.PreferFairness);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(LzssAsync), new LzssPara(mode, file, outputfile));
            }
            return tasks;
        }

        /// <summary>
        /// 对文件夹中的所有文件进行lzss压缩/解压缩。
        /// </summary>
        /// <param name="mode">0：压缩；非0：解压缩</param>
        /// <param name="inputfolder">输入文件夹</param>
        /// <param name="outputfolder">输出文件夹</param>
        public static void LzssDirSeq(int mode, string inputfolder, string outputfolder) {
            string[] files = Directory.GetFiles(inputfolder);
            foreach (string file in files) {
                string outputfile = outputfolder + "\\" + Path.GetFileName(file);
                lzss(mode, file, outputfile);
            }
        }
    }
}
