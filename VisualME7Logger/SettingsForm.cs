using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VisualME7Logger.Configuration;

namespace VisualME7Logger
{
    public partial class SettingsForm : Form
    {
        ECUFile SelectedECUFile{get; set;}
        

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void loadECUFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
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
                this.Text = string.Format("ECU File: {0}", this.SelectedECUFile.FilePath);
                foreach (Measurement m in this.SelectedECUFile.Measurements.Values.Where(m => !string.IsNullOrEmpty(m.Alias)).OrderBy(m => m.Alias).ThenBy(m => m.Name))
                {
                    lstAvailMeasurements.Items.Add(m);
                }
                foreach (Measurement m in this.SelectedECUFile.Measurements.Values.Where(m => string.IsNullOrEmpty(m.Alias)).OrderBy(m => m.Name))
                {
                    lstAvailMeasurements.Items.Add(m);
                }
            }
        }

        private void Clear()
        {
            lstAvailMeasurements.Items.Clear();
            lstSelectedMeasurements.Items.Clear();
            btnAddMeasurement.Enabled = btnRemoveMeasurement.Enabled = false;
        }

        private void btnAddMeasurement_Click(object sender, EventArgs e)
        {
            List<object> oList = new List<object>();
            foreach (object o in lstAvailMeasurements.SelectedItems)
            {
                oList.Add(o);               
            }

            foreach(object o in oList)
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
    }
}
