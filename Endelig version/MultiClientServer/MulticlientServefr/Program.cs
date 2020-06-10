using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace MulticlientServerVersionOne
{
    class MainClass
    {
        // Vi opretter en liste over klienter, den er nyttig idet vi herved kommer til at kunne sende beskeder til alle på listen.
        public static List<TcpClient> clientList = new List<TcpClient>();


        public static void Main(string[] args)
        {
            //vi sætte en tcpListener op til at registrere indkommende forbindelser
            IPAddress ip = IPAddress.Any;
            int port = 21001;
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();

            // herefter kalder vi getClients til at håndtere dem
            getClients(listener);

            // Vi sætter en whileløkke op der afventer at der skal sendes en meddellelse ud. Når brugeren af serveren sender en sådan
            // meddellelse ud, går vi gennem listen af klienter og sørger for at den sendes til hver enkelt. Herved får alle adgang
            // til servermeddellelser. 
            while (true)
            {
                Console.Write("Everything written in the server is sent to all clients: ");
                String messageToBeSent = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(messageToBeSent);

                foreach(TcpClient client in clientList)
                {
                    NetworkStream newStream = client.GetStream();
                    newStream.Write(buffer, 0, buffer.Length);
                }
            }        
        }

       
        public async static void getClients(TcpListener listener)
        {
            int numberOfClients = 0;
            while (true)
            {
                // afventer nye klienter og tilføjer dem til klientlisten efterhånden. Herudover faciliteres kommunikationen
                // og hver klient gives et unikt nummer til at identificere dem i chatten. 
                TcpClient client = await listener.AcceptTcpClientAsync();
                clientList.Add(client);
                NetworkStream stream = client.GetStream();
                numberOfClients++;
                receiveMessage(stream, numberOfClients, client);
            }
        }

        // en asynkron metode der håndterer strømmen af indkommende meddellelser.
        public async static void receiveMessage(NetworkStream stream, int clientNumber, TcpClient client){
            byte[] buffer = new byte[256];
            bool continuerun = true;
            while (continuerun)
            {
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                String messageReceived = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);


                // udskrive den modtagne besked sammen med relevant data om klienten og send dette til alle klienter
                String messageToBeSent = "\n" + "Client " + clientNumber + " says: " + messageReceived;
                Console.WriteLine(messageToBeSent);
                SendToAll(messageToBeSent);

                // Hvis klienten lukker ned sender den en strøm af data. Det kan aflæses som et endeløst loop af tomme strenge.
                // Endvidere kan man ikke fra klientsiden sende en String af længde nul. Derfor tester vi hvorvidt en indkommende
                // streng er af længde nul. Er den det fjernes den fra listen over clienter - så vi undgår at sende til denne
                // klient i fremtiden og herefter lukkes klienten ned. Endeligt afsluttes loopet.

                if((Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead)).Length == 0)
                {
                    clientList.Remove(client);
                    client.Client.Close();
                    Console.WriteLine("Client " + clientNumber +"  shut down on their end and was removed from list of clients");
                    SendToAll("Client " + clientNumber + " is thrown from the chat");
                    continuerun = false;
                }


            }
        }

        // Sender meddellelser ud til alle på listen over klienter, inklusivt den der har sendt beskeden
        public static void SendToAll(String message)
        {
            foreach(TcpClient client in clientList){
                NetworkStream stream = client.GetStream();
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}

// Bonusopgave 1 er løst, idet SendToAll(StringMessage) sørger for at videresende alle indkommende meddellelser til alle andre på klientlisten

// Bonusopgave 3 er løst. Vi sikrer os mod et eventuelt servercrash ved at fjerne den pågældende klient fra klientlisten så den ikke kaldes
// senere og lukker den ned så vi ikke modtager yderligere data, dette er detaljeret beskrevet i kommentarerne. 