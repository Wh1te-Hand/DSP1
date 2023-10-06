using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Graphics
{
    public class Rectangle
    {
        private const double POROG = (double)1 / 2;
        private const double FREQUENCY_OF_DOT = 20;
        public DataPointCollection mas_dot;
        double _A;
        double _F;
        double _f;
        double _N;
        double _d;

        public Rectangle(double A, double F, double f, double N,double d)
        {
            _A = A;
            _F = F;
            _f = f;
            _N = N;
            _d = d;
        }

        public double Amplitude
        {
            get => _A;
            set => _A = value;
        }
        public double Frequency
        {
            get => _F;
            set => _F = value;
        }
        public double Phase
        {
            get => _f;
            set => _f = value;
        }
        public double Length
        {
            get => _N;
            set => _N = value;
        }
        public double Duty
        {
            get => _d;
            set => _d = value;
        }
        public double Generate(double x)
        {
            double y;
            double var;
            if ((2 * (double)(Math.PI) * _F * (double)(x / _N) + _f) >= 0)
            {
                var = (double)((Math.Abs((2 * (double)(Math.PI) * _F * (double)(x / _N) + _f)) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

            }
            else
            {
                var = (double)((Math.Abs(
                    (((2 * (double)(Math.PI) * (_F * (double)(x / _N)) + _f + (_F * (2 * (double)(Math.PI)) * (Math.Abs((int)(x / _N)) + 1)))))) % (2 * (double)(Math.PI)))) / (2 * (double)(Math.PI));

            }
            y = ((var <= _d) ? _A : -_A);
            return y;
        }
    }
}
