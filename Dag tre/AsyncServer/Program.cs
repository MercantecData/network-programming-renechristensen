using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace AsyncServerVersionOne
{
    class AsyncServer
    {
        public static void Main(string[] args)
        {
            // vi starter med at declarere hvilken port(25000) og hvilke ipaddresser (alle) vi senere skal reagere på
            int port = 26000;
            IPAddress ip = IPAddress.Any;

            // så declarerer vi vores byte array som vi bruger til at lagre vore meddellelse i byteform inden de oversættes til charform (og derfra samles som en string)
            byte[] buffer = new byte[256];

            // vi begynder at lede efter indkommende forbindelser
            TcpListener listener = startListening(ip, port);



            Console.WriteLine("Welcome clients");

            // vi sætter den første indkommende forbindelse som TcpClient.
            TcpClient client = listener.AcceptTcpClient();

            // fra client får vi fat i datastrømmen
            NetworkStream stream = client.GetStream();
            

            // Vi sender en besked til client
            Console.Write("Write your message here");
            String text = Console.ReadLine();
            buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);

            // Vi aflæser og udskriver eventuelle medellelser
            ReceiveMessage(stream);

            // endeligt udskriver vi klientens meddelelsen
            Console.ReadKey();
        }

        public static async void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            // Vi afventer klientens meddellelse og indtil da gå vi videre med resten af koden
            int numberOfBytesRead = await stream.ReadAsync(buffer, 0, 256);
            String message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine("\n" + message);
        }

        public static TcpListener startListening(IPAddress ip, int port)
        {
            // Herefter declarerer vi et IPEndpoint.
            IPEndPoint localEndPoint = new IPEndPoint(ip, port);

            // Vi declarerer en Listener. En listener venter på indkommende forbindelser efter den er blevet startet
            TcpListener listener = new TcpListener(localEndPoint);
            listener.Start();
            return listener;
        }
    }
}
