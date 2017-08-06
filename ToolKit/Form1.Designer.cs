namespace ToolKit {
    partial class Form1 {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.inputFile = new System.Windows.Forms.TextBox();
            this.outputDirectory = new System.Windows.Forms.TextBox();
            this.buttonBrowseInputFile = new System.Windows.Forms.Button();
            this.buttonBrowseOutputDir = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonUnpack = new System.Windows.Forms.Button();
            this.folderBrowserDialogOut = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.buttonBrowseInputDir = new System.Windows.Forms.Button();
            this.inputDirectory = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogIn = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonDecompressMT = new System.Windows.Forms.Button();
            this.buttonDecode = new System.Windows.Forms.Button();
            this.buttonEncode = new System.Windows.Forms.Button();
            this.Aio_1 = new System.Windows.Forms.Button();
            this.Aio_2 = new System.Windows.Forms.Button();
            this.buttonRepack = new System.Windows.Forms.Button();
            this.Aio_3 = new System.Windows.Forms.Button();
            this.Aio_4 = new System.Windows.Forms.Button();
            this.ifsTest = new System.Windows.Forms.Button();
            this.dstIfsID = new System.Windows.Forms.TextBox();
            this.ifsList = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.texList = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // inputFile
            // 
            this.inputFile.Location = new System.Drawing.Point(86, 33);
            this.inputFile.Name = "inputFile";
            this.inputFile.Size = new System.Drawing.Size(177, 21);
            this.inputFile.TabIndex = 0;
            this.inputFile.TextChanged += new System.EventHandler(this.inputFile_TextChanged);
            // 
            // outputDirectory
            // 
            this.outputDirectory.Location = new System.Drawing.Point(98, 149);
            this.outputDirectory.Name = "outputDirectory";
            this.outputDirectory.Size = new System.Drawing.Size(177, 21);
            this.outputDirectory.TabIndex = 1;
            this.outputDirectory.TextChanged += new System.EventHandler(this.outputDirectory_TextChanged);
            // 
            // buttonBrowseInputFile
            // 
            this.buttonBrowseInputFile.Location = new System.Drawing.Point(269, 33);
            this.buttonBrowseInputFile.Name = "buttonBrowseInputFile";
            this.buttonBrowseInputFile.Size = new System.Drawing.Size(75, 21);
            this.buttonBrowseInputFile.TabIndex = 2;
            this.buttonBrowseInputFile.Text = "Browse...";
            this.buttonBrowseInputFile.UseVisualStyleBackColor = true;
            this.buttonBrowseInputFile.Click += new System.EventHandler(this.buttonBrowseInputFile_Click);
            // 
            // buttonBrowseOutputDir
            // 
            this.buttonBrowseOutputDir.Location = new System.Drawing.Point(281, 149);
            this.buttonBrowseOutputDir.Name = "buttonBrowseOutputDir";
            this.buttonBrowseOutputDir.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseOutputDir.TabIndex = 3;
            this.buttonBrowseOutputDir.Text = "Browse...";
            this.buttonBrowseOutputDir.UseVisualStyleBackColor = true;
            this.buttonBrowseOutputDir.Click += new System.EventHandler(this.buttonBrowseOutputDir_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonUnpack
            // 
            this.buttonUnpack.Location = new System.Drawing.Point(12, 335);
            this.buttonUnpack.Name = "buttonUnpack";
            this.buttonUnpack.Size = new System.Drawing.Size(75, 45);
            this.buttonUnpack.TabIndex = 4;
            this.buttonUnpack.Text = "Unpack";
            this.buttonUnpack.UseVisualStyleBackColor = true;
            this.buttonUnpack.Click += new System.EventHandler(this.buttonUnpack_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.buttonBrowseInputDir);
            this.groupBox1.Controls.Add(this.buttonBrowseInputFile);
            this.groupBox1.Controls.Add(this.inputDirectory);
            this.groupBox1.Controls.Add(this.inputFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 126);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 83);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(77, 16);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Directory";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 34);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "File";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseInputDir
            // 
            this.buttonBrowseInputDir.Location = new System.Drawing.Point(269, 82);
            this.buttonBrowseInputDir.Name = "buttonBrowseInputDir";
            this.buttonBrowseInputDir.Size = new System.Drawing.Size(75, 21);
            this.buttonBrowseInputDir.TabIndex = 2;
            this.buttonBrowseInputDir.Text = "Browse...";
            this.buttonBrowseInputDir.UseVisualStyleBackColor = true;
            this.buttonBrowseInputDir.Click += new System.EventHandler(this.buttonBrowseInputDir_Click);
            // 
            // inputDirectory
            // 
            this.inputDirectory.Location = new System.Drawing.Point(86, 82);
            this.inputDirectory.Name = "inputDirectory";
            this.inputDirectory.Size = new System.Drawing.Size(177, 21);
            this.inputDirectory.TabIndex = 0;
            this.inputDirectory.TextChanged += new System.EventHandler(this.inputDirectory_TextChanged);
            // 
            // buttonDecompressMT
            // 
            this.buttonDecompressMT.Location = new System.Drawing.Point(98, 335);
            this.buttonDecompressMT.Name = "buttonDecompressMT";
            this.buttonDecompressMT.Size = new System.Drawing.Size(75, 45);
            this.buttonDecompressMT.TabIndex = 6;
            this.buttonDecompressMT.Text = "Decompress4Thread";
            this.buttonDecompressMT.UseVisualStyleBackColor = true;
            this.buttonDecompressMT.Click += new System.EventHandler(this.buttonDecompressMT_Click);
            // 
            // buttonDecode
            // 
            this.buttonDecode.Location = new System.Drawing.Point(180, 335);
            this.buttonDecode.Name = "buttonDecode";
            this.buttonDecode.Size = new System.Drawing.Size(75, 45);
            this.buttonDecode.TabIndex = 7;
            this.buttonDecode.Text = "Decode Image 4Th";
            this.buttonDecode.UseVisualStyleBackColor = true;
            this.buttonDecode.Click += new System.EventHandler(this.buttonDecode_Click);
            // 
            // buttonEncode
            // 
            this.buttonEncode.Location = new System.Drawing.Point(262, 335);
            this.buttonEncode.Name = "buttonEncode";
            this.buttonEncode.Size = new System.Drawing.Size(75, 45);
            this.buttonEncode.TabIndex = 8;
            this.buttonEncode.Text = "Encode";
            this.buttonEncode.UseVisualStyleBackColor = true;
            this.buttonEncode.Click += new System.EventHandler(this.buttonEncode_Click);
            // 
            // Aio_1
            // 
            this.Aio_1.BackColor = System.Drawing.Color.Red;
            this.Aio_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Aio_1.FlatAppearance.BorderSize = 0;
            this.Aio_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Aio_1.Location = new System.Drawing.Point(12, 184);
            this.Aio_1.Name = "Aio_1";
            this.Aio_1.Size = new System.Drawing.Size(83, 46);
            this.Aio_1.TabIndex = 9;
            this.Aio_1.Text = "Don\'t Click";
            this.Aio_1.UseVisualStyleBackColor = false;
            this.Aio_1.Click += new System.EventHandler(this.Aio_1_Click);
            // 
            // Aio_2
            // 
            this.Aio_2.BackColor = System.Drawing.Color.Maroon;
            this.Aio_2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Aio_2.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Aio_2.ForeColor = System.Drawing.Color.Gold;
            this.Aio_2.Location = new System.Drawing.Point(102, 184);
            this.Aio_2.Name = "Aio_2";
            this.Aio_2.Size = new System.Drawing.Size(254, 46);
            this.Aio_2.TabIndex = 10;
            this.Aio_2.Text = "DON\'T EVEN MORE CLICK";
            this.Aio_2.UseVisualStyleBackColor = false;
            this.Aio_2.Click += new System.EventHandler(this.Aio_2_Click);
            // 
            // buttonRepack
            // 
            this.buttonRepack.Location = new System.Drawing.Point(12, 386);
            this.buttonRepack.Name = "buttonRepack";
            this.buttonRepack.Size = new System.Drawing.Size(75, 46);
            this.buttonRepack.TabIndex = 11;
            this.buttonRepack.Text = "Repack";
            this.buttonRepack.UseVisualStyleBackColor = true;
            this.buttonRepack.Click += new System.EventHandler(this.buttonRepack_Click);
            // 
            // Aio_3
            // 
            this.Aio_3.BackColor = System.Drawing.Color.GreenYellow;
            this.Aio_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Aio_3.FlatAppearance.BorderSize = 0;
            this.Aio_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Aio_3.Location = new System.Drawing.Point(12, 236);
            this.Aio_3.Name = "Aio_3";
            this.Aio_3.Size = new System.Drawing.Size(83, 46);
            this.Aio_3.TabIndex = 12;
            this.Aio_3.Text = "Don\'t Click";
            this.Aio_3.UseVisualStyleBackColor = false;
            this.Aio_3.Click += new System.EventHandler(this.Aio_3_Click);
            // 
            // Aio_4
            // 
            this.Aio_4.BackColor = System.Drawing.Color.OliveDrab;
            this.Aio_4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Aio_4.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Aio_4.ForeColor = System.Drawing.Color.Gold;
            this.Aio_4.Location = new System.Drawing.Point(102, 236);
            this.Aio_4.Name = "Aio_4";
            this.Aio_4.Size = new System.Drawing.Size(254, 46);
            this.Aio_4.TabIndex = 13;
            this.Aio_4.Text = "DON\'T EVEN MORE CLICK";
            this.Aio_4.UseVisualStyleBackColor = false;
            this.Aio_4.Click += new System.EventHandler(this.Aio_4_Click);
            // 
            // ifsTest
            // 
            this.ifsTest.Location = new System.Drawing.Point(262, 409);
            this.ifsTest.Name = "ifsTest";
            this.ifsTest.Size = new System.Drawing.Size(75, 23);
            this.ifsTest.TabIndex = 14;
            this.ifsTest.Text = "ifs";
            this.ifsTest.UseVisualStyleBackColor = true;
            this.ifsTest.Click += new System.EventHandler(this.ifsTest_Click);
            // 
            // dstIfsID
            // 
            this.dstIfsID.Location = new System.Drawing.Point(262, 386);
            this.dstIfsID.Name = "dstIfsID";
            this.dstIfsID.Size = new System.Drawing.Size(75, 21);
            this.dstIfsID.TabIndex = 15;
            // 
            // ifsList
            // 
            this.ifsList.Location = new System.Drawing.Point(98, 386);
            this.ifsList.Name = "ifsList";
            this.ifsList.Size = new System.Drawing.Size(75, 46);
            this.ifsList.TabIndex = 16;
            this.ifsList.Text = "IFSLIST";
            this.ifsList.UseVisualStyleBackColor = true;
            this.ifsList.Click += new System.EventHandler(this.ifsList_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(102, 289);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 23);
            this.label1.TabIndex = 17;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // texList
            // 
            this.texList.Location = new System.Drawing.Point(181, 386);
            this.texList.Name = "texList";
            this.texList.Size = new System.Drawing.Size(75, 46);
            this.texList.TabIndex = 18;
            this.texList.Text = "TEXLIST";
            this.texList.UseVisualStyleBackColor = true;
            this.texList.Click += new System.EventHandler(this.texList_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 289);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(42, 16);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "Pic";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(369, 22);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(351, 410);
            this.textBox1.TabIndex = 20;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(12, 312);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 21;
            this.checkBox2.Text = "Lzss";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 454);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.texList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ifsList);
            this.Controls.Add(this.dstIfsID);
            this.Controls.Add(this.ifsTest);
            this.Controls.Add(this.Aio_4);
            this.Controls.Add(this.Aio_3);
            this.Controls.Add(this.buttonRepack);
            this.Controls.Add(this.Aio_2);
            this.Controls.Add(this.Aio_1);
            this.Controls.Add(this.buttonEncode);
            this.Controls.Add(this.buttonDecode);
            this.Controls.Add(this.buttonDecompressMT);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonUnpack);
            this.Controls.Add(this.buttonBrowseOutputDir);
            this.Controls.Add(this.outputDirectory);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputFile;
        private System.Windows.Forms.TextBox outputDirectory;
        private System.Windows.Forms.Button buttonBrowseInputFile;
        private System.Windows.Forms.Button buttonBrowseOutputDir;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonUnpack;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogOut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button buttonBrowseInputDir;
        private System.Windows.Forms.TextBox inputDirectory;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogIn;
        private System.Windows.Forms.Button buttonDecompressMT;
        private System.Windows.Forms.Button buttonDecode;
        private System.Windows.Forms.Button buttonEncode;
        private System.Windows.Forms.Button Aio_1;
        private System.Windows.Forms.Button Aio_2;
        private System.Windows.Forms.Button buttonRepack;
        private System.Windows.Forms.Button Aio_3;
        private System.Windows.Forms.Button Aio_4;
        private System.Windows.Forms.Button ifsTest;
        private System.Windows.Forms.TextBox dstIfsID;
        private System.Windows.Forms.Button ifsList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button texList;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

