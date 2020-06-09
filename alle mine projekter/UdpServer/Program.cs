using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpAsyncServerVersionOne
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 21000);

            UdpClient client = new UdpClient(endpoint);

            Task taskOne = Task.WhenAny(receiveMessage(client));
            taskOne.Wait();
            Console.WriteLine("Server shutting down");
            
        }

        public async static Task receiveMessage(UdpClient client)
        {
            byte[] buffer;
            UdpReceiveResult result = await client.ReceiveAsync();
            buffer = result.Buffer;
            String clientName = Encoding.UTF8.GetString(buffer);

            while (true)
            {
                result = await client.ReceiveAsync();
                buffer = result.Buffer;
                String text = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(clientName + text);
                if(text == "end")
                {
                    break;

                }
            }
        }
    }
}
