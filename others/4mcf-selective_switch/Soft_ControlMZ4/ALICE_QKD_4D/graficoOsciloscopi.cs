using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ALICE_QKD_4D
{
    class graficoOsciloscopi
    {
        public void graficarN(PictureBox p, int[,] dataIN, int Npuntos, double[] maxindex,int N, bool Inverso) //numinicio=17 desface=0
        {
            int[,] data = dataIN;
            int Hmax = p.Height-1, Wmax = p.Width;

            for (int j = 0; j < Wmax; j++)
            {
                for (int i = 0; i < N; i++)
                {                   
                  // data[i, j] = (int)( (data[i, j] * (Hmax))/ 255);
                    if (maxindex[i] < data[i, j])
                        maxindex[i] = data[i, j];
                }
            }

            Bitmap imagen_fondo = new Bitmap(p.Width, p.Height);
            for (int i = 0; i < N; i++)
            {
                int pixelH = 0, pixelL = 0;

                for (int w = 1; w < Wmax - 1; w++)
                {
                    if (Inverso)
                    {
                        pixelH = ( data[i, w]* (Hmax))/ 255;
                        pixelL = ( data[i, w + 1]* (Hmax))/ 255;
                    }
                    else
                    {
                        pixelH = (int)Hmax - (data[i, w] * (Hmax)) / 255;
                        pixelL = (int)Hmax - (data[i, w + 1] * (Hmax)) / 255;
                    }
                    if (pixelH < pixelL)
                        for (int h = pixelH; h <= pixelL; h++)
                        {

                            imagen_fondo.SetPixel(w, h, ponerColor(i));
                        }
                    else
                        for (int h = pixelL; h <= pixelH; h++)
                        {

                            imagen_fondo.SetPixel(w, h, ponerColor(i));
                        }
                }
            }

            p.Image = imagen_fondo;

        }

        public void graficar4(PictureBox p, int[,] data, int Npuntos, double[] maxindex) //numinicio=17 desface=0
        {

            for (int j = 0; j < Npuntos; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (maxindex[i] < data[i,j])
                        maxindex[i] = data[i,j];
                }
            }

            Bitmap imagen_fondo = new Bitmap(512, 256);

            for (int i = 0; i < 4; i++)
            {
                int pixelH = 0, pixelL = 0;

                for (int w = 1; w < Npuntos; w++)
                {

                    pixelH = 256 - data[i,w];
                    pixelL = 256 - data[i,w + 1];
                    if (pixelH < pixelL)
                        for (int h = pixelH; h < pixelL; h++)
                        {

                            imagen_fondo.SetPixel(w, h, ponerColor(i));
                        }
                    else
                        for (int h = pixelL; h < pixelH; h++)
                        {

                            imagen_fondo.SetPixel(w, h, ponerColor(i));
                        }
                }
            }

            p.Image = imagen_fondo;

        }


        public void graficarSimple(PictureBox p, int[] data, int Npuntos,  double maxindex) //numinicio=17 desface=0
        {

            for (int j = 0; j < Npuntos; j++)
            {
                if (maxindex < data[j] + 6)
                    maxindex = data[j] + 6;
                
            }

            Bitmap imagen_fondo = new Bitmap(256, 256);

          
           for (int w = 0; w < 256; w++)
           {
               imagen_fondo.SetPixel(w, 0, ponerColor(3));
               imagen_fondo.SetPixel(w, 255, ponerColor(3));
           }
     
            for (int h = 0; h < 256; h++)
                   {

                       imagen_fondo.SetPixel(0, h, ponerColor(3));
                       imagen_fondo.SetPixel(255, h, ponerColor(3));
                   }
 int pixelH =0, pixelL=0;
            
           for (int w = 0; w < Npuntos; w++)
            {
               
                pixelH= 256-data[w];
                pixelL = 256-data[w + 1];
                if (pixelH < pixelL)
                for (int h = pixelH; h < pixelL; h++)
                    {
                    
                    imagen_fondo.SetPixel(w, h, ponerColor(0));
                    }
                else
                    for (int h = pixelL; h < pixelH; h++)
                    {

                        imagen_fondo.SetPixel(w, h, ponerColor(0));
                    }
            }
                
   
            p.Image = imagen_fondo;
           
        }
        private Color ponerColor(int j)
        {
            Color c = new Color();
            switch (j)
            {
                case 0: c = Color.Yellow; break;
                case 1: c = Color.Blue; break;
                case 2: c = Color.HotPink; break;
                case 3: c = Color.GreenYellow ; break;
                case 4: c = Color.Red; break;
                case 5: c = Color.Gray; break;
                case 6: c = Color.Black; break;
                case 7: c = Color.Green; break;
                case 8: c = Color.LightGray; break;
                case 9: c = Color.DarkOrange; break;


            }
            return c;

        }
    }
}
