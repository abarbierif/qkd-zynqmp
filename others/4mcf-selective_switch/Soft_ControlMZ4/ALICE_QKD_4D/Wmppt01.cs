using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALICE_QKD_4D
{
    class Wmppt01
    {
        public double umbralControl = 1000;
        public double pasosControlSet = 0.1;
        public double pasosControl1 = 0.2;
        public double pasosControl2 = 0.2;
        public double pasosControl3 = 0.2;
 
        public double ETop = 0, Eright = 0, Eleft = 0;
        int Proceso1 = 0;
        public double minComp = -2, maximoComp = 2;
        public bool ModulacionOn = false;
        public double cuentasA = 0;
        double cuentasAnterior = 0;
        double DTop=0, DRight=0, DLeft = 0,delta=0;
        double maximoLocal = 0;


        bool controlOk = false;
        public bool WControlmppt(int cuentasExt, int MaxIntegrar)//bool EnETop, bool EnEright, bool EnEbottom, bool EnEleft,
        {

            cuentasA = cuentasExt;
              
                    switch (Proceso1)
                    {
                        case 0:

                            if (cuentasA < umbralControl * 0.7)
                            {
                                controlOk = false;
                                Proceso1 = 10;
                                pasosControlSet = 0.2;
                                ETop += pasosControlSet;
                            }
                            else
                            {
                                if (cuentasA < umbralControl * 0.9)
                                {
                                    controlOk = false;
                                    Proceso1 = 10;
                                    pasosControlSet = 0.1;
                                    ETop += pasosControlSet;
                                }
                                else
                                {
                                    if (cuentasA < umbralControl)
                                    {
                                        controlOk = false;
                                        Proceso1 = 10;
                                        pasosControlSet = 0.02;
                                        ETop += pasosControlSet;
                                    }
                                    else
                                    { controlOk = true; }
 
                                }
 
                            }
 
                            break;

                        case 10:
                            if(cuentasA>cuentasAnterior)
                                delta = pasosControlSet;
                            else
                                delta = -pasosControlSet;

                            ETop += delta;
                            Proceso1 = 11;
                           break;
                        case 11:
                           if (cuentasA < cuentasAnterior)
                           {
                               ETop -= delta;
                               Proceso1 = 12;
                           }
                           else
                               ETop += delta;
                           break;

                        ///Eright
                        case 12:
                              Proceso1 = 13;
                              Eright += pasosControlSet;
                           break;
                        case 13:
                           if (cuentasA > cuentasAnterior)
                               delta = pasosControlSet;
                           else
                               delta = -pasosControlSet;
                           Eright += delta;
                           Proceso1 = 14;
                           break;
                        case 14:
                           if (cuentasA < cuentasAnterior)
                           {
                               Eright -= delta;
                               Proceso1 = 15;
                           }
                           else
                               Eright += delta;
                           break;

                        ///Eleft
                        case 15:
                           Proceso1 = 16;
                           Eleft += pasosControlSet;
                           break;
                        case 16:
                           if (cuentasA > cuentasAnterior)
                               delta = pasosControlSet;
                           else
                               delta = -pasosControlSet;
                           Eleft += delta;
                           Proceso1 = 17;
                           break;
                        case 17:
                           if (cuentasA < cuentasAnterior)
                           {
                               Eleft -= delta;
                               Proceso1 = 0;
                           }
                           else
                               Eleft += delta;
                           break;
                       //buscagrueso;
                        case 1:
                           ETop += pasosControlSet;
                            if (cuentasA > maximoLocal)
                            {
                                DTop = ETop - pasosControlSet;
                                maximoLocal = cuentasA;
                           }
                            else
                            {
                                ETop = DTop;
                                maximoLocal = 0;
                                Eright = -1;
                                Proceso1 = 2;
                            }
                           

                            break;

                        case 2:

                            Eright += pasosControlSet;
                            if (cuentasA > maximoLocal)
                            {
                                DRight = Eright - pasosControlSet;
                                maximoLocal = cuentasA;
                   
                            }
                            else
                            {
                                Eright = DRight;
                                maximoLocal = 0;
                                Eleft = -1;
                                Proceso1 = 3;
                            }

                           
                            
                            break;

                        case 3:

                            Eleft += pasosControlSet;
                            if (cuentasA > maximoLocal)
                            {
                                DLeft = Eleft - pasosControlSet;
                                maximoLocal = cuentasA;
                               }
                            else
                            {
                                Eleft = DLeft;
                                maximoLocal = 0;
                                Proceso1 = 0;
                            }
                            
                            break;
                        
                       

                    }

                    if (cuentasA * .97 > umbralControl)
                        umbralControl = cuentasA * .97;

                    if (ETop > maximoComp) ETop = -1;
                    if (ETop < minComp) ETop = 1;

                    if (Eright > maximoComp) Eright = -1;
                    if (Eright < minComp) Eright = 1;

                    if (Eleft > maximoComp) Eleft = -1;
                    if (Eleft < minComp) Eleft = 1;

            cuentasAnterior = cuentasA;
            return controlOk;
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
