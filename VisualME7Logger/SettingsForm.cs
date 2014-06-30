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
using System.Windows.Forms.DataVisualization.Charting;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;

namespace VisualME7Logger
{
    public partial class SettingsForm : Form
    {
        enum EditModes
        {
            View,
            Add,
            Edit
        }


        List<Profile> Profiles = new List<Profile>();
        Profile CurrentProfile = new Profile("Default Profile");

        EditModes ProfileEditMode;
        Profile SelectedProfile;
        EditModes GraphVariableEditMode;
        GraphVariable SelectedGraphVariable;
        EditModes ExpressionEditMode;
        Session.ExpressionVariable SelectedExpression;

        public SettingsForm()
        {
            VisualME7Logger.Session.ME7LoggerSession.Debug = Program.Debug;

            InitializeComponent();

            SetupGrid();

            this.LoadSettings();

            this.SwitchUI();

            this.cmbGraphVariableStyle.DataSource = Enum.GetValues(typeof(ChartDashStyle));

#if !DEBUG
            bool isAdmin = false;
            try
            {
                isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch { }
            if (!isAdmin)
            {
                MessageBox.Show(this, "VisualME7Logger detected that it is not running with administrative privileges.  You may have problems using this software.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
#endif
        }

        void SetupGrid()
        {
            this.dataGridView1.AutoGenerateColumns = false;

            DataGridViewCheckBoxColumn cbColumn = new DataGridViewCheckBoxColumn();
            cbColumn.Width = 30;
            cbColumn.DataPropertyName = "Selected";
            dataGridView1.Columns.Add(cbColumn);

            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Name";
            column.Name = "Name";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Alias";
            column.Name = "Alias";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Unit";
            column.Name = "Unit";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(column);

            column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "Comment";
            column.Name = "Comment";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns.Add(column);


            CheckBox ckBox = new CheckBox();
            //Get the column header cell bounds
            Rectangle rect = this.dataGridView1.GetCellDisplayRectangle(0, -1, true);
            ckBox.Size = new Size(13, 13);
            //Change the location of the CheckBox to make it stay on the header
            ckBox.Location = new Point(10, 5);
            ckBox.CheckedChanged += ckBox_CheckedChanged;
            //Add the CheckBox into the DataGridView
            this.dataGridView1.Controls.Add(ckBox);
        }

        void ckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool chked = ((CheckBox)sender).Checked;
            if (this.CurrentProfile.ECUFile != null)
            {
                foreach (Measurement m in this.CurrentProfile.ECUFile.Measurements.Values)
                {
                    m.Selected = chked;
                }
                this.ApplyFilter();
            }
        }

        private void loadECUFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory =
               string.IsNullOrWhiteSpace(this.txtECUFile.Text) ?
               System.IO.Path.Combine(Program.ME7LoggerDirectory, "ecus") :
               System.IO.Path.GetDirectoryName(this.txtECUFile.Text);
            ofd.FileName = System.IO.Path.GetFileName(this.txtECUFile.Text);
            ofd.Title = "Select ECU File";
            ofd.Filter = "ECU Files (*.ecu)|*.ecu";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.CurrentProfile.ECUFile = null;
                ECUFile file = new ECUFile(ofd.FileName);
                file.Open();
                if (!file.Measurements.Values.Any())
                {
                    MessageBox.Show(this, "ECU File loaded without error but no measurements were read.");
                }
                this.CurrentProfile.ECUFile = file;
                this.LoadECUFile();
            }
        }

        private void LoadECUFile()
        {
            this.txtECUFile.Text = this.CurrentProfile.ECUFile.FilePath;
            this.CurrentProfile.ECUFile.Open();
            this.loadConfigFileToolStripMenuItem.Enabled =
            this.saveConfigFileToolStripMenuItem.Enabled =
            this.saveConfigFileAsToolStripMenuItem.Enabled = true;
            this.btnStartLog.Enabled = true;

            this.cmbGraphVariableVariable.DataSource =
                this.CurrentProfile.ECUFile.Measurements.Values.Select(m => m.Name).ToList();

            LoadConfigFile();
        }

