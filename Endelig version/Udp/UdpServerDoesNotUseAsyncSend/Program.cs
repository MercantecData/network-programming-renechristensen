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
            // Vi definerer vores client og hvilken socket den skal lytte på. I dette tilfælde blot vores egen computer
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 21000);
            UdpClient client = new UdpClient(endpoint);

            // metodekald
            receiveMessage(client, endpoint);
            Console.WriteLine("Server shutting down");

        }

        //synkron version af receiver
        public static void receiveMessage(UdpClient client, IPEndPoint endpoint)
        {
            //modtage data fra netop dette endpoint. I dette tilfælde er det det samme som serveren, men det kan være et hvilket
            // som helst endpoint vi forventer at modtage data fra. 
            byte[] buffer = new byte[256];
            buffer = client.Receive(ref endpoint);
            String clientName = Encoding.UTF8.GetString(buffer);

            // modtager beskeder indtil den modtager beskeden "end"
            while (true)
            {
                buffer = client.Receive(ref endpoint);
                String text = Encoding.UTF8.GetString(buffer);
                if (text != "end")
                {
                    Console.WriteLine(clientName + text);
                }
                else
                {
                    Console.WriteLine(clientName + text);
                    break;
                }
            }
        }
    }
}

//Bonusopgave 2 er løst