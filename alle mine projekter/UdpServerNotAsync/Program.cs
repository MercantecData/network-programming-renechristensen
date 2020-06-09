using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpServer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 21000);
            
            UdpClient client = new UdpClient(endpoint);

            receiveMessage(client, endpoint);
            Console.WriteLine("Server shutting down");

        }

        public static void receiveMessage(UdpClient client, IPEndPoint endpoint)
        {
            byte[] buffer = new byte[256];
            buffer = client.Receive(ref endpoint);
            String clientName = Encoding.UTF8.GetString(buffer);

            while (true)
            {
                buffer = client.Receive(ref endpoint);
                String text = Encoding.UTF8.GetString(buffer);
                if (text.Length != 0)
                {
                    Console.WriteLine(clientName + text);
                }
                else if (text == "end")
                {
                    break;

                }
            }
        }
    }
}