using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
        private Boolean old_mode=false;
        private Boolean flag_s, flag_t, flag_r;
        private const double POROG = (double)1 / 2;
        private const double FREQUENCY_OF_DOT = 20;
        private Sinus sinus,sinus_start;
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
            double h = sinus.Length/ (sinus.Frequency * FREQUENCY_OF_DOT);
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
            rectangle.Duty= (double)trackBar_dSum.Value / trackBar_dSum.Maximum;
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
                    rectangle_start.Length=N_number;
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
            rectangle_start.Phase= sinus_start.Phase;
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
            if (old_mode){
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
                    n+=h;
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
                double h = triangle_start.Length / (triangle_start.Frequency* FREQUENCY_OF_DOT);
                while (n <= POROG * triangle_start.Length)
                {
                    y=triangle_start.Generate(n);
                    this.chart_sinus.Series[1].Points.AddXY(n, y);
                    n+=h;
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
        else{
        double h = rectangle_start.Length / (rectangle_start.Frequency * FREQUENCY_OF_DOT);
                while (n <= POROG * rectangle_start.Length)
                {
                    y = rectangle_start.Generate(n);
                    this.chart_sinus.Series[2].Points.AddXY(n, y);
                    n+=h;
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
            double h= sinus.Length / (F_max * FREQUENCY_OF_DOT);
            while (n <= POROG * sinus.Length)
            {
                double y_max = 0;
                if (flag_s) {
                    y_max+=sinus.Generate(n);
                }
                if (flag_t) {
                 y_max+=triangle.Generate(n);
                }
                if (flag_r) {
                 y_max+=rectangle.Generate(n);
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
        }

        //------------------------------------------------DSP2_part---------------------------------------------------------
        private Dictionary<string,Sinus> sinus_collection = new Dictionary<string, Sinus>();
        private Dictionary<string, Triangle> triangle_collection = new Dictionary<string, Triangle>();
        private Dictionary<string, Rectangle> rectangle_collection = new Dictionary<string, Rectangle>();
        private string current;
        private int standart = 0;
        private Sinus currentSinus;
        private Triangle currentTriangle;
        private Rectangle currentRectangle; 
        private void button_add_chart_Click(object sender, EventArgs e)
        {
            current = this.textBox_chart_name.Text.Trim();
            if (sinus_collection.ContainsKey(current))
            {
                current = ($"defaultSignal{standart}");
                standart++;
            }
            this.comboBox_select_chart.Items.Add(current);
            Sinus sinus = new Sinus();
            sinus_collection.Add(current, sinus);
            currentSinus = sinus;
            /*            Triangle triangle=new Triangle();
                        triangle_collection.Add(current, triangle);*/
            select_and_update();            
        }
        private void button_delete_chart_Click(object sender, EventArgs e)
        {
            sinus_collection.Remove(current);
            this.comboBox_select_chart.Items.Remove(current);
            this.label_chart_current_name.Text = "";
        }
        private void select_and_update() { 
            this.label_chart_current_name.Text = current;
            this.comboBox_select_chart.Text = current;
            if (sinus_collection.TryGetValue(current, out currentSinus))
            {
                this.comboBox_select_type.SelectedIndex = 0;
            }
            else if (triangle_collection.TryGetValue(current, out currentTriangle))
            {
                this.comboBox_select_type.SelectedIndex = 1;
            }
            else if (rectangle_collection.TryGetValue(current, out currentRectangle))
            {
                this.comboBox_select_type.SelectedIndex = 2;
            }
        }

        private void comboBox_select_chart_SelectedIndexChanged(object sender, EventArgs e)
        {
            current = this.comboBox_select_chart.SelectedItem.ToString();
            select_and_update();
        }

        private void comboBox_select_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sinus_collection.TryGetValue(current, out currentSinus))
            {
                if (this.comboBox_select_chart.SelectedIndex.ToString() == "1")
                {
                    Triangle triangle = new Triangle();
                    triangle.Amplitude = currentSinus.Amplitude;
                    triangle.Frequency = currentSinus.Frequency;
                    triangle.Phase = currentSinus.Phase;
                    triangle.Length=currentSinus.Length;
                    triangle_collection.Add(current,triangle);
                    currentTriangle = triangle;
                }
                else if (this.comboBox_select_chart.SelectedIndex.ToString() == "2")
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Amplitude = currentSinus.Amplitude;
                    rectangle.Frequency = currentSinus.Frequency;
                    rectangle.Phase = currentSinus.Phase;
                    rectangle.Length = currentSinus.Length;
                    rectangle_collection.Add(current,rectangle);
                    currentRectangle = rectangle;
                }

                    sinus_collection.Remove(current);
            }
            else if (triangle_collection.TryGetValue(current, out currentTriangle))
            {
                if (this.comboBox_select_chart.SelectedIndex.ToString() == "0")
                {
                    Sinus sinus = new Sinus();
                    sinus.Amplitude = currentTriangle.Amplitude; 
                    sinus.Frequency = currentTriangle.Frequency;
                    sinus.Phase = currentTriangle.Phase;
                    sinus.Length = currentTriangle.Length;
                    sinus_collection.Add(current,sinus);
                    currentSinus=sinus;
                }
                else if (this.comboBox_select_chart.SelectedIndex.ToString() == "2")
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Amplitude = currentTriangle.Amplitude;
                    rectangle.Frequency = currentTriangle.Frequency;
                    rectangle.Phase = currentTriangle.Phase;
                    rectangle.Length = currentTriangle.Length;
                    rectangle_collection.Add(current, rectangle);
                    currentRectangle = rectangle;
                }

                        triangle_collection.Remove(current);
            }
            else if (rectangle_collection.TryGetValue(current, out currentRectangle))
            {
                if (this.comboBox_select_chart.SelectedIndex.ToString() == "0")
                {
                    Sinus sinus = new Sinus();
                    sinus.Amplitude = currentRectangle.Amplitude;
                    sinus.Frequency = currentRectangle.Frequency;
                    sinus.Phase = currentRectangle.Phase;
                    sinus.Length = currentRectangle.Length;
                    sinus_collection.Add(current, sinus);
                    currentSinus = sinus;
                }
                else if (this.comboBox_select_chart.SelectedIndex.ToString() == "1")
                {
                    Triangle triangle = new Triangle();
                    triangle.Amplitude = currentSinus.Amplitude;
                    triangle.Frequency = currentSinus.Frequency;
                    triangle.Phase = currentSinus.Phase;
                    triangle.Length = currentSinus.Length;
                    triangle_collection.Add(current, triangle);
                    currentTriangle = triangle;
                }

                       rectangle_collection.Remove(current);
            }

     
        }

    }
}
