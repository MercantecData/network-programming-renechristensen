using System;
using System.Text;

namespace OpgaveTwo
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // En simpel string oversættes til byteform ved hjælp af Ascii
            String text = "Ålen æltør den trædje";
            byte[] array = Encoding.ASCII.GetBytes(text);

            // Hver byte i arrayet udskrives
            foreach (byte b in array)
            {
                Console.WriteLine(b);
            }

            // der oversættes igen til tekstform for at gøre resultatet mere tydeligt
            text = Encoding.ASCII.GetString(array);
            Console.WriteLine(text);
        }
    }
}
// Resultatet af at indsætte tegn der ikke er i ASCII tabellen er at der istedet indsættes et "?". Herved kan
// programmet fortsætte med at oversætte til byteform der herefter kan sendes over netværket. 