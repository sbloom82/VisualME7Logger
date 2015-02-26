using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualME7Logger.Session;
using VisualME7Logger.Log;

using System.Windows.Forms.DataVisualization.Charting;

namespace VisualME7Logger
{
    public partial class Form1 : Form
    {
        public ME7LoggerSession session;

        DisplayOptions DisplayOptions;
        DateTime start;

        Queue<LogLine> queue;
        List<LogLine> buffer;

        Timer refreshTimer = new Timer();

        public Form1(string configFile, VisualME7Logger.Session.LoggerOptions options, DisplayOptions displayOptions)
        {
            InitializeComponent();

            this.pnlSessionData.Visible = this.spSessionData.Visible = sessionOutputToolStripMenuItem.Checked;

            this.DisplayOptions = displayOptions;
            foreach (var v in Enum.GetValues(typeof(SeriesChartType)))
            {
                cmbChartType.Items.Add(v);
            }
            cmbChartType.SelectedItem = SeriesChartType.FastLine;

            refreshTimer.Tick += refreshTimer_Tick;
            refreshTimer.Interval = DisplayOptions.RefreshInterval;
            txtRefreshRate.Text = DisplayOptions.RefreshInterval.ToString();

            if (options.ConnectionType == LoggerOptions.ConnectionTypes.LogFile)
            {
                session = new ME7LoggerSession(Program.ME7LoggerDirectory, options.LogFile,
                    Program.DebugOutput ?
                        ME7LoggerSession.SessionTypes.SessionOutput :
                        ME7LoggerSession.SessionTypes.LogFile);
            }
            else
            {
                session = new ME7LoggerSession(Program.ME7LoggerDirectory, options.LogFile, options, configFile);
            }
            session.ExpressionVariables = DisplayOptions.Expressions;

            session.StatusChanged += new ME7LoggerSession.LoggerSessionStatusChanged(this.SessionStatusChanged);
            session.LogLineRead += new ME7LoggerSession.LogLineReadDel(this.LogLineRead);
            session.DataRead += new ME7LoggerSession.SessionDataRead(this.SessionDataRead);

            this.OpenSession();

            pauseToolStripMenuItem.Enabled =
            forwardToolStripMenuItem.Enabled = 
            reverseToolStripMenuItem.Enabled =
            scrollbar.Visible =
            increasePlaybackSpeedToolStripMenuItem.Enabled =
                decreasePlaybackSpeedToolStripMenuItem.Enabled =
                resetPlaybackSpeedToolStripMenuItem.Enabled = session.CanSetPlaybackSpeed;
        }

