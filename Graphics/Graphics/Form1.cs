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
        private double A, N, F, f, N_number, d;
        private double As, Ns, Fs, fs;
        private double At, Nt, Ft, ft;
        private double Ar, Nr, Fr, fr, dr;
        private double n, y, var;
        private byte A_limit, N_limit, F_limit, f_limit;
        private DataPointCollection sinus_collection,triangle_collection,rectangle_collection,summary_collection;
        private Boolean old_mode=false;
        private Boolean flag_s, flag_t, flag_r;
        private const double POROG = (double)1 / 2;
        private const double FREQUENCY_OF_DOT = 20;

  
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
            n = POROG * Ns * (-1);
            this.chart_all.Series[0].Points.Clear();
            double h = Ns / (Fs * FREQUENCY_OF_DOT);
            while (n <= POROG * Ns)
            {
                y = As * (Math.Sin((2 * (double)(Math.PI)) * Fs * (double)(n / Ns) + fs));
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
                    As = N_number;
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
                    Fs = N_number;
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
            fs = (double)trackBar_fsinus.Value / trackBar_fsinus.Maximum * (2 * (double)(Math.PI));
            chart_summary_draw();
            draw_all_all();
        }

        private void trackBar_ftriangle_ValueChanged(object sender, EventArgs e)
        {
            ft = (double)trackBar_ftriangle.Value / trackBar_ftriangle.Maximum * (2 * (double)(Math.PI));
            chart_summary_draw();
            draw_all_all();
        }

        private void trackBar_frectangle_ValueChanged(object sender, EventArgs e)
        {
            fr = (double)trackBar_frectangle.Value / trackBar_frectangle.Maximum * (2 * (double)(Math.PI));
            chart_summary_draw();
            draw_all_all();
        }

        private void trackBar_dSum_ValueChanged(object sender, EventArgs e)
        {
            dr= (double)trackBar_dSum.Value / trackBar_dSum.Maximum;
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
                    Ns = N_number;
                    Nt = N_number;
                    Nr = N_number;
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
                    At = N_number;
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
                    Ft = N_number;
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
                    Ar = N_number;
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
                    Fr = N_number;
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
            n = POROG * Nt * (-1);
            this.chart_all.Series[1].Points.Clear();
            double h = Nt / (Ft * FREQUENCY_OF_DOT);
            while (n <= POROG * Nt)
            {
                if ((2 * (double)(Math.PI) * Ft * (double)(n / Nt) + ft - (double)(Math.PI) / 2) >= 0)
                {
                    y = ((double)(2 * At) / (double)(Math.PI) *
                    (Math.Abs((Math.Abs(2 * (double)(Math.PI) * Ft * (double)(n / Nt) + ft - (double)(Math.PI) / 2) % (2 * (double)(Math.PI))) - (double)(Math.PI)))) - At;
                }
                else
                {
                    y = ((double)(2 * At) / (double)(Math.PI) *
                    (Math.Abs((Math.Abs(2 * (double)(Math.PI) * Ft * (double)(n / Nt) + ft - (double)(Math.PI) / 2 + (Ft * (2 * (double)(Math.PI)) * (Math.Abs((int)(n / Nt)) + 1))) % (2 * (double)(Math.PI))) - (double)(Math.PI)))) - At;
                }

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
            n = POROG * Nr * (-1);
            this.chart_all.Series[2].Points.Clear();
            double h = Nr / (Fr * FREQUENCY_OF_DOT);
            while (n <= POROG * Nr)
            {
                if ((2 * (double)(Math.PI) * Fr * (double)(n / Nr) + fr) >= 0)
                {
                    var = (double)((Math.Abs((2 * (double)(Math.PI) * Fr * (double)(n / Nr) + fr)) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

                }
                else
                {
                    var = (double)((Math.Abs(
                        (((2 * (double)(Math.PI) * (Fr * (double)(n / Nr)) + fr + (Fr * (2 * (double)(Math.PI)) * (Math.Abs((int)(n / Nr)) + 1)))))) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

                }
                y = ((var <= dr) ? Ar : -Ar);
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
                    N = N_number;
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
            A = trackBar_A.Value;
            draw_all();

        }


        private void trackBar_F_ValueChanged(object sender, EventArgs e)
        {
            F = trackBar_F.Value;
            draw_all();
        }

        private void trackBar_fo_ValueChanged(object sender, EventArgs e)
        {
            f = (double)trackBar_fo.Value / trackBar_fo.Maximum * (2 * (double)(Math.PI));
            draw_all();
        }

        private void trackBar_d_ValueChanged(object sender, EventArgs e)
        {
            d = (double)trackBar_d.Value / trackBar_d.Maximum;
            draw_all();
        }


        private void change_sinus()
        {
            n = POROG * N * (-1);
            this.chart_sinus.Series[0].Points.Clear();
            if (old_mode){
                while (n <= POROG * N)
                {
                    y = A * (Math.Sin((2 * (double)(Math.PI)) * F * (double)(n / N) + f));
                    this.chart_sinus.Series[0].Points.AddXY(n, y);
                    n++;
                }
            }
            else {
                double h = N / (F * FREQUENCY_OF_DOT);
                while (n <= POROG * N)
                {
                    y = A * (Math.Sin((2 * (double)(Math.PI)) * F * (double)(n / N) + f));
                    this.chart_sinus.Series[0].Points.AddXY(n, y);
                    n+=h;
                }
            }

        }   

        private void clear_sinus() { 
        this.chart_sinus.Series[0].Points.Clear();
        }

        private void change_triangle() {
            n = POROG * N * (-1);
            this.chart_sinus.Series[1].Points.Clear();
            if (old_mode)
            {
                while (n <= POROG * N)
                {
                    if ((2 * (double)(Math.PI) * F * (double)(n / N) + f - (double)(Math.PI) / 2) >= 0)
                    {
                        y = ((double)(2 * A) / (double)(Math.PI) *
                        (Math.Abs((Math.Abs(2 * (double)(Math.PI) * F * (double)(n / N) + f - (double)(Math.PI) / 2) % (2 * (double)(Math.PI))) - (double)(Math.PI)))) - A;
                    }
                    else
                    {
                        y = ((double)(2 * A) / (double)(Math.PI) *
                        (Math.Abs((Math.Abs(2 * (double)(Math.PI) * F * (double)(n / N) + f - (double)(Math.PI) / 2 + (F * (2 * (double)(Math.PI)) * (Math.Abs((int)(n / N)) + 1))) % (2 * (double)(Math.PI))) - (double)(Math.PI)))) - A;
                    }

                    this.chart_sinus.Series[1].Points.AddXY(n, y);
                    n++;
                }
            }
            else {
                double h = N / (F * FREQUENCY_OF_DOT);
                while (n <= POROG * N)
                {
                    if ((2 * (double)(Math.PI) * F * (double)(n / N) + f - (double)(Math.PI) / 2) >= 0)
                    {
                        y = ((double)(2 * A) / (double)(Math.PI) *
                        (Math.Abs((Math.Abs(2 * (double)(Math.PI) * F * (double)(n / N) + f - (double)(Math.PI) / 2) % (2 * (double)(Math.PI))) - (double)(Math.PI)))) - A;
                    }
                    else
                    {
                        y = ((double)(2 * A) / (double)(Math.PI) *
                        (Math.Abs((Math.Abs(2 * (double)(Math.PI) * F * (double)(n / N) + f - (double)(Math.PI) / 2 + (F * (2 * (double)(Math.PI)) * (Math.Abs((int)(n / N)) + 1))) % (2 * (double)(Math.PI))) - (double)(Math.PI)))) - A;
                    }

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
            n = POROG * N * (-1);            
            this.chart_sinus.Series[2].Points.Clear();
            if (old_mode) {
                while (n <= POROG * N)
                {

                    if ((2 * (double)(Math.PI) * F * (double)(n / N) + f) >= 0)
                    {
                        var = (double)((Math.Abs((2 * (double)(Math.PI) * F * (double)(n / N) + f)) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

                    }
                    else
                    {
                        var = (double)((Math.Abs(
                            (((2 * (double)(Math.PI) * (F * (double)(n / N)) + f + (F * (2 * (double)(Math.PI)) * (Math.Abs((int)(n / N)) + 1)))))) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

                    }

                    // var = (double)((Math.Abs((2 * (double)(Math.PI) * F * (double)(n / N) + f)) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

                    y = ((var <= d) ? A : -A);
                    this.chart_sinus.Series[2].Points.AddXY(n, y);
                    n++;
                }
            }
        else{
        double h = N / (F * FREQUENCY_OF_DOT);
                while (n <= POROG * N)
                {

                    if ((2 * (double)(Math.PI) * F * (double)(n / N) + f) >= 0)
                    {
                        var = (double)((Math.Abs((2 * (double)(Math.PI) * F * (double)(n / N) + f)) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

                    }
                    else
                    {
                        var = (double)((Math.Abs(
                            (((2 * (double)(Math.PI) * (F * (double)(n / N)) + f + (F * (2 * (double)(Math.PI)) * (Math.Abs((int)(n / N)) + 1)))))) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

                    }
                    y = ((var <= d) ? A : -A);
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
            double F_max = Fs;           
            this.chart_summary.Series[0].Points.Clear();
            F_max = find_max_frequense(Fs, Ft, Fr);
            n = POROG * Ns * (-1);
            double h= Ns / (F_max * FREQUENCY_OF_DOT);
            while (n <= POROG * Ns)
            {
                double y_max = 0;
                if (flag_s) {
                    y_max+= As * (Math.Sin((2 * (double)(Math.PI)) * Fs * (double)(n / Ns) + fs));
                }
                if (flag_t) {
                    if ((2 * (double)(Math.PI) * Ft * (double)(n / Nt) + ft - (double)(Math.PI) / 2) >= 0)
                    {
                        y_max += ((double)(2 * At) / (double)(Math.PI) *
                        (Math.Abs((Math.Abs(2 * (double)(Math.PI) * Ft * (double)(n / Nt) + ft - (double)(Math.PI) / 2) % (2 * (double)(Math.PI))) - (double)(Math.PI)))) - At;
                    }
                    else
                    {
                        y_max += ((double)(2 * At) / (double)(Math.PI) *
                        (Math.Abs((Math.Abs(2 * (double)(Math.PI) * Ft * (double)(n / Nt) + ft - (double)(Math.PI) / 2 + (Ft * (2 * (double)(Math.PI)) * (Math.Abs((int)(n / Nt)) + 1))) % (2 * (double)(Math.PI))) - (double)(Math.PI)))) - At;
                    }
                }
                if (flag_r) {
                    if ((2 * (double)(Math.PI) * Fr * (double)(n / Nr) + fr) >= 0)
                    {
                        var = (double)((Math.Abs((2 * (double)(Math.PI) * Fr * (double)(n / Nr) + fr)) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

                    }
                    else
                    {
                        var = (double)((Math.Abs(
                            (((2 * (double)(Math.PI) * (Fr * (double)(n / Nr)) + fr + (Fr * (2 * (double)(Math.PI)) * (Math.Abs((int)(n / Nr)) + 1)))))) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

                    }
                    y_max+= ((var <= dr) ? Ar : -Ar);
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
            A = 5;
            F = 5;
            d = 0.5;
            f = (double)5*(2 * (double)(Math.PI))/20;
            N = 500;

            As= 5;
            Fs = 5;
            fs = (double)5 * (2 * (double)(Math.PI)) / 20;
            Ns = 500;

            At = 5;
            Ft = 5;
            ft = (double)5 * (2 * (double)(Math.PI)) / 20;
            Nt = 500;

            Ar = 5;
            Fr = 5;
            dr = 0.5;
            fr = (double)5 * (2 * (double)(Math.PI)) / 20;
            Nr = 500;

            flag_s = false;
            flag_t = false;
            flag_r = false;
        }
    }
}
