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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpMeasurements = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tbGraphConfig = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstGraphVariables = new System.Windows.Forms.ListBox();
            this.btnEditGraphVariable = new System.Windows.Forms.Button();
            this.gbGraphVariables = new System.Windows.Forms.GroupBox();
            this.chkGraphVariableActive = new System.Windows.Forms.CheckBox();
            this.nudGraphVariableThickness = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGraphVariableVariable = new System.Windows.Forms.TextBox();
            this.btnCancelGraphVariable = new System.Windows.Forms.Button();
            this.btnSaveGraphVariable = new System.Windows.Forms.Button();
            this.txtGraphVariableName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGraphVariableColor = new System.Windows.Forms.TextBox();
            this.nudGraphVariableMax = new System.Windows.Forms.NumericUpDown();
            this.nudGraphVariableMin = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteGraphVariable = new System.Windows.Forms.Button();
            this.btnAddGraphVariable = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConfigFile = new System.Windows.Forms.TextBox();
            this.txtECUFile = new System.Windows.Forms.TextBox();
            this.btnStartLog = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadECUFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadConfigFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearConfigFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.filterTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripFilterTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mE7CheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createECUFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.cmbGraphVariableStyle = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpMeasurements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tbGraphConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbGraphVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMin)).BeginInit();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 341);
            this.panel1.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpMeasurements);
            this.tabControl1.Controls.Add(this.tbGraphConfig);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(733, 341);
            this.tabControl1.TabIndex = 8;
            // 
            // tpMeasurements
            // 
            this.tpMeasurements.Controls.Add(this.dataGridView1);
            this.tpMeasurements.Location = new System.Drawing.Point(4, 22);
            this.tpMeasurements.Name = "tpMeasurements";
            this.tpMeasurements.Padding = new System.Windows.Forms.Padding(3);
            this.tpMeasurements.Size = new System.Drawing.Size(725, 292);
            this.tpMeasurements.TabIndex = 0;
            this.tpMeasurements.Text = "Measurements";
            this.tpMeasurements.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(719, 286);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // tbGraphConfig
            // 
            this.tbGraphConfig.BackColor = System.Drawing.Color.Transparent;
            this.tbGraphConfig.Controls.Add(this.groupBox1);
            this.tbGraphConfig.Location = new System.Drawing.Point(4, 22);
            this.tbGraphConfig.Name = "tbGraphConfig";
            this.tbGraphConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tbGraphConfig.Size = new System.Drawing.Size(725, 315);
            this.tbGraphConfig.TabIndex = 1;
            this.tbGraphConfig.Text = "Graph Data";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstGraphVariables);
            this.groupBox1.Controls.Add(this.btnEditGraphVariable);
            this.groupBox1.Controls.Add(this.gbGraphVariables);
            this.groupBox1.Controls.Add(this.btnDeleteGraphVariable);
            this.groupBox1.Controls.Add(this.btnAddGraphVariable);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 303);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Variables";
            // 
            // lstGraphVariables
            // 
            this.lstGraphVariables.FormattingEnabled = true;
            this.lstGraphVariables.Location = new System.Drawing.Point(6, 19);
            this.lstGraphVariables.Name = "lstGraphVariables";
            this.lstGraphVariables.Size = new System.Drawing.Size(300, 121);
            this.lstGraphVariables.TabIndex = 0;
            this.lstGraphVariables.SelectedIndexChanged += new System.EventHandler(this.lstGraphVariables_SelectedIndexChanged);
            // 
            // btnEditGraphVariable
            // 
            this.btnEditGraphVariable.Location = new System.Drawing.Point(312, 42);
            this.btnEditGraphVariable.Name = "btnEditGraphVariable";
            this.btnEditGraphVariable.Size = new System.Drawing.Size(75, 23);
            this.btnEditGraphVariable.TabIndex = 4;
            this.btnEditGraphVariable.Text = "Edit";
            this.btnEditGraphVariable.UseVisualStyleBackColor = true;
            this.btnEditGraphVariable.Click += new System.EventHandler(this.btnEditGraphVariable_Click);
            // 
            // gbGraphVariables
            // 
            this.gbGraphVariables.Controls.Add(this.label9);
            this.gbGraphVariables.Controls.Add(this.cmbGraphVariableStyle);
            this.gbGraphVariables.Controls.Add(this.chkGraphVariableActive);
            this.gbGraphVariables.Controls.Add(this.nudGraphVariableThickness);
            this.gbGraphVariables.Controls.Add(this.label8);
            this.gbGraphVariables.Controls.Add(this.txtGraphVariableVariable);
            this.gbGraphVariables.Controls.Add(this.btnCancelGraphVariable);
            this.gbGraphVariables.Controls.Add(this.btnSaveGraphVariable);
            this.gbGraphVariables.Controls.Add(this.txtGraphVariableName);
            this.gbGraphVariables.Controls.Add(this.label7);
            this.gbGraphVariables.Controls.Add(this.label6);
            this.gbGraphVariables.Controls.Add(this.label5);
            this.gbGraphVariables.Controls.Add(this.label2);
            this.gbGraphVariables.Controls.Add(this.txtGraphVariableColor);
            this.gbGraphVariables.Controls.Add(this.nudGraphVariableMax);
            this.gbGraphVariables.Controls.Add(this.nudGraphVariableMin);
            this.gbGraphVariables.Controls.Add(this.label1);
            this.gbGraphVariables.Location = new System.Drawing.Point(8, 142);
            this.gbGraphVariables.Name = "gbGraphVariables";
            this.gbGraphVariables.Size = new System.Drawing.Size(298, 155);
            this.gbGraphVariables.TabIndex = 1;
            this.gbGraphVariables.TabStop = false;
            // 
            // chkGraphVariableActive
            // 
            this.chkGraphVariableActive.AutoSize = true;
            this.chkGraphVariableActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGraphVariableActive.Location = new System.Drawing.Point(6, 10);
            this.chkGraphVariableActive.Name = "chkGraphVariableActive";
            this.chkGraphVariableActive.Size = new System.Drawing.Size(59, 17);
            this.chkGraphVariableActive.TabIndex = 5;
            this.chkGraphVariableActive.Text = "Active:";
            this.chkGraphVariableActive.UseVisualStyleBackColor = true;
            // 
            // nudGraphVariableThickness
            // 
            this.nudGraphVariableThickness.Location = new System.Drawing.Point(254, 99);
            this.nudGraphVariableThickness.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudGraphVariableThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGraphVariableThickness.Name = "nudGraphVariableThickness";
            this.nudGraphVariableThickness.Size = new System.Drawing.Size(38, 20);
            this.nudGraphVariableThickness.TabIndex = 5;
            this.nudGraphVariableThickness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(193, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Thickness:";
            // 
            // txtGraphVariableVariable
            // 
            this.txtGraphVariableVariable.Location = new System.Drawing.Point(51, 29);
            this.txtGraphVariableVariable.Name = "txtGraphVariableVariable";
            this.txtGraphVariableVariable.Size = new System.Drawing.Size(241, 20);
            this.txtGraphVariableVariable.TabIndex = 0;
            // 
            // btnCancelGraphVariable
            // 
            this.btnCancelGraphVariable.Location = new System.Drawing.Point(136, 125);
            this.btnCancelGraphVariable.Name = "btnCancelGraphVariable";
            this.btnCancelGraphVariable.Size = new System.Drawing.Size(75, 23);
            this.btnCancelGraphVariable.TabIndex = 10;
            this.btnCancelGraphVariable.Text = "Cancel";
            this.btnCancelGraphVariable.UseVisualStyleBackColor = true;
            this.btnCancelGraphVariable.Click += new System.EventHandler(this.btnCancelGraphVariable_Click);
            // 
            // btnSaveGraphVariable
            // 
            this.btnSaveGraphVariable.Location = new System.Drawing.Point(217, 125);
            this.btnSaveGraphVariable.Name = "btnSaveGraphVariable";
            this.btnSaveGraphVariable.Size = new System.Drawing.Size(75, 23);
            this.btnSaveGraphVariable.TabIndex = 11;
            this.btnSaveGraphVariable.Text = "Save";
            this.btnSaveGraphVariable.UseVisualStyleBackColor = true;
            this.btnSaveGraphVariable.Click += new System.EventHandler(this.btnSaveGraphVariable_Click);
            // 
            // txtGraphVariableName
            // 
            this.txtGraphVariableName.Location = new System.Drawing.Point(51, 53);
            this.txtGraphVariableName.Name = "txtGraphVariableName";
            this.txtGraphVariableName.Size = new System.Drawing.Size(241, 20);
            this.txtGraphVariableName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Color:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(171, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Max:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Min:";
            // 
            // txtGraphVariableColor
            // 
            this.txtGraphVariableColor.Location = new System.Drawing.Point(51, 99);
            this.txtGraphVariableColor.Name = "txtGraphVariableColor";
            this.txtGraphVariableColor.ReadOnly = true;
            this.txtGraphVariableColor.Size = new System.Drawing.Size(20, 20);
            this.txtGraphVariableColor.TabIndex = 4;
            this.txtGraphVariableColor.Click += new System.EventHandler(this.txtGraphVariableColor_Click);
            // 
            // nudGraphVariableMax
            // 
            this.nudGraphVariableMax.DecimalPlaces = 2;
            this.nudGraphVariableMax.Location = new System.Drawing.Point(203, 76);
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
            this.nudGraphVariableMax.TabIndex = 3;
            // 
            // nudGraphVariableMin
            // 
            this.nudGraphVariableMin.DecimalPlaces = 2;
            this.nudGraphVariableMin.Location = new System.Drawing.Point(51, 76);
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
            this.nudGraphVariableMin.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Variable:";
            // 
            // btnDeleteGraphVariable
            // 
            this.btnDeleteGraphVariable.Location = new System.Drawing.Point(312, 65);
            this.btnDeleteGraphVariable.Name = "btnDeleteGraphVariable";
            this.btnDeleteGraphVariable.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteGraphVariable.TabIndex = 3;
            this.btnDeleteGraphVariable.Text = "Delete";
            this.btnDeleteGraphVariable.UseVisualStyleBackColor = true;
            this.btnDeleteGraphVariable.Click += new System.EventHandler(this.btnDeleteGraphVariable_Click);
            // 
            // btnAddGraphVariable
            // 
            this.btnAddGraphVariable.Location = new System.Drawing.Point(312, 19);
            this.btnAddGraphVariable.Name = "btnAddGraphVariable";
            this.btnAddGraphVariable.Size = new System.Drawing.Size(75, 23);
            this.btnAddGraphVariable.TabIndex = 2;
            this.btnAddGraphVariable.Text = "Add";
            this.btnAddGraphVariable.UseVisualStyleBackColor = true;
            this.btnAddGraphVariable.Click += new System.EventHandler(this.btnAddGraphVariable_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtConfigFile);
            this.panel2.Controls.Add(this.txtECUFile);
            this.panel2.Controls.Add(this.btnStartLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 368);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(733, 89);
            this.panel2.TabIndex = 10;
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
            this.btnStartLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartLog.Location = new System.Drawing.Point(646, 60);
            this.btnStartLog.Name = "btnStartLog";
            this.btnStartLog.Size = new System.Drawing.Size(75, 23);
            this.btnStartLog.TabIndex = 0;
            this.btnStartLog.Text = "Start Log";
            this.btnStartLog.UseVisualStyleBackColor = true;
            this.btnStartLog.Click += new System.EventHandler(this.btnStartLog_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 365);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(733, 3);
            this.splitter1.TabIndex = 11;
            this.splitter1.TabStop = false;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadECUFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.loadConfigFileToolStripMenuItem,
            this.saveConfigFileToolStripMenuItem,
            this.clearConfigFileToolStripMenuItem,
            this.toolStripMenuItem3,
            this.saveSettingsToolStripMenuItem,
            this.toolStripMenuItem5,
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
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(158, 6);
            // 
            // loadConfigFileToolStripMenuItem
            // 
            this.loadConfigFileToolStripMenuItem.Enabled = false;
            this.loadConfigFileToolStripMenuItem.Name = "loadConfigFileToolStripMenuItem";
            this.loadConfigFileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.loadConfigFileToolStripMenuItem.Text = "Load Config File";
            this.loadConfigFileToolStripMenuItem.Click += new System.EventHandler(this.loadConfigFileToolStripMenuItem_Click);
            // 
            // saveConfigFileToolStripMenuItem
            // 
            this.saveConfigFileToolStripMenuItem.Enabled = false;
            this.saveConfigFileToolStripMenuItem.Name = "saveConfigFileToolStripMenuItem";
            this.saveConfigFileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saveConfigFileToolStripMenuItem.Text = "Save Config File";
            this.saveConfigFileToolStripMenuItem.Click += new System.EventHandler(this.saveConfigFileToolStripMenuItem_Click);
            // 
            // clearConfigFileToolStripMenuItem
            // 
            this.clearConfigFileToolStripMenuItem.Name = "clearConfigFileToolStripMenuItem";
            this.clearConfigFileToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.clearConfigFileToolStripMenuItem.Text = "Clear Config File";
            this.clearConfigFileToolStripMenuItem.Click += new System.EventHandler(this.clearConfigFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(158, 6);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saveSettingsToolStripMenuItem.Text = "Save Settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(158, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingToolStripMenuItem.Text = "Settings";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.selectedToolStripMenuItem,
            this.unselectedToolStripMenuItem,
            this.toolStripSeparator1,
            this.filterTextToolStripMenuItem,
            this.toolStripFilterTextBox});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Checked = true;
            this.allToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.allToolStripMenuItem.Text = "All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // selectedToolStripMenuItem
            // 
            this.selectedToolStripMenuItem.Name = "selectedToolStripMenuItem";
            this.selectedToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.selectedToolStripMenuItem.Text = "Selected";
            this.selectedToolStripMenuItem.Click += new System.EventHandler(this.selectedToolStripMenuItem_Click);
            // 
            // unselectedToolStripMenuItem
            // 
            this.unselectedToolStripMenuItem.Name = "unselectedToolStripMenuItem";
            this.unselectedToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.unselectedToolStripMenuItem.Text = "Unselected";
            this.unselectedToolStripMenuItem.Click += new System.EventHandler(this.unselectedToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // filterTextToolStripMenuItem
            // 
            this.filterTextToolStripMenuItem.Enabled = false;
            this.filterTextToolStripMenuItem.Name = "filterTextToolStripMenuItem";
            this.filterTextToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.filterTextToolStripMenuItem.Text = "Filter Text:";
            // 
            // toolStripFilterTextBox
            // 
            this.toolStripFilterTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripFilterTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripFilterTextBox.Name = "toolStripFilterTextBox";
            this.toolStripFilterTextBox.Size = new System.Drawing.Size(100, 23);
            this.toolStripFilterTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripFilterTextBox_KeyDown);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mE7CheckToolStripMenuItem,
            this.createECUFileToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // mE7CheckToolStripMenuItem
            // 
            this.mE7CheckToolStripMenuItem.Name = "mE7CheckToolStripMenuItem";
            this.mE7CheckToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.mE7CheckToolStripMenuItem.Text = "Checksum Validation";
            this.mE7CheckToolStripMenuItem.Click += new System.EventHandler(this.mE7CheckToolStripMenuItem_Click);
            // 
            // createECUFileToolStripMenuItem
            // 
            this.createECUFileToolStripMenuItem.Name = "createECUFileToolStripMenuItem";
            this.createECUFileToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.createECUFileToolStripMenuItem.Text = "Create ECU File";
            this.createECUFileToolStripMenuItem.Click += new System.EventHandler(this.createECUFileToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(733, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cmbGraphVariableStyle
            // 
            this.cmbGraphVariableStyle.FormattingEnabled = true;
            this.cmbGraphVariableStyle.Location = new System.Drawing.Point(111, 99);
            this.cmbGraphVariableStyle.Name = "cmbGraphVariableStyle";
            this.cmbGraphVariableStyle.Size = new System.Drawing.Size(80, 21);
            this.cmbGraphVariableStyle.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(76, 102);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Style:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 457);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.Text = "Visual ME7Logger";
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpMeasurements.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tbGraphConfig.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbGraphVariables.ResumeLayout(false);
            this.gbGraphVariables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMin)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnStartLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtConfigFile;
        private System.Windows.Forms.TextBox txtECUFile;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadECUFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadConfigFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearConfigFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mE7CheckToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem createECUFileToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpMeasurements;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tbGraphConfig;
        private System.Windows.Forms.GroupBox gbGraphVariables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstGraphVariables;
        private System.Windows.Forms.Button btnEditGraphVariable;
        private System.Windows.Forms.Button btnDeleteGraphVariable;
        private System.Windows.Forms.Button btnAddGraphVariable;
        private System.Windows.Forms.Button btnCancelGraphVariable;
        private System.Windows.Forms.Button btnSaveGraphVariable;
        private System.Windows.Forms.TextBox txtGraphVariableName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGraphVariableColor;
        private System.Windows.Forms.NumericUpDown nudGraphVariableMax;
        private System.Windows.Forms.NumericUpDown nudGraphVariableMin;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox txtGraphVariableVariable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem filterTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripFilterTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudGraphVariableThickness;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkGraphVariableActive;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbGraphVariableStyle;


    }
}