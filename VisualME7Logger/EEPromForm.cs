using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VisualME7Logger
{
    public partial class EEPromForm : Form
    {
        public VisualME7Logger.Output.EEProm EEProm { get; set; }
        public EEPromForm(VisualME7Logger.Output.EEProm eeProm)
        {
            InitializeComponent();
            this.EEProm = eeProm.Clone();
            this.LoadOptions();
        }

        private void LoadOptions()
        {
            this.txtAppPath.Text = this.EEProm.ApplicationPath;
            this.txtBinPath.Text = this.EEProm.BinPath;

            this.cmbBaudrate.SelectedIndex = 0;
        }

        private void SaveOptions()
        {
            this.EEProm.ApplicationPath = this.txtAppPath.Text;
            this.EEProm.BinPath = this.txtBinPath.Text;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveOptions();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAppPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory =
                string.IsNullOrWhiteSpace(txtAppPath.Text) ?
                Program.ME7LoggerDirectory :
                System.IO.Path.GetDirectoryName(txtAppPath.Text);
            ofd.FileName = System.IO.Path.GetFileName(txtAppPath.Text);
            ofd.Title = "Select the path of the eeprom application";
            if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.txtAppPath.Text = ofd.FileName;
            }
        }

        private void btnBinPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory =
                string.IsNullOrWhiteSpace(txtBinPath.Text) ?
                System.IO.Path.Combine(Program.ME7LoggerDirectory, "images") :
                System.IO.Path.GetDirectoryName(txtBinPath.Text);
            ofd.FileName = System.IO.Path.GetFileName(txtBinPath.Text);
            ofd.Title = "Select the bin/save path";
            if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.txtBinPath.Text = ofd.FileName;
            }
        }

        private void btnGetSKC_Click(object sender, EventArgs e)
        {
            this.txtOutput.Text = string.Format("SKC: {0}", EEProm.GetSKC());
        }

        private void SetData()
        {
            this.txtVIN.Text = EEProm.GetVIN();
            this.txtSKC.Text = EEProm.GetSKC();
            this.txtImmoID.Text = EEProm.GetImmoID();
            string immoData = EEProm.GetImmoData();
            bool? immoOn = EEProm.ImmoEnabled();
            bool? deathCodeOn = EEProm.DeathCodeOn();

            this.txtImmoData.Text = immoData;

            this.chkImmoEnabled.Checked = immoOn.HasValue && immoOn.Value;
            this.chkImmoDisabled.Checked = immoOn.HasValue && !immoOn.Value;

            chkFixDeathCode.Checked = false;
            chkFixDeathCode.Enabled = false;
            chkFixDeathCode.ForeColor = Color.Black;
            if (deathCodeOn.HasValue && deathCodeOn.Value)
            {
                chkFixDeathCode.Enabled = true;
                chkFixDeathCode.ForeColor = Color.Red;
                chkFixDeathCode.Checked = true;
            }

            this.chkCorrectChecksums.Checked = true;
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            this.txtOutput.Text = "";
            Output.EEProm.EEPromResult result = EEProm.ReadEEProm(false);
            this.txtOutput.Text = result.Output;

            this.SetData();

            MessageBox.Show(
                this,
                string.Format("EEProm Read {0}successful", result.Success ? "" : "not "),
                result.Success ? "Success" : "Error",
                MessageBoxButtons.OK,
                result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private void btnReadBootmode_Click(object sender, EventArgs e)
        {
            this.txtOutput.Text = "";
            MessageBox.Show("Bootmode required.  Ground pin 24 on eeprom chip for 5 seconds while powering up ecu before proceeding");
            Output.EEProm.EEPromResult result = EEProm.ReadEEProm(true);
            this.txtOutput.Text = result.Output;

            this.SetData();

            MessageBox.Show(
                this,
                string.Format("EEProm Read {0}successful", result.Success ? "" : "not "),
                result.Success ? "Success" : "Error",
                MessageBoxButtons.OK,
                result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            this.SetData();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            this.txtOutput.Text = "";
            if(DialogResult.OK == 
                MessageBox.Show(
                    this, 
                    string.Format("This will write the contents of {0} to the EEProm.  Please verify that you have corrected checksums in the file before proceeding",txtBinPath.Text),
                    "CAUTION",  
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation, 
                    MessageBoxDefaultButton.Button2))
            {
                MessageBox.Show("Bootmode required.  Ground pin 24 on eeprom chip for 5 seconds while powering up ecu before proceeding");
                Output.EEProm.EEPromResult result = EEProm.WriteEEProm();
                this.txtOutput.Text = result.Output;
            }
        }

        

        private void txtAppPath_TextChanged(object sender, EventArgs e)
        {
            EEProm.ApplicationPath = txtAppPath.Text;
        }

        private void txtBinPath_TextChanged(object sender, EventArgs e)
        {
            EEProm.BinPath = txtBinPath.Text;
        }

        private void txtCOMPort_TextChanged(object sender, EventArgs e)
        {
            EEProm.COMPort = txtCOMPort.Text;
        }

        private void cmbBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            EEProm.Baudrate = cmbBaudrate.SelectedIndex <= 0 ? "" : cmbBaudrate.Text;
        }        

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = EEProm.BinPath;
            if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                Output.EEProm.EEPromResult result = EEProm.WriteFile(
                    dialog.FileName,
                    this.txtVIN.Text,
                    this.txtSKC.Text,
                    this.txtImmoID.Text,
                    this.txtImmoData.Text,
                    !this.chkImmoEnabled.Checked && !chkImmoDisabled.Checked ? new bool?() : new bool?(chkImmoEnabled.Checked),
                    this.chkFixDeathCode.Checked,
                    this.chkCorrectChecksums.Checked);
                this.txtOutput.Text = result.Output;
            }
        }

        private void chkImmoEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImmoEnabled.Checked)
            {
                chkImmoDisabled.Checked = false;
            }
        }

        private void chkImmoDisabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImmoDisabled.Checked)
            {
                chkImmoEnabled.Checked = false;
            }
        }
    }
}
