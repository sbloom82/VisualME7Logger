namespace VisualME7Logger
{
    partial class SettingsForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadECUFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearConfigFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstAvailMeasurements = new System.Windows.Forms.ListBox();
            this.btnAddMeasurement = new System.Windows.Forms.Button();
            this.btnRemoveMeasurement = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstSelectedMeasurements = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtLogFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConfigFile = new System.Windows.Forms.TextBox();
            this.txtECUFile = new System.Windows.Forms.TextBox();
            this.btnStartLog = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(733, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadECUFileToolStripMenuItem,
            this.loadConfigFileToolStripMenuItem,
            this.clearConfigFileToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadECUFileToolStripMenuItem
            // 
            this.loadECUFileToolStripMenuItem.Name = "loadECUFileToolStripMenuItem";
            this.loadECUFileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.loadECUFileToolStripMenuItem.Text = "Load ECU File";
            this.loadECUFileToolStripMenuItem.Click += new System.EventHandler(this.loadECUFileToolStripMenuItem_Click);
            // 
            // loadConfigFileToolStripMenuItem
            // 
            this.loadConfigFileToolStripMenuItem.Enabled = false;
            this.loadConfigFileToolStripMenuItem.Name = "loadConfigFileToolStripMenuItem";
            this.loadConfigFileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.loadConfigFileToolStripMenuItem.Text = "Load Config File";
            this.loadConfigFileToolStripMenuItem.Click += new System.EventHandler(this.loadConfigFileToolStripMenuItem_Click);
            // 
            // clearConfigFileToolStripMenuItem
            // 
            this.clearConfigFileToolStripMenuItem.Name = "clearConfigFileToolStripMenuItem";
            this.clearConfigFileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.clearConfigFileToolStripMenuItem.Text = "Clear Config File";
            this.clearConfigFileToolStripMenuItem.Click += new System.EventHandler(this.clearConfigFileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // lstAvailMeasurements
            // 
            this.lstAvailMeasurements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstAvailMeasurements.FormattingEnabled = true;
            this.lstAvailMeasurements.Location = new System.Drawing.Point(9, 32);
            this.lstAvailMeasurements.Name = "lstAvailMeasurements";
            this.lstAvailMeasurements.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAvailMeasurements.Size = new System.Drawing.Size(300, 147);
            this.lstAvailMeasurements.TabIndex = 3;
            this.lstAvailMeasurements.SelectedValueChanged += new System.EventHandler(this.lstAvailMeasurements_SelectedValueChanged);
            this.lstAvailMeasurements.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstAvailMeasurements_KeyUp);
            // 
            // btnAddMeasurement
            // 
            this.btnAddMeasurement.Location = new System.Drawing.Point(315, 32);
            this.btnAddMeasurement.Name = "btnAddMeasurement";
            this.btnAddMeasurement.Size = new System.Drawing.Size(75, 23);
            this.btnAddMeasurement.TabIndex = 5;
            this.btnAddMeasurement.Text = ">>";
            this.btnAddMeasurement.UseVisualStyleBackColor = true;
            this.btnAddMeasurement.Click += new System.EventHandler(this.btnAddMeasurement_Click);
            // 
            // btnRemoveMeasurement
            // 
            this.btnRemoveMeasurement.Location = new System.Drawing.Point(315, 61);
            this.btnRemoveMeasurement.Name = "btnRemoveMeasurement";
            this.btnRemoveMeasurement.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveMeasurement.TabIndex = 6;
            this.btnRemoveMeasurement.Text = "<<";
            this.btnRemoveMeasurement.UseVisualStyleBackColor = true;
            this.btnRemoveMeasurement.Click += new System.EventHandler(this.btnRemoveMeasurement_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lstSelectedMeasurements);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lstAvailMeasurements);
            this.groupBox1.Controls.Add(this.btnAddMeasurement);
            this.groupBox1.Controls.Add(this.btnRemoveMeasurement);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(733, 185);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Measurements:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(399, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Selected";
            // 
            // lstSelectedMeasurements
            // 
            this.lstSelectedMeasurements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSelectedMeasurements.FormattingEnabled = true;
            this.lstSelectedMeasurements.Location = new System.Drawing.Point(396, 32);
            this.lstSelectedMeasurements.Name = "lstSelectedMeasurements";
            this.lstSelectedMeasurements.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSelectedMeasurements.Size = new System.Drawing.Size(328, 147);
            this.lstSelectedMeasurements.TabIndex = 8;
            this.lstSelectedMeasurements.SelectedValueChanged += new System.EventHandler(this.lstSelectedMeasurements_SelectedValueChanged);
            this.lstSelectedMeasurements.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstSelectedMeasurements_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Available";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 185);
            this.panel1.TabIndex = 8;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 209);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(733, 3);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtLogFile);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtConfigFile);
            this.panel2.Controls.Add(this.txtECUFile);
            this.panel2.Controls.Add(this.btnStartLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 209);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(733, 97);
            this.panel2.TabIndex = 10;
            // 
            // txtLogFile
            // 
            this.txtLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogFile.Location = new System.Drawing.Point(69, 62);
            this.txtLogFile.Name = "txtLogFile";
            this.txtLogFile.Size = new System.Drawing.Size(571, 20);
            this.txtLogFile.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Log File:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Config File:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "ECU File:";
            // 
            // txtConfigFile
            // 
            this.txtConfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfigFile.Location = new System.Drawing.Point(69, 34);
            this.txtConfigFile.Name = "txtConfigFile";
            this.txtConfigFile.ReadOnly = true;
            this.txtConfigFile.Size = new System.Drawing.Size(652, 20);
            this.txtConfigFile.TabIndex = 2;
            // 
            // txtECUFile
            // 
            this.txtECUFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtECUFile.Location = new System.Drawing.Point(69, 8);
            this.txtECUFile.Name = "txtECUFile";
            this.txtECUFile.ReadOnly = true;
            this.txtECUFile.Size = new System.Drawing.Size(652, 20);
            this.txtECUFile.TabIndex = 1;
            this.txtECUFile.Text = "   ";
            // 
            // btnStartLog
            // 
            this.btnStartLog.Location = new System.Drawing.Point(646, 60);
            this.btnStartLog.Name = "btnStartLog";
            this.btnStartLog.Size = new System.Drawing.Size(75, 23);
            this.btnStartLog.TabIndex = 0;
            this.btnStartLog.Text = "Start Log";
            this.btnStartLog.UseVisualStyleBackColor = true;
            this.btnStartLog.Click += new System.EventHandler(this.btnStartLog_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 306);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.Text = "Visual ME7Logger";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadECUFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ListBox lstAvailMeasurements;
        private System.Windows.Forms.Button btnAddMeasurement;
        private System.Windows.Forms.Button btnRemoveMeasurement;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstSelectedMeasurements;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStartLog;
        private System.Windows.Forms.ToolStripMenuItem loadConfigFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearConfigFileToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtConfigFile;
        private System.Windows.Forms.TextBox txtECUFile;
        private System.Windows.Forms.TextBox txtLogFile;
        private System.Windows.Forms.Label label5;


    }
}