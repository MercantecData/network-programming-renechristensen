using System;
using System.Text;

namespace OpgaveThree
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            String text = "well well æ æ æ æ ";
            byte[] array = Encoding.UTF8.GetBytes(text);
            foreach (byte b in array)
            {
                Console.WriteLine(b);
            }
        }
    }
}
