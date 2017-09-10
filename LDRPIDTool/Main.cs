using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using VisualME7Logger.Log;
using VisualME7Logger.Session;
using System.Linq;
using System.Reflection;

namespace LDRPIDTool
{
    public partial class Main : Form
    {
        public static Settings settings = new Settings();

        public Main()
        {
            InitializeComponent();

            BuildGrid();
        }

        private void BuildGrid()
        {
            this.txtFilterMbar.Text = settings.RangeFilter.mbar.ToString("0.00");
            this.txtFilterRPM.Text = settings.RangeFilter.rpmRangeLengthMin.ToString("0.00");
            this.txtFilterSeconds.Text = settings.RangeFilter.seconds.ToString("0.00");
            this.txtAmbient.Text = settings.ambient.ToString("0.00");

            DataGridViewColumn column = new DataGridViewColumn(new DataGridViewTextBoxCell());
            column.Name = "rowheadercol";
            column.Width = 75;
            grdPressureByDCandRPM.Columns.Add(column);

            grdPressureByDCandRPM.Rows.Add();
           
            for (int i = 0; i < settings.KFLDRLRpms.Length; ++i)
            {
                grdPressureByDCandRPM.Rows.Add();
                grdPressureByDCandRPM.Rows[i + 1].Cells[0].Value = settings.KFLDRLRpms[i];
            }

            for (int i = 0; i < settings.KFLDRLDutyCycles.Length; ++i)
            {
                column = new DataGridViewColumn(new DataGridViewTextBoxCell());
                column.Name =
                    column.HeaderText = settings.KFLDRLDutyCycles[i].ToString();
                column.Width = 75;
                grdKFLDRLDutyCycleAxis.Columns.Add(column);
                grdKFLDRLDutyCycleAxis.Rows[0].Cells[i].Value = settings.KFLDRLDutyCycles[i];
            }

            for (int i = 0; i < settings.KFLDIMXPressures.Length; ++i)
            {
                column = new DataGridViewColumn(new DataGridViewTextBoxCell());
                column.Name =
                    column.HeaderText = settings.KFLDIMXPressures[i].ToString();
                column.Width = 75;
                grdKFLDIMX.Columns.Add(column);
            }
            grdKFLDIMX.Rows.Add();

            for (int i = 0; i < settings.KFLDIMXPressures.Length; ++i)
            {
                grdKFLDIMX.Rows[0].Cells[i].Value = settings.KFLDIMXPressures[i];
            }

            grdKFLDIMX.Rows.Add();
            for (int i = 0; i < settings.KFLDIMXDutyCycles.Length; ++i)
            {
                grdKFLDIMX.Rows[1].Cells[i].Value = settings.KFLDIMXDutyCycles[i];
            }
        }

