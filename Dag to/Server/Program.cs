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
            int port = 22000;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localIpEndPoint = new IPEndPoint(ip, port);

            TcpListener listerner = new TcpListener(localIpEndPoint);
            listerner.Start();

            Console.WriteLine("Okay so now we just wait for input");
            while (true)
            {

                TcpClient ourClient = listerner.AcceptTcpClient();
                NetworkStream clientStream = ourClient.GetStream();

                byte[] textReceivedBuffer = new byte[1024];
                int bytesInTextReceivedBuffer = clientStream.Read(textReceivedBuffer, 0, 1024);
                String textReceived = Encoding.UTF8.GetString(textReceivedBuffer, 0, bytesInTextReceivedBuffer);

                Console.WriteLine(textReceived);
            }
        }
    }
}