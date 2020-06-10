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
            int port = 21000;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            UdpClient client = new UdpClient(endPoint);

            receiveMessage(client, endPoint);

            while (true)
            {
                String serverMessage = Console.ReadLine();
                sendMessageToAllClients(serverMessage, client);
            }
        }

        public async static void receiveMessage(UdpClient client, IPEndPoint clientEndPoint)
        {
            byte[] buffer;

            while (true)
            {
                UdpReceiveResult result = await client.ReceiveAsync();
                buffer = result.Buffer;
                String text = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(text);
                if (!endPointList.Contains(result.RemoteEndPoint))
                {
                    endPointList.Add(result.RemoteEndPoint);
                }
                sendMessageToAllClients(text, result.RemoteEndPoint, clientEndPoint, client);

            }
        }

        public static void sendMessageToAllClients(String message, IPEndPoint foreignEndPoint, IPEndPoint clientEndPoint, UdpClient client)
        {
            foreach (IPEndPoint endPoint in endPointList)
            {

                if (!(endPoint.Address.Equals(foreignEndPoint.Address)))
                {
                    byte[] translatedFromString = Encoding.UTF8.GetBytes(message);
                    client.Send(translatedFromString, translatedFromString.Length, endPoint);
                }
            }
        }

        public static void sendMessageToAllClients(String message, UdpClient client)
        {
            foreach (IPEndPoint endPoint in endPointList)
            {
                byte[] translatedFromString = Encoding.UTF8.GetBytes(message);
                client.Send(translatedFromString, translatedFromString.Length, endPoint);
            }
        }
    }
}