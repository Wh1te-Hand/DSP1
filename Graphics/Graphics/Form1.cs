﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Schema;

namespace Graphics
{
    public partial class Form1 : Form
    {
        //       private double A, N, F, f, N_number, d;
        /*        private double As, Ns, Fs, fs;
                private double At, Nt, Ft, ft;
                private double Ar, Nr, Fr, fr, dr;*/
        private double n, y, var, N_number;
        /*        private byte A_limit, N_limit, F_limit, f_limit;*/
        private Boolean old_mode = false;
        private Boolean flag_s, flag_t, flag_r;
        private const double POROG = (double)1 / 2;
        private const double FREQUENCY_OF_DOT = 20;
        private Sinus sinus, sinus_start;
        private Rectangle rectangle, rectangle_start;
        private Triangle triangle, triangle_start;


        public Form1()
        {
            InitializeComponent();
            Default_params();
            draw_all();

        }
        private void sinus_sum_CheckedChanged(object sender, EventArgs e)
        {
            flag_s = !flag_s;
            chart_summary_draw();
            if (flag_s == false)
            {
                remove_sinus_all();
            }
            else
            {
                change_sinus_all();

            }
        }

        private void triangle_sum_CheckedChanged(object sender, EventArgs e)
        {
            flag_t = !flag_t;
            chart_summary_draw();
            if (flag_t == false)
            {
                remove_triangle_all();
            }
            else
            {
                change_triangle_all();

            }
        }

        private void rectangle_sum_CheckedChanged(object sender, EventArgs e)
        {
            flag_r = !flag_r;
            chart_summary_draw();
            if (flag_r == false)
            {
                remove_rectangle_all();
            }
            else
            {
                change_rectangle_all();
            }
        }


        public void remove_sinus_all()
        {
            this.chart_all.Series[0].Points.Clear();
        }


        public void change_sinus_all()
        {
            // n = POROG * Ns * (-1);
            n = POROG * sinus.Length * (-1);
            this.chart_all.Series[0].Points.Clear();
            // double h = Ns / (Fs * FREQUENCY_OF_DOT);
            double h = sinus.Length / (sinus.Frequency * FREQUENCY_OF_DOT);
            while (n <= POROG * sinus.Length)
            {
                y = sinus.Generate(n);
                this.chart_all.Series[0].Points.AddXY(n, y);
                n += h;
            }
        }

