namespace VisualME7Logger
{
    partial class GaugeWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMax = new System.Windows.Forms.NumericUpDown();
            this.nudMin = new System.Windows.Forms.NumericUpDown();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).BeginInit();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 226);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(288, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chart1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(288, 226);
            this.panel2.TabIndex = 2;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.DimGray;
            chartArea2.BackColor = System.Drawing.Color.DimGray;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(288, 226);
            this.chart1.TabIndex = 0;
            this.chart1.Tag = "";
            this.chart1.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nudMax);
            this.panel1.Controls.Add(this.nudMin);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 229);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 27);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Max";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Min";
            // 
            // nudMax
            // 
            this.nudMax.Location = new System.Drawing.Point(124, 4);
            this.nudMax.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudMax.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.nudMax.Name = "nudMax";
            this.nudMax.Size = new System.Drawing.Size(50, 20);
            this.nudMax.TabIndex = 1;
            this.nudMax.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudMax.ValueChanged += new System.EventHandler(this.nudMax_ValueChanged);
            // 
            // nudMin
            // 
            this.nudMin.Location = new System.Drawing.Point(34, 3);
            this.nudMin.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudMin.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.nudMin.Name = "nudMin";
            this.nudMin.Size = new System.Drawing.Size(50, 20);
            this.nudMin.TabIndex = 0;
            this.nudMin.ValueChanged += new System.EventHandler(this.nudMin_ValueChanged);
            // 
            // GaugeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 256);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GaugeWindow";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GaugeWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GaugeWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GaugeWindow_FormClosed);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudMax;
        private System.Windows.Forms.NumericUpDown nudMin;
    }
}