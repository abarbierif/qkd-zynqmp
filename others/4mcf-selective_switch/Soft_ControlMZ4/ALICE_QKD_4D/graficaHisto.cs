using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ALICE_QKD_4D
{
    class graficaHisto
    {
        int w_p = 100, h_p = 100;
        int anchoBin = 0;
        // public int maximoLocal =500; 
        Bitmap imagen_fondo1_1 = new Bitmap(100, 100);
        int pixeh = 0, w0 = 0;
        int Nbins = 6  * 4+1;//= 36;
        int[] datatw = new int[40] { 1, 0, 0, 0, 1, 0, 0, 0, 1,0, 0, 0,1, 0, 0,0, 1, 0, 0, 0, 1, 0, 0, 0, 0,0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 };
        public int[] graficarHisto(bool ajustarMax, int[] maximoCC, int[] dataCC, PictureBox pictureBox1) //numinicio=17 desface=0
        {

            // maximoLocal = maximoCC;

            //defino dimensiones
            if (w_p != pictureBox1.Width || h_p != pictureBox1.Height)
            {
                w_p = pictureBox1.Width;
                h_p = pictureBox1.Height;
                //grafico solo 21 bins.
                anchoBin = (int)((w_p - 2 - 3 * 3) / Nbins);//23lineas separatorias.
                //0-9,10,11-21 10 es el centroRojo
                w0 = (w_p - anchoBin * Nbins - 3 * 3) / 2;
                // w0 = 0;
            }
            imagen_fondo1_1 = new Bitmap(w_p, h_p);

            int k = 0;
            int indexmax = 0, countIndexmax = 0;

            for (int w = w0; w < w_p - anchoBin + 1; w += anchoBin)
            {

                for (int ww = 0; ww < anchoBin; ww++)
                {
                    if (ww == 0)
                    {
                        if (datatw[k] == 1)
                        {
                            for (int h = 0; h < h_p; h++)
                                imagen_fondo1_1.SetPixel(w, h, Color.WhiteSmoke);
                            w++;
                            for (int h = 0; h < h_p; h++)
                                imagen_fondo1_1.SetPixel(w, h, Color.WhiteSmoke);
                            w++;
                            for (int h = 0; h < h_p; h++)
                                imagen_fondo1_1.SetPixel(w, h, Color.WhiteSmoke);
                        }
                        else
                        {

                            for (int h = 0; h < h_p; h++)
                                imagen_fondo1_1.SetPixel(w, h, Color.DarkSeaGreen);
                        }
                    }
                    else
                    {
                        if (k < (Nbins - 5))
                        {


                            if (dataCC[k] > maximoCC[indexmax])
                            {
                                if (ajustarMax) maximoCC[indexmax] = dataCC[k];
                                else
                                    pixeh = h_p;

                            }
                            else
                            {

                                pixeh = dataCC[k] * h_p / maximoCC[indexmax];

                            }




                            if (pixeh > h_p)
                                pixeh = 0;
                            else
                                pixeh = h_p - pixeh;

                            for (int h = pixeh; h < h_p; h++)
                            {
                                imagen_fondo1_1.SetPixel(w + ww, h, Color.Orange);
                            }
                        }
                        else
                        {
                             
                                if (k < (Nbins))
                                {


                                    if (dataCC[k] > maximoCC[indexmax])
                                    {
                                        if (ajustarMax) maximoCC[indexmax] = dataCC[k];
                                        else
                                            pixeh = h_p;

                                    }
                                    else
                                    {

                                        pixeh = dataCC[k] * h_p / maximoCC[indexmax];

                                    }




                                    if (pixeh > h_p)
                                        pixeh = 0;
                                    else
                                        pixeh = h_p - pixeh;

                                    for (int h = pixeh; h < h_p; h++)
                                    {
                                        imagen_fondo1_1.SetPixel(w + ww, h, Color.Red);
                                    }


                                 

                            }
                        }

                    }

                }


                k++;
                countIndexmax++;
                if (countIndexmax == 16)
                {
                    indexmax++;


                }
               // if (countIndexmax ==20)  indexmax++;
                

                
            }
            int w1 = w_p - w0 - 3;
            for (int h = 0; h < h_p; h++)
                imagen_fondo1_1.SetPixel(w1, h, Color.WhiteSmoke);
            w1++;
            for (int h = 0; h < h_p; h++)
                imagen_fondo1_1.SetPixel(w1, h, Color.WhiteSmoke);
            w1++;
            for (int h = 0; h < h_p; h++)
                imagen_fondo1_1.SetPixel(w1, h, Color.WhiteSmoke);
            pictureBox1.Image = imagen_fondo1_1;
            return maximoCC;
        }

    }
}
