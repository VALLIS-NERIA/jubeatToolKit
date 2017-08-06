using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ToolKit {
    public static class IFS {
        public static string exedir;
        public static string dumpexe;
        public static string buildexe;
        static Form1 frm = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d">dumpImgFS.exe</param>
        /// <param name="b">buildImgFS.exe</param>
        public static void SetExe(string d, string b) {
            dumpexe = d;
            buildexe = b;
            exedir = Path.GetDirectoryName(d);
        }

        public static void SetExe(string d, string b, Form1 frm1) {
            dumpexe = d;
            buildexe = b;
            exedir = Path.GetDirectoryName(d);
            frm = frm1;
        }

        private static string GetPathFromId(string id, string ifs_pack) { return ifs_pack + "\\d" + id.Substring(0, id.Length - 1) + "\\" + id + "_msc.ifs"; }

        /// <summary>
        /// 按照csv转换编号
        /// </summary>
        /// <param name="list">csv</param>
        /// <param name="src">源文件夹</param>
        /// <param name="dst">目标文件夹</param>
        /// <returns></returns>
        public static bool RefractList(string list, string src, string dst) {
            FileStream fs = new FileStream(list, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string srcid, dstid;
            while (true) {
                string read = sw.ReadLine();
                if (read.Split(',').Length < 2) break;
                if (read.Split(',').Length > 2) continue;
                dstid = read.Split(',')[0];
                srcid = read.Split(',')[1];
                if (srcid == "") break;
                string filesrc = GetPathFromId(srcid, src);
                string filedst = GetPathFromId(dstid, dst);
                frm.Log("开始转换：src = " + srcid + " dst = " + dstid);
                Refract(filesrc, filedst);
                frm.Counter();
            }
            sw.Close();
            fs.Close();
            return true;
        }


        /// <summary>
        /// 把编号为src的bin改成编号为dst的
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <returns></returns>
        public static bool Refract(string src, string dst) {
            string src_ext = Extract(src);
            if (src_ext == null) {
                frm.Log("Refract失败！\r\nsrc = " + src + " \r\ndst = " + dst);
            }

            string dst_id = Path.GetFileNameWithoutExtension(dst).TrimEnd("_mscimgfs.ifs".ToCharArray());
            string[] files = Directory.GetFiles(src_ext);
            string dst_ext = Path.GetDirectoryName(dst) + "\\" + Path.GetFileNameWithoutExtension(dst);
            Directory.CreateDirectory(dst_ext);
            foreach (string file in files) {
                string nfile;
                string ofile = Path.GetFileName(file);
                switch (ofile.TrimStart("0123456789".ToCharArray())) {
                case "adv.eve":
                case "a.eve":
                    nfile = dst_id + "a.eve";
                    break;
                case "bsc.eve":
                case "b.eve":
                    nfile = dst_id + "b.eve";
                    break;
                case "ext.eve":
                case "e.eve":
                    nfile = dst_id + "e.eve";
                    break;
                case "idx.bin":
                    nfile = dst_id + "idx.bin";
                    break;
                case "bgm.bin":
                    nfile = dst_id + "bgm.bin";
                    break;
                default:
                    nfile = file;
                    //throw new System.Exception("预料之外的文件：" + file);
                    continue;
                }
                File.Copy(file, dst_ext + "\\" + nfile, true);
            }
            string bld = Build(dst_ext);
            if (bld == null) return false;
            File.Copy(bld, dst, true);
            return true;
        }

        public static string Extract(string inputfile) {
            Process dump = new Process();
            ProcessStartInfo ps = new ProcessStartInfo(dumpexe, inputfile);
            ps.CreateNoWindow = true;
            ps.UseShellExecute = false;
            dump.StartInfo = ps;
            dump.Start();
            dump.WaitForExit(3000);
            string gendir = exedir + "\\" + Path.GetFileNameWithoutExtension(inputfile) + "_imgfs";
            return Directory.Exists(gendir) ? gendir : null;
        }

        public static bool Extract(string inputfile, string outputfolder) {
            string gendir = outputfolder + "\\" + Path.GetFileName(inputfile) + "_imgfs";
            Process dump = new Process();
            dump.StartInfo = new ProcessStartInfo(dumpexe, inputfile);
            dump.Start();
            dump.WaitForExit(3000);
            if (outputfolder.TrimEnd(new char[] {'\\'}) != exedir.TrimEnd(new char[] {'\\'})) {
                if (Directory.Exists(gendir)) {
                    string[] files = Directory.GetFiles(gendir);
                    foreach (string file in files) {
                        string dest = outputfolder + "\\" + Path.GetFileName(inputfile) + "\\" + Path.GetFileName(file);
                        File.Copy(file, dest, true);
                    }
                    return true;
                }
            }
            return false;
        }

        public static string Build(string inputfolder) {
            Process build = new Process();
            ProcessStartInfo ps = new ProcessStartInfo(buildexe, inputfolder + " iidx");
            ps.CreateNoWindow = true;
            ps.UseShellExecute = false;
            build.StartInfo = ps;
            build.Start();
            build.WaitForExit(3000);
            string genfile = exedir + "\\" + Path.GetFileName(inputfolder) + ".ifs";
            return File.Exists(genfile) ? genfile : null;
        }

        public static void Build(string inputfolder, string outputfile) {
            string genfile = exedir + "\\" + Path.GetFileName(inputfolder) + ".ifs";
            Process build = new Process();
            build.StartInfo = new ProcessStartInfo(buildexe, inputfolder + " iidx");
            build.Start();
            build.WaitForExit(3000);
            File.Copy(genfile, outputfile, true);
        }
    }
}