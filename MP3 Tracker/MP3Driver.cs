           /////////////////////////////////////////////////////////////////////////////////////////////////////////
          //                                                                                                     //
         //                                                                                                     //
        // Project: MP3 Tracker                                                                                //
       // File Name: MP3Driver                                                                                //
      // Description:                                                                                        //
     // Course: CSCI 1260 – Introduction to Computer Science II                                             //
    // Author: Scotty Snyder, snyderds@etsu.edu, Department of Computing, East Tennessee State University  //
   // Created: Sunday, September 7, 2022                                                                  //
  // Copyright: Scotty Snyder, 2022                                                                      //
 //                                                                                                     //
/////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MP3_Tracker
{
    /// <summary>
    /// MP3 Driver class communicates with the user and the MP3 class
    /// </summary>
    public class MP3Driver
    {
        #region Main Method
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
            userName = Console.ReadLine();

            //uses the menu method to display all options available to the user
            Console.Write(Menu());

            //Takes the number the user input so they can navigate the menu
            choice = Int32.Parse(Console.ReadLine());

            //depending on what the user inputs, will either create a new MP3, display an existing MP3 (if possible)
            //or exit the program
            do
            {
                //creates a new MP3
                if (choice == 1)
                {
                    CreateANewSong();
                }

                //displays an existing MP3 if possible, if not, prompts the user to make one
                else if (choice == 2)
                {
                    DisplayASong();
                }

                else if (choice == 3)
                {
                    CreateAPlaylist();
                }

                else if (choice == 5)
                {
                    ShowPlaylist();
                }

                //if the user does not input a number between 1 and 13, prompts the user to input a number between 1 and 13
                else if (choice > 13)
                {
                    do
                    {
                        Console.WriteLine("\nInvalid input please press 1 to make a new MP3, " +
                                                "2 to display an MP3, or 13 to exit the program:\n");
                        choice = Int32.Parse(Console.ReadLine());

                    } while (choice > 13 || choice <= 0);
                }

            } while (choice != 13);

            //thanks the user and exits the program when they input 13
            if (choice == 13)
            {
                Console.WriteLine($"\nThank you for using the Funky Munky MP3 Tracker {userName}! Have a great day!");
            }
        }
        #endregion

        #region Attributes
        //creates a string for the user's name to be stored in
        static string userName;
        //creates a variable that will hold the number corresponding to the choice the user inputs
        static int choice;
        //creates a new MP3 variable called newSong for the user to input information relating to their song into
        static MP3 newSong = new MP3();
        //creates a new playlist object so CreateANewSong method can tell if a playlist has been made
        static Playlist userPlaylist = new Playlist();
        #endregion

        #region Methods
        /// <summary>
        /// menu method simply displays all options the user can choose from
        /// </summary>
        /// <returns>returns a string that contains all menu choices</returns>
        public static string Menu()
        {
            string info = "";

            info += "\n-----------------------------------------";
            info += "\n1. Create a new MP3 file";
            info += "\n2. Display an MP3 file";
            info += "\n3. Create a new Playlist";
            info += "\n4. WIP";
            info += "\n5. Display your playlist";
            info += "\n6. WIP";
            info += "\n7. WIP";
            info += "\n8. WIP";
            info += "\n9. WIP";
            info += "\n10. WIP";
            info += "\n11. WIP";
            info += "\n12. WIP";
            info += "\n13. Terminate the program";
            info += "\n-----------------------------------------";
            info += "\n\nPlease type the number corresponding to what you would like to do: ";

            return info;
        }

        /// <summary>
        ///prompt the user to create a new song
        /// </summary>
        public static void CreateANewSong()
        {
            newSong = new MP3();

            //stores the title of the song
            Console.Write("\nPlease enter the song's title: ");
            newSong.title = Console.ReadLine();

            //stores the artist of the song
            Console.Write("\nPlease enter the name of the song's Artist: ");
            newSong.artist = Console.ReadLine();

            //stores when the song was released
            Console.Write("\nPlease enter the song's release date in MM/DD/YY format: ");
            newSong.releaseDate = Console.ReadLine();

            //stores the length of the song in seconds and converts it into minutes
            do
            {
                try
                {
                    Console.Write("\nPlease enter the playback length of the song in seconds: ");
                    newSong.playbackTime = (Double.Parse(Console.ReadLine()));
                }
                catch (FormatException e)
                {
                    Console.WriteLine("\ninvalid playback time");
                }
            } while (newSong.playbackTime <= 0);

            //stores the genre of the song
            do
            {
                try
                {
                    Console.Write("\nPlease enter the genre of the song, this program recognizes the following:" +
                    " \nRock, Pop, Jazz, Country, Classical, and Other: ");
                    string userGenre = Console.ReadLine();
                    newSong.genre = (Genre)Enum.Parse(typeof(Genre), userGenre.ToUpper());
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("\nInvalid genre");
                }
            } while (newSong.genre == new Genre());

            //stores the cost of the song
            Console.Write("\nPlease enter the cost to download the song (do not enter a $): ");
            newSong.downloadCost = Decimal.Parse(Console.ReadLine());

            //stores the file size of the song
            Console.Write("\nPlease enter the size of the song in MB: ");
            newSong.fileSizeMB = Double.Parse(Console.ReadLine());

            //stores the location in which the song's cover art is downloaded
            Console.WriteLine("\nPlease enter the image path(i.e. C:/Users/Scotty/Downloads/FunkyMunky.png):\n");
            newSong.filePath = Console.ReadLine();

            if (userPlaylist.playlistCreator != "")
            {
                Console.Write("\nwould you like to add this song to your playlist? Y/N: ");
                char addToPlaylist = Console.ReadLine().ToUpper()[0];

                //do
                //{
                    if (addToPlaylist == 'Y')
                    {
                        userPlaylist.AddToPlaylist(newSong);
                        Console.WriteLine($"\n{newSong.title} has been added to {userPlaylist.playlistName}");
                    }
                    else if (addToPlaylist == 'N')
                    {
                        Console.WriteLine($"\n{newSong.title} will not be added to {userPlaylist.playlistName}");
                    }
                    else
                    {
                        do
                        {
                            Console.Write("invalid response, would you like to add this song to your playlist? Y/N: ");
                            addToPlaylist = Console.ReadLine().ToUpper()[0];
                        } while (addToPlaylist != 'Y' && addToPlaylist != 'N');
                    }
                //} while (addToPlaylist != 'Y' && addToPlaylist != 'N');
            }

            //displays the menu for the user after creating their MP3
            Console.Write($"\nMP3 created successfully \n {Menu()}");
            choice = Int32.Parse(Console.ReadLine());
        }

        /// <summary>
        /// displays a song if one exists, if not, prompts the user to either create a new song or exit the program
        /// </summary>
        public static void DisplayASong()
        {
            do
            {
                //if an MP3 does not currently exist, prompts the user to create a new one or close the program
                if (newSong.title == "")
                {
                    Console.WriteLine($"\nThere is currently no existing MP3\n" +
                                      $"{Menu()}");
                    choice = Int32.Parse(Console.ReadLine());
                }
                //if an MP3 currently exists, displays that MP3
                else
                {
                    Console.WriteLine(newSong);
                    Console.WriteLine(Menu());
                    choice = Int32.Parse(Console.ReadLine());
                }
            } while (choice > 13 || choice <= 0);
        }

        /// <summary>
        /// prompts the user to give the information needed to create a new playlist
        /// </summary>
        public static void CreateAPlaylist()
        {
            Console.Write("\nPlease enter the name of your playlist: ");
            userPlaylist.playlistName = Console.ReadLine();
            userPlaylist.playlistCreator = userName;

            do
            {
                try
                {
                    Console.Write("\nPlease enter when your playlist was created in MM\\DD\\YY format: ");
                    userPlaylist.creationDate = DateOnly.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid date");
                }
            } while (userPlaylist.creationDate.ToString() == "1/1/0001");
            
            Console.Write(Menu());
            choice = Int32.Parse(Console.ReadLine());

        }

        public static void EditSongInPlaylist()
        {

        }

        public static void RemoveFromPlaylist()
        {

        }

        /// <summary>
        /// Displays the user's playlist
        /// </summary>
        public static void ShowPlaylist()
        {
            if (userPlaylist.playlistCreator == "")
            {
                Console.WriteLine("A playlist does not currently exist, please create one and try again");
            }
            else
            {
                Console.WriteLine(userPlaylist.ToString());
            }

            Console.Write(Menu());
            choice = Int32.Parse(Console.ReadLine());
        }

        #endregion
    }
}