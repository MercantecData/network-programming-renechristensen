using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;


//This class is partially based on the given code for multiclient, not what we developed for singleclient server
namespace MulticlientServerVersionOne
{
    class MainClass
    {

        public static List<TcpClient> clientList = new List<TcpClient>();

        public static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Any;
            int port = 21000;
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();

            getClients(listener);
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
                TcpClient client = await listener.AcceptTcpClientAsync();
                clientList.Add(client);
                NetworkStream stream = client.GetStream();
                numberOfClients++;
                receiveMessage(stream, numberOfClients);
            }
        }

        public async static void receiveMessage(NetworkStream stream, int clientNumber){
            byte[] buffer = new byte[256];
            while (true)
            {
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                String messageReceived = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
                Console.WriteLine("\n" + "Client "+ clientNumber + " says: " + messageReceived);
            }
        }
    }
}
