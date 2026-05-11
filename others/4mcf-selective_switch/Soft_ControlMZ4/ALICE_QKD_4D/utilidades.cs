using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Windows.Forms;
using System.IO.Ports;

namespace ALICE_QKD_4D
{
    class utilidades
    {


        public double[] LeerDouble(string nombre, string extension, int lmax)
        {

            char separador = '\t';
            double[] dataP = new double[lmax + 1];
            StreamReader sr = new StreamReader(@"Parameter\" + nombre + extension);
            string entrada = sr.ReadToEnd();
            sr.Close();
            int Stmax = entrada.Length;

            string dato = "";
            double numAct = 0;
            int k = 0;
            for (int i = 0; i < Stmax; i++)
            {

                byte num = (byte)entrada[i];

                if (num == separador)
                {


                    numAct = Convert.ToDouble(dato);
                    dataP[k] = numAct;
                    dato = "";
                    k++;

                }
                else { dato += entrada[i]; }
                if (k > lmax) break;
            }

            return dataP;

        }

        public void Guardardouble(string nombre, string extension, double[] data, int lmax)
        {

            string s = ""; char separador = '\t';
            for (int i = 0; i < lmax + 1; i++)
            {
                s += data[i].ToString();
                s += separador;
            }


           System.IO.File.WriteAllText(@"Parameter\" + nombre + extension, "");//Borrar la data
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"Parameter\" + nombre + extension);
            file.Write(s);
            file.WriteLine(nombre + extension);
            file.Close();

        }
        string s = "";
        public bool ingresaEnter(TextBox t)
        {
            bool enter = false;
            s = t.Text;
            int n = s.Length - 1;
            if (n > -1)
            {
                byte b = (byte)s[n];
                if (b == 10) enter = true;
            }


            return enter;
        }

        public byte VerKey(string t)
        {
            byte b = 0;
            s = t;

            for (int i = 0; i < s.Length; i++)
            {
                b = (byte)s[i];
                if (b == 10 || b == 42 || b == 47)
                    break;
            }




            return b;
        }

        public int[] tiempoMinutosSeg(int t)
        {
            int[] tiempoout = new int[2];
            int minutos = t / 60;
            int seg = t - minutos * 60;

            tiempoout[0] = minutos;
            tiempoout[1] = seg;



            return tiempoout;
        }

        public double str2double(double valorAnt, TextBox t, double max, double pasos)
        {
            valorAnt = Math.Round(valorAnt * 100000) / 100000;
            bool enable = false;
            char okp = ',';
            char badp = '.';
            string ss = "0.1", ss2 = "0,1";

            if (Convert.ToDouble(ss) < Convert.ToDouble(ss2))
            { okp = '.'; badp = ','; }
            string s = t.Text;
            string s1 = "";
            byte b = VerKey(s);
            if (b == 10)
            {


                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == badp)
                        s1 += okp;
                    else
                        if (s[i] == okp || s[i] == '-' || s[i] == '0' || s[i] == '1' || s[i] == '2' || s[i] == '3' || s[i] == '4' || s[i] == '5' || s[i] == '6' || s[i] == '7' || s[i] == '8' || s[i] == '9')
                            s1 += s[i];
                }

                try
                {
                    valorAnt = Convert.ToDouble(s1);
                    // NumUsbDriversEspejo = Convert.ToInt32(toolStripTextBox_NusbEspejo.Text);
                    enable = true;
                }
                catch
                {

                }

            }

            else if (b == 42)
            {
                valorAnt += pasos;
                enable = true;
            }
            else if (b == 47)
            {
                valorAnt -= pasos;
                enable = true;
            }

            if (enable)
            {
                if (valorAnt > max) valorAnt = max;
                if (valorAnt < -max) valorAnt = -max;


                t.Text = valorAnt.ToString();
                // 
                //   t.Text = valorAnt.ToString();
            }

            return valorAnt;

        }
        public int string2int(int valorAnt, ToolStripTextBox t, int max, int pasos)
        {



            string s = t.Text;
            string s1 = "";



            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '-' || s[i] == '0' || s[i] == '1' || s[i] == '2' || s[i] == '3' || s[i] == '4' || s[i] == '5' || s[i] == '6' || s[i] == '7' || s[i] == '8' || s[i] == '9')
                    s1 += s[i];
            }

            try
            {
                valorAnt = Convert.ToInt32(s1);
                // NumUsbDriversEspejo = Convert.ToInt32(toolStripTextBox_NusbEspejo.Text);

            }
            catch
            {

            }




