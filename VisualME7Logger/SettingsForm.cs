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

        EditModes GraphVariableEditMode;
        GraphVariable SelectedGraphVariable;

        ECUFile SelectedECUFile { get; set; }
        VisualME7Logger.Session.LoggerOptions LoggerOptions { get; set; }
        VisualME7Logger.Output.ChecksumInfo ChecksumInfo { get; set; }
        DisplayOptions DisplayOptions { get; set; }

        public SettingsForm()
        {
            InitializeComponent();

            SetupGrid();

            this.LoggerOptions = new Session.LoggerOptions(Program.ME7LoggerDirectory);
            this.ChecksumInfo = new Output.ChecksumInfo();
            this.DisplayOptions = new DisplayOptions();

            this.LoadSettings();
            this.SwitchUI();

            this.lstGraphVariables.DataSource = this.DisplayOptions.GraphVariables;
            this.cmbGraphVariableStyle.DataSource = Enum.GetValues(typeof(ChartDashStyle));
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
            if (this.SelectedECUFile != null)
            {
                foreach (Measurement m in this.SelectedECUFile.Measurements.Values)
                {
                    m.Selected = chked;
                }
                this.ApplyFilter();
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
            if (this.SelectedECUFile != null)
            {
                this.txtECUFile.Text = this.SelectedECUFile.FilePath;
                this.loadConfigFileToolStripMenuItem.Enabled =
                this.saveConfigFileToolStripMenuItem.Enabled = 
                this.saveConfigFileAsToolStripMenuItem.Enabled = true;
                this.btnStartLog.Enabled = true;
                LoadConfigFile(txtConfigFile.Text);
            }
        }

        private void SwitchUI()
        {
            this.lstGraphVariables.Enabled =
                this.btnAddGraphVariable.Enabled =
                this.btnEditGraphVariable.Enabled =
                this.btnDeleteGraphVariable.Enabled = this.GraphVariableEditMode == EditModes.View;

            this.gbGraphVariables.Enabled = this.GraphVariableEditMode != EditModes.View;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ApplyFilter()
        {
            if (this.SelectedECUFile != null)
            {
                IEnumerable<Measurement> measurements =
                    this.SelectedECUFile.Measurements.Values.Where(m => !string.IsNullOrEmpty(m.Alias)).OrderBy(m => m.Alias).ThenBy(m => m.Name).Union(
                    this.SelectedECUFile.Measurements.Values.Where(m => string.IsNullOrEmpty(m.Alias)).OrderBy(m => m.Alias).ThenBy(m => m.Name));

                if (!string.IsNullOrEmpty(this.toolStripFilterTextBox.Text))
                {
                    string lookup = this.toolStripFilterTextBox.Text;
                    measurements = measurements.Where(m =>
                        m.Name.IndexOf(lookup, StringComparison.InvariantCultureIgnoreCase) > -1 ||
                        m.Alias.IndexOf(lookup, StringComparison.InvariantCultureIgnoreCase) > -1 ||
                        m.Comment.IndexOf(lookup, StringComparison.InvariantCultureIgnoreCase) > -1);
                }

                if (selectedToolStripMenuItem.Checked)
                {
                    dataGridView1.DataSource = measurements.Where(m => m.Selected).ToList();
                }
                else if (unselectedToolStripMenuItem.Checked)
                {
                    dataGridView1.DataSource = measurements.Where(m => !m.Selected).ToList();
                }
                else
                {
                    dataGridView1.DataSource = measurements.ToList();
                }
            }
        }

        private ConfigFile SaveConfigFile(bool saveNew = false)
        {
            if (this.SelectedECUFile != null)
            {
                Measurements ms = new Measurements();
                foreach (Measurement m in this.SelectedECUFile.Measurements.Values.Where(m => m.Selected))
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

                    ConfigFile configFile = new ConfigFile(this.SelectedECUFile.FileName, ms);
                    configFile.Write(txtConfigFile.Text);
                    return configFile;
                }
                else
                {
                    MessageBox.Show("No Measurements Selected");
                }
            }
            return null;
        }

        private void LoadConfigFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            ConfigFile configFile = new ConfigFile(this.SelectedECUFile.FileName);
            configFile.Read(filePath);
            this.txtConfigFile.Text = filePath;
           
            foreach (Measurement m in this.SelectedECUFile.Measurements.Values)
            {
                if (configFile.Measurements[m.Name] != null)
                {
                    m.Selected = true;
                }
            }
            ApplyFilter();
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

            this.Visible = false;
            Form1 logForm = new Form1(txtConfigFile.Text, this.LoggerOptions, this.DisplayOptions);
            logForm.ShowDialog(this);
            this.Visible = true;
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
            this.DisplayOptions.RefreshInterval = (int)this.nudResfreshRate.Value;
            this.DisplayOptions.GraphHRes = (int)this.nudGraphResH.Value;
            this.DisplayOptions.GraphVRes = (int)this.nudGraphResV.Value;

            try
            {
                XElement root = new XElement("VisualME7LoggerSettings");
                root.Add(new XAttribute("ECUFile", this.txtECUFile.Text));
                root.Add(new XAttribute("ConfigFile", this.txtConfigFile.Text));

                root.Add(this.LoggerOptions.Write());
                root.Add(this.ChecksumInfo.Write());
                root.Add(this.DisplayOptions.Write());
                root.Save(System.IO.Path.Combine(Program.ME7LoggerDirectory, "VisualME7Logger.cfg.xml"));
            }
            catch(Exception e)
            {
                MessageBox.Show(string.Format("An error occurred while saving settings file\r\n{0}", e.ToString()));
            }
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
                        case "DisplayOptions":
                            this.DisplayOptions.Read(ele);
                            break;
                    }
                }
            }

            this.nudResfreshRate.Value = this.DisplayOptions.RefreshInterval;
            this.nudGraphResH.Value = this.DisplayOptions.GraphHRes;
            this.nudGraphResV.Value = this.DisplayOptions.GraphVRes;
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
        private void saveConfigFileAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveConfigFile(true);
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new OptionsForm(this.LoggerOptions).ShowDialog(this);
            this.SwitchUI();
        }

        private void selectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedToolStripMenuItem.Checked = true;
            allToolStripMenuItem.Checked = false;
            unselectedToolStripMenuItem.Checked = false;

            ApplyFilter();
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedToolStripMenuItem.Checked = false;
            allToolStripMenuItem.Checked = true;
            unselectedToolStripMenuItem.Checked = false;

            ApplyFilter();
        }

        private void unselectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedToolStripMenuItem.Checked = false;
            allToolStripMenuItem.Checked = false;
            unselectedToolStripMenuItem.Checked = true;

            ApplyFilter();
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
                this.DisplayOptions.GraphVariables.Remove(this.SelectedGraphVariable);
                this.lstGraphVariables.DataSource = null;
                this.lstGraphVariables.DataSource = this.DisplayOptions.GraphVariables;
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
            this.SwitchUI();
        }

        private void btnSaveGraphVariable_Click(object sender, EventArgs e)
        {
            this.SelectedGraphVariable.Variable = txtGraphVariableVariable.Text;
            this.SelectedGraphVariable.Name = txtGraphVariableName.Text;
            this.SelectedGraphVariable.Min = nudGraphVariableMin.Value;
            this.SelectedGraphVariable.Max = nudGraphVariableMax.Value;
            this.SelectedGraphVariable.LineColor = txtGraphVariableColor.BackColor;
            this.SelectedGraphVariable.LineThickness = (int)nudGraphVariableThickness.Value;
            this.SelectedGraphVariable.Active = chkGraphVariableActive.Checked;
            this.SelectedGraphVariable.LineStyle = (ChartDashStyle)cmbGraphVariableStyle.SelectedItem;

            if (this.GraphVariableEditMode == EditModes.Add)
                this.DisplayOptions.GraphVariables.Add(this.SelectedGraphVariable);
            this.lstGraphVariables.DataSource = null;
            this.lstGraphVariables.DataSource = this.DisplayOptions.GraphVariables;

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
                txtGraphVariableVariable.Text = SelectedGraphVariable.Variable;
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
                if (unselectedToolStripMenuItem.Checked || selectedToolStripMenuItem.Checked)
                {
                    ApplyFilter();
                }
            }
        }

        private void toolStripFilterTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            {
                ApplyFilter();
            }
        }

       
    }

    public class DisplayOptions
    {
        public int RefreshInterval = 35;
        public int GraphVRes = 1000;
        public int GraphHRes = 1200;       
        public List<GraphVariable> GraphVariables = new List<GraphVariable>();
        public XElement Write()
        {
            XElement retval = new XElement("DisplayOptions");
            retval.Add(new XAttribute("RefreshInterval", this.RefreshInterval));
            retval.Add(new XAttribute("GraphVRes", this.GraphVRes));
            retval.Add(new XAttribute("GraphHRes", this.GraphHRes));
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
                    case"GraphVRes":
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
                    case "GraphVariables":
                        this.ReadGraphVariables(child);
                        break;
                }
            }
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

        public override string ToString()
        {
            return string.Format("{0}, Name: {1}", this.Variable, this.Name);
        }
    }
}