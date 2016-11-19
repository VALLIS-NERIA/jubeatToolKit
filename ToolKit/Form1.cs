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

        private void buttonBrowseInputFile_Click(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
            inputFile.Text = openFileDialog1.FileName;

        }

        private void buttonBrowseOutputDir_Click(object sender, EventArgs e) {
            folderBrowserDialogOut.ShowDialog();
            outputDirectory.Text = folderBrowserDialogOut.SelectedPath;
        }

        private void buttonUnpack_Click(object sender, EventArgs e) {
            Tex.Unpack(inputFile.Text, outputDirectory.Text);
        }

        private void buttonBrowseInputDir_Click(object sender, EventArgs e) {
            folderBrowserDialogIn.ShowDialog();
            inputDirectory.Text = folderBrowserDialogIn.SelectedPath;
        }

        private void buttonDecompressMT_Click(object sender, EventArgs e) {
            Tex.LzssDir(1, inputDirectory.Text, outputDirectory.Text);
        }

        private void buttonDecode_Click(object sender, EventArgs e) {
            Tex.Picture.DecodeDir(inputDirectory.Text, outputDirectory.Text, 4);
        }

        private void buttonEncode_Click(object sender, EventArgs e) {
            Tex.Picture.EncodeDir(inputDirectory.Text, outputDirectory.Text, 4);
        }

        private void Aio_1_Click(object sender, EventArgs e) {
            Tex.Aio_U(inputFile.Text, outputDirectory.Text);
        }

        private void Aio_2_Click(object sender, EventArgs e) {
            Tex.Aio_U_D(inputDirectory.Text, outputDirectory.Text, 4);
        }
    }
}
