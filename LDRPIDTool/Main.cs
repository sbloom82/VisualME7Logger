using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using VisualME7Logger.Log;
using VisualME7Logger.Session;
using System.Linq;


namespace LDRPIDTool
{
    public partial class Main : Form
    {
        public int[] dutyCycles = new int[]
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

        public int[] rpms = new int[]
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


        public Main()
        {
            InitializeComponent();

            BuildGrid();
        }

        private void BuildGrid()
        {
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

            for (int i = 0; i < rpms.Length; ++i)
            {
                grdTable.Rows.Add();
                grdTable.Rows[i + 1].Cells[0].Value = rpms[i];
            }
        }

        List<ME7LoggerLog> logs;
        Dictionary<ME7LoggerLog, List<DataPoint>> dataPointsByLog;
        Dictionary<decimal, HashSet<ME7LoggerLog>> logsByDutyCycle;
        bool wait;
        int closedCount;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            wait = true;
            closedCount = 0;
            logs = new List<ME7LoggerLog>();
            dataPointsByLog = new Dictionary<ME7LoggerLog, List<DataPoint>>();
            logsByDutyCycle = new Dictionary<decimal, HashSet<ME7LoggerLog>>();

            DirectoryInfo dir = new DirectoryInfo(txtDir.Text);

            int errors = 0;
            foreach (FileInfo file in dir.GetFiles("*.csv"))
            {
                try
                {
                    ME7LoggerSession session = new ME7LoggerSession("", file.FullName, noWait: true);
                    session.LogLineRead += LogLineRead;
                    session.StatusChanged += SessionStatusChanged;
                    session.Open();

                    ME7LoggerLog log = session.Log;
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
                List<DataPoint> dataPoints = dataPointsByLog[log];
                if (dataPoints.Count == 0)
                    continue;
                int roundedDC = (int)Math.Round(dataPoints[dataPoints.Count / 2].dutyCycle);

                for (int d = 0; d < dutyCycles.Length; ++d)
                {
                    if (dutyCycles[d] == roundedDC)
                    {
                        for (int r = 0; r < rpms.Length; ++r)
                        {
                            int rpm = rpms[r];
                            List<DataPoint> highPoints = new List<DataPoint>();
                            List<DataPoint> lowPoints = new List<DataPoint>();
                            for (int i = 0; i < dataPoints.Count; ++i)
                            {
                                if (dataPoints[i].rpm <= rpm && (dataPoints.Count >= i + 1 || dataPoints[i + 1].rpm >= rpm))
                                {
                                    lowPoints.Add(dataPoints[i]);
                                }
                                else if (dataPoints[i].rpm >= rpm && (i - 1 < 0 || dataPoints[i - 1].rpm <= rpm))
                                {
                                    highPoints.Add(dataPoints[i]);
                                }
                            }

                            decimal highPressure = highPoints.Count > 0 ? highPoints.Average(p => p.absolutePressure) : 0;
                            decimal lowPressure = lowPoints.Count > 0 ? lowPoints.Average(p => p.absolutePressure) : 0;

                            decimal value = highPressure == 0 ? lowPressure : lowPressure == 0 ? highPressure : (highPressure + lowPressure) / 2;

                            grdTable.Rows[r + 1].Cells[d + 1].Value = value.ToString("0.000");
                            
                        }
                    }
                }
            }

            MessageBox.Show(string.Format("Done with {0} errors", errors));
        }
        /*
        public decimal interpolate(decimal x, decimal x0, decimal y0, decimal x1, decimal y1)
        {
            if ((x1 - x0) == 0)
            {
                return (y0 + y1) / 2;
            }
            return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
        }*/

        public void LogLineRead(LogLine line)
        {
            lock (dataPointsByLog)
            {
                //take a variable read from a log, make convert it into something more usable for this application

                //todo, detect ramp up and filter those out, otherwise results will be inaccurate.
                Variable accelPedal = line.GetVariableByName("wped");
                if (accelPedal == null || accelPedal.Value >= 80)
                {
                    Variable dc = line.GetVariableByName("ldtvm");
                    if (dc != null && dutyCycles.Contains((int)Math.Round(dc.Value)))
                    {
                        DataPoint p = new DataPoint();
                        p.timestamp = line.TimeStamp;

                        Variable v = line.GetVariableByName("nmot");
                        if (v == null)
                            v = line.GetVariableByName("nmot_w");
                        p.rpm = v.Value;

                        v = line.GetVariableByName("pvdks_w");
                        p.absolutePressure = v.Value;


                        p.dutyCycle = dc.Value;

                        v = line.GetVariableByName("pu");
                        if (v == null)
                            v = line.GetVariableByName("pu_w");
                        if (v != null)
                            p.baroPressure = v.Value;

                        lock (logsByDutyCycle)
                        {
                            dataPointsByLog[line.Log].Add(p);

                            if (!logsByDutyCycle.ContainsKey(p.dutyCycle))
                            {
                                logsByDutyCycle[p.dutyCycle] = new HashSet<ME7LoggerLog>();
                            }
                            logsByDutyCycle[p.dutyCycle].Add(line.Log);
                        }
                    }
                }
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
            dia.SelectedPath = txtDir.Text;
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

        public override string ToString()
        {
            return string.Format("{0}rpm - {1}mbar", rpm, absolutePressure);            
        }
    }
}
