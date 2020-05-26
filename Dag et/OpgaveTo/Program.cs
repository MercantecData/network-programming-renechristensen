using System;
using System.Text;

namespace OpgaveTwo
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            String text = "Ålen æltør den trædje";
            byte[] array = Encoding.ASCII.GetBytes(text);
            foreach (byte b in array)
            {
                Console.WriteLine(b);
            }
        }
    }
}
