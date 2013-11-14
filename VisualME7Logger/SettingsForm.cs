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
        ECUFile SelectedECUFile { get; set; }

        public SettingsForm()
        {
            InitializeComponent();
            this.LoadSettings();
            this.txtLogFile.Text = System.IO.Path.Combine(Program.ME7LoggerDirectory, "logs", "VisualME7Logger_log.txt");
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
                this.loadConfigFileToolStripMenuItem.Enabled = true;
                this.btnStartLog.Enabled = true;

                foreach (Measurement m in this.SelectedECUFile.Measurements.Values)
                {
                    lstAvailMeasurements.Items.Add(m);
                }
            }
        }

        private void Clear()
        {
            lstAvailMeasurements.Items.Clear();
            lstSelectedMeasurements.Items.Clear();
            txtConfigFile.Text = string.Empty;
            btnAddMeasurement.Enabled =
                btnRemoveMeasurement.Enabled =
                loadConfigFileToolStripMenuItem.Enabled =
                btnStartLog.Enabled = false;
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

        private void btnStartLog_Click(object sender, EventArgs e)
        {
            if (lstSelectedMeasurements.Items.Count > 0)
            {
                if (string.IsNullOrEmpty(this.txtConfigFile.Text))
                {
                    SaveFileDialog d = new SaveFileDialog();
                    d.Title = "Save Config File As...";
                    d.InitialDirectory = System.IO.Path.Combine(Program.ME7LoggerDirectory, "logs");
                    if (d.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }
                    this.txtConfigFile.Text = d.FileName;
                }

                this.SaveSettings();

                Measurements ms = new Measurements();
                foreach (Measurement m in lstSelectedMeasurements.Items)
                {
                    ms.AddMeasurement(m);
                }

                ConfigFile configFile = new ConfigFile(this.SelectedECUFile.FileName, ms);
                configFile.Write(txtConfigFile.Text);

                string parameters = "-p COM1 -R";
                if (!string.IsNullOrEmpty(this.txtLogFile.Text))
                {
                    parameters += " -o \"" + this.txtLogFile.Text.Trim() + "\"";
                }

                Form1 logForm = new Form1(txtConfigFile.Text, parameters);
                logForm.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("No Measurements Selected");
            }
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
                root.Add(new XAttribute("LogFile", this.txtLogFile.Text));
                root.Save(System.IO.Path.Combine(Program.ME7LoggerDirectory, "VisualME7Logger.cfg"));
            }
            catch { }
        }

        void LoadSettings()
        {
            string filePath = System.IO.Path.Combine(Program.ME7LoggerDirectory, "VisualME7Logger.cfg");                
            if(System.IO.File.Exists(filePath))
            {
                XElement root = XElement.Load(filePath);
                foreach(XAttribute att in root.Attributes())
                {
                    switch(att.Name.LocalName)
                    {
                        case "ECUFile":                        
                            ECUFile file = new ECUFile(att.Value);
                            if(file.Open())
                            {
                                txtECUFile.Text = att.Value;
                                this.SelectedECUFile = file;
                                LoadECUFile();
                            }
                            break;
                        case "ConfigFile":
                            this.LoadConfigFile(att.Value);
                            break;
                        case "LogFile":
                            txtLogFile.Text = att.Value;
                            break;
                    }
                }
            }            
        }
    }
}