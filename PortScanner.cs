using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace EDITME
{
    class PortScanner
    {
        public static Boolean portIsOpen(IPAddress ip, int port)
        {

            TcpClient tcpClient = new TcpClient();

            try
            {
                tcpClient.Connect(ip.ToString(), port);
                // Will cast exception if that fails, therefore avoiding this
                return true;
            }
            catch (Exception)
            {
                return false
            }

            return false;

        }
    }
}
