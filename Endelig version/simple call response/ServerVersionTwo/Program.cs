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
            // vi begynder at lede efter indkommende forbindelser
            TcpListener listener = startListening();

            Console.WriteLine("Welcome client");

            // vi sætter den første indkommende forbindelse som TcpClient.
            TcpClient client = listener.AcceptTcpClient();




            // så declarerer vi vores byte array som vi bruger til at lagre vore meddellelse i byteform
            // inden de oversættes til charform (og derfra samles som en string)
            byte[] buffer = new byte[256];

            // fra client får vi fat i datastrømmen og aflæser antallet af indkommende bytes.
            NetworkStream stream = client.GetStream();
            int numberOfBytesRead = stream.Read(buffer, 0, buffer.Length);

            // vi afkoder meddelelsen fra klienten
            String message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

            // endeligt udskriver vi meddelelsen
            Console.WriteLine(message);

            // vi sender en serverbesked til klienten
            sendMessage(client);

            client.Close();
        }

        // oversætter en besked til byteform og sender den over netværket til klienten. Dette er den første serverbesked.
        public static void sendMessage(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] array = Encoding.UTF8.GetBytes("Welcome, to the server of Wonders!! Server shutting down in 3, 2, 1, puff!!");
            stream.Write(array, 0, array.Length);
        }

        public static TcpListener startListening()
        {
            // vi starter med at declarere hvilken port(25000) og hvilke ipaddresser (alle) vi senere skal reagere på
            int port = 25002;
            IPAddress ip = IPAddress.Any;

            // Herefter declarerer vi et IPEndpoint.
            IPEndPoint localEndPoint = new IPEndPoint(ip, port);

            // Vi declarerer en Listener. En listener venter på indkommende forbindelser efter den er blevet startet
            TcpListener listener = new TcpListener(localEndPoint);
            listener.Start();
            return listener;
        }
    }
}

// simple bonusopgave 1 er løst, der sendes en meddellelse til klienten der har fået den nødvendige kode til at håndtere det
// simple bonusopgave 2 er løst, klienten vælger selv hvilke beskeder der skal sendes til serveren