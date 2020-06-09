using System;
using System.Text;


namespace opgaveOne
{
    class AsciiReader
    {

        public static void Main(string[] args)
        {
            // takes a string and converts it into a array of bytes using the ASCII table for translation. 
            Console.WriteLine("Welcome to the text-to-byte converter. Please stick to common signs " +
                "as it uses the limited ascii table instead of UTF8. ");
            String text = Console.ReadLine();
            Byte[] array = Encoding.ASCII.GetBytes(text);

            // Writes out each letter in byteform 
            foreach (byte b in array)
            {
                Console.WriteLine(b);
            }
        }
    }
}