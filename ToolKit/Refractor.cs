using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ToolKit {
    public static class Refractor {
        //string texdir;
        //string ifsdir;
        ////TODO
        //public Refractor() {

        //}

        //public void Refract(string srcid, string dstid) {
        //    //IFS
        //    //TEX: BNR_BIG, BNR_, IDX_BIG, IDX_
        //    //XML
        //}
        static char[] CHARS = "BNR_BIG_ID_IDX_MINI".ToCharArray();

        public static void RefractTEX_2(string list, string src, string dst) {
            FileStream fs = new FileStream(list, FileMode.Open, FileAccess.Read);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            StreamReader sw = new StreamReader(fs);
            string srcid, dstid;
            while (true) {
                string read = sw.ReadLine();
                if (read.Split(',').Length < 2) break;
                //if (read.Split(',').Length > 2) continue;
                dstid = read.Split(',')[0];
                srcid = read.Split(',')[1];
                //if (read.Split(',')[2] != "") continue;
                if (srcid == "") break;
                dict.Add(srcid, dstid);
            }
            string[] dirs1 = Directory.GetDirectories(src);
            foreach (string dir1 in dirs1) {
                string pngdir = dir1 + "\\" + "png\\";
                string[] files_de = Directory.GetFiles(pngdir);
                foreach (string file_de in files_de) {
                    string thisname_de = Path.GetFileNameWithoutExtension(file_de);
                    string srcid_de = thisname_de.Trim(CHARS);
                    if (!dict.ContainsKey(srcid_de)) continue;
                    string[] dstfiles_de = Directory.GetFiles(dst, thisname_de.Substring(0, thisname_de.Length - 8) + dict[srcid_de], SearchOption.AllDirectories);
                    foreach (string dstfile_de in dstfiles_de) {
                        string dst_pngdir = Path.GetDirectoryName(Path.GetDirectoryName(dstfile_de)) + "\\png\\";
                        if(!Directory.Exists(dst_pngdir))
                            Directory.CreateDirectory(dst_pngdir);
                        string dest = dst_pngdir + thisname_de.Substring(0, thisname_de.Length - 8) + dict[srcid_de] + ".png";

                        File.Copy(file_de, dest, true);
                        break;

                    }
                }
            }
        }


        public static void RefractTEX(string list, string src, string dst,bool regen) {
            FileStream fs = new FileStream(list, FileMode.Open, FileAccess.Read);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            StreamReader sw = new StreamReader(fs);
            string srcid, dstid;
            while (true) {
                string read = sw.ReadLine();
                if (read.Split(',').Length < 2) break;
                //if (read.Split(',').Length > 2) continue;
                dstid = read.Split(',')[0];
                srcid = read.Split(',')[1];
                //if (read.Split(',')[2] != "") continue;
                if (srcid == "") break;
                dict.Add(srcid, dstid);
            }
            string[] dirs1 = Directory.GetDirectories(src);
            if (!regen) {
                foreach (string dir1 in dirs1) {
                    string decompressdir = dir1 + "\\" + "decompress\\";
                    string unpackdir = dir1 + "\\unpack\\";
                    string[] files_de = Directory.GetFiles(decompressdir);
                    foreach (string file_de in files_de) {
                        string thisname_de = Path.GetFileNameWithoutExtension(file_de);
                        string srcid_de = thisname_de.Trim(CHARS);
                        if (!dict.ContainsKey(srcid_de)) continue;
                        string[] dstfiles_de = Directory.GetFiles(dst, thisname_de.Substring(0, thisname_de.Length - 8) + dict[srcid_de], SearchOption.AllDirectories);
                        foreach (string dstfile_de in dstfiles_de) {
                            if (Path.GetDirectoryName(dstfile_de).EndsWith("decompress")) {
                                File.Copy(file_de, dstfile_de, true);
                                break;
                            }
                        }
                    }
                    string[] files_un = Directory.GetFiles(unpackdir);
                    foreach (string file_un in files_un) {
                        string thisname_un = Path.GetFileNameWithoutExtension(file_un);
                        string srcid_un = thisname_un.Trim(CHARS);

                        if (!dict.ContainsKey(srcid_un)) continue;
                        string[] dstfiles_un = Directory.GetFiles(dst, thisname_un.Substring(0, thisname_un.Length - 8) + dict[srcid_un], SearchOption.AllDirectories);
                        foreach (string dstfile_un in dstfiles_un) {
                            if (Path.GetDirectoryName(dstfile_un).EndsWith("unpack")) {
                                File.Copy(file_un, dstfile_un, true);
                                break;
                            }
                        }
                    }
                }
            }
            else {
                foreach (string dir1 in dirs1) {
                    string pngdir = dir1 + "\\png\\";
                    string[] files_png = Directory.GetFiles(pngdir);
                    foreach (string file_png in files_png) {
                        string thisname_png = Path.GetFileNameWithoutExtension(file_png);
                        string srcid_png = thisname_png.Trim(CHARS);
                        if (!dict.ContainsKey(srcid_png)) continue;
                        string dstfilename_png = thisname_png.Substring(0, thisname_png.Length - 8) + dict[srcid_png] + ".PNG";
                        string[] dstfiles_png = Directory.GetFiles(dst,dstfilename_png , SearchOption.AllDirectories);
                        if (dstfiles_png.Count() > 1) {
                            string s = "";
                            s += "what";
                        }
                        foreach (string dstfile_png in dstfiles_png) {
                            if (Path.GetDirectoryName(dstfile_png).EndsWith("png")) {
                                File.Copy(file_png, dstfile_png, true);
                                break;
                            }
                        }
                    }
                }
            }
        }

    }
}
