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
            // declaration
            UdpClient client = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 21000);

            // metodekald
            receiver(client);
            Task taskOne = sendMessage(client, endPoint);

            // vent på resultat
            taskOne.Wait();
            Console.WriteLine("client shutting down");
        }


        public static async void receiver(UdpClient client)
        {
            while (true)
            {
                // afvent meddelelser
                UdpReceiveResult result = await client.ReceiveAsync();

                // aflæs bytes, oversæt og udskriv beskeden
                Console.WriteLine("Received " + Encoding.UTF8.GetString(result.Buffer));
            }
        }

        public static async Task sendMessage(UdpClient client, IPEndPoint endPoint)
        {

            // en bruger af chatten kan her sætte eget chatnavn
            Console.WriteLine("Hello User. What is your name?");
            String username = Console.ReadLine();

            // brugernavn oversættes og sendes til server
            byte[] translatedFromString = Encoding.UTF8.GetBytes(username + " says: ");
            await client.SendAsync(translatedFromString, translatedFromString.Length, endPoint);

            // fortsæt med at sende beskeder til brugeren skriver "end"
            bool done = true;
            while (done)
            {
                String text =  Console.ReadLine();
                translatedFromString = Encoding.UTF8.GetBytes(text);
                Console.WriteLine(username + " says: " + text);
                await client.SendAsync(translatedFromString, translatedFromString.Length, endPoint);
                if (text == "end")
                {
                    done = true;
                }
            }
        }
    }
}

// Bonusopgave 1 løst. 