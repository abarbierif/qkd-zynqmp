using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace ALICE_QKD_4D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void refrescarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetectarPuertos();
        }
        public void DetectarPuertos()
        {

            comboBox_Puertosexistentes.Items.Clear();
            comboBox_Puertosexistentes_LAserdecontrol.Items.Clear();
            string namePort = "";

            comboBox_Puertosexistentes.Items.Add("None");
            comboBox_Puertosexistentes_LAserdecontrol.Items.Add("None");
            for (int i = 0; i < 14; i++)
            {
                if (Sp.IsOpen)
                    Sp.Close();
                namePort = "COM" + i.ToString();
                try
                {
                    Sp.PortName = namePort;
                    Sp.Open();
                    comboBox_Puertosexistentes.Items.Insert(0, namePort);
                    comboBox_Puertosexistentes.Text = namePort;
                    comboBox_Puertosexistentes_LAserdecontrol.Items.Insert(0, namePort);
                    comboBox_Puertosexistentes_LAserdecontrol.Text = namePort;
                }
                catch
                {
                }

            }
            if (Sp.IsOpen)
                Sp.Close();

        }

        private void conectarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (conectarToolStripMenuItem.Text == "Conectar")
            {


                if (conectar(comboBox_Puertosexistentes.SelectedItem.ToString()))
                {
                    conectarToolStripMenuItem.Text = "Desconectar";



                    
                       // LeerPropiedades001();
                    



                }
                else
                {
                    MessageBox.Show("Error en el Puerto  ");

                }







            }
            else
            {
                Desconectar();
                conectarToolStripMenuItem.Text = "Conectar";

            }
        }
        public void LeerPropiedades001()
        {
            /*
            GuardaPropiedades.leerPRopiedades();
            numericUpDown8.Value =  GuardaPropiedades.cargar (0, numericUpDown8.Value);
            numericUpDown21.Value = GuardaPropiedades.cargar (1,numericUpDown21.Value);
            numericUpDown26.Value = GuardaPropiedades.cargar (2,numericUpDown26.Value);
            numericUpDown22.Value = GuardaPropiedades.cargar (3, numericUpDown22.Value);
            numericUpDown24.Value = GuardaPropiedades.cargar (4, numericUpDown24.Value);
            numericUpDown23.Value = GuardaPropiedades.cargar (5, numericUpDown23.Value);
            numericUpDown_Noise.Value = GuardaPropiedades.cargar (6, numericUpDown_Noise.Value);
            numericUpDown28.Value = GuardaPropiedades.cargar (7, numericUpDown28.Value);
            numericUpDown27.Value = GuardaPropiedades.cargar (8, numericUpDown27.Value);
            numericUpDown30.Value = GuardaPropiedades.cargar (9, numericUpDown30.Value);
            numericUpDown29.Value = GuardaPropiedades.cargar (10, numericUpDown29.Value);
            EnablePM0 = (int)GuardaPropiedades.cargar(11, EnablePM0);
            if (EnablePM0 == 1) checkBox13.Checked = true;
            EnablePM1 = (int)GuardaPropiedades.cargar(12, EnablePM1);
            if (EnablePM1 == 2) checkBox14.Checked = true;
            EnablePM2 = (int)GuardaPropiedades.cargar(13, EnablePM2);
            if (EnablePM2 == 4) checkBox15.Checked = true;


            EnableMod0 = (int)GuardaPropiedades.cargar(14, EnableMod0);
            if (EnableMod0 == 1) checkBox10.Checked = true;
            EnableMod1 = (int)GuardaPropiedades.cargar(15, EnableMod1);
            if (EnableMod1 == 2) checkBox11.Checked = true;
            EnableMod2 = (int)GuardaPropiedades.cargar(16, EnableMod2);
            if (EnableMod2 == 4) checkBox12.Checked = true;

            numericUpDown32.Value = GuardaPropiedades.cargar (17, numericUpDown32.Value);
            numericUpDown_Cs.Value = GuardaPropiedades.cargar(18, numericUpDown_Cs.Value);
            numericUpDown_Estados.Value = GuardaPropiedades.cargar(19, numericUpDown_Estados.Value);
            numericUpDown33.Value = GuardaPropiedades.cargar (20, numericUpDown33.Value);
            numericUpDown34.Value = GuardaPropiedades.cargar (21, numericUpDown34.Value);
            numericUpDown35.Value = GuardaPropiedades.cargar (22, numericUpDown35.Value);
            numericUpDown36.Value = GuardaPropiedades.cargar (23, numericUpDown36.Value);
            numericUpDown37.Value = GuardaPropiedades.cargar (24, numericUpDown37.Value);

numericUpDown38.Value = GuardaPropiedades.cargar (25, numericUpDown38.Value);
            numericUpDown39.Value = GuardaPropiedades.cargar (26, numericUpDown39.Value);
            numericUpDown40.Value = GuardaPropiedades.cargar (27, numericUpDown40.Value);
            numericUpDown41.Value = GuardaPropiedades.cargar(28, numericUpDown41.Value);
            numericUpDownAjustePM1.Value = GuardaPropiedades.cargar(29, numericUpDownAjustePM1.Value);
            numericUpDownAjustePM2.Value = GuardaPropiedades.cargar(30, numericUpDownAjustePM2.Value);
            numericUpDownAjustePM3.Value = GuardaPropiedades.cargar(31, numericUpDownAjustePM3.Value);
            numericUpDown_DElayDEte.Value = GuardaPropiedades.cargar(32, numericUpDown_DElayDEte.Value);
            numericUpDown_delayAPD3.Value = GuardaPropiedades.cargar(33, numericUpDown_delayAPD3.Value);

            */


        }
        public bool conectar(string Portname)
        {
            bool coneccionOk = false;
            if (Sp.IsOpen) Close();
            try
            {
                Sp.PortName = Portname;
                Sp.Open();


                Sp.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Recepcion);
                coneccionOk = true;

            }
            catch
            {
                coneccionOk = false;
            }

            return coneccionOk;

        }
        public bool conectar2(string Portname)
        {
            bool coneccionOk = false;
            if (serialPort1cONTROLLASER.IsOpen) Close();
            try
            {
                serialPort1cONTROLLASER.PortName = Portname;
                serialPort1cONTROLLASER.Open();


             //   serialPort1cONTROLLASER.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(Recepcion2);
                coneccionOk = true;

            }
            catch
            {
                coneccionOk = false;
            }

            return coneccionOk;

        }
        public void Desconectar()
        {
            if (Sp.IsOpen) Sp.Close();
        }

        #region
        ListBox list1 = new ListBox();
        int[] matrizbuferRx = new int[250000];// 25k
        int[] matrizbuferRx2 = new int[250000];// 25k
        int[] matrizbuferRx3 = new int[250000];// 25k
        int[] matrizbuferRx4 = new int[250000];// 25k
        int indexRx = 0, indexRx2 = 0, indexRx3 = 0, indexRx4 = 0;


        int receocionData = 0; //o idle; 1 data APD; 2dataParametros 
        #endregion

        private void Recepcion(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            while (Sp.BytesToRead != 0)
            {
                switch (receocionData)
                {
                    case 0:


                        list1.Items.Insert(0, Sp.ReadByte());

                        if (list1.Items.Count > 4)
                        {
                             

                            if (((int)list1.Items[1] == 254) && ((int)list1.Items[2] == 0)
                                && ((int)list1.Items[3] == 255) && ((int)list1.Items[4] == 0)
                                )
                            {
                                indexRx = 0;
                                receocionData = (int)list1.Items[0];



                            }
                            





                        }

                        break;  //espero mascaras

                    case 20:
                        if (indexRx < 2560)
                        {
                            matrizbuferRx[indexRx] = Sp.ReadByte();
                             indexRx++;

                        }
                        else
                        {
                            this.Invoke(new EventHandler(ActualizarBufer20));
                            receocionData = 0;
                        }
                        break;

                    
                    case 30:
                        try
                        {

                            if (indexRx < 6132)
                            {
                                matrizbuferRx[indexRx] = Sp.ReadByte();
                                indexRx++;

                            }
                            else
                            {
                                this.Invoke(new EventHandler(ActualizarBufer30));
                                receocionData = 0;
                            }
                        }
                        catch { }
                        break;

                    case 50:
                         

                            if (indexRx < 30)
                            {
                                matrizbuferRx[indexRx] = Sp.ReadByte();
                                DAtaM += Sp.ReadByte() +"\r\n";
                                indexRx++;

                            }
                            else
                            {
                                this.Invoke(new EventHandler(ActualizarBufer2));
                                receocionData = 0;
                            }
                            EnableMod = 1;
                        break;
                    case 60:

                        EnableMod = 0;
                        if (indexRx < 35)
                        {
                            matrizbuferRx[indexRx] = Sp.ReadByte();
                            indexRx++;

                        }
                        else
                        {
                            this.Invoke(new EventHandler(ActualizarBufer2));
                            receocionData = 0;
                        }

                        break;

                }


            }
        }
        int[,] VoltajeADCs = new int[6,600];
        graficoOsciloscopi GADCs = new graficoOsciloscopi();
        double[] maximosAdcs = new double[6]; 
        private void ActualizarBufer20(object sender, EventArgs e)
        {
            indexRx2 = indexRx;
            int k= 0;
            

            for (int i = 1; i < 2560; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    VoltajeADCs[j, k] = matrizbuferRx[i]; i++;
                }
                
                k++;
            }
              GADCs.graficarN(pictureBox1, VoltajeADCs, 510, maximosAdcs,4,true);

            /*
      for (int i = 0; i < 1280; i++)
      {
          VoltajeADC01[k] = matrizbuferRx[i]; i++;
          VoltajeADC02[k] = matrizbuferRx[i]; i++;
          VoltajeADC03[k] = matrizbuferRx[i]; i++;
          VoltajeADC04[k] = matrizbuferRx[i]; i++;
          k++;
      }
      GADCs.graficarSimple(pictureBox1, VoltajeADC01, 256, 255);
      GADCs.graficarSimple(pictureBox2, VoltajeADC02, 256, 255);
      GADCs.graficarSimple(pictureBox3, VoltajeADC03, 256, 255);
      GADCs.graficarSimple(pictureBox4, VoltajeADC04, 256, 255);
       * */
        }
        int[,] VoltajeDAC_Ctrl = new int[3, 600];
        int[,] VoltajeDAC_fase = new int[3, 600];
        graficoOsciloscopi GADCs2 = new graficoOsciloscopi();
        graficoOsciloscopi GADCs3 = new graficoOsciloscopi();
        double[] maximosfase = new double[3];
        double[] maximosctrl = new double[3];
        bool readyfraficoOsc = true;
        private void ActualizarBufer30(object sender, EventArgs e)
        {
            indexRx2 = indexRx;
            int k = 0;


            for (int i = 1; i < 2560; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    VoltajeADCs[j, k] = matrizbuferRx[i]; i++;
                }

                k++;
            }
            GADCs.graficarN(pictureBox1, VoltajeADCs, 508, maximosAdcs, 6, false);
            k = 1;
            int suma_inst_VoltajeADCs = VoltajeADCs[0,k-1] + VoltajeADCs[1,k-1] + VoltajeADCs[2,k-1] + VoltajeADCs[3,k-1];
            label93.Text = Convert.ToString(suma_inst_VoltajeADCs);
            k = 0;
            for (int i = 2561; i < 6132; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    VoltajeDAC_fase[j, k] = matrizbuferRx[i]; i++;
                }
                for (int j = 0; j < 3; j++)
                {
                    VoltajeDAC_Ctrl[j, k] = matrizbuferRx[i]; i++;
                }

                k++;
            }
            GADCs2.graficarN(pictureBoxfase, VoltajeDAC_fase, 508, maximosfase, 3,false);
            GADCs3.graficarN(pictureBoxControl, VoltajeDAC_Ctrl, 508, maximosctrl, 3, true);
            readyfraficoOsc = true;
            /*
      for (int i = 0; i < 1280; i++)
      {
          VoltajeADC01[k] = matrizbuferRx[i]; i++;
          VoltajeADC02[k] = matrizbuferRx[i]; i++;
          VoltajeADC03[k] = matrizbuferRx[i]; i++;
          VoltajeADC04[k] = matrizbuferRx[i]; i++;
          k++;
      }
      GADCs.graficarSimple(pictureBox1, VoltajeADC01, 256, 255);
      GADCs.graficarSimple(pictureBox2, VoltajeADC02, 256, 255);
      GADCs.graficarSimple(pictureBox3, VoltajeADC03, 256, 255);
      GADCs.graficarSimple(pictureBox4, VoltajeADC04, 256, 255);
       * */
        }

        string DAtaM = "";
        int UPm1, UPm2, UPm3,regMAx=0,regMin=0,rErgef,SumCs;
        double tiempo;
        double Prob;
        int NumBuffer = 0;
        graficaHisto histo01=new graficaHisto();
        int[] maximosHisto01 = new int[4]; 
        int[] CuentasCC0 = new int[100];
        int[] CuentasCC = new int[100]; 
        int[] CuentasCCGraficar = new int[100];
        private void ActualizarBufer10(object sender, EventArgs e)
        {


            for (int i = 0; i < 385; i++)
            {

                Vector_RxBruto[i] = (byte)matrizbuferRx[i];
            }

            int indexM = 0;
            for (int i = 0; i < 385; i += 5)//parte de dos por que {TX[0],Tx[1]}=num bitMax; 
            {
                CuentasCC0[indexM] = Vector_RxBruto[0 + i] + Vector_RxBruto[1 + i] * 256 + Vector_RxBruto[2 + i] * 65536 + Vector_RxBruto[3 + i] * 16777216;
                indexM++;
            }
            for (int i = 1; i < 100; i++)//parte de dos por que {TX[0],Tx[1]}=num bitMax; 
            {
                CuentasCC[i - 1] = CuentasCC0[i];
            }
            tiempo = ((double)CuentasCC0[0]) / 100000000;


            double tasa = (200000000 / ((double)numericUpDown21.Value + 1));
            double tiempoInt = (((double)numericUpDown26.Value) / 100000000);




            if (guardarDataToolStripMenuItem.Checked)
            {
                if (checkBox21.Checked)
                {
                    enviarNum(41);
                    button1.Text = "Medida On";
                }

                string s = "";
                for (int i = 1; i < 76; i++)
                {
                    s += CuentasCC[i] + "\t";
                }

                tiempoS = tiempoOk.timeString();

                if (ToolStripMenuItem_GuardarOsc.Checked)
                {
                    enviarOrden(0);
                    enviarNum(63);

                    switch (ProcCambiaProbEn)
                    {
                        case 0:
                            numericUpDown22.Value = 4;
                            checkBox10.Checked = false;
                            checkBox11.Checked = false;
                            checkBox12.Checked = false;

                            NombreProbEn = "_EstadisticaOsc_" + tiempoS + "_.txt";
                            checkBox20.Checked = false;
                            ProcCambiaProbEn = 1; break;
                        case 1: ProcCambiaProbEn = 2; break;
                        case 2:
                            countProbEn++;
                            if (countProbEn > numericUpDown42.Value)
                            {
                                countProbEn = 0;
                                ProcCambiaProbEn = 3;
                            }
                            Guardar10.escribelinea(tiempoS + "\t" + s + Double2string(tiempo, true) + "\t" + Double2string(tasa, true) + "\t" + Double2string(tiempoInt, true) + "\t" + Double2string(0, true), NombreProbEn);
                            label58.Text = "Osc:" + countProbEn.ToString();
                            break;
                        case 3:
                            checkBox20.Checked = true;
                            numericUpDown22.Value = 2;
                           // checkBox10.Checked = true;
                            //checkBox11.Checked = true;
                            //checkBox12.Checked = true;
                            NombreProbEn = "_Estadistica_" + tiempoS + "_.txt";
                            ProcCambiaProbEn = 4;
                            break;
                        case 4: ProcCambiaProbEn = 5; break;
                        case 5:
                            countProbEn++;
                            if (countProbEn > numericUpDown43.Value)
                            {
                                countProbEn = 0;
                                ProcCambiaProbEn = 0;
                            }
                            Guardar10.escribelinea(tiempoS + "\t" + s + Double2string(tiempo, true) + "\t" + Double2string(tasa, true) + "\t" + Double2string(tiempoInt, true) + "\t" + Double2string(0, true) + "\t" + UPm1.ToString() + "\t" + UPm2.ToString() + "\t" + UPm3.ToString(), NombreProbEn);
                            label58.Text = "data:" + countProbEn.ToString();
                            break;

                    }




                }
                else
                    Guardar10.escribelinea(tiempoS + "\t" + s + Double2string(tiempo, true) + "\t" + Double2string(tasa, true) + "\t" + Double2string(tiempoInt, true) + "\t" + Double2string(0, true), "_Estadistica.txt");

            }




            if (tiempo == 0)
                label44.Text = "t: 0s rate: 0Mdet/s";
            //else
            //   label44.Text = "t: " + (Math.Round(tiempo, 3)).ToString() + "s rate: " + (Math.Round(rateT, 3)).ToString() + "Mdet/s mi:" + (Math.Round(mi, 3)).ToString() + "Ph/p";

            CuentasCCGraficar[0] = CuentasCC[1];
            CuentasCCGraficar[1] = CuentasCC[5];
            CuentasCCGraficar[2] = CuentasCC[8];
            CuentasCCGraficar[3] = CuentasCC[10];

            CuentasCCGraficar[4] = CuentasCC[16];
            CuentasCCGraficar[5] = CuentasCC[20];
            CuentasCCGraficar[6] = CuentasCC[23];
            CuentasCCGraficar[7] = CuentasCC[25];


            CuentasCCGraficar[8] = CuentasCC[31];
            CuentasCCGraficar[9] = CuentasCC[35];
            CuentasCCGraficar[10] = CuentasCC[38];
            CuentasCCGraficar[11] = CuentasCC[40];

            CuentasCCGraficar[12] = CuentasCC[46];
            CuentasCCGraficar[13] = CuentasCC[50];
            CuentasCCGraficar[14] = CuentasCC[53];
            CuentasCCGraficar[15] = CuentasCC[55];

            CuentasCCGraficar[16] = CuentasCC[61];
            CuentasCCGraficar[17] = CuentasCC[65];
            CuentasCCGraficar[18] = CuentasCC[68];
            CuentasCCGraficar[19] = CuentasCC[70];

           


            calculaProb(CuentasCCGraficar, checkBox_2Brazos.Checked);
            if (numericUpDown34.Value != 0)
            {
                CuentasCCGraficar[20] = buscaMAx(CuentasCC[1], CuentasCC[16], CuentasCC[31], CuentasCC[46], checkBox_2Brazos.Checked);
                CuentasCCGraficar[21] = buscaMAx(CuentasCC[5], CuentasCC[20], CuentasCC[35], CuentasCC[50], checkBox_2Brazos.Checked);
                CuentasCCGraficar[22] = buscaMAx(CuentasCC[8], CuentasCC[23], CuentasCC[38], CuentasCC[53], checkBox_2Brazos.Checked);
                CuentasCCGraficar[23] = buscaMAx(CuentasCC[10], CuentasCC[25], CuentasCC[40], CuentasCC[55], checkBox_2Brazos.Checked);
            }
            graficar05.graficar4Entradas(Pro1, Pro2, Pro3, Pro4, numericUpDown_prob, pictureBox_Prob);

            label_prob1.Text = (((double)Pro1) / 1000).ToString();
            label_prob2.Text = (((double)Pro2) / 1000).ToString();
            label_prob3.Text = (((double)Pro3) / 1000).ToString();
            label_prob4.Text = (((double)Pro4) / 1000).ToString();



            maximosHisto01[0] = (int)numericUpDown_Estados.Value;
            maximosHisto01[1] = (int)numericUpDown_xi.Value;
            maximosHisto01[2] = (int)numericUpDown_Cs.Value;


            maximosHisto01 = histo01.graficarHisto(checkBox_hist.Checked, maximosHisto01, CuentasCCGraficar, pictureBox_casillas1);
            if (checkBox_hist.Checked)
            {

                numericUpDown_Estados.Value = (decimal)maximosHisto01[0];
                numericUpDown_xi.Value = (decimal)maximosHisto01[1];
                numericUpDown_Cs.Value = (decimal)maximosHisto01[2];

            }
            if (checkBox20.Checked == false && numericUpDown22.Value == 4) 
            {
                if (restOsc)
                {
                    numericUpDown_OScP1.Value = 0;
                    numericUpDown_OScP2.Value = 0;
                    numericUpDown_OScP3.Value = 0;
                    numericUpDown_OScP4.Value = 0;
                    restOsc = false;
                }
                else 
                { 
                    if(CuentasCC[1]> numericUpDown_OScP1.Value)  numericUpDown_OScP1.Value=CuentasCC[1];
                    if (CuentasCC[5] > numericUpDown_OScP2.Value) numericUpDown_OScP2.Value = CuentasCC[5];
                    if (CuentasCC[8] > numericUpDown_OScP3.Value) numericUpDown_OScP3.Value = CuentasCC[8];
                    if (CuentasCC[10] > numericUpDown_OScP4.Value) numericUpDown_OScP4.Value = CuentasCC[10];
                }
            }
            else 
             restOsc=true;
            
            //label63_P1 label61_P2 label64_P3 label62_P4
            CuentasCCGraficar[0] = CuentasCC[1] - (int)numericUpDown_OScP1.Value;
            CuentasCCGraficar[1] = CuentasCC[5] - (int)numericUpDown_OScP2.Value;
            CuentasCCGraficar[2] = CuentasCC[8] - (int)numericUpDown_OScP3.Value;
            CuentasCCGraficar[3] = CuentasCC[10] - (int)numericUpDown_OScP4.Value;

            CuentasCCGraficar[4] = CuentasCC[16] - (int)numericUpDown_OScP1.Value;
            CuentasCCGraficar[5] = CuentasCC[20] - (int)numericUpDown_OScP2.Value;
            CuentasCCGraficar[6] = CuentasCC[23] - (int)numericUpDown_OScP3.Value;
            CuentasCCGraficar[7] = CuentasCC[25] - (int)numericUpDown_OScP4.Value;


            CuentasCCGraficar[8] = CuentasCC[31] - (int)numericUpDown_OScP1.Value;
            CuentasCCGraficar[9] = CuentasCC[35] - (int)numericUpDown_OScP2.Value;
            CuentasCCGraficar[10] = CuentasCC[38] - (int)numericUpDown_OScP3.Value;
            CuentasCCGraficar[11] = CuentasCC[40] - (int)numericUpDown_OScP4.Value;

            CuentasCCGraficar[12] = CuentasCC[46] - (int)numericUpDown_OScP1.Value; ;
            CuentasCCGraficar[13] = CuentasCC[50] - (int)numericUpDown_OScP2.Value;
            CuentasCCGraficar[14] = CuentasCC[53] - (int)numericUpDown_OScP3.Value;
            CuentasCCGraficar[15] = CuentasCC[55] - (int)numericUpDown_OScP4.Value;
            if (CuentasCCGraficar[0] < 0) CuentasCCGraficar[0] = 0;
            if (CuentasCCGraficar[1] < 0) CuentasCCGraficar[1] = 0;
            if (CuentasCCGraficar[2] < 0) CuentasCCGraficar[2] = 0;
            if (CuentasCCGraficar[3] < 0) CuentasCCGraficar[3] = 0;
            if (CuentasCCGraficar[4] < 0) CuentasCCGraficar[4] = 0;
            if (CuentasCCGraficar[5] < 0) CuentasCCGraficar[5] = 0;
            if (CuentasCCGraficar[6] < 0) CuentasCCGraficar[6] = 0;
            if (CuentasCCGraficar[7] < 0) CuentasCCGraficar[7] = 0;
            if (CuentasCCGraficar[8] < 0) CuentasCCGraficar[8] = 0;
            if (CuentasCCGraficar[9] < 0) CuentasCCGraficar[9] = 0;
            if (CuentasCCGraficar[10] < 0) CuentasCCGraficar[10] = 0;
            if (CuentasCCGraficar[11] < 0) CuentasCCGraficar[11] = 0;
            if (CuentasCCGraficar[12] < 0) CuentasCCGraficar[12] = 0;
            if (CuentasCCGraficar[13] < 0) CuentasCCGraficar[13] = 0;
            if (CuentasCCGraficar[14] < 0) CuentasCCGraficar[14] = 0;
            if (CuentasCCGraficar[15] < 0) CuentasCCGraficar[15] = 0;




            calculaProb2(CuentasCCGraficar, checkBox_2Brazos.Checked);
            label63_P1.Text = (((double)Pro1) / 1000).ToString();
            label61_P2.Text = (((double)Pro2) / 1000).ToString();
            label64_P3.Text = (((double)Pro3) / 1000).ToString();
            label62_P4.Text = (((double)Pro4) / 1000).ToString();

        }
 
        public int buscaMAx(  int Cs1, int Cs2, int Cs3, int Cs4,bool Chec2b)
        {
            int MaxF = Cs1;
            if (Cs2 > MaxF) MaxF = Cs2;
            if (Cs3 > MaxF)MaxF = Cs3;
            if (Cs4 > MaxF) MaxF = Cs4;
            if(Chec2b)
            return MaxF/2;
            else
                return MaxF / 4;
        }

        bool restOsc = false;
        int countProbEn = 0, ProcCambiaProbEn=0;
        string NombreProbEn = "_Estadistic0000.txt";
        public string Double2string(double d, bool punto) //numinicio=17 desface=0
        {
            string dd = d.ToString(), salida = "";

            for (int i = 0; i < dd.Length; i++)
            {
                if (punto)
                {
                    if (dd[i] == ',')
                        salida += '.';
                    else

                        salida += dd[i];
                }
                else
                {
                    if (dd[i] == '.')
                        salida += ',';
                    else

                        salida += dd[i];
                }
            }

            return salida;
        }

        int NtcountsN1 = 0;
        private void calculaProb(int[] CuentasCC0,  bool Boll2B)//checkBox_2Brazos
        {
            NtcountsN1 = 0;
            int[] minmax = new int[2];
            if (Boll2B)
            {


                minmax = Sum_mayor(generaVector(CuentasCC0, 0, 3));
                label_Sphi1.Text = minmax[1].ToString();
                Pro1 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];

                minmax = Sum_mayor(generaVector(CuentasCC0, 4, 7));
                label_Sphi2.Text = minmax[1].ToString();
                                Pro2 = (minmax[0] * 1000) / (minmax[1]);
                                NtcountsN1 += minmax[1];

                minmax = Sum_mayor(generaVector(CuentasCC0, 8, 11));
                label_Sphi3.Text = minmax[1].ToString();
                Pro3 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_mayor(generaVector(CuentasCC0, 12, 15));
                label_Sphi4.Text = minmax[1].ToString();
                Pro4 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_mayor(generaVector(CuentasCC0, 16, 19));
                label_Sphi5.Text = minmax[1].ToString();
                NtcountsN1 += minmax[1];
            }
            else
            {
                minmax = Sum_Vmayor(generaVector(CuentasCC0, 0, 3));
                label_Sphi1.Text = minmax[1].ToString();
                Pro1 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_Vmayor(generaVector(CuentasCC0, 4, 7));
                label_Sphi2.Text = minmax[1].ToString();
                Pro2 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_Vmayor(generaVector(CuentasCC0, 8, 11));
                label_Sphi3.Text = minmax[1].ToString();
                Pro3 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_Vmayor(generaVector(CuentasCC0, 12, 15));
                label_Sphi4.Text = minmax[1].ToString();
                Pro4 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_Vmayor(generaVector(CuentasCC0, 16, 19));
                label_Sphi5.Text = minmax[1].ToString();
                NtcountsN1 += minmax[1];
            }

            label_NtN1.Text = NtcountsN1.ToString();
        }
        private void calculaProb2(int[] CuentasCC0, bool Boll2B)//checkBox_2Brazos
        {
            NtcountsN1 = 0;
            int[] minmax = new int[2];
            if (Boll2B)
            {


                minmax = Sum_mayor(generaVector(CuentasCC0, 0, 3));
        
                Pro1 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];

                minmax = Sum_mayor(generaVector(CuentasCC0, 4, 7));
          
                Pro2 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];

                minmax = Sum_mayor(generaVector(CuentasCC0, 8, 11));
 
                Pro3 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_mayor(generaVector(CuentasCC0, 12, 15));
 
                Pro4 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_mayor(generaVector(CuentasCC0, 16, 19));
 
                NtcountsN1 += minmax[1];
            }
            else
            {
                minmax = Sum_Vmayor(generaVector(CuentasCC0, 0, 3));
 
                Pro1 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_Vmayor(generaVector(CuentasCC0, 4, 7));
   
                Pro2 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_Vmayor(generaVector(CuentasCC0, 8, 11));
 
                Pro3 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_Vmayor(generaVector(CuentasCC0, 12, 15));
 
                Pro4 = (minmax[0] * 1000) / (minmax[1]);
                NtcountsN1 += minmax[1];
                minmax = Sum_Vmayor(generaVector(CuentasCC0, 16, 19));
      
                NtcountsN1 += minmax[1];
            }

   

        }
        private int[] generaVector(int[] CuentasCC0,int i0,int i1)//checkBox_2Brazos
        {
            int[] CsOr = new int[i1-i0+1];
            int k = 0;
            for (int i = i0; i < i1 + 1; i++)
            {
                CsOr[k] = CuentasCC0[i];
                k++;
            }
            return CsOr;
        }
        private int[] Sum_Vmayor(int[] CuentasC)//checkBox_2Brazos
        {
            
            int[] CsOr = new int[2];
            int sum = CuentasC[0];
            int k = 0;
            for (int i = 1; i < 4; i++)
            {
                if (CuentasC[k] < CuentasC[i])
                {
                    k = i;
                }
                sum += CuentasC[i];
            }
            
            if (sum == 0)
            {
                CsOr[0] = 0;
                CsOr[1] = 1;
            }
            else
            {
                CsOr[0] = CuentasC[k] ;
                CsOr[1] = sum;
            }
            return CsOr;

        }
       
        private int[] Sum_mayor (int[] CuentasC)//checkBox_2Brazos
        {
            int max = 0;
            int[] CsOr = new int[2];
            int[] CsOr2 = new int[3];
            int sum = CuentasC[0];
            int k = 0;
            for (int i = 1; i < 4; i++)
            { 
                if(CuentasC[k]<CuentasC[i])
                {
                    k = i;
                }
                sum += CuentasC[i];
            }
            int kk=0;
            for (int i = 0; i < 4; i++)
            {
                if (k!=i)
                {
                    CsOr2[kk] = CuentasC[i];
                    kk++;
                }

            }
            int k2 = 0;

            for (int i = 1; i < 3; i++)
            {
                if (CsOr2[k2] < CsOr2[i])
                {
                    k2 = i;
                }

            }
            
            if (sum == 0)
            {
                CsOr[0] = 0;
                CsOr[1] = 1;
            }
            else
            { CsOr[0] = CuentasC[k] + CsOr2[k2];
                CsOr[1] = sum;
            }
            return CsOr;

        }
       
        int Pro1, Pro2, Pro3, Pro4;
        private void ActualizarBufer320(object sender, EventArgs e)
        {
            if (checkBox_enOsc.Checked)
            {
                enviarOrden(1);
                enviarNum(63);
            }
            for (int i = 0; i < 44; i++)
            {

                Vector_RxBruto[i] = (byte)matrizbuferRx[i];
            }
            Cs1 = Vector_RxBruto[0] + Vector_RxBruto[1] * 256 + Vector_RxBruto[2] * 65536 + Vector_RxBruto[3] * 16777216;
            Cs2 = Vector_RxBruto[4] + Vector_RxBruto[5] * 256 + Vector_RxBruto[6] * 65536 + Vector_RxBruto[7] * 16777216;
            Cs3 = Vector_RxBruto[8] + Vector_RxBruto[9] * 256 + Vector_RxBruto[10] * 65536 + Vector_RxBruto[11] * 16777216;
            Cs4 = Vector_RxBruto[12] + Vector_RxBruto[13] * 256 + Vector_RxBruto[14] * 65536 + Vector_RxBruto[15] * 16777216;

            rErgef = Vector_RxBruto[16] + Vector_RxBruto[17] * 256 + Vector_RxBruto[18] * 65536 + Vector_RxBruto[19] * 16777216; 
            UPm1 = Vector_RxBruto[20] + Vector_RxBruto[21] * 256 + Vector_RxBruto[22] * 65536 + Vector_RxBruto[23] * 16777216;
            UPm2 =  Vector_RxBruto[24] + Vector_RxBruto[25] * 256 + Vector_RxBruto[26] * 65536 + Vector_RxBruto[27] * 16777216;
            UPm3 = Vector_RxBruto[28] + Vector_RxBruto[29] * 256 + Vector_RxBruto[30] * 65536 + Vector_RxBruto[31] * 16777216;
            regMAx = Vector_RxBruto[32] + Vector_RxBruto[33] * 256 + Vector_RxBruto[34] * 65536 + Vector_RxBruto[35] * 16777216;
            regMin = Vector_RxBruto[36] + Vector_RxBruto[37] * 256 + Vector_RxBruto[38] * 65536 + Vector_RxBruto[39] * 16777216;
            Prob = Vector_RxBruto[40] + Vector_RxBruto[41] * 256 + Vector_RxBruto[42] * 65536 + Vector_RxBruto[43] * 16777216;
            label_Cs1.Text = Cs1.ToString();
            label_Cs2.Text = Cs2.ToString();
            label_Cs3.Text = Cs3.ToString();
            label_Cs4.Text = Cs4.ToString();//


            int[] vectcas = new int[4] { Cs1, Cs2, Cs3, Cs4 };
            int[] minmax = new int[2]; 
            double Pro1 =0;
            if (checkBox_2Brazos.Checked)
            {

                minmax = Sum_mayor(vectcas);
                Pro1 = (minmax[0] * 1000) / (minmax[1]);


            }
            else
            {
                minmax = Sum_Vmayor(vectcas);
                Pro1 = (minmax[0] * 1000) / (minmax[1]);
            }
             
            label40.Text = (Pro1/1000).ToString();


            label_ModCs1.Text = UPm1.ToString();
            label_ModCs2.Text = UPm2.ToString();
            label_ModCs3.Text = UPm3.ToString();


          // label28.Text = (Cs1).ToString() + " Max:" + rErgef.ToString();
            SumCs=Cs1 + Cs2 + Cs3 + Cs4;
            label28.Text = "Sum: " + (SumCs).ToString() + "\nRef: " + rErgef.ToString() + "  Prob: " + (Prob/1000).ToString();
            if(checkBox18.Checked)  graficar03.graficar6Entradas(Cs1, Cs2, Cs3, Cs4, rErgef, (int)Prob* SumaEn, numericUpDown_MaxC1, pictureBox_ADC1);
            else graficar03.graficar6Entradas(Cs1, Cs2, Cs3, Cs4, rErgef, SumCs * SumaEn, numericUpDown_MaxC1, pictureBox_ADC1);

            //graficar04.graficar4Entradas(UPm1, UPm2, UPm3, 0, numericUpDown12, pictureBox_mod);
            label20.Text = FucnMinMax(regMin,regMAx);
            if (guardarDataToolStripMenuItem.Checked)
            {
                tiempoS = tiempoOk.timeString();
                Guardar10.escribelinea(tiempoS + "\t" + Cs1.ToString() + " \t" + Cs2.ToString() + "\t" + Cs3.ToString() + "\t" + Cs4.ToString() + "\t" + rErgef.ToString()
                     + "\t"  + UPm1.ToString() + "\t" + UPm2.ToString() + "\t" + UPm3.ToString(), "_Cs.txt");

            }
            if (enable_muestrear)
            {
               
                if (Nmuestrea > numericUpDown_muestrear.Value-2)
                {
                    Guardar10.escribelinea(tiempoS + "\t" + SmuestreaAPD1 + Cs1.ToString(), "_MAPD1.txt");
                    Guardar10.escribelinea(tiempoS + "\t" + SmuestreaAPD2 + Cs2.ToString(), "_MAPD2.txt");
                    Guardar10.escribelinea(tiempoS + "\t" + SmuestreaAPD3 + Cs3.ToString(), "_MAPD3.txt");
                    Guardar10.escribelinea(tiempoS + "\t" + SmuestreaAPD4 + Cs4.ToString(), "_MAPD4.txt");
 
                    enable_muestrear = false;
                    Nmuestrea = 0;
                   SmuestreaAPD1 = "";
                    SmuestreaAPD2 = "";
                    SmuestreaAPD3 = "";
                    SmuestreaAPD4 = "";
                    Console.Beep(700, 100);
                }
                else
                {
                    SmuestreaAPD1 += Cs1.ToString() + " \t";
                    SmuestreaAPD2 += Cs2.ToString() + " \t";
                    SmuestreaAPD3 += Cs3.ToString() + " \t";
                    SmuestreaAPD4 += Cs4.ToString() + " \t";
                Nmuestrea++;
 
                }
            }
            double counsFid=Cs1;
            if (radioButton_c2.Checked)
                counsFid=Cs2;
            if (radioButton_c3.Checked)
                counsFid=Cs3;
            if (radioButton_c4.Checked)
                counsFid=Cs4;
            if(SumCs>0)Fidelidad01=counsFid/SumCs;

            label_fid.Text="F="+(Math.Round(Fidelidad01,2)).ToString();
           // if(Fidelidad01>0.8)                numericUpDown8.Value=(decimal)(counsFid*0.95);
            if(checkBox_Cs4Ref.Checked)
            FuncambiaUmbral();
        }
        string SmuestreaAPD1 = "",SmuestreaAPD2 = "",SmuestreaAPD3 = "",SmuestreaAPD4 = "";
        double Fidelidad01 = 0;
        int CountChan = 0;
        private void FuncambiaUmbral()
        {
            CountChan++;
            if (CountChan > numericUpDown25.Value)
            {
                CountChan = 0;
                switch (cambiaUmbral)
                {
                    case 0: radioButton_c1.Checked = true; cambiaUmbral = 1; break;
                    case 1: radioButton_c2.Checked = true; cambiaUmbral = 2; break;
                    case 2: radioButton_c3.Checked = true; cambiaUmbral = 3; break;
                    case 3: radioButton_c4.Checked = true; cambiaUmbral = 0; break;
                    default: radioButton_c1.Checked = true; cambiaUmbral = 0;
                        break;

                }

            }
            
        }
        double MinZ1=0, MAxZ1=0, CountMinMax=0;

        private string FucnMinMax(int rmin,int rmx )
        {
  
            if (CountMinMax >10)
            {
                CountMinMax = 0;
                MinZ1 = rmin;
                minCs4 = 1000;
                MAxZ1 = rmx;
                MaxCs4 = 0;
                if(checkBox16.Checked)
                enviarNum(15); //resetMinMax

            }
            else
                CountMinMax++;


            Visi = (MAxZ1 - MinZ1) / (MAxZ1 + MinZ1);
           string s= "max:" + MAxZ1.ToString() + "\r\n Min:" + MinZ1.ToString() + "\r\n  V:" + Math.Round(Visi, 2).ToString();
           return s;
        }

        int EnableMod = 0;
        int Cs1 = 0, Cs2 = 0, Cs3 = 0, Cs4 = 0, Vp1 = 0, Vp2 = 0, Vp3 = 0, Vp4 = 0;
        graficar  graficar01=new graficar();
        graficar graficar02 = new graficar();
        graficar graficar03 = new graficar();
        graficar graficar04 = new graficar();
        graficar graficar05 = new graficar();
        int ADC2 = 0, ADC3 = 0, ADC4 = 0;
        int ControlLaser = 0, MuestreoLaser = 0;
        double MaxCs4 = 0, minCs4 = 100 ;
        int ProCs4=0;
        double Visi = 0;
      
          private void ActualizarBufer2(object sender, EventArgs e)
        {
            

            for (int i = 0; i < 30; i++)
            {
         
                Vector_RxBruto[i ] = (byte)matrizbuferRx[i];
            }
           // Cs1 = Vector_RxBruto[0] + Vector_RxBruto[1] * 256 + Vector_RxBruto[2] * 65536 + Vector_RxBruto[3] * 16777216;
            // Cs2 = Vector_RxBruto[4] + Vector_RxBruto[5] * 256 + Vector_RxBruto[6] * 65536 + Vector_RxBruto[7] * 16777216;
            // Cs3 = Vector_RxBruto[8] + Vector_RxBruto[9] * 256 + Vector_RxBruto[10] * 65536 + Vector_RxBruto[11] * 16777216;
            // Cs4 = Vector_RxBruto[12] + Vector_RxBruto[13] * 256 + Vector_RxBruto[14] * 65536 + Vector_RxBruto[15] * 16777216;

            Cs1 = Vector_RxBruto[0] + Vector_RxBruto[1] * 256 + Vector_RxBruto[2] * 65536 + Vector_RxBruto[3] * 16777216;
            Cs3 = Vector_RxBruto[4] + Vector_RxBruto[5] * 256 + Vector_RxBruto[6] * 65536 + Vector_RxBruto[7] * 16777216;
            Cs4 = Vector_RxBruto[8] + Vector_RxBruto[9] * 256 + Vector_RxBruto[10] * 65536 + Vector_RxBruto[11] * 16777216;
            Cs2 = Vector_RxBruto[12] + Vector_RxBruto[13] * 256 + Vector_RxBruto[14] * 65536 + Vector_RxBruto[15] * 16777216;

            Vp1 = Vector_RxBruto[16];
            Vp2 = Vector_RxBruto[17];
            Vp3 = Vector_RxBruto[18];
            Vp4 = Vector_RxBruto[19];

            label28.Text = (Cs1 + Cs2 + Cs3 + Cs4).ToString();
            VisorMaxMin(  Cs1,   Cs2,  Cs3,  Cs4) ; 
            indexRx2 = indexRx;

            ADC2 = Vector_RxBruto[20] + Vector_RxBruto[21] * 256;
            ADC3 = Vector_RxBruto[22] + Vector_RxBruto[23] * 256;
            ADC4 = Vector_RxBruto[24] + Vector_RxBruto[25] * 256;
            MuestreoLaser = Vector_RxBruto[26] + Vector_RxBruto[27] * 256;
            ControlLaser = Vector_RxBruto[28] + Vector_RxBruto[29]  * 256;

            ReferenciaMovil();

            if (EnableMod == 1)
            {
                if (checkBox2.Checked)
                {
                    if (Cs1 > MaxG[0])
                        MaxG[0] = Cs1;

                    if (Cs2 > MaxG[1])
                        MaxG[1] = Cs2;
                    if (Cs3 > MaxG[2])
                        MaxG[2] = Cs3;

                    if (Cs4 > MaxG[3])
                        MaxG[3] = Cs4;

                    label_ModCs1.Text =  ((int)(100* Cs1 /MaxG[0])).ToString();
                    label_ModCs2.Text = ((int)(100 * Cs2 / MaxG[1])).ToString();
                    label_ModCs3.Text = ((int)(100 * Cs3 / MaxG[2])).ToString();
                    label_ModCs4.Text = ((int)(100 * Cs4 / MaxG[3])).ToString();
                 //   graficar04.graficar4Entradas((int)(100 * Cs1 / MaxG[0]), (int)(100 * Cs2 / MaxG[1]), (int)(100 * Cs3 / MaxG[2]), (int)(100 * Cs4 / MaxG[3]), numericUpDown12, pictureBox_mod);

                }
                else
                {
                    label_ModCs1.Text = Cs1.ToString();
                    label_ModCs2.Text = Cs2.ToString();
                    label_ModCs3.Text = Cs3.ToString();
                    label_ModCs4.Text = Cs4.ToString();//
                  //  graficar04.graficar4Entradas(Cs1, Cs2, Cs3, Cs4, numericUpDown12, pictureBox_mod);
                }
                double MAxCss = Cs1;
                if(Cs2>MAxCss)
                    MAxCss=Cs2;
                if (Cs3 > MAxCss)
                    MAxCss = Cs3;
                if (Cs4 > MAxCss)
                    MAxCss = Cs4;


                label27.Text = Math.Round((MAxCss / (double)(Cs1 + Cs2 + Cs3 + Cs4)), 3).ToString();

                if (guardarDataToolStripMenuItem.Checked)
                {
                    tiempoS = tiempoOk.timeString();
                    Guardar10.escribelinea(tiempoS + "\t" + Cs1.ToString() + " \t" + Cs2.ToString() + "\t" + Cs3.ToString() + "\t" + Cs4.ToString() + "\t"
                          + Vp1.ToString() + "\t" + Vp2.ToString() + "\t" + Vp3.ToString() + "\t" + Vp4.ToString() + "\t" + MuestreoLaser.ToString() + "\t" + ControlLaser.ToString() + "\t255"
                         + "\t" + numericUpDown6.Value.ToString() + "\t" + numericUpDown10.Value.ToString() + "\t" + numericUpDown17.Value.ToString(), "_ModCs.txt");

                    if (checkBox_cambiarExt.Checked)
                    {
                        NumVext++;
                        if (NumVext > numericUpDown18.Value-1)
                        {  
                            
                            NumVext = 0;
                            if (checkBox_EnableDaniel.Checked)
                            {

                                switch (cambiaUmbral)
                                {
                                    case 0: radioButton_c1.Checked = true;
                                       numericUpDown6.Value =0;
                                            numericUpDown10.Value = 0;
                                            numericUpDown17.Value = 0;
   
                                        cambiaUmbral = 1; break;
                                    case 1: radioButton_c2.Checked = true; cambiaUmbral = 2; break;
                                    case 2: radioButton_c3.Checked = true; cambiaUmbral = 3; break;
                                    case 3: radioButton_c4.Checked = true; cambiaUmbral = 4; break;
                                    case 4: radioButton_c1.Checked = true; 

                                            numericUpDown6.Value = MatrizPhases[0, 0];
                                            numericUpDown10.Value = MatrizPhases[0, 1];
                                            numericUpDown17.Value = MatrizPhases[0, 2];
                                            ModulacionStep++;
                                            if (ModulacionStep > numericUpDown20.Value)
                                            {
                                                ModulacionStep = 0;
                                                cambiaUmbral = 0;
                                            }
                                        
                                        break;

                                }
                            
 


                            }
                            else
                            {

                             
                                numericUpDown6.Value = MatrizPhases[ModulacionStep, 0];
                                numericUpDown10.Value = MatrizPhases[ModulacionStep, 1];
                                numericUpDown17.Value = MatrizPhases[ModulacionStep, 2];
                                ModulacionStep++;
                                if (ModulacionStep > Phases1.Nphases)
                                {
                                    ModulacionStep = 0;
                                }
                            }


                        }
                    }
                    else NumVext = 0;



                }

            }
            else
            {
                graficar03.graficar5Entradas(Cs1, Cs2, Cs3, Cs4, (int)numericUpDown_umbralControl1.Value, numericUpDown_MaxC1, pictureBox_ADC1);

                label_Cs1.Text = Cs1.ToString();
                label_Cs2.Text = Cs2.ToString();
                label_Cs3.Text = Cs3.ToString();
                label_Cs4.Text = Cs4.ToString();//
            }

           if (guardarDataToolStripMenuItem.Checked)
           {
               tiempoS = tiempoOk.timeString();
               if (EnableMod == 1)
               {
                   Guardar1.escribelinea(tiempoS + "\t" + Cs1.ToString() + " \t" + Cs2.ToString() + "\t" + Cs3.ToString() + "\t" + Cs4.ToString() + "\t"
                             + Vp1.ToString() + "\t" + Vp2.ToString() + "\t" + Vp3.ToString() + "\t" + Vp4.ToString() + "\t" + MuestreoLaser.ToString() + "\t" + ControlLaser.ToString() + "\t255"
                            + "\t" + numericUpDown6.Value.ToString() + "\t" + numericUpDown10.Value.ToString() + "\t" + numericUpDown17.Value.ToString(), "_Cs.txt");
               }
               else
               {
                   Guardar1.escribelinea(tiempoS + "\t" + Cs1.ToString() + " \t" + Cs2.ToString() + "\t" + Cs3.ToString() + "\t" + Cs4.ToString() + "\t"
                        + Vp1.ToString() + "\t" + Vp2.ToString() + "\t" + Vp3.ToString() + "\t" + Vp4.ToString() + "\t" + MuestreoLaser.ToString() + "\t" + ControlLaser.ToString() + "\t0"
                        + "\t" + numericUpDown6.Value.ToString() + "\t" + numericUpDown10.Value.ToString() + "\t" + numericUpDown17.Value.ToString(), "_Cs.txt");
               }


           }
                    
                

              if (button6.Text == "End mod")
              {

                  enviarNum(1);
                  enviarNum(VoltADC);
                  enviarNum(16);
                  enviarNum(17);
                  enviarNum(18);
                  VoltADC++;
                  if (VoltADC > 128) VoltADC = 0;
              }

  

        }
          
        private void VisorMaxMin(int Css1, int Css2, int Css3, int Css4)          
        {
            int Csss = Css1;
            if(SelecVisor==2)
            Csss = Css2;
            if (SelecVisor ==3)
                Csss = Css3;

            if (SelecVisor == 4)
                Csss = Css4;

            if (Csss > MaxCs4)
                    {
                        MaxCs4 = Csss;
           
                    }
            if (Csss < minCs4)
                {
                    minCs4 = Csss;

                }
            
              Visi = (MaxCs4 - minCs4) / (MaxCs4 + minCs4);
 
           
            label20.Text ="max:"+MaxCs4.ToString()+"\r\n Min:"+minCs4.ToString()+"\r\n  V:"+ Math.Round(Visi, 2).ToString(); 
       
}
          private void ReferenciaMovil( )
          {
             /* int CsMref=Cs1;
              if(radioButton_c2.Checked)
                  CsMref=Cs2;
              if(radioButton_c3.Checked)
                  CsMref=Cs3;
              if(radioButton_c4.Checked)
                  CsMref=Cs4;
 
              if (checkBox_Cs4Ref.Checked)
              {

                  if (numericUpDown_umbralControl1.Value < (CsMref))
                  {
                      CuentaCs4ParaReset++;
                      if (CuentaCs4ParaReset > 10)
                      {
                          CuentaCs4ParaReset = 0;
                          Conv.REsetmedia(CsMref);
                      }
                  }

                  if (EnableMod != 1)
                  {
                      try
                      {

                          if (numericUpDown_umbralControl1.Value < (decimal)(CsMref * 0.97))
                          {
                              numericUpDown_umbralControl1.Value = (decimal)(CsMref * 0.97);

                          }
                          else
                          {
                              if (numericUpDown_umbralControl1.Value > mediaCs4)
                                  numericUpDown_umbralControl1.Value -= 1;



                          }
                          mediaCs4 = Conv.media(CsMref, 500);
                      }
                      catch { }
                  }
              }*/



          }
          decimal mediaCs4 = 0;
          double[] MaxG = new double[4];
          int NumVext = 0;
