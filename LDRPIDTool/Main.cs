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
        public static RangeFilter RangeFilter = new RangeFilter();

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
        Dictionary<ME7LoggerLog, DataPointCollection> dataPointsByLog;
        Dictionary<decimal, HashSet<ME7LoggerLog>> logsByDutyCycle;
        bool wait;
        int closedCount;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            wait = true;
            closedCount = 0;
            logs = new List<ME7LoggerLog>();
            dataPointsByLog = new Dictionary<ME7LoggerLog, DataPointCollection>();
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
                    dataPointsByLog[log] = new DataPointCollection();
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
                DataPointCollection dataPoints = dataPointsByLog[log];
                if (dataPoints.Count == 0)
                    continue;

                for (int d = 0; d < dutyCycles.Length; ++d)
                {
                    if (dutyCycles[d] == dataPoints.DutyCycle)
                    {
                        for (int r = 0; r < rpms.Length; ++r)
                        {
                            int rpm = rpms[r];
                            List<DataPoint> highPoints = new List<DataPoint>();
                            List<DataPoint> lowPoints = new List<DataPoint>();
                            List<DataPoint> filteredPoints = dataPoints.FilteredPoints;
                            for (int i = 0; i < filteredPoints.Count; ++i)
                            {
                                if (filteredPoints[i].rpm <= rpm && (filteredPoints.Count > i + 1 && filteredPoints[i + 1].rpm >= rpm))
                                {
                                    lowPoints.Add(filteredPoints[i]);
                                }
                                else if (filteredPoints[i].rpm >= rpm && (i != 0 && filteredPoints[i - 1].rpm <= rpm))
                                {
                                    highPoints.Add(filteredPoints[i]);
                                }
                            }

                            string value = "";
                            if (highPoints.Count > 0 || lowPoints.Count > 0)
                            {
                                decimal highPressure = highPoints.Count > 0 ? highPoints.Average(p => p.absolutePressure) : 0;
                                decimal lowPressure = lowPoints.Count > 0 ? lowPoints.Average(p => p.absolutePressure) : 0;

                                value = ((highPressure + lowPressure) / 2).ToString("0.000");
                            }

                            grdTable.Rows[r + 1].Cells[d + 1].Value = value;
                            
                        }
                    }
                }
            }

            MessageBox.Show(string.Format("Done with {0} errors", errors));
        }

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

        private void Main_Load(object sender, EventArgs e)
        {

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


    public class DataPointCollection
    {
        List<DataPoint> dataPoints = new List<DataPoint>();      

        int? dutyCycle;
        public int DutyCycle
        {
            get
            {
                if (dutyCycle == null && dataPoints.Count > 0)
                {
                    //this is dumb, the dutyCycle should be what the majority of the dp's duty is
                    // when you stomp the pedal, dutycycle may not immediately be your fixed duty cycle.
                    dutyCycle = (int)Math.Round(dataPoints[Count / 2].dutyCycle);
                }
                return dutyCycle.Value;
            }
        }

        public int Count { get { return dataPoints.Count; } }

        public void Add(DataPoint dataPoint)
        {
            this.dataPoints.Add(dataPoint);
        }

        private List<DataPoint> filteredPoints;
        public List<DataPoint> FilteredPoints
        {
            get
            {
                if (filteredPoints == null)
                {
                    filteredPoints = GetFilteredPoints();
                }
                return filteredPoints;
            }
        }

        List<DataPoint> GetFilteredPoints()
        {
            List<DataPoint> retval = new List<DataPoint>();
            foreach (DataPointCollection range in Ranges)
            {
                //detect spool up points in this range and filter those out.
                List<DataPoint> window = new List<DataPoint>();
                List<DataPoint> filteredRange = new List<DataPoint>();
                foreach (DataPoint dp in range.dataPoints)
                {
                    window.Add(dp);

                    DataPoint firstInWindow = window[0];
                    if (dp.timestamp - firstInWindow.timestamp > Main.RangeFilter.seconds)
                    {
                        window.Remove(firstInWindow);
                        if (Math.Abs(dp.absolutePressure - firstInWindow.absolutePressure) < Main.RangeFilter.mbar)
                        {
                            filteredRange.Add(firstInWindow);
                        }
                    }
                }
                filteredRange.AddRange(window);
                if (filteredRange[filteredRange.Count - 1].rpm - filteredRange[0].rpm >= Main.RangeFilter.rpmRangeLengthMin)
                {
                    retval.AddRange(filteredRange);
                }

            }
            return retval;
        }

        List<DataPointCollection> ranges = null;
        List<DataPointCollection> Ranges
        {
            get
            {
                if (ranges == null)
                {
                    ranges = this.GetRanges();
                }
                return ranges;
            }
        }

        List<DataPointCollection> GetRanges()
        {
            List<DataPointCollection> ranges = new List<DataPointCollection>();
            DataPoint last = null;
            DataPointCollection range = null;
            for (int i = 0; i < dataPoints.Count; ++i)
            {
                DataPoint current = dataPoints[i];
                if (range == null ||
                    current.timestamp - last.timestamp > .25m) //probably should do this a better way
                {
                    range = new DataPointCollection();
                    ranges.Add(range);
                }
                range.Add(current);
                last = current;
            }
            return ranges;
        }
    }

    public class RangeFilter
    {
        public decimal rpmRangeLengthMin = 2000;
        public decimal seconds = .1m;
        public decimal mbar = 100m;
    }
}
