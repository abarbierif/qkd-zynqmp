
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace ALICE_QKD_4D
{
    class graficos
    {

        double[,] MatrizGrafico = new double[10, 1000];
        int index_matris = 0;
        int[] Num = new int[10];
        double[] maxindex2 = new double[10];
        public double[] graficar3entradas(PictureBox p, double[] data, int Npuntos, int Ngraficos, double[] maxindex) //numinicio=17 desface=0
        {


            for (int j = 0; j < Ngraficos; j++)
            {
                MatrizGrafico[j, index_matris] = data[j];
            }
            if (index_matris < Npuntos)
            {

                index_matris++;
            }
            else
            {

                for (int i = 1; i < index_matris + 1; i++)
                {
                    for (int j = 0; j < Ngraficos; j++)
                    {
                        MatrizGrafico[j, i - 1] = MatrizGrafico[j, i];
                    }

                }
            }
            for (int j = 0; j < Ngraficos; j++)
            {
                if (maxindex[j] < data[j] + 6)
                    maxindex[j] = data[j] + 6;
                maxindex2[j] = maxindex[j];
            }


            Bitmap imagen_fondo = new Bitmap(p.Width, p.Height);



            for (int w = 0; w < index_matris; w++)
            {
                for (int j = 0; j < Ngraficos; j++)
                {
                    Num[j] = (int)(p.Height - (MatrizGrafico[j, w] * p.Height / maxindex[j]));
                    if (Num[j] < 2) { Num[j] = 2; }
                    if (Num[j] > p.Height - 6) { Num[j] = p.Height - 6; }

                }


                for (int ww = 0; ww < 4; ww++)
                {
                    for (int hh = -2; hh < 2; hh++)
                    {

                        for (int j = 0; j < Ngraficos; j++)
                        {
                            int pixel = Num[j] - hh;
                            imagen_fondo.SetPixel((w) * 5 + ww, pixel, ponerColor(j));

                        }

                    }
                }

            }

            p.Image = imagen_fondo;
            return maxindex2;
        }

        private Color ponerColor(int j)
        {
            Color c = new Color();
            switch (j)
            {
                case 0: c = Color.Red; break;
                case 1: c = Color.Green; break;
                case 2: c = Color.Blue; break;
                case 3: c = Color.Black ; break;
                case 4: c = Color.Yellow; break;
                case 5: c = Color.Gray; break;
                case 6: c = Color.DarkOrange; break;
                case 7: c = Color.HotPink; break;
                case 8: c = Color.LightGray; break;
                case 9: c = Color.GreenYellow; break;


            }
            return c;

        }


        public byte[] txt2Vector(string textbox, int largo)
        {
            byte[] matriz1 = new byte[largo];//mínimo espacio es 1
            string dato = "";
            int index = 0;

            for (int i = 0; i < textbox.Length; i++)
            {

                byte num = (byte)textbox[i];

                //     listBox1.Items.Add("s=" + entrada[i].ToString() + " int=" + num + " char=" + (char)num);
                if (num == 9 || num == 32)
                {
                    try
                    {
                        matriz1[index] = Byte.Parse(dato);
                        index++;

                    }
                    catch { }
                    dato = "";

                }
                else if (num == 13)
                {
                    try
                    {
                        matriz1[index] = Byte.Parse(dato);
                        index++;


                    }
                    catch { }
                    dato = "";

                }
                else { dato += textbox[i]; }
                if (num == 10)
                {
                    dato = "";

                }
            }

            return matriz1;
        }
        int operative = 0;
        public string operativo()
        {
            string st = "";
            switch (operative)
            {
                case 0: operative = 1; st = "|"; break;
                case 1: operative = 2; st = "/"; break;
                case 2: operative = 3; st = "-"; break;
                case 3: operative = 0; st = "\\"; break;

            }
            return st;
        }
        public string RegistroAccionesUDP(byte[] registro)
        {
            string st = "";
            switch (registro[0])
            {
                //Punto de vista ALICE
                case 0: st = "Env: Solicitando bases Index: " + registro[1].ToString(); break;
                case 1: st = "Rec:      Bases de Bob index: " + registro[1].ToString(); break;
                case 2: st = "Rec:       Error Bases index: " + registro[1].ToString(); break;

                case 3: st = "Int:  Analizando Bases index: " + registro[1].ToString(); break;

                case 4: st = "Env:   Resultado Bases index: " + registro[1].ToString(); break;
                case 5: st = "Rec: parte de la clave index: " + registro[1].ToString(); break;
                case 6: st = "Rec:      Error  clave index: " + registro[1].ToString(); break;

                case 7: st = "Int:  Analizando clave index: " + registro[1].ToString(); break;
                case 8: st = "Int:  obteniendo QBER index: " + registro[1].ToString(); break;
                case 9: st = "Env:      Orden clave index: " + registro[1].ToString(); break;
                case 10: st = "Rec:        AKC final index: " + registro[1].ToString(); break;


                //Punto de vista BOBases
                case 100: st = "Rec: Peticion de bases index: " + registro[1].ToString(); break;
                case 101: st = "Env:    Bases Medicion index: " + registro[1].ToString(); break;

                case 102: st = "Rec: Igualdad de Bases index: " + registro[1].ToString(); break;
                case 103: st = "Int:  obteniendo clave index: " + registro[1].ToString(); break;
                case 104: st = "Env: parte de la clave index: " + registro[1].ToString(); break;

                case 105: st = "Rec:      Oreden clave index: " + registro[1].ToString(); break;
                case 106: st = "Env:   agradesco comunicación index: " + registro[1].ToString(); break;

                case 255: st = "reset"; break;

            }
            return st;
        }


        public decimal graficarBarraSimple(PictureBox p, int[] data, int Nceldas, int maximo, bool AjustarMax) //numinicio=17 desface=0
        {

            int maximoLocal = maximo;
            int[] dataInt = new int[Nceldas];
            if (AjustarMax)
            {
                for (int j = 0; j < Nceldas; j++)
                {
                    if (maximoLocal < data[j] + 6)
                        maximoLocal = data[j] + 6;

                }
            }
            int MaxAux = 0;
            int wmax = p.Width - 2, hmax = p.Height - 2;
            for (int j = 0; j < Nceldas; j++)
            {
                MaxAux = (data[j] * hmax) / maximo;
                if (MaxAux > hmax)
                    MaxAux = hmax;

                dataInt[j] = hmax - MaxAux;


            }


            int wcelda = wmax / Nceldas - 2;
            Bitmap imagen_fondo = new Bitmap(wmax + 2, hmax + 2);


            int numCelda = 0;
            for (int cel = 0; cel < wmax - wcelda / 2; cel += wcelda + 2)
            {
                for (int h = 0; h < hmax - 6; h += 10)//primera linea divisora
                {
                    for (int hh = h; hh < h + 5; hh++)
                    {
                        imagen_fondo.SetPixel(cel, hh, Color.Gray);
                    }
                }
                for (int w = cel + 1; w < cel + wcelda; w++)
                {
                    for (int h = 0; h < hmax - 6; h++)//primera linea divisora
                    {
                        if (h > dataInt[numCelda])
                            imagen_fondo.SetPixel(w, h, Color.Orange);
                    }

                }
                numCelda++;
            }
            for (int h = 0; h < hmax - 6; h += 10)//primera linea divisora
            {
                for (int hh = h; hh < h + 5; hh++)
                {
                    imagen_fondo.SetPixel(wmax - 2, hh, Color.Gray);
                }
            }
            p.Image = imagen_fondo;
            decimal dm = (decimal)maximoLocal;
            return dm;


        }

    }
}