            t.Text = valorAnt.ToString();

            return valorAnt;

        }


        public int[] sumarVectores(int[] V1, int[] V2, int lmax)
        {
            int[] vectorsuma = new int[lmax + 1];
            for (int i = 0; i < lmax + 1; i++)
            {
                vectorsuma[i] = V1[i] + V2[i];

            }
            return vectorsuma;
        }

        public void GuardarData8bit(string nombre, string extension, int[] data, int lmax)
        {
            byte[] dataP = new byte[20];

            System.IO.File.WriteAllText(@"Parameter\" + nombre + extension, "");//Borrar la data
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"Parameter\" + nombre + extension);
            for (int i = 0; i < lmax + 1; i++)
                file.WriteLine(data[i]);
            file.WriteLine(nombre + extension);
            file.Close();


        }
        public int[] LeerData8bit(string nombre, string extension, int lmax)
        {

            int[] dataP = new int[lmax + 1];
            StreamReader sr = new StreamReader(@"Parameter\" + nombre + extension);
            string entrada = sr.ReadToEnd();
            int Stmax = entrada.Length;
            sr.Close();

            string dato = "";
            int numAct = 0;
            int k = 0;
            for (int i = 0; i < Stmax; i++)
            {

                byte num = (byte)entrada[i];
                if (num == 9 || num == 32)
                {
                    dato = "";
                }
                else if (num == 13)
                {


                    numAct = Convert.ToInt32(dato);
                    dataP[k] = numAct;

                    dato = "";
                    k++;

                }
                else { dato += entrada[i]; }
                if (num == 10)
                {
                    dato = "";

                }
                if (k > lmax) break;



            }

            return dataP;

        }
        public int[] matrizEP2vector(int[,] ValoresEstadoProy, int index)
        {
            int[] vector = new int[37];
            //int[,] ValoresEstadoProy = new int[10, 37];
            for (int i = 1; i < 37; i++)
            {
                vector[i] = ValoresEstadoProy[index, i];

            }
            //   valoresNum = Numeric2Vector();
            // ValoresEstadoProy = Utilidades.vector2matrizEP(valoresNum, (int)numericUpDown_estadoProyeccion.value);
            return vector;
        }
        public void vector2matrizEP(int[,] ValoresEstadoProy, int[] vector, int index)
        {

            for (int i = 1; i < 37; i++)
            {
                if (index > 0)
                    ValoresEstadoProy[index, i] = ValoresEstadoProy[0, i] + vector[i];
                else
                    ValoresEstadoProy[index, i] = vector[i];

            }
            //   valoresNum = Numeric2Vector();
            // ValoresEstadoProy = Utilidades.vector2matrizEP(valoresNum, (int)numericUpDown_estadoProyeccion.value);
            //    return ValoresEstadoProy;
        }


        public int str2int(int valorAnt, ToolStripTextBox t, int max)
        {
            try
            {
                valorAnt = Convert.ToInt32(t.Text);
                // NumUsbDriversEspejo = Convert.ToInt32(toolStripTextBox_NusbEspejo.Text);
            }
            catch
            {

            }
            if (valorAnt > max) valorAnt = max;
            t.Text = valorAnt.ToString();
            return valorAnt;

        }

        public ComboBox leerPuetosSerial()  //("w0=" + (dataP[0] * 256 + dataP[1]).ToString());
        {
            string namePort = "";
            ComboBox combox = new ComboBox();
            SerialPort sp = new SerialPort();

            combox.Items.Add("None");
            for (int i = 1; i < 100; i++)
            {
                if (sp.IsOpen)
                    sp.Close();

                namePort = "COM" + i.ToString();
                sp.PortName = namePort;
                try
                {
                    sp.Open();
                    combox.Items.Add(namePort);


                }
                catch
                {
                }

            }
            combox.Text = combox.Items[0].ToString();
            if (sp.IsOpen)
                sp.Close();
            return combox;
        }


        public int[,] LeerTexbox2Matriz(string textbox)
        {
            int[,] matriz1 = new int[255, 255];//mínimo espacio es 1
            string dato = "";
            int w = 0, h = 1, wmax = 0;

            for (int i = 0; i < textbox.Length; i++)
            {

                byte num = (byte)textbox[i];

                //     listBox1.Items.Add("s=" + entrada[i].ToString() + " int=" + num + " char=" + (char)num);
                if (num == 9 || num == 32)
                {
                    try
                    {
                        matriz1[h, w] = Int32.Parse(dato);
                        w++;

                    }
                    catch { }
                    dato = "";

                }
                else if (num == 13)
                {
                    try
                    {
                        matriz1[h, w] = Int32.Parse(dato);
                        wmax = w;
                        w = 0;
                        h++;


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
            matriz1[0, 0] = h - 1;
            matriz1[0, 1] = wmax;
            return matriz1;
        }
        public void GuardarParametros(string direccion, decimal w0,
            decimal h0, decimal nr, decimal hr, decimal espacio)
        {
            byte[] dataP = new byte[10];

            byte[] b = new byte[2];
            b = num2byte(w0);
            dataP[0] = b[1];
            dataP[1] = b[0];
            b = num2byte(h0);
            dataP[2] = b[1];
            dataP[3] = b[0];
            b = num2byte(nr);
            dataP[4] = b[1];
            dataP[5] = b[0];
            b = num2byte(hr);
            dataP[6] = b[1];
            dataP[7] = b[0];
            b = num2byte(espacio);
            dataP[8] = b[1];
            dataP[9] = b[0];
            System.IO.File.WriteAllText(@"Parameter\" + direccion, "");//Borrar la data
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"Parameter\" + direccion);
            file.WriteLine("w0\t" + (dataP[0] * 256 + dataP[1]).ToString());
            file.WriteLine("h0\t" + (dataP[2] * 256 + dataP[3]).ToString());
            file.WriteLine("Nr\t" + (dataP[4] * 256 + dataP[5]).ToString());

            file.WriteLine("Hr\t" + (dataP[6] * 256 + dataP[7]).ToString());
            file.WriteLine("E\t" + (dataP[8] * 256 + dataP[9]).ToString());
            file.WriteLine(direccion);
            file.Close();


        }
        public void GuardarParametrosLectura(string direccion, decimal w0,
           decimal h0, decimal nr, decimal hr, decimal espacio)
        {
            byte[] dataP = new byte[20];

            byte[] b = new byte[2];
            b = num32bit2byte(w0);
            dataP[0] = b[3];
            dataP[1] = b[2];
            dataP[2] = b[1];
            dataP[3] = b[0];
            b = num32bit2byte(h0);
            dataP[4] = b[3];
            dataP[5] = b[2];
            dataP[6] = b[1];
            dataP[7] = b[0];
            b = num32bit2byte(nr);
            dataP[8] = b[3];
            dataP[9] = b[2];
            dataP[10] = b[1];
            dataP[11] = b[0];
            b = num32bit2byte(hr);
            dataP[12] = b[3];
            dataP[13] = b[2];
            dataP[14] = b[1];
            dataP[15] = b[0];
            b = num32bit2byte(espacio);
            dataP[16] = b[3];
            dataP[17] = b[2];
            dataP[18] = b[1];
            dataP[19] = b[0];
            System.IO.File.WriteAllText(@"Parameter\" + direccion, "");//Borrar la data
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"Parameter\" + direccion);
            file.WriteLine("w0\t" + (dataP[0] * 16777216 + dataP[1] * 65536 + dataP[2] * 256 + dataP[3]).ToString());
            file.WriteLine("h0\t" + (dataP[4] * 16777216 + dataP[5] * 65536 + dataP[6] * 256 + dataP[7]).ToString());
            file.WriteLine("Nr\t" + (dataP[8] * 16777216 + dataP[9] * 65536 + dataP[10] * 256 + dataP[11]).ToString());
            file.WriteLine("Hr\t" + (dataP[12] * 16777216 + dataP[13] * 65536 + dataP[14] * 256 + dataP[15]).ToString());
            file.WriteLine("E\t" + (dataP[16] * 16777216 + dataP[17] * 65536 + dataP[18] * 256 + dataP[19]).ToString());
            file.WriteLine(direccion);
            file.Close();


        }
        public byte[] leerParametrosLectura(string direccion)
        {

            byte[] dataP = new byte[20];
            string entrada = "";

            StreamReader sr = new StreamReader(@"Parameter\" + direccion);
            entrada = sr.ReadToEnd();
            sr.Close();

            string dato = "";
            int t = 0;
            byte[] b = new byte[2];


            for (int i = 0; i < entrada.Length; i++)
            {

                byte num = (byte)entrada[i];
                if (num == 9 || num == 32)
                {
                    dato = "";
                }
                else if (num == 13)
                {
                    if (t < 20)
                    {

                        b = num32bit2byte(decimal.Parse(dato));
                        dataP[t] = b[3];
                        t++;
                        dataP[t] = b[2];
                        t++;
                        dataP[t] = b[1];
                        t++;
                        dataP[t] = b[0];
                        t++;

                    }
                    dato = "";

                }
                else { dato += entrada[i]; }
                if (num == 10)
                {
                    dato = "";

                }


            }

            return dataP;
        }
        public byte[] num32bit2byte(decimal numin)
        {
            /*  if (numin > 600)
                  numin = 600;
             * */
            byte[] b = new byte[4];
            int num = (int)numin;
            int b3 = num / 16777216;
            int b2 = (num - b3 * 16777216) / 65536;
            int b1 = (num - (b3 * 16777216 + b2 * 65536)) / 256;
            int b0 = num - (b3 * 16777216 + b2 * 65536 + b1 * 256);
            b[3] = (byte)b3;
            b[2] = (byte)b2;
            b[1] = (byte)b1;
            b[0] = (byte)b0;
            return b;

        }

        public byte[] leerParametros(string direccion)  //("w0=" + (dataP[0] * 256 + dataP[1]).ToString());
        {
            /// ("w0=" + (dataP[0] * 256 + dataP[1]).ToString());
            //"h0\t" + (dataP[2] * 256 + dataP[3]).ToString());
            //"Nr\t" + (dataP[4] * 256 + dataP[5]).ToString());
            //"Hr\t" + (dataP[6] * 256 + dataP[7]).ToString());
            //"E\t" + (dataP[8] * 256 + dataP[9]).ToString());
            byte[] dataP = new byte[10];
            string entrada = "";

            StreamReader sr = new StreamReader(@"Parameter\" + direccion);
            entrada = sr.ReadToEnd();
            sr.Close();

            string dato = "";
            int t = 0;
            byte[] b = new byte[2];


            for (int i = 0; i < entrada.Length; i++)
            {

                byte num = (byte)entrada[i];
                if (num == 9 || num == 32)
                {
                    dato = "";
                }
                else if (num == 13)
                {
                    if (t < 10)
                    {
                        b = num2byte(decimal.Parse(dato));
                        dataP[t] = b[1];
                        t++;
                        dataP[t] = b[0];
                        t++;

                    }
                    dato = "";

                }
                else { dato += entrada[i]; }
                if (num == 10)
                {
                    dato = "";

                }


            }

            return dataP;
        }
        public byte[] num2byte(decimal numin)
        {
            /*  if (numin > 600)
                  numin = 600;
             * */
            byte[] b = new byte[2];
            int num = (int)numin;
            int b1 = num / 256;
            int b0 = num - b1 * 256;
            b[1] = (byte)b1;
            b[0] = (byte)b0;
            return b;

        }

        public byte[] leerNivelesGrises(string direc)
        {
            byte[] vectorNg = new byte[256];
            StreamReader sr = new StreamReader(@"Parameter\" + direc);
            string entrada = sr.ReadToEnd();
            sr.Close();
            string dato = "";
            int j = 0;
            int numRead = 0;
            for (int i = 0; i < entrada.Length; i++)
            {

                byte num = (byte)entrada[i];
                dato += entrada[i];

                if (num == 13)
                {
                    numRead = int.Parse(dato);
                    if (numRead > 255)
                        numRead = 255;
                    vectorNg[j] = (byte)numRead;
                    j++;

                }
                if (num == 10)
                {
                    dato = "";

                }


            }

            return vectorNg;
        }
        public byte[] leerNivelesFase()
        {
            //1,0,-1
            byte[] vectorfase = new byte[3];
            StreamReader sr = new StreamReader(@"Parameter\" + "ngfase.dat");
            string entrada = sr.ReadToEnd();
            sr.Close();
            string dato = "";
            int j = 0;
            for (int i = 0; i < entrada.Length; i++)
            {

                byte num = (byte)entrada[i];


                if (num == 13)
                {

                    vectorfase[j] = byte.Parse(dato);
                    j++;

                }
                else { dato += entrada[i]; }
                if (num == 10)
                {
                    dato = "";

                }


            }
            return vectorfase;
        }
        public byte[] leerMatrizAmplitud(string direccion)
        {
            byte[] dataMatrizAmplitud = new byte[24004];
            //utilizo 3X8k=24kbyte para almacenarlas+2byte para Nr  +2byte para almacenar dimension;
            StreamReader sr = new StreamReader(@"Parameter\" + direccion);
            string entrada = sr.ReadToEnd();
            sr.Close();
            string dato = "";
            int nr = 0, w = 1, dim = 0, ver = 0;
            bool detectarNr = true;

            for (int i = 0; i < entrada.Length; i++)
            {

                byte num = (byte)entrada[i];

                //     listBox1.Items.Add("s=" + entrada[i].ToString() + " int=" + num + " char=" + (char)num);
                if (num == 9 || num == 32)
                {
                    ver = int.Parse(dato);
                    if (ver < 0) ver = -ver;
                    dataMatrizAmplitud[dim + 6] = (byte)ver;
                    if (detectarNr) w++;
                    dato = "";
                    dim++;

                }
                else if (num == 13)
                {
                    ver = int.Parse(dato);
                    if (ver < 0) ver = -ver;
                    dataMatrizAmplitud[dim + 6] = (byte)ver;
                    dim++;
                    if (detectarNr)
                    {
                        nr = w;
                        detectarNr = false;
                    }
                    dato = "";

                }
                else { dato += entrada[i]; }
                if (num == 10)
                {
                    dato = "";

                }


            }
            byte[] b = new byte[2];
            b = num2byte((decimal)nr);
            dataMatrizAmplitud[0] = b[1];
            dataMatrizAmplitud[1] = b[0];//LSB
            b = num2byte((decimal)(dim / nr));
            dataMatrizAmplitud[2] = b[1];
            dataMatrizAmplitud[3] = b[0];//LSB
            b = num2byte((decimal)dim);
            dataMatrizAmplitud[4] = b[1];
            dataMatrizAmplitud[5] = b[0];//LSB

            return dataMatrizAmplitud;
        }

        public byte[] texbox2Ng(string entrada)
        {
            byte[] Vector_maximo = new byte[300];
            string dato = "";
            int numeroMatriz = 0, numeroNg = 0;
            int j = 0;
            for (int i = 0; i < entrada.Length; i++)
            {

                byte num = (byte)entrada[i];
                dato += entrada[i];
                if (num == 9 || num == 32)
                {
                    try
                    {
                        numeroMatriz = Int32.Parse(dato);
                        dato = "";
                    }
                    catch { }

                }
                if (num == 13)
                {
                    try
                    {
                        numeroNg = Int32.Parse(dato);
                        Vector_maximo[numeroMatriz] = (byte)numeroNg;
                        j++;

                    }
                    catch { }

                }
                if (num == 10)
                {
                    dato = "";

                }


            }
            return Vector_maximo;
        }
        public byte[] leerMatrizFase()
        {
            byte[] dataMatrizfase = new byte[24004];
            //utilizo 3X8k=24kbyte para almacenarlas+2byte para Nr  +2byte para almacenar dimension;
            StreamReader sr = new StreamReader(@"Parameter\" + "MascaraFase.dat");
            string entrada = sr.ReadToEnd();
            sr.Close();
            string dato = "";
            int nr = 0, w = 1, dim = 0;
            bool detectarNr = true;
            sr = new StreamReader(@"Parameter\" + "MascaraFase.dat");
            entrada = sr.ReadToEnd();
            sr.Close();
            dato = "";

            for (int i = 0; i < entrada.Length; i++)
            {

                byte num = (byte)entrada[i];

                //     listBox1.Items.Add("s=" + entrada[i].ToString() + " int=" + num + " char=" + (char)num);
                if (num == 9 || num == 32)
                {
                    switch (entrada[i - 2])
                    {
                        case '-': dataMatrizfase[dim + 6] = 0; break;
                        case '1': dataMatrizfase[dim + 6] = 1; break;
                        case '+': dataMatrizfase[dim + 6] = 2; break;
                    }
                    if (detectarNr) w++;
                    dim++;

                    dato = "";

                }
                else if (num == 13)
                {
                    switch (entrada[i - 2])
                    {
                        case '-': dataMatrizfase[dim + 6] = 0; break;
                        case '1': dataMatrizfase[dim + 6] = 1; break;
                        case '+': dataMatrizfase[dim + 6] = 2; break;
                    }
                    dim++;
                    if (detectarNr)
                    {
                        nr = w;
                        detectarNr = false;
                    }
                    dato = "";

                }
                else { dato += entrada[i]; }
                if (num == 10)
                {
                    dato = "";

                }


            }
            byte[] b = new byte[2];
            b = num2byte((decimal)nr);
            dataMatrizfase[0] = b[1];
            dataMatrizfase[1] = b[0];//LSB
            b = num2byte((decimal)(dim / nr));
            dataMatrizfase[2] = b[1];
            dataMatrizfase[3] = b[0];//LSB
            b = num2byte((decimal)dim);
            dataMatrizfase[4] = b[1];
            dataMatrizfase[5] = b[0];//LSB

            return dataMatrizfase;
        }
    }
}
