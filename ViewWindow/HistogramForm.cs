using BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace ViewWindow
{
    public partial class HistogramForm : Form
    {
        public HistogramForm()
        {
            InitializeComponent();
            this.Text = "Распределение по специальностям";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;            
        }
 
        public void ShowHistogram(BL logic)
        {
            GraphPane pane = new GraphPane();
            pane.CurveList.Clear();

            pane.AddBar("Количество студентов", null, logic.DistributionByScpecialities(), 
                Color.Blue);

            pane.XAxis.Type = AxisType.Text;

            pane.XAxis.Scale.TextLabels = logic.GetSpecialities();

            zedGraphControl1.GraphPane = pane;
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            Invalidate();
        }
    }
}