int VoltADC = 0;
int[,] MatrizPhases = new int[1000, 3];
int ModulacionStep = 0;

          private void ControlarLaser(decimal referencia,int Ylaser)
          {
              if (Ylaser > (referencia) || Ylaser < (referencia))
              {

                  numericUpDown9.Value +=(int)( ( referencia-Ylaser)/4);
              }
          }
          Control_MPPT01 mppt01 = new Control_MPPT01();
        //label12.Text = "PC:" + procesoControlAntiguo.ToString() + "  Ps:" + pasosControl.ToString();
      
         
        CtrlMPPT3 CtrlMPPT3_01 = new CtrlMPPT3();
        int countControl = 0;
        bool rutinaFase = false;
        double ETopAnt = 0, EleftAnt = 0, ErightAnt=0;
   



          string tiempoS = "0";
          TimerJc tiempoOk = new TimerJc();

        byte bRx = 0;
        int FrecuenciaTrabajo = 0, Qucocinete = 0;
        private void ActualizarParametros(object sender, EventArgs e)
        {
            matrizbuferRx2 = matrizbuferRx;
            indexRx2 = indexRx;
            listBox1.Items.Clear();


            listBox1.Items.Add("Mx");
            for (int i = 0; i < indexRx; i++)//256 + dimMax; i++)
            {
                listBox1.Items.Add((i).ToString() + " - " + matrizbuferRx2[i].ToString());
            }
            FrecuenciaTrabajo = matrizbuferRx2[0] + matrizbuferRx2[1] * 256 + matrizbuferRx2[2] * 65536 + matrizbuferRx2[3] * 16777216;
            Qucocinete = matrizbuferRx2[4] + matrizbuferRx2[5] * 256 + matrizbuferRx2[6] * 65536 + matrizbuferRx2[7] * 16777216;
            listBox1.Items.Add("NRx:.." + (bRx).ToString() + ".. "
                );
            bRx++;
            label1.Text = "Frec: " + FrecuenciaTrabajo.ToString() + " Q:" + Qucocinete.ToString();

        }


        byte[,] matrizEstados = new byte[1000, 2];
      /// <summary>
      /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void ActualizarBufer(object sender, EventArgs e)
        {
            indexRx2 = indexRx;
            for (int i = 0; i < indexRx2; i++)
            {
                matrizbuferRx2[i] = matrizbuferRx[i + 1];
                Vector_RxBruto[i + 1] = (byte)matrizbuferRx[i];
            }
            Vector_RxBruto[0] = 10;
            EnviarEthernet(Vector_RxBruto);

            matrizEstados = Conv.CodeBuf2MAtriz2(matrizbuferRx2, (int)numericUpDown1.Value / 2);
            listBox1.Items.Clear();

            listBox1.Items.Add("Mx");
            for (int i = 0; i < 20; i++)//256 + dimMax; i++)
            {
                listBox1.Items.Add((i).ToString() + " BE:" + matrizEstados[i, 0].ToString());
            }
            listBox1.Items.Insert(0, "NRx:.." + (bRx).ToString() + ".. ");

            bRx++;

        }

        int CuentaCs4ParaReset = 0;

        Phases Phases1 = new Phases();
        private void Form1_Load(object sender, EventArgs e)
        {
            DetectarPuertos();
            tiempoOk.tiempoReset();

            MatrizPhases = Phases1.LeerPhases("phases", ".txt");
            for (int i = 0; i < Phases1.Nphases+1; i++)
            {
                listBox1.Items.Add(MatrizPhases[i, 0].ToString() + "\t" + MatrizPhases[i, 1].ToString() + "\t" + MatrizPhases[i,2].ToString());
            }
        // GuardaPropiedades.DireccionArchivoPropiedades = "Propiedades.txt";
        


           
            
        }
        /*
 
30 	30	30
30	34	40
30	41	50
30	48	60
30	55	70
30	62	80
30	69	90
30	76	100
30	83	110
30	90	120
40	80	120
50	70	120
60	60	120
70	50	120
80	40	120
90	30	120
90	35	107
90	40	100
90	45	93
90	50	86 enviarNum(1);
            enviarNum(30);
            enviarNum(30);
90	55	79
90	60	72
90	65	65
90	70	58
90	75	51
90	80	44
90	85	37
90	90	30
80	83	30
70	76	30
         * 
         * */
        private void button1_Click(object sender, EventArgs e)
        {
            if ( button1.Text == "Medida On")
            {
                enviarNum(40);
               //  enviarNum(41);
               button1.Text = "Medida Off";
            }
            else
            {
                  enviarNum(41);
                 button1.Text = "Medida On";
            }
            
          



        }
        public void enviarNum(int num)
        {

            byte[] xxbufer = new byte[1];
            xxbufer[0] = (byte)num;
            //xxbufer = Encoding.ASCII.GetBytes(nnbufer);
            Sp.Write(xxbufer, 0, xxbufer.Length);
        }

        Conversiones Conv = new Conversiones();
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                enviarOrden((int)numericUpDown1.Value);
                enviarNum(10);
            }
            catch { }
        }
        private void enviarOrden(int orden)
        {
            Conv.int2Byte4(orden);
            enviarNum(1);
            enviarNum(Conv.Byte4[0]);
            enviarNum(2);
            enviarNum(Conv.Byte4[1]);
            enviarNum(3);
            enviarNum(Conv.Byte4[2]);
            enviarNum(4);
            enviarNum(Conv.Byte4[3]);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Enviar1sesion();
        }
        private void Enviar1sesion()
        {
            enviarNum(12);
            enviarNum(10);
            enviarNum(12);
            enviarNum(0);
        }
        private void Enviar1sesionAs()
        {
            enviarNum(12);
            enviarNum(9);
            enviarNum(12);
            enviarNum(8);
        }

        
        private void ContinuaaON()
        {
            enviarNum(12);
            button3.Text = "Fin";
            enviarNum(10);
        }
        private void ContinuaaOff()
        {
            enviarNum(12);
            button3.Text = "Continuo";


            enviarNum(0);
        }           
        
        private void button3_Click(object sender, EventArgs e)
        {

            if (button3.Text == "Continuo")
            {
                ContinuaaON();
               
            }
            else
            {
                ContinuaaOff();
            }
        }

