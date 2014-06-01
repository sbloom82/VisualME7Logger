﻿namespace VisualME7Logger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpMeasurements = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.lblMeasurementCount = new System.Windows.Forms.Label();
            this.radFilterUnselected = new System.Windows.Forms.RadioButton();
            this.radFilterSelected = new System.Windows.Forms.RadioButton();
            this.radFilterAll = new System.Windows.Forms.RadioButton();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tpExpressions = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnExpressionClone = new System.Windows.Forms.Button();
            this.lstExpressions = new System.Windows.Forms.ListBox();
            this.btnExpressionEdit = new System.Windows.Forms.Button();
            this.gbExpressions = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtExpressionExpression = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtExpressionUnit = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnExpressionCancel = new System.Windows.Forms.Button();
            this.btnExpressionSave = new System.Windows.Forms.Button();
            this.txtExpressionName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnExpressionDelete = new System.Windows.Forms.Button();
            this.btnExpressionAdd = new System.Windows.Forms.Button();
            this.tbGraphConfig = new System.Windows.Forms.TabPage();
            this.nudResfreshRate = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudGraphResV = new System.Windows.Forms.NumericUpDown();
            this.nudGraphResH = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstGraphVariables = new System.Windows.Forms.ListBox();
            this.btnEditGraphVariable = new System.Windows.Forms.Button();
            this.gbGraphVariables = new System.Windows.Forms.GroupBox();
            this.cmbGraphVariableVariable = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbGraphVariableStyle = new System.Windows.Forms.ComboBox();
            this.chkGraphVariableActive = new System.Windows.Forms.CheckBox();
            this.nudGraphVariableThickness = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
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
            this.tpProfiles = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnProfileSetCurrent = new System.Windows.Forms.Button();
            this.btnProfileClone = new System.Windows.Forms.Button();
            this.lstProfiles = new System.Windows.Forms.ListBox();
            this.btnProfileEdit = new System.Windows.Forms.Button();
            this.gbProfile = new System.Windows.Forms.GroupBox();
            this.btnProfileCancel = new System.Windows.Forms.Button();
            this.btnProfileSave = new System.Windows.Forms.Button();
            this.txtProfileName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnProfileDelete = new System.Windows.Forms.Button();
            this.btnProfileAdd = new System.Windows.Forms.Button();
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
            this.saveConfigFileAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mE7CheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createECUFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpMeasurements.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.tpExpressions.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbExpressions.SuspendLayout();
            this.tbGraphConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudResfreshRate)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphResV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphResH)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbGraphVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMin)).BeginInit();
            this.tpProfiles.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbProfile.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.tpExpressions);
            this.tabControl1.Controls.Add(this.tbGraphConfig);
            this.tabControl1.Controls.Add(this.tpProfiles);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(733, 341);
            this.tabControl1.TabIndex = 8;
            // 
            // tpMeasurements
            // 
            this.tpMeasurements.BackColor = System.Drawing.SystemColors.Control;
            this.tpMeasurements.Controls.Add(this.dataGridView1);
            this.tpMeasurements.Controls.Add(this.panel3);
            this.tpMeasurements.Location = new System.Drawing.Point(4, 22);
            this.tpMeasurements.Name = "tpMeasurements";
            this.tpMeasurements.Padding = new System.Windows.Forms.Padding(3);
            this.tpMeasurements.Size = new System.Drawing.Size(725, 315);
            this.tpMeasurements.TabIndex = 0;
            this.tpMeasurements.Text = "Measurements";
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
            this.dataGridView1.Location = new System.Drawing.Point(3, 29);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(719, 283);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.lblMeasurementCount);
            this.panel3.Controls.Add(this.radFilterUnselected);
            this.panel3.Controls.Add(this.radFilterSelected);
            this.panel3.Controls.Add(this.radFilterAll);
            this.panel3.Controls.Add(this.txtFilter);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(719, 26);
            this.panel3.TabIndex = 12;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(823, 295);
            this.label15.Name = "label15";
            this.label15.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label15.Size = new System.Drawing.Size(48, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "label14C";
            // 
            // lblMeasurementCount
            // 
            this.lblMeasurementCount.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblMeasurementCount.Location = new System.Drawing.Point(575, 0);
            this.lblMeasurementCount.Name = "lblMeasurementCount";
            this.lblMeasurementCount.Size = new System.Drawing.Size(142, 24);
            this.lblMeasurementCount.TabIndex = 5;
            this.lblMeasurementCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radFilterUnselected
            // 
            this.radFilterUnselected.AutoSize = true;
            this.radFilterUnselected.Location = new System.Drawing.Point(382, 4);
            this.radFilterUnselected.Name = "radFilterUnselected";
            this.radFilterUnselected.Size = new System.Drawing.Size(109, 17);
            this.radFilterUnselected.TabIndex = 4;
            this.radFilterUnselected.Text = "Show &Unselected";
            this.radFilterUnselected.UseVisualStyleBackColor = true;
            this.radFilterUnselected.CheckedChanged += new System.EventHandler(this.radFilterUnselected_CheckedChanged);
            // 
            // radFilterSelected
            // 
            this.radFilterSelected.AutoSize = true;
            this.radFilterSelected.Location = new System.Drawing.Point(279, 4);
            this.radFilterSelected.Name = "radFilterSelected";
            this.radFilterSelected.Size = new System.Drawing.Size(97, 17);
            this.radFilterSelected.TabIndex = 3;
            this.radFilterSelected.Text = "Show &Selected";
            this.radFilterSelected.UseVisualStyleBackColor = true;
            this.radFilterSelected.CheckedChanged += new System.EventHandler(this.radFilterSelected_CheckedChanged);
            // 
            // radFilterAll
            // 
            this.radFilterAll.AutoSize = true;
            this.radFilterAll.Checked = true;
            this.radFilterAll.Location = new System.Drawing.Point(207, 4);
            this.radFilterAll.Name = "radFilterAll";
            this.radFilterAll.Size = new System.Drawing.Size(66, 17);
            this.radFilterAll.TabIndex = 2;
            this.radFilterAll.TabStop = true;
            this.radFilterAll.Text = "Show &All";
            this.radFilterAll.UseVisualStyleBackColor = true;
            this.radFilterAll.CheckedChanged += new System.EventHandler(this.radFilterAll_CheckedChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(62, 2);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(139, 20);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(28, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Filter:";
            // 
            // tpExpressions
            // 
            this.tpExpressions.BackColor = System.Drawing.SystemColors.Control;
            this.tpExpressions.Controls.Add(this.groupBox4);
            this.tpExpressions.Location = new System.Drawing.Point(4, 22);
            this.tpExpressions.Name = "tpExpressions";
            this.tpExpressions.Size = new System.Drawing.Size(725, 315);
            this.tpExpressions.TabIndex = 3;
            this.tpExpressions.Text = "Expressions";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.btnExpressionClone);
            this.groupBox4.Controls.Add(this.lstExpressions);
            this.groupBox4.Controls.Add(this.btnExpressionEdit);
            this.groupBox4.Controls.Add(this.gbExpressions);
            this.groupBox4.Controls.Add(this.btnExpressionDelete);
            this.groupBox4.Controls.Add(this.btnExpressionAdd);
            this.groupBox4.Location = new System.Drawing.Point(8, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(393, 303);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Expressions";
            // 
            // btnExpressionClone
            // 
            this.btnExpressionClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpressionClone.Location = new System.Drawing.Point(312, 19);
            this.btnExpressionClone.Name = "btnExpressionClone";
            this.btnExpressionClone.Size = new System.Drawing.Size(75, 23);
            this.btnExpressionClone.TabIndex = 2;
            this.btnExpressionClone.Text = "Clone";
            this.btnExpressionClone.UseVisualStyleBackColor = true;
            this.btnExpressionClone.Click += new System.EventHandler(this.btnExpressionClone_Click);
            // 
            // lstExpressions
            // 
            this.lstExpressions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstExpressions.FormattingEnabled = true;
            this.lstExpressions.Location = new System.Drawing.Point(6, 19);
            this.lstExpressions.Name = "lstExpressions";
            this.lstExpressions.Size = new System.Drawing.Size(300, 147);
            this.lstExpressions.TabIndex = 0;
            this.lstExpressions.SelectedIndexChanged += new System.EventHandler(this.lstExpressions_SelectedIndexChanged);
            // 
            // btnExpressionEdit
            // 
            this.btnExpressionEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpressionEdit.Location = new System.Drawing.Point(312, 65);
            this.btnExpressionEdit.Name = "btnExpressionEdit";
            this.btnExpressionEdit.Size = new System.Drawing.Size(75, 23);
            this.btnExpressionEdit.TabIndex = 4;
            this.btnExpressionEdit.Text = "Edit";
            this.btnExpressionEdit.UseVisualStyleBackColor = true;
            this.btnExpressionEdit.Click += new System.EventHandler(this.btnExpressionEdit_Click);
            // 
            // gbExpressions
            // 
            this.gbExpressions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbExpressions.Controls.Add(this.label19);
            this.gbExpressions.Controls.Add(this.txtExpressionExpression);
            this.gbExpressions.Controls.Add(this.label18);
            this.gbExpressions.Controls.Add(this.txtExpressionUnit);
            this.gbExpressions.Controls.Add(this.label16);
            this.gbExpressions.Controls.Add(this.btnExpressionCancel);
            this.gbExpressions.Controls.Add(this.btnExpressionSave);
            this.gbExpressions.Controls.Add(this.txtExpressionName);
            this.gbExpressions.Controls.Add(this.label14);
            this.gbExpressions.Location = new System.Drawing.Point(8, 172);
            this.gbExpressions.Name = "gbExpressions";
            this.gbExpressions.Size = new System.Drawing.Size(298, 125);
            this.gbExpressions.TabIndex = 1;
            this.gbExpressions.TabStop = false;
            this.gbExpressions.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(67, 39);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(217, 15);
            this.label19.TabIndex = 14;
            this.label19.Text = "ex. ([pvdks_w] - 1000) * .0145";
            // 
            // txtExpressionExpression
            // 
            this.txtExpressionExpression.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpressionExpression.Location = new System.Drawing.Point(6, 57);
            this.txtExpressionExpression.Multiline = true;
            this.txtExpressionExpression.Name = "txtExpressionExpression";
            this.txtExpressionExpression.Size = new System.Drawing.Size(286, 35);
            this.txtExpressionExpression.TabIndex = 2;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(4, 39);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 13);
            this.label18.TabIndex = 13;
            this.label18.Text = "Expression:";
            // 
            // txtExpressionUnit
            // 
            this.txtExpressionUnit.Location = new System.Drawing.Point(251, 14);
            this.txtExpressionUnit.Name = "txtExpressionUnit";
            this.txtExpressionUnit.Size = new System.Drawing.Size(41, 20);
            this.txtExpressionUnit.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(220, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "Unit:";
            // 
            // btnExpressionCancel
            // 
            this.btnExpressionCancel.Location = new System.Drawing.Point(136, 96);
            this.btnExpressionCancel.Name = "btnExpressionCancel";
            this.btnExpressionCancel.Size = new System.Drawing.Size(75, 23);
            this.btnExpressionCancel.TabIndex = 3;
            this.btnExpressionCancel.Text = "Cancel";
            this.btnExpressionCancel.UseVisualStyleBackColor = true;
            this.btnExpressionCancel.Click += new System.EventHandler(this.btnExpressionCancel_Click);
            // 
            // btnExpressionSave
            // 
            this.btnExpressionSave.Location = new System.Drawing.Point(217, 96);
            this.btnExpressionSave.Name = "btnExpressionSave";
            this.btnExpressionSave.Size = new System.Drawing.Size(75, 23);
            this.btnExpressionSave.TabIndex = 4;
            this.btnExpressionSave.Text = "Save";
            this.btnExpressionSave.UseVisualStyleBackColor = true;
            this.btnExpressionSave.Click += new System.EventHandler(this.btnExpressionSave_Click);
            // 
            // txtExpressionName
            // 
            this.txtExpressionName.Location = new System.Drawing.Point(44, 14);
            this.txtExpressionName.Name = "txtExpressionName";
            this.txtExpressionName.Size = new System.Drawing.Size(174, 20);
            this.txtExpressionName.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "Name:";
            // 
            // btnExpressionDelete
            // 
            this.btnExpressionDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpressionDelete.Location = new System.Drawing.Point(312, 88);
            this.btnExpressionDelete.Name = "btnExpressionDelete";
            this.btnExpressionDelete.Size = new System.Drawing.Size(75, 23);
            this.btnExpressionDelete.TabIndex = 5;
            this.btnExpressionDelete.Text = "Delete";
            this.btnExpressionDelete.UseVisualStyleBackColor = true;
            this.btnExpressionDelete.Click += new System.EventHandler(this.btnExpressionDelete_Click);
            // 
            // btnExpressionAdd
            // 
            this.btnExpressionAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpressionAdd.Location = new System.Drawing.Point(312, 42);
            this.btnExpressionAdd.Name = "btnExpressionAdd";
            this.btnExpressionAdd.Size = new System.Drawing.Size(75, 23);
            this.btnExpressionAdd.TabIndex = 3;
            this.btnExpressionAdd.Text = "Add";
            this.btnExpressionAdd.UseVisualStyleBackColor = true;
            this.btnExpressionAdd.Click += new System.EventHandler(this.btnExpressionAdd_Click);
            // 
            // tbGraphConfig
            // 
            this.tbGraphConfig.BackColor = System.Drawing.Color.Transparent;
            this.tbGraphConfig.Controls.Add(this.nudResfreshRate);
            this.tbGraphConfig.Controls.Add(this.groupBox2);
            this.tbGraphConfig.Controls.Add(this.label10);
            this.tbGraphConfig.Controls.Add(this.groupBox1);
            this.tbGraphConfig.Location = new System.Drawing.Point(4, 22);
            this.tbGraphConfig.Name = "tbGraphConfig";
            this.tbGraphConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tbGraphConfig.Size = new System.Drawing.Size(725, 315);
            this.tbGraphConfig.TabIndex = 1;
            this.tbGraphConfig.Text = "Graph Data";
            // 
            // nudResfreshRate
            // 
            this.nudResfreshRate.Location = new System.Drawing.Point(505, 77);
            this.nudResfreshRate.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudResfreshRate.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudResfreshRate.Name = "nudResfreshRate";
            this.nudResfreshRate.Size = new System.Drawing.Size(94, 20);
            this.nudResfreshRate.TabIndex = 11;
            this.nudResfreshRate.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudGraphResV);
            this.groupBox2.Controls.Add(this.nudGraphResH);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(407, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(209, 65);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Graph Resolution (points)";
            // 
            // nudGraphResV
            // 
            this.nudGraphResV.Location = new System.Drawing.Point(98, 41);
            this.nudGraphResV.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudGraphResV.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudGraphResV.Name = "nudGraphResV";
            this.nudGraphResV.Size = new System.Drawing.Size(94, 20);
            this.nudGraphResV.TabIndex = 10;
            this.nudGraphResV.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // nudGraphResH
            // 
            this.nudGraphResH.Location = new System.Drawing.Point(98, 18);
            this.nudGraphResH.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudGraphResH.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudGraphResH.Name = "nudGraphResH";
            this.nudGraphResH.Size = new System.Drawing.Size(94, 20);
            this.nudGraphResH.TabIndex = 9;
            this.nudGraphResH.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(51, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Vertical:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Horizontal:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(408, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Refresh Rate (ms):";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.lstGraphVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstGraphVariables.FormattingEnabled = true;
            this.lstGraphVariables.Location = new System.Drawing.Point(6, 19);
            this.lstGraphVariables.Name = "lstGraphVariables";
            this.lstGraphVariables.Size = new System.Drawing.Size(300, 121);
            this.lstGraphVariables.TabIndex = 0;
            this.lstGraphVariables.SelectedIndexChanged += new System.EventHandler(this.lstGraphVariables_SelectedIndexChanged);
            // 
            // btnEditGraphVariable
            // 
            this.btnEditGraphVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.gbGraphVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbGraphVariables.Controls.Add(this.cmbGraphVariableVariable);
            this.gbGraphVariables.Controls.Add(this.label9);
            this.gbGraphVariables.Controls.Add(this.cmbGraphVariableStyle);
            this.gbGraphVariables.Controls.Add(this.chkGraphVariableActive);
            this.gbGraphVariables.Controls.Add(this.nudGraphVariableThickness);
            this.gbGraphVariables.Controls.Add(this.label8);
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
            // cmbGraphVariableVariable
            // 
            this.cmbGraphVariableVariable.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGraphVariableVariable.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGraphVariableVariable.FormattingEnabled = true;
            this.cmbGraphVariableVariable.Location = new System.Drawing.Point(51, 29);
            this.cmbGraphVariableVariable.Name = "cmbGraphVariableVariable";
            this.cmbGraphVariableVariable.Size = new System.Drawing.Size(241, 21);
            this.cmbGraphVariableVariable.TabIndex = 1;
            this.cmbGraphVariableVariable.SelectedValueChanged += new System.EventHandler(this.cmbGraphVariableVariable_SelectedValueChanged);
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
            // cmbGraphVariableStyle
            // 
            this.cmbGraphVariableStyle.FormattingEnabled = true;
            this.cmbGraphVariableStyle.Location = new System.Drawing.Point(111, 99);
            this.cmbGraphVariableStyle.Name = "cmbGraphVariableStyle";
            this.cmbGraphVariableStyle.Size = new System.Drawing.Size(80, 21);
            this.cmbGraphVariableStyle.TabIndex = 6;
            // 
            // chkGraphVariableActive
            // 
            this.chkGraphVariableActive.AutoSize = true;
            this.chkGraphVariableActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkGraphVariableActive.Location = new System.Drawing.Point(6, 10);
            this.chkGraphVariableActive.Name = "chkGraphVariableActive";
            this.chkGraphVariableActive.Size = new System.Drawing.Size(59, 17);
            this.chkGraphVariableActive.TabIndex = 0;
            this.chkGraphVariableActive.Text = "Active:";
            this.chkGraphVariableActive.UseVisualStyleBackColor = true;
            // 
            // nudGraphVariableThickness
            // 
            this.nudGraphVariableThickness.Location = new System.Drawing.Point(254, 99);
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
            this.label8.Location = new System.Drawing.Point(193, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Thickness:";
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
            this.txtGraphVariableName.TabIndex = 2;
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
            this.txtGraphVariableColor.TabIndex = 5;
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
            this.nudGraphVariableMax.TabIndex = 4;
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
            this.nudGraphVariableMin.TabIndex = 3;
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
            this.btnDeleteGraphVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.btnAddGraphVariable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddGraphVariable.Location = new System.Drawing.Point(312, 19);
            this.btnAddGraphVariable.Name = "btnAddGraphVariable";
            this.btnAddGraphVariable.Size = new System.Drawing.Size(75, 23);
            this.btnAddGraphVariable.TabIndex = 2;
            this.btnAddGraphVariable.Text = "Add";
            this.btnAddGraphVariable.UseVisualStyleBackColor = true;
            this.btnAddGraphVariable.Click += new System.EventHandler(this.btnAddGraphVariable_Click);
            // 
            // tpProfiles
            // 
            this.tpProfiles.BackColor = System.Drawing.SystemColors.Control;
            this.tpProfiles.Controls.Add(this.groupBox3);
            this.tpProfiles.Location = new System.Drawing.Point(4, 22);
            this.tpProfiles.Name = "tpProfiles";
            this.tpProfiles.Size = new System.Drawing.Size(725, 315);
            this.tpProfiles.TabIndex = 2;
            this.tpProfiles.Text = "Profiles";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.btnProfileSetCurrent);
            this.groupBox3.Controls.Add(this.btnProfileClone);
            this.groupBox3.Controls.Add(this.lstProfiles);
            this.groupBox3.Controls.Add(this.btnProfileEdit);
            this.groupBox3.Controls.Add(this.gbProfile);
            this.groupBox3.Controls.Add(this.btnProfileDelete);
            this.groupBox3.Controls.Add(this.btnProfileAdd);
            this.groupBox3.Location = new System.Drawing.Point(8, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(393, 303);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Profiles";
            // 
            // btnProfileSetCurrent
            // 
            this.btnProfileSetCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfileSetCurrent.Location = new System.Drawing.Point(312, 19);
            this.btnProfileSetCurrent.Name = "btnProfileSetCurrent";
            this.btnProfileSetCurrent.Size = new System.Drawing.Size(75, 23);
            this.btnProfileSetCurrent.TabIndex = 1;
            this.btnProfileSetCurrent.Text = "Set Current";
            this.btnProfileSetCurrent.UseVisualStyleBackColor = true;
            this.btnProfileSetCurrent.Click += new System.EventHandler(this.btnProfileSetCurrent_Click);
            // 
            // btnProfileClone
            // 
            this.btnProfileClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfileClone.Location = new System.Drawing.Point(312, 58);
            this.btnProfileClone.Name = "btnProfileClone";
            this.btnProfileClone.Size = new System.Drawing.Size(75, 23);
            this.btnProfileClone.TabIndex = 2;
            this.btnProfileClone.Text = "Clone";
            this.btnProfileClone.UseVisualStyleBackColor = true;
            this.btnProfileClone.Click += new System.EventHandler(this.btnProfileClone_Click);
            // 
            // lstProfiles
            // 
            this.lstProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstProfiles.FormattingEnabled = true;
            this.lstProfiles.Location = new System.Drawing.Point(6, 19);
            this.lstProfiles.Name = "lstProfiles";
            this.lstProfiles.Size = new System.Drawing.Size(300, 199);
            this.lstProfiles.TabIndex = 0;
            this.lstProfiles.SelectedIndexChanged += new System.EventHandler(this.lstProfiles_SelectedIndexChanged);
            // 
            // btnProfileEdit
            // 
            this.btnProfileEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfileEdit.Location = new System.Drawing.Point(312, 104);
            this.btnProfileEdit.Name = "btnProfileEdit";
            this.btnProfileEdit.Size = new System.Drawing.Size(75, 23);
            this.btnProfileEdit.TabIndex = 4;
            this.btnProfileEdit.Text = "Edit";
            this.btnProfileEdit.UseVisualStyleBackColor = true;
            this.btnProfileEdit.Click += new System.EventHandler(this.btnProfileEdit_Click);
            // 
            // gbProfile
            // 
            this.gbProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbProfile.Controls.Add(this.btnProfileCancel);
            this.gbProfile.Controls.Add(this.btnProfileSave);
            this.gbProfile.Controls.Add(this.txtProfileName);
            this.gbProfile.Controls.Add(this.label17);
            this.gbProfile.Location = new System.Drawing.Point(8, 224);
            this.gbProfile.Name = "gbProfile";
            this.gbProfile.Size = new System.Drawing.Size(298, 73);
            this.gbProfile.TabIndex = 1;
            this.gbProfile.TabStop = false;
            // 
            // btnProfileCancel
            // 
            this.btnProfileCancel.Location = new System.Drawing.Point(133, 45);
            this.btnProfileCancel.Name = "btnProfileCancel";
            this.btnProfileCancel.Size = new System.Drawing.Size(75, 23);
            this.btnProfileCancel.TabIndex = 1;
            this.btnProfileCancel.Text = "Cancel";
            this.btnProfileCancel.UseVisualStyleBackColor = true;
            this.btnProfileCancel.Click += new System.EventHandler(this.btnProfileCancel_Click);
            // 
            // btnProfileSave
            // 
            this.btnProfileSave.Location = new System.Drawing.Point(214, 45);
            this.btnProfileSave.Name = "btnProfileSave";
            this.btnProfileSave.Size = new System.Drawing.Size(75, 23);
            this.btnProfileSave.TabIndex = 2;
            this.btnProfileSave.Text = "Save";
            this.btnProfileSave.UseVisualStyleBackColor = true;
            this.btnProfileSave.Click += new System.EventHandler(this.btnProfileSave_Click);
            // 
            // txtProfileName
            // 
            this.txtProfileName.Location = new System.Drawing.Point(51, 19);
            this.txtProfileName.Name = "txtProfileName";
            this.txtProfileName.Size = new System.Drawing.Size(241, 20);
            this.txtProfileName.TabIndex = 0;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(11, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Name:";
            // 
            // btnProfileDelete
            // 
            this.btnProfileDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfileDelete.Location = new System.Drawing.Point(312, 127);
            this.btnProfileDelete.Name = "btnProfileDelete";
            this.btnProfileDelete.Size = new System.Drawing.Size(75, 23);
            this.btnProfileDelete.TabIndex = 5;
            this.btnProfileDelete.Text = "Delete";
            this.btnProfileDelete.UseVisualStyleBackColor = true;
            this.btnProfileDelete.Click += new System.EventHandler(this.btnProfileDelete_Click);
            // 
            // btnProfileAdd
            // 
            this.btnProfileAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProfileAdd.Location = new System.Drawing.Point(312, 81);
            this.btnProfileAdd.Name = "btnProfileAdd";
            this.btnProfileAdd.Size = new System.Drawing.Size(75, 23);
            this.btnProfileAdd.TabIndex = 3;
            this.btnProfileAdd.Text = "Add";
            this.btnProfileAdd.UseVisualStyleBackColor = true;
            this.btnProfileAdd.Click += new System.EventHandler(this.btnProfileAdd_Click);
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
            this.saveConfigFileAsToolStripMenuItem,
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
            this.loadECUFileToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.loadECUFileToolStripMenuItem.Text = "Load ECU File";
            this.loadECUFileToolStripMenuItem.Click += new System.EventHandler(this.loadECUFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 6);
            // 
            // loadConfigFileToolStripMenuItem
            // 
            this.loadConfigFileToolStripMenuItem.Enabled = false;
            this.loadConfigFileToolStripMenuItem.Name = "loadConfigFileToolStripMenuItem";
            this.loadConfigFileToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.loadConfigFileToolStripMenuItem.Text = "Load Config File";
            this.loadConfigFileToolStripMenuItem.Click += new System.EventHandler(this.loadConfigFileToolStripMenuItem_Click);
            // 
            // saveConfigFileToolStripMenuItem
            // 
            this.saveConfigFileToolStripMenuItem.Enabled = false;
            this.saveConfigFileToolStripMenuItem.Name = "saveConfigFileToolStripMenuItem";
            this.saveConfigFileToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveConfigFileToolStripMenuItem.Text = "Save Config File";
            this.saveConfigFileToolStripMenuItem.Click += new System.EventHandler(this.saveConfigFileToolStripMenuItem_Click);
            // 
            // saveConfigFileAsToolStripMenuItem
            // 
            this.saveConfigFileAsToolStripMenuItem.Name = "saveConfigFileAsToolStripMenuItem";
            this.saveConfigFileAsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveConfigFileAsToolStripMenuItem.Text = "Save Config File As...";
            this.saveConfigFileAsToolStripMenuItem.Click += new System.EventHandler(this.saveConfigFileAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(180, 6);
            // 
            // saveSettingsToolStripMenuItem
            // 
            this.saveSettingsToolStripMenuItem.Name = "saveSettingsToolStripMenuItem";
            this.saveSettingsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveSettingsToolStripMenuItem.Text = "Save Settings";
            this.saveSettingsToolStripMenuItem.Click += new System.EventHandler(this.saveSettingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(180, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem});
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
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 457);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SettingsForm";
            this.Text = "Visual ME7Logger";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpMeasurements.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tpExpressions.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.gbExpressions.ResumeLayout(false);
            this.gbExpressions.PerformLayout();
            this.tbGraphConfig.ResumeLayout(false);
            this.tbGraphConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudResfreshRate)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphResV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphResH)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.gbGraphVariables.ResumeLayout(false);
            this.gbGraphVariables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGraphVariableMin)).EndInit();
            this.tpProfiles.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.gbProfile.ResumeLayout(false);
            this.gbProfile.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadECUFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadConfigFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem saveSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudGraphVariableThickness;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkGraphVariableActive;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbGraphVariableStyle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudGraphResV;
        private System.Windows.Forms.NumericUpDown nudGraphResH;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudResfreshRate;
        private System.Windows.Forms.ToolStripMenuItem saveConfigFileAsToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radFilterUnselected;
        private System.Windows.Forms.RadioButton radFilterSelected;
        private System.Windows.Forms.RadioButton radFilterAll;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbGraphVariableVariable;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblMeasurementCount;
        private System.Windows.Forms.TabPage tpProfiles;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnProfileSetCurrent;
        private System.Windows.Forms.Button btnProfileClone;
        private System.Windows.Forms.ListBox lstProfiles;
        private System.Windows.Forms.Button btnProfileEdit;
        private System.Windows.Forms.GroupBox gbProfile;
        private System.Windows.Forms.Button btnProfileCancel;
        private System.Windows.Forms.Button btnProfileSave;
        private System.Windows.Forms.TextBox txtProfileName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnProfileDelete;
        private System.Windows.Forms.Button btnProfileAdd;
        private System.Windows.Forms.TabPage tpExpressions;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnExpressionClone;
        private System.Windows.Forms.ListBox lstExpressions;
        private System.Windows.Forms.Button btnExpressionEdit;
        private System.Windows.Forms.GroupBox gbExpressions;
        private System.Windows.Forms.Button btnExpressionCancel;
        private System.Windows.Forms.Button btnExpressionSave;
        private System.Windows.Forms.TextBox txtExpressionName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnExpressionDelete;
        private System.Windows.Forms.Button btnExpressionAdd;
        private System.Windows.Forms.TextBox txtExpressionExpression;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtExpressionUnit;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;


    }
}