using System;
using System.Windows.Forms;

namespace LDRPIDTool
{
    public partial class DataForm : Form
    {
        public DataForm(Settings settings, decimal[][] data)
        {
            InitializeComponent();

            DataGridViewColumn column = new DataGridViewColumn(new DataGridViewTextBoxCell());
            column.Name = "rowheadercol";
            column.Width = 75;
            grdData.Columns.Add(column);
            for (int i = 0; i < settings.KFLDRLDutyCycles.Length; ++i)
            {
                column = new DataGridViewColumn(new DataGridViewTextBoxCell());
                column.Name =
                    column.HeaderText = settings.KFLDRLDutyCycles[i].ToString();
                column.Width = 75;
                grdData.Columns.Add(column);
            }

            grdData.Rows.Add();
            for (int i = 0; i < settings.KFLDRLDutyCycles.Length; ++i)
            {
                grdData.Rows[0].Cells[i + 1].Value = settings.KFLDRLDutyCycles[i];
            }

            for (int i = 0; i < settings.KFLDRLRpms.Length; ++i)
            {
                grdData.Rows.Add();
                grdData.Rows[i + 1].Cells[0].Value = settings.KFLDRLRpms[i];
            }


            for (int i = 0; i < settings.KFLDRLRpms.Length; ++i)
            {
                for (int j = 0; j < settings.KFLDRLDutyCycles.Length; ++j)
                {
                    decimal value = data[i][j];// - settings.ambient;
                    decimal rpm = settings.KFLDRLRpms[i];
                    decimal dutyCycle = settings.KFLDRLDutyCycles[j];

                    int index = 0;
                    for (int k = 0; k < settings.KFLDIMXPressures.Length; ++k)
                    {
                        index = k;
                        if (settings.KFLDIMXPressures[k] >= value)
                        {
                            break;
                        }
                    }

                    decimal zValue = Program.Interpolate(
                        value,
                        settings.KFLDIMXPressures[(index - 1 < 0) ? 0 : index - 1],
                        settings.KFLDIMXDutyCycles[(index - 1 < 0) ? 0 : index - 1],
                        settings.KFLDIMXPressures[index],
                        settings.KFLDIMXDutyCycles[index]
                        );

#if DEBUG
                    Console.WriteLine(string.Format("rpm {0}, dc {1}, value {2}", rpm, dutyCycle, value));

                    Console.WriteLine(string.Format("mbar: {0}, pres0: {1}, dc0: {2}, pres1: {3}, dc1: {4} = value: {5}",
                      value,
                      settings.KFLDIMXPressures[(index - 1 < 0) ? 0 : index - 1],
                      settings.KFLDIMXDutyCycles[(index - 1 < 0) ? 0 : index - 1],
                      settings.KFLDIMXPressures[index],
                       settings.KFLDIMXDutyCycles[index],
                       zValue
                      ));
#endif

                    if (zValue <= 0)
                    {
                        zValue = dutyCycle;
                    }

                    grdData.Rows[i + 1].Cells[j + 1].Value = zValue.ToString("0.000");
                }
            }
        }     
    }
}
