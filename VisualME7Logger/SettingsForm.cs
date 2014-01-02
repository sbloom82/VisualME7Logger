using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualME7Logger.Configuration;
using System.Xml.Linq;

namespace VisualME7Logger
{
    public partial class SettingsForm : Form
    {
        enum GridColumns
        {
            Selected = 0,
            Name,
            Alias,
            Unit,
            Comment,
            MeasurementObject
        }

        ECUFile SelectedECUFile { get; set; }
        VisualME7Logger.Session.LoggerOptions LoggerOptions { get; set; }
        VisualME7Logger.Output.ChecksumInfo ChecksumInfo { get; set; }

        public SettingsForm()
        {
            InitializeComponent();

            SetupGrid();

            this.LoggerOptions = new Session.LoggerOptions(Program.ME7LoggerDirectory);
            this.ChecksumInfo = new Output.ChecksumInfo();

            this.LoadSettings();
            this.SwitchUI();
        }

        void SetupGrid()
        {
            CheckBox ckBox = new CheckBox();
            //Get the column header cell bounds
            Rectangle rect = this.dataGridView1.GetCellDisplayRectangle(0, -1, true);
            ckBox.Size = new Size(13, 13);
            //Change the location of the CheckBox to make it stay on the header
            ckBox.Location = new Point(5, 5);
            ckBox.CheckedChanged += ckBox_CheckedChanged;
            //Add the CheckBox into the DataGridView
            this.dataGridView1.Controls.Add(ckBox);
        }

