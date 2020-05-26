using System;
using System.Text;


namespace opgaveOne
{
    class AsciiReader
    {
        public static void Main(string[] args)
        {
            String text = "Well ain't that swell";
            Byte[] array = Encoding.ASCII.GetBytes(text);
            foreach (byte b in array)
            {
                Console.WriteLine(b);
            }
        }
    }
}