using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALICE_QKD_4D
{
    class Control2
    {
         
        public int umbralControl = 7000;
        public double pasosControlSet = 0.2;
        public double pasosControl1 = 0.2;
        public double pasosControl2 = 0.2;
        public double pasosControl3 = 0.2;
 
        public double ETop = 0, Eright = 0, Eleft = 0;
        int Proceso1 = 0;
        public double minComp = -2, maximoComp = 2;
        public bool ModulacionOn = false;
        int cuentasA = 0;
        int cuentasAnterior = 0;
        double DTop=0, DRight=0, DLeft = 0;
        int Ncilcos = 0;
        double pasosControl = 0;
        public void control2(int cuentasExt, int MaxIntegrar)//bool EnETop, bool EnEright, bool EnEbottom, bool EnEleft,
        {

             pasosControl = (umbralControl - cuentasA) / (umbralControl + cuentasA);

            if (Integrar(MaxIntegrar, cuentasExt))
            {
                if (cuentasA > umbralControl)
                {
                    umbralControl = cuentasA*97/100;
                    Proceso1 = 0;
                }
                else
                {
                    switch (Proceso1)
                    {
                        case 0:
                            ETop += pasosControl;
                            Proceso1 = 1;
                            break;

                        case 1: 
                          
                            
                            if (cuentasA < cuentasAnterior)
ETop += pasosControl ;
                            Proceso1 = 2;
             
                            break;
                        case 2:
                            Eleft += 0.05;
                            Proceso1 = 3;
                            break;

                        case 3:
                        
                            Eleft += pasosControl ;
                            if (cuentasA < cuentasAnterior)
                                Proceso1 = 4;

                            break;
                        case 4:
                            Eright += 0.05;
                            Proceso1 = 5;
                            break;

                        case 5:
                          
                            Eright += pasosControl ;
                            if (cuentasA < cuentasAnterior)
                            {
                                Ncilcos++;
                                if(Ncilcos>4)
                                Proceso1 = 6;
                                else
                                    Proceso1 = 0;
                            }
                            break;
                      
                        case 6:
                            Ncilcos = 0;
                            Eright = 0;

                            Eleft = 0;
                            ETop = 0;
                            Proceso1 = 0; break;

                    }
                }


            }
            cuentasAnterior = cuentasA;
            if (ETop > maximoComp) ETop = 0;
            if (ETop < minComp) ETop = 0;

            if (Eright > maximoComp) Eright = 0;
            if (Eright < minComp) Eright = 0;

            if (Eleft > maximoComp) Eleft = 0;
            if (Eleft < minComp) Eleft = 0;
        }
        int Count_Integrar = 0;
        int AccIntegrar = 0;
     

        private bool Integrar(int MaxIntegrar, int cuentas)
        {
            bool integraOk = false;
            AccIntegrar += cuentas;
            Count_Integrar++;
            if (MaxIntegrar - 1 < Count_Integrar)
            {
                integraOk = true;
                cuentasA = AccIntegrar / MaxIntegrar;
                AccIntegrar = 0;
                Count_Integrar = 0;
            }
            return integraOk;

        }
    }
}

 