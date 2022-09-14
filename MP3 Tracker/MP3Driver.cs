using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MP3_Tracker
{
    public class MP3Driver
    {
        public static void Main()
        {
            Console.WriteLine("Welcome to the Funny Monkey MP3 Tracker, please enter your name:");
            string UserName = Console.ReadLine();

            Console.WriteLine("\n1. Create a new MP3 file" +
                "\n2. Display an MP3 file" +
                "\n3. Terminate the program" +
                "\n\nPlease type the number corresponding to what you would like to do:");
            int choice = Int32.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    
                    break;
                case 2:
                    
                    break;
                case 3:
                    Console.WriteLine($"Thank you for using the program {UserName}, have a great day!");
                    break;
            }
        }

        public static void CreateMP3()
        {

        }
    }
}
