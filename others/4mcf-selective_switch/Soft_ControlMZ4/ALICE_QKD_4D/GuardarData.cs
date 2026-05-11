using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
namespace ALICE_QKD_4D
{
    class GuardarData
    {
        public string pathdataCs;
        public void GeneraArchivo(string dataNAme)
        {
            StreamWriter swG = new StreamWriter(dataNAme);
            swG.Close(); 
        }
        public void escribelinea(string data,string dataNAme)
        {
            StreamWriter swG = File.AppendText(pathdataCs + dataNAme);
            swG.WriteLine(data);
            swG.Close();
        }
    }
}
