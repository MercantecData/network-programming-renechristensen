using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpClientVersionZero
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // socket oprettes
            UdpClient client = new UdpClient();
            byte[] translatedFromString;

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 21000);

            // en bruger af chatten kan her sætte eget chatnavn
            Console.WriteLine("Hello User. What is your name?");
            String username = Console.ReadLine();

            // brugernavn oversættes og sendes til server
            translatedFromString = Encoding.UTF8.GetBytes(username + " says: ");
            client.Send(translatedFromString, translatedFromString.Length, endPoint);

            //Sender beskeder til serveren over netværksstrømmen
            while (true)
            {
                String text = Console.ReadLine();
                byte[] translatedFromStringTwo = Encoding.UTF8.GetBytes(text);
                Console.WriteLine(text);
                client.Send(translatedFromStringTwo, translatedFromStringTwo.Length, endPoint);
            }
        }
    }
}