        private void textBox_Asum_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_Asum_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_Asum_sinus.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    sinus.Amplitude = N_number;
                    chart_summary_draw();
                    draw_all_all();
                }
            }
            else
            {
                this.chart_all.Series[0].Points.Clear();
                this.chart_summary.Series[0].Points.Clear();
            }
        }

        private void textBox_Fsum_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_Fsum_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_Fsum_sinus.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    sinus.Frequency = N_number;
                    chart_summary_draw();
                    draw_all_all();

                }
            }
            else
            {
                this.chart_all.Series[0].Points.Clear();
                this.chart_summary.Series[0].Points.Clear();
            }
        }




        private void trackBar_fsinus_ValueChanged(object sender, EventArgs e)
        {
            sinus.Phase = (double)trackBar_fsinus.Value / trackBar_fsinus.Maximum * (2 * (double)(Math.PI));
            chart_summary_draw();
            draw_all_all();
        }

        private void trackBar_ftriangle_ValueChanged(object sender, EventArgs e)
        {
            triangle.Phase = (double)trackBar_ftriangle.Value / trackBar_ftriangle.Maximum * (2 * (double)(Math.PI));
            chart_summary_draw();
            draw_all_all();
        }

        private void trackBar_frectangle_ValueChanged(object sender, EventArgs e)
        {
            rectangle.Phase = (double)trackBar_frectangle.Value / trackBar_frectangle.Maximum * (2 * (double)(Math.PI));
            chart_summary_draw();
            draw_all_all();
        }

        private void trackBar_dSum_ValueChanged(object sender, EventArgs e)
        {
            rectangle.Duty = (double)trackBar_dSum.Value / trackBar_dSum.Maximum;
            chart_summary_draw();
            draw_all_all();
        }
        private void textBox_Nsum_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_Nsum_KeyUp_1(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_Nsum.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    sinus.Length = N_number;
                    triangle.Length = N_number;
                    rectangle.Length = N_number;
                    chart_summary_draw();
                    draw_all_all();

                }
            }
            else
            {
                for (int i = 0; i < this.chart_sinus.Series.Count; i++)
                    this.chart_all.Series[i].Points.Clear();
                this.chart_summary.Series[0].Points.Clear();
            }
        }

        private void textBox_Asum_triangle_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_Asum_triangle_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_Asum_triangle.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    triangle.Amplitude = N_number;
                    chart_summary_draw();
                    draw_all_all();

                }
            }
            else
            {
                this.chart_all.Series[1].Points.Clear();
                this.chart_summary.Series[0].Points.Clear();
            }
        }

        private void textBox_Fsum_triangle_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_Fsum_triangle_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_Fsum_triangle.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    triangle.Frequency = N_number;
                    chart_summary_draw();
                    draw_all_all();

                }
            }
            else
            {
                this.chart_all.Series[1].Points.Clear();
                this.chart_summary.Series[0].Points.Clear();
            }
        }

        private void textBox_Asum_rectangle_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_Asum_rectangle_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_Asum_rectangle.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    rectangle.Amplitude = N_number;
                    chart_summary_draw();
                    draw_all_all();
                }
            }
            else
            {
                this.chart_all.Series[2].Points.Clear();
                this.chart_summary.Series[0].Points.Clear();
            }
        }

        private void textBox_Fsum_rectangle_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_Fsum_rectangle_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_Fsum_rectangle.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    rectangle.Frequency = N_number;
                    chart_summary_draw();
                    draw_all_all();
                }
            }
            else
            {
                this.chart_all.Series[2].Points.Clear();
                this.chart_summary.Series[0].Points.Clear();
            }
        }



        public void remove_triangle_all() {
            this.chart_all.Series[1].Points.Clear();
        }

        public void change_triangle_all()
        {
            //n = POROG * Nt * (-1);
            n = POROG * triangle.Length * (-1);
            this.chart_all.Series[1].Points.Clear();
            // double h = Nt / (Ft * FREQUENCY_OF_DOT);
            double h = triangle.Length / (triangle.Frequency * FREQUENCY_OF_DOT);
            while (n <= POROG * triangle.Length)
            {
                y = triangle.Generate(n);
                this.chart_all.Series[1].Points.AddXY(n, y);
                n += h;
            }
        }

        public void remove_rectangle_all()
        {
            this.chart_all.Series[2].Points.Clear();
        }


        public void change_rectangle_all()
        {
            //n = POROG * Nr * (-1);
            n = POROG * rectangle.Length * (-1);
            this.chart_all.Series[2].Points.Clear();
            // double h = Nr / (Fr * FREQUENCY_OF_DOT);
            double h = rectangle.Length / (rectangle.Frequency * FREQUENCY_OF_DOT);
            while (n <= POROG * rectangle.Length)
            {
                y = rectangle.Generate(n);
                this.chart_all.Series[2].Points.AddXY(n, y);
                n += h;
            }
        }
        private void checkBox_sinus_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_sinus.Checked)
            //  if (this.tabControl_graphic.TabPages[0].Controls.OfType<CheckBox>().First().Checked)
            {
                change_sinus();
            }
            else
            {
                clear_sinus();
            }
        }

        private void checkBox_triangle_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox_triangle.Checked)
            {
                change_triangle();
            }
            else
            {
                clear_triangle();
            }

        }

        private void checkBox_rectangle_CheckedChanged(object sender, EventArgs e)
        {

            if (this.checkBox_rectangle.Checked)
            {
                change_rectangle();
                this.trackBar_d.Enabled = true;
            }
            else
            {
                clear_rectangle();
                this.trackBar_d.Enabled = false;
            }
        }

        private void textBox_N_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
            /*  else {
                  if (Char.IsDigit(number))
                  {
                      line = String.Concat(this.textBox_N.Text, e.KeyChar);
                      N_number = double.Parse(line);
                      if (N_number > 0 && N_number != double.NaN)
                      {
                          N = N_number;
                          draw_all();
                      }
                  }
                  else{ 
                  int count=this.textBox_N.Text.Length;
                      for (int i = 0; i < count - 1; i++){
                          line= String.Concat(line, this.textBox_N.Text[i]);
                      }
                  }
                  N_number = double.Parse(line);
                  if (N_number > 0 && N_number != double.NaN)
                  {
                      N = N_number;
                      draw_all();
                  }
              }*/
        }

        private void textBox_N_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_N.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    sinus_start.Length = N_number;
                    triangle_start.Length = N_number;
                    rectangle_start.Length = N_number;
                    draw_all();
                }
            }
            else
            {
                for (int i = 0; i < this.chart_sinus.Series.Count; i++)
                    this.chart_sinus.Series[i].Points.Clear();
            }

        }

        private void trackBar_A_ValueChanged(object sender, EventArgs e)
        {
            sinus_start.Amplitude = trackBar_A.Value;
            triangle_start.Amplitude = trackBar_A.Value;
            rectangle_start.Amplitude = trackBar_A.Value;
            draw_all();

        }


        private void trackBar_F_ValueChanged(object sender, EventArgs e)
        {
            sinus_start.Frequency = trackBar_F.Value;
            triangle_start.Frequency = trackBar_F.Value;
            rectangle_start.Frequency = trackBar_F.Value;
            draw_all();
        }

        private void trackBar_fo_ValueChanged(object sender, EventArgs e)
        {
            sinus_start.Phase = (double)trackBar_fo.Value / trackBar_fo.Maximum * (2 * (double)(Math.PI));
            triangle_start.Phase = sinus_start.Phase;
            rectangle_start.Phase = sinus_start.Phase;
            draw_all();
        }

        private void trackBar_d_ValueChanged(object sender, EventArgs e)
        {
            rectangle_start.Duty = (double)trackBar_d.Value / trackBar_d.Maximum;
            draw_all();
        }


        private void change_sinus()
        {
            n = POROG * sinus_start.Length * (-1);
            this.chart_sinus.Series[0].Points.Clear();
            if (old_mode) {
                while (n <= POROG * sinus_start.Length)
                {
                    y = sinus_start.Generate(n);
                    this.chart_sinus.Series[0].Points.AddXY(n, y);
                    n++;
                }
            }
            else {
                double h = sinus_start.Length / (sinus_start.Frequency * FREQUENCY_OF_DOT);
                while (n <= POROG * sinus_start.Length)
                {
                    y = sinus_start.Generate(n);
                    this.chart_sinus.Series[0].Points.AddXY(n, y);
                    n += h;
                }
            }

        }

        private void clear_sinus() {
            this.chart_sinus.Series[0].Points.Clear();
        }

        private void change_triangle() {
            n = POROG * triangle_start.Length * (-1);
            this.chart_sinus.Series[1].Points.Clear();
            if (old_mode)
            {
                while (n <= POROG * triangle_start.Length)
                {
                    y = triangle_start.Generate(n);
                    this.chart_sinus.Series[1].Points.AddXY(n, y);
                    n++;
                }
            }
            else {
                double h = triangle_start.Length / (triangle_start.Frequency * FREQUENCY_OF_DOT);
                while (n <= POROG * triangle_start.Length)
                {
                    y = triangle_start.Generate(n);
                    this.chart_sinus.Series[1].Points.AddXY(n, y);
                    n += h;
                }
            }

        }

        private void checkBox_old_mode_CheckedChanged(object sender, EventArgs e)
        {
            old_mode = this.checkBox_old_mode.Checked ? true : false;
            draw_all();
        }

        private void clear_triangle() {
            this.chart_sinus.Series[1].Points.Clear();
        }

        private void change_rectangle()
        {
            n = POROG * rectangle_start.Length * (-1);
            this.chart_sinus.Series[2].Points.Clear();
            if (old_mode) {
                while (n <= POROG * rectangle_start.Length)
                {
                    // var = (double)((Math.Abs((2 * (double)(Math.PI) * F * (double)(n / N) + f)) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

                    y = rectangle_start.Generate(n);
                    this.chart_sinus.Series[2].Points.AddXY(n, y);
                    n++;
                }
            }
            else {
                double h = rectangle_start.Length / (rectangle_start.Frequency * FREQUENCY_OF_DOT);
                while (n <= POROG * rectangle_start.Length)
                {
                    y = rectangle_start.Generate(n);
                    this.chart_sinus.Series[2].Points.AddXY(n, y);
                    n += h;
                }
            }
        }

        private void clear_rectangle() {
            this.chart_sinus.Series[2].Points.Clear();
        }

        private void draw_all() {

            if (this.checkBox_sinus.Checked)
            {
                change_sinus();
            }
            if (this.checkBox_triangle.Checked)
            {
                change_triangle();
            }
            if (this.checkBox_rectangle.Checked)
            {
                change_rectangle();
            }
        }

        private void draw_all_all() {
            if (flag_s)
            {
                change_sinus_all();
            }
            if (flag_t)
            {
                change_triangle_all();
            }
            if (flag_r)
            {
                change_rectangle_all();
            }
        }

        private void chart_summary_draw() {
            double F_max = 0;
            this.chart_summary.Series[0].Points.Clear();
            F_max = find_max_frequense(sinus.Frequency, triangle.Frequency, rectangle.Frequency);
            n = POROG * sinus.Length * (-1);
            double h = sinus.Length / (F_max * FREQUENCY_OF_DOT);
            while (n <= POROG * sinus.Length)
            {
                double y_max = 0;
                if (flag_s) {
                    y_max += sinus.Generate(n);
                }
                if (flag_t) {
                    y_max += triangle.Generate(n);
                }
                if (flag_r) {
                    y_max += rectangle.Generate(n);
                }
                this.chart_summary.Series[0].Points.AddXY(n, y_max);
                n += h;
            }

        }

        private double find_max_frequense(double Fs, double Ft, double Fr) {
            if (flag_r && !flag_s && !flag_t)
            {
                return Fr;
            }
            else if (!flag_r && flag_s && !flag_t)
            {
                return Fs;
            }
            else if (!flag_r && !flag_s && flag_t)
            {
                return Ft;
            }
            else if (flag_r && flag_s && !flag_t)
            {
                if (Fr >= Fs)
                { return Fr; }
                else
                { return Fs; }
            }
            else if (flag_r && !flag_s && flag_t)
            {
                if (Fr >= Ft)
                { return Fr; }
                else
                { return Ft; }
            }
            else if (!flag_r && flag_s && flag_t)
            {
                if (Fs >= Ft)
                { return Fs; }
                else
                { return Ft; }
            }
            else if (flag_r && flag_s && flag_t)
            {
                if ((Fs >= Ft) && (Fs >= Fr))
                { return Fs; }
                else if ((Ft >= Fs) && (Ft >= Fr))
                { return Ft; }
                else
                { return Fr; }
            }
            else
            { return 0; }

        }
        private void Default_params() {
            /*            A = 5;
                        F = 5;
                        d = 0.5;
                        f = (double)5*(2 * (double)(Math.PI))/20;
                        N = 500;
                        Ar = 5;
                        Fr = 5;
                        dr = 0.5;
                        fr = (double)5 * (2 * (double)(Math.PI)) / 20;
                        Nr = 500;
                        At = 5;
                        Ft = 5;
                        ft = (double)5 * (2 * (double)(Math.PI)) / 20;
                        Nt = 500;*/

            sinus = new Sinus(5, 5, (double)5 * (2 * (double)(Math.PI)) / 20, 500);
            triangle = new Triangle(5, 5, (double)5 * (2 * (double)(Math.PI)) / 20, 500);
            rectangle = new Rectangle(5, 5, (double)5 * (2 * (double)(Math.PI)) / 20, 500, 0.5);

            sinus_start = new Sinus(5, 5, (double)5 * (2 * (double)(Math.PI)) / 20, 500);
            triangle_start = new Triangle(5, 5, (double)5 * (2 * (double)(Math.PI)) / 20, 500);
            rectangle_start = new Rectangle(5, 5, (double)5 * (2 * (double)(Math.PI)) / 20, 500, 0.5);

            flag_s = false;
            flag_t = false;
            flag_r = false;

            chart_lab2_common.ChartAreas[0].AxisX.Minimum = 0;
            chart_lab2_common.ChartAreas[0].AxisX.Maximum = 1;

            chart_lab2_summary.ChartAreas[0].AxisX.Minimum = 0;
            chart_lab2_summary.ChartAreas[0].AxisX.Maximum = 1;

            chart_lab2_spectrums.ChartAreas[0].AxisX.Minimum = 0;
        }

        //------------------------------------------------DSP2_part---------------------------------------------------------
        private Dictionary<string, Sinus> sinus_collection = new Dictionary<string, Sinus>();
        private Dictionary<string, Triangle> triangle_collection = new Dictionary<string, Triangle>();
        private Dictionary<string, Rectangle> rectangle_collection = new Dictionary<string, Rectangle>();
        private List<PointF> sinusList = new List<PointF>();
        private List<PointF> triangleList = new List<PointF>();
        private List<PointF> rectangleList = new List<PointF>();

        private string current;
        private int currentSignal; //0-sinus  1-triangle  2-rectangle
        private Boolean theFirst = false, isRectangle = false;
        int lab2_k = 0;
        private int standart = 0;
        private Sinus currentSinus;
        private Triangle currentTriangle;
        private Rectangle currentRectangle;
        private void button_add_chart_Click(object sender, EventArgs e)
        {
            current = this.textBox_chart_name.Text.Trim();
            if ((sinus_collection.ContainsKey(current))|| (triangle_collection.ContainsKey(current))|| (rectangle_collection.ContainsKey(current)))
            {
                current = ($"defaultSignal{standart}");
                standart++;
            }
            this.comboBox_select_chart.Text = current;
            this.comboBox_select_chart.Items.Add(current);
            Sinus sinus = new Sinus();
            sinus_collection.Add(current, sinus);
            currentSinus = sinus;
            theFirst = true;
            select_and_update();
            theFirst = false;
            this.chart_lab2_common.Series.Add(current);
            this.chart_lab2_common.Series[current].ChartType=SeriesChartType.Spline;
            this.chart_lab2_common.Series[current].BorderWidth = 2;
            this.chart_lab2_common.Legends.Add(current);
            updateParametrs(current);
            drawSignal(current);
        }
        private void button_delete_chart_Click(object sender, EventArgs e)
        {
            if (sinus_collection.TryGetValue(current, out currentSinus))
            {
                sinus_collection.Remove(current);
            }
            else if (triangle_collection.TryGetValue(current, out currentTriangle))
            {
                triangle_collection.Remove(current);
            }
            else if (rectangle_collection.TryGetValue(current, out currentRectangle))
            {
                rectangle_collection.Remove(current);
            }
            this.comboBox_select_chart.Items.Remove(current);
            this.label_chart_current_name.Text = "";
            theFirst = true;
            this.comboBox_select_type.SelectedIndex = 3;
            theFirst = false;
            this.chart_lab2_common.Series.Remove(chart_lab2_common.Series.FindByName(current));
            UpdateSum(0);
        }
        private void select_and_update() {
            this.label_chart_current_name.Text = current;
            if (sinus_collection.TryGetValue(current, out currentSinus))
            {
                this.comboBox_select_type.SelectedIndex = 0;
                trackBar_lab2_d.Enabled = false;
            }
            else if (triangle_collection.TryGetValue(current, out currentTriangle))
            {
                this.comboBox_select_type.SelectedIndex = 1;
                trackBar_lab2_d.Enabled = false;
            }
            else if (rectangle_collection.TryGetValue(current, out currentRectangle))
            {
                this.comboBox_select_type.SelectedIndex = 2;
                trackBar_lab2_d.Enabled = true;
            }
        }

        private void comboBox_select_chart_SelectedIndexChanged(object sender, EventArgs e)
        {
            current = this.comboBox_select_chart.SelectedItem.ToString();
            theFirst = true;
            select_and_update();
            theFirst = false;
            updateParametrs(current);
            drawSignal(current);
        }

        private void comboBox_select_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (theFirst)
            { }
            else
            {
                if (this.comboBox_select_type.SelectedIndex != 3) { 
                trackBar_lab2_d.Enabled = false;
                if (sinus_collection.TryGetValue(current, out currentSinus))
                {
                    if (this.comboBox_select_type.SelectedIndex == 1)
                    {
                        Triangle triangle = new Triangle();
                        triangle.Amplitude = currentSinus.Amplitude;
                        triangle.Frequency = currentSinus.Frequency;
                        triangle.Phase = currentSinus.Phase;
                        triangle.Length = currentSinus.Length;
                        triangle_collection.Add(current, triangle);
                        currentTriangle = triangle;
                    }
                    else if (this.comboBox_select_type.SelectedIndex == 2)
                    {
                        Rectangle rectangle = new Rectangle();
                        rectangle.Amplitude = currentSinus.Amplitude;
                        rectangle.Frequency = currentSinus.Frequency;
                        rectangle.Phase = currentSinus.Phase;
                        rectangle.Length = currentSinus.Length;
                        rectangle_collection.Add(current, rectangle);
                        currentRectangle = rectangle;
                        trackBar_lab2_d.Enabled = true;
                    }
                        this.chart_lab2_common.Series[current].ChartType = SeriesChartType.Line;
                        sinus_collection.Remove(current);
                    }
                else if (triangle_collection.TryGetValue(current, out currentTriangle))
                {
                    if (this.comboBox_select_type.SelectedIndex == 0)
                    {
                        Sinus sinus = new Sinus();
                        sinus.Amplitude = currentTriangle.Amplitude;
                        sinus.Frequency = currentTriangle.Frequency;
                        sinus.Phase = currentTriangle.Phase;
                        sinus.Length = currentTriangle.Length;
                        sinus_collection.Add(current, sinus);
                        currentSinus = sinus;
                        this.chart_lab2_common.Series[current].ChartType = SeriesChartType.Spline;
                        }
                    else if (this.comboBox_select_type.SelectedIndex == 2)
                    {
                        Rectangle rectangle = new Rectangle();
                        rectangle.Amplitude = currentTriangle.Amplitude;
                        rectangle.Frequency = currentTriangle.Frequency;
                        rectangle.Phase = currentTriangle.Phase;
                        rectangle.Length = currentTriangle.Length;
                        rectangle_collection.Add(current, rectangle);
                        currentRectangle = rectangle;
                        trackBar_lab2_d.Enabled = true;
                            this.chart_lab2_common.Series[current].ChartType = SeriesChartType.Line;
                        }

                    triangle_collection.Remove(current);
                    }
                else if (rectangle_collection.TryGetValue(current, out currentRectangle))
                {
                    if (this.comboBox_select_type.SelectedIndex == 0)
                    {
                        Sinus sinus = new Sinus();
                        sinus.Amplitude = currentRectangle.Amplitude;
                        sinus.Frequency = currentRectangle.Frequency;
                        sinus.Phase = currentRectangle.Phase;
                        sinus.Length = currentRectangle.Length;
                        sinus_collection.Add(current, sinus);
                        currentSinus = sinus;
                            this.chart_lab2_common.Series[current].ChartType = SeriesChartType.Spline;
                        }
                    else if (this.comboBox_select_type.SelectedIndex == 1)
                    {
                        Triangle triangle = new Triangle();
                        triangle.Amplitude = currentSinus.Amplitude;
                        triangle.Frequency = currentSinus.Frequency;
                        triangle.Phase = currentSinus.Phase;
                        triangle.Length = currentSinus.Length;
                        triangle_collection.Add(current, triangle);
                        currentTriangle = triangle;
                            this.chart_lab2_common.Series[current].ChartType = SeriesChartType.Line;
                        }
                    rectangle_collection.Remove(current);
                        
                }
                    drawSignal(current);
                }

            }

        }

        public void updateParametrs(string thisCurrent){
            if (sinus_collection.TryGetValue(thisCurrent, out currentSinus))
            {
                this.trackBar_lab2_phase.Value = (int)((double)((currentSinus.Phase)* (this.trackBar_lab2_phase.Maximum))/(2*Math.PI)); //may be mistake
                this.textBox_lab2_A.Text= currentSinus.Amplitude.ToString();
                this.textBox_lab2_F.Text = currentSinus.Frequency.ToString();
            }
            else if (triangle_collection.TryGetValue(thisCurrent, out currentTriangle))
            {
                this.trackBar_lab2_phase.Value = (int)((double)((currentTriangle.Phase) * (this.trackBar_lab2_phase.Maximum)) / (2 * Math.PI)); //may be mistake
                this.textBox_lab2_A.Text = currentTriangle.Amplitude.ToString();
                this.textBox_lab2_F.Text = currentTriangle.Frequency.ToString();
            }
            else if (rectangle_collection.TryGetValue(thisCurrent, out currentRectangle))
            {
                this.trackBar_lab2_phase.Value = (int)((double)((currentRectangle.Phase) * (this.trackBar_lab2_phase.Maximum)) / (2 * Math.PI)); //may be mistake
                this.textBox_lab2_A.Text = currentRectangle.Amplitude.ToString();
                this.textBox_lab2_F.Text = currentRectangle.Frequency.ToString();
                this.trackBar_lab2_d.Value = (int)(currentRectangle.Duty * this.trackBar_lab2_d.Maximum);
            }
        }

        private void trackBar_lab2_phase_ValueChanged(object sender, EventArgs e)
        {
            if (sinus_collection.TryGetValue(current, out currentSinus))
            {
                sinus_collection[current].Phase=((double)trackBar_lab2_phase.Value/ trackBar_lab2_phase.Maximum)*2*Math.PI;
            }
            else if (triangle_collection.TryGetValue(current, out currentTriangle))
            {
                triangle_collection[current].Phase = ((double)trackBar_lab2_phase.Value / trackBar_lab2_phase.Maximum) * 2 * Math.PI;
            }
            else if (rectangle_collection.TryGetValue(current, out currentRectangle))
            {
                rectangle_collection[current].Phase = ((double)trackBar_lab2_phase.Value / trackBar_lab2_phase.Maximum) * 2 * Math.PI; 
            }
            drawSignal(current);
        }

        private void trackBar_lab2_d_ValueChanged(object sender, EventArgs e)
        {
            if (rectangle_collection.TryGetValue(current, out currentRectangle))
            {
                rectangle_collection[current].Duty = ((double)trackBar_lab2_d.Value / trackBar_lab2_d.Maximum);
            }
            drawSignal(current);
        }

        private void textBox_lab2_k_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_lab2_k_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_lab2_k.Text;//
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    lab2_k = (int)N_number;
                    UpdateSum(1);
                    //drawFurie();
                }
            }
            else
            {//очистка графиков
                lab2_k = 0;
                clearFourier();
            }
        }

        private void textBox_lab2_A_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_lab2_A_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_lab2_A.Text;//
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    if (sinus_collection.TryGetValue(current, out currentSinus))
                    {
                        sinus_collection[current].Amplitude = N_number;
                    }
                    else if (triangle_collection.TryGetValue(current, out currentTriangle))
                    {
                        triangle_collection[current].Amplitude = N_number;
                    }
                    else if (rectangle_collection.TryGetValue(current, out currentRectangle))
                    {
                        rectangle_collection[current].Amplitude = N_number;
                       
                    }
                    drawSignal(current);
                }
            }
            else
            {//очистка графиков
                this.chart_lab2_common.Series[current].Points.Clear();
            }
        }

        private void textBox_lab2_F_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_lab2_F_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_lab2_F.Text;//
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    if (sinus_collection.TryGetValue(current, out currentSinus))
                    {
                        sinus_collection[current].Frequency = N_number;
                    }
                    else if (triangle_collection.TryGetValue(current, out currentTriangle))
                    {
                        triangle_collection[current].Frequency = N_number;
                    }
                    else if (rectangle_collection.TryGetValue(current, out currentRectangle))
                    {
                        rectangle_collection[current].Frequency = N_number;
                    }
                    drawSignal(current);
                }
            }
            else
            {//очистка графиков
                this.chart_lab2_common.Series[current].Points.Clear();
            }
        }


        private void drawSignal(string thisCurrent) {
           double var=0;
            this.chart_lab2_common.Series[thisCurrent].Points.Clear();
            if (sinus_collection.TryGetValue(thisCurrent, out currentSinus))
            {                
                double h = (double)1/(double)(currentSinus.Frequency*4) ;
                var -= (currentSinus.Phase + Math.PI * 0.5) / (currentSinus.Frequency * 2 * Math.PI) ;
                while (var <= 1)
                {
                    y = currentSinus.Generate2(var);
                    this.chart_lab2_common.Series[thisCurrent].Points.AddXY(var, y);
                    var += h;
                }
                UpdateSum(0);
            }
            else if (triangle_collection.TryGetValue(thisCurrent, out currentTriangle))
            {
                double h = (double)1 / (currentTriangle.Frequency*2);
                var -= (currentTriangle.Phase + Math.PI * 0.5 )/ (currentTriangle.Frequency*2*Math.PI) ;
                while (var <= 1)
                {
                    y = currentTriangle.Generate2(var);
                    this.chart_lab2_common.Series[thisCurrent].Points.AddXY(var, y);
                    var += h;
                }
                UpdateSum(1);
            }
            else if (rectangle_collection.TryGetValue(thisCurrent, out currentRectangle))
            {
                double h = (double)1 / (currentRectangle.Frequency * 32);
                double lambda = 0.002;
                double fix = ((double)1 / (currentRectangle.Frequency))*currentRectangle.Duty;
                Boolean flag = false;
                int counter = 0;
                var -= (currentRectangle.Phase + Math.PI * 0.5) / (currentRectangle.Frequency * 2 * Math.PI);
                while (var <= 1)
                {
                    y = currentRectangle.Generate2(var);
                    this.chart_lab2_common.Series[thisCurrent].Points.AddXY(var, y);
                    var += h;
                   /* counter++;
                    if (flag)
                    {
                        if (counter == 3)
                        {
                            y = currentRectangle.Generate2(var + lambda + fix);
                            this.chart_lab2_common.Series[thisCurrent].Points.AddXY(var + lambda + fix, y);
                            var += 2 * h;
                            flag = false;
                            counter = 0;
                        }
                        else {
                            y = currentRectangle.Generate2(var + lambda);
                            this.chart_lab2_common.Series[thisCurrent].Points.AddXY(var + lambda , y);
                            var += 2 * h;
                            flag = false;
                        }
                    }
                    else
                    {
                        if (counter ==2)
                        {
                            y = currentRectangle.Generate2(var - lambda + fix);
                            this.chart_lab2_common.Series[thisCurrent].Points.AddXY(var - lambda +fix, y);
                            flag = true;
                        }
                        else
                        {
                            y = currentRectangle.Generate2(var - lambda);
                            this.chart_lab2_common.Series[thisCurrent].Points.AddXY(var - lambda , y);
                            flag = true;
                        }
                    }*/                    
                }
                UpdateSum(2);
            }
            
        }

        double prevMax = 0;
        double maxAmplitude = 0;
        Random rand=new Random();

        private void checkBox_lab3_Noise_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSum(1);
        }

        private List<PointF> sumList = new List<PointF>();
        List<PointF> sumList2 = new List<PointF>();

  
        List<PointF> sumList3 = new List<PointF>();
        List<PointF> copyList = new List<PointF>();

        public void UpdateSum(int mode) {
            sumList.Clear();
            sumList2.Clear();
            sumList3.Clear();
            double var = 0;
            Boolean update_all = true;
            Boolean sin = false, tri = false, rec = false;
            double maxFrequency = 0;
            maxAmplitude = 0.001;
            foreach (var signal in sinus_collection)
            {
                if (signal.Value.Frequency > maxFrequency)
                { maxFrequency = signal.Value.Frequency; }
                maxAmplitude=(signal.Value.Amplitude>maxAmplitude)?signal.Value.Amplitude:maxAmplitude;
            }
            foreach (var signal in triangle_collection)
            {
                if (signal.Value.Frequency > maxFrequency)
                { maxFrequency = signal.Value.Frequency; }
                maxAmplitude = (signal.Value.Amplitude > maxAmplitude) ? signal.Value.Amplitude : maxAmplitude;
            }
            foreach (var signal in rectangle_collection)
            {
                if (signal.Value.Frequency > maxFrequency)
                { maxFrequency = signal.Value.Frequency; }
                maxAmplitude = (signal.Value.Amplitude > maxAmplitude) ? signal.Value.Amplitude : maxAmplitude;
            }
            if (maxFrequency > prevMax)
            {
                prevMax = maxFrequency;
                update_all = true;
            }

            this.label_lab2_Fk.Text=(maxFrequency*2).ToString();
            if (update_all)
            {
                sinusList.Clear();
                Boolean first = true;
                

                foreach (var signal in sinus_collection)
                {
                    
                    var = 0;
                    double h = (double)1 / (double)(maxFrequency * 64);
                    //  var -= (signal.Value.Phase + Math.PI * 0.5) / (signal.Value.Frequency * 2 * Math.PI);
                    if (first)
                    {
                        if (sumList3.Count == 0)
                        {
                            sumList3 = findDotsFuriSinus(lab2_k, signal.Value);
                        }
                        else
                        {
                            var variable = findDotsFuriSinus(lab2_k, signal.Value);
                            for (int z = 0; z < variable.Count; z++)
                            {
                                sumList3[z] = new PointF(variable[z].X, (variable[z].Y + sumList3[z].Y));
                            }
                        }
                        
                        first = false;
                        while (var <= 1-h)
                        {
                            y = signal.Value.Generate2(var);
                            sinusList.Add(new PointF((float)var, (float)y));
                            var += h;
                        }
                    }
                    else
                    {
                        var variable =findDotsFuriSinus(lab2_k, signal.Value);
                        for (int z = 0; z < variable.Count; z++)
                        {
                            sumList3[z] = new PointF(variable[z].X, (variable[z].Y+ sumList3[z].Y));
                        }

                        int i = 0;
                        while (var <= 1-h)
                        {
                            y = signal.Value.Generate2(var);
                            sinusList[i] = new PointF((float)var, (sinusList[i].Y + (float)y));
                            var += h;
                            if (i < sinusList.Count - 1) //kostyl
                            {
                                i++;
                            }
                        }
                    }
                }
                triangleList.Clear();
                first = true;

                foreach (var signal in triangle_collection)
                {
                    var = 0;
                    double h = (double)1 / (double)(maxFrequency * 64);
                    //  var -= (signal.Value.Phase + Math.PI * 0.5) / (signal.Value.Frequency * 2 * Math.PI);
                    if (first)
                    {
                        if (sumList3.Count == 0)
                        {
                            sumList3 = findDotsFuriTriangle(lab2_k, signal.Value);
                        }
                        else
                        {
                            var variable = findDotsFuriTriangle(lab2_k, signal.Value);
                            for (int z = 0; z < variable.Count; z++)
                            {
                                sumList3[z] = new PointF(variable[z].X, (variable[z].Y + sumList3[z].Y));
                            }
                        }
                        first = false;

                        while (var <= 1-h)
                        {
                            y = signal.Value.Generate2(var);
                            triangleList.Add(new PointF((float)var, (float)y));
                            var += h;
                        }
                    }
                    else
                    {
                        int i = 0;
                        var variable = findDotsFuriTriangle(lab2_k, signal.Value);
                        for (int z = 0; z < variable.Count; z++)
                        {
                            sumList3[z] = new PointF(variable[z].X, (variable[z].Y + sumList3[z].Y));
                        }
                        while (var <= 1 - h)
                        {
                            y = signal.Value.Generate2(var);
                            triangleList[i] = new PointF((float)var, (triangleList[i].Y + (float)y));
                            var += h;
                            if (i < sinusList.Count - 1) //kostyl
                            {
                                i++;
                            }

                        }
                    }
                }
                rectangleList.Clear();
                first = true;

                foreach (var signal in rectangle_collection)
                {
                    var = 0;
                    double h = (double)1 / (double)(maxFrequency * 64);
                    if (first)
                    {
                        if (sumList3.Count == 0)
                        {
                            sumList3 = findDotsFuriRectangle(lab2_k, signal.Value);
                        }
                        else
                        {
                            var variable = findDotsFuriRectangle(lab2_k, signal.Value);
                            for (int z = 0; z < variable.Count; z++)
                            {
                                sumList3[z] = new PointF(variable[z].X, (variable[z].Y + sumList3[z].Y));
                            }
                        }
                        first = false;
                        while (var <= 1-h)
                        {
                            y = signal.Value.Generate2(var);
                            rectangleList.Add(new PointF((float)var, (float)y));
                            var += h;
                        }
                    }
                    else
                    {
                        var variable = findDotsFuriRectangle(lab2_k, signal.Value);
                        for (int z = 0; z < variable.Count; z++)
                        {
                            sumList3[z] = new PointF(variable[z].X, (variable[z].Y + sumList3[z].Y));
                        }
                        int i = 0;
                        while (var <= 1 - h)
                        {
                            y = signal.Value.Generate2(var);
                            rectangleList[i] = new PointF((float)var, (rectangleList[i].Y + (float)y));
                            var += h;
                            if (i < rectangleList.Count - 1) //kostyl
                            {
                                i++;
                            }
                        }
                    }
                }
            }
            else {
                if (mode == 0)
                {
                    sinusList.Clear();
                    Boolean first = true;

                    foreach (var signal in sinus_collection)
                    {
                        var = 0;
                        double h = (double)1 / (double)(maxFrequency * 32);
                        //  var -= (signal.Value.Phase + Math.PI * 0.5) / (signal.Value.Frequency * 2 * Math.PI);
                        if (first)
                        {
                            first = false;
                            while (var <= 1)
                            {
                                y = signal.Value.Generate2(var);
                                sinusList.Add(new PointF((float)var, (float)y));
                                var += h;
                            }
                        }
                        else
                        {
                            int i = 0;
                            while (var <= 1)
                            {
                                y = signal.Value.Generate2(var);
                                sinusList[i] = new PointF((float)var, (sinusList[i].Y + (float)y));
                                var += h;
                                if (i < sinusList.Count - 1) //kostyl
                                {
                                    i++;
                                }
                            }
                        }
                    }
                }
                else if (mode == 1)
                {
                    triangleList.Clear();
                    Boolean first = true;

                    foreach (var signal in triangle_collection)
                    {
                        var = 0;
                        double h = (double)1 / (double)(maxFrequency * 32);
                        //  var -= (signal.Value.Phase + Math.PI * 0.5) / (signal.Value.Frequency * 2 * Math.PI);
                        if (first)
                        {
                            first = false;
                            while (var <= 1)
                            {
                                y = signal.Value.Generate2(var);
                                triangleList.Add(new PointF((float)var, (float)y));
                                var += h;
                            }
                        }
                        else
                        {
                            int i = 0;
                            while (var <= 1)
                            {
                                y = signal.Value.Generate2(var);
                                triangleList[i] = new PointF((float)var, (sinusList[i].Y + (float)y));
                                var += h;
                                if (i < sinusList.Count - 1) //kostyl
                                {
                                    i++;
                                }

                            }
                        }
                    }
                }
                else if (mode == 2) {
                    rectangleList.Clear();
                    Boolean first = true;

                    foreach (var signal in rectangle_collection)
                    {
                        var = 0;
                        double h = (double)1 / (double)(maxFrequency * 32);
                        if (first)
                        {
                            first = false;
                            while (var <= 1)
                            {
                                y = signal.Value.Generate2(var);
                                rectangleList.Add(new PointF((float)var, (float)y));
                                var += h;
                            }
                        }
                        else
                        {
                            int i = 0;
                            while (var <= 1)
                            {
                                y = signal.Value.Generate2(var);
                                rectangleList[i] = new PointF((float)var, (sinusList[i].Y + (float)y));
                                var += h;
                                if (i < rectangleList.Count - 1) //kostyl
                                {
                                    i++;
                                }
                            }
                        }
                    }

                }
            }


            this.chart_lab2_summary.Series[0].Points.Clear();
            int length = 0;
            int length1 = 0;
            int length2 = 0;
            int length3 = 0;

            if (sinusList.Count != 0)
            {
                sin = true;
                length1 = sinusList.Count;
            }
            if (triangleList.Count != 0)
            { tri = true;
                length2 = triangleList.Count;
            }
            if (rectangleList.Count != 0)
            { rec = true;
                length3 = rectangleList.Count;
            }

            if (sin && tri && rec)
            {
                if ((length1 <= length2) || (length1 <= length3))
                {
                    length = length1;
                }
                else if ((length2 <= length1) || (length2 <= length3))
                {
                    length = length2;
                }
                else { length = length3; }
            }
            else if (sin)
            {
                length = length1;
            }
            else if (tri)
            {
                length = length2;
            }
            else if (rec) {
                length = length3;
            }

            double hi ;
            if (lab2_k == 0)
            {
              hi = length;
            }
            else
            { hi = (double)length / (lab2_k);
            
            }
            

                int num = 0;
            for (int j = 0; j < length; j++)
            {
                float sum = 0;
                if (sin)
                    sum += sinusList[j].Y;
                if (tri)
                    sum += triangleList[j].Y;
                if (rec)
                    sum += rectangleList[j].Y;
                if (sin)
                {
                    sumList.Add(new PointF(sinusList[j].X, sum));
                   
                    if ((num * hi) < j)
                    {
                        sumList2.Add(new PointF(sinusList[j].X, sum));
                        num++;
                    }
                }
                else if (tri)
                {
                    sumList.Add(new PointF(triangleList[j].X, sum));
                    if ((num * hi) < j)
                    {
                        sumList2.Add(new PointF(triangleList[j].X, sum));
                        num++;
                    }
                }
                else if (rec)
                {
                    sumList.Add(new PointF(rectangleList[j].X, sum));
                    if ((num * hi) < j)
                    {
                        sumList2.Add(new PointF(rectangleList[j].X, sum));
                        num++;
                    }
                }
            }

            copyList.Clear();
            for (int i = 0; i < sumList.Count; i++)
            {
                double varRand = (rand.NextDouble() - 0.5);             

                if (this.checkBox_lab3_Noise.Checked)
                {
                    copyList.Add(new PointF(sumList[i].X, sumList[i].Y + (float)((varRand) * maxAmplitude * 0.5)));
                    sumList[i] = new PointF(sumList[i].X, sumList[i].Y + (float)((varRand) * maxAmplitude * 0.5));
                }
            }

            foreach (var point in sumList)
            {
                this.chart_lab2_summary.Series[0].Points.AddXY(point.X,point.Y);
            }
            drawFurie();
        }

        public void clearFourier() {
            this.chart_lab2_spectrums.Series[0].Points.Clear();
            this.chart_lab2_summary.Series[1].Points.Clear();
            this.chart_lab2_phase.Series[0].Points.Clear();
        }
        public void drawFurie() { 
         List<float> masAj= new List<float>();
         List<float> phaseAj = new List<float>();
         List<float> masAj1 = new List<float>();
         List<float> phaseAj1 = new List<float>();
           // Furi furi = new Furi();
            clearFourier();
            int discret = (int)prevMax * 2;
            if (lab2_k == 0)
            {
                for (int j = 0; j < Math.Round((double)(sumList.Count / 2) - 1); j++)
               // for (int j = 0; j < sumList.Count; j++)
                {
                    masAj.Add(Furi.findAmplitude(Furi.findCosinusComponent1(sumList, sumList.Count, j), Furi.findSinusComponent1(sumList, sumList.Count, j)));
                    phaseAj.Add(Furi.findPhase(Furi.findCosinusComponent1(sumList, sumList.Count, j), Furi.findSinusComponent1(sumList, sumList.Count, j)));

                    /*if (masAj[j] < 1) {
                        this.chart_lab2_spectrums.Series[0].Points.AddXY(j, 0);
                    }
                    else*/
                    { this.chart_lab2_spectrums.Series[0].Points.AddXY(j, masAj[j]); };

                    this.chart_lab2_phase.Series[0].Points.AddXY(j, phaseAj[j]);
                }
                for (int i = 0; i < sumList.Count; i++)
                {
                    this.chart_lab2_summary.Series[1].Points.AddXY(sumList[i].X, (Furi.recoverySignal(masAj, phaseAj, sumList.Count, i)));
                
                }
            }
            else
            {
/*                if (!this.checkBox_lab3_Noise.Checked)
                { // for (int j = 0; j < Math.Round((double)(sumList2.Count / 2) - 1); j++)
                    for (int j = 0; j < lab2_k; j++)
                    {
                        masAj1.Add(Furi.findAmplitude(Furi.findCosinusComponent1(sumList3, sumList3.Count, j), Furi.findSinusComponent1(sumList3, sumList3.Count, j)));
                        phaseAj1.Add(Furi.findPhase(Furi.findCosinusComponent1(sumList3, sumList3.Count, j), Furi.findSinusComponent1(sumList3, sumList3.Count, j)));
                       // masAj1.Add(furi.findAmplitude(furi.findCosinusComponent1(sumList, sumList.Count, j), furi.findSinusComponent1(sumList, sumList.Count, j)));
                       // phaseAj1.Add(furi.findPhase(furi.findCosinusComponent1(sumList, sumList.Count, j), furi.findSinusComponent1(sumList, sumList.Count, j)));
                        *//*                    if (masAj1[j] < 1)
                                            {
                                                this.chart_lab2_spectrums.Series[0].Points.AddXY(j, 0);
                                            }
                                            else*//*
                        { this.chart_lab2_spectrums.Series[0].Points.AddXY(j, masAj1[j]); };
                        this.chart_lab2_phase.Series[0].Points.AddXY(j, phaseAj1[j]);
                    }
                    // double h = (double)sumList.Count / (double)lab2_k;
                    double h = (double)1 / sumList.Count;
                    for (int i = 0; i < sumList.Count; i++)
                    //  for (int i = 0; i < sumList2.Count - 1; i++)
                    {

                        // this.chart_lab2_summary.Series[1].Points.AddXY(sumList2[(int)(i*h)].X, (furi.recoverySignal(masAj1, phaseAj1, sumList2.Count, (int)(i*h))));
                        //  this.chart_lab2_summary.Series[1].Points.AddXY(sumList2[(int)(i)].X, (furi.recoverySignal(masAj1, phaseAj1, sumList2.Count, (int)(i ))));
                       // this.chart_lab2_summary.Series[1].Points.AddXY(sumList[i].X, (furi.recoverySignal(masAj1, phaseAj1, sumList.Count, (i))));
                         this.chart_lab2_summary.Series[1].Points.AddXY(h*i, (Furi.recoverySignal3(masAj1, phaseAj1, lab2_k, i*h)));

                        // this.chart_lab2_spectrums.Series[0].Points.AddXY(i, masAj[(int)(i * h)]);
                    }
                }
                else
                {*/
                    for (int j = 0; j < lab2_k; j++)
                    {
                        /*                        masAj1.Add(furi.findAmplitude(furi.findCosinusComponent1(sumList, sumList.Count, j), furi.findSinusComponent1(sumList, sumList.Count, j)));
                                                phaseAj1.Add(furi.findPhase(furi.findCosinusComponent1(sumList, sumList.Count, j), furi.findSinusComponent1(sumList, sumList.Count, j)));*/
                        masAj1.Add(Furi.findAmplitude(Furi.findCosinusComponent3(sumList, sumList.Count, j), Furi.findSinusComponent3(sumList, sumList.Count, j)));
                        phaseAj.Add(Furi.findPhase(Furi.findCosinusComponent1(sumList3, sumList3.Count, j), Furi.findSinusComponent1(sumList3, sumList3.Count, j)));
                        phaseAj1.Add(Furi.findPhase(Furi.findCosinusComponent3(sumList, sumList.Count, j), Furi.findSinusComponent3(sumList, sumList.Count, j)));

                        { this.chart_lab2_spectrums.Series[0].Points.AddXY(j, masAj1[j]); };
                        this.chart_lab2_phase.Series[0].Points.AddXY(j, phaseAj[j]);
                    }
                    double h = (double)1 /( sumList.Count - 1);
                    for (int i = 0; i < sumList.Count; i++)
                    {
                          this.chart_lab2_summary.Series[1].Points.AddXY(sumList[i].X, (Furi.recoverySignal(masAj1, phaseAj1, sumList.Count, (i))));
                    }
                //}

                /*if (lab2_k < discret)
                {
                    double h = sumList.Count / lab2_k;
                    for (int j = 0; j < lab2_k; j++)
                    {
                        masAj.Add(furi.findAmplitude(furi.findCosinusComponent2(sumList, sumList.Count,j,h), furi.findSinusComponent2(sumList, sumList.Count, j, h)));
                        phaseAj.Add(furi.findPhase(furi.findCosinusComponent2(sumList, sumList.Count, j, h), furi.findSinusComponent2(sumList, sumList.Count, j, h)));
                        this.chart_lab2_spectrums.Series[0].Points.AddXY((int)(j * h), masAj[j]);
                    }
                    for (int i = 0; i < lab2_k; i++)
                    {
                        this.chart_lab2_summary.Series[1].Points.AddXY(sumList[(int)(i*h)].X, (furi.recoverySignal2(masAj, phaseAj, sumList.Count, (int)(i * h))));
                    }
                }
                else  if (lab2_k>= discret)
                {
                    double h = sumList.Count / discret;
                    for (int j = 0; j < discret; j++)
                    {
                        masAj.Add(furi.findAmplitude(furi.findCosinusComponent2(sumList, sumList.Count, j, h), furi.findSinusComponent2(sumList, sumList.Count, j, h)));
                        phaseAj.Add(furi.findPhase(furi.findCosinusComponent2(sumList, sumList.Count, j, h), furi.findSinusComponent2(sumList, sumList.Count, j, h)));
                        this.chart_lab2_spectrums.Series[0].Points.AddXY((int)(j * h), masAj[j]);
                    }
                    for (int i = 0; i < discret; i++)
                    {
                        this.chart_lab2_summary.Series[1].Points.AddXY(sumList[(int)(i * h)].X, (furi.recoverySignal2(masAj, phaseAj, sumList.Count, (int)(i * h))));
                    }
                }*/
            }
        }

        public List<PointF> findDotsFuriSinus(int k_lab2, Sinus sinus)
        {
            double h = (double)1 / k_lab2;
            List<PointF> result = new List<PointF>();
            for (int i = 0; i < k_lab2; i++)
            {
                result.Add(new PointF((float)(i * h), (float)sinus.Generate2(i * h)));
            }
            return result;
        }
        public List<PointF> findDotsFuriTriangle(int k_lab2, Triangle triangle)
        {
            double h = (double)1 / k_lab2;
            List<PointF> result = new List<PointF>();
            for (int i = 0; i < k_lab2; i++)
            {
                result.Add(new PointF((float)(i * h), (float)triangle.Generate2(i * h)));
            }
            return result;
        }

        public List<PointF> findDotsFuriRectangle(int k_lab2, Rectangle rectangle)
        {
            double h = (double)1 / k_lab2;
            List<PointF> result = new List<PointF>();
            for (int i = 0; i < k_lab2; i++)
            {
                result.Add(new PointF((float)(i * h), (float)rectangle.Generate2(i * h)));
            }
            return result;
        }
        //----------------------------------------------------------------------------------------------------------------------------------

        public float slidingAveraging(List<PointF> sumL,int position,int gate)
        {
            float sum = 0;
            for (int i = position-((gate-1)/2); i < position + ((gate - 1) / 2); i++)
            {
                if (i>=0)
                sum += sumL[i].Y;
            }
            return sum / gate;
        }

    }
}