        private void SwitchUI()
        {
            this.lstGraphVariables.Enabled =
                this.btnAddGraphVariable.Enabled  = this.GraphVariableEditMode == EditModes.View;            
            this.btnEditGraphVariable.Enabled =
                this.btnDeleteGraphVariable.Enabled  = this.GraphVariableEditMode == EditModes.View && lstGraphVariables.SelectedItem != null;
            this.gbGraphVariables.Enabled = this.GraphVariableEditMode != EditModes.View;

            this.lstProfiles.Enabled =
                this.btnProfileAdd.Enabled = this.ProfileEditMode == EditModes.View;
            this.btnProfileSetCurrent.Enabled =
                this.btnProfileClone.Enabled =
                this.btnProfileEdit.Enabled =
                this.btnProfileDelete.Enabled = this.ProfileEditMode == EditModes.View && lstProfiles.SelectedItem != null;
            this.gbProfile.Enabled = this.ProfileEditMode != EditModes.View;

            this.lstExpressions.Enabled =
                this.btnExpressionAdd.Enabled = this.ExpressionEditMode == EditModes.View;
            this.btnExpressionClone.Enabled =
                this.btnExpressionEdit.Enabled =
                this.btnExpressionDelete.Enabled = this.ExpressionEditMode == EditModes.View && lstExpressions.SelectedItem != null;
            this.gbExpressions.Enabled = this.ExpressionEditMode != EditModes.View;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ApplyFilter()
        {
            lblMeasurementCount.Text = string.Empty;

            IEnumerable<Measurement> measurements =
                this.CurrentProfile.ECUFile.Measurements.Values.Where(m => !string.IsNullOrEmpty(m.Alias)).OrderBy(m => m.Alias).ThenBy(m => m.Name).Union(
                this.CurrentProfile.ECUFile.Measurements.Values.Where(m => string.IsNullOrEmpty(m.Alias)).OrderBy(m => m.Alias).ThenBy(m => m.Name));

            if (!string.IsNullOrEmpty(this.txtFilter.Text))
            {
                string lookup = this.txtFilter.Text;
                measurements = measurements.Where(m =>
                    m.Name.IndexOf(lookup, StringComparison.InvariantCultureIgnoreCase) > -1 ||
                    m.Alias.IndexOf(lookup, StringComparison.InvariantCultureIgnoreCase) > -1 ||
                    m.Comment.IndexOf(lookup, StringComparison.InvariantCultureIgnoreCase) > -1);
            }

            List<Measurement> filtered = null;
            if (radFilterSelected.Checked)
            {
                filtered = measurements.Where(m => m.Selected).ToList();
            }
            else if (radFilterUnselected.Checked)
            {
                filtered = measurements.Where(m => !m.Selected).ToList();
            }
            else
            {
                filtered = measurements.ToList();
            }
            dataGridView1.DataSource = filtered;
            lblMeasurementCount.Text = string.Format("Showing {0} of {1}",
                filtered.Count,
                this.CurrentProfile.ECUFile.Measurements.Values.Count());
        }

        private ConfigFile SaveConfigFile(bool saveNew = false)
        {
            if (this.CurrentProfile.ECUFile != null)
            {
                Measurements ms = new Measurements();
                foreach (Measurement m in this.CurrentProfile.ECUFile.Measurements.Values.Where(m => m.Selected))
                {
                    ms.AddMeasurement(m);
                }

                if (ms.Values.Count() > 0)
                {
                    if (saveNew || string.IsNullOrEmpty(this.txtConfigFile.Text))
                    {
                        SaveFileDialog d = new SaveFileDialog();
                        d.Title = "Save Config File As...";
                        d.InitialDirectory =
                            string.IsNullOrWhiteSpace(this.txtConfigFile.Text) ?
                            System.IO.Path.Combine(Program.ME7LoggerDirectory, "logs") :
                            System.IO.Path.GetDirectoryName(this.txtConfigFile.Text);
                        d.FileName = System.IO.Path.GetFileName(this.txtConfigFile.Text);
                        if (d.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        {
                            return null;
                        }
                        this.txtConfigFile.Text = d.FileName;
                    }

                    ConfigFile configFile = new ConfigFile(txtConfigFile.Text, this.CurrentProfile.ECUFile.FileName, ms);
                    configFile.Write();
                    return configFile;
                }
                else
                {
                    MessageBox.Show("No Measurements Selected");
                }
            }
            return null;
        }

        private void LoadConfigFile()
        {
            txtConfigFile.Text = this.CurrentProfile.ConfigFile.FilePath;
            this.CurrentProfile.ConfigFile.Read();
            
            foreach (Measurement m in this.CurrentProfile.ECUFile.Measurements.Values)
            {
                m.Selected = false;
                if (this.CurrentProfile.ConfigFile.Measurements[m.Name] != null)
                {
                    m.Selected = true;
                }
            }
            ApplyFilter();
        }

        private void btnStartLog_Click(object sender, EventArgs e)
        {
            if (this.CurrentProfile.LoggerOptions.ConnectionType != Session.LoggerOptions.ConnectionTypes.LogFile)
            {
                if (this.SaveConfigFile() == null)
                {
                    return;
                }
            }

            this.SaveSettings();

            this.Visible = false;
            Form1 logForm = new Form1(txtConfigFile.Text, this.CurrentProfile.LoggerOptions, this.CurrentProfile.DisplayOptions);
            logForm.ShowDialog(this);

            this.SaveSettings();

            this.lstGraphVariables.DataSource = null;
            this.lstGraphVariables.DataSource = this.CurrentProfile.DisplayOptions.GraphVariables;

            this.Visible = true;
        }

        private void loadConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory =
              string.IsNullOrWhiteSpace(this.txtConfigFile.Text) ?
              System.IO.Path.Combine(Program.ME7LoggerDirectory, "logs") :
              System.IO.Path.GetDirectoryName(this.txtConfigFile.Text);
            ofd.FileName = System.IO.Path.GetFileName(this.txtConfigFile.Text);
            ofd.Title = "Select Config File";
            ofd.Filter = "Config Files (*.cfg)|*.cfg";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.CurrentProfile.ConfigFile = new ConfigFile(ofd.FileName);
                    this.LoadConfigFile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
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
                        ecuFile.Open();
                        if (!ecuFile.Measurements.Values.Any())
                        {
                            MessageBox.Show(this, "ECU File loaded without error but no measurements were read.");
                        }
                        this.CurrentProfile.ECUFile = ecuFile;
                        this.LoadECUFile();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public void SaveSettings()
        {
            this.CurrentProfile.DisplayOptions.RefreshInterval = (int)this.nudResfreshRate.Value;
            this.CurrentProfile.DisplayOptions.GraphHRes = (int)this.nudGraphResH.Value;
            this.CurrentProfile.DisplayOptions.GraphVRes = (int)this.nudGraphResV.Value;
       
            try
            {
                XElement root = new XElement("VisualME7LoggerSettings");
                root.Add(new XAttribute("CurrentProfile", this.CurrentProfile.Name));
                XElement profiles = new XElement("Profiles");
                foreach (Profile p in this.Profiles)
                {
                    profiles.Add(p.Write());
                }
                root.Add(profiles);
                root.Save(System.IO.Path.Combine(Program.ME7LoggerDirectory, "VisualME7Logger.cfg.xml"));
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("An error occurred while saving settings file\r\n{0}", e.ToString()));
            }
        }

        void LoadSettings()
        {
            if (!Profiles.Contains(CurrentProfile))
            {
                Profiles.Add(CurrentProfile);
            }

            try
            {
                string filePath = System.IO.Path.Combine(Program.ME7LoggerDirectory, "VisualME7Logger.cfg.xml");
                if (System.IO.File.Exists(filePath))
                {
                    string currentProfile = null;
                    XElement root = XElement.Load(filePath);
                    foreach (XAttribute att in root.Attributes())
                    {
                        switch (att.Name.LocalName)
                        {
                            case "CurrentProfile":
                                currentProfile = att.Value;
                                break;
                        }
                    }

                    foreach (XElement ele in root.Elements())
                    {
                        switch (ele.Name.LocalName)
                        {
                            case "Profiles":
                                this.Profiles = new List<Profile>();
                                foreach (XElement child in ele.Elements())
                                {
                                    Profile p = new Profile(string.Empty);
                                    p.Read(child);
                                    Profiles.Add(p);
                                    if (p.Name == currentProfile)
                                    {
                                        CurrentProfile = p;
                                    }
                                }
                                break;
                        }
                    }

                    if (currentProfile == null)
                    {
                        this.Profiles = new List<Profile>();
                        CurrentProfile = new Profile("Default Profile");
                        CurrentProfile.Read(root);
                        this.Profiles.Add(CurrentProfile);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("An error occurred while loading settings.\r\n\r\n{0}", e));
            }

            this.lstProfiles.Items.Clear();
            this.lstProfiles.Items.AddRange(this.Profiles.ToArray());
            this.LoadProfile(CurrentProfile);
        }

        private void LoadProfile(Profile profile)
        {
            CurrentProfile = profile;
            this.Text = string.Format("VisualME7Logger - {0}", CurrentProfile.Name);

            txtECUFile.Text = CurrentProfile.ECUFile.FilePath;
            LoadECUFile();
            txtConfigFile.Text = CurrentProfile.ConfigFile.FilePath;
            LoadConfigFile();

            lstGraphVariables.DataSource = CurrentProfile.DisplayOptions.GraphVariables;
            lstExpressions.DataSource = CurrentProfile.DisplayOptions.Expressions;
            nudResfreshRate.Value = CurrentProfile.DisplayOptions.RefreshInterval;
            nudGraphResH.Value = CurrentProfile.DisplayOptions.GraphHRes;
            nudGraphResV.Value = CurrentProfile.DisplayOptions.GraphVRes;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                foreach (DataGridViewRow r in this.dataGridView1.SelectedRows)
                {
                    Measurement m = (Measurement)r.DataBoundItem;
                    m.Selected = !m.Selected;
                }
            }
        }

        private void mE7CheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChecksumForm(this.CurrentProfile.ChecksumInfo).ShowDialog(this);
        }

