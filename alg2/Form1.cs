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

namespace alg2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            chart2.Series[0].Name = "Deykstra 3-heap";
            chart2.Series.Add("Ford-Bellman");
            chart2.Series[0].Enabled = false;
            chart2.Series[1].Enabled = false;


            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart2.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart2.Series[0].Enabled = true;
            chart2.Series[1].Enabled = true;
            chart2.Series[0].Color = Color.Red;
            chart2.Series[1].Color = Color.DarkBlue;

            if (comboBox2.Text == "№2")
                Result2();
            else if (comboBox2.Text == "№1")
                Result1();
            else ResultCustom();
        }
        public void Result2()
        {
         

            Graph graph = new Graph();
            List<int>[] fullTime = graph.getResult2();
            List<int> time1 = fullTime[0];
            List<int> time2 = fullTime[1];
            int[] y = new int[time1.Count];
            int[] y1 = new int[time2.Count];


            chart2.Series[0].Points.DataBindXY(y, time1);
            chart2.Series[1].Points.DataBindXY(y1, time2);
            
        }
        public void Result1()
        {
            
            Graph graph = new Graph();
            List<int>[] fullTime = graph.getResult1();
            List<int> time1 = fullTime[0];
            List<int> time2 = fullTime[1];
            int[] y = new int[time1.Count];
            int[] y1 = new int[time2.Count];

            chart2.Series[0].Points.DataBindXY(y, time1);
            chart2.Series[1].Points.DataBindXY(y1, time2);
 

        }
        
            public void ResultCustom()
        {
            Graph graph = new Graph();
            chart2.Series[0].Enabled = false;
            chart2.Series[1].Enabled = false;

            int m = Convert.ToInt32(textBox4.Text, 10);
            int n = Convert.ToInt32(textBox1.Text, 10);
            int q = Convert.ToInt32(textBox3.Text, 10);
            int r = Convert.ToInt32(textBox2.Text, 10);

            int dey = graph.CustomDeykstra(n, m, q, r);
            int bel = graph.CustomBellman(n, m, q, r);

            label6.Enabled = true;
            label7.Enabled = true;
            label6.Text = "Время работы беллмана: " + bel + " ms";
            label7.Text = "Время работы дейкстры: " + dey + " ms";


        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "Эксперименты")
            {
                comboBox2.Enabled = true;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                label2.Enabled = false;
                label3.Enabled = false;
                label4.Enabled = false;
                label5.Enabled = false;
            }
            else
            {
                comboBox2.Enabled = false;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                label2.Enabled = true;
                label3.Enabled = true;
                label4.Enabled = true;
                label1.Enabled = true;
            }
        }



        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text!="")
            {
                button1.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
