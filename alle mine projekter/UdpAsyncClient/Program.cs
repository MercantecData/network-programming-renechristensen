using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UdpAsyncClientVersionOne
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            UdpClient client = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 21000);

            Task taskOne = sendMessage(client, endPoint);
            taskOne.Wait();
            Console.WriteLine("client shutting down");
        }

        public static async Task sendMessage(UdpClient client, IPEndPoint endPoint)
        {
            
            // en bruger af chatten kan her sætte eget chatnavn
            Console.WriteLine("Hello User. What is your name?");
            String username = Console.ReadLine();

            // brugernavn oversættes og sendes til server
            byte[] translatedFromString = Encoding.UTF8.GetBytes(username + " says: ");
            await client.SendAsync(translatedFromString, translatedFromString.Length, endPoint);

            while (true)
            {
                String text = Console.ReadLine();
                translatedFromString = Encoding.UTF8.GetBytes(text);
                Console.WriteLine(text);
                await client.SendAsync(translatedFromString, translatedFromString.Length, endPoint);
            }
        }
    }
}
