using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public  static class Furi
    {
      //  public Furi() {;}

        public static float findCosinusComponent1(List<PointF> mas, int N, int j)
        {

            float sum = 0;
            for (int i = 0; i < (N ); i++)
            {
                sum += mas[(i)].Y * (float)Math.Cos((float)(2 * Math.PI * j * i) / N);
            }
            return (float)(sum * 2) / N;
        }

        public static float findSinusComponent1(List<PointF> mas, int N, int j)
        {

            float sum = 0;
            for (int i = 0; i < (N ); i++)
            {
                sum += mas[i].Y * (float)Math.Sin((float)(2 * Math.PI * j * i) / N);
            }
            return (float)(sum * 2) / N;
        }
        public static float findCosinusComponent3(List<PointF> mas, int N, int j)
        {

            float sum = 0;
            for (int i = 0; i < (N-1); i++)
            {
                sum += mas[(i)].Y * (float)Math.Cos((float)(2 * Math.PI * j * i) / N);
            }
            return (float)(sum * 2) / N;
        }

        public static float findSinusComponent3(List<PointF> mas, int N, int j)
        {

            float sum = 0;
            for (int i = 0; i < (N-1); i++)
            {
                sum += mas[i].Y * (float)Math.Sin((float)(2 * Math.PI * j * i) / N);
            }
            return (float)(sum * 2) / N;
        }
        public static float findCosinusComponent2(List<PointF> mas,int N,int j,double h) {

            float sum = 0;
            for (int i = 0; i < (int)(N/h);i++)
            {
                sum += mas[(int)(i*h)].Y * (float)Math.Cos((float)(2 * Math.PI * j * i) / (N / h));
            }
            return (float)(sum*2)/ (float)(N / h);
        }

        public static float findSinusComponent2(List<PointF> mas, int N, int j,double h)
        {

            float sum = 0;
            for (int i = 0; i < (int)(N / h); i++)
            {
                sum += mas[(int)(i * h)].Y * (float)Math.Sin((float)(2 * Math.PI * j * i) / (N / h));
            }
            return (float)(sum * 2) / (float)(N / h);
        }

        public static float findAmplitude(float aCos,float aSin)
        {
            return (float)Math.Sqrt(aCos*aCos+aSin*aSin);
        }

        public static float findPhase(float aCos, float aSin)
        {
            return (float)Math.Atan2(aSin,aCos);
        }

        public static float recoverySignal(List<float> aJmas,List<float> phaseMas, int N,int i) 
        {
            float sum = 0;

           for (int j = 1; j< aJmas.Count / 2+1 ; j++) 
      //      for (int j = 0; j < N ; j++)
            {
                //  sum += aJmas[j] * (float)Math.Cos((2 * Math.PI * j * i) / N );
                sum += aJmas[j] * (float)Math.Cos(((float)(2 * Math.PI * j * i) / N) - phaseMas[j]); 
            }
       // return sum+aJmas[0]/2;
       return sum;
        }
        public static float recoverySignal3(List<float> aJmas, List<float> phaseMas, int N,double i)
        {
            float sum = 0;
            // for (int j = 0; j < N / 2 - 1; j++)
            for (int j = 1; j < aJmas.Count / 2+1 ; j++)
            {
                //  sum += aJmas[j] * (float)Math.Cos((2 * Math.PI * j * i) / N );
                // sum += aJmas[j] * (float)Math.Cos(((2 * Math.PI * j * i) / N) - phaseMas[j]);
                sum += aJmas[j] * (float)Math.Cos(((2 * Math.PI * j * i)) - phaseMas[j]);
            }
            // return sum+aJmas[0]/2;
            return sum;
        }

        public static float recoverySignal2(List<float> aJmas, List<float> phaseMas, int N, int i)
        {
            float sum = 0;

            for (int j = 0; j <aJmas.Count; j++)
            {
                //  sum += aJmas[j] * (float)Math.Cos((2 * Math.PI * j * i) / N );
                sum += aJmas[j] * (float)Math.Cos(((2 * Math.PI * j * i) / (aJmas.Count*2)) - phaseMas[j]);
            }
            // return sum+aJmas[0]/2;
            return sum;
        }
    }
}
