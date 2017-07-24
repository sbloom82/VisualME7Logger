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



            DataGridViewColumn column = new DataGridViewColumn(new DataGridViewTextBoxCell());
            column.Name = "rowheadercol";
            column.Width = 75;
            grdKFLDRL.Columns.Add(column);
            for (int i = 0; i < settings.KFLDRLDutyCycles.Length; ++i)
            {
                column = new DataGridViewColumn(new DataGridViewTextBoxCell());
                column.Name =
                    column.HeaderText = settings.KFLDRLDutyCycles[i].ToString();
                column.Width = 75;
                grdKFLDRL.Columns.Add(column);
            }

            grdKFLDRL.Rows.Add();
            for (int i = 0; i < settings.KFLDRLDutyCycles.Length; ++i)
            {
                grdKFLDRL.Rows[0].Cells[i + 1].Value = settings.KFLDRLDutyCycles[i];
            }

            for (int i = 0; i < settings.KFLDRLRpms.Length; ++i)
            {
                grdKFLDRL.Rows.Add();
                grdKFLDRL.Rows[i + 1].Cells[0].Value = settings.KFLDRLRpms[i];
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
        Dictionary<decimal, HashSet<ME7LoggerLog>> logsByDutyCycle;
        bool wait;
        int closedCount;
        private void btnLoad_Click(object sender, EventArgs e)
        {

            if (!Directory.Exists(txtDir.Text))
            {
                MessageBox.Show("Please choose a log directory");
                return;
            }

            settings = LoadSettings();


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
                    dataPointsByLog[log] = new DataPointCollection(settings);
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

            BuildValues();
        }


        public void BuildValues()
        {
            foreach (var log in dataPointsByLog.Keys)
            {
                DataPointCollection dataPoints = dataPointsByLog[log];

                if (dataPoints.Count == 0)
                    continue;

                for (int d = 0; d < settings.KFLDRLDutyCycles.Length; ++d)
                {
                    if (settings.KFLDRLDutyCycles[d] == dataPoints.DutyCycle)
                    {
                        for (int r = 0; r < settings.KFLDRLRpms.Length; ++r)
                        {
                            int rpm = settings.KFLDRLRpms[r];
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

                            string value = "0.000";
                            if (highPoints.Count > 0 || lowPoints.Count > 0)
                            {
                                decimal highPressure = highPoints.Count > 0 ? highPoints.Average(p => p.absolutePressure) : 0;
                                decimal lowPressure = lowPoints.Count > 0 ? lowPoints.Average(p => p.absolutePressure) : 0;

                                value = ((highPressure + lowPressure) / 2).ToString("0.000");
                            }

                            grdKFLDRL.Rows[r + 1].Cells[d + 1].Value = value;

                        }
                    }
                }
            }
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
                    if (dc != null && settings.KFLDRLDutyCycles.Contains((int)Math.Round(dc.Value)))
                    {
                        DataPoint p = new DataPoint();
                        p.timestamp = line.TimeStamp;

                        Variable v = line.GetVariableByName("nmot");
                        if (v == null)
                            v = line.GetVariableByName("nmot_w");
                        p.rpm = v.Value;

                        v = line.GetVariableByName("pvdks_w");
                        p.actualPresure = v.Value;
                       // p.absolutePressure = v.Value;


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

        public Settings LoadSettings()
        {
            settings = new Settings();

            settings.RangeFilter = new RangeFilter();
            try
            {
                settings.RangeFilter.mbar = decimal.Parse(txtFilterMbar.Text);
                settings.RangeFilter.seconds = decimal.Parse(txtFilterSeconds.Text);
                settings.RangeFilter.rpmRangeLengthMin = decimal.Parse(txtFilterSeconds.Text);

                settings.KFLDRLDutyCycles = new int[grdKFLDRL.Columns.Count - 1];
                for (int i = 1; i < grdKFLDRL.Columns.Count; ++i)
                {
                    settings.KFLDRLDutyCycles[i - 1] = int.Parse(grdKFLDRL.Rows[0].Cells[i].Value.ToString());
                }

                settings.KFLDRLRpms = new int[grdKFLDRL.Rows.Count - 2];
                for (int i = 1; i < grdKFLDRL.Rows.Count - 1; ++i)
                {
                    settings.KFLDRLRpms[i - 1] = int.Parse(grdKFLDRL.Rows[i].Cells[0].Value.ToString());
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
                MessageBox.Show("Error while loading settings\r\n" + e.ToString() );

            }

            return settings;
        }

        private void radDutyCycle_Click(object sender, EventArgs e)
        {
            BuildValues();
        }

        private void radPressure_Click(object sender, EventArgs e)
        {
            BuildValues();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            settings = LoadSettings();

            decimal[][] data = new decimal[settings.KFLDRLRpms.Length][];
            for (int i = 0; i < settings.KFLDRLRpms.Length; ++i)
            {
                data[i] = new decimal[settings.KFLDRLDutyCycles.Length];
                for (int j = 0; j < settings.KFLDRLDutyCycles.Length; ++j)
                {
                    decimal value = 0;
                    object obj = grdKFLDRL.Rows[i + 1].Cells[j + 1].Value;
                    if (obj != null)
                    {
                        decimal.TryParse(grdKFLDRL.Rows[i + 1].Cells[j + 1].Value.ToString(), out value);
                    }
                    data[i][j] = value;
                }
            }

            DataForm dataForm = new DataForm(settings, data);
            dataForm.Show(this);
        }
    }

    
}
