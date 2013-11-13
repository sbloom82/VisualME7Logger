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
        Dictionary<string, List<double>> chartVariables = new Dictionary<string, List<double>>()
        {
            //"wdkba", //ThrottlePlateAngle
            {"nmot", new List<double>(){-680, .2}}, //EngineSpeed
           // {"mshfm_w",new List<double>(){0, 4}},//MassAirFlow
            {"plsol", new List<double>(){-960, .8}}, //BoostPressDesired
            {"pvdkds", new List<double>(){-960, .8}}, //BoostPressureActual
            //{"rl", new List<double>(){0, 5}} ,//EngineLoad,
            //{"vfzg", new List<double>(){0, 7}}//speed
        };

        public Form1(string configFile, string parameters)
        {
            InitializeComponent();

            cmbChartType.DataSource = Enum.GetValues(typeof(SeriesChartType));
            cmbChartType.SelectedItem = SeriesChartType.FastLine;

            session = new ME7LoggerSession(Program.ME7LoggerDirectory, parameters, configFile);
            //session = new ME7LoggerSession(@"C:\ME7Logger\out7.out", @"C:\ME7Logger\logs\allroad.log");
            //session = new ME7LoggerSession(Program.ME7LoggerDirectory, @"-p COM1 -R -o C:\me7logger\logs\allroad.log", @"C:\me7logger\logs\allroad-config.cfg");
            session.StatusChanged += new ME7LoggerSession.LoggerSessionStatusChanged(this.SessionStatusChanged);
            session.Log.LineRead += new ME7LoggerLog.LogLineRead(this.LogLineRead);
            session.Open();
        }

        DateTime start;
        private void button1_Click(object sender, EventArgs e)
        {
            if (session.Status != ME7LoggerSession.Statuses.Open)
            {
                session.Open();
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

            if (status == ME7LoggerSession.Statuses.Open)
            {
                foreach (string varName in this.chartVariables.Keys.ToList())
                {
                    if (session.Variables.GetByName(varName) == null)
                    {
                        chartVariables.Remove(varName);
                    }
                }

                //Initailization logic here  
                StringBuilder namesBuilder = new StringBuilder();
                foreach (SessionVariable var in session.Variables.Values)
                {
                    namesBuilder.AppendFormat(var.ToString()).AppendLine();
                }
                txtNames.Text = namesBuilder.ToString();

                txtCommunication.Text = string.Format("Connect={0}, Communicate={1}, LogSpeed={2}",
                    session.CommunicationInfo.Connect,
                    session.CommunicationInfo.Communicate,
                    session.CommunicationInfo.LogSpeed);
                txtIdentification.Text = string.Format("HW#={0}, SW#={1}, Part#={2}, EngineId={3}",
                    session.IdentificationInfo.HWNumber,
                    session.IdentificationInfo.SWNumber,
                    session.IdentificationInfo.PartNumber,
                    session.IdentificationInfo.EngineId);

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
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = 1000;
            chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            chart1.ChartAreas[0].AxisY.LabelStyle = new LabelStyle() { Enabled = false };
            chart1.ChartAreas[0].AxisX.LabelStyle = new LabelStyle() { Enabled = false };

            chart1.Series.Clear();

            SessionVariable var;
            Series s;
            foreach (string chartVariable in chartVariables.Keys)
            {
                var = session.Variables[chartVariable];
                if (var != null)
                {
                    s = new Series(var.ToString());

                    s.ChartType = (SeriesChartType)cmbChartType.SelectedItem;
                    chart1.Series.Add(s);
                    for (int i = 0; i < 250; ++i)
                        s.Points.Add(0, 0);
                }
            }

            chart2.Series.Clear();
            var = session.Variables["nmot"];
            if (var != null)
            {
                s = new Series(var.ToString());
                chart2.Series.Add(s);
                s.ChartType = SeriesChartType.Doughnut;
                s["PieStartAngle"] = "145";

                chart2.ChartAreas[0].Area3DStyle.Enable3D = true;
                chart2.ChartAreas[0].AxisY.Minimum = 0;
                chart2.ChartAreas[0].AxisY.Maximum = 10000;

                s.Points.Add(new DataPoint() { });
                s.Points.Add(new DataPoint(0, 7000) { });
                s.Points.Add(new DataPoint(0, 3000) { });
            }
        }

        void PlotLineOnChart(LogLine line)
        {
            int i = 0;
            Variable v;
            Series s;
            foreach (string chartVariable in chartVariables.Keys)
            {
                v = line[chartVariable];
                s = chart1.Series[i++];
                double parse;
                if (double.TryParse(v.Value, out parse))
                {
                    List<double> additiveAndMultiplier = chartVariables[chartVariable];

                    double add = 0;
                    double multi = 1;
                    if (additiveAndMultiplier != null)
                    {
                        add = chartVariables[chartVariable][0];
                        multi = chartVariables[chartVariable][1];
                    }
                    s.Points.Add((parse + add) * multi, (double)line.TimeStamp);
                   // s.Points.Last().ToolTip = v.Value;
                }

                s.Points.RemoveAt(0);
            }

            v = line["nmot"];
            if (v != null)
            {
                s = chart2.Series[0];
                double rpm = double.Parse(v.Value);
                s.Points[0].SetValueY(rpm);
                s.Points[1].SetValueY(7000 - rpm);
                chart2.Invalidate();
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
    }
}
