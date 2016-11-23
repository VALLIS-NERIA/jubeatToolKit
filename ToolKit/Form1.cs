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

namespace ToolKit {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        string inputfile;
        string inputdir;
        string outputdir;

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

        private void inputFile_TextChanged(object sender, EventArgs e) {
            inputfile = inputFile.Text;
        }

        private void inputDirectory_TextChanged(object sender, EventArgs e) {
            inputdir = inputDirectory.Text;
        }

        private void outputDirectory_TextChanged(object sender, EventArgs e) {
            outputdir = outputDirectory.Text;
        }

        private void buttonUnpack_Click(object sender, EventArgs e) {
            Tex.Unpack(inputfile, outputdir);
        }

        private void buttonDecompressMT_Click(object sender, EventArgs e) {
            Tex.LzssDir(1, inputdir, outputdir);
        }

        private void buttonDecode_Click(object sender, EventArgs e) {
            Picture.DecodeDir(inputdir, outputdir);
        }

        private void buttonEncode_Click(object sender, EventArgs e) {
            Picture.EncodeDir(inputdir, outputdir);
        }

        private void Aio_1_Click(object sender, EventArgs e) {
            Tex.UnpackFull(inputfile, outputdir);
        }

        private void Aio_2_Click(object sender, EventArgs e) {
            Tex.UnpackFull_D(inputdir, outputdir);
        }

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
            string outputfile = oribin+"_new";
            Tex.RepackFull(pngdir, decompressdir, unpackdir, oribin, outputfile);
        }

        private void Aio_4_Click(object sender, EventArgs e) {
            Tex.RepackFull_D(inputdir, outputdir);
        }

        private void ifsTest_Click(object sender, EventArgs e) {
            IFS.SetExe(Environment.CurrentDirectory + "\\dumpImgFs.exe", Environment.CurrentDirectory + "\\buildImgFS.exe");
            string outputifs = outputdir + "\\" + dstIfsID.Text + ".ifs";
            IFS.Refract(inputfile, outputifs);
        }
    }
}
