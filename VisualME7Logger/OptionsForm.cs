using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VisualME7Logger
{
    public partial class OptionsForm : Form
    {
        private VisualME7Logger.Session.LoggerOptions Options { get; set; }
        public OptionsForm(VisualME7Logger.Session.LoggerOptions options)
        {
            InitializeComponent();
            this.txtLogFilePath.Text = System.IO.Path.Combine(Program.ME7LoggerDirectory, "logs", "VisualME7Logger_log.txt");
            this.cmbBaudRate.Text = "56000";
            this.Options = options;
            this.LoadOptions();
            this.SwitchUI();
        }

        private void LoadOptions()
        {
            //todo
        }

        private void SaveOptions()
        {
            //todo
        }

        private void SwitchUI()
        {
            this.txtCOMPort.Enabled = radCommCOMPort.Checked;
            this.chkFTDILocation.Enabled =
                this.chkFTDIDesc.Enabled =
                this.chkFTDISerial.Enabled = 
                this.txtFTDIInfo.Enabled = radFTDI.Checked;
            this.cmbBaudRate.Enabled = this.chkOverrideBaudRate.Checked;
            this.nudSampleRate.Enabled = this.chkOverrideSampleRate.Checked; 
        }
               
        private void chkFTDISerial_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFTDISerial.Checked)
            {
                chkFTDIDesc.Checked =
                    chkFTDILocation.Checked = false;
            }
            this.SwitchUI();
        }

        private void chkFTDIDesc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFTDIDesc.Checked)
            {
                chkFTDISerial.Checked =
                    chkFTDILocation.Checked = false;
            }
            this.SwitchUI();
        }

        private void chkFTDILocation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFTDILocation.Checked)
            {
                chkFTDIDesc.Checked =
                    chkFTDISerial.Checked = false;
            }
            this.SwitchUI();
        }

        private void radFTDI_CheckedChanged(object sender, EventArgs e)
        {
            this.SwitchUI();
        }

        private void radCommCOMPort_CheckedChanged(object sender, EventArgs e)
        {
            this.SwitchUI();
        }

        private void radCommDefault_CheckedChanged(object sender, EventArgs e)
        {
            this.SwitchUI();
        }

        private void chkOverrideBaudRate_CheckedChanged(object sender, EventArgs e)
        {
            this.SwitchUI();
        }

        private void chkOverrideSampleRate_CheckedChanged(object sender, EventArgs e)
        {
            this.SwitchUI();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveOptions();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
