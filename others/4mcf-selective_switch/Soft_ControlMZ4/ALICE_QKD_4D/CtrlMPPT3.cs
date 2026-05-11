using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALICE_QKD_4D
{
    class CtrlMPPT3
    {
        public double MAxMod = 2, MinMod = -2;
        public int Proceso=0;
        public double umbral = 100;
        public double ETop = 0, Eleft = 0, Eright = 0;
        public double deltaC = 0;
        double CuentasAnt = 10;
        double cuentas = 10;
        double Count_Integrar = 0;
        double AccIntegrar = 0;

        private bool Integrar(int MaxIntegrar, double cuentasI)
        {
            bool integraOk = false;
            AccIntegrar += cuentasI;
            Count_Integrar++;
            if (MaxIntegrar - 1 < Count_Integrar)
            {
                integraOk = true;
                cuentas = AccIntegrar / MaxIntegrar;
                AccIntegrar = 0;
                Count_Integrar = 0;
            }
            return integraOk;

        }

        public void CtrlMPPT_3(double cuentas0, double pasos, int NumInt)
        {
            if (Integrar(NumInt,  cuentas0))
            {
                switch (Proceso)
                {
                    /////ETop
                    case 0:
                        if (umbral > cuentas)
                        {

                            Proceso = 1;
                            ETop += pasos;

                        }

                        break;

                    case 1:
                        deltaC = (cuentas - CuentasAnt) / (cuentas + CuentasAnt+0.01) * 10;
                        if (deltaC > 0.2) deltaC = 0.2;
                        if (deltaC < -0.2) deltaC = -0.2;
                        ETop += deltaC;
                        Proceso = 2;
                        break;

                    case 2:
                        if (cuentas > CuentasAnt)
                        {

                            ETop += deltaC;
                        }
                        else
                        {
                            ETop -= deltaC;
                            Proceso = 3;
                        }
                        break;
                    /////Eleft  
                    case 3:

                        Proceso = 4;
                        Eleft += pasos;


                        break;

                    case 4:
                        deltaC = (cuentas - CuentasAnt) / (cuentas + CuentasAnt) * 10;
                        if (deltaC > 0.2) deltaC = 0.2;
                        if (deltaC < -0.2) deltaC = -0.2;
                        Eleft += deltaC;
                        Proceso = 5;
                        break;

                    case 5:
                        if (cuentas > CuentasAnt)
                        {

                            Eleft += deltaC;
                        }
                        else
                        {
                            Eleft -= deltaC;
                            Proceso = 6;
                        }
                        break;
                    /////  Eright
                    case 6:

                        Proceso = 7;
                        Eright += pasos;


                        break;

                    case 7:
                        deltaC = (cuentas - CuentasAnt) / (cuentas + CuentasAnt) * 10;
                        if (deltaC > 0.2) deltaC = 0.2;
                        if (deltaC < -0.2) deltaC = -0.2;
                        Eright += deltaC;
                        Proceso = 8;
                        break;

                    case 8:
                        if (cuentas > CuentasAnt)
                        {

                            Eright += deltaC;
                        }
                        else
                        {
                            Eright -= deltaC;
                            Proceso = 0;
                        }
                        break;




                }
                CuentasAnt = cuentas;
                if (cuentas > umbral / 0.97)
                    umbral = cuentas * 0.97;

                if (ETop > 2) ETop = 0;
                if (ETop < -2) ETop = 0;

                if (Eleft > 2) Eleft = 0;
                if (Eleft < -2) Eleft = 0;

                if (Eright > 2) Eright = 0;
                if (Eright < -2) Eright = 0;

                ETop = Math.Round(ETop, 2);
                Eleft = Math.Round(Eleft, 2);
                Eright = Math.Round(Eright, 2);
            }
        }

    }

  
}
