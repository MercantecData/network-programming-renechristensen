using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AsyncClientVersionOne
{
    class AsyncClient
    {
        public static void Main(string[] args)
        {
            // Vi declarerer tcpclienten der indeholder alt funktionalitet til at facilitere kommunikation med en anden maskine
            TcpClient client = new TcpClient();

            // declarerer variable der er nødvendige til at lave endpoint
            int port = 13356;
            IPAddress ip = IPAddress.Parse("172.16.114.12");

            // vi declarerer et endpoint, altså det sted på internettet hvor dataene skal sendes hen, med andre ord addressen på serveren
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            // vi opretter en forbindelse mellem klient og server. Dette fejler hvis port eller ip ikke er korrekt, eller serveren ikke kører
            // da der i såfald ikke er noget at forbinde til
            client.Connect(endPoint);

            // Så laver vi en reference til netværksstrømmen mellem klient og server.
            NetworkStream stream = client.GetStream();

            // så sender vi en besked til serveren
            Console.WriteLine("hvad vil du gerne skrive til serveren?");
            String text = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);

            // vi afventer en besked fra serveren
            receiveMessage(stream);
            // og endelig ender vi forbindelsen til serveren
            Console.ReadKey();
            client.Close();
        }

        public static async void receiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            int numberOfBytesRead = await stream.ReadAsync(buffer, 0, 256);
            String receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine(receivedMessage);
        }
    }
}
