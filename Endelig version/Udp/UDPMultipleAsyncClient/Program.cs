using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace UdpAsyncMultiClientServerVersionOne
{
    class MainClass
    {
        // En væsentlig forskel mellem Tcp og Udp ligger i at man ikke behøver en tovejsforbindelse mellem dem. Det er derfor
        // ikke nødvendigt med flere forskllige UDPclienter. Istedet gemmes Endpoint 
        public static List<IPEndPoint> endPointList = new List<IPEndPoint>();
         
        public static void Main(string[] args)
        {
            // sætter en client op med tilhørende endpoint 
            int port = 21000;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            UdpClient client = new UdpClient(endPoint);

            // metodekald
            receiveMessage(client, endPoint);

            // send beskeder ud til alle klienter, afslut server med kommandoen "end"
            while (true)
            {
                String serverMessage = Console.ReadLine();
                sendMessageToAllClients(serverMessage, client);
                if (serverMessage == "end")
                {
                    break;
                }
            }
        }

        
        public async static void receiveMessage(UdpClient client, IPEndPoint clientEndPoint)
        {
            byte[] buffer;

            while (true)
            {
                // afvent beskeder fra klienter
                UdpReceiveResult result = await client.ReceiveAsync();

                // gem bytes og oversæt til tekst
                buffer = result.Buffer;
                String text = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(text);

                // er det indkommende endpoint på endpointlisten? 
                if(!endPointList.Contains(result.RemoteEndPoint))
                {
                    endPointList.Add(result.RemoteEndPoint);
                }
                // send beskeder til alle klienter
                sendMessageToAllClients(text, client);
            }
        }

        public static void sendMessageToAllClients(String message, UdpClient client)
        {
            // oversæt message til byteform og send til hver client
            foreach (IPEndPoint endPoint in endPointList)
            {
                byte[] translatedFromString = Encoding.UTF8.GetBytes(message);
                client.Send(translatedFromString, translatedFromString.Length, endPoint);
            }
        }
    }
}