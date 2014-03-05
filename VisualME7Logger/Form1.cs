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
        Timer refreshTimer = new Timer();

        public Form1(string configFile, VisualME7Logger.Session.LoggerOptions options, DisplayOptions displayOptions)
        {
            InitializeComponent();

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
                session = new ME7LoggerSession(options.LogFile,
                    Program.DebugOutput ?
                        ME7LoggerSession.SessionTypes.SessionOutput :
                        ME7LoggerSession.SessionTypes.LogFile);
            }
            else
            {
                session = new ME7LoggerSession(Program.ME7LoggerDirectory, options, configFile);
            }

            session.StatusChanged = new ME7LoggerSession.LoggerSessionStatusChanged(this.SessionStatusChanged);
            Program.WriteDebug("before session.lineread assignment.  Null? " + (session.LineRead == null).ToString());
            session.LineRead = new ME7LoggerSession.LogLineRead(this.LogLineRead);
            Program.WriteDebug("after session.lineread assignment.  Null? " + (session.LineRead == null).ToString() + " Value " + (session.LineRead != null ? session.LineRead.ToString() : "null"));

            this.OpenSession();

            pauseToolStripMenuItem.Enabled = session.CanPause;
        }

        void refreshTimer_Tick(object sender, EventArgs e)
        {
            Program.WriteDebug("refresh timer tick session.lineread Null? " + (session.LineRead == null).ToString() + " Value " + (session.LineRead != null ? session.LineRead.ToString() : "null"));
            
            while (queue.Count() > 0)
            {
                LogLine line = queue.Dequeue();

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
                this.PlotLineOnChart(line);
            }
        }

        void SessionStatusChanged(ME7LoggerSession.Statuses status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate() { SessionStatusChanged(status); }));
                return;
            }

            this.btnOpenCloseSession.Enabled = false;

            lblStatus.Text = string.Format("Log Status: {0}", status);

            if (status == ME7LoggerSession.Statuses.Initialized)
            {
                queue = new Queue<LogLine>();

                flpVariables.Controls.Clear();
                Font f = null;
                foreach (SessionVariable v in session.Variables.Values)
                {
                    FlowLayoutPanel flp = new FlowLayoutPanel();
                    flp.Name = v.Name;
                    flp.FlowDirection = FlowDirection.LeftToRight;
                    flp.WrapContents = false;
                    flp.AutoSize = true;
                    flp.Margin = new Padding(0, 1, 0, 1);

                    Label name = new Label();
                    if (f == null)
                    {
                        f = new Font(name.Font.FontFamily, name.Font.Size + 1);
                    }
                    name.Name = v.Name;
                    name.Height = 20;
                    name.Text = v.ToString();
                    name.BorderStyle = BorderStyle.Fixed3D;
                    name.TextAlign = ContentAlignment.MiddleLeft;
                    name.Font = f;
                    name.RightToLeft = System.Windows.Forms.RightToLeft.No;

                    Label value = new Label();
                    value.Name = v.Name;
                    value.TextAlign = ContentAlignment.MiddleLeft;
                    value.Height = 20;
                    value.BorderStyle = BorderStyle.Fixed3D;
                    value.Font = f;
                    value.RightToLeft = System.Windows.Forms.RightToLeft.No;

                    flp.Controls.Add(value);
                    flp.Controls.Add(name);
                    flpVariables.Controls.Add(flp);
                }

                this.BuildChart();

                start = DateTime.Now;
                refreshTimer.Start();
            }
            else if (status == ME7LoggerSession.Statuses.Open)
            {
                flpVariables_Resize(null, null);
                this.btnOpenCloseSession.Enabled = true;
                this.btnOpenCloseSession.Text = "Close Session";
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
        }

        void BuildChart()
        {
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = this.DisplayOptions.GraphVRes;

            chart1.Series.Clear();

            foreach (GraphVariable graphVariable in this.DisplayOptions.GraphVariables.Where(v => v.Active))
            {
                SessionVariable var = session.Variables[graphVariable.Variable];
                if (var != null)
                {
                    Series s = new Series(string.Format("{0} {1}-{2} {3}", graphVariable.Name, graphVariable.Min.ToString("0.##"), graphVariable.Max.ToString("0.##"), var.Unit));
                    s.Color = graphVariable.LineColor;
                    s.ChartType = (SeriesChartType)cmbChartType.SelectedItem;
                    s.BorderWidth = graphVariable.LineThickness;
                    s.BorderDashStyle = graphVariable.LineStyle;
                    s.ToolTip = "so tool tips show up";
                    chart1.Series.Add(s);
                    for (int i = 0; i < this.DisplayOptions.GraphHRes; ++i)
                    {
                        s.Points.Add(-1, -1).AxisLabel = "0";
                    }
                }
            }
        }

        void PlotLineOnChart(LogLine line)
        {
            int i = 0;
            foreach (GraphVariable graphVariable in this.DisplayOptions.GraphVariables.Where(gv => gv.Active))
            {
                if (session.Variables.GetByName(graphVariable.Variable) != null)
                {
                    Variable v = line[graphVariable.Variable];
                    Series s = chart1.Series[i++];
                    decimal parse;
                    if (decimal.TryParse(v.Value, out parse))
                    {
                        decimal percent = (parse - graphVariable.Min) / (graphVariable.Max - graphVariable.Min) * this.DisplayOptions.GraphVRes;
                        DataPoint p = s.Points.Add((double)percent);
                        p.AxisLabel = decimal.Round(line.TimeStamp, 1).ToString();
                        p.ToolTip = string.Format("{0}: {1} {2}", graphVariable.Name, v.Value, v.SessionVariable.Unit);
                    }
                    s.Points.RemoveAt(0);
                }
            }
        }

        void LogLineRead(LogLine line)
        {
            queue.Enqueue(line);
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
            if (session.Status != ME7LoggerSession.Statuses.Open)
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
            if (session.Status == ME7LoggerSession.Statuses.Open)
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
                double size = chart1.ChartAreas[0].AxisX.ScaleView.Size;
                double pos = chart1.ChartAreas[0].AxisX.ScaleView.Position;
                bool zoomed = chart1.ChartAreas[0].AxisX.ScaleView.IsZoomed;
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
                        lowest.Label = lowest.ToolTip;
                        lowest.LabelForeColor = Color.White;
                    }

                    if (highest != null)
                    {
                        highest.Label = highest.ToolTip;
                        highest.LabelForeColor = Color.White;
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
            }
            if (!this.freezeToolStripMenuItem.Checked && this.session.Status == ME7LoggerSession.Statuses.Open)
            {
                HighlightPoints(false);
                refreshTimer.Start();
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

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {           
            if(e.KeyData != Keys.Enter)
            {
                chart1_KeyUp(sender, e);
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
            this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;
        }

        private void ProcessSelect(System.Windows.Forms.KeyEventArgs e)
        {
            // Process keyboard keys
            if (e.KeyCode == Keys.Right)
            {
                // Make sure the selection start value is assigned
                if (this.SelectionStart == double.NaN)
                    this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;

                // Set the new cursor position 
                chart1.ChartAreas[0].CursorX.Position += chart1.ChartAreas[0].CursorX.Interval;
            }
            else if (e.KeyCode == Keys.Left)
            {
                // Make sure the selection start value is assigned
                if (this.SelectionStart == double.NaN)
                    this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;

                // Set the new cursor position 
                chart1.ChartAreas[0].CursorX.Position -= chart1.ChartAreas[0].CursorX.Interval;
            }

            // If the cursor is outside the view, set the view
            // so that the cursor can be seen
            SetView();

            chart1.ChartAreas[0].CursorX.SelectionStart = this.SelectionStart;
            chart1.ChartAreas[0].CursorX.SelectionEnd = chart1.ChartAreas[0].CursorX.Position;
        }

        private void SetView()
        {
            // Keep the cursor from leaving the max and min axis points
            if (chart1.ChartAreas[0].CursorX.Position < 0)
                chart1.ChartAreas[0].CursorX.Position = 0;
            else if (chart1.ChartAreas[0].CursorX.Position > this.DisplayOptions.GraphHRes)
                chart1.ChartAreas[0].CursorX.Position = this.DisplayOptions.GraphHRes;

            // Move the view to keep the cursor visible
            if (chart1.ChartAreas[0].CursorX.Position < chart1.ChartAreas[0].AxisX.ScaleView.Position)
            {
                chart1.ChartAreas[0].AxisX.ScaleView.Position = chart1.ChartAreas[0].CursorX.Position;
            }
            else if (chart1.ChartAreas[0].CursorX.Position >
                (chart1.ChartAreas[0].AxisX.ScaleView.Position + chart1.ChartAreas[0].AxisX.ScaleView.Size))
            {
                chart1.ChartAreas[0].AxisX.ScaleView.Position =
                    (chart1.ChartAreas[0].CursorX.Position - chart1.ChartAreas[0].AxisX.ScaleView.Size);
            }
        }

        private void ProcessScroll(System.Windows.Forms.KeyEventArgs e)
        {
            // Process keyboard keys
            if (e.KeyCode == Keys.Right)
            {
                // set the new cursor position 
                chart1.ChartAreas[0].CursorX.Position += chart1.ChartAreas[0].CursorX.Interval;
            }
            else if (e.KeyCode == Keys.Left)
            {
                // Set the new cursor position 
                chart1.ChartAreas[0].CursorX.Position -= chart1.ChartAreas[0].CursorX.Interval;
            }

            // If the cursor is outside the view, set the view
            // so that the cursor can be seen
            SetView();

            // Set the selection start variable in case shift arrows are selected
            this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;

            // Reset the old selection start and end
            chart1.ChartAreas[0].CursorX.SelectionStart = double.NaN;
            chart1.ChartAreas[0].CursorX.SelectionEnd = double.NaN;
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

                if (chart1.ChartAreas[0].CursorX.SelectionStart > chart1.ChartAreas[0].CursorX.SelectionEnd)
                {
                    start = chart1.ChartAreas[0].CursorX.SelectionEnd;
                    end = chart1.ChartAreas[0].CursorX.SelectionStart;
                }
                else
                {
                    end = chart1.ChartAreas[0].CursorX.SelectionEnd;
                    start = chart1.ChartAreas[0].CursorX.SelectionStart;
                }

                // Return if no selection actually made
                if (start == end)
                    return;

                // Zoom the selection
                chart1.ChartAreas[0].AxisX.ScaleView.Zoom(start, (end - start), DateTimeIntervalType.Number, true);

                // Reset selection values
                this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;
                chart1.ChartAreas[0].CursorX.SelectionStart = double.NaN;
                chart1.ChartAreas[0].CursorX.SelectionEnd = double.NaN;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Back || e.KeyCode == Keys.Escape)
            {
                // Reset zoom back to previous view state
                chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset(1);

                // Reset selection values
                this.SelectionStart = chart1.ChartAreas[0].CursorX.Position;
                chart1.ChartAreas[0].CursorX.SelectionStart = double.NaN;
                chart1.ChartAreas[0].CursorX.SelectionEnd = double.NaN;
            }

            if (this.freezeToolStripMenuItem.Checked || session.Status == ME7LoggerSession.Statuses.Paused)
            {
                HighlightPoints();
            }
        }

        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (this.freezeToolStripMenuItem.Checked || session.Status == ME7LoggerSession.Statuses.Paused)
            {
                HighlightPoints();
            }
        }      
        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }     
    }
}