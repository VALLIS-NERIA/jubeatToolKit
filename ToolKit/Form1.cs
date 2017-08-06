using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace ToolKit {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            Picture.frm = this;
        }

        string inputfile;
        string inputdir;
        string outputdir;
        int i = 0;
        bool rework_pic = false;

        bool rework_lzss = false;

        private void buttonBrowseInputFile_Click(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
            inputFile.Text = openFileDialog1.FileName;
        }

        private void buttonBrowseInputDir_Click(object sender, EventArgs e) {
            folderBrowserDialogIn.ShowDialog();
            inputDirectory.Text = folderBrowserDialogIn.SelectedPath;
        }

        private void buttonBrowseOutputDir_Click(object sender, EventArgs e) {
            folderBrowserDialogOut.ShowDialog();
            outputDirectory.Text = folderBrowserDialogOut.SelectedPath;
        }

        private void inputFile_TextChanged(object sender, EventArgs e) { inputfile = inputFile.Text; }

        private void inputDirectory_TextChanged(object sender, EventArgs e) { inputdir = inputDirectory.Text; }

        private void outputDirectory_TextChanged(object sender, EventArgs e) { outputdir = outputDirectory.Text; }

        private void buttonUnpack_Click(object sender, EventArgs e) { Tex.Unpack(inputfile, outputdir); }

        private void buttonDecompressMT_Click(object sender, EventArgs e) { Tex.LzssDir(1, inputdir, outputdir); }

        private void buttonDecode_Click(object sender, EventArgs e) { Picture.DecodeDir(inputdir, outputdir); }

        private void buttonEncode_Click(object sender, EventArgs e) { Picture.EncodeDir(inputdir, outputdir); }

        private void Aio_1_Click(object sender, EventArgs e) { Tex.UnpackFull(inputfile, outputdir, rework_pic); }

        private void Aio_2_Click(object sender, EventArgs e) { Tex.UnpackFull_D(inputdir, outputdir, rework_pic); }

        private void buttonRepack_Click(object sender, EventArgs e) {
            string oribin = inputfile;
            string workingdir = Path.GetDirectoryName(inputfile);
            string unpack = workingdir + "\\unpack\\";
            string decompress = workingdir + "\\decompress\\";
            string newbin = inputfile + "_new";
            Tex.Repack(unpack, decompress, newbin, oribin);
        }

        private void Aio_3_Click(object sender, EventArgs e) {
            string workingdir = inputdir;
            string unpackdir = workingdir + "\\unpack\\";
            string decompressdir = workingdir + "\\decompress\\";
            string pngdir = workingdir + "\\png\\";
            string oribin = workingdir + "\\" + Path.GetFileName(workingdir) + ".bin";
            string outputfile = oribin + "_new";
            Tex.RepackFull(pngdir, decompressdir, unpackdir, oribin, outputfile, rework_pic, rework_lzss);
        }

        private void Aio_4_Click(object sender, EventArgs e) { Tex.RepackFull_D(inputdir, outputdir, rework_pic, rework_lzss); }

        private void ifsTest_Click(object sender, EventArgs e) {
            IFS.SetExe(Environment.CurrentDirectory + "\\dumpImgFs.exe", Environment.CurrentDirectory + "\\buildImgFS.exe");
            string outputifs = outputdir + "\\" + dstIfsID.Text + ".ifs";
            IFS.Refract(inputfile, outputifs);
        }

        private void ifsList_Click(object sender, EventArgs e) {
            IFS.SetExe(Environment.CurrentDirectory + "\\dumpImgFs.exe", Environment.CurrentDirectory + "\\buildImgFS.exe", this);
            Thread ps = new Thread(new ParameterizedThreadStart(RefractHandler));
            ps.IsBackground = true;
            ps.Start(new TransPara(inputfile, inputdir, outputdir));
        }

        struct TransPara {
            public string Inputfile;
            public string Outputdir;
            public string Inputdir;

            public TransPara(string infile, string indir, string outdir) {
                Inputfile = infile;
                Outputdir = outdir;
                Inputdir = indir;
            }
        }

        void RefractHandler(object param) {
            TransPara para = (TransPara) param;

            IFS.RefractList(para.Inputfile, para.Inputdir, para.Outputdir);
        }

        public delegate void BlankDelegate();

        public delegate void StringDelegate(string s);

        public void Log(string s) {
            if (this.InvokeRequired) {
                StringDelegate setpos = Log; //实例化
                this.Invoke(setpos, s); //调用
            }
            else {
                this.textBox1.Text += s + Environment.NewLine; //本体 
            }
        }

        public void Counter() {
            if (this.InvokeRequired) {
                BlankDelegate setpos = Counter; //实例化
                this.Invoke(setpos); //调用
            }
            else {
                this.label1.Text = $" {++i}file(s) finished..."; //本体 
            }
        }

        private void texList_Click(object sender, EventArgs e) { Refractor.RefractTEX(inputfile, inputdir, outputdir, rework_pic); }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) { rework_pic = checkBox1.Checked; }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) { rework_lzss = checkBox2.Checked; }
    }
}