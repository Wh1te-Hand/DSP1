using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Graphics
{
    public class Triangle
    {
        private const double POROG = (double)1 / 2;
        private const double FREQUENCY_OF_DOT = 20;
        public DataPointCollection mas_dot;
        double _A;
        double _F;
        double _f;
        double _N;

        public Triangle(double A, double F, double f, double N)
        {
            _A = A;
            _F = F;
            _f = f;
            _N = N;
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
        public double Generate(double x)
        {
            double y;
            if ((2 * (double)(Math.PI) * _F * (double)(x / _N) + _f - (double)(Math.PI) / 2) >= 0)
            {
                y = ((double)(2 * _A) / (double)(Math.PI) *
                (Math.Abs((Math.Abs(2 * (double)(Math.PI) * _F * (double)(x / _N) + _f - (double)(Math.PI) / 2) % (2 * (double)(Math.PI))) - (double)(Math.PI)))) - _A;
            }
            else
            {
                y = ((double)(2 * _A) / (double)(Math.PI) *
                (Math.Abs((Math.Abs(2 * (double)(Math.PI) * _F * (double)(x/ _N) + _f - (double)(Math.PI) / 2 + (_F * (2 * (double)(Math.PI)) * (Math.Abs((int)(x/ _N)) + 1))) % (2 * (double)(Math.PI))) - (double)(Math.PI)))) - _A;
            }
            return y;
        }
    }
}
