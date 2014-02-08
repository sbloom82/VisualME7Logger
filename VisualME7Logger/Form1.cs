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
           
            session.StatusChanged += new ME7LoggerSession.LoggerSessionStatusChanged(this.SessionStatusChanged);
            session.Log.LineRead += new ME7LoggerLog.LogLineRead(this.LogLineRead);
            
            this.OpenSession();

            pauseToolStripMenuItem.Enabled = session.CanPause;
        }

        void refreshTimer_Tick(object sender, EventArgs e)
        {
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
                    flp.Margin = new Padding(2, 0, 2, 2);

                    Label name = new Label();
                    if (f == null)
                    {
                        f = new Font(name.Font.FontFamily, name.Font.Size + 1);
                    }
                    name.Name = v.Name;
                    name.Height = 20;
                    name.Width = 175;
                    name.Text = v.ToString();
                    name.BorderStyle = BorderStyle.Fixed3D;
                    name.TextAlign = ContentAlignment.MiddleLeft;
                    name.Font = f;
                    name.RightToLeft = System.Windows.Forms.RightToLeft.No;
               
                    Label value = new Label();
                    value.Name = v.Name;
                    value.TextAlign = ContentAlignment.MiddleLeft;
                    value.Height = 20;
                    value.Width = 75;
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
            else if (status == ME7LoggerSession.Statuses.Closed)
            {
                refreshTimer.Stop();
                if (session.ExitCode > 0)
                {
                    MessageBox.Show(string.Format("Error! Exit Code:{0}{1}{2}", session.ExitCode, Environment.NewLine, session.ErrorText));
                }
            }
        }

        void BuildChart()
        {
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = this.DisplayOptions.GraphVRes;

            chart1.Series.Clear();

            SessionVariable var;
            Series s;
            foreach (GraphVariable graphVariable in this.DisplayOptions.GraphVariables.Where(v => v.Active))
            {
                var = session.Variables[graphVariable.Variable];
                if (var != null)
                {
                    s = new Series(string.Format("{0} {1}-{2} {3}", graphVariable.Name, graphVariable.Min.ToString("0.##"), graphVariable.Max.ToString("0.##"), var.Unit));
                    s.Color = graphVariable.LineColor;
                    s.ChartType = (SeriesChartType)cmbChartType.SelectedItem;
                    s.BorderWidth = graphVariable.LineThickness;
                    s.BorderDashStyle = graphVariable.LineStyle;
                    s.ToolTip = "so tool tips show up";
                    chart1.Series.Add(s);
                    for (int i = 0; i < this.DisplayOptions.GraphHRes; ++i)
                        s.Points.Add(-1, -1);
                }
            }
        }

        void PlotLineOnChart(LogLine line)
        {
            int i = 0;
            Variable v;
            Series s;
            foreach (GraphVariable graphVariable in this.DisplayOptions.GraphVariables.Where(gv => gv.Active))
            {
                if (session.Variables.GetByName(graphVariable.Variable) != null)
                {
                    v = line[graphVariable.Variable];
                    s = chart1.Series[i++];
                    decimal parse;
                    if (decimal.TryParse(v.Value, out parse))
                    {
                        decimal percent = (parse - graphVariable.Min) / (graphVariable.Max - graphVariable.Min) * this.DisplayOptions.GraphVRes;
                        DataPoint p = s.Points.Add((double)percent);
                        p.ToolTip = string.Format("{0} {1} {2}", graphVariable.Name, v.Value, v.SessionVariable.Unit);
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
            if (session.Status == ME7LoggerSession.Statuses.Open)
            {
                session.Close();
            }
        }

        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeriesChartType type = (SeriesChartType)cmbChartType.SelectedItem;
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

        private void startToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenSession();
        }

        private void stopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            session.Close();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = string.Format("Communication:\r\nConnect={0}\r\nCommunication={1}\r\nLogSpeed={2}",
                  session.CommunicationInfo.Connect,
                  session.CommunicationInfo.Communicate,
                  session.CommunicationInfo.LogSpeed);

            s += string.Format("\r\n\r\nIdentification:\r\nHW#={0}\r\nSW#={1}\r\nPart#={2}\r\nEngineId={3}",
                session.IdentificationInfo.HWNumber,
                session.IdentificationInfo.SWNumber,
                session.IdentificationInfo.PartNumber,
                session.IdentificationInfo.EngineId);

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
                    if (value > 0 && value < 5000)
                    {
                        success = true;
                        refreshTimer.Interval = value;
                    }
                }

                if (!success)
                {
                    txtRefreshRate.Text = refreshTimer.Interval.ToString();
                }
            }
        }

        private void freezeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoFreeze();           
        }

        void DoFreeze()
        {
            this.freezeToolStripMenuItem.Checked =
                   !this.freezeToolStripMenuItem.Checked;

            if (this.freezeToolStripMenuItem.Checked)
            {
                refreshTimer.Stop();
            }
            if (!this.freezeToolStripMenuItem.Checked && this.session.Status == ME7LoggerSession.Statuses.Open)
            {
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
            }
            else if (this.session.Status == ME7LoggerSession.Statuses.Paused)
            {
                this.session.Resume();
            }
        }
    }
}