        void SessionStatusChanged(ME7LoggerSession.Statuses status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate() { SessionStatusChanged(status); }));
                return;
            }

            txtSessionData.AppendText(string.Format("VISUALME7LOGGER STATUS: {0}\r\n", status));
            if (status == ME7LoggerSession.Statuses.Closed)
            {
                txtSessionData.AppendText("\r\n**********************VISUALME7LOGGER SESSION CLOSED**********************\r\n\r\n");
            }

            lblStatus.Text = string.Format("Log Status: {0} - Samples: {1}/sec{2}",
                status,
                session.SamplesPerSecond,
                session.CurrentSamplesPerSecond != session.SamplesPerSecond ? string.Format(" - Displaying: {0}/sec", session.CurrentSamplesPerSecond) : "");

            this.btnOpenCloseSession.Enabled = false;
            this.showDataGridViewToolStripMenuItem.Enabled = true;
            if (status == ME7LoggerSession.Statuses.Initialized)
            {
                this.Initialize();
            }
            else if (status == ME7LoggerSession.Statuses.Open)
            {
                refreshTimer.Start();
                flpVariables_Resize(null, null);
                this.btnOpenCloseSession.Enabled = true;
                this.btnOpenCloseSession.Text = "Close Session";

                this.showDataGridViewToolStripMenuItem.Enabled =
                    this.spDataGrid.Visible =
                    this.dataGridView1.Visible =
                    this.showDataGridViewToolStripMenuItem.Checked = false;
            }
            else if (status == ME7LoggerSession.Statuses.Closed)
            {
                refreshTimer.Stop();
                if (session.ExitCode > 0)
                {
                    MessageBox.Show(string.Format("Error! Exit Code:{0}{1}{2}", session.ExitCode, Environment.NewLine, session.ErrorText));
                }
                this.btnOpenCloseSession.Enabled = true;
                this.btnOpenCloseSession.Text = "Open Session";
            }
            else if (status == ME7LoggerSession.Statuses.Paused)
            {
                refreshTimer.Stop();
            }
        }

        void SessionDataRead(string line, bool error = false)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate() { SessionDataRead(line, error); }));
                return;
            }
            this.txtSessionData.AppendText(string.Format("{0}{1}\r\n", error ? "***" : "", line));
        }

        void LogLineRead(LogLine line)
        {
             queue.Enqueue(line);
        }

        void Initialize()
        {
            queue = new Queue<LogLine>();
            buffer = new List<LogLine>();

            flpVariables.Controls.Clear();

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Timestamp", "TIME");
            foreach (SessionVariable v in session.Variables.Values)
            {
                bool hasActiveGraphVariable = this.DisplayOptions.GraphVariables.Any(gv => gv.Variable == v.Name && gv.Active);
                dataGridView1.Columns.Add(v.Name, string.IsNullOrEmpty(v.Alias) ? v.Name : v.Alias);

                FlowLayoutPanel flp = new FlowLayoutPanel();
                flp.Name = v.Name;
                flp.FlowDirection = FlowDirection.LeftToRight;
                flp.WrapContents = false;
                flp.AutoSize = true;
                flp.Margin = new Padding(0, 1, 0, 1);
                flp.Tag = v;
                flp.MouseUp += flp_MouseUp;

                Label name = new Label();
                Font f = new Font(name.Font.FontFamily, name.Font.Size + 1, hasActiveGraphVariable ? FontStyle.Bold : FontStyle.Regular);

                name.Name = v.Name;
                name.Height = 20;
                name.Text = v.ToString();
                name.BorderStyle = BorderStyle.Fixed3D;
                name.TextAlign = ContentAlignment.MiddleLeft;
                name.Font = f;
                name.RightToLeft = System.Windows.Forms.RightToLeft.No;
                name.Tag = v;
                name.MouseUp += flp_MouseUp;

                Label value = new Label();
                value.Name = v.Name;
                value.TextAlign = ContentAlignment.MiddleLeft;
                value.Height = 20;
                value.BorderStyle = BorderStyle.Fixed3D;
                value.Font = f;
                value.RightToLeft = System.Windows.Forms.RightToLeft.No;
                value.Tag = v;
                value.MouseUp += flp_MouseUp;

                flp.Controls.Add(value);
                flp.Controls.Add(name);
                flpVariables.Controls.Add(flp);
            }

            this.BuildChart();

            start = DateTime.Now;
        }

        void refreshTimer_Tick(object sender, EventArgs e)
        {
            while (queue.Count() > 0)
            {
                LogLine line = queue.Dequeue();

                if (this.session.SessionType == ME7LoggerSession.SessionTypes.LogFile)
                {
                    this.scrollbar.Maximum = (int)this.session.Log.TotalFileSize;
                    this.scrollbar.Value = (int)this.session.Log.CurrentPosition;                    
                }

                lock (buffer)
                {
                    buffer.Add(line);
                    if (buffer.Count > this.DisplayOptions.GraphHRes)
                    {
                        buffer.RemoveAt(0);
                    }
                }

                this.AddLineToGrid(line);
                this.DisplayLine(line);
                this.PlotLineOnChart(line);
            }
        }

        void AddLineToGrid(LogLine line)
        {
            if (dataGridView1.Visible)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Height = 14;
                row.Tag = line;
                object[] gridValues = new object[line.Variables.Count() + 1];
                int i = 0;
                gridValues[i++] = line.TimeStamp;
                foreach (Variable var in line.Variables)
                {
                    gridValues[i++] = var.Value;
                }
                row.CreateCells(dataGridView1, gridValues);

                int firstDisplayed = dataGridView1.FirstDisplayedScrollingRowIndex;
                int lastVisible = firstDisplayed + dataGridView1.DisplayedRowCount(true) - 1;
                int lastIndex = dataGridView1.RowCount - 1;

                dataGridView1.Rows.Add(row);
                if (dataGridView1.Rows.Count > this.DisplayOptions.GraphHRes)
                {
                    dataGridView1.Rows.RemoveAt(0);
                }

                if (lastVisible == lastIndex)
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = firstDisplayed + 1;
                }
            }
        }

        void DisplayLine(LogLine line)
        {
            if (lblInfo.Tag == null || DateTime.Now.Subtract((DateTime)lblInfo.Tag).TotalSeconds > 3)
            {
                lblInfo.Text = string.Format("Timestamp: {0}", line.TimeStamp.ToString());
            }

            for (int i = 0; i < line.Variables.Count(); ++i)
            {
                Label l = (Label)flpVariables.Controls[i].Controls[0];
                Variable v = line.Variables.ElementAt(i);
                l.Text = string.Format("{0} {1}", v.Value, v.SessionVariable.Unit);
            }
        }

        void flp_MouseUp(object sender, EventArgs e)
        {
            SessionVariable v = ((Control)sender).Tag as SessionVariable;
            if (v != null)
            {
                bool newVar = false;
                GraphVariable graphVariable = this.DisplayOptions.GraphVariables.FirstOrDefault(gv => gv.Variable == v.Name);
                if (graphVariable == null)
                {
                    newVar = true;
                    graphVariable = new GraphVariable()
                    {
                        Variable = v.Name,
                        Name = v.Alias
                    };
                }
                GraphVariableForm gvf = new GraphVariableForm(graphVariable);
                if (DialogResult.OK == gvf.ShowDialog())
                {
                    Control panel = (Control)sender;
                    if (sender is Label)
                    {
                        panel = ((Control)sender).Parent;
                    }
                    this.AddGraphVariable(graphVariable, panel, newVar);
                }
                
                /*
                GaugeWindow gw = new GaugeWindow(session, v);
                session.LogLineRead += new ME7LoggerSession.LogLineReadDel(gw.Refresh);
                gw.Show(this);
                */
            }
        }

        void graphed_Click(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            SessionVariable v = c.Tag as SessionVariable;
            if (v != null)
            {
                bool newVar = false;
                GraphVariable graphVariable = this.DisplayOptions.GraphVariables.FirstOrDefault(gv => gv.Variable == v.Name);
                if (graphVariable == null)
                {
                    newVar = true;
                    graphVariable = new GraphVariable()
                    {
                        Variable = v.Name,
                        Name = v.Alias
                    };
                }
                graphVariable.Active = c.Checked;

                this.AddGraphVariable(graphVariable, c.Parent, newVar);
            }
        }

        void AddGraphVariable(GraphVariable graphVariable, Control panel, bool isNew)
        {
            if (isNew)
            {
                this.DisplayOptions.GraphVariables.Add(graphVariable);
            }
            this.BuildChart();

            Font f = panel.Controls[0].Font;
            f = new Font(f, graphVariable.Active ? FontStyle.Bold : FontStyle.Regular);
            foreach (Control c in panel.Controls)
            {
                if (c is Label)
                {
                    c.Font = f;
                }
                else if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = graphVariable.Active;
                }
            }
        }

        void BuildChart()
        {            
            chart1.ChartAreas["Default"].AxisY.Minimum = 0;
            chart1.ChartAreas["Default"].AxisY.Maximum = this.DisplayOptions.GraphVRes;

            chart1.Series.Clear();

            foreach (GraphVariable graphVariable in this.DisplayOptions.GraphVariables)
            {
                SessionVariable var = session.Variables[graphVariable.Variable];
                if (var != null)
                {
                    Series s = new Series(string.Format("{0} {1}-{2} {3}", graphVariable.Name, graphVariable.Min.ToString("0.##"), graphVariable.Max.ToString("0.##"), var.Unit));
                    s.Color = graphVariable.LineColor;
                    s.ChartType = (SeriesChartType)cmbChartType.SelectedItem;
                    s.BorderWidth = graphVariable.LineThickness;
                    s.BorderDashStyle = graphVariable.LineStyle;
                    s.Enabled = graphVariable.Active;
                    s.ToolTip = "so tool tips show up";
                    chart1.Series.Add(s);

                    for (int i = 0; i < this.DisplayOptions.GraphHRes; ++i)
                    {
                        s.Points.Add(-1, -1).AxisLabel = "0";
                    }
                }
            }

            this.AddAxis();
           
            foreach (LogLine line in buffer)
            {
                this.PlotLineOnChart(line);
            }
        }

        List<ChartArea> axis;
        void ClearAxis() 
        {
            if (axis != null)
            {
                foreach (ChartArea axisArea in axis)
                {
                    chart1.ChartAreas.Remove(axisArea);
                }
            }
            axis = new List<ChartArea>();
        }
        void AddAxis()
        {
            this.ClearAxis();

            int i = 0;
            foreach (var group in this.DisplayOptions.GraphVariables.Where(v => v.Active && v.ShowAxis && v.Min < v.Max).GroupBy(v => new { v.Min, v.Max }))
            {
                GraphVariable firstVar = group.First();
                Guid g = Guid.NewGuid();
                ChartArea ca = new ChartArea(g.ToString());
                ca.AxisY.Minimum = (double)firstVar.Min;
                ca.AxisY.Maximum = (double)firstVar.Max;
                ca.Tag = firstVar;

                ca.BackColor = Color.Transparent;
                foreach (Axis a in ca.Axes)
                {
                    a.LineWidth = 3;
                    a.LineColor = firstVar.LineColor;
                }

                ca.AxisY.LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont;
                ca.InnerPlotPosition.Height = 95;
                ca.InnerPlotPosition.Width = 100;
                ca.InnerPlotPosition.X = 100;
                ca.InnerPlotPosition.Y = 0;
                ca.AxisY.LabelStyle.TruncatedLabels = false;
                ca.AxisY.LabelStyle.ForeColor = Color.White;
                ca.AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.AcrossAxis;
                ca.AxisY.MajorTickMark.LineWidth = 2;
                ca.AxisY.MajorTickMark.LineColor = firstVar.LineColor;
                ca.AxisX.MajorTickMark.Enabled = false;

                ca.Position.X = (i++ * 4);
                ca.Position.Y = (this.DisplayOptions.GraphVariables.Count(v => v.Active) / 2) + 5; 
                ca.Position.Width = 4f;
                ca.Position.Height = 92 - (this.DisplayOptions.GraphVariables.Count(v => v.Active) / 2);
                
                Series s = new Series();
                s.IsVisibleInLegend = false;
                s.ChartArea = g.ToString();
                s.Points.Add();
               
                chart1.Series.Add(s);
                chart1.ChartAreas.Add(ca);

                axis.Add(ca);
            }

            ChartArea _default = chart1.ChartAreas["Default"];
            Legend l = chart1.Legends[0];
            l.Position.X = 2;
            l.Position.Y  = 2;
            l.Position.Width = 100;
            l.Position.Height = this.DisplayOptions.GraphVariables.Count(v => v.Active) / 2;

            _default.InnerPlotPosition.Height = 95;
            _default.InnerPlotPosition.Width = 96f;
            _default.InnerPlotPosition.X = 2;
            _default.InnerPlotPosition.Y = 0;

            _default.Position.X = (axis.Count * 4) - (axis.Count == 0 ? 0 : 2 - (axis.Count * .10f));
            _default.Position.Y = (this.DisplayOptions.GraphVariables.Count(v => v.Active) / 2) + 5; 
            _default.Position.Width = 100 - (axis.Count * 4);
            _default.Position.Height = 92 - (this.DisplayOptions.GraphVariables.Count(v => v.Active) / 2);        
        }

        void PlotLineOnChart(LogLine line)
        {
            int i = 0;
            foreach (GraphVariable graphVariable in this.DisplayOptions.GraphVariables)
            {
                if (session.Variables.GetByName(graphVariable.Variable) != null)
                {
                    Variable v = line[graphVariable.Variable];
                    Series s = chart1.Series[i++];

                    decimal percent = (v.Value - graphVariable.Min) / (graphVariable.Max - graphVariable.Min) * this.DisplayOptions.GraphVRes;
                    DataPoint p = s.Points.Add((double)percent);
                    p.AxisLabel = decimal.Round(line.TimeStamp, 1).ToString();
                    p.ToolTip = string.Format("{0}: {1} {2}\r\nMin: {3} {2}\r\nMax: {4} {2}", graphVariable.Name, v.Value, v.SessionVariable.Unit, v.CurrentMinValue, v.CurrentMaxValue);
                    p.Tag = v;

                    s.Points.RemoveAt(0);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.CloseSession())
            {
                e.Cancel = true;
            }
        }

        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetChartType((SeriesChartType)cmbChartType.SelectedItem);
        }

        private void SetChartType(SeriesChartType type)
        {
            chart1.SuspendLayout();
            foreach (Series s in chart1.Series)
            {
                s.ChartType = type;
            }
            chart1.ResumeLayout();
        }

        private void snapImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaptureGraphImage();
        }

        void CaptureGraphImage()
        {
            string path = System.IO.Path.Combine(Program.ME7LoggerDirectory, "logs",
                string.Format("graphCapture{0}.jpg", Guid.NewGuid().ToString("D")));
            try
            {
                chart1.SaveImage(path, ChartImageFormat.Jpeg);
                lblInfo.Text = "Info: Image saved to log directory";
                lblInfo.Tag = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving image.  " + ex.ToString());
            }
        }

        private void OpenSession()
        {
            if (!session.IsOpen)
            {
                try
                {
                    session.Open();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    session.Close();
                }
            }
            else
            {
                MessageBox.Show("A session is already open");
            }
        }

        private bool CloseSession()
        {
            if (session.Status != ME7LoggerSession.Statuses.Closed)
            {
                if (DialogResult.No ==
                   MessageBox.Show(this, "Do you wish to close the session?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    return false;
                }

                session.Close();
            }
            return true;
        }

        private void startToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenSession();
        }

        private void stopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.CloseSession();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s;
            if (session.CommunicationInfo == null)
            {
                s = "Not Connected!";
            }
            else
            {
                s = string.Format("Communication:\r\nConnect={0}\r\nCommunication={1}\r\nLogSpeed={2}",
                     session.CommunicationInfo.Connect,
                     session.CommunicationInfo.Communicate,
                     session.CommunicationInfo.LogSpeed);

                s += string.Format("\r\n\r\nIdentification:\r\nHW#={0}\r\nSW#={1}\r\nPart#={2}\r\nEngineId={3}",
                    session.IdentificationInfo.HWNumber,
                    session.IdentificationInfo.SWNumber,
                    session.IdentificationInfo.PartNumber,
                    session.IdentificationInfo.EngineId);
            }
            MessageBox.Show(this, s);
        }

        private void txtRefreshRate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int value;
                bool success = false;
                if (int.TryParse(txtRefreshRate.Text, out value))
                {
                    if (value > 0 && value <= 5000)
                    {
                        success = true;
                        refreshTimer.Interval = value;
                    }
                }

                if (!success)
                {
                    txtRefreshRate.Text = refreshTimer.Interval.ToString();
                }
                e.Handled = true;
            }
        }

        private void freezeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoFreeze();
        }

        void HighlightPoints(bool highlight = true)
        {
            if (highlight && (SeriesChartType)cmbChartType.SelectedItem == SeriesChartType.FastLine)
            {
                SetChartType(SeriesChartType.Line);
            }
            else if (!highlight)
            {
                SetChartType((SeriesChartType)cmbChartType.SelectedItem);
            }

            foreach (Series s in chart1.Series)
            {

                double size = chart1.ChartAreas["Default"].AxisX.ScaleView.Size;
                double pos = chart1.ChartAreas["Default"].AxisX.ScaleView.Position;
                bool zoomed = chart1.ChartAreas["Default"].AxisX.ScaleView.IsZoomed;
                DataPoint lowest = null;
                DataPoint highest = null;
                int currentPos = 0;
                foreach (DataPoint p in s.Points)
                {
                    p.Label = null;

                    if (!zoomed || (currentPos >= pos && currentPos <= pos + size))
                    {
                        if (p.YValues[0] > 0 && (lowest == null || p.YValues[0] <= lowest.YValues[0]))
                        {
                            lowest = p;
                        }

                        if (highest == null || p.YValues[0] >= highest.YValues[0])
                        {
                            highest = p;
                        }
                    }
                    currentPos++;
                }

                if (highlight)
                {
                    if (lowest != null)
                    {
                        Variable v = lowest.Tag as Variable;
                        if (v != null)
                        {
                            GraphVariable graphVar = this.DisplayOptions.GraphVariables.FirstOrDefault(gv => gv.Variable.Equals(v.SessionVariable.Name, StringComparison.InvariantCultureIgnoreCase));
                            lowest.Label = string.Format("{0}: {1} {2}", graphVar != null ? graphVar.Name : v.SessionVariable.Name, v.Value, v.SessionVariable.Unit);
                            lowest.LabelForeColor = Color.White;
                        }
                    }

                    if (highest != null)
                    {
                        Variable v = highest.Tag as Variable;
                        if (v != null)
                        {
                            GraphVariable graphVar = this.DisplayOptions.GraphVariables.FirstOrDefault(gv => gv.Variable.Equals(v.SessionVariable.Name, StringComparison.InvariantCultureIgnoreCase));
                            highest.Label = string.Format("{0}: {1} {2}", graphVar != null ? graphVar.Name : v.SessionVariable.Name, v.Value, v.SessionVariable.Unit);
                            highest.LabelForeColor = Color.White;
                        }
                    }
                }
            }
        }

        void DoFreeze()
        {
            this.freezeToolStripMenuItem.Checked =
                   !this.freezeToolStripMenuItem.Checked;

            if (this.freezeToolStripMenuItem.Checked)
            {
                refreshTimer.Stop();
                HighlightPoints();

                this.showDataGridViewToolStripMenuItem.Enabled = true;
            }
            if (!this.freezeToolStripMenuItem.Checked && this.session.Status == ME7LoggerSession.Statuses.Open)
            {
                HighlightPoints(false);
                refreshTimer.Start();

                this.showDataGridViewToolStripMenuItem.Enabled =
                    this.showDataGridViewToolStripMenuItem.Checked =
                    this.dataGridView1.Visible =
                    this.spDataGrid.Visible = false;

            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pauseToolStripMenuItem.Checked = false;
            if (this.session.Status == ME7LoggerSession.Statuses.Open)
            {
                this.session.Pause();
                this.pauseToolStripMenuItem.Checked = true;
                HighlightPoints();
            }
            else if (this.session.Status == ME7LoggerSession.Statuses.Paused)
            {
                this.session.Resume();
                HighlightPoints(false);
            }
        }

        private void btnOpenCloseSession_Click(object sender, EventArgs e)
        {
            if (this.session.Status == ME7LoggerSession.Statuses.Open)
            {
                this.CloseSession();
            }
            else
            {
                this.OpenSession();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.session.Status == ME7LoggerSession.Statuses.Open)
                {
                    this.CloseSession();
                }
                else
                {
                    this.OpenSession();
                }
            }
        }

        private void flpVariables_Resize(object sender, EventArgs e)
        {
            foreach (Control c in flpVariables.Controls)
            {
                c.Controls[1].Width = (int)((double)flpVariables.Width * .65);
                c.Controls[0].Width = (int)((double)flpVariables.Width * .35) - (this.flpVariables.VerticalScroll.Visible ? 30 : 12);
            }
        }

        #region Grid scrolling and zooming

        private double SelectionStart = double.NaN;
        private void chart1_Click(object sender, System.EventArgs e)
        {
            chart1.Focus();

            this.SelectionStart = chart1.ChartAreas["Default"].CursorX.Position;
            this.DisplayLineAtCursor();
        }

        private void ProcessSelect(System.Windows.Forms.KeyEventArgs e)
        {
            chart1.ChartAreas["Default"].CursorX.Interval = 10;
            // Process keyboard keys
            if (e.KeyCode == Keys.Right)
            {
                // Make sure the selection start value is assigned
                if (this.SelectionStart == double.NaN)
                    this.SelectionStart = chart1.ChartAreas["Default"].CursorX.Position;

                // Set the new cursor position 
                chart1.ChartAreas["Default"].CursorX.Position += chart1.ChartAreas["Default"].CursorX.Interval;
            }
            else if (e.KeyCode == Keys.Left)
            {
                // Make sure the selection start value is assigned
                if (this.SelectionStart == double.NaN)
                    this.SelectionStart = chart1.ChartAreas["Default"].CursorX.Position;

                // Set the new cursor position 
                chart1.ChartAreas["Default"].CursorX.Position -= chart1.ChartAreas["Default"].CursorX.Interval;
            }

            // If the cursor is outside the view, set the view
            // so that the cursor can be seen
            SetView();

            chart1.ChartAreas["Default"].CursorX.SelectionStart = this.SelectionStart;
            chart1.ChartAreas["Default"].CursorX.SelectionEnd = chart1.ChartAreas["Default"].CursorX.Position;
        }

        private void SetView()
        {
            // Keep the cursor from leaving the max and min axis points
            if (chart1.ChartAreas["Default"].CursorX.Position < 0)
                chart1.ChartAreas["Default"].CursorX.Position = 0;
            else if (chart1.ChartAreas["Default"].CursorX.Position > this.DisplayOptions.GraphHRes)
                chart1.ChartAreas["Default"].CursorX.Position = this.DisplayOptions.GraphHRes;

            // Move the view to keep the cursor visible
            if (chart1.ChartAreas["Default"].CursorX.Position < chart1.ChartAreas["Default"].AxisX.ScaleView.Position)
            {
                chart1.ChartAreas["Default"].AxisX.ScaleView.Position = chart1.ChartAreas["Default"].CursorX.Position;
            }
            else if (chart1.ChartAreas["Default"].CursorX.Position >
                (chart1.ChartAreas["Default"].AxisX.ScaleView.Position + chart1.ChartAreas["Default"].AxisX.ScaleView.Size))
            {
                chart1.ChartAreas["Default"].AxisX.ScaleView.Position =
                    (chart1.ChartAreas["Default"].CursorX.Position - chart1.ChartAreas["Default"].AxisX.ScaleView.Size);
            }
        }

        private void ProcessScroll(System.Windows.Forms.KeyEventArgs e)
        {
            chart1.ChartAreas["Default"].CursorX.Interval = 1;
            // Process keyboard keys
            if (e.KeyCode == Keys.Right)
            {
                // set the new cursor position 
                chart1.ChartAreas["Default"].CursorX.Position += chart1.ChartAreas["Default"].CursorX.Interval;
                this.DisplayLineAtCursor();
            }
            else if (e.KeyCode == Keys.Left)
            {
                // Set the new cursor position 
                chart1.ChartAreas["Default"].CursorX.Position -= chart1.ChartAreas["Default"].CursorX.Interval;
                this.DisplayLineAtCursor();
            }

            // If the cursor is outside the view, set the view
            // so that the cursor can be seen
            SetView();

            // Set the selection start variable in case shift arrows are selected
            this.SelectionStart = chart1.ChartAreas["Default"].CursorX.Position;

            // Reset the old selection start and end
            chart1.ChartAreas["Default"].CursorX.SelectionStart = double.NaN;
            chart1.ChartAreas["Default"].CursorX.SelectionEnd = double.NaN;
        }

        private void chart1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.Left))
            {
                // If the key event is shifted, process as a selection
                if (e.Shift)
                {
                    ProcessSelect(e);
                }
                else // Process as a scroll
                {
                    ProcessScroll(e);
                }
                return;
            }
            // On enter, zoom the selection
            else if (e.KeyCode == Keys.Up)
            {
                double start, end;

                if (chart1.ChartAreas["Default"].CursorX.SelectionStart > chart1.ChartAreas["Default"].CursorX.SelectionEnd)
                {
                    start = chart1.ChartAreas["Default"].CursorX.SelectionEnd;
                    end = chart1.ChartAreas["Default"].CursorX.SelectionStart;
                }
                else
                {
                    end = chart1.ChartAreas["Default"].CursorX.SelectionEnd;
                    start = chart1.ChartAreas["Default"].CursorX.SelectionStart;
                }

                // Return if no selection actually made
                if (start == end)
                    return;

                // Zoom the selection
                chart1.ChartAreas["Default"].AxisX.ScaleView.Zoom(start, (end - start), DateTimeIntervalType.Number, true);

                // Reset selection values
                this.SelectionStart = chart1.ChartAreas["Default"].CursorX.Position;
                chart1.ChartAreas["Default"].CursorX.SelectionStart = double.NaN;
                chart1.ChartAreas["Default"].CursorX.SelectionEnd = double.NaN;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Back || e.KeyCode == Keys.Escape)
            {
                // Reset zoom back to previous view state
                chart1.ChartAreas["Default"].AxisX.ScaleView.ZoomReset(1);

                // Reset selection values
                this.SelectionStart = chart1.ChartAreas["Default"].CursorX.Position;
                chart1.ChartAreas["Default"].CursorX.SelectionStart = double.NaN;
                chart1.ChartAreas["Default"].CursorX.SelectionEnd = double.NaN;
            }

            if (this.freezeToolStripMenuItem.Checked || 
                session.Status == ME7LoggerSession.Statuses.Paused ||
                session.Status == ME7LoggerSession.Statuses.Closed)
            {
                HighlightPoints();
            }
        }

        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (this.freezeToolStripMenuItem.Checked || 
                session.Status == ME7LoggerSession.Statuses.Paused ||
                session.Status == ME7LoggerSession.Statuses.Closed)
            {
                HighlightPoints();
            }
        }
        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void increasePlaybackSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.session.Status == ME7LoggerSession.Statuses.Open)
            {
                this.session.SetSpeed(5);
            }
        }

        private void decreasePlaybackSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.session.Status == ME7LoggerSession.Statuses.Open)
            {
                this.session.SetSpeed(-5);
            }
        }

        private void resetPlaybackSpeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.session.Status == ME7LoggerSession.Statuses.Open)
            {
                this.session.ResetSpeed();
            }
        }

        private void chart1_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            this.DisplayLineAtCursor();
        }

        private void DisplayLineAtCursor()
        {
            if (chart1.Series.Count > 0)
            {
                int pos = (int)chart1.ChartAreas["Default"].CursorX.Position;
                if (pos > 0 && pos < chart1.Series[0].Points.Count)
                {
                    DataPoint p = chart1.Series[0].Points[pos - 1];
                    Variable var = p.Tag as Variable;
                    if (var != null)
                    {
                        DisplayLine(var.LogLine);

                        if (dataGridView1.Visible)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                            {
                                if (dataGridView1.Rows[i].Tag == var.LogLine)
                                {
                                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                                    dataGridView1.ClearSelection();
                                    dataGridView1.FirstDisplayedScrollingRowIndex = i;
                                    dataGridView1.Rows[i].Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void sessionOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sessionOutputToolStripMenuItem.Checked = !this.sessionOutputToolStripMenuItem.Checked;
            this.spSessionData.Visible = this.pnlSessionData.Visible = sessionOutputToolStripMenuItem.Checked;
        }

        private void showDataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showDataGridViewToolStripMenuItem.Checked = !showDataGridViewToolStripMenuItem.Checked;
            dataGridView1.Visible =
                spDataGrid.Visible = showDataGridViewToolStripMenuItem.Checked;
        }

        private void dataGridView1_VisibleChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.Visible)
            {
                this.dataGridView1.Rows.Clear();
                foreach (LogLine line in buffer)
                {
                    this.AddLineToGrid(line);
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Visible && dataGridView1.SelectedRows.Count > 0)
            {
                LogLine logLine = dataGridView1.SelectedRows[0].Tag as LogLine;
                if (logLine != null)
                {
                    DataPoint dp = chart1.Series[0].Points.FirstOrDefault(p => p.Tag != null && ((Variable)p.Tag).LogLine == logLine);
                    if (dp != null)
                    {
                        chart1.ChartAreas["Default"].CursorX.Position = chart1.Series[0].Points.IndexOf(dp);
                    }
                }
            }
        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            session.Log.ForwardLarge();
        }

        private void reverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            session.Log.ReverseLarge();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            this.session.Log.SetPostion(scrollbar.Value);
        }

        private void scrollbar_KeyUp(object sender, KeyEventArgs e)
        {
            chart1_KeyUp(sender, e);
        }
    }

    public class DataGridViewCSVCopy : DataGridView
    {
        public override DataObject GetClipboardContent()
        {
            this.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject obj = base.GetClipboardContent();
            if (obj != null)
            {
                string txt = obj.GetText();
                obj.SetText(obj.GetText().Replace("\t", ","));
            }
            return obj;
        }
    }
}