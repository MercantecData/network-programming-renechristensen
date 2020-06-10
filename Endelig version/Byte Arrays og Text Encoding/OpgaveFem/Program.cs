using System;
using System.Text;

namespace OpgaveFive
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // vi starter med at oversætte en streng til byteform. Strengen gives af brugeren
            String text = Console.ReadLine();
            byte[] array = Encoding.UTF8.GetBytes(text);
            String output = "";

            // hver byte i bytearrayet oversættes til charform og tilføjes til en tom streng. 
            foreach (byte b in array)
            {
                Console.WriteLine(b);
                output += Convert.ToChar(b);
            }

            Console.WriteLine(output);
        }
    }
}

// Alternativt kunne man med lethed have brugt output = Encoding.UTF8.GetString(array); men jeg har valgt at bibeholde
// min oprindelige fremgangsmåde.