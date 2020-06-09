using System;
using System.Threading.Tasks;
using System.Text;
using System.Net.Sockets;
using System.Net;


namespace ExperimentalServer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            serverInitializer();
            String wait = "";
            while (wait != "quit") {
                wait = Console.ReadLine();
            }
        }

        public async static void serverInitializer(){
            IPAddress ip = IPAddress.Any;
            int port = 21001;
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(endPoint);
            listener.Start();

            Console.WriteLine("Waiting for client...");
            EstablishConnectionsWithClients(listener);
            
        }

        public async static void EstablishConnectionsWithClients(TcpListener listener)
        {
            int numberOfClients = 0;
            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                numberOfClients++;
                Console.Write("Client " + numberOfClients +" has come online");
                NetworkStream stream = client.GetStream();
                communicateWithClients(stream);    
            }
        }

        public static void communicateWithClients(NetworkStream stream)
        {
            receivedMessage(stream);
            Console.Write("\n Server ready: ");
    
        }

        public async static void receivedMessage(NetworkStream stream)
        {

            byte[] receivedBuffer = new byte[256];
            while (true)
            {
                Task<int> packedNumberOfBytesRead = stream.ReadAsync(receivedBuffer, 0, 256);
                int numberOfBytesRead = await packedNumberOfBytesRead;
                Console.Write("Client Says:");
                String receivedMessage = Encoding.UTF8.GetString(receivedBuffer, 0, numberOfBytesRead);
                Console.WriteLine(receivedMessage);
            }
        }
    }
}
