using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace ALICE_QKD_4D
{
    class IO_Propiedades
    {
        public string DireccionArchivoPropiedades = "propiedades.txt";
        public string[,] matrizPropiedades = new string[100, 2];
        public string carpetaPublic = "archivo";
        public int NumArchivo = 0;
        string directorioInicial = @"Escritorio";

        public bool direccionGuardar()
        {

            bool CarpaetaOk = false;
            FolderBrowserDialog s = new FolderBrowserDialog();
            s.SelectedPath = directorioInicial;
            string mm = DateTime.Now.ToString(), timeSt = "\\"; ;
            for (int i = 0; i < mm.Length; i++)
            {
                if (mm[i] == '/' || mm[i] == ' ' || mm[i] == ':')
                    timeSt += '-';
                else
                    timeSt += mm[i];
            }

            if (s.ShowDialog() == DialogResult.OK)
            {
                carpetaPublic = s.SelectedPath + timeSt;
                System.IO.Directory.CreateDirectory(carpetaPublic);
                directorioInicial = s.SelectedPath;
                CarpaetaOk = true;
            }

            return CarpaetaOk;
        }


        public void generaArchivo(string NombreArchivo)
        {

            StreamWriter writer2 = File.AppendText(carpetaPublic + "\\" + NombreArchivo);
            writer2.Write("");
            writer2.Close();
        }
        public void GuardarEnArchivo(string NombreArchivo, string data)
        {
            StreamWriter file4 = File.AppendText(carpetaPublic + "\\" + NombreArchivo);

            file4.WriteLine(data);
            file4.Close();

        }

        public void reset()
        {
            string data = "";
            for (int i = 0; i < 10; i++)
            {
                data += "V" + i.ToString() + "\t" + "0" + "\r\n";
            }
            StreamWriter sw = new StreamWriter(carpetaPublic);
            sw.Write(data);
            sw.Close();

        }
        public void guardar(int index, string NombrePropiedad, string ValorPropiedad)
        {
            matrizPropiedades[index, 0] = NombrePropiedad;
            matrizPropiedades[index, 1] = ValorPropiedad;

            string data = "";
            for (int i = 0; i < 40; i++)
            {
                data += matrizPropiedades[i, 0] + "\t" + matrizPropiedades[i, 1] + "\r\n";
            }
            StreamWriter sw = new StreamWriter(DireccionArchivoPropiedades);
            sw.Write(data);
            sw.Close();
        }
        public decimal cargar(int index,decimal ValorAnt )
        {
            decimal d = 0;
            if (matrizPropiedades[index, 0] == "NONE")
                d = ValorAnt;
            else
                d = Convert.ToDecimal(matrizPropiedades[index, 1]);

            return d;

        }
        public void leerPRopiedades()
        {
            StreamReader sr = new StreamReader(DireccionArchivoPropiedades);
            string dataIn = sr.ReadToEnd();
            sr.Close();
            char c;
            byte b;
            int lmax = dataIn.Length;
            int index = 0;
            string s0 = "";
            for (int i = 0; i < lmax; i++)
            {

                c = dataIn[i];
                b = (byte)c;
                if (b == 9)
                {
                    if(s0=="")
                        matrizPropiedades[index, 0] = "NONE";
                    else
                    matrizPropiedades[index, 0] = s0;
                    s0 = "";
                }
                else
                {
                    if ((byte)dataIn[i] == 13)
                    {
                        matrizPropiedades[index, 1] = s0;
                        index++;
                    }
                    else
                    {
                        if ((byte)dataIn[i] == 10)
                            s0 = "";
                        else
                            s0 = s0 + dataIn[i];
                    }
                }

            }

        }
    }
}
