using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class Furi
    {
        public Furi() {;}
        public float findCosinusComponent(List<PointF> mas,int N,int j) {

            float sum = 0;
            for (int i = 0; i < N;i++)
            {
                sum += mas[i].Y * (float)Math.Cos((float)(2 * Math.PI * j * i) / N);
            }
            return (float)(sum*2)/N;
        }

        public float findSinusComponent(List<PointF> mas, int N, int j)
        {

            float sum = 0;
            for (int i = 0; i < N; i++)
            {
                sum += mas[i].Y * (float)Math.Sin((float)(2 * Math.PI * j * i) / N);
            }
            return (float)(sum * 2) / N;
        }

        public float findAmplitude(float aCos,float aSin)
        {
            return (float)Math.Sqrt(aCos*aCos+aSin*aSin);
        }

        public float findPhase(float aCos, float aSin)
        {
            return (float)Math.Atan2(aSin,aCos);
        }

        public float recoverySignal(List<float> aJmas,List<float> phaseMas, int N,int i) {
            float sum = 0;

            for (int j = 0; j< N / 2 - 1; j++) {
                //  sum += aJmas[j] * (float)Math.Cos((2 * Math.PI * j * i) / N );
                sum += aJmas[j] * (float)Math.Cos(((2 * Math.PI * j * i) / N) - phaseMas[j]); 
            }
       // return sum+aJmas[0]/2;
       return sum;
        }
    }
}
