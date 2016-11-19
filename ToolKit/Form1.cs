using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void buttonUnpack_Click(object sender, EventArgs e) {
            Tex.Unpack(inputfile, outputdir);
        }

        private void buttonDecompressMT_Click(object sender, EventArgs e) {
            Tex.LzssDir(1, inputdir, outputdir);
        }

        private void buttonDecode_Click(object sender, EventArgs e) {
            Tex.Picture.DecodeDir(inputdir, outputdir, 4);
        }

        private void buttonEncode_Click(object sender, EventArgs e) {
            Tex.Picture.EncodeDir(inputdir, outputdir, 4);
        }

        private void Aio_1_Click(object sender, EventArgs e) {
            Tex.Aio_U(inputfile, outputdir);
        }

        private void Aio_2_Click(object sender, EventArgs e) {
            Tex.Aio_U_D(inputdir, outputdir, 4);
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
    }
}
