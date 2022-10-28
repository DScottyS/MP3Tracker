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

            //uses the menu method to display all options available to the user and lets them choose what they want to do
            Menu();

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
                else if (choice == 4)
                {
                    ShowPlaylist();
                }
                else if (choice == 5)
                {
                    EditSongInPlaylist();
                }
                else if (choice == 6)
                {
                    RemoveFromPlaylist();
                }
                else if (choice == 7)
                {
                    DisplayBasedOnGenreOrArtist();
                }
                else if (choice == 8)
                {
                    SortBasedOnTitleOrReleaseDate();
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
        static string userName = "";
        //creates a variable that will hold the number corresponding to the choice the user inputs
        static int choice;
        //creates a new MP3 variable called newSong for the user to input information relating to their song into
        static MP3 newSong = new MP3();
        //creates a new playlist object so CreateANewSong method can tell if a playlist has been made
        static Playlist userPlaylist = new Playlist();
        //creates a variable that lets the user choose what part of a song's file they would like to edit
        static string editSong = "";
        //creates a variable that lets the user choose what song they would like to remove from the playlist
        static int songToRemove;

        static int songNumber;

        static string displayBasedOn;

        static string sortBasedOn;


        #endregion

        #region Methods
        /// <summary>
        /// menu method displays all options the user can choose from and gives them a prompt to choose from them
        /// </summary>
        public static void Menu()
        {
            
            Console.Write("\n-----------------------------------------" +
            "\n1. Create a new MP3 file" +
            "\n2. Display an MP3 file" +
            "\n3. Create a new Playlist" +
            "\n4. Display your playlist" +
            "\n5. Edit a song in the playlist" +
            "\n6. Remove a song from the playlist" +
            "\n7. WIP" +
            "\n8. Sort the songs by either title or release date" +
            "\n9. WIP" +
            "\n10. WIP" +
            "\n11. WIP" +
            "\n12. WIP" +
            "\n13. Terminate the program" +
            "\n-----------------------------------------\n");

            do
            {
                try
                {
                    Console.WriteLine("\nPlease type the number corresponding to what you would like to do: ");
                    choice = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("\ninvalid selection");
                }
                /*catch (Exception)
                {
                    throw;
                }*/
            } while (!(choice !<= 13 && choice > 0));
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
            Console.Write($"\nMP3 created successfully \n");
            Menu();
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
                    Console.WriteLine($"\nThere is currently no existing MP3\n");
                    Menu();
                }
                //if an MP3 currently exists, displays that MP3
                else
                {
                    Console.WriteLine(newSong);
                    Menu();
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

            Menu();

        }

        /// <summary>
        /// asks the user to choose the numbered position of a song and allows them to edit that song
        /// </summary>
        public static void EditSongInPlaylist()
        {

            do
            {
                try
                {
                    Console.Write("Please enter the number of the song in the playlist you would like to edit: ");
                    songNumber = Int32.Parse(Console.ReadLine());
                    userPlaylist.ChooseSong(songNumber - 1);

                    try
                    {
                        do
                        {
                            try
                            {
                                Console.Write("what would you like to edit? The choices are as follows:" +
                                    "\nTitle, Artist, Release Date (type Date), Genre, Download Cost (type Price), File Size (type size)," +
                                    "\nand File Path (type Path): ");
                                editSong = Console.ReadLine().ToLower();

                                switch (Enum.Parse(typeof(EditSongChoice), editSong))
                                {
                                    case EditSongChoice.title:
                                        Console.Write("\nWhat would you like to rename the song: ");
                                        newSong.title = Console.ReadLine();
                                        break;
                                    case EditSongChoice.artist:
                                        Console.Write("\nWhat is the artist's name: ");
                                        newSong.artist = Console.ReadLine();
                                        break;
                                    case EditSongChoice.date:
                                        Console.Write("\nWhen was the song released (MM/DD/YY): ");
                                        newSong.releaseDate = Console.ReadLine();
                                        break;
                                    case EditSongChoice.genre:
                                        Console.Write("\nWhat is the genre of the song, remember, genre must be Rock, Pop, Jazz, Country, Classical, or Other: ");
                                        newSong.genre = (Genre)Enum.Parse(typeof(Genre), Console.ReadLine().ToUpper());
                                        break;
                                    case EditSongChoice.price:
                                        Console.Write("\nWhat is the price of the song: ");
                                        newSong.downloadCost = decimal.Parse(Console.ReadLine());
                                        break;
                                    case EditSongChoice.size:
                                        Console.Write("\nWhat is the file size: ");
                                        newSong.fileSizeMB = double.Parse(Console.ReadLine());
                                        break;
                                    case EditSongChoice.path:
                                        Console.Write("\nWhat is the file path: ");
                                        newSong.filePath = Console.ReadLine();
                                        break;
                                    default:
                                        Console.WriteLine("\ninvalid choice");
                                        break;
                                }
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        } while (editSong != Enum.Parse(typeof(EditSongChoice), editSong).ToString());
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine("\ninvalid choice");
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("\ninput must be an integer");
                }
                catch (Exception)
                {
                    throw;
                }
            } while (songNumber <= -1);

            Menu();
        }

        /// <summary>
        /// removes the users desired song from the playlist
        /// </summary>
        public static void RemoveFromPlaylist()
        {
            try
            {
                do
                {
                    Console.Write("\nWhat is the numbered position of the song you would like to remove: ");
                    songToRemove = Int32.Parse(Console.ReadLine());

                    userPlaylist.RemoveSong(songToRemove - 1);
                } while (songToRemove <= 0);
            }
            catch (FormatException e)
            {
                Console.WriteLine("\nThat is not a numbered position");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"\nThere is no song at position {songToRemove} in the playlist {userPlaylist.playlistName}");
            }

            Menu();
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

            Menu();
        }

        public static void DisplayBasedOnGenreOrArtist()
        {
            Console.WriteLine("\nWould you like to see 1.) all songs with the same genre or 2.) all songs by the same artist?");
            displayBasedOn = Console.ReadLine().ToLower();

            do
            {
                if (displayBasedOn != "release date" && displayBasedOn != "1")
                {
                    Console.Write("\nwhat genre of songs would you like to display?: ");
                    Genre genre = (Genre)Enum.Parse(typeof(Genre), Console.ReadLine().ToUpper());
                    userPlaylist.DisplaySongsByGenre(genre);
                }
                else if (displayBasedOn != "artist" && displayBasedOn != "2")
                {
                    Console.Write("\nwhat artist's songs would you like to display?: ");
                    string artist = Console.ReadLine();
                    userPlaylist.DisplaySongsByArtist(artist);
                }
                else
                {
                    Console.WriteLine("that artist or genre does not exist");
                }
            } while (displayBasedOn != "title" && displayBasedOn != "release date" && displayBasedOn != "1" && displayBasedOn != "2");

            Menu();
        }

        public static void SortBasedOnTitleOrReleaseDate()
        {
            Console.Write("Would you like to sort the playlist based on 1.) title, or 2.) release date: ");
            sortBasedOn = Console.ReadLine().ToLower();

            do
            {
                switch (sortBasedOn)
                {
                    case "title": case "1":
                        userPlaylist.SortByTitle();
                        Console.WriteLine("\nplaylist has been sorted by title");

                        break;
                    case "release date": case "2":
                        userPlaylist.SortByReleaseDate();
                        Console.WriteLine("\nplaylist has been sorted by release date");
                        break;
                    default:
                        Console.WriteLine("invalid input");
                        break;
                }
            } while (sortBasedOn != "title" && sortBasedOn != "release date");
            
            Menu();

        }

        #endregion
    }
}