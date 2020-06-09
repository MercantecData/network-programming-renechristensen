using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;



namespace udpClient1
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpClient client = new UdpClient();
            bool done = false;
            string text = "";
            int port = 21000;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            Receiver(client);
            Console.WriteLine("Write a message");
            while (!done)
            {
                if (text != "end")
                {
                    text = Console.ReadLine();
                    byte[] bytes = Encoding.UTF8.GetBytes(text);
                    client.SendAsync(bytes, bytes.Length, endPoint);
                }
                else
                {
                    Console.WriteLine("client will close now");
                    done = true;
                }
            }
        }

        public async static void Receiver(UdpClient client)
        {
            byte[] buffer;
            while (true)
            {
                
                UdpReceiveResult result = await client.ReceiveAsync();
                buffer = result.Buffer;
                string text = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(text);
            }
        }
    }
}