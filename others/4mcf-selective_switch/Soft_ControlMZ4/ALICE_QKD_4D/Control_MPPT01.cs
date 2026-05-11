using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALICE_QKD_4D
{
    class Control_MPPT01
    {
        public int umbralControlAnt = 7000;
        public double pasosControlSet = 0.2;
        public double pasosControl = 0.2;
        double[] Casillas = new double[4];
        public double ETop = 0, Eright = 0, Ebottom = 0, Eleft = 0;
        int procesoControlAntiguo = 0;
        public double minComp = -1, maximoComp = 2;
        int ValorAanterior = 0;
        int Niteracion = 2;

        private double suma(double Valor, double pasos)
        {
            Valor += pasos;

            if (Valor > maximoComp) Valor = 0;// minComp + (Valor - maximoComp);
            return Valor;
        }
        private double resta(double Valor, double pasos)
        {
            Valor -= pasos;

            if (Valor < minComp) Valor =  0;//maximoComp + (Valor - minComp);
            return Valor;
        }
        int ProbuscaM = 0;
        bool RedyBuscar = false;
        private double buscaM(double Vcas, int cuentas, double pasos)
        {
            switch (ProbuscaM)
            {
                case 0: RedyBuscar = false; ProbuscaM = 1; Vcas = suma(Vcas, pasos); break;
                case 1:
                    if (cuentas < ValorAanterior)
                    {
                        ProbuscaM = 2;
                        Vcas = resta(Vcas, pasos);
                        Vcas = resta(Vcas, pasos);

                       
                    }
                    else { Vcas = suma(Vcas, pasos); }

                    break;

                case 2:

                    if (cuentas < ValorAanterior)
                    {
                        ProbuscaM = 0;
                        Vcas = suma(Vcas, pasos);
                        RedyBuscar = true;

                         
                    }
                    else
                    {
                        Vcas = resta(Vcas, pasos);
                        

                    }
                    break;
            }

           
            ValorAanterior = cuentas;
            return Vcas;
        }
        int ProcBuscaMAx = 0;
        private void BuscaMAx(int cuentas, double pasos)
        {
            switch (ProcBuscaMAx)
            {

                case 0:
                    ETop = buscaM(ETop, cuentas, pasos); if (RedyBuscar) ProcBuscaMAx = 1;

                    break;

                case 1:
                    Eleft = buscaM(Eleft, cuentas, pasos); if (RedyBuscar) ProcBuscaMAx = 3;
                    break;

             // case 2:
             //    Ebottom = buscaM(Ebottom, cuentas, pasos); if (RedyBuscar) ProcBuscaMAx = 3;
            //     break;

                case 3:
                    Eright = buscaM(Eright, cuentas, pasos); if (RedyBuscar) ProcBuscaMAx = 4;
                    break;


                case 4:
                    ProcBuscaMAx = 0;///////////////////////////cambiarv a 0

                    if (Niteracion > 2)
                    {
                        Niteracion = 0;
                        Eright = 0;
                        Ebottom = 0;
                        Eleft = 0;
                        ETop = 0;
                    }
                    else Niteracion++;

                    break;

            }
        }
        int ProbuscaMi = 0;
        private double buscaMi(double Vcas, int cuentas, double pasos)
        {
            switch (ProbuscaMi)
            {
                case 0: RedyBuscar = false; ProbuscaMi = 1; Vcas = suma(Vcas, pasos); break;
                case 1:
                    if (ValorAanterior < cuentas)
                    {
                        ProbuscaMi = 2;
                        Vcas = resta(Vcas, pasos);
                    }
                    else { Vcas = suma(Vcas, pasos); }

                    break;

                case 2:

                    if (ValorAanterior < cuentas)
                    {
                        ProbuscaMi = 0;
                        Vcas = suma(Vcas, pasos);
                        RedyBuscar = true;
                    }
                    else
                    {
                        Vcas = resta(Vcas, pasos);

                    }
                    break;
            }
            ValorAanterior = cuentas;
            return Vcas;
        }
        int procBuscaMin = 0;
        private void BuscaMin(int cuentas, double pasos)
        {
            switch (procBuscaMin)
            {

                case 0:
                    ETop = buscaMi(ETop, cuentas, pasos); if (RedyBuscar) procBuscaMin = 1;

                    break;

                case 1:
                    Eleft = buscaMi(Eleft, cuentas, pasos); if (RedyBuscar) procBuscaMin = 2;
                    break;

                case 2:
                    Ebottom = buscaMi(Ebottom, cuentas, pasos); if (RedyBuscar) procBuscaMin = 3;

                    break;

                case 3:
                    Eright = buscaMi(Eright, cuentas, pasos); if (RedyBuscar) procBuscaMin = 4;
                    break;


                case 4:
                    procBuscaMin = 0;///////////////////////////cambiarv a 0



                    break;

            }
        }
        bool ModulandoEst = false;
        int CuentaPasos = 0;
        int MAxPasosModEst = 100;
        int Proc_ModEst = 0;
        bool Modula_Est(double pasos)
        {
            switch (Proc_ModEst)
            {
                case 0:
                    CuentaPasos++;
                    ModulandoEst = false;
                    if (CuentaPasos > MAxPasosModEst / 2)
                    {
                        Proc_ModEst = 1;
                        CuentaPasos = 0;
                    }
                    break;
                case 1:
                    CuentaPasos++;
                    ETop = suma(ETop, pasos);
                    if (CuentaPasos > MAxPasosModEst)
                    {
                        Proc_ModEst = 2;
                        CuentaPasos = 0;
                    }
                    break;

                case 2:

                    CuentaPasos++;
                    Eleft = suma(Eleft, pasos);
                    if (CuentaPasos > MAxPasosModEst)
                    {
                        Proc_ModEst = 3;
                        CuentaPasos = 0;
                    }
                    break;

                case 3:
                    CuentaPasos++;
                    Ebottom = suma(Ebottom, pasos);
                    if (CuentaPasos > MAxPasosModEst)
                    {
                        Proc_ModEst = 4;
                        CuentaPasos = 0;
                    }
                    break;

                case 4:
                    CuentaPasos++;
                    Eright = suma(Eright, pasos);
                    if (CuentaPasos > MAxPasosModEst)
                    {
                        Proc_ModEst = 5;
                        CuentaPasos = 0;
                    }

                    break;

                case 5:
                    CuentaPasos++;
                    if (CuentaPasos > MAxPasosModEst / 2)
                    {
                        Proc_ModEst = 0;
                        CuentaPasos = 0;
                        ModulandoEst = true;
                    }
                    break;

            }
            return ModulandoEst;
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
       
        public double UmbralC  = 1000;
        public int Umbral_infM = 7100;
        public int Umbral_supM = 7200;
        public int Umbral_sup = 7300;
        public bool ModulacionOn = false;
        int cuentasA = 0;
        bool ControlOk = false;
        public bool  Controlmppt(int cuentasExt,int MaxIntegrar)//bool EnETop, bool EnEright, bool EnEbottom, bool EnEleft,
        {
            

                if (Integrar(MaxIntegrar, cuentasExt))
                {
                    if (cuentasA < UmbralC * 0.9)  //bajo el umbral inferior
                    {
                        ControlOk = false;
                        pasosControl = pasosControlSet;
                        BuscaMAx(cuentasA, pasosControl);

                    }
                    else
                    {
                        if (cuentasA < UmbralC )  //bajo el umbral inferior
                        {
                            ControlOk = false;
                            pasosControl = 0.02;
                            BuscaMAx(cuentasA, pasosControl);

                        }
                        else {

                            ControlOk = true;
                            Niteracion = 0; }
                    }
                    
                   
                }
                if (cuentasA > UmbralC / 0.97)
                    UmbralC = cuentasA * 0.97;

               
                return ControlOk;

        }
    }
}

