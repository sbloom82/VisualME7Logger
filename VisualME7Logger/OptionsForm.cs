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
            this.Options = options;
            this.LoadOptions();
            this.SwitchUI();
        }

        private void LoadOptions()
        {
            radCommDefault.Checked = Options.ConnectionType == Session.LoggerOptions.ConnectionTypes.Default;
            radLogFile.Checked = Options.ConnectionType == Session.LoggerOptions.ConnectionTypes.LogFile;
            radCommCOMPort.Checked = Options.ConnectionType == Session.LoggerOptions.ConnectionTypes.COM;
            radFTDI.Checked = !radCommDefault.Checked && !radCommCOMPort.Checked && !radLogFile.Checked;

            chkFTDISerial.Checked = Options.ConnectionType == Session.LoggerOptions.ConnectionTypes.FTDISerial;
            chkFTDIDesc.Checked = Options.ConnectionType == Session.LoggerOptions.ConnectionTypes.FTDIDescription;
            chkFTDILocation.Checked = Options.ConnectionType == Session.LoggerOptions.ConnectionTypes.FTDILocation;

            txtCOMPort.Text = Options.COMPort;
            txtFTDIInfo.Text = Options.FTDIInfo;

            chkOverrideBaudRate.Checked = Options.OverrideBaudRate;
            cmbBaudRate.Text = Options.BaudRate.ToString();

            chkOverrideSampleRate.Checked = Options.OverrideSampleRate;
            nudSampleRate.Value = Options.SampleRate;

            chkWriteToLogRealTime.Checked = Options.WriteLogRealTime;
            chkTimeSync.Checked = Options.TimeSync;
            chkWriteAbsoluteTimeStamp.Checked = Options.WriteAbsoluteTimestamp;
            chkReadSingleMeasurement.Checked = Options.ReadSingleMeasurement;

            chkWriteToLog.Checked = Options.WriteLogToFile;
            txtLogFilePath.Text = Options.LogFile;

            chkWriteOutputToFile.Checked = Options.WriteOutputFile;
            
            chkDisableRealTimeDisplay.Checked = !Options.RealTimeOutput;
            chkWriteLogFileWithVME7L.Checked = Options.WriteLogFileWithVME7L;
        }

        private void SaveOptions()
        {
            if (radCommDefault.Checked)
            {
                Options.ConnectionType = Session.LoggerOptions.ConnectionTypes.Default;
            }
            else if (radLogFile.Checked)
            {
                Options.ConnectionType = Session.LoggerOptions.ConnectionTypes.LogFile;
            }
            else if (radCommCOMPort.Checked)
            {
                Options.ConnectionType = Session.LoggerOptions.ConnectionTypes.COM;
            }
            else
            {
                Options.ConnectionType = Session.LoggerOptions.ConnectionTypes.FTDI;
                if (chkFTDIDesc.Checked)
                {
                    Options.ConnectionType = Session.LoggerOptions.ConnectionTypes.FTDIDescription;
                }
                else if (chkFTDILocation.Checked)
                {
                    Options.ConnectionType = Session.LoggerOptions.ConnectionTypes.FTDILocation;
                }
                else if (chkFTDISerial.Checked)
                {
                    Options.ConnectionType = Session.LoggerOptions.ConnectionTypes.FTDISerial;
                }
            }

            Options.COMPort = txtCOMPort.Text;
            Options.FTDIInfo = txtFTDIInfo.Text;

            Options.OverrideBaudRate = chkOverrideBaudRate.Checked;
            Options.BaudRate = int.Parse(cmbBaudRate.Text);

            Options.OverrideSampleRate = chkOverrideSampleRate.Checked;
            Options.SampleRate = (int)nudSampleRate.Value;

            Options.WriteLogRealTime = chkWriteToLogRealTime.Checked;
            Options.TimeSync = chkTimeSync.Checked;
            Options.WriteAbsoluteTimestamp = chkWriteAbsoluteTimeStamp.Checked;
            Options.ReadSingleMeasurement = chkReadSingleMeasurement.Checked;

            Options.WriteLogToFile = chkWriteToLog.Checked;
            Options.LogFile = txtLogFilePath.Text;

            Options.WriteOutputFile = chkWriteOutputToFile.Checked;
            Options.RealTimeOutput = !chkDisableRealTimeDisplay.Checked;
            Options.WriteLogFileWithVME7L = chkWriteLogFileWithVME7L.Checked;
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
            this.txtLogFilePath.Enabled = this.chkWriteToLog.Checked || radLogFile.Checked;
            this.gpOther.Enabled =
                this.gbTroubleshooting.Enabled =
                this.chkWriteToLog.Enabled =
                this.chkOverrideBaudRate.Enabled = !this.radLogFile.Checked;
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


        private void chkWriteToLog_CheckedChanged(object sender, EventArgs e)
        {
            this.SwitchUI();
        }

        private void radLogFile_CheckedChanged(object sender, EventArgs e)
        {
            SwitchUI();
        }  

        private void btnChooseLogPath_Click(object sender, EventArgs e)
        {
            FileDialog dialog = null;
            if (radLogFile.Checked)
            {
                dialog = new OpenFileDialog();
                dialog.Title = "Open a log file"; 
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                dialog = sfd;
                dialog.Title = "Save log file as...";
                sfd.Filter = "Log Files (*.csv)|*.csv|All Files (*.*)|*.*";
                sfd.OverwritePrompt = false;
            }

            dialog.DefaultExt = ".csv";
            dialog.InitialDirectory =
                    string.IsNullOrWhiteSpace(txtLogFilePath.Text) ?
                    System.IO.Path.Combine(Program.ME7LoggerDirectory, "logs") :
                    System.IO.Path.GetDirectoryName(txtLogFilePath.Text);
            dialog.FileName = System.IO.Path.GetFileName(txtLogFilePath.Text);
            
            if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.txtLogFilePath.Text = dialog.FileName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
