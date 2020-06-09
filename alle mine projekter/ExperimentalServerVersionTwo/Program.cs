using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ExperimentalServerVersionTwo
{
    class MainClass
    {
        static String ServerSays = "";
        public static void Main(string[] args)
        {
            int port = 21500;
            IPAddress ip = IPAddress.Any;
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(endPoint);
            listener.Start();

            Console.WriteLine("Waiting for clients");
            getClients(listener);

            while (ServerSays != "quit") {
                ServerSays = Console.ReadLine();
            }
        }

        public async static void getClients(TcpListener listener)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            Console.WriteLine("New client found");
            NetworkStream stream = client.GetStream();
            ReceiveMessage(stream);
        }

        public async static void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            int numberOfBytesRead = await stream.ReadAsync(buffer, 0, 256);
            String receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine(receivedMessage);

           
        }
    }
    
}
