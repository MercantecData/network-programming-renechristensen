using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

//Dette  er en simplificeret version af udpClient, der sender username og text sammen. Dette er den eneste forskel.
namespace UdpClientVersionTwo
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            UdpClient client = new UdpClient();
            byte[] translatedFromString;

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 21000);

            // en bruger af chatten kan her sætte eget chatnavn
            Console.WriteLine("Hello User. What is your name?");
            String username = Console.ReadLine();

            //Modtager alle beskeder fra serveren. Idet alle beskeder videresendes fra serveren ud til alle klienter får vi herigennem
            // adgang til hele chatten
            receiveServerMessage(client);

            // sender meddelelser til serveren. For hver string der skrives tilføjes brugerens navn og teksten oversættes til byteform.
            while (true)
            {
                String text = username + " says: " + Console.ReadLine();
                translatedFromString = Encoding.UTF8.GetBytes(text);
                client.Send(translatedFromString, translatedFromString.Length, endPoint);
            }
        }

        public async static void receiveServerMessage(UdpClient client)
        {
            while (true)
            {
                UdpReceiveResult result = await client.ReceiveAsync();
                Console.WriteLine("Message received :)");
                byte[] buffer = result.Buffer;
                String text = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(text);
            }
        }
    }
}
