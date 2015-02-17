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

            UdpClient udpClient = new UdpClient();

            udpClient.Ttl = 5;
            
            try
            {
                udpClient.Connect(ip, port);
                // Will cast exception if that fails, therefore avoiding this
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
