namespace VisualME7Logger
{
    partial class GraphVariableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphVariableForm));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbGraphVariableStyle = new System.Windows.Forms.ComboBox();
            this.chkGraphVariableActive = new System.Windows.Forms.CheckBox();
            this.nudGraphVariableThickness = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGraphVariableName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGraphVariableColor = new System.Windows.Forms.TextBox();
            this.nudGraphVariableMax = new System.Windows.Forms.NumericUpDown();
            this.nudGraphVariableMin = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGraphVariable = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMin)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(225, 128);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(144, 128);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(84, 101);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Style:";
            // 
            // cmbGraphVariableStyle
            // 
            this.cmbGraphVariableStyle.FormattingEnabled = true;
            this.cmbGraphVariableStyle.Location = new System.Drawing.Point(119, 98);
            this.cmbGraphVariableStyle.Name = "cmbGraphVariableStyle";
            this.cmbGraphVariableStyle.Size = new System.Drawing.Size(80, 21);
            this.cmbGraphVariableStyle.TabIndex = 24;
            // 
            // chkGraphVariableActive
            // 
            this.chkGraphVariableActive.AutoSize = true;
            this.chkGraphVariableActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGraphVariableActive.Location = new System.Drawing.Point(14, 9);
            this.chkGraphVariableActive.Name = "chkGraphVariableActive";
            this.chkGraphVariableActive.Size = new System.Drawing.Size(59, 17);
            this.chkGraphVariableActive.TabIndex = 0;
            this.chkGraphVariableActive.Text = "Active:";
            this.chkGraphVariableActive.UseVisualStyleBackColor = true;
            // 
            // nudGraphVariableThickness
            // 
            this.nudGraphVariableThickness.Location = new System.Drawing.Point(262, 98);
            this.nudGraphVariableThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGraphVariableThickness.Name = "nudGraphVariableThickness";
            this.nudGraphVariableThickness.Size = new System.Drawing.Size(38, 20);
            this.nudGraphVariableThickness.TabIndex = 7;
            this.nudGraphVariableThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(201, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Thickness:";
            // 
            // txtGraphVariableName
            // 
            this.txtGraphVariableName.Location = new System.Drawing.Point(59, 52);
            this.txtGraphVariableName.Name = "txtGraphVariableName";
            this.txtGraphVariableName.Size = new System.Drawing.Size(241, 20);
            this.txtGraphVariableName.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Color:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(179, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Max:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Min:";
            // 
            // txtGraphVariableColor
            // 
            this.txtGraphVariableColor.Location = new System.Drawing.Point(59, 98);
            this.txtGraphVariableColor.Name = "txtGraphVariableColor";
            this.txtGraphVariableColor.ReadOnly = true;
            this.txtGraphVariableColor.Size = new System.Drawing.Size(20, 20);
            this.txtGraphVariableColor.TabIndex = 5;
            this.txtGraphVariableColor.Click += new System.EventHandler(this.txtGraphVariableColor_Click);
            // 
            // nudGraphVariableMax
            // 
            this.nudGraphVariableMax.DecimalPlaces = 2;
            this.nudGraphVariableMax.Location = new System.Drawing.Point(211, 75);
            this.nudGraphVariableMax.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.nudGraphVariableMax.Minimum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            -2147483648});
            this.nudGraphVariableMax.Name = "nudGraphVariableMax";
            this.nudGraphVariableMax.Size = new System.Drawing.Size(89, 20);
            this.nudGraphVariableMax.TabIndex = 4;
            // 
            // nudGraphVariableMin
            // 
            this.nudGraphVariableMin.DecimalPlaces = 2;
            this.nudGraphVariableMin.Location = new System.Drawing.Point(59, 75);
            this.nudGraphVariableMin.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.nudGraphVariableMin.Minimum = new decimal(new int[] {
            1215752192,
            23,
            0,
            -2147483648});
            this.nudGraphVariableMin.Name = "nudGraphVariableMin";
            this.nudGraphVariableMin.Size = new System.Drawing.Size(89, 20);
            this.nudGraphVariableMin.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Variable:";
            // 
            // txtGraphVariable
            // 
            this.txtGraphVariable.Location = new System.Drawing.Point(59, 28);
            this.txtGraphVariable.Name = "txtGraphVariable";
            this.txtGraphVariable.ReadOnly = true;
            this.txtGraphVariable.Size = new System.Drawing.Size(241, 20);
            this.txtGraphVariable.TabIndex = 1;
            // 
            // GraphVariableForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(306, 155);
            this.ControlBox = false;
            this.Controls.Add(this.txtGraphVariable);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbGraphVariableStyle);
            this.Controls.Add(this.chkGraphVariableActive);
            this.Controls.Add(this.nudGraphVariableThickness);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtGraphVariableName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtGraphVariableColor);
            this.Controls.Add(this.nudGraphVariableMax);
            this.Controls.Add(this.nudGraphVariableMin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GraphVariableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ME7Logger Graph Options";
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbGraphVariableStyle;
        private System.Windows.Forms.CheckBox chkGraphVariableActive;
        private System.Windows.Forms.NumericUpDown nudGraphVariableThickness;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGraphVariableName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGraphVariableColor;
        private System.Windows.Forms.NumericUpDown nudGraphVariableMax;
        private System.Windows.Forms.NumericUpDown nudGraphVariableMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGraphVariable;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}