        void ckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool chked = ((CheckBox)sender).Checked;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                r.Cells[(int)GridColumns.Selected].Value = chked;
            }
        }

        private void loadECUFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.IO.Path.Combine(Program.ME7LoggerDirectory, "ecus");
            ofd.Title = "Select ECU File";
            ofd.Filter = "ECU Files (*.ecu)|*.ecu";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.SelectedECUFile = null;
                ECUFile file = new ECUFile(ofd.FileName);
                if (file.Open())
                    this.SelectedECUFile = file;
                this.LoadECUFile();
            }
        }

        private void LoadECUFile()
        {
            this.Clear();
            if (this.SelectedECUFile != null)
            {
                this.txtECUFile.Text = this.SelectedECUFile.FilePath;
                this.loadConfigFileToolStripMenuItem.Enabled = 
                this.saveConfigFileToolStripMenuItem.Enabled = true;
                this.btnStartLog.Enabled = true;

                foreach (Measurement m in this.SelectedECUFile.Measurements.Values.Where(m => !string.IsNullOrEmpty(m.Alias)).OrderBy(m => m.Alias).ThenBy(m => m.Name))
                {
                    lstAvailMeasurements.Items.Add(m);
                    dataGridView1.Rows.Add(false, m.Name, m.Alias, m.Unit, m.Comment, m);
                }
                foreach (Measurement m in this.SelectedECUFile.Measurements.Values.Where(m => string.IsNullOrEmpty(m.Alias)).OrderBy(m => m.Alias).ThenBy(m => m.Name))
                {
                    lstAvailMeasurements.Items.Add(m);
                    dataGridView1.Rows.Add(false, m.Name, m.Alias, m.Unit, m.Comment, m);
                }
            }
        }

        private void Clear()
        {
            dataGridView1.Rows.Clear();
            lstAvailMeasurements.Items.Clear();
            lstSelectedMeasurements.Items.Clear();
            txtConfigFile.Text = string.Empty;
            btnAddMeasurement.Enabled =
                btnRemoveMeasurement.Enabled =
                loadConfigFileToolStripMenuItem.Enabled =
                saveConfigFileToolStripMenuItem.Enabled =
                btnStartLog.Enabled = false;
        }

        private void SwitchUI()
        {

        }

        private void btnAddMeasurement_Click(object sender, EventArgs e)
        {
            List<object> oList = new List<object>();
            foreach (object o in lstAvailMeasurements.SelectedItems)
            {
                oList.Add(o);
            }

            foreach (object o in oList)
            {
                lstSelectedMeasurements.Items.Add(o);
                lstAvailMeasurements.Items.Remove(o);
            }
        }

        private void btnRemoveMeasurement_Click(object sender, EventArgs e)
        {
            List<object> oList = new List<object>();
            foreach (object o in lstSelectedMeasurements.SelectedItems)
            {
                oList.Add(o);
            }

            foreach (object o in oList)
            {
                lstAvailMeasurements.Items.Add(o);
                lstSelectedMeasurements.Items.Remove(o);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstAvailMeasurements_SelectedValueChanged(object sender, EventArgs e)
        {
            this.btnAddMeasurement.Enabled = lstAvailMeasurements.SelectedItems.Count > 0;
        }

        private void lstSelectedMeasurements_SelectedValueChanged(object sender, EventArgs e)
        {
            this.btnRemoveMeasurement.Enabled = lstSelectedMeasurements.SelectedItems.Count > 0;
        }

        private ConfigFile SaveConfigFile()
        {
            Measurements ms = new Measurements();
            foreach (DataGridViewRow r in this.dataGridView1.Rows)
            {
                if ((bool)r.Cells[(int)GridColumns.Selected].Value == true)
                {
                    ms.AddMeasurement((Measurement)r.Cells[(int)GridColumns.MeasurementObject].Value);
                }
            }

            if (ms.Values.Count() > 0)
            {
                if (string.IsNullOrEmpty(this.txtConfigFile.Text))
                {
                    SaveFileDialog d = new SaveFileDialog();
                    d.Title = "Save Config File As...";
                    d.InitialDirectory = System.IO.Path.Combine(Program.ME7LoggerDirectory, "logs");
                    if (d.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        return null;
                    }
                    this.txtConfigFile.Text = d.FileName;
                }

                ConfigFile configFile = new ConfigFile(this.SelectedECUFile.FileName, ms);
                configFile.Write(txtConfigFile.Text);
                return configFile;
            }
            else
            {
                MessageBox.Show("No Measurements Selected");
            }
            return null;
        }

        private void btnStartLog_Click(object sender, EventArgs e)
        {
            if (this.LoggerOptions.ConnectionType != Session.LoggerOptions.ConnectionTypes.LogFile)
            {
                if (this.SaveConfigFile() == null)
                {
                    return;
                }
            }

            this.SaveSettings();

            Form1 logForm = new Form1(txtConfigFile.Text, this.LoggerOptions);
            logForm.ShowDialog(this);
        }

        private void loadConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.IO.Path.Combine(Program.ME7LoggerDirectory, "logs");
            ofd.Title = "Select Config File";
            ofd.Filter = "Config Files (*.cfg)|*.cfg";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.LoadConfigFile(ofd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void LoadConfigFile(string filePath)
        {
            ConfigFile configFile = new ConfigFile(this.SelectedECUFile.FileName);
            configFile.Read(filePath);
            this.txtConfigFile.Text = filePath;

            lstAvailMeasurements.Items.Clear();
            lstSelectedMeasurements.Items.Clear();

            foreach (Measurement m in this.SelectedECUFile.Measurements.Values)
            {
                if (configFile.Measurements[m.Name] != null)
                {
                    lstSelectedMeasurements.Items.Add(m);

                    foreach (DataGridViewRow r in this.dataGridView1.Rows)
                    {
                        if (r.Cells[(int)GridColumns.Name].Value == m.Name)
                        {
                            r.Cells[(int)GridColumns.Selected].Value = true;
                        }
                    }
                }
                else
                {
                    lstAvailMeasurements.Items.Add(m);
                }
            }
        }

        private void clearConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.txtConfigFile.Text = string.Empty;
        }

        private void lstSelectedMeasurements_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                btnRemoveMeasurement_Click(sender, e);
            }
        }

        private void lstAvailMeasurements_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                btnAddMeasurement_Click(sender, e);
            }
        }

        private void createECUFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = System.IO.Path.Combine(Program.ME7LoggerDirectory, "images");
            ofd.Title = "Select ECU Image File";
            ofd.Filter = "ECU Images (*.bin)|*.bin";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    ECUFile ecuFile = ECUFile.Create(Program.ME7LoggerDirectory, ofd.FileName);
                    if (DialogResult.Yes == MessageBox.Show(string.Format("ECU File Created at:{1}{1}{0}{1}{1}Would you like to load this ECU File now?", ecuFile.FilePath, Environment.NewLine),
                                                            "ECU File created successfully", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                    {
                        if (ecuFile.Open())
                            this.SelectedECUFile = ecuFile;
                        this.LoadECUFile();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        void SaveSettings()
        {
            try
            {
                XElement root = new XElement("VisualME7LoggerSettings");
                root.Add(new XAttribute("ECUFile", this.txtECUFile.Text));
                root.Add(new XAttribute("ConfigFile", this.txtConfigFile.Text));

                root.Add(this.LoggerOptions.Write());
                root.Add(this.ChecksumInfo.Write());
                root.Save(System.IO.Path.Combine(Program.ME7LoggerDirectory, "VisualME7Logger.cfg.xml"));
            }
            catch { }
        }

        void LoadSettings()
        {
            string filePath = System.IO.Path.Combine(Program.ME7LoggerDirectory, "VisualME7Logger.cfg.xml");
            if (System.IO.File.Exists(filePath))
            {
                XElement root = XElement.Load(filePath);
                foreach (XAttribute att in root.Attributes())
                {
                    switch (att.Name.LocalName)
                    {
                        case "ECUFile":
                            ECUFile file = new ECUFile(att.Value);
                            if (file.Open())
                            {
                                txtECUFile.Text = att.Value;
                                this.SelectedECUFile = file;
                                LoadECUFile();
                            }
                            break;
                        case "ConfigFile":
                            this.LoadConfigFile(att.Value);
                            break;
                    }
                }

                foreach (XElement ele in root.Elements())
                {
                    switch (ele.Name.LocalName)
                    {
                        case "Options":
                            this.LoggerOptions.Read(ele);
                            break;
                        case "ChecksumInfo":
                            this.ChecksumInfo.Read(ele);
                            break;
                    }
                }
            }
        }

        private void lstAvailMeasurements_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnAddMeasurement_Click(sender, e);
        }

        private void lstSelectedMeasurements_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnRemoveMeasurement_Click(sender, e);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                foreach (DataGridViewRow r in this.dataGridView1.SelectedRows)
                {
                    r.Cells[(int)GridColumns.Selected].Value = !(bool)r.Cells[(int)GridColumns.Selected].Value;
                }
            }
        }

        private void mE7CheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChecksumForm(this.ChecksumInfo).ShowDialog(this);
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveSettings();
        }

        private void saveConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveConfigFile();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new OptionsForm(this.LoggerOptions).ShowDialog(this);
            this.SwitchUI();
        }

        private void selectedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void unselectedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}