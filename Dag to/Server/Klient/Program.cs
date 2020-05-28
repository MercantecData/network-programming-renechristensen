using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Klient
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // basale typer declareres
            String textToSend = "";
            int port = 22000;

            // connection relaterede typer declareres
            TcpClient client = new TcpClient();
            IPAddress IP = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(IP, port);
            

            //Herefter laves der en reference til datastrømmen kaldet streaming.
            NetworkStream stream = findConnection(client, endPoint);

            sendingMessages(textToSend, stream);

            client.Close();
            
        }

        // client opretter en forbindelse til vores endpoint, altså det device der har ipaddressen IP. Her forbindes den til den process på modtagercomputeren der
        // har pågældende portnr. I dette tilfælde er det vores serverprogram. 
        public static NetworkStream findConnection(TcpClient client, IPEndPoint endPoint)
        {

            client.Connect(endPoint);
            return client.GetStream();
        }

        public static void sendingMessages( String textToSend, NetworkStream stream)
        {
            int count = 1;
            while (count <= 5)
            {
                Console.WriteLine("What message do you want to send to the server?");
                textToSend = Console.ReadLine();
                byte[] textBuffer = Encoding.UTF8.GetBytes(textToSend);
                stream.Write(textBuffer, 0, textBuffer.Length);
                count++;
            }
        }
    }
}