        List<ME7LoggerLog> logs;
        Dictionary<ME7LoggerLog, DataPointCollection> dataPointsByLog;
        int closedCount;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtDir.Text))
            {
                MessageBox.Show("Please choose a log directory");
                return;
            }

            settings = LoadSettings();

            closedCount = 0;
            logs = new List<ME7LoggerLog>();
            dataPointsByLog = new Dictionary<ME7LoggerLog, DataPointCollection>();

            DirectoryInfo dir = new DirectoryInfo(txtDir.Text);

            int errors = 0;
            foreach (FileInfo file in dir.GetFiles("*.csv"))
            {
                try
                {
                    ME7LoggerSession session = new ME7LoggerSession("", file.FullName, noWait: true);
                    session.LogLineRead += LogLineRead;
                    session.StatusChanged += SessionStatusChanged;

                    ME7LoggerLog log = session.Log;
                    logs.Add(log);
                    dataPointsByLog[log] = new DataPointCollection(settings);

                    session.Open();
                }
                catch (Exception ex)
                {
                    errors++;
                    closedCount++;
                }
            }

            //waiting for all logs to finish reading (hope visualme7logger.output is threadsafe :) )

            while (errors + closedCount < logs.Count)
            {
                System.Threading.Thread.Sleep(10);
            }

            BuildValues();

            if (errors > 0)
            {
                MessageBox.Show("Errors while processing log files");
            }
        }

        public void BuildValues()
        {
            int columnCount = grdPressureByDCandRPM.ColumnCount;
            for (int i = 1; i < columnCount; ++i)
            {
                grdPressureByDCandRPM.Columns.RemoveAt(1);
            }
            Dictionary<int, int> dutyCycles = new Dictionary<int, int>();
            var logsWithFilteredDataPoints = dataPointsByLog.Values.Where(dpc => dpc.FilteredPoints.Count > 0);
            foreach (var dataPoints in logsWithFilteredDataPoints.OrderBy(dpc => dpc.DutyCycle))
            {                
                int column = 0;
                if (!dutyCycles.ContainsKey(dataPoints.DutyCycle))
                {
                    column = grdPressureByDCandRPM.Rows[0].Cells.Count;
                    dutyCycles[dataPoints.DutyCycle] = column;

                    DataGridViewColumn dgvcolumn = new DataGridViewColumn(new DataGridViewTextBoxCell());
                    dgvcolumn = new DataGridViewColumn(new DataGridViewTextBoxCell());
                    dgvcolumn.Name =
                        dgvcolumn.HeaderText = dataPoints.DutyCycle.ToString();
                    dgvcolumn.Width = 75;
                    grdPressureByDCandRPM.Columns.Add(dgvcolumn);

                    grdPressureByDCandRPM.Rows[0].Cells[column].Value = dataPoints.DutyCycle;
                }
                else
                {
                    continue;
                }

                List<DataPoint> filteredPoints = new List<DataPoint>();
                foreach(var dpc in logsWithFilteredDataPoints.Where(fdp => fdp.DutyCycle == dataPoints.DutyCycle))
                {
                    filteredPoints.AddRange(dpc.FilteredPoints);
                }

                for (int r = 0; r < settings.KFLDRLRpms.Length; ++r)
                {
                    int rpm = settings.KFLDRLRpms[r];
                    List<DataPoint> highPoints = new List<DataPoint>();
                    List<DataPoint> lowPoints = new List<DataPoint>();
                    for (int i = 0; i < filteredPoints.Count; ++i)
                    {
                        DataPoint current = filteredPoints[i];

                        if (current.rpm <= rpm &&
                            (r == 0 || current.rpm > settings.KFLDRLRpms[r - 1]))
                        {
                            lowPoints.Add(current);
                        }
                        else if (current.rpm >= rpm &&
                            (settings.KFLDRLRpms.Length <= r + 1 || current.rpm < settings.KFLDRLRpms[r + 1]))
                        {
                            highPoints.Add(current);
                        }
                    }

                    decimal value = 0;
                    if (highPoints.Count > 0 || lowPoints.Count > 0)
                    {
                        decimal highPressure = highPoints.Count > 0 ? highPoints.Average(p => p.absolutePressure) : lowPoints.Average(p => p.absolutePressure);
                        decimal lowPressure = lowPoints.Count > 0 ? lowPoints.Average(p => p.absolutePressure) : highPoints.Average(p => p.absolutePressure);

                        value = ((highPressure + lowPressure) / 2);
                    }

                    var cell = grdPressureByDCandRPM.Rows[r + 1].Cells[column];
                    cell.Value = value.ToString("0.000");
                    cell.Style.BackColor = System.Drawing.Color.White;
                }

            }

           
            if (settings.InterpolateBlankCells)
            {
                for(int d = 0; d < grdPressureByDCandRPM.ColumnCount - 1; ++d)
                { 
                    int found = 0;
                    for (int r = settings.KFLDRLRpms.Length -1; r >= 0; --r)
                    {
                        var cell = grdPressureByDCandRPM.Rows[r + 1].Cells[d + 1];
                        if (cell.Value == null)
                            continue;

                        decimal value = decimal.Parse(cell.Value.ToString());
                        if (found > 1 && value == 0)
                        {
                            //get new value by interpolating last two pointsds
                            decimal newValue = Program.Interpolate(
                                settings.KFLDRLRpms[r],
                                settings.KFLDRLRpms[r + 1],
                                decimal.Parse(grdPressureByDCandRPM.Rows[r + 2].Cells[d + 1].Value.ToString()),
                                settings.KFLDRLRpms[r + 2],
                                decimal.Parse(grdPressureByDCandRPM.Rows[r + 3].Cells[d + 1].Value.ToString()));
                            cell.Value = (newValue < 0 ? 0 : newValue).ToString("0.000");
                            cell.Style.BackColor = System.Drawing.Color.Red;

                        }
                        else if (value != 0)
                        {
                            found++;
                        }
                        else
                        {
                            found = 0;
                        }
                    }

                    found = 0;
                    for (int r = 0; r <= settings.KFLDRLRpms.Length - 1; ++r)
                    {
                        var cell = grdPressureByDCandRPM.Rows[r + 1].Cells[d + 1];
                        if (cell.Value == null)
                            continue;

                        decimal value = decimal.Parse(cell.Value.ToString());
                        if (found > 1 && value == 0)
                        {
                            //get new value by interpolating last two pointsds
                            decimal newValue = Program.Interpolate(
                                settings.KFLDRLRpms[r],
                                settings.KFLDRLRpms[r - 1],
                                decimal.Parse(grdPressureByDCandRPM.Rows[r].Cells[d + 1].Value.ToString()),
                                settings.KFLDRLRpms[r - 2],
                                decimal.Parse(grdPressureByDCandRPM.Rows[r - 1].Cells[d + 1].Value.ToString()));
                            cell.Value = (newValue < 0 ? 0 : newValue).ToString("0.000");
                            cell.Style.BackColor = System.Drawing.Color.Red;
                        }
                        else if (value != 0)
                        {
                            found++;
                        }
                        else
                        {
                            found = 0;
                        }
                    }
                }
            }
        }

        public void LogLineRead(LogLine line)
        {
            lock (dataPointsByLog)
            {
                //take a variable read from a log, make it into something more usable for this application
                Variable throttleOrAccelAngle = line.GetVariableByName("wdkba", "wped", "wped_w");
                if (throttleOrAccelAngle == null ||
                     throttleOrAccelAngle.Value > 90)
                {
                    Variable dc = line.GetVariableByName("ldtvm");
                    if (dc != null)
                    {
                        DataPoint p = new DataPoint();
                        p.timestamp = line.TimeStamp;
                        p.dutyCycle = dc.Value;

                        Variable v = line.GetVariableByName("nmot", "nmot_w");
                        p.rpm = v.Value;

                        v = line.GetVariableByName("pvdks_w", "pvdkds_w");
                        p.actualPresure = v.Value;

                        v = line.GetVariableByName("pu", "pu_w", "pus_w");
                        p.baroPressure = v == null ? settings.ambient : v.Value;

                        lock (dataPointsByLog)
                        {
                            dataPointsByLog[line.Log].Add(p);
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

        public Settings LoadSettings()
        {
            settings = new Settings();

            settings.RangeFilter = new RangeFilter();
            try
            {
                settings.RangeFilter.mbar = decimal.Parse(txtFilterMbar.Text);
                settings.RangeFilter.seconds = decimal.Parse(txtFilterSeconds.Text);
                settings.RangeFilter.rpmRangeLengthMin = decimal.Parse(txtFilterSeconds.Text);
                settings.ambient = decimal.Parse(txtAmbient.Text);

                settings.KFLDRLDutyCycles = new int[grdKFLDRLDutyCycleAxis.Columns.Count];
                for (int i = 0; i < grdKFLDRLDutyCycleAxis.Columns.Count; ++i)
                {
                    settings.KFLDRLDutyCycles[i] = int.Parse(grdKFLDRLDutyCycleAxis.Rows[0].Cells[i].Value.ToString());
                }

                settings.KFLDRLRpms = new int[grdPressureByDCandRPM.Rows.Count - 1];
                for (int i = 0; i < grdPressureByDCandRPM.Rows.Count - 1; ++i)
                {
                    settings.KFLDRLRpms[i] = int.Parse(grdPressureByDCandRPM.Rows[i + 1].Cells[0].Value.ToString());
                }

                settings.LoggedDutyCycles = new int[grdPressureByDCandRPM.ColumnCount - 1];
                for (int i = 1; i < grdPressureByDCandRPM.ColumnCount; ++i)
                {
                    settings.LoggedDutyCycles[i-1] = int.Parse(grdPressureByDCandRPM.Rows[0].Cells[i].Value.ToString());
                } 

                settings.KFLDIMXPressures = new int[grdKFLDIMX.Columns.Count];
                for (int i = 0; i < grdKFLDIMX.Columns.Count; ++i)
                {
                    settings.KFLDIMXPressures[i] = int.Parse(grdKFLDIMX.Rows[0].Cells[i].Value.ToString());
                }

                settings.KFLDIMXDutyCycles = new int[grdKFLDIMX.Columns.Count];
                for (int i = 0; i < grdKFLDIMX.Columns.Count; ++i)
                {
                    settings.KFLDIMXDutyCycles[i] = int.Parse(grdKFLDIMX.Rows[1].Cells[i].Value.ToString());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while loading settings\r\n" + e.ToString());
            }

            return settings;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            settings = LoadSettings();

            decimal[][] data = new decimal[settings.KFLDRLRpms.Length][];
            for (int i = 0; i < settings.KFLDRLRpms.Length; ++i)
            {
                data[i] = new decimal[settings.LoggedDutyCycles.Length];
                for (int j = 0; j < settings.LoggedDutyCycles.Length; ++j)
                {
                    decimal value = 0;
                    object obj = grdPressureByDCandRPM.Rows[i + 1].Cells[j + 1].Value;
                    if (obj != null)
                    {
                        decimal.TryParse(grdPressureByDCandRPM.Rows[i + 1].Cells[j + 1].Value.ToString(), out value);
                    }
                    data[i][j] = value;
                }
            }

            DataForm dataForm = new DataForm(settings, data);
            dataForm.Show();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string title = $"{AssemblyInfo.Title} - {AssemblyInfo.Version} - {AssemblyInfo.Copyright} {AssemblyInfo.Company}";
            string helpText =
                "LDRPID Tool creates a \"feed forward\" KFLDRL by using KFLDIMX to convert the KFLDRL duty cycle axis into pressure\r\n\r\n" +
                "For best results, log the follow parameters at a fixed duty cycle per each duty cycle in the KFLDRL axis:\r\n\r\n" +
                "Throttle Plate Angle = wdkba\r\n" +
                "OR\r\n" +
                "Accel Pedal Position = wped or wped_w\r\n" +
                "Duty Cycle = ldtvm\r\n" +
                "Engine Speed = nmot or nmot_w\r\n" +
                "Charge Pressure = pvdks_w or pvdkds_w\r\n" +
                "Barometric Pressure = pu or pu_w or pus_w or ambient override value\r\n\r\n" +
                "Note: Ensure LDDIMXN is accounted for by either zeroing the map or modifying KFLDIMX";

            MessageBox.Show(this, helpText, title);
        }
    }
}