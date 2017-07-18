using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using VisualME7Logger.Log;
using VisualME7Logger.Session;


namespace LDRPIDTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            BuildGrid();
        }

        private void BuildGrid()
        {
            int[] dutyCycles = new int[]
            {
                0,
                10,
                20,
                30,
                40,
                50,
                60,
                70,
                80,
                95
            };

            DataGridViewColumn column = new DataGridViewColumn(new DataGridViewTextBoxCell());
            column.Name = "rowheadercol";
            column.Width = 75;
            grdTable.Columns.Add(column);
            for (int i = 0; i < dutyCycles.Length; ++i)
            {
                column = new DataGridViewColumn(new DataGridViewTextBoxCell());
                column.Name =
                    column.HeaderText = dutyCycles[i].ToString();
                column.Width = 75;
                grdTable.Columns.Add(column);
            }

            grdTable.Rows.Add();
            for (int i = 0; i < dutyCycles.Length; ++i)
            {
                grdTable.Rows[0].Cells[i + 1].Value = dutyCycles[i];
            }

            int[] rpms = new int[]
            {
                1000,
                1250,
                1500,
                1750,
                2000,
                2250,
                2500,
                3000,
                3500,
                4000,
                4500,
                5000,
                5500,
                6000,
                6500
            };

            for (int i = 0; i < rpms.Length; ++i)
            {
                grdTable.Rows.Add();
                grdTable.Rows[i + 1].Cells[0].Value = rpms[i];
            }
        }

        List<ME7LoggerLog> logs;
        Dictionary<ME7LoggerLog, List<DataPoint>> dataPointsByLog;
        bool wait;
        int closedCount;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            wait = true;
            closedCount = 0;
            logs = new List<ME7LoggerLog>();
            dataPointsByLog = new Dictionary<ME7LoggerLog, List<DataPoint>>();

            DirectoryInfo dir = new DirectoryInfo(txtDir.Text);

            int errors=0;
            foreach (FileInfo file in dir.GetFiles("*.csv"))
            {
                try
                {
                    VisualME7Logger.Session.ME7LoggerSession session = new ME7LoggerSession("", file.FullName, noWait: true);
                    session.LogLineRead += LogLineRead;
                    session.StatusChanged += SessionStatusChanged;
                    session.Open();

                    VisualME7Logger.Log.ME7LoggerLog log = session.Log;
                    logs.Add(log);
                    dataPointsByLog[log] = new List<DataPoint>();
                }
                catch 
                {
                    errors++;                    
                }
            }


            //waiting for all logs to finish reading (hope visualme7logger.output is threadsafe :) )
            
            while (wait)
            {
                System.Threading.Thread.Sleep(10);
            }

            foreach (var log in dataPointsByLog.Keys)
            {
                List<DataPoint> dps = dataPointsByLog[log];

                foreach (DataPoint dp in dps)
                {
                    //do some things here to make the boost real good.
                    //int dc = (int)Math.Round(dp.dutyCycle, 0);

                    
                }

            }

            MessageBox.Show(string.Format("Done with {0} errors", errors));
        }

        public void LogLineRead(LogLine line)
        {
            //take a variable read from a log, make convert it into something more usable for this application
            Variable accelPedal = line.GetVariableByName("wped");
            if (accelPedal == null || accelPedal.Value >= 80)
            {
                DataPoint p = new DataPoint();
                p.timestamp = line.TimeStamp;

                Variable v = line.GetVariableByName("nmot");
                if (v == null)
                    v = line.GetVariableByName("nmot_w");
                p.rpm = v.Value;

                v = line.GetVariableByName("pvdks_w");
                p.absolutePressure = v.Value;

                v = line.GetVariableByName("ldtvm");
                p.dutyCycle = v.Value;

                v = line.GetVariableByName("pu");
                if (v == null)
                    v = line.GetVariableByName("pu_w");
                if (v != null)
                    p.baroPressure = v.Value;

                dataPointsByLog[line.Log].Add(p);
            }
        }
        
        
        public void SessionStatusChanged(ME7LoggerSession.Statuses status)
        {
            if (status == ME7LoggerSession.Statuses.Closed)
            {
                closedCount++;
                if (closedCount == logs.Count)
                {
                    wait = false;
                }

            }
        }

        private void btnChooseDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dia = new FolderBrowserDialog();
            if (dia.ShowDialog() == DialogResult.OK)
            {
                txtDir.Text = dia.SelectedPath;
            }
        }
    }

    public class DataPoint
    {
        public decimal rpm;
        public decimal absolutePressure;
        public decimal dutyCycle;
        public decimal baroPressure = 1000;
        public decimal timestamp;
    }
}
