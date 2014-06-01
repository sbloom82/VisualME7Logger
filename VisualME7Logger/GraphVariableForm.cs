using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace VisualME7Logger
{
    public partial class GraphVariableForm : Form
    {
        private GraphVariable GraphVariable { get; set; }
        public GraphVariableForm(GraphVariable variable)
        {
            InitializeComponent();
            this.cmbGraphVariableStyle.DataSource = Enum.GetValues(typeof(ChartDashStyle));
            this.GraphVariable = variable;
            this.LoadVariable();
            this.SwitchUI();
        }

        private void LoadVariable()
        {
            txtGraphVariable.Text = GraphVariable.Variable;
            txtGraphVariableName.Text = GraphVariable.Name;
            nudGraphVariableMin.Value = GraphVariable.Min;
            nudGraphVariableMax.Value = GraphVariable.Max;
            txtGraphVariableColor.BackColor = GraphVariable.LineColor;
            nudGraphVariableThickness.Value = GraphVariable.LineThickness;
            chkGraphVariableActive.Checked = true;// GraphVariable.Active;
            cmbGraphVariableStyle.SelectedItem = GraphVariable.LineStyle;            
        }

        private void SaveVariable()
        {
            GraphVariable.Name = txtGraphVariableName.Text;
            GraphVariable.Min = nudGraphVariableMin.Value;
            GraphVariable.Max = nudGraphVariableMax.Value;
            GraphVariable.LineColor = txtGraphVariableColor.BackColor;
            GraphVariable.LineThickness = (int)nudGraphVariableThickness.Value;
            GraphVariable.Active = chkGraphVariableActive.Checked;
            GraphVariable.LineStyle = (ChartDashStyle)cmbGraphVariableStyle.SelectedItem;
        }

        private void SwitchUI()
        { }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SaveVariable();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void txtGraphVariableColor_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.colorDialog1.ShowDialog())
            {
                this.txtGraphVariableColor.BackColor = this.colorDialog1.Color;
            }
        }
    }
}
