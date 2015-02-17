using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace EDITME
{
    class IPScanner
    {

        private static List<Ping> pingers = new List<Ping>();
        private static int instances = 0;
        private static Main main;

        private static Stopwatch watch = Stopwatch.StartNew();

        private static object @lock = new object();

        private static int result = 0;
        private static int responses = 0;
        private static int timeOut = 250;

        private static int ttl = 5;

        public static void start(Main main1)
        {

            main = main1;

            string baseIP = "192.168.1.";

            Console.WriteLine("Pinging 255 destinations of D-class in {0}*", baseIP);

            CreatePingers(255);

            PingOptions po = new PingOptions(ttl, true);
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            byte[] data = enc.GetBytes("abababababababababababababababab");

            int cnt = 1;

            Stopwatch watch = Stopwatch.StartNew();

            foreach (Ping p in pingers)
            {
                lock (@lock)
                {
                    instances += 1;
                }

                p.SendAsync(string.Concat(baseIP, cnt.ToString()), timeOut, data, po);
                cnt += 1;
            }

        }

        public static void Ping_completed(object s, PingCompletedEventArgs e)
        {
            lock (@lock)
            {
                instances -= 1;
            }

            responses++;

            main.setProgress(responses);

            if (e.Reply.Status == IPStatus.Success)
            {
                Console.WriteLine(string.Concat("Active IP: ", e.Reply.Address.ToString()));
                result += 1;
            }
            
            if(responses == 255)
            {

                watch.Stop();

                DestroyPingers();

                Console.WriteLine("Finished in {0}. Found {1} active IP-addresses.", watch.Elapsed.ToString(), result);

            }

        }


        private static void CreatePingers(int cnt)
        {
            for (int i = 1; i <= cnt; i++)
            {
                Ping p = new Ping();
                p.PingCompleted += new PingCompletedEventHandler(Ping_completed);
                pingers.Add(p);
            }
        }

        private static void DestroyPingers()
        {
            foreach (Ping p in pingers)
            {
                p.PingCompleted -= Ping_completed;
                p.Dispose();
            }

            pingers.Clear();

        }

    }
}
