using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series[0].Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graph graphA = new Graph();

             List<int> x = graphA.FirstExperimentForFirstMethod();
             int[] y = new int[x.Count];
             for (int i = 0; i < x.Count; i++)
             {
                 y[i] = i;
             }
            List<int> x1 = graphA.FirstExperimentForSecondMethod();
            int[] y1 = new int[x1.Count];
            for (int i = 0; i < x1.Count; i++)
            {
                y1[i] = i;
            }
            chart1.Series[0].Name = "Boruvka";
            chart1.Series.Add("Kruskal");
           chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].Color = Color.DarkBlue;
            chart1.Series[0].Color = Color.Red;
          chart1.Series[0].Points.DataBindXY(y, x);
           chart1.Series[1].Points.DataBindXY(y1, x1);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graph graphA = new Graph();

            List<int> x = graphA.SecondExperimentForFirstMethod();
            int[] y = new int[x.Count];
            for (int i = 0; i < x.Count; i++)
            {
                y[i] = i;
            }
            List<int> x1 = graphA.SecondExperimentForSecondMethod();
            int[] y1 = new int[x1.Count];
            for (int i = 0; i < x1.Count; i++)
            {
                y1[i] = i;
            }
            chart1.Series[0].Name = "Boruvka";
            chart1.Series.Add("Kruskal");
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].Color = Color.DarkBlue;
            chart1.Series[0].Color = Color.Red;
            chart1.Series[0].Points.DataBindXY(y, x);
            chart1.Series[1].Points.DataBindXY(y1, x1);
        }
    }
}
