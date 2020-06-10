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
            // Vi declarerer tcpclienten, vores socket der indeholder alt information til at facilitere kommunikation
            TcpClient client = new TcpClient();

            // declarerer variable 
            int port = 25002;
            IPAddress ip = IPAddress.Parse("127.0.0.1");

            // vi declarerer endpointet, altså der hvor den socket der skal modtage data er
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

            // gør klar til at modtage en serverbesked, uden hvilken klienten sidder fast her
            receiveMessage(client);

            // og endelig ender vi forbindelsen til serveren
            client.Close();
        }

        public static void receiveMessage(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int numberOfBytesRead = stream.Read(buffer, 0, buffer.Length);
            String message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine(message);
        }


    }
    
}
