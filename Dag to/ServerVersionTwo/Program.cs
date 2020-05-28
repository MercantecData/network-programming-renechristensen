using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ServerVersionTwo
{
    class ServerTwo
    {
        public static void Main(string[] args)
        {
            // vi starter med at declarere hvilken port(25000) og hvilke ipaddresser (alle) vi senere skal reagere på
            int port = 25000;
            IPAddress ip = IPAddress.Any;

            // vi begynder at lede efter indkommende forbindelser
            TcpListener listener = startListening(ip, port);


            Console.WriteLine("Welcome clients");
            // vi sætter den første indkommende forbindelse som TcpClient, dvs. det der peges tilbage på.
            TcpClient client = listener.AcceptTcpClient();

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int numberOfBytesRead = stream.Read(buffer, 0, 256);

            // vi afkoder meddelelsen fra klienten
            String message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

            Console.WriteLine(message);
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
