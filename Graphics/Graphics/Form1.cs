using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using Gray = Emgu.CV.Structure.Gray;

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

            textBox_lab3_AverageK.Text = lab3_AverageK.ToString();
            textBox_lab3_ParabolaK.Text = lab3_ParabolaK.ToString();
            textBox_lab3_MedianK.Text = lab3_MedianK.ToString();
            textBox_lab3_MedianN.Text = lab3_MedianN.ToString();
            textBox_lab3_to.Text=lab3_FiltrTo.ToString();
            textBox_lab3_from.Text=lab3_filtrFrom.ToString();
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
                   // copyList.Add(new PointF(sumList[i].X, sumList[i].Y + (float)((varRand) * maxAmplitude * 0.5)));
                    sumList[i] = new PointF(sumList[i].X, sumList[i].Y + (float)((varRand) * maxAmplitude * 0.5));
                }
            }

            

            if (this.checkBox_lab3_Noise.Checked)
            {
                for (int i = 0; i < sumList3.Count; i++)
                {
                    double varRand = (rand.NextDouble() - 0.5);
                    // copyList.Add(new PointF(sumList[i].X, sumList[i].Y + (float)((varRand) * maxAmplitude * 0.5)));
                    sumList3[i] = new PointF(sumList3[i].X, sumList3[i].Y + (float)((varRand) * maxAmplitude * 0.5));
                }
            }


            this.chart_lab2_summary.Series.Remove(chart_lab2_summary.Series.FindByName("Modify"));
            this.chart_lab2_summary.Series.Remove(chart_lab2_summary.Series.FindByName("Furier2"));

            if (this.checkBox_lab3_smooth.Checked||this.checkBox_lab3_filtr.Checked)
            {
                this.chart_lab2_summary.Series.Add("Modify");
                this.chart_lab2_summary.Series["Modify"].ChartType = SeriesChartType.Spline;
                this.chart_lab2_summary.Series["Modify"].BorderWidth = 2;

                this.chart_lab2_summary.Series.Add("Furier2");
                this.chart_lab2_summary.Series["Furier2"].ChartType = SeriesChartType.Spline;
                this.chart_lab2_summary.Series["Furier2"].BorderWidth = 2;
                this.chart_lab2_summary.Series["Furier2"].Color = Color.Green;
            }

            if ( this.checkBox_lab3_smooth.Checked)
            {

                List<float> masAj2 = new List<float>();
                List<float> phaseAj2 = new List<float>();

                
                if (lab3_AverageFlag)
                {
                    for (int i = 0; i < sumList.Count; i++)
                    {
                        copyList.Add(new PointF(sumList[i].X, slidingAveraging(sumList, i, lab3_AverageK)));
                    }
                }

                if (lab3_MedianFlag)
                {
                    for (int i = 0; i < sumList.Count; i++)
                    {
                        copyList.Add(new PointF(sumList[i].X, medianAveraging(sumList, i, lab3_MedianN,lab3_MedianK)));
                    }
                }

                if (lab3_ParabolaFlag)
                {
                    for (int i = 0; i < sumList.Count; i++)
                    {
                        copyList.Add(new PointF(sumList[i].X, parabol4(sumList, i, lab3_ParabolaK)));
                    }
                }

                /*                foreach (var point in copyList)
                                {
                                    this.chart_lab2_summary.Series["Modify"].Points.AddXY(point.X, point.Y);
                                }*/

            }

            if (checkBox_lab3_filtr.Checked && !this.checkBox_lab3_smooth.Checked)
            {
                for (int i = 0; i < sumList.Count; i++)
                {
                    copyList.Add(new PointF(sumList[i].X, sumList[i].Y));
                }

                /*for (int j = 0; j < lab2_k; j++)
                {
                    float A = Furi.findAmplitude(Furi.findCosinusComponent3(copyList, copyList.Count, j), Furi.findSinusComponent3(copyList, copyList.Count, j));

                    if ((A >= lab3_filtrFrom) && (A <= lab3_FiltrTo))
                    { masAj2.Add(A); }
                    else
                    { masAj2.Add(0); }
                    phaseAj2.Add(Furi.findPhase(Furi.findCosinusComponent3(copyList, copyList.Count, j), Furi.findSinusComponent3(copyList, copyList.Count, j)));

                }

                copyList.Clear();
                for (int i = 0; i < sumList.Count; i++)
                {
                    copyList.Add(new PointF(sumList[i].X, (Furi.recoverySignal(masAj2, phaseAj2, sumList.Count, (i)))));
                }*/
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
         List<float> masAj2 = new List<float>();
         List<float> phaseAj2 = new List<float>();
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
                foreach (var point in copyList)
                {
                    this.chart_lab2_summary.Series["Modify"].Points.AddXY(point.X, point.Y);
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

                    masAj2.Add(Furi.findAmplitude(Furi.findCosinusComponent3(copyList, copyList.Count, j), Furi.findSinusComponent3(copyList, copyList.Count, j)));
                    phaseAj2.Add(Furi.findPhase(Furi.findCosinusComponent3(copyList, copyList.Count, j), Furi.findSinusComponent3(copyList, copyList.Count, j)));

                    { this.chart_lab2_spectrums.Series[0].Points.AddXY(j, masAj1[j]); };
                        this.chart_lab2_phase.Series[0].Points.AddXY(j, phaseAj[j]);
                    }

                    double h = (double)1 /( sumList.Count - 1);
                    for (int i = 0; i < sumList.Count; i++)
                    {
                          this.chart_lab2_summary.Series[1].Points.AddXY(sumList[i].X, (Furi.recoverySignal(masAj1, phaseAj1, sumList.Count, (i))));
                    }

                if (this.checkBox_lab3_filtr.Checked)
                {
                    for (int i = 0; i < copyList.Count; i++)
                    {
                        this.chart_lab2_summary.Series["Furier2"].Points.AddXY(copyList[i].X, (Furi.recoverySignalFiltr(masAj2, phaseAj2, copyList.Count, (i), lab3_filtrFrom, lab3_FiltrTo, !checkBox_lab3_reverse.Checked)));
                    }
                }
                else
                {
                    for (int i = 0; i < copyList.Count; i++)
                    {
                        this.chart_lab2_summary.Series["Furier2"].Points.AddXY(copyList[i].X, (Furi.recoverySignal(masAj2, phaseAj2, copyList.Count, (i))));
                    }
                }

                foreach (var point in copyList)
                {
                    this.chart_lab2_summary.Series["Modify"].Points.AddXY(point.X, point.Y);
                }

                int N = 20;
                double Fd=copyList.Count;
                double Fs = lab3_FiltrTo;
                double Fx = lab3_FiltrTo + 1;

                 double[] H = new double[N]; //Импульсная характеристика фильтра
                 double[] H_id = new double[N]; //Идеальная импульсная характеристика
                 double[] W = new double [N]; //Весовая функция

                double Fc = (Fs + Fx) / (2 * Fd);

                for (int i = 0; i < N; i++)
                {
                    if (i == 0) H_id[i] = 2 * Math.PI * Fc;
                    else H_id[i] = Math.Sin(2 * Math.PI * Fc * i) / (Math.PI * i);
                    // весовая функция Блекмена
                    W[i] = 0.42 - 0.5 * Math.Cos((2 * Math.PI * i) / (N - 1)) + 0.08 * Math.Cos((4 * Math.PI * i) / (N - 1));
                    H[i] = H_id[i] * W[i];
                }

                //Нормировка импульсной характеристики
                double SUM = 0;
                for (int i = 0; i < N; i++) { SUM += H[i]; }
                for (int i = 0; i < N; i++) { H[i] /= SUM; } //сумма коэффициентов равна 1 

                //Фильтрация входных данных
                var result = new double[copyList.Count];
                for (int i = 0; i < copyList.Count; i++)
                {
                    result[i]= 0;
                    for (int j = 0; j < N - 1; j++)// та самая формула фильтра
                    { 
                        if (i - j >= 0)
                    result[i]+= H[j] *copyList[i-j].Y; 
                    }
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

       

        //lab3----------------------------------------------------------------------------------------------------------------------------------

        bool lab3_AverageFlag = true;
        bool lab3_ParabolaFlag = false;
        bool lab3_MedianFlag = false;

        int lab3_AverageK = 5;
        int lab3_ParabolaK = 7;
        int lab3_MedianK = 1;
        int lab3_MedianN = 7;
        int lab3_filtrFrom = 0;
        int lab3_FiltrTo = 10;

        private void textBox_lab3_to_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_lab3_to_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_lab3_to.Text;//
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    lab3_FiltrTo = (int)N_number;
                    UpdateSum(1);
                    //drawFurie();
                }
            }
            else
            {//очистка графиков
                //lab2_k = 0;
                //clearFourier();
                UpdateSum(1);
            }
        }

        private void textBox_lab3_from_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_lab3_from_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_lab3_from.Text;//
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    lab3_filtrFrom= (int)N_number;
                    UpdateSum(1);
                    //drawFurie();
                }
            }
            else
            {//очистка графиков
                //lab2_k = 0;
                //clearFourier();
                UpdateSum(1);
            }
        }
        private void textBox_lab3_AverageK_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_lab3_AverageK_KeyUp(object sender, KeyEventArgs e)
        {
             String line = "";
            line = this.textBox_lab3_AverageK.Text;//
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    lab3_AverageK = (int)N_number;
                    UpdateSum(1);
                    //drawFurie();
                }
            }
            else
            {//очистка графиков
                //lab2_k = 0;
                //clearFourier();
                UpdateSum(1);
            }
        }

        private void textBox_lab3_ParabolaK_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_lab3_ParabolaK_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_lab3_ParabolaK.Text;//
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    lab3_ParabolaK = (int)N_number;
                    UpdateSum(1);
                    //drawFurie();
                }
            }
            else
            {//очистка графиков
                //lab2_k = 0;
                //clearFourier();
                UpdateSum(1);
            }
        }

        private void textBox_lab3_MedianK_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void checkBox_lab3_filtrEn_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSum(1);
        }

        private void checkBox_lab3_reverse_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSum(1);
        }
        Bitmap bitmapModify = null;
        private void button_lab3_openImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "png files (*.png)|*.png| jpg files (*.jpg)|*.jpg";

                if (dlg.ShowDialog() == DialogResult.OK)
                {                  

                    // Create a new Bitmap object from the picture file on disk,
                    // and assign that to the PictureBox.Image property
                    this.pictureBox_lab3_original.Image = new Bitmap(dlg.FileName);
                    bitmapModify=new Bitmap(dlg.FileName);
                }
                
                //bitmapModify.
                var copybit = (Bitmap)bitmapModify.Clone();
               // double[,] kernel = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
           //      double[,] kernel =  { { -1,-1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };
             //  double[,] kernel = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
                //  double[,] kernel = { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
                /*                kernel[0, 0]= 1;
                                kernel[0, 1] = 0;
                                kernel[0, 2] = -1;

                                kernel[1, 0] = 1;
                                kernel[1, 1] = 0;
                                kernel[1, 2] = -1;

                                kernel[2, 0] = 1;
                                kernel[2, 1] = 0;
                                kernel[2, 2] = -1;*/

                /*                kernel[0, 0] = 0;
                                kernel[0, 1] = -1;
                                kernel[0, 2] = -0;

                                kernel[1, 0] = -1;
                                kernel[1, 1] = 5;
                                kernel[1, 2] = -1;

                                kernel[2, 0] = 0;
                                kernel[2, 1] = -1;
                                kernel[2, 2] = -0;*/

                /*                byte[] inputBytes =bitmapModify
                                byte[] outputBytes = new byte[inputBytes.Length];*/

              
            }
        }

        private void comboBox_lab3_kernels_SelectedIndexChanged(object sender, EventArgs e)
        {
            double[,] kernel;
            double[,] kernel0 = new double[3, 3] { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } };
            double[,] kernel1 = new double[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
            double[,] kernel2 = new double[3, 3] { { (double)1 / 9, (double)1 / 9, (double)1 / 9 }, { (double)1 / 9, (double)1 / 9, (double)1 / 9 }, { (double)1 / 9, (double)1 / 9, (double)1 / 9 } };
            double[,] kernel3 = new double[3, 3] { { (double)1 / 16, (double)1 / 8, (double)1 / 16 }, { (double)1 / 8, (double)1 / 4, (double)1 / 8 }, { (double)1 / 16, (double)1 / 8, (double)1 / 16 } };

            var copybit = (Bitmap)bitmapModify.Clone();
            //  double[,] kernel = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };
            //  double[,] kernel = { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
            
            if (this.comboBox_lab3_kernels.SelectedIndex == 0)
                {
                kernel = kernel0 ;
                }
            else


            if (this.comboBox_lab3_kernels.SelectedIndex == 1)
            {
                kernel = new double[3,3]{ { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
            }
            else

            if (this.comboBox_lab3_kernels.SelectedIndex == 2)
            {
                kernel = new double[3, 3] { { (double)1 / 9, (double)1 / 9, (double)1 / 9 }, { (double)1 / 9, (double)1 / 9, (double)1 / 9 }, { (double)1 / 9, (double)1 / 9, (double)1 / 9 } };
            }
            else

            if (this.comboBox_lab3_kernels.SelectedIndex == 3)
            {
                kernel = new double[3, 3] { { (double)1 / 16, (double)1 / 8, (double)1 / 16 }, { (double)1 / 8, (double)1 / 4, (double)1 / 8 }, { (double)1 / 16, (double)1 / 8, (double)1 / 16 } };
            }
            else
            if (this.comboBox_lab3_kernels.SelectedIndex == 4)
            {
                kernel = new double[5, 5] { { (double)-1 / 256, (double)-4 / 256, (double)-6 / 256 , (double)-4 / 256, (double)-1 / 256 }, { (double)-4 / 256, (double)-16 / 256, (double)-24 / 256, (double)-16/ 256, (double)-4 / 256 }, { (double)-6 / 256, (double)-24 / 256, (double)476 / 256 , (double)-24 / 256 , (double)-6 / 256 },
                                               { (double)-4 / 256, (double)-16 / 256, (double)-24 / 256, (double)-16/ 256, (double)-4 / 256 },{ (double)-1 / 256, (double)-4 / 256, (double)-6 / 256 , (double)-4 / 256, (double)-1 / 256 }};
            }
            else
            kernel = new double[3, 3] { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };



            int width = bitmapModify.Width;
            int height = bitmapModify.Height;

            int kernelWidth = kernel.GetLength(0);
            int kernelHeight = kernel.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double rSum = 0, gSum = 0, bSum = 0, kSum = 0;

                    for (int i = 0; i < kernelWidth; i++)
                    {
                        for (int j = 0; j < kernelHeight; j++)
                        {
                            int pixelPosX = x + (i - (kernelWidth / 2));
                            int pixelPosY = y + (j - (kernelHeight / 2));
                            if ((pixelPosX < 0) ||
                              (pixelPosX >= width) ||
                              (pixelPosY < 0) ||
                              (pixelPosY >= height)) continue;

                            double r = bitmapModify.GetPixel(pixelPosX, pixelPosY).R;//inputBytes[3 * (width * pixelPosY + pixelPosX) + 0];
                            double g = bitmapModify.GetPixel(pixelPosX, pixelPosY).G;//inputBytes[3 * (width * pixelPosY + pixelPosX) + 1];
                            double b = bitmapModify.GetPixel(pixelPosX, pixelPosY).B;//inputBytes[3 * (width * pixelPosY + pixelPosX) + 2];

                            double kernelVal = kernel[i, j];

                            rSum += r * kernelVal;
                            gSum += g * kernelVal;
                            bSum += b * kernelVal;

                            //   kSum += kernelVal;
                        }
                    }

                    if (kSum <= 0) kSum = 1;

                    //  rSum /= kSum;
                    if (rSum < 0) rSum = 0;
                    if (rSum > 255) rSum = 255;

                    //  gSum /= kSum;
                    if (gSum < 0) gSum = 0;
                    if (gSum > 255) gSum = 255;

                    // bSum /= kSum;
                    if (bSum < 0) bSum = 0;
                    if (bSum > 255) bSum = 255;

                    Color color = Color.FromArgb((byte)rSum, (byte)gSum, (byte)bSum);
                    copybit.SetPixel(x, y, color);/*
                        outputBytes[3 * (width * y + x) + 0] = (byte)rSum;
                        outputBytes[3 * (width * y + x) + 1] = (byte)gSum;
                        outputBytes[3 * (width * y + x) + 2] = (byte)bSum;*/
                }
            }
            this.pictureBox_lab3_modify.Image = copybit;
        }


        private void textBox_lab3_MedianK_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_lab3_MedianK.Text;//
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    lab3_MedianK = (int)N_number;
                    UpdateSum(1);
                    //drawFurie();
                }
            }
            else
            {//очистка графиков
                //lab2_k = 0;
                //clearFourier();
                UpdateSum(1);
            }
        }

        private void textBox_lab3_MedianN_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }


        private void textBox_lab3_MedianN_KeyUp(object sender, KeyEventArgs e)
        {
            String line = "";
            line = this.textBox_lab3_MedianN.Text;//
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    lab3_MedianN = (int)N_number;
                    UpdateSum(1);
                    //drawFurie();
                }
            }
            else
            {//очистка графиков
                //lab2_k = 0;
                //clearFourier();
                UpdateSum(1);
            }
        }


        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab == tabControl2.TabPages["tabPage_lab3_average"])
            {
                 lab3_AverageFlag = true;
                 lab3_ParabolaFlag = false;
                 lab3_MedianFlag = false;
            }

            if (tabControl2.SelectedTab == tabControl2.TabPages["tabPage_lab3_median"])
            {
                lab3_AverageFlag = false;
                lab3_ParabolaFlag = false;
                lab3_MedianFlag = true;
            }

            if (tabControl2.SelectedTab == tabControl2.TabPages["tabPage_lab3_parabola"])
            {
                lab3_AverageFlag = false;
                lab3_ParabolaFlag = true;
                lab3_MedianFlag = false;
            }
            UpdateSum(1);
        }

        private void checkBox_lab3_smooth_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSum(1);
        }

        private void checkBox_lab3_filtr_CheckedChanged(object sender, EventArgs e)
        {
            UpdateSum(1);
        }


        public float slidingAveraging(List<PointF> sumL,int position,int gate)
        {
            float sum = 0;
            for (int i = position-((gate-1)/2); i < position + ((gate - 1) / 2); i++)
            {
                if ((i>=0)&&(i<sumL.Count))
                sum += sumL[i].Y;
            }
            return sum / gate;
        }

        public float parabol4(List<PointF> sumL, int position, int gate)
        {
            float sum = 0;
            int half= ((gate - 1) / 2);

            if ((half == 3)&&(position>3)&&(position<(sumL.Count-3)))
            {
                var result=(float)(((float)1 / 231) * (5 * sumL[position-half].Y-30* sumL[position - half+1].Y+75* sumL[position - half+2].Y+131* sumL[position].Y
                                                    +75* sumL[position +half-2].Y-30* sumL[position + half - 1].Y+5* sumL[position + half].Y));
                return result;
            }

            if ((half == 4) && (position > 4) && (position < (sumL.Count - 4)))
            {
                return (float)(((float)1 / 429) * (15 * sumL[position - half].Y - 55 * sumL[position - half + 1].Y + 30 * sumL[position - half + 2].Y +135 * sumL[position - half + 3].Y + 179 * sumL[position].Y
                                                     + 135 * sumL[position + half - 3].Y + 30 * sumL[position + half - 2].Y - 55 * sumL[position + half - 1].Y + 15 * sumL[position + half].Y));
            }

            if ((half == 5) && (position > 5) && (position < (sumL.Count - 5)))
            {
                return (float)(((float)1 / 429) * (18 * sumL[position - half].Y - 45 * sumL[position - half + 1].Y -10* sumL[position - half + 2].Y + 60 * sumL[position - half + 3].Y + 120 * sumL[position - half + 4].Y + 143 * sumL[position].Y
                                                    + 120 * sumL[position + half - 4].Y + 60 * sumL[position + half - 3].Y -10 * sumL[position + half - 2].Y - 45 * sumL[position + half - 1].Y + 18 * sumL[position + half].Y));
            }

            if((half == 6) && (position > 6) && (position < (sumL.Count - 6)))
            {
                return (float)(((float)1 / 2431) * (110 * sumL[position - half].Y - 198 * sumL[position - half + 1].Y - 135 * sumL[position - half + 2].Y + 110 * sumL[position - half + 3].Y + 390 * sumL[position - half + 4].Y + 600 * sumL[position - half + 5].Y + 677 * sumL[position].Y
                                                   + 600 * sumL[position + half - 4].Y + 390 * sumL[position + half - 4].Y + 110 * sumL[position + half - 3].Y - 135 * sumL[position + half - 2].Y - 198 * sumL[position + half - 1].Y + 110 * sumL[position + half].Y));
            }

            
            return sumL[position].Y;
        }

        public float medianAveraging(List<PointF> sumL, int position, int gate,int K)
        {
            float sum = 0;
            List<PointF> temp = new List<PointF>();
            List<PointF> result = new List<PointF>();
            for (int i = position - ((gate - 1) / 2); i <= position + ((gate - 1) / 2); i++)
            {
                if ((i >= 0) && (i < sumL.Count))
                    temp.Add(sumL[i]);
            }

           
            int length=temp.Count;

            for (int i = 0; i < length; i++)
            {
                double min = double.MinValue;
                int pos = 0;
                for (int j = 0; j< temp.Count; j++)
                {
                    if (temp[j].Y<min) 
                    {
                        min = temp[j].Y;
                        pos = j;
                    }
                }
                result.Add(temp[pos]);
                temp.Remove(temp[pos]);
            }

            int diff = (gate - result.Count);
            int newK = K;
            if (diff > 0)
            {
                while (diff != 0)
                {
                    if (diff % 2 == 0)
                    {
                        result.Remove(result[0]);
                    }
                    else
                    {
                        result.Remove(result[result.Count - 1]);
                    }
                    diff--;
                }
            }
            else
            {
                while (newK != 0)
                {
                    result.Remove(result[0]);
                    result.Remove(result[result.Count - 1]);
                    newK--;
                } 
            }

            for (int i = 0; i < result.Count; i++)
            {
                sum += result[i].Y;
            }

            return sum/ result.Count;
        }


        //----------------------------------------------------------------------------#4------------------------------------------------------------
        //----------------------------------------------------------------------------#4------------------------------------------------------------
        //----------------------------------------------------------------------------#4------------------------------------------------------------

        Bitmap bitmapIllum;
        Bitmap findIllum;
        List<(int, int)> lab4_findList = new List<(int, int)>();
        int lab4_x=0;
        int lab4_y =0;
        int lab4_h=0;
        int lab4_w=0;
        private void button_lab4_image_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "png files (*.png)|*.png| jpg files (*.jpg)|*.jpg";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.pictureBox_lab4_startImage.Image = new Bitmap(dlg.FileName);
                    bitmapIllum = new Bitmap(dlg.FileName);
                }
                UpdateIlluminator();
            }
        }

        private void UpdateIlluminator()
        {
            if (lab4_x != 0 && lab4_y != 0 && lab4_w != 0 && lab4_h != 0)
            {
                if (bitmapIllum.Width != 0)
                {
                    var copybit = new Bitmap(lab4_w, lab4_h); //change

                    int width = copybit.Width;
                    int height = copybit.Height;

                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            copybit.SetPixel(x, y, bitmapIllum.GetPixel(x + lab4_x, y + lab4_y));
                        }
                    }
                    this.pictureBox_lab4_illuminator.Image = copybit;
                    findIllum = (Bitmap)copybit.Clone();
                }
            }
        }


        private void trackBar_lab4_x_ValueChanged(object sender, EventArgs e)
        {
            if (bitmapIllum != null && bitmapIllum.Width != 0)
                if (((trackBar_lab4_x.Value * bitmapIllum.Width) / trackBar_lab4_x.Maximum + (trackBar_lab4_w.Value * bitmapIllum.Width) / (trackBar_lab4_w.Maximum * 2)) < bitmapIllum.Width)
                {
                    {
                        lab4_x = (trackBar_lab4_x.Value * bitmapIllum.Width) / trackBar_lab4_x.Maximum;
                        lab4_y = (trackBar_lab4_y.Value * bitmapIllum.Height) / trackBar_lab4_y.Maximum;
                        lab4_h = (trackBar_lab4_h.Value * bitmapIllum.Width) / (trackBar_lab4_h.Maximum * 2);
                        lab4_w = (trackBar_lab4_w.Value * bitmapIllum.Width) / (trackBar_lab4_w.Maximum * 2);
                        UpdateIlluminator();
                    }
                }
        }


        private void trackBar_lab4_y_ValueChanged(object sender, EventArgs e)
        {
            if (bitmapIllum != null && bitmapIllum.Width != 0)
            {
                if (((trackBar_lab4_y.Value * bitmapIllum.Height) / trackBar_lab4_y.Maximum +
                (trackBar_lab4_h.Value * bitmapIllum.Width) / (trackBar_lab4_h.Maximum * 2)) < bitmapIllum.Height)
                {
                    lab4_x = (trackBar_lab4_x.Value * bitmapIllum.Width) / trackBar_lab4_x.Maximum;
                    lab4_y = (trackBar_lab4_y.Value * bitmapIllum.Height) / trackBar_lab4_y.Maximum;
                    lab4_h = (trackBar_lab4_h.Value * bitmapIllum.Width) / (trackBar_lab4_h.Maximum * 2);
                    lab4_w = (trackBar_lab4_w.Value * bitmapIllum.Width) / (trackBar_lab4_w.Maximum * 2);
                    UpdateIlluminator();
                }
            }
        }

        private void trackBar_lab4_h_ValueChanged(object sender, EventArgs e)
        {
            if (bitmapIllum != null && bitmapIllum.Width != 0)
            {
                if (((trackBar_lab4_y.Value * bitmapIllum.Height) / trackBar_lab4_y.Maximum +
                (trackBar_lab4_h.Value * bitmapIllum.Width) / (trackBar_lab4_h.Maximum * 2)) < bitmapIllum.Height)
                {
                    lab4_x = (trackBar_lab4_x.Value * bitmapIllum.Width) / trackBar_lab4_x.Maximum;
                    lab4_y = (trackBar_lab4_y.Value * bitmapIllum.Height) / trackBar_lab4_y.Maximum;
                    lab4_h = (trackBar_lab4_h.Value * bitmapIllum.Width) / (trackBar_lab4_h.Maximum * 2);
                    lab4_w = (trackBar_lab4_w.Value * bitmapIllum.Width) / (trackBar_lab4_w.Maximum * 2);
                    UpdateIlluminator();
                }
            }
        }

        private void trackBar_lab4_w_ValueChanged(object sender, EventArgs e)
        {
            if (bitmapIllum != null && bitmapIllum.Width != 0)
            {
                if (((trackBar_lab4_x.Value * bitmapIllum.Width) / trackBar_lab4_x.Maximum + (trackBar_lab4_w.Value * bitmapIllum.Width) / (trackBar_lab4_w.Maximum * 2)) < bitmapIllum.Width)
                {
                    lab4_x = (trackBar_lab4_x.Value * bitmapIllum.Width) / trackBar_lab4_x.Maximum;
                    lab4_y = (trackBar_lab4_y.Value * bitmapIllum.Height) / trackBar_lab4_y.Maximum;
                    lab4_h = (trackBar_lab4_h.Value * bitmapIllum.Width) / (trackBar_lab4_h.Maximum * 2);
                    lab4_w = (trackBar_lab4_w.Value * bitmapIllum.Width) / (trackBar_lab4_w.Maximum * 2);
                    UpdateIlluminator();
                }
            }
        }

        private void button_lab4_find_Click(object sender, EventArgs e)
        {
            int width = bitmapIllum.Width;
            int height = bitmapIllum.Height;
            bool flagwrong = false;
            lab4_findList.Clear();

                for (int x = 0; x < width - lab4_w; x++)
                {
                    for (int y = 0; y < height - lab4_h; y++)
                    {

                        for (int i = 0; i < findIllum.Width; i++)
                        {
                            for (int j = 0; j < findIllum.Height; j++)
                            {
                                int pixelPosX = x + (i);
                                int pixelPosY = y + (j);

                                Color r = findIllum.GetPixel(i, j);//inputBytes[3 * (width * pixelPosY + pixelPosX) + 0];
                                Color g = bitmapIllum.GetPixel(pixelPosX, pixelPosY);
                                if (r != g)
                                {
                                    flagwrong = true;
                                    break;
                                }
                                //   kSum += kernelVal;
                            }

                            if (flagwrong)
                            {
                                break;
                            }
                        }

                        if (flagwrong == false)
                        {
                            lab4_findList.Add((x, y));
                        }
                        flagwrong = false;
                    }
                }
            Bitmap copybitmap = (Bitmap)bitmapIllum.Clone();
            if (lab4_findList.Count != 0)
                {
                this.pictureBox_lab4_map.Image = CalculateCorrelationMap(bitmapIllum,findIllum);                
                Color color = Color.FromArgb(255, 0, 0);
                    foreach (var item in lab4_findList)
                    {
                        int startx = item.Item1;
                        int starty = item.Item2;

                        for (int x = 0; x < lab4_w; x++)
                        {
                            for (int j = -1; j < 1; j++)
                            {
                                copybitmap.SetPixel(startx + x, starty + j, color);
                                copybitmap.SetPixel(startx + x, starty + lab4_h + j, color);
                            }
                        }

                        for (int y = 0; y < lab4_h; y++)
                        {
                            for (int j = -1; j < 1; j++)
                            {
                                copybitmap.SetPixel(startx + j, starty + y, color);
                                copybitmap.SetPixel(startx + lab4_w + j, starty + y, color);
                            }
                        }
                    }
                }
                this.pictureBox_lab4_startImage.Image = copybitmap;
            
        }

        public Bitmap CalculateCorrelationMap(Bitmap sourceImage, Bitmap targetImage)
        {
            Image<Gray, float> image1 = new Image<Gray, float>(sourceImage);
            Image<Gray, float> image2 = new Image<Gray, float>(targetImage);

            Image<Gray, float> correlationMap = image1.MatchTemplate(image2, TemplateMatchingType.CcorrNormed);
            CvInvoke.Normalize(correlationMap, correlationMap, 0, 1, NormType.MinMax, DepthType.Cv32F);
            for (int i = 0; i < correlationMap.Rows; i++)
            {
                for (int j = 0; j < correlationMap.Cols; j++)
                {
                    if (correlationMap[i, j].Intensity < 0.7)
                    {
                        correlationMap[i, j] = new Gray(0);
                    }
                }
            }

            correlationMap._Mul(255);

            Bitmap correlationBitmap = correlationMap.Bitmap;

            return correlationBitmap;
        }


        //----------------------------------------------------------------------------#4 signal------------------------------------------------------------
        //----------------------------------------------------------------------------#4 signal------------------------------------------------------------
        //----------------------------------------------------------------------------#4 signal------------------------------------------------------------
        //----------------------------------------------------------------------------#4 signal------------------------------------------------------------

    }
}
