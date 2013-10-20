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
        List<string> chartVariables = new List<string>()
        {
            //"wdkba", //ThrottlePlateAngle
            //"nmot", //EngineSpeed
            //"mshfm_w", //MassAirFlow
            "plsol", //BoostPressDesired
            "pvdkds_w", //BoostPressureActual
            //"rl" //EngineLoad
        };
        public Form1()
        {
            InitializeComponent();



            session = new ME7LoggerSession(@"C:\ME7Logger\out5.out", @"C:\ME7Logger\logs\allroadtestSTAGE2v13.csv");
            //session = new ME7LoggerSession("-p COM1 -R", @"C:\me7logger\logs\allroad-config.cfg", true);
            session.StatusChanged += new ME7LoggerSession.LoggerSessionStatusChanged(this.SessionStatusChanged);
            session.Log.LineRead += new ME7LoggerLog.LogLineRead(this.LogLineRead);
        }

        DateTime start;
        private void button1_Click(object sender, EventArgs e)
        {
            session.Open();
        }

        void SessionStatusChanged(ME7LoggerSession.Statuses status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate() { SessionStatusChanged(status); }));
                return;
            }

            if (status == ME7LoggerSession.Statuses.Open)
            {
                //Initailization logic here  
                StringBuilder namesBuilder = new StringBuilder();
                foreach (SessionVariable var in session.Variables.Values)
                {
                    namesBuilder.AppendFormat(var.ToString()).AppendLine();
                }
                txtNames.Text = namesBuilder.ToString();

                this.BuildChart();
            }
            else if (status == ME7LoggerSession.Statuses.Closed)
            {
                if (session.ExitCode < 1)
                {
                    MessageBox.Show("Done");
                }
                else
                {
                    MessageBox.Show(string.Format("Error! Exit Code:{0}{1}{2}", session.ExitCode, Environment.NewLine, session.ErrorText));
                }
            }
        }

        void BuildChart()
        {
            chart1.ChartAreas[0].AxisY.Minimum = 500;
            chart1.ChartAreas[0].AxisY.Maximum = 2500;
         
            chart1.Series.Clear();

            SessionVariable var;
            Series s;
            foreach (string chartVariable in chartVariables)
            {
                var = session.Variables.GetByName(chartVariable);
                s = new Series(var.ToString());
                
                s.ChartType = SeriesChartType.FastLine;
                chart1.Series.Add(s);
                for (int i = 0; i < 100; ++i)
                    s.Points.Add(0, 0);
            }

            chart2.Series.Clear();
            var = session.Variables.GetByName("nmot");
            if (var != null)
            {
                s = new Series(var.ToString());
                chart2.Series.Add(s);
                s.ChartType = SeriesChartType.Doughnut;
                s["PieStartAngle"] = "135";
                chart2.ChartAreas[0].AxisY.Minimum = 0;
                chart2.ChartAreas[0].AxisY.Maximum = 7000;
            }
        }

        void PlotLineOnChart(LogLine line)
        {
            int i=0;
            Variable v;
            Series s;
            foreach (string chartVariable in chartVariables)
            {
                v = line.GetVariableByName(chartVariable);
                s = chart1.Series[i++];
                double parse;
                if (double.TryParse(v.Value, out parse))
                    s.Points.Add(parse, (double)line.TimeStamp);

                s.Points.RemoveAt(0);
            }

            v = line.GetVariableByName("nmot");
            if (v != null)
            {
                s = chart2.Series[0];
                double parse = double.Parse(v.Value);
                s.Points.Clear();
                s.Points.AddY(parse);
                s.Points.AddY(parse - 7000);
            }

        }

        void LogLineRead(LogLine line)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    delegate() { LogLineRead(line); }));
                return;
            }

            if (line.LineNumber == 1)
            {
                start = DateTime.Now;
            }

            txtRunningTime.Text = DateTime.Now.Subtract(start).TotalSeconds.ToString();
            txtTimestamp.Text = line.TimeStamp.ToString();

            StringBuilder values = new StringBuilder();
            foreach (Variable var in line.Variables)
            {
                values.AppendFormat("{0} {1}", var.Value, var.SessionVariable.Unit).AppendLine();
            }
            txtValues.Text = values.ToString();

            this.PlotLineOnChart(line);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            session.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (session.Status == ME7LoggerSession.Statuses.Open)
            {
                MessageBox.Show("please stop the active logging session");
                e.Cancel = true;
            }
        }
    }
}
