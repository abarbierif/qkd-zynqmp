using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Windows.Forms;
using System.IO.Ports;


namespace ALICE_QKD_4D
{
    class Phases
    {

         public int Nphases = 1000;
        public int[,] LeerPhases(string nombre, string extension )
        {

            char separador = '\t';

            int[,] dataP = new int[1000, 3];
            StreamReader sr = new StreamReader(@"Parameter\" + nombre + extension);
            string entrada = sr.ReadToEnd();
            sr.Close();
            int Stmax = entrada.Length;

            string dato = "";
            int numAct = 0;
            int k = 0;
            int r = 0;
            for (int i = 0; i < Stmax; i++)
            {

                byte num = (byte)entrada[i];

                if (num == separador)
                {


                    numAct = Convert.ToInt16(dato);
                    if(numAct==255)
                    break;
                    Nphases = k;
                    dataP[k,r] = numAct;

                    
                    r++;
                     dato = "";
                    

                }
                else if (num == 13)
                {
                    numAct = Convert.ToInt16(dato);
                    dataP[k, 2] = numAct;
                    dato = "";

                }
                else if (num == 10)
                {
                    r = 0;
                  
                    k++;
                    dato = "";

                }
                else
                {
                    dato += entrada[i];
                }
                
            }

            return dataP;

        }



    }
}
