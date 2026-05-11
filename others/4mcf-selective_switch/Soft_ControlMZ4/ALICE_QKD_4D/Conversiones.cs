using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace ALICE_QKD_4D
{
    class Conversiones
    {
         double MediaCs = 1000;
         double[] MAtrizMedia = new double[2000];
         int indexMedia = 0;
        public decimal media(int inCs,int NumMaxCtas)
        {
            MAtrizMedia[indexMedia] = inCs;

            indexMedia++;
            if (indexMedia > NumMaxCtas - 1)
            {
                MediaCs = 0;
                for (int i = 0; i < NumMaxCtas; i++)
                {
                    MediaCs += MAtrizMedia[i];
                }
                MediaCs /= NumMaxCtas;
                indexMedia = 0;
            }
             


            return (decimal)MediaCs;
        }
        public void REsetmedia(int inCs)
        {
            if (MediaCs<inCs)
            MediaCs = inCs;
            indexMedia = 0;
        }
        public byte[] Voltaje2Hexa(double[] vect,byte Index) //numinicio=17 desface=0
        {
            byte[] VHex = new byte[73];
            VHex[0] = Index;
            double num = 0;
            int b0=0,b1=0;
            int index = 1;
            for (int i = 0; i < 36; i++)
            {
                num = (double)vect[i];
                num=num * 65535 / 100;
                b1 = (int)num / 256;
                b0 = (int)num - b1 * 256;
                VHex[index] = (byte)b1;
                index++;
                VHex[index] = (byte)b0;
                index++;
 
            }
            return VHex;
        }
        public byte[,] CodeBuf2MAtriz(int[] vect,int lmax) //numinicio=17 desface=0
        {
            int index = 0;
             byte[,] b=new byte[1000,2];

             int[] data = new int[8];
             for (int i = 0; i < lmax; i++)
             {
                 data = int2bits(vect[i]);
                 b[index, 0] =(byte)( data[0] + data[1] * 2);
                 b[index, 1] = (byte)(data[3]);
                 index++;
                 b[index, 0] = (byte)(data[4] + data[5] * 2);
                 b[index, 1] = (byte)(data[6]);
                 index++;
             }
             return b;
        }
        public byte[,] CodeBuf2MAtriz2(int[] vect, int lmax) //numinicio=17 desface=0
        {
            int index = 0;
            byte[,] b = new byte[8000, 2];

            int[] data = new int[8];
            for (int i = 0; i < lmax; i++)
            {
                data = int2bits(vect[i]);
                b[index, 0] = (byte)(data[0] + data[1] * 2+data[2]*4);
   
                index++;
                b[index, 0] = (byte)(data[4] + data[5] * 2+data[6]*4);
        
                index++;
            }
            return b;
        }
        private int[] int2bits(int num)
        {
            int[] data = new int[8];
            int[] div = new int[8] { 1, 2, 4, 8, 16, 32, 64, 128 };
            for (int i = 7; i > -1; i--)
            {
                data[i] = num / div[i];
                num -= data[i] * div[i];
            }

            return data;

        }
        public string num2string(int num) //numinicio=17 desface=0
        {
            double d = 0;
            if (num > 0)
                d = (int)(Math.Log10(num) + 1);
            else
                d = 1;
            string s = "";
            for (int i = 0; i < 10 - d; i++)
            {
                s += "0";

            }
            s += num.ToString();
            return s;
        }
        double tiempo_ant = 0, tiempo_fin = 0;
        public double tiempo_jc() //numinicio=17 desface=0
        {
            double time = 0;
            tiempo_fin = DateTime.Now.Ticks;
            time = ((tiempo_fin - tiempo_ant) / 1000);
            tiempo_ant = tiempo_fin;
            time = Math.Round(time, 2);
            return time;

        }
        public string fecha()
        {
            string st = "";
            st = DateTime.Now.ToString();

            string st2 = "";
            for (int i = 0; i < st.Length; i++)
            {
                char c = st[i];
                if (st[i] == '-' || st[i] == ' ' || st[i] == '\\' || st[i] == ':')
                    st2 += "_";
                else
                    st2 += st[i];
            }
            st2 += "_";
            return st2;


        }
        public int[,] comparaMAtrices(byte[,] ALice, byte[,] Bob)
        {
            int[,] MatrizData = new int[1024 * 8, 5];
            int[,] data = new int[8, 2];
            int k = 0;
            for (int i = 0; i < 511; i++)
            {
                if (ALice[Bob[i, 0], 0] == Bob[i, 1])
                {

                    MatrizData[k, 0] = Bob[i, 0];
                    MatrizData[k, 1] = Bob[i, 1];
                    MatrizData[k, 2] = ALice[Bob[i, 0], 0];
                    MatrizData[k, 3] = Bob[i, 2];
                    MatrizData[k, 4] = ALice[Bob[i, 0], 1];
                    k++;
                }
            }
            int[,] ourdata = new int[k, 5];
            for (int i = 0; i < k; i++)
            {
                ourdata[i, 0] = MatrizData[i, 0];
                ourdata[i, 1] = MatrizData[i, 1];
                ourdata[i, 2] = MatrizData[i, 2];
                ourdata[i, 3] = MatrizData[i, 3];
                ourdata[i, 4] = MatrizData[i, 4];
            }

            return ourdata;

        }

        public int[,] Componevector2matriz(byte[] vectorB, byte[] vectorE, int desfase)
        {
            int lmax = vectorB.Length;
            int[,] MatrizData = new int[lmax * 8, 2];
            int[,] data = new int[8, 2];
            for (int i = 0; i < lmax; i++)
            {
                data = byte2bits(vectorB[i], vectorE[i]);
                for (int j = 0; j < 8; j++)
                {

                    MatrizData[i * 8 + j, 0] = data[j, 0];
                    MatrizData[i * 8 + j, 1] = data[j, 1];
                }
            }
            int[,] dataout = new int[6000, 2];

            for (int i = desfase; i < 6000; i++)
            {

                dataout[i, 0] = MatrizData[i - desfase, 0];
                dataout[i, 1] = MatrizData[i - desfase, 1];

            }
            return dataout;

        }

        private byte bits2byte(int b0, int b1, int b2, int b3, int b4, int b5, int b6, int b7)
        {
            int data = b0 + 2 * b1 + 4 * b2 + 8 * b3 + 16 * b4 + 32 * b5 + 64 * b6 + 128 * b7;
            byte ck = (byte)data;

            return ck;
        }


        public byte[] KeyVector2string(int[] vectorKey, int lmax)
        {
            byte[] Key8bit = new byte[500];
            int j = 0;
            for (int i = 0; i < lmax; i += 8)
            {
                Key8bit[j] = bits2byte(vectorKey[i], vectorKey[i + 1], vectorKey[i + 2], vectorKey[i + 3], vectorKey[i + 4], vectorKey[i + 5], vectorKey[i + 6], vectorKey[i + 7]);
                j++;
            }

            return Key8bit;


        }


        private int[,] byte2bits(byte by1, byte by2)
        {
            int[,] data = new int[8, 2];
            int num = by1, num2 = by2;
            int[] div = new int[8] { 1, 2, 4, 8, 16, 32, 64, 128 };
            for (int i = 7; i > -1; i--)
            {
                data[i, 0] = num / div[i];
                num -= data[i, 0] * div[i];

                data[i, 1] = num2 / div[i];
                num2 -= data[i, 1] * div[i];
            }

            return data;
        }
        private int[] vector2num(byte by1, byte by2)
        {

            int[] data = new int[3];
            int b0 = by2 / 128;
            int b1 = (by2 - b0 * 128) / 64;
            int b3 = (by2 - b0 * 128 - b1 * 64);

            data[0] = by1 + b3 * 256;
            data[1] = b0;//base
            data[2] = b1;//base
            return data;
        }
        public int[,] decomponer1412(byte[] vector, int LmaxRx)
        {
            int[,] MatrizData = new int[1024, 3];
            byte b = 0;
            int[] data = new int[3];
            int k = 0;
            for (int i = 0; i < LmaxRx / 2; i += 2)
            {
                data = vector2num(vector[i], vector[i + 1]);
                MatrizData[k, 0] = data[0];
                MatrizData[k, 1] = data[1];
                MatrizData[k, 2] = data[2];
                k++;

            }

            return MatrizData;
        }

        public byte[] Byte_EB_in_C(byte[] E, byte[] B, int Lmax)
        {
            byte[] C = new byte[Lmax];
            int k = 0;
            for (int i = 0; i < 1024; i++)
            {
                C[k] = E[i];
                k++;
                C[k] = B[i];
                k++;
            }
            return C;
        }
        public byte[] Byte4 = new byte[4];
        public void int2Byte4(int num)
        {


            int Byte_4 = num / (16777216);
            int Byte_3 = (num - Byte_4 * 16777216) / 65536;
            int Byte_2 = (num - Byte_4 * 16777216 - Byte_3 * 65536) / 256;
            int Byte_1 = num - Byte_4 * 16777216 - Byte_3 * 65536 - Byte_2 * 256;

            Byte4[0] = (byte)Byte_1;
            Byte4[1] = (byte)Byte_2;
            Byte4[2] = (byte)Byte_3;
            Byte4[3] = (byte)Byte_4;

        }
        public int Byte4_2int(byte[] Byte)
        {
            int i = Byte[0] + Byte[1] * 256 + Byte[2] * 65536 + Byte[3] * 16777216;
            return i;
        }

    }
}