        private void eEPromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EEPromForm form = new EEPromForm(this.CurrentProfile.EEProm);
            if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.CurrentProfile.EEProm = form.EEProm;
            }
        }

        private void saveSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveSettings();
        }

        private void saveConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveConfigFile();
        }
        private void saveConfigFileAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveConfigFile(true);
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new OptionsForm(this.CurrentProfile.LoggerOptions).ShowDialog(this);
            this.SwitchUI();
        }

        private void btnAddGraphVariable_Click(object sender, EventArgs e)
        {
            this.GraphVariableEditMode = EditModes.Add;
            this.SelectedGraphVariable = new GraphVariable();
            LoadSelectedGraphVariable();
            this.SwitchUI();
        }

        private void btnDeleteGraphVariable_Click(object sender, EventArgs e)
        {
            if (this.SelectedGraphVariable != null)
            {
                this.CurrentProfile.DisplayOptions.GraphVariables.Remove(this.SelectedGraphVariable);
                this.lstGraphVariables.DataSource = null;
                this.lstGraphVariables.DataSource = this.CurrentProfile.DisplayOptions.GraphVariables;
            }
        }

        private void btnEditGraphVariable_Click(object sender, EventArgs e)
        {
            if (this.SelectedGraphVariable != null)
            {
                this.GraphVariableEditMode = EditModes.Edit;
                this.SwitchUI();
            }
        }

        private void btnCancelGraphVariable_Click(object sender, EventArgs e)
        {
            if (lstGraphVariables.Items.Count > 0)
                lstGraphVariables.SelectedIndex = 0;
            this.GraphVariableEditMode = EditModes.View;
            this.LoadSelectedGraphVariable();
            this.SwitchUI();
        }

        private void btnSaveGraphVariable_Click(object sender, EventArgs e)
        {
            this.SelectedGraphVariable.Variable = cmbGraphVariableVariable.Text;
            this.SelectedGraphVariable.Name = txtGraphVariableName.Text;
            this.SelectedGraphVariable.Min = nudGraphVariableMin.Value;
            this.SelectedGraphVariable.Max = nudGraphVariableMax.Value;
            this.SelectedGraphVariable.LineColor = txtGraphVariableColor.BackColor;
            this.SelectedGraphVariable.LineThickness = (int)nudGraphVariableThickness.Value;
            this.SelectedGraphVariable.Active = chkGraphVariableActive.Checked;
            this.SelectedGraphVariable.LineStyle = (ChartDashStyle)cmbGraphVariableStyle.SelectedItem;

            if (this.GraphVariableEditMode == EditModes.Add)
                this.CurrentProfile.DisplayOptions.GraphVariables.Add(this.SelectedGraphVariable);
            this.lstGraphVariables.DataSource = null;
            this.lstGraphVariables.DataSource = this.CurrentProfile.DisplayOptions.GraphVariables;

            this.GraphVariableEditMode = EditModes.View;
            this.SwitchUI();
        }

        private void lstGraphVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedGraphVariable = (GraphVariable)lstGraphVariables.SelectedItem;
            this.LoadSelectedGraphVariable();
        }

        void LoadSelectedGraphVariable()
        {
            if (this.SelectedGraphVariable != null)
            {
                cmbGraphVariableVariable.Text = SelectedGraphVariable.Variable;
                txtGraphVariableName.Text = SelectedGraphVariable.Name;
                nudGraphVariableMin.Value = SelectedGraphVariable.Min;
                nudGraphVariableMax.Value = SelectedGraphVariable.Max;
                txtGraphVariableColor.BackColor = SelectedGraphVariable.LineColor;
                nudGraphVariableThickness.Value = SelectedGraphVariable.LineThickness;
                chkGraphVariableActive.Checked = SelectedGraphVariable.Active;
                cmbGraphVariableStyle.SelectedItem = SelectedGraphVariable.LineStyle;
            }
        }

        private void txtGraphVariableColor_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.colorDialog1.ShowDialog())
            {
                this.txtGraphVariableColor.BackColor = this.colorDialog1.Color;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                Measurement m = (Measurement)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                m.Selected = !m.Selected;
                if (radFilterSelected.Checked || radFilterUnselected.Checked)
                {
                    ApplyFilter();
                }
            }
        }

        private void radFilterAll_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void radFilterSelected_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void radFilterUnselected_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cmbGraphVariableVariable_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.GraphVariableEditMode != EditModes.View &&
                this.CurrentProfile.ECUFile != null &&
                this.CurrentProfile.ECUFile.Measurements != null)
            {
                Measurement m = this.CurrentProfile.ECUFile.Measurements[cmbGraphVariableVariable.Text];
                if (m != null)
                {
                    txtGraphVariableName.Text = m.Alias;
                }
            }
        }

        private void btnProfileSetCurrent_Click(object sender, EventArgs e)
        {
            this.LoadProfile((Profile)lstProfiles.SelectedItem);
        }

        private void lstProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedProfile = lstProfiles.SelectedItem as Profile;
            this.txtProfileName.Text = this.SelectedProfile != null ? this.SelectedProfile.Name : string.Empty;
            this.SwitchUI();
        }

        private void btnProfileClone_Click(object sender, EventArgs e)
        {
            Profile clone = ((Profile)lstProfiles.SelectedItem).Clone();
            clone.Name += " Clone";
            while (true)
            {
                if (Profiles.Any(p => p.Name == clone.Name))
                {
                    clone.Name += " Clone";
                }
                else 
                {
                    break;
                }
            }
            Profiles.Add(clone);
            lstProfiles.DataSource = null;
            lstProfiles.DataSource = this.Profiles;
            lstProfiles.SelectedItem = clone;
        }

        private void btnProfileAdd_Click(object sender, EventArgs e)
        {
            this.ProfileEditMode = EditModes.Add;
            SwitchUI();
        }

        private void btnProfileEdit_Click(object sender, EventArgs e)
        {
            this.ProfileEditMode = EditModes.Edit;
            SwitchUI();
        }

        private void btnProfileDelete_Click(object sender, EventArgs e)
        {
            this.Profiles.Remove(this.SelectedProfile);
            if (this.SelectedProfile == this.CurrentProfile)
            {
                if(Profiles.Count > 0)
                {
                    this.CurrentProfile = Profiles[0];
                }
                else
                {
                    this.CurrentProfile = new Profile("Default Profile");
                    this.Profiles.Add(this.CurrentProfile);                  
                }
                SelectedProfile = CurrentProfile;
                this.LoadProfile(CurrentProfile);                          
            }

            this.lstProfiles.DataSource = null;
            this.lstProfiles.DataSource = Profiles;
            this.lstProfiles.SelectedItem = this.SelectedProfile;
        }

        private void btnProfileCancel_Click(object sender, EventArgs e)
        {
            this.ProfileEditMode = EditModes.View;
            this.SwitchUI();
        }

        private void btnProfileSave_Click(object sender, EventArgs e)
        {
            if (this.Profiles.Any(p => p != SelectedProfile && p.Name == txtProfileName.Text))
            {
                MessageBox.Show("Duplicate Profile Name");
                return;
            }

            if (this.ProfileEditMode == EditModes.Add)
            {
                this.SelectedProfile = new Profile(txtProfileName.Text);
                this.Profiles.Add(SelectedProfile);
            }
            this.SelectedProfile.Name = txtProfileName.Text;
            if (this.SelectedProfile == CurrentProfile)
            {
                this.Text = string.Format("VisualME7Logger - {0}", this.CurrentProfile.Name);
            }

            lstProfiles.DataSource = null;
            lstProfiles.DataSource = this.Profiles;
            lstProfiles.SelectedItem = this.SelectedProfile;           

            this.ProfileEditMode = EditModes.View;
            SwitchUI();
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void lstExpressions_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedExpression = lstExpressions.SelectedItem as Session.ExpressionVariable;
            this.txtExpressionName.Text = this.SelectedExpression != null ? this.SelectedExpression.Name : string.Empty;
            this.txtExpressionUnit.Text = this.SelectedExpression != null ? this.SelectedExpression.Unit : string.Empty;
            this.txtExpressionExpression.Text = this.SelectedExpression != null ? this.SelectedExpression.Expression : string.Empty;
            this.SwitchUI();
        }

        private void btnExpressionClone_Click(object sender, EventArgs e)
        {
            Session.ExpressionVariable clone = ((Session.ExpressionVariable)lstExpressions.SelectedItem).Clone();
            clone.Name += " Clone";
            while (true)
            {
                if (this.CurrentProfile.DisplayOptions.Expressions.Any(p => p.Name == clone.Name))
                {
                    clone.Name += " Clone";
                }
                else
                {
                    break;
                }
            }
            this.CurrentProfile.DisplayOptions.Expressions.Add(clone);
            lstExpressions.DataSource = null;
            lstExpressions.DataSource = this.CurrentProfile.DisplayOptions.Expressions;
            lstExpressions.SelectedItem = clone;
        }

        private void btnExpressionAdd_Click(object sender, EventArgs e)
        {
            this.ExpressionEditMode = EditModes.Add;
            SwitchUI();
        }

        private void btnExpressionEdit_Click(object sender, EventArgs e)
        {
            this.ExpressionEditMode = EditModes.Edit;
            SwitchUI();
        }

        private void btnExpressionDelete_Click(object sender, EventArgs e)
        {
            this.CurrentProfile.DisplayOptions.Expressions.Remove(this.SelectedExpression);
            this.lstExpressions.DataSource = null;
            this.lstExpressions.DataSource = this.CurrentProfile.DisplayOptions.Expressions;
            this.lstExpressions.SelectedItem = this.SelectedExpression;
        }

        private void btnExpressionCancel_Click(object sender, EventArgs e)
        {
            this.ExpressionEditMode = EditModes.View;
            this.SwitchUI();
        }

        private void btnExpressionSave_Click(object sender, EventArgs e)
        {
            if (this.CurrentProfile.DisplayOptions.Expressions.Any(p => p != SelectedExpression && p.Name == txtExpressionName.Text))
            {
                MessageBox.Show("Duplicate Expression Name");
                return;
            }

            if (this.CurrentProfile.ECUFile.Measurements.Values.Any(m => m.Name == txtExpressionName.Text))
            {
                MessageBox.Show("Invalid Expression Name");
                return;
            }

            if (this.ExpressionEditMode == EditModes.Add)
            {
                this.SelectedExpression = new Session.ExpressionVariable(txtExpressionName.Text, txtExpressionUnit.Text, txtExpressionExpression.Text);
                this.CurrentProfile.DisplayOptions.Expressions.Add(SelectedExpression);
            }
            this.SelectedExpression.Name = txtExpressionName.Text;
            this.SelectedExpression.Unit = txtExpressionUnit.Text;
            this.SelectedExpression.Expression = txtExpressionExpression.Text;

            lstExpressions.DataSource = null;
            lstExpressions.DataSource = this.CurrentProfile.DisplayOptions.Expressions;
            lstExpressions.SelectedItem = this.SelectedExpression;

            this.ExpressionEditMode = EditModes.View;
            SwitchUI();
        }
    }

    public class Profile
    {
        public string Name { get; set; }
        public ECUFile ECUFile { get; set; }
        public ConfigFile ConfigFile { get; set; }
        public VisualME7Logger.Session.LoggerOptions LoggerOptions { get; set; }
        public VisualME7Logger.Output.ChecksumInfo ChecksumInfo { get; set; }
        public VisualME7Logger.Output.EEProm EEProm { get; set; }
        public DisplayOptions DisplayOptions { get; set; }

        public Profile()
        {
            this.LoggerOptions = new Session.LoggerOptions(Program.ME7LoggerDirectory);
            this.ChecksumInfo = new Output.ChecksumInfo();
            this.EEProm = new Output.EEProm();
            this.DisplayOptions = new DisplayOptions();
            this.ECUFile = new ECUFile(string.Empty);
            this.ConfigFile = new ConfigFile(string.Empty);
        }

        public Profile(string name)
            : this()
        {
            this.Name = name;
        }

        public void Read(XElement ele)
        {
            foreach (XAttribute att in ele.Attributes())
            {
                switch (att.Name.LocalName)
                {
                    case "Name":
                        this.Name = att.Value;
                        break;
                    case "ECUFile":
                        this.ECUFile = new ECUFile(att.Value);
                        break;
                    case "ConfigFile":
                        this.ConfigFile = new ConfigFile(att.Value);
                        break;
                }
            }

            foreach (XElement childEle in ele.Elements())
            {
                switch (childEle.Name.LocalName)
                {
                    case "Options":
                        this.LoggerOptions.Read(childEle);
                        break;
                    case "ChecksumInfo":
                        this.ChecksumInfo.Read(childEle);
                        break;
                    case "EEProm":
                        this.EEProm.Read(childEle);
                        break;
                    case "DisplayOptions":
                        this.DisplayOptions.Read(childEle);
                        break;
                }
            }
        }

        public XElement Write()
        {
            XElement retval = new XElement("Profile");
            retval.Add(new XAttribute("Name", this.Name));
            retval.Add(new XAttribute("ECUFile", this.ECUFile.FilePath));
            retval.Add(new XAttribute("ConfigFile", this.ConfigFile.FilePath));
            retval.Add(this.LoggerOptions.Write());
            retval.Add(this.ChecksumInfo.Write());
            retval.Add(this.EEProm.Write());
            retval.Add(this.DisplayOptions.Write());
            return retval;
        }

        public Profile Clone()
        {
            Profile clone = new Profile();
            clone.Name = this.Name;
            clone.ECUFile = new ECUFile(this.ECUFile.FilePath);
            clone.ConfigFile = new ConfigFile(this.ConfigFile.FilePath);
            clone.LoggerOptions = this.LoggerOptions.Clone();
            clone.ChecksumInfo = this.ChecksumInfo.Clone();
            clone.EEProm = this.EEProm.Clone();
            clone.DisplayOptions = this.DisplayOptions.Clone();
            return clone;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class DisplayOptions
    {
        public int RefreshInterval = 35;
        public int GraphVRes = 1000;
        public int GraphHRes = 1200;
        public List<GraphVariable> GraphVariables = new List<GraphVariable>();
        public List<Session.ExpressionVariable> Expressions = new List<Session.ExpressionVariable>();
       
        public XElement Write()
        {
            XElement retval = new XElement("DisplayOptions");
            
            retval.Add(new XAttribute("RefreshInterval", this.RefreshInterval));
            retval.Add(new XAttribute("GraphVRes", this.GraphVRes));
            retval.Add(new XAttribute("GraphHRes", this.GraphHRes));

            XElement expressionsEle = new XElement("Expressions");
            foreach (var exp in Expressions)
            {
                expressionsEle.Add(exp.Write());
            }
            retval.Add(expressionsEle);

            XElement graphVarsEle = new XElement("GraphVariables");
            foreach (GraphVariable gv in this.GraphVariables)
            {
                graphVarsEle.Add(gv.Write());
            }
            retval.Add(graphVarsEle);
           
            return retval;
        }

        public void Read(XElement ele)
        {
            foreach (XAttribute att in ele.Attributes())
            {
                switch (att.Name.LocalName)
                {
                    case "RefreshInterval":
                        this.RefreshInterval = int.Parse(att.Value);
                        break;
                    case "GraphVRes":
                        this.GraphVRes = int.Parse(att.Value);
                        break;
                    case "GraphHRes":
                        this.GraphHRes = int.Parse(att.Value);
                        break;
                }
            }

            foreach (XElement child in ele.Elements())
            {
                switch (child.Name.LocalName)
                {
                    case "Expressions":
                        this.ReadExpressions(child);
                        break;
                    case "GraphVariables":
                        this.ReadGraphVariables(child);
                        break;
                }
            }
        }

        public DisplayOptions Clone()
        {
            DisplayOptions clone = new DisplayOptions();
            clone.RefreshInterval = this.RefreshInterval;
            clone.GraphVRes = this.GraphVRes;
            clone.GraphHRes = this.GraphHRes;

            clone.Expressions = new List<Session.ExpressionVariable>();
            foreach (var ev in this.Expressions)
            {
                clone.Expressions.Add(ev.Clone());
            }

            clone.GraphVariables = new List<GraphVariable>();
            foreach (GraphVariable gv in this.GraphVariables)
            {
                clone.GraphVariables.Add(gv.Clone());
            }
            return clone;
        }

        private void ReadGraphVariables(XElement ele)
        {
            foreach (XElement e in ele.Elements())
            {
                GraphVariable v = new GraphVariable();
                v.Read(e);
                GraphVariables.Add(v);
            }
        }

        private void ReadExpressions(XElement ele)
        {
            foreach (XElement e in ele.Elements())
            {
                Session.ExpressionVariable v = new Session.ExpressionVariable();
                v.Read(e);
                Expressions.Add(v);
            }
        }
    }

    public class GraphVariable
    {
        public bool Active { get; set; }
        public string Variable { get; set; }
        public string Name { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public Color LineColor { get; set; }
        public int LineThickness { get; set; }
        public ChartDashStyle LineStyle { get; set; }

        public GraphVariable()
        {
            LineColor = Color.Red;
            Min = 0;
            Max = 100;
            Name = "";
            Variable = "";
            LineThickness = 1;
            LineStyle = ChartDashStyle.Solid;
            Active = true;
        }

        public XElement Write()
        {
            XElement retval = new XElement("GraphVariable");
            retval.Add(new XAttribute("Active", this.Active));
            retval.Add(new XAttribute("Variable", this.Variable));
            retval.Add(new XAttribute("Name", this.Name));
            retval.Add(new XAttribute("Min", this.Min));
            retval.Add(new XAttribute("Max", this.Max));
            retval.Add(new XAttribute("LineColor", this.LineColor.ToArgb()));
            retval.Add(new XAttribute("LineThickness", this.LineThickness));
            retval.Add(new XAttribute("LineStyle", (int)this.LineStyle));
            return retval;
        }

        public void Read(XElement ele)
        {
            foreach (XAttribute att in ele.Attributes())
            {
                switch (att.Name.LocalName)
                {
                    case "Active":
                        Active = bool.Parse(att.Value);
                        break;
                    case "Variable":
                        Variable = att.Value;
                        break;
                    case "Name":
                        Name = att.Value;
                        break;
                    case "Min":
                        Min = decimal.Parse(att.Value);
                        break;
                    case "Max":
                        Max = decimal.Parse(att.Value);
                        break;
                    case "LineColor":
                        LineColor = Color.FromArgb(int.Parse(att.Value));
                        break;
                    case "LineThickness":
                        LineThickness = int.Parse(att.Value);
                        break;
                    case "LineStyle":
                        LineStyle = (ChartDashStyle)int.Parse(att.Value);
                        break;
                }
            }
        }

        public GraphVariable Clone()
        {
            GraphVariable clone = new GraphVariable();
            clone.Active = this.Active;
            clone.Variable = this.Variable;
            clone.Name = this.Name;
            clone.Min = this.Min;
            clone.Max = this.Max;
            clone.LineColor = this.LineColor;
            clone.LineThickness = this.LineThickness;
            clone.LineStyle = this.LineStyle;
            return clone;
        }

        public override string ToString()
        {
            return string.Format("{0}, Name: {1}", this.Variable, this.Name);
        }
    }
}