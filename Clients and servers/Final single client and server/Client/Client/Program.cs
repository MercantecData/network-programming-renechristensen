using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace awaitClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();

            int port = 21000;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            string text = "";
            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();
            bool done = false;

            Console.Write("Write your message or wait for a message from server \n");


            RecieveMessage(stream, client);
            while (!done)
            {
                if (text != "done")
                {
                    text = Console.ReadLine();
                    byte[] buffer = Encoding.UTF8.GetBytes(text);

                    if (client.Connected)
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        done = true;
                    }
                }
                else
                {
                    done = true;
                }
            }
            Console.WriteLine("client will close down now");
            client.Close();
        }

        public static async void RecieveMessage(NetworkStream stream, TcpClient client)
        {
            byte[] buffer = new byte[256];
            bool done = false;
            String receivedMessage = "";
            while (!done)
            {

                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
                if (receivedMessage != "end" && receivedMessage.Length > 0)
                {
                    Console.WriteLine("Server message: " + receivedMessage);
                }
                else
                {
                    Console.WriteLine("Server message: " + receivedMessage);
                    stream.Close();
                    client.Close();
                    done = true;
                    Console.WriteLine("Hit enter to close the client");
                }
            }
        }
    }
}













