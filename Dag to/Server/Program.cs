using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace DagtoServer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //Basic declarations
            int port = 22000;
            int bytesInTextReceivedBuffer = 0;
            byte[] textReceivedBuffer = new byte[1024];
            String textReceived = "";

            // Declares Ipaddress, Endpoint and TcpClient
            IPAddress ip = IPAddress.Any;
            IPEndPoint localIpEndPoint = new IPEndPoint(ip, port);
            


            // Declares tcpListerner and starts listening for incoming connections
            TcpListener listerner = new TcpListener(localIpEndPoint);
            listerner.Start();

            
            Console.WriteLine("Okay so now we just wait for input");

            TcpClient ourClient = listerner.AcceptTcpClient();
            NetworkStream clientStream = ourClient.GetStream();

            while (textReceived != "End")
            { 
                
                bytesInTextReceivedBuffer = clientStream.Read(textReceivedBuffer, 0, 1024);
                textReceived = Encoding.UTF8.GetString(textReceivedBuffer, 0, bytesInTextReceivedBuffer);

                Console.WriteLine(textReceived);
            }
        }
    }
}