using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declarationer, oprettelse af endpoint og listener
            string text = "";
            bool done = false;

            int port = 21000;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(localEndpoint);

            // vi sætter vores TcpListener til at vente på indkommenende forbindelser fra klientsiden
            listener.Start();

            Console.WriteLine("Await Clients");

            // Vi opretter en task og beder tråden forholde sig afventende indtil programmet er udført og der er oprettet en tcpClient
            // til kommunikation med klienten 
            Task<TcpClient> asyncClient = acceptClient(listener);
            asyncClient.Wait();
            TcpClient client = asyncClient.Result;


            NetworkStream stream = client.GetStream();

            Console.Write("Write your message or wait for a message from client \n");

            //ReceiveMessage(stream);

            // Oversætter servermeddellelser til byteform og sender dem over netværksstrømmen til klienten. Meddellelsen "end" afslutter
            // både klient(der er kode sat op til dette på klientsiden) og server
            while (!done)
            {
                if (text != "end")
                {
                    text = Console.ReadLine();
                    byte[] buffer = Encoding.UTF8.GetBytes(text);

                    stream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    done = true;
                }
            }
            Console.WriteLine("Hit enter to close the server");
            Console.ReadLine();
        }

        // Metoden returnerer en task der afventer kommunikationer fra klienter. Herudfra oprettes der en TcpClient.
        // Ved at returnere en task kan vi bede den om at returnere resultatet til en funktion der ikke er asynkron (main)
        // hvor vi dog skal være opmærksomme på ikke at tilgå resultatet får opgaven er udført. Man kan derfor med fordel
        // kalde wait() metoden der stopper tråden til tasken er færdig og har returneret et resultat.
        // Vi kalder også ReceiveMessage der modtager og udskriver beskeder fra klienten. Dette kan også gøres fra main
        public static async Task<TcpClient> acceptClient(TcpListener listener)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            NetworkStream stream = client.GetStream();
            ReceiveMessage(stream);
            // tilføjet fordi det var en bonusopgave at få serveren til at sende en besked efter klienten forbandt.
            sendFirstServerMessage(stream);
            return client;
        }

        // Oversætter en simpel string til byteform og sender den til klienten
        public static void sendFirstServerMessage(NetworkStream stream)
        {
            byte[] buffer = Encoding.UTF8.GetBytes("Welcome Klient, to the server of Wonders!!");
            stream.Write(buffer, 0, buffer.Length);
        }

        // Denne metode er beskrevet i client og er endvidere givet. 
        public static async void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            bool done = false;
            while (!done)
            {
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, 256);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
                if (receivedMessage.Length > 0)
                {
                    Console.WriteLine("client message: " + receivedMessage);
                }
                else
                {
                    done = true;
                }
            }
        }
    }
}

// bonusopgave fra simpel call response løst. Vi sender en start besked til klienten.
