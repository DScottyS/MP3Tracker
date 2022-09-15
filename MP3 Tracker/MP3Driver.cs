using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MP3_Tracker
{
    /// <summary>
    /// 
    /// </summary>
    public class MP3Driver
    {
        /// <summary>
        /// Main method, initializes the program and prompts the user to enter their name and make a choice
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("------------------------------------------------------------" +
                "\n\t  Welcome to the Funky Munky MP3 Tracker\n" +
                "------------------------------------------------------------" +
                "\nHere, you can create and display MP3s, please enter your name:\n");

            //stores the name the user inputs for later
            string userName = Console.ReadLine(); 
            
            Console.WriteLine("\n---------------------------------" +
                "\n1. Create a new MP3 file" +
                "\n2. Display an MP3 file" +
                "\n3. Terminate the program" +
                "\n---------------------------------" +
                "\n\nPlease type the number corresponding to what you would like to do:\n");

            //Takes the number the user input so they can navigate the menu
            int choice = Int32.Parse(Console.ReadLine()); 

            //Creates a new MP3 object so that the information the user inputs can be stored
            MP3 newSong = new MP3(); 

            //depending on what the user input, will either create a new MP3, display an existing MP3 (if possible)
            //or will exit the program
            do
            {
                //creates a new MP3
                if (choice == 1)  
                {
                    //stores the title of the song
                    Console.WriteLine("\nPlease enter the song's title: \n");
                    newSong.title = Console.ReadLine();

                    //stores the artist of the song
                    Console.WriteLine("\nPlease enter the name of the song's Artist:\n");
                    newSong.artist = Console.ReadLine();

                    //stores when the song was released
                    Console.WriteLine("\nPlease enter the song's release date:\n");
                    newSong.releaseDate = Console.ReadLine();

                    //stores the length of the song in seconds
                    Console.WriteLine("\nPlease enter the playback length of the song in seconds:\n");
                    newSong.playbackTime = Double.Parse(Console.ReadLine());

                    //stores the genre of the song
                    Console.WriteLine("\nPlease enter the genre of the song, this program recognizes the following:" +
                    " \nRock, Pop, Jazz, Country, Classical, and Other:\n");
                    string userGenre = Console.ReadLine();
                    newSong.genre = (Genre)Enum.Parse(typeof(Genre), userGenre.ToUpper());

                    //stores the cost of the song
                    Console.WriteLine("\nPlease enter the cost to download the song (do not enter a $):\n");
                    newSong.downloadCost = Decimal.Parse(Console.ReadLine());

                    //stores the file size of the song
                    Console.WriteLine("\nPlease enter the size of the song in MB:\n");
                    newSong.fileSizeMB = Double.Parse(Console.ReadLine());

                    //stores the location in which the song's cover art is downloaded
                    Console.WriteLine("\nPlease enter the image path(i.e. C:/Users/Scotty/Downloads/FunkyMunky.png):\n");
                    newSong.filePath = Console.ReadLine();

                    //prompts the user to either create a new MP3, display an MP3, or quit the program
                    Console.WriteLine("\nPress 1 to make a new MP3, 2 to display an MP3, or 3 to exit the program\n");
                    choice = Int32.Parse(Console.ReadLine());
                }

                //displays an existing MP3 if possible, if not, prompts the user to make one
                else if (choice == 2) 
                {
                    //if an MP3 does not currently exist, prompts the user to create a new one or close the program
                    if(newSong.title == "")
                    {
                        Console.WriteLine("\nThe is currently no existing MP3, please press 1 to create a new one" +
                            " or 3 to exit the program:\n");
                        choice = Int32.Parse(Console.ReadLine());
                    }

                    //if an MP3 currently exists, displays that MP3
                    else
                    {
                        Console.WriteLine(newSong);
                        Console.WriteLine("\nPress 1 to make a new MP3, 2 to display an MP3, or 3 to exit the program:\n");
                        choice = Int32.Parse(Console.ReadLine());
                    }
                }

                //if the user does not input 1, 2, or 3, prompts the user to input a number between 1 and 3
                else if (choice > 3) 
                {
                    Console.WriteLine("\nInvalid input please press 1 to make a new MP3, " +
                        "2 to display an MP3, or 3 to exit the program:\n");
                    choice = Int32.Parse(Console.ReadLine());
                }

            }while(choice != 3);

            //thanks the user and exits the program when they input 3
            if(choice == 3) 
            {
                Console.WriteLine($"\nThank you for using the program {userName}, have a great day!");
            }
        }
    }
}
