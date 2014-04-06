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
    public partial class ChecksumForm : Form
    {
        private VisualME7Logger.Output.ChecksumInfo ChecksumInfo { get; set; }
        public ChecksumForm(VisualME7Logger.Output.ChecksumInfo checksumInfo )
        {
            InitializeComponent();
            this.ChecksumInfo = checksumInfo;
            this.LoadOptions();
        }

        private void LoadOptions()
        {
            this.txtAppPath.Text = this.ChecksumInfo.ApplicationPath;
            this.txtBinPath.Text = this.ChecksumInfo.BinPath;
        }

        private void SaveOptions()
        {
            this.ChecksumInfo.ApplicationPath = this.txtAppPath.Text;
            this.ChecksumInfo.BinPath = this.txtBinPath.Text;
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
            ofd.Title = "Select the path of the checksum application";
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
            ofd.Title = "Select the bin path";
            if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.txtBinPath.Text = ofd.FileName;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            VisualME7Logger.Output.ChecksumInfo checksumInfo = new Output.ChecksumInfo();
            checksumInfo.ApplicationPath = this.txtAppPath.Text;
            checksumInfo.BinPath = this.txtBinPath.Text;
            Output.ChecksumResult result = checksumInfo.Check();
            this.txtOutput.Text = result.Output;
            MessageBox.Show(
                this,
                string.Format("Checksum validation {0}successful", result.Success  ? "" : "not "),
                result.Success ? "Success" :"Error",
                MessageBoxButtons.OK,
                result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
    }
}
