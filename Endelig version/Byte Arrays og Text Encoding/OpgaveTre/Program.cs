using System;
using System.Text;

namespace OpgaveThree
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Introducerer programmet og oversætter en string givet af brugeren til byteform
            Console.WriteLine("Welcommen til version to af text-to-byte converteren. Der er her benyttet UTF8 og dermed kan der skrives" +
                " langt flere tegn end i ASCII versionen. Du er velkommen til at skrive en streng og se resultatet på byteform");
            String text = Console.ReadLine();
            byte[] array = Encoding.UTF8.GetBytes(text);

            // Hver byte udskrives
            foreach (byte b in array)
            {
                Console.WriteLine(b);
            }

            // vi oversætter tilbage til tekstform og udskriver den "nye" version af teksten. Såfremt man benytter sig af tegn der findes
            // i utf8 tabellen kan der oversættes tilbage uden indsættelse af spørgsmålstegn. Nogen tegn er dog ikke kendt af visual studio
            text = Encoding.UTF8.GetString(array);
            Console.WriteLine(text);
        }
    }
}
