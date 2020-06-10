using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Vi sætter vores egen socket op. Samtidigt får vi adgang til metoder der gør det muligt at sende og modtage data
            TcpClient client = new TcpClient();

            // declarerer simple typer
            string text = "";
            bool done = false;

            // så sætter vi et endpoint op indholdende addressen på en anden socket som vi gerne vil sende data til. I dette tilfælde
            // vil det være vores clientServer
            int port = 21000;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            // Vi skaber forbindelse til vores endpoint, altså serveren. Herefter får vi adgang til netværksstrømmen
            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();
            

            Console.Write("Write your message or wait for a message from server \n");

            // ReceiveMessage kører asynkront og tager løbende imod data fra vores klient, altså serveren når denne sender data over
            // netværket.
            RecieveMessage(stream, client);

            // Alt der skrives i terminalen sendes løbende over netværket til serveren ved hjælp af netværksstrømmen. Når klienten er
            // færdig kan denne lukke ned for både klientsiden og serversiden ved at skrive "done"
            while (!done)
            {
                if (text != "done")
                {
                    text = Console.ReadLine();
                    byte[] buffer = Encoding.UTF8.GetBytes(text);

                    if (client.Connected)
                    {
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    else
                    {
                        done = true;
                    }
                }
                else
                {
                    done = true;
                }
            }
            Console.WriteLine("client will close down now");
            client.Close();
        }

        // vi aflæser datastrømmen og afventer at der kommer et relevant input. Dette input gemmes some en bytearray.
        // ByteArrayen oversættes herefter til tekstform og udskrives hvis det ikke er en "end" kommando. Såfremt den er længere end 0.
        public static async void RecieveMessage(NetworkStream stream, TcpClient client)
        {
            byte[] buffer = new byte[256];
            bool done = false;
            String receivedMessage = "";
            while (!done)
            {

                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
                if (receivedMessage != "end" && receivedMessage.Length > 0)
                {
                    Console.WriteLine("Server message: " + receivedMessage);
                }
                else
                {
                    Console.WriteLine("Server message: " + receivedMessage);

                    // Det her er en måde at stoppe klienten fra serversiden. Det gøres ved at "lukke" vores socket (ved at sende strengen "end")
                    // og afbryde datastrømmen. Herefter fejler client.connected(); tjekket i main og programmet stopper efterfølgende. 
                    stream.Close();
                    client.Close();
                    done = true;
                }
            }
        }
    }
}

// I forhold til simple response bonus opgaverne kan man selv vælge hvilke beskeder der skal kunne sendes og modtages.
// Der er ikke lavet en samlet version af klient og server som sådan, men både klient og server er sat op til at kunne sende og modtage
// beskeder løbende. 











