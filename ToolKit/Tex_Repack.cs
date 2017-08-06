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
        public static void RepackFull_D(string inputfolder, string outputfolder, bool reGeneratePic = false, bool reLzss = false) {
            string[] dirs = Directory.GetDirectories(inputfolder);
            Directory.CreateDirectory(outputfolder);
            foreach (string workingdir in dirs) {
                //TODO:Log
                string unpackdir = workingdir + "\\unpack\\";
                string decompressdir = workingdir + "\\decompress\\";
                string pngdir = workingdir + "\\png\\";
                string oribin = workingdir + "\\" + Path.GetFileName(workingdir) + ".bin";
                string outputfile = outputfolder + "\\" + Path.GetFileName(workingdir) + ".bin";
                RepackFull(pngdir, decompressdir, unpackdir, oribin, outputfile, reGeneratePic, reLzss);
            }
        }

        public static void RepackFull(string pngdir, string decompressdir, string unpackdir, string oribin, string outputfile, bool reGeneratePic = false, bool reLzss = false) {
            if (reGeneratePic)
                Task.WaitAll(Picture.EncodeDir(pngdir, decompressdir));
            if (reLzss)
                Task.WaitAll(LzssDir(0, decompressdir, unpackdir));
            Repack(unpackdir, decompressdir, outputfile, oribin);
        }

        /// <summary>
        /// 把解包出来的文件重新打包。
        /// </summary>
        /// <param name="inputfolder">经过lzss压缩后的文件的目录</param>
        /// <param name="outputfile">输出目标</param>
        /// <param name="originalbin">用作参照的原始bin文件</param>
        /// <param name="inputfolder_uncompressed">lzss压缩前的文件的目录</param>
        public static unsafe void Repack(string inputfolder, string inputfolder_uncompressed, string outputfile, string originalbin) {
            
            FileStream ori = new FileStream(originalbin, FileMode.Open, FileAccess.ReadWrite);

            ori.Seek(0x3c, SeekOrigin.Begin);
            int fileMapBegin = readint(ref ori);
            ori.Seek(0x50, SeekOrigin.Begin);
            int fileNum = readint(ref ori);
            int fileNum_1 = fileNum;
            ori.Seek(8 + 12 * fileNum, SeekOrigin.Current);
            List<string> fileNameList = new List<string>();
            string nameBuf = "";
            while (fileNum_1 > 0) {
                byte b = (byte)ori.ReadByte();
                if (b != '\0')
                    nameBuf += (char)b;
                else {
                    fileNameList.Add(nameBuf);
                    nameBuf = "";
                    fileNum_1--;
                }
            }
            ori.Seek(0, SeekOrigin.Begin);
            FileStream newbin = new FileStream(outputfile, FileMode.Create, FileAccess.ReadWrite);
            byte[] buf1 = new byte[fileMapBegin];
            ori.Read(buf1, 0, fileMapBegin);
            newbin.Write(buf1, 0, fileMapBegin);
            ori.Close();

            //0,length,address
            //mapBuf暂存filemap，之后一起写入。
            int fileaddress = fileMapBegin + 12 * fileNum;
            byte[] mapBuf = new byte[12 * fileNum];
            fixed (byte* pb = &mapBuf[0]) {
                int* p = (int*)pb;
                int address = fileaddress;
                newbin.Write(mapBuf, 0, mapBuf.Length);//占位

                foreach (string file in fileNameList) {
                    FileStream f_zipped = new FileStream(inputfolder + "\\" + file, FileMode.Open, FileAccess.Read);
                    //更新filemap
                    *p++ = 0;
                    *p++ = (int)f_zipped.Length + 8;
                    //SIZE,ZIPPED_SIZE,正文。所以+8。
                    *p++ = address;
                    address += (int)f_zipped.Length + 8;

                    //写入文件
                    //SIZE
                    FileStream f_unzipped = new FileStream(inputfolder_uncompressed + "\\" + file, FileMode.Open, FileAccess.Read);
                    int t = (int)f_unzipped.Length;
                    int unzipped_size = t - t % 0xf;
                    newbin.Write(SwitchEndian(BitConverter.GetBytes(f_unzipped.Length)), 0, 4);
                    //ZIPPED_SIZE
                    int zipped_size = (int)f_zipped.Length;
                    newbin.Write(SwitchEndian(BitConverter.GetBytes(zipped_size)), 0, 4);

                    //正文
                    f_zipped.CopyTo(newbin);

                    //32位对齐
                    //因为ori是对齐的，所以直到写完第一个文件之前肯定都是对齐的
                    if (f_zipped.Length % 4 != 0) {
                        int padl = 4 - (int)f_zipped.Length % 4;
                        for (; padl > 0; padl--) {
                            newbin.WriteByte(0x00);
                            address++;
                        }
                    }


                    f_zipped.Close();
                    f_unzipped.Close();
                }

                //所有文件都写回之后，写入mapBuf。
                newbin.Seek(fileMapBegin, SeekOrigin.Begin);
                newbin.Write(mapBuf, 0, 12 * fileNum);
                newbin.Close();

            }
        }


    }
}
