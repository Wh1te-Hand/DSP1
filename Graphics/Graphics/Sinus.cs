using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Graphics
{
    public class Sinus 
    {
        private const double POROG = (double)1 / 2;
        private const double FREQUENCY_OF_DOT = 20;
        public DataPointCollection mas_dot;
        double _A;
        double _F;
        double _f;
        double _N;

        public Sinus(double A,double F,double f, double N) {
          _A= A;
          _F= F;
          _f= f;
          _N= N;            
        }

        public Sinus()
        {
            _A = 5;
            _F = 5;
            _f = 0.5;
            _N = 512;
        }

        public double Amplitude {
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
        public double Generate(double x) {
           return _A * (Math.Sin((2 * (double)(Math.PI)) * _F * (double)(x / _N) + _f));
        }

        public double Generate2(double x)
        {
            return _A * (Math.Sin((2 * (double)(Math.PI)) * _F * (double)(x ) + _f));
        }
    }
}
