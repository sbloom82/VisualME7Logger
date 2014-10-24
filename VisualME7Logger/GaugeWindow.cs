using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualME7Logger
{
    public partial class GaugeWindow : Form
    {
        private Session.ME7LoggerSession session;
        private Session.SessionVariable sessionVariable;
        public GaugeWindow(Session.ME7LoggerSession session, Session.SessionVariable sessionVariable)
        {
            InitializeComponent();

            this.Text = sessionVariable.ToString();
            this.sessionVariable = sessionVariable;
            this.session = session;

            this.chart1.Series.Add(sessionVariable.Name);
            this.chart1.Series[0].Points.Add(0);

            this.chart1.ChartAreas[0].AxisY.Minimum = 0;
            this.chart1.ChartAreas[0].AxisY.Maximum = 100;
        }

        public void Refresh(Log.LogLine logLine)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                                 delegate() { Refresh(logLine); }));
                return;
            }

            Log.Variable var = logLine.GetVariableByName(sessionVariable.Name);
            if (var != null)
            {
                this.chart1.Series[0].Points[0].YValues[0] = (double)var.Value;
                this.chart1.Invalidate();
            }
        }

        private void nudMin_ValueChanged(object sender, EventArgs e)
        {
            this.chart1.ChartAreas[0].AxisY.Minimum = (double)nudMin.Value;
        }

        private void nudMax_ValueChanged(object sender, EventArgs e)
        {
            this.chart1.ChartAreas[0].AxisY.Maximum = (double)nudMax.Value;
        }

        private void GaugeWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void GaugeWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.session.LogLineRead -= this.Refresh;
        }
    }
}