/*
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown3.Value);
            enviarNum(9);
        }
 * */

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown4.Value);
            enviarNum(11);
        }
        utilidades Utilidades = new utilidades();


        double ETop = 0;
        double Eleft = 0;
        double Eright = 0;
        double Ebottom = 0;
        double pasos = 0.05;
        double limitePi =2;
        string[] labels = new string[4];
        static int casillaT = 15 - 1;
        static int casillaL = 22 - 1;
        static int casillaR = 10 - 1;
        static int casillaB = 17 - 1;
        ConversionesEspejo ce = new ConversionesEspejo();
        double[] FLATMAP = new double[37];// ce.FLATMAP_DM1_nm;
        string s = "";
        bool cambiarTodo = true;
        

      

        double[] correcion = new double[37];
        private void GuardarData()
        {
            double[] MonitorActualPi = new double[37];
            double[] MonitorActualVolt = new double[37];
            int index = (int)numericUpDown_indexEstadoActual.Value;
            string[] labels = new string[4];
            FLATMAP = ce.FLATMAP_DM1_nm;
            MonitorActualVolt = ce.FLATMAP_DM1_Porcentaje;

            MonitorActualPi[casillaT] = ETop;
            MonitorActualPi[casillaL] = Eleft;
            MonitorActualPi[casillaR] = Eright;
            MonitorActualPi[casillaB] = Ebottom;
            Utilidades.Guardardouble("Estado" + index.ToString(), ".pi", MonitorActualPi, 36);

            if (index == 0)
            {
                correcion = MonitorActualPi;
                MonitorActualVolt[casillaT] = ce.pi2porcentajeDM1(ETop, 0, FLATMAP[casillaT]);
                MonitorActualVolt[casillaL] = ce.pi2porcentajeDM1(Eleft, 0, FLATMAP[casillaL]);
                MonitorActualVolt[casillaR] = ce.pi2porcentajeDM1(Eright, 0, FLATMAP[casillaR]);
                MonitorActualVolt[casillaB] = ce.pi2porcentajeDM1(Ebottom, 0, FLATMAP[casillaB]);
            }
            else
            {
                MonitorActualVolt[casillaT] = ce.pi2porcentajeDM1(ETop, correcion[casillaT], FLATMAP[casillaT]);
                MonitorActualVolt[casillaL] = ce.pi2porcentajeDM1(Eleft, correcion[casillaL], FLATMAP[casillaL]);
                MonitorActualVolt[casillaR] = ce.pi2porcentajeDM1(Eright, correcion[casillaR], FLATMAP[casillaR]);
                MonitorActualVolt[casillaB] = ce.pi2porcentajeDM1(Ebottom, correcion[casillaB], FLATMAP[casillaB]);

            }

            Utilidades.Guardardouble("Estado" + index.ToString(), ".volt", MonitorActualVolt, 36);

            label_top.Text = MonitorActualVolt[casillaT].ToString();
            label_left.Text = MonitorActualVolt[casillaL].ToString();
            label_right.Text = MonitorActualVolt[casillaR].ToString();
           

            byte[] valoreshexadecimal = new byte[73];


            valoreshexadecimal = Conv.Voltaje2Hexa(MonitorActualVolt, (byte)index);
            if (button4.Text == "Off")
            {
                enviarNum(15);
                enviarNum(128);
                for (int i = 0; i < 73; i++)
                {
                    enviarNum(15);
                    enviarNum(valoreshexadecimal[i]);
                }
            }
            enviarNum(15);
            enviarNum(index);

        }

    

        private void textBox_button_TextChanged(object sender, EventArgs e)
        {

            try
            {
                Ebottom = Utilidades.str2double(Ebottom, textBox_button, limitePi, pasos);


            }
            catch { textBox_button.Text = Ebottom.ToString(); }
            if(cambiarTodo)
            GuardarData();
        }
 
        #region clases y variables globales.

        //Comunicación por ethernet.
        Puertos Ptos = new Puertos();
        UdpClient udpServer = new UdpClient(puerto_in);
        UdpClient udpClient = new UdpClient(puerto_out);
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

        byte[] cabecera = new byte[5] { 0, 0, 255, 0, 255 };
        byte[] final = new byte[5] { 0, 0, 253, 0, 253 };

        static int puerto_out = 1011;
        static int puerto_in = 1010;
        public string PuertoLocal = ""; // toolStripIPlocal.Text 
        public string PuertoLink = "";//toolStripCombo_IPconfig.Text


        #endregion
        #region Conectar desconectar Ethernet

        bool UDP_Enable_Receptor = false;
        int errorConexion = 0;
        public void ConectarUDP()
        {
            errorConexion = 100;
            try
            {
                udpClient.Connect(IPAddress.Parse(PuertoLink), puerto_out);
                PuertoLocal = Ptos.verIPLocal() + " --> " + PuertoLink;
                toolStripStatusLabel_coneccionUDP.Text = PuertoLocal;
                UDP_Enable_Receptor = true;
                Thread thdUDPServer = new Thread(new ThreadStart(Fuction_ReceptorUDP));
                thdUDPServer.Start();
            }
            catch { MessageBox.Show("Error de concección ( " + errorConexion.ToString() + ")"); }
        }
        public void DesconectarUDP()
        {
            UDP_Enable_Receptor = false;
            udpServer.Close();
            PuertoLocal = "UDP Desconectado";
        }
        #endregion
        #region Recepción de datos por UDP
        int index_UDP_Rx = 0;
        public bool RecepcionUDPOk = false;

        public void Fuction_ReceptorUDP()
        {
            while (UDP_Enable_Receptor)
            {
                try
                {

                    Byte[] receiveBytes = udpServer.Receive(ref RemoteIpEndPoint);

                    for (int i = 0; i < receiveBytes.Length; i++)
                    {
                        if (i > 4)
                        {
                            if (
                                     Vector_RxBruto[i - 5] == 0 &&
                                     Vector_RxBruto[i - 4] == 0 &&
                                     Vector_RxBruto[i - 3] == 255 &&
                                     Vector_RxBruto[i - 2] == 0 &&
                                     Vector_RxBruto[i - 1] == 255
                                )
                            {
                                index_UDP_Rx = 0; // si detecta la cabecera resetea el index de la matris adquisición.
                            }
                            if (i > 12)
                            {
                                if (
                                          Vector_RxBruto[i - 10] == 0 &&
                                          Vector_RxBruto[i - 9] == 0 &&
                                          Vector_RxBruto[i - 8] == 253 &&
                                          Vector_RxBruto[i - 7] == 0 &&
                                          Vector_RxBruto[i - 6] == 253
                                                 )
                                {
                                    this.Invoke(new EventHandler(ActualizarUDP));
                                    index_UDP_Rx = 0;
                                    break; //si la cadena final se detecta se apaga el "For(receiveBytes)"
                                }
                            }
                        }


                        Vector_RxBruto[index_UDP_Rx] = receiveBytes[i];

                        if (index_UDP_Rx > 1450) index_UDP_Rx = 0; //desborde bufer maximo 1000 String ==> 8000bit/s
                        else index_UDP_Rx++;
                    }

                }

                catch { }
            }

        }

        #endregion
        #region Actualización de data

        byte[] Vector_RxBruto = new byte[400020];
        byte[] Vector_RxOk = new byte[4020];
        byte[] vectorAenviarAnt = new byte[4020];
        bool OrdenEnviar = false;
        private void ActualizarUDP(object sender, EventArgs e)
        {
             
            switch (Vector_RxBruto[0])            //orden =1 recibir paquete.
            {
                case 20:
                    numericUpDown_indexEstadoActual.Value = Vector_RxBruto[1];
                    EnviarEthernet(Vector_RxBruto);
                    break;
                case 30:
                    Enviar1sesion();
                    EnviarEthernet(Vector_RxBruto);
                    break;
                case 31:
                    Enviar1sesionAs();
                    EnviarEthernet(Vector_RxBruto);
                    break;
                case 40:
                    EncenderLAser();
                    EnviarEthernet(Vector_RxBruto);
                    break;
                case 41:
                    OffLAser();
                    EnviarEthernet(Vector_RxBruto);
                    break;

                case 50:
                    ContinuaaON();
                    EnviarEthernet(Vector_RxBruto);
                    break;
                case 51:
                    ContinuaaOff();
                    EnviarEthernet(Vector_RxBruto);
                    break;
                case 52:
                    AsincronoON();
                    EnviarEthernet(Vector_RxBruto);
                    break;
                case 53:
                    AsincronoOff();
                    EnviarEthernet(Vector_RxBruto);
                break;
            }
        }


        #endregion
        #region Salida de datos UDP
        public void EnviarEthernet(byte[] data)
        {
            ArmaEvia_DataRx(data, cabecera, final);
        }
        public void ArmaEvia_DataRx(byte[] data, byte[] cabecera, byte[] final)
        {

            byte[] mensaje = new byte[1500];//8cabecera+1200data+10Final
            int k = 0;
            for (int i = 0; i < 5; i++)
            {
                mensaje[k] = cabecera[i];
                k++;
            }
            for (int i = 0; i < 1400; i++)
            {
                mensaje[k] = data[i];
                k++;
            }

            for (int i = 0; i < 5; i++)
            {
                mensaje[k] = final[i];
                k++;
            }

            outUDP(mensaje);
        }
        private void outUDP(Byte[] senddata)
        {
            try
            {
                //Byte[] senddata = Encoding.ASCII.GetBytes(textBox_out.Text);
                udpClient.Send(senddata, senddata.Length);
            }
            catch { }
        }

        #endregion

        private void conectarToolStripMenuItem_conectar_Click(object sender, EventArgs e)
        {
            if (conectarToolStripMenuItem_conectar.Text == "Conectar")
            {
                PuertoLink = toolStripCombo_IPconfig.Text;
                // EstablecerArchivoRx();
                conectarToolStripMenuItem_conectar.Text = "desconectar";
                ConectarUDP();
            }
            else
            {
                DesconectarUDP();
                conectarToolStripMenuItem_conectar.Text = "Conectar";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            enviarNum(13);
            if (radioButton_ale.Checked)
            { enviarNum(0); }
            if (radioButton_sec.Checked)
            { enviarNum(1); }
            if (radioButton_fijo.Checked)
            { enviarNum(2); }


        }
 

        private void button4_Click(object sender, EventArgs e)
        {

            if (button4.Text == "On Laser Control")
            {

                if (button3.Text == "Fin")
                {
                    button3.Text = "Continua";
                    enviarNum(12);
                    enviarNum(0);
                }

             
                EncenderLAser();
               
            }
            else
            {
               
                              OffLAser();
            }
        }
        private void EncenderLAser()
        {
            enviarLAser(99);
            button4.Text = "Off";
        }
        private void  OffLAser()
        {
             enviarLAser(100);
             button4.Text = "On Laser Control";

        }
        public void enviarLAser(int num)
        {

            byte[] xxbufer = new byte[1];
            xxbufer[0] = (byte)num;
            //xxbufer = Encoding.ASCII.GetBytes(nnbufer);
            serialPort1cONTROLLASER.Write(xxbufer, 0, xxbufer.Length);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "Sincrono")
            {
                AsincronoON();

            }
            else
            {
                AsincronoOff();
            }
        }
        private void AsincronoON()
        {
            enviarNum(12);
            button5.Text = "Fin Sincrono";
            enviarNum(9);
        }
        private void AsincronoOff()
        {
            enviarNum(12);
            button5.Text = "Sincrono";

            enviarNum(8);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DesconectarUDP();
            Desconectar();
        }

        private void conectarToolStripMenuItem_laserControl_Click(object sender, EventArgs e)
        {
            if (conectarToolStripMenuItem_laserControl.Text == "Conectar")
            {


                if (conectar2(comboBox_Puertosexistentes_LAserdecontrol.SelectedItem.ToString()))
                {
                    conectarToolStripMenuItem_laserControl.Text = "Desconectar";


                }
                else
                {
                    MessageBox.Show("Error en el Puerto  ");

                }

            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown2.Value);
            enviarNum(9);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                numericUpDown5.Increment=10;
            else
                 numericUpDown5.Increment=1;
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            int Volt = (int)numericUpDown5.Value;
            int Ndac = (int)numericUpDown3.Value;
            enviarNum(3);
            enviarNum(Volt);
            enviarNum(4);
            enviarNum(Ndac);
            enviarNum(17);

        }

        
        private void EnviarMod(double ValorPi,int NumDac )
        {
            //min-->38
            //max-->95
            double Prevol = (ValorPi + 2)/ 4*255 ;
            int Volt = (int)Prevol;
     
            enviarNum(3);
            enviarNum(Volt);
            enviarNum(4);
            enviarNum(NumDac);
            enviarNum(17);

        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (button6.Text == "Start mod")
            {
                button6.Text = "End mod";
            }
            else
            {
                button6.Text = "Start mod";
            }
        }
        bool BoolSube = true;

        private void sierra()
        {
            if (BoolSube)
            {
                ETop = ETop + pasos;
                Eleft = Eleft + pasos;
                Eright = Eright + pasos;
                if (ETop > limitePi)
                {
                    ETop = limitePi;
                    Eleft = limitePi;
                    Eright = limitePi;
                    BoolSube = false;
                }

            }
            else {
                ETop = ETop - pasos;
                Eleft = Eleft - pasos;
                Eright = Eright - pasos;
                if (ETop < -limitePi)
                {
                    ETop = -limitePi;
                    Eleft = -limitePi;
                    Eright =- limitePi;
                    BoolSube = true;
                }
 
            }
        }
        bool posEgde = true, posEgdeX2=true;
        int cuentaPasos = 0;
        private void PruebaModulacion()
        {
            cuentaPasos++;

            ETop = ETop + pasos;
            if (ETop > limitePi)
                ETop = -limitePi;


            Eleft = Eleft - pasos;
            if (Eleft < -limitePi)
                Eleft = limitePi;

            Eright = Eright + pasos * 2;

            if (Eright > limitePi)
            {
                Eright = -limitePi;
               
            }
            if (cuentaPasos > 40)
            {
                groupBox2.BackColor = Color.WhiteSmoke;
                rutinaFase = false;
                cuentaPasos = 0;
            }
                 
        }
        private void Mod()
        {


            /* Eleft = Eleft - pasos;
             if (Eleft < -limitePi) Eleft = limitePi;


             Eright = Eright + pasos*2;
             if (Eright > limitePi)
             {
                 Eright = -limitePi;
                 ETop = -limitePi;
                 Eleft = limitePi;
             }
             */
            

            if (posEgde)
            {
                ETop = ETop + pasos;
                Eleft = Eleft- pasos;
                if (ETop > limitePi)
                {
                    posEgde = false;
                    posEgdeX2 = true;
                    ETop = limitePi;
                    Eleft =-limitePi; 
                }

                if (posEgdeX2)
                {
                    Eright = Eright + pasos * 2;
                    if (Eright > limitePi)
                    {
                        posEgdeX2 = false;
                        Eright = limitePi;
                    }

                }
                else
                {
                    Eright = Eright - pasos * 2;
                    if (Eright < -limitePi)
                    {
                        posEgdeX2 = true;
                        Eright = -limitePi;
                    }
                }
            }
            else{
                ETop = ETop - pasos;
                Eleft = Eleft  + pasos;

                if (ETop < -limitePi)
                {
                    posEgde = true;
                    posEgdeX2 = true;
                    ETop = -limitePi;
                    Eleft = limitePi;
                }

                if (posEgdeX2)
                {
                    Eright = Eright + pasos * 2;
                    if (Eright > limitePi)
                    {
                        posEgdeX2 = false;
                        Eright = limitePi;
                    }

                }
                else
                {
                    Eright = Eright - pasos * 2;
                    if (Eright < -limitePi)
                    {
                        posEgdeX2 = true;
                        Eright = -limitePi;
                    }
                }

            }

        }
        private void dienteSierra()
        {
             
                ETop = ETop + pasos;
                Eleft = Eleft + pasos;
                Eright = Eright + pasos;

 
        }
   

     
        private void textBox_limitepi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                limitePi = Utilidades.str2double(Eright, textBox_limitepi, 1.25, 0.1);

            }
            catch { textBox_limitepi.Text = limitePi.ToString(); }
            
        }

        private void textBox_paoss_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pasos = Utilidades.str2double(pasos, textBox_paoss, 1.25, 0.0039);

            }
            catch { textBox_paoss.Text = pasos.ToString(); }
            mppt01.pasosControlSet = pasos;
            //controlwmppt.pasosControlSet = pasos;
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ///reset Fifo
            enviarNum(1);
            enviarNum(10); ///reset on
            enviarNum(14);
            enviarNum(1);
            enviarNum(11); ///reset off
            enviarNum(14);
      
            if (timer3.Enabled)
            {
                //readyfraficoOsc = true;
                timer3.Enabled = false;
                button7.Text = "Cuentas On";
            }
            else
            {
                timer3.Enabled = true;
                button7.Text = "Cuentas Off";
            }
            
        }
        int SelecVisor = 1;
        private void label_Cs1_Click(object sender, EventArgs e)
        {
            SelecVisor =1;
        }

        IO_Propiedades GuardaPropiedades = new IO_Propiedades();

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown8.Value);
            enviarNum(20);
            GuardaPropiedades.guardar(0, "ref", numericUpDown8.Value.ToString());

        }
        GuardarData Guardar1=new GuardarData();
        GuardarData Guardar10 = new GuardarData();
        string dataP = "";
        private void guardarDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!guardarDataToolStripMenuItem.Checked)
            {
                ModulacionStep = 0;
                cambiaUmbral = 0;
                NumVext = (int)numericUpDown18.Value;
                // tiempoOk.tiempoReset();
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                { dataP = saveFileDialog1.FileName;
                    Guardar1.pathdataCs = saveFileDialog1.FileName;
                    Guardar1.GeneraArchivo("_Cs.txt");
                    Guardar1.escribelinea("%time \tCs1 \tCs2 \tCs3 \tCs4 \tVpm1 \tVpm2 \tVpm3 \tVpm4 \tPotLAser \tControlLAser", "_Cs.txt");

                    Guardar10.pathdataCs = saveFileDialog1.FileName;
                 //   Guardar10.GeneraArchivo("_ModCs.txt");
                ////    Guardar10.escribelinea("%time \tCs1 \tCs2 \tCs3 \tCs4 \tVpm1 \tVpm2 \tVpm3 \tVpm4 \tPotLAser \tControlLAser", "_ModCs.txt");


                    guardarDataToolStripMenuItem.Checked = true;
                    Guardar2.pathdataCs = dataP;
                    NumG = 0;
                    Nombre2 = "01" + "_3H_Cs.txt";
                   // Guardar2.GeneraArchivo(Nombre2);
                   // Guardar2.escribelinea("%time \tCs1 \tCs2 \tCs3 \tCs4 \tVpm1 \tVpm2 \tVpm3 \tVpm4 \tPotLAser \tControlLAser", Nombre2);

                }
                else
                    guardarDataToolStripMenuItem.Checked = false;

            }
            else
                guardarDataToolStripMenuItem.Checked = false;


        } 
        GuardarData Guardar2 = new GuardarData();

        private void guardardat3horasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            countProbEn = 0;
            ProcCambiaProbEn = 0;
        }
        string Nombre2 = "";
        int NumG = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            label10.Text = NumG.ToString();
            if(NumG<10)
            Nombre2 = "0" + NumG.ToString() + "_3H_Cs.txt";
            else
                Nombre2 =  NumG.ToString() + "_3H_Cs.txt";

            NumG++;
            if (control1ToolStripMenuItem_Control2.Checked)
            {
                control1ToolStripMenuItem_Control2.Checked = false;
                control1ToolStripMenuItem_Control1.Checked = true;

            }
            else
            {
                control1ToolStripMenuItem_Control2.Checked =true ;
                control1ToolStripMenuItem_Control1.Checked = false;
            }
            
            Guardar2.GeneraArchivo(Nombre2);
            Guardar2.escribelinea("%time \tCs1 \tCs2 \tCs3 \tCs4 \tVpm1 \tVpm2 \tVpm3 \tVpm4 \tPotLAser \tControlLAser", Nombre2);
        }
        
        private void control1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            
        }

        private void modularToolStripMenuItem_modular1_Click(object sender, EventArgs e)
        {

        }
        int Nmuestras = 2;

      

   

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click_1(object sender, EventArgs e)
        {
            Nmuestras = toolStripComboBox1.SelectedIndex + 1;
        }

        private void control1ToolStripMenuItem_Control2_Click(object sender, EventArgs e)
        {

        }

        
        int cambiaUmbral =0;
        int countLaserControl = 0;
        private void timer3_Tick(object sender, EventArgs e)
        {
            //if (readyfraficoOsc)
            {
               // readyfraficoOsc = false;
                enviarNum(1);
                enviarNum(30);
                enviarNum(14);
                enviarNum(1);
                enviarNum(0);
                enviarNum(14);
            }
            

        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown9.Value);
            enviarNum(21);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox3.Checked)
                numericUpDown9.Increment = 100;
            else
                numericUpDown9.Increment = 1;
        }

         

         

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)

                enviarOrden(1);
            else
                enviarOrden(0);

            enviarNum(31);
            numericUpDown9.Value = ControlLaser;
        }

   
 
        private void radioButton_c1_CheckedChanged(object sender, EventArgs e)
        {
            //Conv.REsetmedia(Cs1);
            if(checkBox_2Brazos.Checked)
            enviarOrden(0+4);
            else
                enviarOrden(0);
            enviarNum(17);
        }

        private void radioButton_c2_CheckedChanged(object sender, EventArgs e)
        {
            //Conv.REsetmedia(Cs3);
            if (checkBox_2Brazos.Checked)
                enviarOrden(1 + 4);
            else
            enviarOrden(1);
            enviarNum(17);

 



        }

        private void radioButton_c3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_2Brazos.Checked)
                enviarOrden(2+ 4);
            else
            enviarOrden(2);
            enviarNum(17);
        }

        private void radioButton_c4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_2Brazos.Checked)
                enviarOrden(3 + 4);
            else
            enviarOrden(3);
            enviarNum(17);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox5.Checked)
            enviarOrden(1);
            else
             enviarOrden(0);
            enviarNum(34);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            int Volt = Vp2-(int)numericUpDown11.Value ;
  
            enviarNum(1);
            enviarNum(Volt);
            enviarNum(16);
       

 

        }

        private void button11_Click(object sender, EventArgs e)
        {
            int Volt = Vp2 + (int)numericUpDown11.Value;

            enviarNum(1);
            enviarNum(Volt);
            enviarNum(16);
       

        }

        private void button9_Click(object sender, EventArgs e)
        {
            int Volt = Vp4 - (int)numericUpDown11.Value;

            enviarNum(1);
            enviarNum(Volt);
            enviarNum(18);

     
        }

        private void button13_Click(object sender, EventArgs e)
        {
 

            int Volt = Vp3 + (int)numericUpDown11.Value;

            enviarNum(1);
            enviarNum(Volt);
            enviarNum(17);
       


        }

        private void button10_Click(object sender, EventArgs e)
        {
            int Volt = Vp4 + (int)numericUpDown11.Value;

            enviarNum(1);
            enviarNum(Volt);
            enviarNum(18);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int Volt = Vp3 - (int)numericUpDown11.Value;

            enviarNum(1);
            enviarNum(Volt);
            enviarNum(17);
        }

        private void numericUpDown13_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown13.Value);
            enviarNum(40);
        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown11.Value);
            enviarNum(35);
        }

        private void numericUpDown14_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown14.Value);
            enviarNum(36);
        }

        private void numericUpDown15_ValueChanged(object sender, EventArgs e)
        {
            //enviarOrden((int)numericUpDown15.Value);
            //enviarNum(37);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            timer3.Enabled = true;
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            numericUpDown_MaxC1.Value = 100;
        }

      
        private void numericUpDown6_ValueChanged_1(object sender, EventArgs e)
        {
            enviarNum(1);
            enviarNum((int)numericUpDown6.Value);
            enviarNum(17); 
        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            enviarNum(1);
            enviarNum((int)numericUpDown10.Value);
            enviarNum(16);
        }

        private void numericUpDown17_ValueChanged(object sender, EventArgs e)
        {
            enviarNum(1);
            enviarNum((int)numericUpDown17.Value);
            enviarNum(18);
        }

        private void numericUpDown_umbralControl1_ValueChanged(object sender, EventArgs e)
        {
           // enviarOrden((int)numericUpDown_umbralControl1.Value);
           // enviarNum(30); 
           
        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {
            MaxG[0] = 1;
            MaxG[1] = 1;
            MaxG[2] = 1;
            MaxG[3] = 1;
        }

        private void label20_Click(object sender, EventArgs e)
        {
            minCs4 = 1000000;
            MaxCs4 = 0;
        }

        private void label_Cs4_Click(object sender, EventArgs e)
        {
            SelecVisor = 4;
        }
        int EnableModPhase = 1 + 2 + 4 + 8;
    

        private int codePMEnable(bool Eni1, bool Eni2, bool Eni3) 
        {
            int En1 = 0, En2 = 0, En3 = 0;
 
            if (Eni1) En1 = 1;  

            if (Eni2) En2 = 2;  

            if (Eni3) En3 = 4;  

            return (En1 + En2 + En3);
        }

      
        private void checkBox_enPM1_CheckedChanged(object sender, EventArgs e)
        {
            int DAta = codePMEnable(checkBox_enPM1.Checked, checkBox_enPM2.Checked, checkBox_enPM3.Checked);
            enviarNum(1);
            enviarNum(DAta);
            enviarNum(41);
        }

        private void checkBox_enPM3_CheckedChanged(object sender, EventArgs e)
        {
            int DAta = codePMEnable(checkBox_enPM1.Checked, checkBox_enPM2.Checked, checkBox_enPM3.Checked);
            enviarNum(1);
            enviarNum(DAta); 
            enviarNum(41);
        }

        private void checkBox_enPM2_CheckedChanged(object sender, EventArgs e)
        {
            int DAta = codePMEnable(checkBox_enPM1.Checked, checkBox_enPM2.Checked, checkBox_enPM3.Checked);
            enviarNum(1);
            enviarNum(DAta);
            enviarNum(41);
        }

        private void pictureBox_mod_Click(object sender, EventArgs e)
        {
    
        }

        private void checkBox_Cs4Re_fCheckedChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown19_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown19.Value);
            enviarNum(37);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label_Cs2_Click(object sender, EventArgs e)
        {
            SelecVisor = 2;
        }

        private void label_Cs3_Click(object sender, EventArgs e)
        {
            SelecVisor = 3;
        }

        private void checkBox_EnableDaniel_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown20_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }
        int NumGuardar = 10;
        private void crearArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!crearArchivoToolStripMenuItem.Checked)
            {
                ModulacionStep = 0;
                cambiaUmbral = 0;
                NumVext = (int)numericUpDown18.Value;
                // tiempoOk.tiempoReset();
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    dataP = saveFileDialog1.FileName;
                    Guardar1.pathdataCs = saveFileDialog1.FileName;
                    Guardar1.GeneraArchivo("_Cs.txt");
                    Guardar1.escribelinea("%time \tCs1 \tCs2 \tCs3 \tCs4 \tVpm1 \tVpm2 \tVpm3 \tVpm4 \tPotLAser \tControlLAser", "_Cs.txt");
                   crearArchivoToolStripMenuItem.Checked = true;
                   NumGuardar = 0;

                }
                else
                {
                    crearArchivoToolStripMenuItem.Checked = false;
                    guardar10MToolStripMenuItem.Enabled = false;
                }

            }
            else
            {
                crearArchivoToolStripMenuItem.Checked = false;
                guardar10MToolStripMenuItem.Enabled = false;
            }
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void numericUpDown21_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown21.Value);
            enviarNum(12);
            GuardaPropiedades.guardar(1, "PeriodoMod", numericUpDown21.Value.ToString());

            numericUpDown_DElayDEte.Value = numericUpDown21.Value /2;
        }
        int Selec_control =0, Selec_Visor=0, Selec_ref = 0;
         
        private void enviar_selct()
        {
            enviarOrden(Selec_control + Selec_Visor + Selec_ref);
            enviarNum(21);
        }

        

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked) Selec_ref = 4; else Selec_ref =0;
            enviar_selct();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked) Selec_ref = 2; else Selec_ref = 0;
            enviar_selct();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked) Selec_control = 1; else Selec_control = 0;
            enviar_selct();
        }

        int EnableMod0 = 0, EnableMod1 = 2, EnableMod2=0;
        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked) EnableMod0 = 1; else EnableMod0 = 0;
            GuardaPropiedades.guardar(14, "EnableMod0", EnableMod0.ToString());
            enviar_EnMod();
        }
        private void enviar_EnMod()
        {
            enviarOrden(EnableMod0 + EnableMod1 + EnableMod2);
            enviarNum(13);
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked) EnableMod1 = 2; else EnableMod1 = 0;
            enviar_EnMod();
            GuardaPropiedades.guardar(15, "EnableMod1", EnableMod1.ToString());
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked) EnableMod2 = 4; else EnableMod2 = 0;
            enviar_EnMod();
            GuardaPropiedades.guardar(16, "EnableMod2", EnableMod2.ToString());
        }

        private void numericUpDown22_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown22.Value);
            enviarNum(21);
            GuardaPropiedades.guardar(3, "TipoCtrl", numericUpDown22.Value.ToString());
        }

        private void numericUpDown23_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown23.Value);
            enviarNum(22);
            GuardaPropiedades.guardar(5, "PAsosctrl", numericUpDown23.Value.ToString());
        }

        private void numericUpDown24_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown24.Value);
            enviarNum(23);
            GuardaPropiedades.guardar(4, "PausaLCD", numericUpDown24.Value.ToString());
        }

     

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked) EnablePM0 = 1; else EnablePM0 = 0;
            enviar_EnPMs();
            GuardaPropiedades.guardar(11, "EnablePM0", EnablePM0.ToString());
        }

        int EnablePM0 =1, EnablePM1 = 0, EnablePM2 = 0;
        private void enviar_EnPMs()
        {
            enviarOrden(EnablePM0 + EnablePM1 + EnablePM2);
            enviarNum(14);
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked) EnablePM1 = 2; else EnablePM1 = 0;
            GuardaPropiedades.guardar(12, "EnablePM1", EnablePM1.ToString());
            enviar_EnPMs();
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked) EnablePM2 = 4; else EnablePM2 = 0;
            GuardaPropiedades.guardar(13, "EnablePM2", EnablePM2.ToString());
            enviar_EnPMs();
        }

        private void numericUpDown26_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown26.Value);
            enviarNum(18);
            GuardaPropiedades.guardar(2, "TimpoSample", numericUpDown26.Value.ToString());
        }

        private void numericUpDown27_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown27.Value);
            enviarNum(11);
            GuardaPropiedades.guardar(8, "WideMz", numericUpDown27.Value.ToString());
        }

        private void numericUpDown28_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown28.Value);
            enviarNum(10);
            GuardaPropiedades.guardar(7, "DElayMz", numericUpDown28.Value.ToString());
            
        }

        private void numericUpDown29_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown29.Value);
            enviarNum(19);
            GuardaPropiedades.guardar(10, "TiempoMod", numericUpDown29.Value.ToString());
        }

        private void numericUpDown30_ValueChanged(object sender, EventArgs e)
        {
            enviarOrden((int)numericUpDown30.Value);
            enviarNum(26);
            GuardaPropiedades.guardar(9, "DElayAPDs", numericUpDown30.Value.ToString());
            //numericUpDown31.Value = numericUpDown21.Value/2 + numericUpDown30.Value;
        }

      

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        int SumaEn = 1;
                private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked) SumaEn = 1; else SumaEn = 0;

        }

         private void checkBox_Cs4Ref_CheckedChanged(object sender, EventArgs e)
         {
          CountChan = 0;
         }
                bool enable_muestrear = false;
                int Nmuestrea = 0;

                private void button_muestrear_Click(object sender, EventArgs e)
                {
                    enable_muestrear = true;
                    Nmuestrea = 0;
                }

                private void numericUpDown32_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown32.Value);
                    enviarNum(42);
                    GuardaPropiedades.guardar(17, "Datalim", numericUpDown32.Value.ToString());
                }

                private void numericUpDown_Cs_ValueChanged(object sender, EventArgs e)
                {

                    GuardaPropiedades.guardar(18, "limGrafCC", numericUpDown_Cs.Value.ToString());
                }

                private void numericUpDown_Estados_ValueChanged(object sender, EventArgs e)
                {
                    GuardaPropiedades.guardar(19, "limGraestadosf", numericUpDown_Estados.Value.ToString());
                }

                private void numericUpDown33_ValueChanged(object sender, EventArgs e)
                {
                     enviarOrden((int)numericUpDown33.Value);
                    enviarNum(43);
                    GuardaPropiedades.guardar(20, "EstadoRx", numericUpDown33.Value.ToString());
                }

                private void groupBox5_Enter(object sender, EventArgs e)
                {

                }

                private void numericUpDown34_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown34.Value);
                    enviarNum(44);
                    GuardaPropiedades.guardar(21, "SelectTipoEstado", numericUpDown34.Value.ToString());
                }

                private void label41_Click(object sender, EventArgs e)
                {

                }

                private void numericUpDown35_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown35.Value);
                    enviarNum(45);
                    GuardaPropiedades.guardar(22, "LAtencia", numericUpDown35.Value.ToString());
                }

                private void numericUpDown37_ValueChanged(object sender, EventArgs e)
                {
                         enviarOrden((int)numericUpDown37.Value);
                    enviarNum(47);
                    GuardaPropiedades.guardar(24, "Tw", numericUpDown37.Value.ToString());
                }

                private void numericUpDown36_ValueChanged(object sender, EventArgs e)
                {
                         enviarOrden((int)numericUpDown36.Value);
                    enviarNum(46);
                    GuardaPropiedades.guardar(23, "delayTw", numericUpDown36.Value.ToString());
                }

                private void button9_Click_1(object sender, EventArgs e)
                {
                    numericUpDown28.Value++;
                 numericUpDown30.Value++;


                }

                private void checkBox_hist_CheckedChanged(object sender, EventArgs e)
                {
                    if (checkBox_hist.Checked)
                    {
                        numericUpDown_Estados.Value = 1; numericUpDown_Cs.Value = 1;
                        numericUpDown_xi.Value = 1;
                    }
                }

                private void button10_Click_1(object sender, EventArgs e)
                {
                    numericUpDown28.Value = 0;
                    numericUpDown30.Value = 17;

                }

                private void checkBox19_CheckedChanged(object sender, EventArgs e)
                {

                    if (checkBox19.Checked) enviarOrden(255); else   enviarOrden(0); 
                   
                    enviarNum(48);
                }

                private void checkBox20_CheckedChanged(object sender, EventArgs e)
                {
                    if (checkBox20.Checked) enviarOrden(1);
                    else enviarOrden(0);
                    enviarNum(49);
                }

                private void numericUpDown38_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown38.Value);
                    enviarNum(50);
                    GuardaPropiedades.guardar(25, "delayPm1", numericUpDown38.Value.ToString());
                }

                private void numericUpDown39_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown39.Value);
                    enviarNum(51);
                    GuardaPropiedades.guardar(26, "delayPm2", numericUpDown39.Value.ToString());
                }

                private void numericUpDown40_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown40.Value);
                    enviarNum(52);
                    GuardaPropiedades.guardar(27, "delayPm3", numericUpDown40.Value.ToString());
                }

                private void numericUpDown41_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown41.Value);
                    enviarNum(57);
                    GuardaPropiedades.guardar(28, "PesoXi", numericUpDown41.Value.ToString());
                }

                private void checkBox22_CheckedChanged(object sender, EventArgs e)
                {
                    if(checkBox22.Checked)
                    enviarOrden(255);
                    else
                        enviarOrden(0);
                    enviarNum(58);
                   
                }

                private void numericUpDownAjustePM1_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDownAjustePM1.Value);
                    enviarNum(60);
                    GuardaPropiedades.guardar(29, "ajustePM1", numericUpDownAjustePM1.Value.ToString());

                }

                private void numericUpDownAjustePM2_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDownAjustePM2.Value);
                    enviarNum(61);
                    GuardaPropiedades.guardar(30, "ajustePM2", numericUpDownAjustePM2.Value.ToString());

                }

                private void numericUpDownAjustePM3_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDownAjustePM3.Value);
                    enviarNum(62);
                    GuardaPropiedades.guardar(31, "ajustePM3", numericUpDownAjustePM3.Value.ToString());

                }

                private void checkBox23_CheckedChanged(object sender, EventArgs e)
                {
                    if (checkBox_enOsc.Checked) enviarOrden(1);
                    else enviarOrden(0);
                    enviarNum(63);
                }

                private void numericUpDown_DElayDEte_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown_DElayDEte.Value);
                    enviarNum(27);
                    GuardaPropiedades.guardar(32, "DElayDEte", numericUpDown_DElayDEte.Value.ToString());
                }

                private void numericUpDown31_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown_delayAPD3.Value);
                    enviarNum(64);
                    GuardaPropiedades.guardar(33, "DElayAPD3", numericUpDown_delayAPD3.Value.ToString());
                }

                private void button11_Click_1(object sender, EventArgs e)
                {
                    numericUpDown38.Value++;
                    numericUpDown39.Value++;
                    numericUpDown40.Value++;
                }

                private void checkBox23_CheckedChanged_1(object sender, EventArgs e)
                {
                    int i = 0;
                    if (checkBox23.Checked) i = 1;
                    if (checkBox24.Checked) i += 2;
                    if (checkBox25.Checked) i += 4;

                    enviarOrden((int)i);
                    enviarNum(65);

                }

                private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)

                {
                   
                }

                private void numericUpDown31_ValueChanged_1(object sender, EventArgs e)
                {
               
                    Conv.int2Byte4((int)numericUpDown31.Value);
                    enviarNum(Conv.Byte4[0]);
                       enviarNum(Conv.Byte4[1]);
                }

                private void numericUpDown_Noise_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown_Noise.Value);
                    enviarNum(25);
                    GuardaPropiedades.guardar(6, "Noise", numericUpDown_Noise.Value.ToString());
                }

                private void numericUpDown_MaxC1_ValueChanged(object sender, EventArgs e)
                {

                }

                private void label19_Click(object sender, EventArgs e)
                {

                }

                private void numericUpDown_muestrear_ValueChanged(object sender, EventArgs e)
                {

                }

                private void numericUpDown25_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown29.Value);
                    enviarNum(23);
                    GuardaPropiedades.guardar(10, "TiempoMod", numericUpDown29.Value.ToString());
                }

                private void pictureBox1_Click(object sender, EventArgs e)
                {

                }
                
                private void numericUpDown44_ValueChanged(object sender, EventArgs e)
                {
                    int Vtrigger = (int)numericUpDown44.Value;
                    for(int i=0;i<600;i++)
                        VoltajeADCs[4, i] = Vtrigger;

                    enviarNum(1);
                    enviarNum(Vtrigger);


                    int num = (int)comboBox1.SelectedIndex;
                    enviarNum(2);
                    enviarNum(num);
                    enviarNum(46);
                }

                private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
                {
                    int num = (int)comboBox1.SelectedIndex;
                    enviarNum(2);
                    enviarNum(num);
                    enviarNum(46);
                }

                private void checkBox26_CheckedChanged(object sender, EventArgs e)
                {

                }

                private void numericUpDown45_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown45.Value);
                    enviarNum(30);
                }

                private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
                {
                    int num = (int)comboBox2.SelectedIndex;
                    enviarNum(1);
                    if(num==0) enviarNum(0); //PM off
                    if (num == 1) enviarNum(5); //PM zeros
                    if (num == 2) enviarNum(10); //PM 1
                    if (num == 3) enviarNum(11); //PM 2
                    if (num == 4) enviarNum(12); //PM 3
                    if (num == 5) enviarNum(15); //control max
                    if (num == 6) enviarNum(16); //control min
                    if (num == 7) enviarNum(17); //control ref
                    enviarNum(39);
                }

                private void numericUpDown46_ValueChanged(object sender, EventArgs e)
                {
                    int Vref = (int)numericUpDown46.Value;
                    for (int i = 0; i < 600; i++)
                        VoltajeADCs[5, i] = Vref/16;

                    enviarOrden((int)numericUpDown46.Value);
                    enviarNum(42);
                }

                private void numericUpDown47_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown47.Value);
                    enviarNum(41);
                }

                private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
                {
                    int num = (int)comboBox3.SelectedIndex;
                    enviarNum(1);
                    enviarNum(num);
                    enviarNum(43);
                }

                private void numericUpDown48_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown48.Value);
                    enviarNum(31);
                }

                private void numericUpDown49_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown49.Value);
                    enviarNum(32);
                }

                private void numericUpDown50_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown50.Value);
                    enviarNum(33);
                }

                private void checkBox27_CheckedChanged(object sender, EventArgs e)
                {
                   
                    
                        enviarNum(1);
                        if (checkBox27.Checked) enviarNum(255);
                        else enviarNum(0);
                        enviarNum(44);
                   
                }

                private void label78_Click(object sender, EventArgs e)
                {

                }

                private void checkBox28_CheckedChanged(object sender, EventArgs e)
                {
                    int num=0;
                    if (checkBox28.Checked) num = num+1;
                    if (checkBox29.Checked) num = num + 2;
                    if (checkBox30.Checked) num = num + 4;
                         enviarNum(1);
                         enviarNum(num);
                        enviarNum(40);

                }

                private void numericUpDown52_ValueChanged(object sender, EventArgs e)
                {
                    enviarOrden((int)numericUpDown52.Value);
                    enviarNum(45);
                }

              GuardarData Guardar01 = new GuardarData();
              int Nguardao = 0;
         
              private void button12_Click_1(object sender, EventArgs e)
                {
                      //readyfraficoOsc = true;
                timer3.Enabled = false;
                button7.Text = "Cuentas On";
                Guardar01.pathdataCs = "";
                string nombreD = textBox1.Text+ "_" + Nguardao.ToString() + ".txt";
                Guardar01.GeneraArchivo(nombreD);
                Guardar10.escribelinea("%ADC1\tADC2\tADC3\tADC4\tref\trigger\tPM1_C\tPM2_C\tPM3_C\tPM1_F\tPM2_F\tPM3_F", nombreD);

                for (int i = 0; i < 500; i++) 
                {
                    Guardar10.escribelinea(
                       VoltajeADCs[0, i].ToString() + "\t" + VoltajeADCs[1, i].ToString() + "\t" + VoltajeADCs[2, i].ToString() + "\t"+
                       VoltajeADCs[3, i].ToString() + "\t" + VoltajeADCs[4, i].ToString() + "\t" + VoltajeADCs[5, i].ToString() + "\t"+
                      VoltajeDAC_Ctrl[0,i] + "\t"+ VoltajeDAC_Ctrl[1,i] + "\t"+ VoltajeDAC_Ctrl[2,i] + "\t"+
                      VoltajeDAC_fase[0,i] + "\t"+ VoltajeDAC_fase[1,i] + "\t"+ VoltajeDAC_fase[2,i] + "\t"
                    , nombreD);
                }
                Nguardao++;

                timer3.Enabled = true;
                button7.Text = "Cuentas Off";
                }

              private void groupBox7_Enter(object sender, EventArgs e)
              {

              }

              private void numericUpDown53_ValueChanged(object sender, EventArgs e)
              {
                  enviarOrden((int)numericUpDown53.Value);
                  enviarNum(47);
              }

              private void numericUpDown54_ValueChanged(object sender, EventArgs e)
              {
                  enviarOrden((int)(numericUpDown54.Value));
                  enviarNum(48);
              }

              private void comboBox_Puertosexistentes_Click(object sender, EventArgs e)
              {

              }

           


             
         

      
               

       
      

         

 
     

      

        
        
    }
}
