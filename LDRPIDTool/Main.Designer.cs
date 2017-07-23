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
            this.grdKFLDRL = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grdKFLDIMX = new System.Windows.Forms.DataGridView();
            this.btnLoad = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdKFLDRL)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKFLDIMX)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLogDir
            // 
            this.lblLogDir.AutoSize = true;
            this.lblLogDir.Location = new System.Drawing.Point(5, 10);
            this.lblLogDir.Name = "lblLogDir";
            this.lblLogDir.Size = new System.Drawing.Size(97, 17);
            this.lblLogDir.TabIndex = 1;
            this.lblLogDir.Text = "Log Directory:";
            // 
            // txtDir
            // 
            this.txtDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDir.Location = new System.Drawing.Point(108, 10);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(887, 22);
            this.txtDir.TabIndex = 4;
            // 
            // btnChooseDir
            // 
            this.btnChooseDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChooseDir.Location = new System.Drawing.Point(1018, 9);
            this.btnChooseDir.Name = "btnChooseDir";
            this.btnChooseDir.Size = new System.Drawing.Size(90, 23);
            this.btnChooseDir.TabIndex = 5;
            this.btnChooseDir.Text = "Choose";
            this.btnChooseDir.UseVisualStyleBackColor = true;
            this.btnChooseDir.Click += new System.EventHandler(this.btnChooseDir_Click);
            // 
            // grdKFLDRL
            // 
            this.grdKFLDRL.AccessibleName = "";
            this.grdKFLDRL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdKFLDRL.ColumnHeadersVisible = false;
            this.grdKFLDRL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdKFLDRL.Location = new System.Drawing.Point(0, 0);
            this.grdKFLDRL.Name = "grdKFLDRL";
            this.grdKFLDRL.RowHeadersVisible = false;
            this.grdKFLDRL.RowTemplate.Height = 24;
            this.grdKFLDRL.Size = new System.Drawing.Size(1114, 507);
            this.grdKFLDRL.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdKFLDRL);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1114, 624);
            this.panel1.TabIndex = 8;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnGenerate.Location = new System.Drawing.Point(0, 601);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(1114, 23);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1114, 66);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnLoad);
            this.panel3.Controls.Add(this.btnChooseDir);
            this.panel3.Controls.Add(this.txtDir);
            this.panel3.Controls.Add(this.lblLogDir);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1114, 66);
            this.panel3.TabIndex = 8;
            // 
            // grdKFLDIMX
            // 
            this.grdKFLDIMX.AccessibleName = "";
            this.grdKFLDIMX.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdKFLDIMX.ColumnHeadersVisible = false;
            this.grdKFLDIMX.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdKFLDIMX.Location = new System.Drawing.Point(0, 23);
            this.grdKFLDIMX.Name = "grdKFLDIMX";
            this.grdKFLDIMX.RowHeadersVisible = false;
            this.grdKFLDIMX.RowTemplate.Height = 24;
            this.grdKFLDIMX.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.grdKFLDIMX.Size = new System.Drawing.Size(1114, 71);
            this.grdKFLDIMX.TabIndex = 8;
            // 
            // btnLoad
            // 
            this.btnLoad.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLoad.Location = new System.Drawing.Point(0, 42);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(1114, 24);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.grdKFLDIMX);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 507);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1114, 94);
            this.panel4.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "KFLDIMX";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 690);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "Main";
            this.Text = "LDRPID tool";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdKFLDRL)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdKFLDIMX)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLogDir;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Button btnChooseDir;
        private System.Windows.Forms.DataGridView grdKFLDRL;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView grdKFLDIMX;
        private System.Windows.Forms.Button btnLoad;
    }
}

