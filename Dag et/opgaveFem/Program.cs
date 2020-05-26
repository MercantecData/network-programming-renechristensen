using System;
using System.Text;

namespace OpgaveFive
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            String text = "Ladies and hats";
            byte[] array = Encoding.UTF8.GetBytes(text);
            String output = "";
            foreach (byte b in array)
            {
                Console.WriteLine(b);
                output += Convert.ToChar(b);
            }

            Console.WriteLine(output);
        }
    }
}
