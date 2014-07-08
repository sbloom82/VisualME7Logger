namespace VisualME7Logger
{
    partial class EEPromForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EEPromForm));
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAppPath = new System.Windows.Forms.TextBox();
            this.txtBinPath = new System.Windows.Forms.TextBox();
            this.btnAppPath = new System.Windows.Forms.Button();
            this.btnBinPath = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.txtCOMPort = new System.Windows.Forms.TextBox();
            this.cmbBaudrate = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.btnReadBootmode = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.chkFixDeathCode = new System.Windows.Forms.CheckBox();
            this.chkCorrectChecksums = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtImmoData = new System.Windows.Forms.TextBox();
            this.txtImmoID = new System.Windows.Forms.TextBox();
            this.chkImmoDisabled = new System.Windows.Forms.CheckBox();
            this.chkImmoEnabled = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVIN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSKC = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(9, 273);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(535, 137);
            this.txtOutput.TabIndex = 6;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(470, 416);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(389, 416);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "EEProm Application Path (me7_95040.exe)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Bin Path";
            // 
            // txtAppPath
            // 
            this.txtAppPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppPath.Location = new System.Drawing.Point(9, 29);
            this.txtAppPath.Name = "txtAppPath";
            this.txtAppPath.Size = new System.Drawing.Size(535, 20);
            this.txtAppPath.TabIndex = 0;
            this.txtAppPath.TextChanged += new System.EventHandler(this.txtAppPath_TextChanged);
            // 
            // txtBinPath
            // 
            this.txtBinPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBinPath.Location = new System.Drawing.Point(9, 76);
            this.txtBinPath.Name = "txtBinPath";
            this.txtBinPath.Size = new System.Drawing.Size(535, 20);
            this.txtBinPath.TabIndex = 2;
            this.txtBinPath.TextChanged += new System.EventHandler(this.txtBinPath_TextChanged);
            // 
            // btnAppPath
            // 
            this.btnAppPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAppPath.Location = new System.Drawing.Point(469, 4);
            this.btnAppPath.Name = "btnAppPath";
            this.btnAppPath.Size = new System.Drawing.Size(75, 23);
            this.btnAppPath.TabIndex = 1;
            this.btnAppPath.Text = "Choose";
            this.btnAppPath.UseVisualStyleBackColor = true;
            this.btnAppPath.Click += new System.EventHandler(this.btnAppPath_Click);
            // 
            // btnBinPath
            // 
            this.btnBinPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBinPath.Location = new System.Drawing.Point(469, 51);
            this.btnBinPath.Name = "btnBinPath";
            this.btnBinPath.Size = new System.Drawing.Size(75, 23);
            this.btnBinPath.TabIndex = 3;
            this.btnBinPath.Text = "Choose";
            this.btnBinPath.UseVisualStyleBackColor = true;
            this.btnBinPath.Click += new System.EventHandler(this.btnBinPath_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(9, 38);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(133, 23);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "Read (OBD)";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(9, 88);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(133, 23);
            this.btnWrite.TabIndex = 3;
            this.btnWrite.Text = "Write (Bootmode)";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // txtCOMPort
            // 
            this.txtCOMPort.Location = new System.Drawing.Point(67, 115);
            this.txtCOMPort.Name = "txtCOMPort";
            this.txtCOMPort.Size = new System.Drawing.Size(75, 20);
            this.txtCOMPort.TabIndex = 4;
            this.txtCOMPort.Text = "1";
            this.txtCOMPort.TextChanged += new System.EventHandler(this.txtCOMPort_TextChanged);
            // 
            // cmbBaudrate
            // 
            this.cmbBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudrate.FormattingEnabled = true;
            this.cmbBaudrate.Items.AddRange(new object[] {
            "(Default)",
            "9600",
            "10400",
            "19200",
            "57600"});
            this.cmbBaudrate.Location = new System.Drawing.Point(67, 140);
            this.cmbBaudrate.Name = "cmbBaudrate";
            this.cmbBaudrate.Size = new System.Drawing.Size(75, 21);
            this.cmbBaudrate.TabIndex = 5;
            this.cmbBaudrate.SelectedIndexChanged += new System.EventHandler(this.cmbBaudrate_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReadFile);
            this.groupBox1.Controls.Add(this.btnReadBootmode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnRead);
            this.groupBox1.Controls.Add(this.cmbBaudrate);
            this.groupBox1.Controls.Add(this.btnWrite);
            this.groupBox1.Controls.Add(this.txtCOMPort);
            this.groupBox1.Location = new System.Drawing.Point(9, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 166);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Read/Write";
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(9, 13);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(133, 23);
            this.btnReadFile.TabIndex = 0;
            this.btnReadFile.Text = "Read (File)";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btnReadBootmode
            // 
            this.btnReadBootmode.Location = new System.Drawing.Point(9, 63);
            this.btnReadBootmode.Name = "btnReadBootmode";
            this.btnReadBootmode.Size = new System.Drawing.Size(133, 23);
            this.btnReadBootmode.TabIndex = 2;
            this.btnReadBootmode.Text = "Read (Bootmode)";
            this.btnReadBootmode.UseVisualStyleBackColor = true;
            this.btnReadBootmode.Click += new System.EventHandler(this.btnReadBootmode_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Baudrate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "COM Port:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSaveToFile);
            this.groupBox3.Controls.Add(this.chkFixDeathCode);
            this.groupBox3.Controls.Add(this.chkCorrectChecksums);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtImmoData);
            this.groupBox3.Controls.Add(this.txtImmoID);
            this.groupBox3.Controls.Add(this.chkImmoDisabled);
            this.groupBox3.Controls.Add(this.chkImmoEnabled);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtVIN);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtSKC);
            this.groupBox3.Location = new System.Drawing.Point(165, 101);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(380, 166);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EEProm Data";
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(83, 138);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(164, 23);
            this.btnSaveToFile.TabIndex = 8;
            this.btnSaveToFile.Text = "Save Changes to File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // chkFixDeathCode
            // 
            this.chkFixDeathCode.AutoSize = true;
            this.chkFixDeathCode.Location = new System.Drawing.Point(253, 17);
            this.chkFixDeathCode.Name = "chkFixDeathCode";
            this.chkFixDeathCode.Size = new System.Drawing.Size(99, 17);
            this.chkFixDeathCode.TabIndex = 1;
            this.chkFixDeathCode.Text = "Fix Death Code";
            this.chkFixDeathCode.UseVisualStyleBackColor = true;
            // 
            // chkCorrectChecksums
            // 
            this.chkCorrectChecksums.AutoSize = true;
            this.chkCorrectChecksums.Location = new System.Drawing.Point(253, 142);
            this.chkCorrectChecksums.Name = "chkCorrectChecksums";
            this.chkCorrectChecksums.Size = new System.Drawing.Size(118, 17);
            this.chkCorrectChecksums.TabIndex = 6;
            this.chkCorrectChecksums.Text = "Correct Checksums";
            this.chkCorrectChecksums.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Immo Data:";
            // 
            // txtImmoData
            // 
            this.txtImmoData.Location = new System.Drawing.Point(84, 115);
            this.txtImmoData.MaxLength = 20;
            this.txtImmoData.Name = "txtImmoData";
            this.txtImmoData.Size = new System.Drawing.Size(163, 20);
            this.txtImmoData.TabIndex = 7;
            // 
            // txtImmoID
            // 
            this.txtImmoID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtImmoID.Location = new System.Drawing.Point(84, 90);
            this.txtImmoID.MaxLength = 14;
            this.txtImmoID.Name = "txtImmoID";
            this.txtImmoID.Size = new System.Drawing.Size(163, 20);
            this.txtImmoID.TabIndex = 5;
            // 
            // chkImmoDisabled
            // 
            this.chkImmoDisabled.AutoSize = true;
            this.chkImmoDisabled.Location = new System.Drawing.Point(154, 66);
            this.chkImmoDisabled.Name = "chkImmoDisabled";
            this.chkImmoDisabled.Size = new System.Drawing.Size(67, 17);
            this.chkImmoDisabled.TabIndex = 4;
            this.chkImmoDisabled.Text = "Disabled";
            this.chkImmoDisabled.UseVisualStyleBackColor = true;
            this.chkImmoDisabled.CheckedChanged += new System.EventHandler(this.chkImmoDisabled_CheckedChanged);
            // 
            // chkImmoEnabled
            // 
            this.chkImmoEnabled.AutoSize = true;
            this.chkImmoEnabled.Location = new System.Drawing.Point(84, 66);
            this.chkImmoEnabled.Name = "chkImmoEnabled";
            this.chkImmoEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkImmoEnabled.TabIndex = 3;
            this.chkImmoEnabled.Text = "Enabled";
            this.chkImmoEnabled.UseVisualStyleBackColor = true;
            this.chkImmoEnabled.CheckedChanged += new System.EventHandler(this.chkImmoEnabled_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Immobilizer:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Immobilizer ID:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "VIN:";
            // 
            // txtVIN
            // 
            this.txtVIN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVIN.Location = new System.Drawing.Point(84, 15);
            this.txtVIN.MaxLength = 17;
            this.txtVIN.Name = "txtVIN";
            this.txtVIN.Size = new System.Drawing.Size(163, 20);
            this.txtVIN.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "SKC:";
            // 
            // txtSKC
            // 
            this.txtSKC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSKC.Location = new System.Drawing.Point(84, 40);
            this.txtSKC.MaxLength = 5;
            this.txtSKC.Name = "txtSKC";
            this.txtSKC.Size = new System.Drawing.Size(163, 20);
            this.txtSKC.TabIndex = 2;
            // 
            // EEPromForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(552, 443);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBinPath);
            this.Controls.Add(this.btnAppPath);
            this.Controls.Add(this.txtBinPath);
            this.Controls.Add(this.txtAppPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EEPromForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EEProm Tools";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAppPath;
        private System.Windows.Forms.TextBox txtBinPath;
        private System.Windows.Forms.Button btnAppPath;
        private System.Windows.Forms.Button btnBinPath;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.TextBox txtCOMPort;
        private System.Windows.Forms.ComboBox cmbBaudrate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReadBootmode;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSKC;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtVIN;
        private System.Windows.Forms.TextBox txtImmoID;
        private System.Windows.Forms.CheckBox chkImmoDisabled;
        private System.Windows.Forms.CheckBox chkImmoEnabled;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtImmoData;
        private System.Windows.Forms.CheckBox chkFixDeathCode;
        private System.Windows.Forms.CheckBox chkCorrectChecksums;
        private System.Windows.Forms.Button btnSaveToFile;
    }
}