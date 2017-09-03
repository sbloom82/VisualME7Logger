namespace LDRPIDTool
{
    partial class Main
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
            this.lblLogDir = new System.Windows.Forms.Label();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.btnChooseDir = new System.Windows.Forms.Button();
            this.grdKFLDRL = new LDRPIDTool.SlightlyBetterDataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.grdKFLDIMX = new LDRPIDTool.SlightlyBetterDataGridView();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAmbient = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilterRPM = new System.Windows.Forms.TextBox();
            this.txtFilterSeconds = new System.Windows.Forms.TextBox();
            this.txtFilterMbar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdKFLDRL)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKFLDIMX)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLogDir
            // 
            this.lblLogDir.AutoSize = true;
            this.lblLogDir.Location = new System.Drawing.Point(4, 8);
            this.lblLogDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogDir.Name = "lblLogDir";
            this.lblLogDir.Size = new System.Drawing.Size(73, 13);
            this.lblLogDir.TabIndex = 1;
            this.lblLogDir.Text = "Log Directory:";
            // 
            // txtDir
            // 
            this.txtDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDir.Location = new System.Drawing.Point(81, 8);
            this.txtDir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(667, 20);
            this.txtDir.TabIndex = 0;
            // 
            // btnChooseDir
            // 
            this.btnChooseDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseDir.Location = new System.Drawing.Point(764, 7);
            this.btnChooseDir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnChooseDir.Name = "btnChooseDir";
            this.btnChooseDir.Size = new System.Drawing.Size(68, 19);
            this.btnChooseDir.TabIndex = 1;
            this.btnChooseDir.Text = "Choose";
            this.btnChooseDir.UseVisualStyleBackColor = true;
            this.btnChooseDir.Click += new System.EventHandler(this.btnChooseDir_Click);
            // 
            // grdKFLDRL
            // 
            this.grdKFLDRL.AccessibleName = "";
            this.grdKFLDRL.AllowUserToResizeColumns = false;
            this.grdKFLDRL.AllowUserToResizeRows = false;
            this.grdKFLDRL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdKFLDRL.ColumnHeadersVisible = false;
            this.grdKFLDRL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKFLDRL.Location = new System.Drawing.Point(0, 0);
            this.grdKFLDRL.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grdKFLDRL.Name = "grdKFLDRL";
            this.grdKFLDRL.RowHeadersVisible = false;
            this.grdKFLDRL.RowTemplate.Height = 24;
            this.grdKFLDRL.Size = new System.Drawing.Size(836, 437);
            this.grdKFLDRL.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdKFLDRL);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 72);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(836, 532);
            this.panel1.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.grdKFLDIMX);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 437);
            this.panel4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(836, 76);
            this.panel4.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "KFLDIMX";
            // 
            // grdKFLDIMX
            // 
            this.grdKFLDIMX.AccessibleName = "";
            this.grdKFLDIMX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdKFLDIMX.ColumnHeadersVisible = false;
            this.grdKFLDIMX.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdKFLDIMX.Location = new System.Drawing.Point(0, 18);
            this.grdKFLDIMX.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grdKFLDIMX.Name = "grdKFLDIMX";
            this.grdKFLDIMX.RowHeadersVisible = false;
            this.grdKFLDIMX.RowTemplate.Height = 24;
            this.grdKFLDIMX.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.grdKFLDIMX.Size = new System.Drawing.Size(836, 58);
            this.grdKFLDIMX.TabIndex = 8;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnGenerate.Location = new System.Drawing.Point(0, 513);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(836, 19);
            this.btnGenerate.TabIndex = 8;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(836, 72);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnHelp);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.txtAmbient);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtFilterRPM);
            this.panel3.Controls.Add(this.txtFilterSeconds);
            this.panel3.Controls.Add(this.txtFilterMbar);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btnLoad);
            this.panel3.Controls.Add(this.btnChooseDir);
            this.panel3.Controls.Add(this.txtDir);
            this.panel3.Controls.Add(this.lblLogDir);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(836, 72);
            this.panel3.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(622, 33);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "ambient:";
            // 
            // txtAmbient
            // 
            this.txtAmbient.Location = new System.Drawing.Point(672, 31);
            this.txtAmbient.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAmbient.Name = "txtAmbient";
            this.txtAmbient.Size = new System.Drawing.Size(76, 20);
            this.txtAmbient.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(412, 33);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "rpm range min:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 33);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "/ seconds:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Spool Increase mbar:";
            // 
            // txtFilterRPM
            // 
            this.txtFilterRPM.Location = new System.Drawing.Point(492, 31);
            this.txtFilterRPM.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtFilterRPM.Name = "txtFilterRPM";
            this.txtFilterRPM.Size = new System.Drawing.Size(76, 20);
            this.txtFilterRPM.TabIndex = 4;
            // 
            // txtFilterSeconds
            // 
            this.txtFilterSeconds.Location = new System.Drawing.Point(324, 31);
            this.txtFilterSeconds.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtFilterSeconds.Name = "txtFilterSeconds";
            this.txtFilterSeconds.Size = new System.Drawing.Size(76, 20);
            this.txtFilterSeconds.TabIndex = 3;
            // 
            // txtFilterMbar
            // 
            this.txtFilterMbar.Location = new System.Drawing.Point(190, 31);
            this.txtFilterMbar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtFilterMbar.Name = "txtFilterMbar";
            this.txtFilterMbar.Size = new System.Drawing.Size(76, 20);
            this.txtFilterMbar.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Range Filter:";
            // 
            // btnLoad
            // 
            this.btnLoad.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLoad.Location = new System.Drawing.Point(0, 52);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(836, 20);
            this.btnLoad.TabIndex = 6;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(806, 30);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(2);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(26, 19);
            this.btnHelp.TabIndex = 17;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 604);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Main";
            this.Text = "LDRPID tool";
            ((System.ComponentModel.ISupportInitialize)(this.grdKFLDRL)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKFLDIMX)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLogDir;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Button btnChooseDir;
        private SlightlyBetterDataGridView grdKFLDRL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private SlightlyBetterDataGridView grdKFLDIMX;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TextBox txtFilterRPM;
        private System.Windows.Forms.TextBox txtFilterSeconds;
        private System.Windows.Forms.TextBox txtFilterMbar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAmbient;
        private System.Windows.Forms.Button btnHelp;
    }
}

