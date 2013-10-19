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

namespace VisualME7Logger
{
    public partial class Form1 : Form
    {
        public ME7LoggerSession session;
        public Form1()
        {
            InitializeComponent();

            //session = new LoggerSession(@"C:\ME7Logger\out5.out", @"C:\ME7Logger\logs\allroadtestSTAGE2v13.csv");
            session = new ME7LoggerSession("-p COM1 -R", @"C:\me7logger\logs\allroad-config.cfg", true);
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
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            session.Close();
        }
    }
}
