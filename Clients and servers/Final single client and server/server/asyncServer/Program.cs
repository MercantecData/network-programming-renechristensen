using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 13358;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(localEndpoint);
            string text = "";

            bool done = false;
            listener.Start();

            Console.WriteLine("Await Clients");

            Task<TcpClient> asyncClient = acceptClient(listener);
            asyncClient.Wait();
            TcpClient client = asyncClient.Result;

            NetworkStream stream = client.GetStream();

            Console.Write("Write your message or wait for a message from client \n");

            //ReceiveMessage(stream);
            while (!done)
            {
                if (text != "done")
                {
                    text = Console.ReadLine();
                    byte[] buffer = Encoding.UTF8.GetBytes(text);

                    stream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    done = true;
                }
            }
            Console.WriteLine("Hit enter to close the server");
        }

        public static async Task<TcpClient> acceptClient(TcpListener listener)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            NetworkStream stream = client.GetStream();
            ReceiveMessage(stream);
            return client;
        }

        public static async void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            bool done = false;
            while (!done)
            {
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, 256);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
                if (receivedMessage.Length > 0)
                {
                    Console.WriteLine("client message: " + receivedMessage);
                }
                else
                {
                    done = true;
                }
            }
        }
    }
}