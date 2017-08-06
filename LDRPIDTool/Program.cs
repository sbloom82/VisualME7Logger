using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDRPIDTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        public static decimal Interpolate(decimal x, decimal x0, decimal y0, decimal x1, decimal y1)
        {
            if ((x1 - x0) == 0)
            {
                return (y0 + y1) / 2;
            }
            return y0 + (x - x0) * (y1 - y0) / (x1 - x0);
        }
    }

    public class DataPoint
    {
        public decimal rpm;
        public decimal actualPresure;
        public decimal absolutePressure { get { return actualPresure - baroPressure; } }
        public decimal dutyCycle;
        public decimal baroPressure = 1000;
        public decimal timestamp;

        public override string ToString()
        {
            return string.Format("{0}rpm - {1}mbar", rpm, absolutePressure);
        }
    }

    public class DataPointCollection
    {
        public DataPointCollection(Settings settings)
        {
            this.settings = settings;
        }

        Settings settings;
        List<DataPoint> dataPoints = new List<DataPoint>();

        int? dutyCycle;
        public int DutyCycle
        {
            get
            {
                if (dutyCycle == null && dataPoints.Count > 0)
                {
                    //this is dumb, the dutyCycle should be what the majority of the dp's duty is
                    // when you stomp the pedal, dutycycle may not immediately be your fixed duty cycle.
                    dutyCycle = (int)Math.Round(dataPoints[Count / 2].dutyCycle);
                }
                return dutyCycle.Value;
            }
        }

        decimal? dutyCycleActual;
        private decimal DutyCycleActual
        {
            get
            {
                if (dutyCycleActual == null && dataPoints.Count > 0)
                {
                    //this is dumb, the dutyCycle should be what the majority of the dp's duty is
                    // when you stomp the pedal, dutycycle may not immediately be your fixed duty cycle.
                    dutyCycleActual = dataPoints[Count / 2].dutyCycle;
                }
                return dutyCycleActual.Value;
            }
        }

        public int Count { get { return dataPoints.Count; } }

        public void Add(DataPoint dataPoint)
        {
            this.dataPoints.Add(dataPoint);
        }

        private List<DataPoint> filteredPoints;
        public List<DataPoint> FilteredPoints
        {
            get
            {
                if (filteredPoints == null)
                {
                    filteredPoints = GetFilteredPoints();
                }
                return filteredPoints;
            }
        }

        List<DataPoint> GetFilteredPoints()
        {
            List<DataPoint> retval = new List<DataPoint>();
            foreach (DataPointCollection range in Ranges)
            {
                //detect spool up points in this range and filter those out.
                List<DataPoint> window = new List<DataPoint>();
                List<DataPoint> filteredRange = new List<DataPoint>();
                foreach (DataPoint dp in range.dataPoints)
                {
                    window.Add(dp);

                    DataPoint firstInWindow = window[0];
                    if (dp.timestamp - firstInWindow.timestamp > settings.RangeFilter.seconds)
                    {
                        window.Remove(firstInWindow);
                        if (Math.Abs(dp.absolutePressure - firstInWindow.absolutePressure) < settings.RangeFilter.mbar)
                        {
                            filteredRange.Add(firstInWindow);
                        }
                    }
                }
                filteredRange.AddRange(window);
                if (filteredRange[filteredRange.Count - 1].rpm - filteredRange[0].rpm >= settings.RangeFilter.rpmRangeLengthMin)
                {
                    retval.AddRange(filteredRange);
                }

            }
            return retval;
        }

        List<DataPointCollection> ranges = null;
        List<DataPointCollection> Ranges
        {
            get
            {
                if (ranges == null)
                {
                    ranges = this.GetRanges();
                }
                return ranges;
            }
        }

        List<DataPointCollection> GetRanges()
        {
            List<DataPointCollection> ranges = new List<DataPointCollection>();
            DataPoint last = null;
            DataPointCollection range = null;
            for (int i = 0; i < dataPoints.Count; ++i)
            {
                DataPoint current = dataPoints[i];
                if (current.dutyCycle != this.DutyCycleActual)
                    continue;

                if (range == null ||
                    current.timestamp - last.timestamp > .25m) //probably should do this a better way
                {
                    range = new DataPointCollection(settings);
                    ranges.Add(range);
                }
                range.Add(current);
                last = current;
            }
            return ranges;
        }
    }

    public class Settings
    {
        public bool InterpolateBlankCells = true;
        public decimal ambient = 1000;

        public RangeFilter RangeFilter = new RangeFilter();

        public int[] KFLDRLDutyCycles = new int[]
        {
            0,
            10,
            20,
            30,

            40,
            50,
            60,
            70,

            80,
            95
        };

        public int[] KFLDRLRpms = new int[]
        {
            1000,
            1500,
            2000,
            2500,

            2750,
            3000,
            3250,
            3500,

            3750,
            4000,
            4500,
            5000,

            5500,
            6000,
            6500,
            6800,
        };

        public int[] KFLDIMXPressures = new int[]
        {
            0,
            400,
            800,
            1200,

            1400,
            1600,
            1800,
            2000
        };

        public int[] KFLDIMXDutyCycles = new int[]
        {
            0,
            18,
            36,
            54,

            63,
            72,
            81,
            90
        };
    }

    public class RangeFilter
    {
        public decimal rpmRangeLengthMin = 2500;
        public decimal seconds = .1m;
        public decimal mbar = 75m;
    }


    public class SlightlyBetterDataGridView : DataGridView
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Control && e.KeyCode == Keys.V)
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                int row = this.CurrentCell.RowIndex;
                int col = this.CurrentCell.ColumnIndex;
                foreach (string line in lines)
                {
                    string[] cells = line.Split('\t');
                    int cellsSelected = cells.Length;
                    if (row < this.Rows.Count)
                    {
                        for (int i = 0; i < cellsSelected; i++)
                        {
                            if (col + i < this.Columns.Count)
                                this[col + i, row].Value = cells[i];
                            else
                                break;
                        }
                        row++;
                    }
                    else
                    {
                        break;
                    }
                }
            }            
        }

        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            base.OnCellEndEdit(e);
            if (this.SelectedCells.Count > 0)
            {
                object value = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                foreach (DataGridViewCell v in this.SelectedCells)
                {
                    v.Value = value;
                }
            }
        }
    }
}
