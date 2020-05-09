using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace odbiornik_k
{
    class Program
    {
        static void Main(string[] args)
        {

            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener serwer = new TcpListener(ip, 808);
            TcpClient client = default(TcpClient);
            

                try
                {

                    serwer.Start();
                    Console.WriteLine("Serwer aktywny...");
                    Console.Read();

                }
                catch (Exception ex)
                {
                   Console.WriteLine(ex);
                   Console.Read();
                    
                }


            while (true) {

                client = serwer.AcceptTcpClient();
                byte[] otrzymane = new byte[100];
                NetworkStream stream = client.GetStream();

                stream.Read(otrzymane, 0, otrzymane.Length);

                string wiadomosc = Encoding.ASCII.GetString(otrzymane, 0, otrzymane.Length);
                    
                Console.WriteLine(wiadomosc);
                Console.Read();

            }
            }
        }
}
