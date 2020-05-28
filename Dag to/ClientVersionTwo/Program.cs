using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientVersionTwo
{
    class ClientTwo
    {
        public static void Main(string[] args)
        {
            // Vi declarerer tcpclienten der indeholder alt funktionalitet til at facilitere kommunikation med en anden maskine
            TcpClient client = new TcpClient();

            // declarerer variable der er nødvendige til at lave endpoint
            int port = 25000;
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            // vi declarerer et endpoint, altså det sted på internettet hvor dataene skal sendes hen, med andre ord addressen på serveren
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            // vi opretter en forbindelse mellem klient og server. Dette fejler hvis port eller ip ikke er korrekt, eller serveren ikke kører
            // da der i såfald ikke er noget at forbinde til
            client.Connect(endPoint);

            // Så laver vi en reference til netværksstrømmen mellem klient og server.
            NetworkStream stream = client.GetStream();

            // så sender vi en besked
            Console.WriteLine("hvad vil du gerne skrive til serveren?");
            String text = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);

            // og endelig ender vi forbindelsen til serveren
            client.Close();
        }
    }
}
