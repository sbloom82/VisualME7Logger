namespace VisualME7Logger
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radLogFile = new System.Windows.Forms.RadioButton();
            this.txtFTDIInfo = new System.Windows.Forms.TextBox();
            this.chkFTDILocation = new System.Windows.Forms.CheckBox();
            this.chkFTDIDesc = new System.Windows.Forms.CheckBox();
            this.chkFTDISerial = new System.Windows.Forms.CheckBox();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.chkOverrideBaudRate = new System.Windows.Forms.CheckBox();
            this.txtCOMPort = new System.Windows.Forms.TextBox();
            this.radFTDI = new System.Windows.Forms.RadioButton();
            this.radCommCOMPort = new System.Windows.Forms.RadioButton();
            this.radCommDefault = new System.Windows.Forms.RadioButton();
            this.gpOther = new System.Windows.Forms.GroupBox();
            this.chkReadSingleMeasurement = new System.Windows.Forms.CheckBox();
            this.chkWriteAbsoluteTimeStamp = new System.Windows.Forms.CheckBox();
            this.nudSampleRate = new System.Windows.Forms.NumericUpDown();
            this.chkTimeSync = new System.Windows.Forms.CheckBox();
            this.chkWriteToLogRealTime = new System.Windows.Forms.CheckBox();
            this.chkOverrideSampleRate = new System.Windows.Forms.CheckBox();
            this.chkWriteOutputToFile = new System.Windows.Forms.CheckBox();
            this.txtLogFilePath = new System.Windows.Forms.TextBox();
            this.chkWriteToLog = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkUseDefaultLogFile = new System.Windows.Forms.CheckBox();
            this.btnChooseLogPath = new System.Windows.Forms.Button();
            this.chkDisableRealTimeDisplay = new System.Windows.Forms.CheckBox();
            this.gbTroubleshooting = new System.Windows.Forms.GroupBox();
            this.chkTailLogWithME7L = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.gpOther.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSampleRate)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.gbTroubleshooting.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radLogFile);
            this.groupBox1.Controls.Add(this.txtFTDIInfo);
            this.groupBox1.Controls.Add(this.chkFTDILocation);
            this.groupBox1.Controls.Add(this.chkFTDIDesc);
            this.groupBox1.Controls.Add(this.chkFTDISerial);
            this.groupBox1.Controls.Add(this.cmbBaudRate);
            this.groupBox1.Controls.Add(this.chkOverrideBaudRate);
            this.groupBox1.Controls.Add(this.txtCOMPort);
            this.groupBox1.Controls.Add(this.radFTDI);
            this.groupBox1.Controls.Add(this.radCommCOMPort);
            this.groupBox1.Controls.Add(this.radCommDefault);
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Communication";
            // 
            // radLogFile
            // 
            this.radLogFile.AutoSize = true;
            this.radLogFile.Location = new System.Drawing.Point(6, 35);
            this.radLogFile.Name = "radLogFile";
            this.radLogFile.Size = new System.Drawing.Size(62, 17);
            this.radLogFile.TabIndex = 1;
            this.radLogFile.Text = "Log File";
            this.radLogFile.UseVisualStyleBackColor = true;
            this.radLogFile.CheckedChanged += new System.EventHandler(this.radLogFile_CheckedChanged);
            // 
            // txtFTDIInfo
            // 
            this.txtFTDIInfo.Location = new System.Drawing.Point(265, 73);
            this.txtFTDIInfo.Name = "txtFTDIInfo";
            this.txtFTDIInfo.Size = new System.Drawing.Size(114, 20);
            this.txtFTDIInfo.TabIndex = 8;
            // 
            // chkFTDILocation
            // 
            this.chkFTDILocation.AutoSize = true;
            this.chkFTDILocation.Location = new System.Drawing.Point(192, 76);
            this.chkFTDILocation.Name = "chkFTDILocation";
            this.chkFTDILocation.Size = new System.Drawing.Size(67, 17);
            this.chkFTDILocation.TabIndex = 7;
            this.chkFTDILocation.Text = "Location";
            this.chkFTDILocation.UseVisualStyleBackColor = true;
            this.chkFTDILocation.CheckedChanged += new System.EventHandler(this.chkFTDILocation_CheckedChanged);
            // 
            // chkFTDIDesc
            // 
            this.chkFTDIDesc.AutoSize = true;
            this.chkFTDIDesc.Location = new System.Drawing.Point(113, 76);
            this.chkFTDIDesc.Name = "chkFTDIDesc";
            this.chkFTDIDesc.Size = new System.Drawing.Size(79, 17);
            this.chkFTDIDesc.TabIndex = 6;
            this.chkFTDIDesc.Text = "Description";
            this.chkFTDIDesc.UseVisualStyleBackColor = true;
            this.chkFTDIDesc.CheckedChanged += new System.EventHandler(this.chkFTDIDesc_CheckedChanged);
            // 
            // chkFTDISerial
            // 
            this.chkFTDISerial.AutoSize = true;
            this.chkFTDISerial.Location = new System.Drawing.Point(61, 76);
            this.chkFTDISerial.Name = "chkFTDISerial";
            this.chkFTDISerial.Size = new System.Drawing.Size(52, 17);
            this.chkFTDISerial.TabIndex = 5;
            this.chkFTDISerial.Text = "Serial";
            this.chkFTDISerial.UseVisualStyleBackColor = true;
            this.chkFTDISerial.CheckedChanged += new System.EventHandler(this.chkFTDISerial_CheckedChanged);
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "10400",
            "14400",
            "19200",
            "38400",
            "56000",
            "76800",
            "125000"});
            this.cmbBaudRate.Location = new System.Drawing.Point(129, 97);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(121, 21);
            this.cmbBaudRate.TabIndex = 10;
            // 
            // chkOverrideBaudRate
            // 
            this.chkOverrideBaudRate.AutoSize = true;
            this.chkOverrideBaudRate.Location = new System.Drawing.Point(6, 99);
            this.chkOverrideBaudRate.Name = "chkOverrideBaudRate";
            this.chkOverrideBaudRate.Size = new System.Drawing.Size(117, 17);
            this.chkOverrideBaudRate.TabIndex = 9;
            this.chkOverrideBaudRate.Text = "Override baud rate:";
            this.chkOverrideBaudRate.UseVisualStyleBackColor = true;
            this.chkOverrideBaudRate.CheckedChanged += new System.EventHandler(this.chkOverrideBaudRate_CheckedChanged);
            // 
            // txtCOMPort
            // 
            this.txtCOMPort.Location = new System.Drawing.Point(86, 54);
            this.txtCOMPort.Name = "txtCOMPort";
            this.txtCOMPort.Size = new System.Drawing.Size(114, 20);
            this.txtCOMPort.TabIndex = 3;
            this.txtCOMPort.Text = "COM1";
            // 
            // radFTDI
            // 
            this.radFTDI.AutoSize = true;
            this.radFTDI.Location = new System.Drawing.Point(6, 75);
            this.radFTDI.Name = "radFTDI";
            this.radFTDI.Size = new System.Drawing.Size(52, 17);
            this.radFTDI.TabIndex = 4;
            this.radFTDI.Text = "FTDI:";
            this.radFTDI.UseVisualStyleBackColor = true;
            this.radFTDI.CheckedChanged += new System.EventHandler(this.radFTDI_CheckedChanged);
            // 
            // radCommCOMPort
            // 
            this.radCommCOMPort.AutoSize = true;
            this.radCommCOMPort.Location = new System.Drawing.Point(6, 55);
            this.radCommCOMPort.Name = "radCommCOMPort";
            this.radCommCOMPort.Size = new System.Drawing.Size(74, 17);
            this.radCommCOMPort.TabIndex = 2;
            this.radCommCOMPort.Text = "COM Port:";
            this.radCommCOMPort.UseVisualStyleBackColor = true;
            this.radCommCOMPort.CheckedChanged += new System.EventHandler(this.radCommCOMPort_CheckedChanged);
            // 
            // radCommDefault
            // 
            this.radCommDefault.AutoSize = true;
            this.radCommDefault.Checked = true;
            this.radCommDefault.Location = new System.Drawing.Point(6, 15);
            this.radCommDefault.Name = "radCommDefault";
            this.radCommDefault.Size = new System.Drawing.Size(59, 17);
            this.radCommDefault.TabIndex = 0;
            this.radCommDefault.TabStop = true;
            this.radCommDefault.Text = "Default";
            this.radCommDefault.UseVisualStyleBackColor = true;
            this.radCommDefault.CheckedChanged += new System.EventHandler(this.radCommDefault_CheckedChanged);
            // 
            // gpOther
            // 
            this.gpOther.Controls.Add(this.chkReadSingleMeasurement);
            this.gpOther.Controls.Add(this.chkWriteAbsoluteTimeStamp);
            this.gpOther.Controls.Add(this.nudSampleRate);
            this.gpOther.Controls.Add(this.chkTimeSync);
            this.gpOther.Controls.Add(this.chkWriteToLogRealTime);
            this.gpOther.Controls.Add(this.chkOverrideSampleRate);
            this.gpOther.Location = new System.Drawing.Point(3, 220);
            this.gpOther.Name = "gpOther";
            this.gpOther.Size = new System.Drawing.Size(424, 133);
            this.gpOther.TabIndex = 2;
            this.gpOther.TabStop = false;
            this.gpOther.Text = "Other";
            // 
            // chkReadSingleMeasurement
            // 
            this.chkReadSingleMeasurement.AutoSize = true;
            this.chkReadSingleMeasurement.Location = new System.Drawing.Point(6, 111);
            this.chkReadSingleMeasurement.Name = "chkReadSingleMeasurement";
            this.chkReadSingleMeasurement.Size = new System.Drawing.Size(195, 17);
            this.chkReadSingleMeasurement.TabIndex = 5;
            this.chkReadSingleMeasurement.Text = "Read single measurement then stop";
            this.chkReadSingleMeasurement.UseVisualStyleBackColor = true;
            // 
            // chkWriteAbsoluteTimeStamp
            // 
            this.chkWriteAbsoluteTimeStamp.AutoSize = true;
            this.chkWriteAbsoluteTimeStamp.Location = new System.Drawing.Point(6, 88);
            this.chkWriteAbsoluteTimeStamp.Name = "chkWriteAbsoluteTimeStamp";
            this.chkWriteAbsoluteTimeStamp.Size = new System.Drawing.Size(144, 17);
            this.chkWriteAbsoluteTimeStamp.TabIndex = 4;
            this.chkWriteAbsoluteTimeStamp.Text = "Write absolute timestamp";
            this.chkWriteAbsoluteTimeStamp.UseVisualStyleBackColor = true;
            // 
            // nudSampleRate
            // 
            this.nudSampleRate.Location = new System.Drawing.Point(145, 18);
            this.nudSampleRate.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudSampleRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSampleRate.Name = "nudSampleRate";
            this.nudSampleRate.Size = new System.Drawing.Size(55, 20);
            this.nudSampleRate.TabIndex = 1;
            this.nudSampleRate.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // chkTimeSync
            // 
            this.chkTimeSync.AutoSize = true;
            this.chkTimeSync.Location = new System.Drawing.Point(6, 65);
            this.chkTimeSync.Name = "chkTimeSync";
            this.chkTimeSync.Size = new System.Drawing.Size(324, 17);
            this.chkTimeSync.TabIndex = 3;
            this.chkTimeSync.Text = "Time Sync (Logging will start on next full second of system time)";
            this.chkTimeSync.UseVisualStyleBackColor = true;
            // 
            // chkWriteToLogRealTime
            // 
            this.chkWriteToLogRealTime.AutoSize = true;
            this.chkWriteToLogRealTime.Location = new System.Drawing.Point(6, 42);
            this.chkWriteToLogRealTime.Name = "chkWriteToLogRealTime";
            this.chkWriteToLogRealTime.Size = new System.Drawing.Size(154, 17);
            this.chkWriteToLogRealTime.TabIndex = 2;
            this.chkWriteToLogRealTime.Text = "Write data to log in realtime";
            this.chkWriteToLogRealTime.UseVisualStyleBackColor = true;
            // 
            // chkOverrideSampleRate
            // 
            this.chkOverrideSampleRate.AutoSize = true;
            this.chkOverrideSampleRate.Location = new System.Drawing.Point(6, 19);
            this.chkOverrideSampleRate.Name = "chkOverrideSampleRate";
            this.chkOverrideSampleRate.Size = new System.Drawing.Size(126, 17);
            this.chkOverrideSampleRate.TabIndex = 0;
            this.chkOverrideSampleRate.Text = "Override sample rate:";
            this.chkOverrideSampleRate.UseVisualStyleBackColor = true;
            this.chkOverrideSampleRate.CheckedChanged += new System.EventHandler(this.chkOverrideSampleRate_CheckedChanged);
            // 
            // chkWriteOutputToFile
            // 
            this.chkWriteOutputToFile.AutoSize = true;
            this.chkWriteOutputToFile.Location = new System.Drawing.Point(6, 39);
            this.chkWriteOutputToFile.Name = "chkWriteOutputToFile";
            this.chkWriteOutputToFile.Size = new System.Drawing.Size(170, 17);
            this.chkWriteOutputToFile.TabIndex = 2;
            this.chkWriteOutputToFile.Text = "Write ME7Logger output to file";
            this.chkWriteOutputToFile.UseVisualStyleBackColor = true;
            // 
            // txtLogFilePath
            // 
            this.txtLogFilePath.Location = new System.Drawing.Point(6, 64);
            this.txtLogFilePath.Name = "txtLogFilePath";
            this.txtLogFilePath.Size = new System.Drawing.Size(329, 20);
            this.txtLogFilePath.TabIndex = 2;
            // 
            // chkWriteToLog
            // 
            this.chkWriteToLog.AutoSize = true;
            this.chkWriteToLog.Checked = true;
            this.chkWriteToLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWriteToLog.Location = new System.Drawing.Point(6, 19);
            this.chkWriteToLog.Name = "chkWriteToLog";
            this.chkWriteToLog.Size = new System.Drawing.Size(96, 17);
            this.chkWriteToLog.TabIndex = 0;
            this.chkWriteToLog.Text = "Write log to file";
            this.chkWriteToLog.UseVisualStyleBackColor = true;
            this.chkWriteToLog.CheckedChanged += new System.EventHandler(this.chkWriteToLog_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(344, 422);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(263, 422);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkUseDefaultLogFile);
            this.groupBox3.Controls.Add(this.btnChooseLogPath);
            this.groupBox3.Controls.Add(this.chkWriteToLog);
            this.groupBox3.Controls.Add(this.txtLogFilePath);
            this.groupBox3.Location = new System.Drawing.Point(3, 127);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(423, 92);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log File";
            // 
            // chkUseDefaultLogFile
            // 
            this.chkUseDefaultLogFile.AutoSize = true;
            this.chkUseDefaultLogFile.Checked = true;
            this.chkUseDefaultLogFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseDefaultLogFile.Location = new System.Drawing.Point(6, 41);
            this.chkUseDefaultLogFile.Name = "chkUseDefaultLogFile";
            this.chkUseDefaultLogFile.Size = new System.Drawing.Size(201, 17);
            this.chkUseDefaultLogFile.TabIndex = 1;
            this.chkUseDefaultLogFile.Text = "Use default log file \'config_date_time\'";
            this.chkUseDefaultLogFile.UseVisualStyleBackColor = true;
            this.chkUseDefaultLogFile.CheckedChanged += new System.EventHandler(this.chkUseDefaultLogFile_CheckedChanged);
            // 
            // btnChooseLogPath
            // 
            this.btnChooseLogPath.Location = new System.Drawing.Point(341, 63);
            this.btnChooseLogPath.Name = "btnChooseLogPath";
            this.btnChooseLogPath.Size = new System.Drawing.Size(75, 23);
            this.btnChooseLogPath.TabIndex = 3;
            this.btnChooseLogPath.Text = "Choose";
            this.btnChooseLogPath.UseVisualStyleBackColor = true;
            this.btnChooseLogPath.Click += new System.EventHandler(this.btnChooseLogPath_Click);
            // 
            // chkDisableRealTimeDisplay
            // 
            this.chkDisableRealTimeDisplay.AutoSize = true;
            this.chkDisableRealTimeDisplay.Location = new System.Drawing.Point(6, 16);
            this.chkDisableRealTimeDisplay.Name = "chkDisableRealTimeDisplay";
            this.chkDisableRealTimeDisplay.Size = new System.Drawing.Size(149, 17);
            this.chkDisableRealTimeDisplay.TabIndex = 0;
            this.chkDisableRealTimeDisplay.Text = "Disable Real Time Display";
            this.chkDisableRealTimeDisplay.UseVisualStyleBackColor = true;
            // 
            // gbTroubleshooting
            // 
            this.gbTroubleshooting.Controls.Add(this.chkTailLogWithME7L);
            this.gbTroubleshooting.Controls.Add(this.chkDisableRealTimeDisplay);
            this.gbTroubleshooting.Controls.Add(this.chkWriteOutputToFile);
            this.gbTroubleshooting.Location = new System.Drawing.Point(3, 354);
            this.gbTroubleshooting.Name = "gbTroubleshooting";
            this.gbTroubleshooting.Size = new System.Drawing.Size(424, 62);
            this.gbTroubleshooting.TabIndex = 3;
            this.gbTroubleshooting.TabStop = false;
            this.gbTroubleshooting.Text = "Troubleshooting";
            // 
            // chkTailLogWithME7L
            // 
            this.chkTailLogWithME7L.AutoSize = true;
            this.chkTailLogWithME7L.Location = new System.Drawing.Point(192, 16);
            this.chkTailLogWithME7L.Name = "chkTailLogWithME7L";
            this.chkTailLogWithME7L.Size = new System.Drawing.Size(194, 17);
            this.chkTailLogWithME7L.TabIndex = 1;
            this.chkTailLogWithME7L.Text = "Tail Log File With VisualME7Logger";
            this.chkTailLogWithME7L.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(431, 449);
            this.ControlBox = false;
            this.Controls.Add(this.gbTroubleshooting);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gpOther);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visual ME7Logger Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpOther.ResumeLayout(false);
            this.gpOther.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSampleRate)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.gbTroubleshooting.ResumeLayout(false);
            this.gbTroubleshooting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCOMPort;
        private System.Windows.Forms.RadioButton radFTDI;
        private System.Windows.Forms.RadioButton radCommCOMPort;
        private System.Windows.Forms.RadioButton radCommDefault;
        private System.Windows.Forms.GroupBox gpOther;
        private System.Windows.Forms.CheckBox chkReadSingleMeasurement;
        private System.Windows.Forms.CheckBox chkWriteAbsoluteTimeStamp;
        private System.Windows.Forms.NumericUpDown nudSampleRate;
        private System.Windows.Forms.CheckBox chkTimeSync;
        private System.Windows.Forms.CheckBox chkWriteToLogRealTime;
        private System.Windows.Forms.CheckBox chkOverrideSampleRate;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtLogFilePath;
        private System.Windows.Forms.CheckBox chkWriteToLog;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.CheckBox chkOverrideBaudRate;
        private System.Windows.Forms.TextBox txtFTDIInfo;
        private System.Windows.Forms.CheckBox chkFTDILocation;
        private System.Windows.Forms.CheckBox chkFTDIDesc;
        private System.Windows.Forms.CheckBox chkFTDISerial;
        private System.Windows.Forms.RadioButton radLogFile;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnChooseLogPath;
        private System.Windows.Forms.CheckBox chkWriteOutputToFile;
        private System.Windows.Forms.CheckBox chkDisableRealTimeDisplay;
        private System.Windows.Forms.GroupBox gbTroubleshooting;
        private System.Windows.Forms.CheckBox chkTailLogWithME7L;
        private System.Windows.Forms.CheckBox chkUseDefaultLogFile;
    }
}