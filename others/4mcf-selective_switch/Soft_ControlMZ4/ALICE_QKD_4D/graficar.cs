using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ALICE_QKD_4D
{
    class graficar
    {
        graficos graf6 = new graficos();
        double[] graficarC6 = new double[10];
        double[] maxdataC6 = new double[10];

        public void graficar6Entradas(int Cs_1, int Cs_2, int Cs_3, int Cs_4, int Ref, int Sum, NumericUpDown Nd, PictureBox p)
        {

            graficarC6[0] = Cs_1;
            graficarC6[1] = Cs_2;
            graficarC6[2] = Cs_3;
            graficarC6[3] = Cs_4;
            graficarC6[4] = Ref;
            graficarC6[5] = Sum;

            maxdataC6[0] = (int)Nd.Value;
            maxdataC6[1] = (int)Nd.Value;
            maxdataC6[2] = (int)Nd.Value;
            maxdataC6[3] = (int)Nd.Value;
            maxdataC6[4] = (int)Nd.Value;
            maxdataC6[5] = (int)Nd.Value;

            maxdataC6 = graf1.graficar3entradas(p, graficarC6, p.Width / 6, 6, maxdataC6);
            if (Cs_1 > Nd.Value)
                Nd.Value = (decimal)Cs_1;
            if (Cs_2 > Nd.Value)
                Nd.Value = (decimal)Cs_2;
            if (Cs_3 > Nd.Value)
                Nd.Value = (decimal)Cs_3;
            if (Cs_4 > Nd.Value)
                Nd.Value = (decimal)Cs_4;
            if (Ref > Nd.Value)
                Nd.Value = (decimal)Ref;
            if (Sum > Nd.Value)
                Nd.Value = (decimal)Sum;
        }




        graficos graf1 = new graficos();
        double[] graficarC1 = new double[10];
        double[] maxdataC1 = new double[10];
      
        public void graficar5Entradas(int Cs_1, int Cs_2, int Cs_3, int Cs_4,int Ref ,NumericUpDown Nd, PictureBox p)
        {

            graficarC1[0] = Cs_1;
            graficarC1[1] = Cs_2;
            graficarC1[2] = Cs_3;
            graficarC1[3] = Cs_4;
            graficarC1[4] = Ref;

            maxdataC1[0] = (int)Nd.Value;
            maxdataC1[1] = (int)Nd.Value;
            maxdataC1[2] = (int)Nd.Value;
            maxdataC1[3] = (int)Nd.Value;
            maxdataC1[4] = (int)Nd.Value;
 
            maxdataC1 = graf1.graficar3entradas(p, graficarC1, p.Width / 6, 5, maxdataC1);
            if (Cs_1 > Nd.Value)
                Nd.Value = (decimal)Cs_1;
            if (Cs_2 > Nd.Value)
                Nd.Value = (decimal)Cs_2;
            if (Cs_3 > Nd.Value)
                Nd.Value = (decimal)Cs_3;
            if (Cs_4 > Nd.Value)
                Nd.Value = (decimal)Cs_4;
            if (Ref > Nd.Value)
                Nd.Value = (decimal)Ref;
        }


        graficos graf2 = new graficos();
        double[] graficarC2 = new double[10];
        double[] maxdataC2 = new double[10];
     
        public void graficar4Entradas(int Cs_1, int Cs_2, int Cs_3, int Cs_4,  NumericUpDown Nd, PictureBox p)
        {

            graficarC2[0] = Cs_1;
            graficarC2[1] = Cs_2;
            graficarC2[2] = Cs_3;
            graficarC2[3] = Cs_4;
 

            maxdataC2[0] = (int)Nd.Value;
            maxdataC2[1] = (int)Nd.Value;
            maxdataC2[2] = (int)Nd.Value;
            maxdataC2[3] = (int)Nd.Value;
           

            maxdataC2 = graf1.graficar3entradas(p, graficarC2, p.Width / 6, 4, maxdataC2);
            if (Cs_1 > Nd.Value)
                Nd.Value = (decimal)Cs_1;
            if (Cs_2 > Nd.Value)
                Nd.Value = (decimal)Cs_2;
            if (Cs_3 > Nd.Value)
                Nd.Value = (decimal)Cs_3;
            if (Cs_4 > Nd.Value)
                Nd.Value = (decimal)Cs_4;

        }
    }
}
