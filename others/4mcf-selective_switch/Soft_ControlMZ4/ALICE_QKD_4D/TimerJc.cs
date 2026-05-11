using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALICE_QKD_4D
{
    class TimerJc
    {
        double tiempoAcc = 0;
        public void tiempoReset()
        {
            tiempoAcc = time(tiempoAcc);
        }

        private double time(double tAct)
        {
            double realTime = 0;
            double t = System.DateTime.Now.ToFileTime();
            realTime = t - tAct;
            return realTime;

        }
        public double timeNow()
        {
            double realTime2 = System.Math.Round(time(tiempoAcc) / 10000000, 4);
            return realTime2;
        }
        public string timeString()
        {
            double realTime2 = System.Math.Round(time(tiempoAcc) / 10000000, 4);
            string s = Double2string(realTime2, true);
            return s;
        }
        public string Double2string(double d, bool punto) //numinicio=17 desface=0
        {
            string dd = d.ToString(), salida = "";

            for (int i = 0; i < dd.Length; i++)
            {
                if (punto)
                {
                    if (dd[i] == ',')
                        salida += '.';
                    else

                        salida += dd[i];
                }
                else
                {
                    if (dd[i] == '.')
                        salida += ',';
                    else

                        salida += dd[i];
                }
            }

            return salida;
        }
    }
}

