namespace VisualME7Logger
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.increasePlaybackSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decreasePlaybackSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPlaybackSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.forwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtRefreshRate = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.chartTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbChartType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.freezeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snapImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.sessionOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDataGridViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeDataToLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.flpVariables = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.scrollbar = new System.Windows.Forms.HScrollBar();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.spDataGrid = new System.Windows.Forms.Splitter();
            this.dataGridView1 = new VisualME7Logger.DataGridViewCSVCopy();
            this.spSessionData = new System.Windows.Forms.Splitter();
            this.pnlSessionData = new System.Windows.Forms.Panel();
            this.txtSessionData = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOpenCloseSession = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlSessionData.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.toolStripStatusLabel1,
            this.lblInfo,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 850);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1312, 25);
            this.statusStrip1.TabIndex = 34;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 20);
            this.lblStatus.Text = "Status:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 20);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(38, 20);
            this.lblInfo.Text = "Info:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 20);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem1,
            this.stopToolStripMenuItem1,
            this.toolStripMenuItem5,
            this.pauseToolStripMenuItem,
            this.increasePlaybackSpeedToolStripMenuItem,
            this.decreasePlaybackSpeedToolStripMenuItem,
            this.resetPlaybackSpeedToolStripMenuItem,
            this.toolStripMenuItem7,
            this.forwardToolStripMenuItem,
            this.reverseToolStripMenuItem,
            this.toolStripMenuItem2,
            this.infoToolStripMenuItem,
            this.toolStripMenuItem4,
            this.exitToolStripMenuItem});
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.startToolStripMenuItem.Text = "&Log";
            // 
            // startToolStripMenuItem1
            // 
            this.startToolStripMenuItem1.Name = "startToolStripMenuItem1";
            this.startToolStripMenuItem1.Size = new System.Drawing.Size(291, 26);
            this.startToolStripMenuItem1.Text = "&Open";
            this.startToolStripMenuItem1.Click += new System.EventHandler(this.startToolStripMenuItem1_Click);
            // 
            // stopToolStripMenuItem1
            // 
            this.stopToolStripMenuItem1.Name = "stopToolStripMenuItem1";
            this.stopToolStripMenuItem1.Size = new System.Drawing.Size(291, 26);
            this.stopToolStripMenuItem1.Text = "&Close";
            this.stopToolStripMenuItem1.Click += new System.EventHandler(this.stopToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(288, 6);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.pauseToolStripMenuItem.Text = "&Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // increasePlaybackSpeedToolStripMenuItem
            // 
            this.increasePlaybackSpeedToolStripMenuItem.Name = "increasePlaybackSpeedToolStripMenuItem";
            this.increasePlaybackSpeedToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+";
            this.increasePlaybackSpeedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.increasePlaybackSpeedToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.increasePlaybackSpeedToolStripMenuItem.Text = "Increase Playback Speed";
            this.increasePlaybackSpeedToolStripMenuItem.Click += new System.EventHandler(this.increasePlaybackSpeedToolStripMenuItem_Click);
            // 
            // decreasePlaybackSpeedToolStripMenuItem
            // 
            this.decreasePlaybackSpeedToolStripMenuItem.Name = "decreasePlaybackSpeedToolStripMenuItem";
            this.decreasePlaybackSpeedToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl-";
            this.decreasePlaybackSpeedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.decreasePlaybackSpeedToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.decreasePlaybackSpeedToolStripMenuItem.Text = "Decrease Playback Speed";
            this.decreasePlaybackSpeedToolStripMenuItem.Click += new System.EventHandler(this.decreasePlaybackSpeedToolStripMenuItem_Click);
            // 
            // resetPlaybackSpeedToolStripMenuItem
            // 
            this.resetPlaybackSpeedToolStripMenuItem.Name = "resetPlaybackSpeedToolStripMenuItem";
            this.resetPlaybackSpeedToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.resetPlaybackSpeedToolStripMenuItem.Text = "Reset Playback Speed";
            this.resetPlaybackSpeedToolStripMenuItem.Click += new System.EventHandler(this.resetPlaybackSpeedToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(288, 6);
            // 
            // forwardToolStripMenuItem
            // 
            this.forwardToolStripMenuItem.Name = "forwardToolStripMenuItem";
            this.forwardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.forwardToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.forwardToolStripMenuItem.Text = "Forward";
            this.forwardToolStripMenuItem.Click += new System.EventHandler(this.forwardToolStripMenuItem_Click);
            // 
            // reverseToolStripMenuItem
            // 
            this.reverseToolStripMenuItem.Name = "reverseToolStripMenuItem";
            this.reverseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.reverseToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.reverseToolStripMenuItem.Text = "Reverse";
            this.reverseToolStripMenuItem.Click += new System.EventHandler(this.reverseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(288, 6);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.infoToolStripMenuItem.Text = "&Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(288, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(291, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshRateToolStripMenuItem,
            this.txtRefreshRate,
            this.toolStripMenuItem3,
            this.chartTypeToolStripMenuItem,
            this.cmbChartType,
            this.toolStripMenuItem1,
            this.freezeToolStripMenuItem,
            this.snapImageToolStripMenuItem,
            this.toolStripMenuItem6,
            this.sessionOutputToolStripMenuItem,
            this.showDataGridViewToolStripMenuItem,
            this.writeDataToLogFileToolStripMenuItem});
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.graphToolStripMenuItem.Text = "&Options";
            // 
            // refreshRateToolStripMenuItem
            // 
            this.refreshRateToolStripMenuItem.Enabled = false;
            this.refreshRateToolStripMenuItem.Name = "refreshRateToolStripMenuItem";
            this.refreshRateToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.refreshRateToolStripMenuItem.Text = "Refresh Rate (ms)";
            // 
            // txtRefreshRate
            // 
            this.txtRefreshRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRefreshRate.Name = "txtRefreshRate";
            this.txtRefreshRate.Size = new System.Drawing.Size(100, 27);
            this.txtRefreshRate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRefreshRate_KeyUp);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(273, 6);
            this.toolStripMenuItem3.Visible = false;
            // 
            // chartTypeToolStripMenuItem
            // 
            this.chartTypeToolStripMenuItem.Enabled = false;
            this.chartTypeToolStripMenuItem.Name = "chartTypeToolStripMenuItem";
            this.chartTypeToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.chartTypeToolStripMenuItem.Text = "Chart Type";
            this.chartTypeToolStripMenuItem.Visible = false;
            // 
            // cmbChartType
            // 
            this.cmbChartType.Name = "cmbChartType";
            this.cmbChartType.Size = new System.Drawing.Size(121, 28);
            this.cmbChartType.Visible = false;
            this.cmbChartType.SelectedIndexChanged += new System.EventHandler(this.cmbChartType_SelectedIndexChanged);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(273, 6);
            // 
            // freezeToolStripMenuItem
            // 
            this.freezeToolStripMenuItem.Name = "freezeToolStripMenuItem";
            this.freezeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.freezeToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.freezeToolStripMenuItem.Text = "&Freeze";
            this.freezeToolStripMenuItem.Click += new System.EventHandler(this.freezeToolStripMenuItem_Click);
            // 
            // snapImageToolStripMenuItem
            // 
            this.snapImageToolStripMenuItem.Name = "snapImageToolStripMenuItem";
            this.snapImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.snapImageToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.snapImageToolStripMenuItem.Text = "&Capture Graph Image";
            this.snapImageToolStripMenuItem.Click += new System.EventHandler(this.snapImageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(273, 6);
            // 
            // sessionOutputToolStripMenuItem
            // 
            this.sessionOutputToolStripMenuItem.Name = "sessionOutputToolStripMenuItem";
            this.sessionOutputToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.sessionOutputToolStripMenuItem.Text = "Show Session Output";
            this.sessionOutputToolStripMenuItem.Click += new System.EventHandler(this.sessionOutputToolStripMenuItem_Click);
            // 
            // showDataGridViewToolStripMenuItem
            // 
            this.showDataGridViewToolStripMenuItem.Name = "showDataGridViewToolStripMenuItem";
            this.showDataGridViewToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.showDataGridViewToolStripMenuItem.Text = "Show Data Grid View";
            this.showDataGridViewToolStripMenuItem.Click += new System.EventHandler(this.showDataGridViewToolStripMenuItem_Click);
            // 
            // writeDataToLogFileToolStripMenuItem
            // 
            this.writeDataToLogFileToolStripMenuItem.Name = "writeDataToLogFileToolStripMenuItem";
            this.writeDataToLogFileToolStripMenuItem.ShortcutKeyDisplayString = "Crtl+W";
            this.writeDataToLogFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.writeDataToLogFileToolStripMenuItem.Size = new System.Drawing.Size(276, 26);
            this.writeDataToLogFileToolStripMenuItem.Text = "Write To Log";
            this.writeDataToLogFileToolStripMenuItem.Click += new System.EventHandler(this.writeDataToLogFileToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.graphToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1312, 28);
            this.menuStrip1.TabIndex = 35;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // flpVariables
            // 
            this.flpVariables.AutoScroll = true;
            this.flpVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpVariables.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpVariables.Location = new System.Drawing.Point(0, 46);
            this.flpVariables.Margin = new System.Windows.Forms.Padding(0);
            this.flpVariables.Name = "flpVariables";
            this.flpVariables.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flpVariables.Size = new System.Drawing.Size(379, 776);
            this.flpVariables.TabIndex = 39;
            this.flpVariables.WrapContents = false;
            this.flpVariables.Resize += new System.EventHandler(this.flpVariables_Resize);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1312, 822);
            this.panel1.TabIndex = 40;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.scrollbar);
            this.panel3.Controls.Add(this.chart1);
            this.panel3.Controls.Add(this.spDataGrid);
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Controls.Add(this.spSessionData);
            this.panel3.Controls.Add(this.pnlSessionData);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(383, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(929, 822);
            this.panel3.TabIndex = 47;
            // 
            // scrollbar
            // 
            this.scrollbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.scrollbar.Location = new System.Drawing.Point(0, 486);
            this.scrollbar.Name = "scrollbar";
            this.scrollbar.Size = new System.Drawing.Size(929, 20);
            this.scrollbar.TabIndex = 54;
            this.scrollbar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            this.scrollbar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.scrollbar_KeyUp);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chart1.BorderlineWidth = 0;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelAutoFitMaxFontSize = 8;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LabelStyle.Interval = 0D;
            chartArea1.AxisX.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisX2.MajorGrid.Enabled = false;
            chartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.BlanchedAlmond;
            chartArea1.AxisX2.MajorTickMark.Enabled = false;
            chartArea1.AxisY.LabelStyle.Enabled = false;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.LineWidth = 0;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MinorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.InsideArea;
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorTickMark.Enabled = false;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.CursorX.Interval = 5D;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorX.LineColor = System.Drawing.Color.RoyalBlue;
            chartArea1.Name = "Default";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(929, 506);
            this.chart1.TabIndex = 47;
            this.chart1.Text = "chart";
            this.chart1.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chart1_AxisViewChanged);
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            this.chart1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.chart1_KeyUp);
            // 
            // spDataGrid
            // 
            this.spDataGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spDataGrid.Location = new System.Drawing.Point(0, 506);
            this.spDataGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.spDataGrid.Name = "spDataGrid";
            this.spDataGrid.Size = new System.Drawing.Size(929, 4);
            this.spDataGrid.TabIndex = 51;
            this.spDataGrid.TabStop = false;
            this.spDataGrid.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 510);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.dataGridView1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGridView1.RowTemplate.Height = 15;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(929, 185);
            this.dataGridView1.TabIndex = 50;
            this.dataGridView1.Visible = false;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.VisibleChanged += new System.EventHandler(this.dataGridView1_VisibleChanged);
            // 
            // spSessionData
            // 
            this.spSessionData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.spSessionData.Location = new System.Drawing.Point(0, 695);
            this.spSessionData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.spSessionData.Name = "spSessionData";
            this.spSessionData.Size = new System.Drawing.Size(929, 4);
            this.spSessionData.TabIndex = 49;
            this.spSessionData.TabStop = false;
            this.spSessionData.Visible = false;
            // 
            // pnlSessionData
            // 
            this.pnlSessionData.Controls.Add(this.txtSessionData);
            this.pnlSessionData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSessionData.Location = new System.Drawing.Point(0, 699);
            this.pnlSessionData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlSessionData.Name = "pnlSessionData";
            this.pnlSessionData.Size = new System.Drawing.Size(929, 123);
            this.pnlSessionData.TabIndex = 52;
            this.pnlSessionData.Visible = false;
            // 
            // txtSessionData
            // 
            this.txtSessionData.BackColor = System.Drawing.SystemColors.Control;
            this.txtSessionData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSessionData.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSessionData.Location = new System.Drawing.Point(0, 0);
            this.txtSessionData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSessionData.Multiline = true;
            this.txtSessionData.Name = "txtSessionData";
            this.txtSessionData.ReadOnly = true;
            this.txtSessionData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSessionData.Size = new System.Drawing.Size(929, 123);
            this.txtSessionData.TabIndex = 49;
            this.txtSessionData.WordWrap = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(379, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 822);
            this.splitter1.TabIndex = 42;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.flpVariables);
            this.panel2.Controls.Add(this.btnOpenCloseSession);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(379, 822);
            this.panel2.TabIndex = 41;
            // 
            // btnOpenCloseSession
            // 
            this.btnOpenCloseSession.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOpenCloseSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenCloseSession.Location = new System.Drawing.Point(0, 0);
            this.btnOpenCloseSession.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenCloseSession.Name = "btnOpenCloseSession";
            this.btnOpenCloseSession.Size = new System.Drawing.Size(379, 46);
            this.btnOpenCloseSession.TabIndex = 40;
            this.btnOpenCloseSession.Text = "Open Session";
            this.btnOpenCloseSession.UseVisualStyleBackColor = true;
            this.btnOpenCloseSession.Click += new System.EventHandler(this.btnOpenCloseSession_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 875);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visual ME7Logger";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlSessionData.ResumeLayout(false);
            this.pnlSessionData.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chartTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cmbChartType;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem snapImageToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem refreshRateToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox txtRefreshRate;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem freezeToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flpVariables;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOpenCloseSession;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem increasePlaybackSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decreasePlaybackSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetPlaybackSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sessionOutputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDataGridViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Splitter spSessionData;
        private System.Windows.Forms.Splitter spDataGrid;
        private DataGridViewCSVCopy dataGridView1;
        private System.Windows.Forms.Panel pnlSessionData;
        private System.Windows.Forms.TextBox txtSessionData;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem forwardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reverseToolStripMenuItem;
        private System.Windows.Forms.HScrollBar scrollbar;
        private System.Windows.Forms.ToolStripMenuItem writeDataToLogFileToolStripMenuItem;
    }
}

