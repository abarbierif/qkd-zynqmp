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
    class Puertos
    {
        public string verIPLocal()
        {

            string dirIP_PC = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            try
            {
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.ToString() == "InterNetwork")
                    {
                        dirIP_PC = ip.ToString();
                    }
                }
            }
            catch { }// MessageBox.Show("no se reconoce IP local"); }

            return dirIP_PC;
        }
    }
}
