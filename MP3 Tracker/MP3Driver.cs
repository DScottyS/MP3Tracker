﻿           /////////////////////////////////////////////////////////////////////////////////////////////////////////
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
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MP3_Tracker
{
    /// <summary>
    /// MP3 Driver class communicates between the user and the MP3 and Playlist classes
    /// </summary>
    public class MP3Driver
    {
        #region Main Method
        /// <summary>
        /// Main method, initializes the program and prompts the user to enter their name and make a choice
        /// </summary>
        public static void Main()
        {
            //welcomes user
            Console.WriteLine("------------------------------------------------------------" +
                "\n\t  Welcome to the Funky Munky MP3 Tracker\n" +
                "------------------------------------------------------------" +
                "\nHere, you can create and display MP3s, please enter your name:\n");

            //stores the name the user inputs for later
            userName = Console.ReadLine();

            Console.WriteLine($"Hello {userName}! Please create a playlist:");
            CreateAPlaylist();

            //using takes the number the user gives at the end of the menu method to call the corresponding method
            do
            {
                switch (choice)
                {
                    case 1:
                        CreateANewSong();
                        break;
                    case 2:
                        DisplayASong();
                        break;
                    case 3:
                        CreateAPlaylist();
                        break;
                    case 4:
                        ShowPlaylist();
                        break;
                    case 5:
                        EditSongInPlaylist();
                        break;
                    case 6:
                        RemoveFromPlaylist();
                        break;
                    case 7:
                        DisplayByGenre();
                        break;
                    case 8:
                        DisplayByArtist();
                        break;
                    case 9:
                        SortByTitle();
                        break;
                    case 10:
                        SortByReleaseDate();
                        break;
                    case 11:
                        FillPlaylistFromFile();
                        break;
                    //when the choice is 12, calls no method and breaks, causing the program to end
                    case 12:
                        break;
                    default:
                        do
                        {
                            Console.Write("\nInvalid input please reference the menu above: ");
                            choice = Int32.Parse(Console.ReadLine());
                        } while (choice > 12 || choice <= 0);
                        break;
                }
            } while (choice != 12);

            //thanks the user and exits the program when they input 9
            if (choice == 12)
            {
                Console.WriteLine($"\nThank you for using the Funky Munky MP3 Tracker, {userName}! Have a great day!");
            }
        }
        #endregion

        #region Variables
        //creates a string for the user's name to be stored in
        private static string userName = "";
        //creates a variable that will hold the number corresponding to the choice the user inputs
        private static int choice;
        //creates a new MP3 variable called newSong for the user to input information relating to their song into
        private static MP3 newSong = new MP3();
        //takes the genre the user wants to make a song be
        private static string userGenre;
        //creates a new playlist object so CreateANewSong method can tell if a playlist has been made
        private static Playlist userPlaylist = new Playlist();
        //creates a variable that lets the user choose what part of a song's file they would like to edit
        private static string editSong = "";
        //creates a variable that lets the user choose what song they would like to remove from the playlist
        private static int songToRemove;
        //the numeric position of the song the user wants to remove from a playlist
        private static int songNumber;
        //ensures the user inputs a valid genre
        private static bool validGenre = false;
        //determines if the user wants to add a new song to the playlist
        private static char addToPlaylist;
        private static string filePath;

        #endregion

        #region Methods
        /// <summary>
        /// menu method displays all options the user can choose from and gives them a prompt to choose from them
        /// </summary>
        public static void Menu()
        {
            //displays the options available to the user
            Console.Write("\n-----------------------------------------" +
            "\n1. Create a new MP3 file" +
            "\n2. Display an MP3 file" +
            "\n3. Create a new Playlist" +
            "\n4. Display your playlist" +
            "\n5. Edit a song in the playlist" +
            "\n6. Remove a song from the playlist" +
            "\n7. Display songs of a specific genre" +
            "\n8. Display songs by a specific artist" +
            "\n9. Sory by title" +
            "\n10. Sort by release date" +
            "\n11. Terminate the program" +
            "\n-----------------------------------------\n");

            //ensures the user chooses a number between 1-11
            do
            {
                try
                {
                    Console.Write("\nPlease type the number corresponding to what you would like to do: ");
                    choice = Int32.Parse(Console.ReadLine());

                    if (choice > 11 && choice < 0)
                    {
                        Console.Write("\nThat is not an option, please reference the menu above: ");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid selection, reference the menu above: ");
                }
            } while (!(choice !<= 11 && choice > 0));
        }

        /// <summary>
        ///prompt the user to create a new song
        /// </summary>
        public static void CreateANewSong()
        {
            //creates a new MP3 object that can be stored in the playlist each time the user makes a new MP3
            newSong = new MP3();

            //stores the title of the song
            Console.Write("\nPlease enter the song's title: ");
            newSong.Title = Console.ReadLine();

            //stores the artist of the song
            Console.Write("\nPlease enter the name of the song's Artist: ");
            newSong.artist = Console.ReadLine();

            //stores when the song was released
            do
            {
                try
                {
                    Console.Write("\nPlease enter the song's release date in MM/DD/YYYY format: ");
                    newSong.releaseDate = DateOnly.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("\ninvalid date");
                }
            } while (newSong.releaseDate <= new DateOnly (0001, 1, 1));
            

            //stores the length of the song in seconds and converts it into minutes
            do
            {
                try
                {
                    Console.Write("\nPlease enter the playback length of the song in seconds: ");
                    newSong.playbackTime = (Double.Parse(Console.ReadLine()));
                }
                catch (FormatException)
                {
                    Console.WriteLine("\ninvalid playback time");
                }
            } while (newSong.playbackTime <= 0);

            //stores the genre of the song
            do
            {
                try
                {
                    do
                    {
                        try
                        {
                            Console.Write("\nPlease enter the genre of the song, this program recognizes the following:" +
                            " \nRock, Pop, Jazz, Country, Classical, and Other: ");
                            userGenre = Console.ReadLine().ToUpper();
                            newSong.genre = (Genre)Enum.Parse(typeof(Genre), userGenre);

                            if (newSong.genre == (Genre)Enum.Parse(typeof(Genre), userGenre))
                            {
                                validGenre = true;
                            }
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("\nInvalid input");
                        }
                    } while (newSong.genre == Genre.NONE);
                }
                catch (Exception)
                {

                }
            } while (validGenre == false);            

            //stores the cost of the song
            do
            {
                try
                {
                    Console.Write("\nPlease enter the cost to download the song (do not enter a $, cannot be negative): ");
                    newSong.downloadCost = Decimal.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("\nInvalid download cost");
                }
            } while (newSong.downloadCost <= -1);

            //stores the file size of the song
            do
            {
                try
                {
                    Console.Write("\nPlease enter the size of the song in MB: ");
                    newSong.fileSizeMB = Double.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    Console.WriteLine("\ninvalid file size");
                }
            } while (newSong.fileSizeMB < 0.001);
            

            //stores the location in which the song's cover art is downloaded
            Console.WriteLine("\nPlease enter the image path(i.e. C:/Users/Scotty/Downloads/FunkyMunky.png):\n");
            newSong.filePath = Console.ReadLine();

            //if the user has created a playlist, asks the user if they want to add the song to their existing playlist
            //if no playlist exists, does nothing
            if (userPlaylist.PlaylistCreator != "")
            {
                //asks the user if they would like to add the song to their playlist, takes the first character of their response
                Console.Write("\nwould you like to add this song to your playlist? Y/N: ");
                //if the user chooses Y, add to playlist
                //if the user chooses N, do not add to playlist
                //if the user fails to choose Y or N, repeat the prompt until they input a valid response

                do
                {
                    addToPlaylist = Console.ReadLine().ToUpper()[0];

                    if (addToPlaylist == 'Y')
                    {
                        userPlaylist.AddToPlaylist(newSong);
                        Console.WriteLine($"\n{newSong.Title} has been added to {userPlaylist.PlaylistName}");
                    }
                    else if (addToPlaylist == 'N')
                    {
                        Console.WriteLine($"\n{newSong.Title} will not be added to {userPlaylist.PlaylistName}");
                    }
                    else
                    {
                        Console.Write("\nInvalid response, would you like to add this song to your playlist? Y/N: ");
                    }
                    
                } while (addToPlaylist != 'Y' && addToPlaylist != 'N');
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
                //if an MP3 does not currently exist, tells the user no MP3s exist and returns them to the menu
                if (newSong.Title == "")
                {
                    Console.WriteLine($"\nThere is currently no existing MP3\n");
                    Menu();
                }
                //if an MP3 currently exists, displays that MP3 and returns the user to the menu
                else
                {
                    Console.WriteLine(newSong);
                    Menu();
                }
            } while (choice > 9 || choice <= 0);
        }

        /// <summary>
        /// prompts the user to give the information needed to create a new playlist
        /// </summary>
        public static void CreateAPlaylist()
        {
            //gets the name the user wants to set the playlist to and sets the creator's name equal to the userName
            Console.Write("\nPlease enter the name of your playlist: ");
            userPlaylist.PlaylistName = Console.ReadLine();
            userPlaylist.PlaylistCreator = userName;

            //ensures the user inputs a valid date for the playlist's creation date
            do
            {
                try
                {
                    Console.Write("\nPlease enter when your playlist was created in MM\\DD\\YY format: ");
                    userPlaylist.CreationDate = DateOnly.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid date");
                }
            } while (userPlaylist.CreationDate.ToString() == "1/1/0001");

            Console.WriteLine($"\nPlaylist {userPlaylist.PlaylistName} created successfully");
            //displays the menu
            Menu();
        }

        /// <summary>
        /// asks the user to choose the numbered position of a song and allows them to edit that song
        /// I had a lot of trouble with this method and ultimately could not find a way to get it to work completely correctly
        /// </summary>
        public static void EditSongInPlaylist()
        {
            //makes the user choose the song they want to edit
            do
            {
                try
                {
                    Console.Write("Please enter the number of the song in the playlist you would like to edit: ");
                    songNumber = Int32.Parse(Console.ReadLine());
                    userPlaylist.EditSong(songNumber);
                }
                catch (Exception)
                {

                    throw;
                }
            } while (songNumber <= 0 && songNumber >= userPlaylist.PlaylistLength());

            //prompts the user to choose what they want to edit in the MP3 they chose
            try
            {
                Console.Write("\nWhat would you like to edit? The choices are as follows:" +
                        "\nTitle, Artist, Release Date (type Date), Genre, Download Cost (type Price), File Size (type size)," +
                        "\nand File Path (type Path): ");
                editSong = Console.ReadLine().ToLower();

            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{editSong} is an invalid input");
            }

            //sets whatever the user was trying to change to a valid value
            try
            {
                switch (Enum.Parse(typeof(EditSongChoice), editSong))
                {
                    case EditSongChoice.title:
                        Console.Write("\nWhat would you like to rename the song: ");
                        newSong.Title = Console.ReadLine();
                        break;
                    case EditSongChoice.artist:
                        Console.Write("\nWhat is the artist's name: ");
                        newSong.artist = Console.ReadLine();
                        break;
                    case EditSongChoice.date:
                        Console.Write("\nWhen was the song released (MM/DD/YY): ");
                        newSong.releaseDate = DateOnly.Parse(Console.ReadLine());
                        break;
                    case EditSongChoice.genre:
                        Console.Write("\nWhat is the genre of the song, remember, genre must be Rock, Pop, Jazz, Country, Classical, or Other: ");
                        newSong.genre = (Genre)Enum.Parse(typeof(Genre), Console.ReadLine().ToUpper());
                        break;
                    case EditSongChoice.price:
                        try
                        {
                            Console.Write("\nWhat is the price of the song: ");
                            newSong.downloadCost = decimal.Parse(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("\nNot a valid price");
                        }
                        break;
                    case EditSongChoice.size:
                        do
                        {
                            try
                            {
                                Console.Write("\nPlease enter the size of the song in MB: ");
                                newSong.fileSizeMB = Double.Parse(Console.ReadLine());
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("\nNot a valid file size");
                            }
                        } while (newSong.fileSizeMB < 0.001);
                        break;
                    case EditSongChoice.path:
                        Console.Write("\nWhat is the file path: ");
                        newSong.filePath = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("\nThat is not an option supported by this program");
                        break;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("\nInvalid date");
            }
            catch (FormatException e)
            {
                Console.WriteLine("\nNot a valid date");
            }
            
            //calls the menu method
            Menu();
        }

        /// <summary>
        /// removes the users desired song from the playlist
        /// </summary>
        public static void RemoveFromPlaylist()
        {
            //gets the numbered position of the song the user wants to remove and deletes it from the playlist
            try
            {
                do
                {
                    Console.Write("\nWhat is the numbered position of the song you would like to remove: ");
                    songToRemove = Int32.Parse(Console.ReadLine());

                    userPlaylist.RemoveSong(songToRemove - 1);
                    Console.WriteLine("Song has been deleted\n");

                } while (songToRemove <= 0);
            }
            catch (FormatException)
            {
                Console.WriteLine("\nThat is not a numbered position");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"\nThere is no song at position {songToRemove} in the playlist {userPlaylist.PlaylistName}");
            }

            //takes the user back to the Menu
            Menu();
        }

        public static void DisplayByGenre()
        {
            try
            {
                Console.Write("\nwhat genre of songs would you like to display?: ");
                Genre genre = (Genre)Enum.Parse(typeof(Genre), Console.ReadLine().ToUpper());

                userPlaylist.DisplaySongsByGenre(genre);


            }
            catch (ArgumentException)
            {
                Console.WriteLine("\nInvalid genre");
            }

            Menu();
        }

        public static void DisplayByArtist()
        {
            Console.Write("\nWhat artist's songs would you like to display?: ");
            string artist = Console.ReadLine();

            userPlaylist.DisplaySongsByArtist(artist);
        }

        public static void SortByTitle()
        {
            userPlaylist.SortByTitle();
            Console.WriteLine("\nPlaylist has been sorted by title");

            Menu();
        }

        public static void SortByReleaseDate()
        {
            userPlaylist.SortByReleaseDate();
            Console.WriteLine("\nPlaylist has been sorted by release date");

            Menu();
        }

        public static void SearchByTitle()
        {
            Console.Write("\nTitle of song you wan: ");
            string songTitle = Console.ReadLine();

            userPlaylist.SearchForTitle(songTitle);
        }

        /// <summary>
        /// Displays the user's playlist
        /// </summary>
        public static void ShowPlaylist()
        {
            //as long as the playlist exists, uses the ToString() method to display the playlist's contents
            if (userPlaylist.PlaylistCreator == "")
            {
                Console.WriteLine("\nA playlist does not currently exist, please create one and try again");
            }
            else
            {
                Console.WriteLine(userPlaylist.ToString());
            }

            //takes the user back to the menu
            Menu();
        }

        public static void FillPlaylistFromFile()
        {
            Console.Write("\nFile path:");
            filePath = Console.ReadLine();

            userPlaylist.FillFromFile(filePath);

            Console.WriteLine("\nRead complete");

            Menu();
        }

        #endregion
    }
}