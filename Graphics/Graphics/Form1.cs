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
        private double n, y, var;
        private Boolean old_mode=false;
        private const double POROG = (double)1 / 2;
        private const double FREQUENCY_OF_DOT = 20;

        public Form1()
        {
            InitializeComponent();
            Default_params();
            draw_all();
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

        private void Default_params() {           
            A = 5;
            F = 5;
            d = 0.5;
            f = (double)5*(2 * (double)(Math.PI))/20;
            N = 500;          
        }
    }
}
