           /////////////////////////////////////////////////////////////////////////////////////////////////////////
          //                                                                                                     //
         //                                                                                                     //
        // Project: MP3 Tracker Cont.                                                                          //
       // File Name: Playlist                                                                                 //
      // Description:                                                                                        //
     // Course: CSCI 1260 – Introduction to Computer Science II                                             //
    // Author: Scotty Snyder, snyderds@etsu.edu, Department of Computing, East Tennessee State University  //
   // Created: Sunday, October 25, 2022                                                                   //
  // Copyright: Scotty Snyder, 2022                                                                      //
 //                                                                                                     //
/////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3_Tracker
{
    /// <summary>
    /// playlist class allows the user to create a new playlist and execute functions having to do with the playlist
    /// </summary>
    internal class Playlist
    {
        /// <summary>
        /// creates a new private list of MP3s
        /// </summary>
        private List<MP3> NewPlaylist;

        /// <summary>
        /// gets and sets the name of the playlist
        /// </summary>
        public string PlaylistName { get; set; }

        /// <summary>
        /// gets the name of the user who created the playlist
        /// </summary>
        public string PlaylistCreator { get; set; }

        /// <summary>
        /// gets the date of when the playlist was created
        /// </summary>
        public DateOnly CreationDate { get; set; }

        public bool SaveNeeded { get; set; }

        /// <summary>
        /// default constructor sets all values to default values
        /// </summary>
        public Playlist()
        {
            NewPlaylist = new List<MP3>();
            PlaylistName = "";
            PlaylistCreator = "";
            CreationDate = new DateOnly(0001, 1, 1);
        }

        /// <summary>
        /// adds a song to the user's playlist
        /// </summary>
        /// <param name="songToAdd">Song being added to the playlist</param>
        public void AddToPlaylist(MP3 songToAdd)
        {
            NewPlaylist.Add(songToAdd);

            SaveNeeded = true;
        }

        /// <summary>
        /// counts the length of the playlist and returns it as an integer
        /// </summary>
        /// <returns>the integer equivalent to the length of the playlist</returns>
        public int PlaylistLength()
        {
            return NewPlaylist.Count();
        }

        /// <summary>
        /// takes the user's input of what song they would like to choose and takes the indexed position of that number - 1
        /// </summary>
        /// <param name="songNum">the MP3 of the song the user chose</param>
        public void EditSong(int songNum)
        {
            try
            {
                NewPlaylist.Add(NewPlaylist.ElementAt(songNum - 1));
                NewPlaylist.RemoveAt(songNum - 1);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"The song at position {songNum} does not exist");
            }

            SaveNeeded = true;
        }

        /// <summary>
        /// removes a desired song in position songNum
        /// </summary>
        /// <param name="songNum">the indexed position of the song that should be removed</param>
        public void RemoveSong(int songNum)
        {
            NewPlaylist.RemoveAt(songNum);

            SaveNeeded = true;
        }

        /// <summary>
        /// allows the user to search for songs of a given title
        /// </summary>
        /// <param name="title">title of the song the user wishes display</param>
        public void SearchForTitle(string title)
        {
            //https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-6.0
            IEnumerable<MP3> songInPlaylist = NewPlaylist.Where(song => song.Title.ToLower() == title.ToLower());

            foreach (MP3 songTitle in songInPlaylist)
            {
                Console.WriteLine(songTitle);
            }
        }

        /// <summary>
        /// takes the Genre the user would like to display and displays each song with that Genre
        /// </summary>
        /// <param name="genre">the Genre of songs the user would like to display</param>
        public void DisplaySongsByGenre(Genre genre)
        {
            NewPlaylist = new List<MP3>();

            //https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-6.0
            IEnumerable<MP3> genrePlaylist = NewPlaylist.Where(song => song.Genre == genre);

            foreach (MP3 songGenre in genrePlaylist)
            {
                Console.WriteLine(songGenre);
            }
        }

        /// <summary>
        /// displays all songs of a given Artist as determined by the user
        /// </summary>
        /// <param name="artist">the name of the Artist the user wants to display the songs of</param>
        public void DisplaySongsByArtist(string artist)
        {
            //https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-6.0
            IEnumerable<MP3> artistPlaylist = NewPlaylist.Where(song => song.Artist.ToLower() == artist.ToLower());

            foreach (MP3 songArtist in artistPlaylist)
            {
                Console.WriteLine(songArtist);
            }
        }

        /// <summary>
        /// sorts all songs by title alphabetically 
        /// </summary>
        public void SortByTitle()
        {
            NewPlaylist.Sort((x, y) => x.Title.CompareTo(y.Title));

            SaveNeeded = true;
        }

        /// <summary>
        /// sorts all songs from most recent to oldest
        /// </summary>
        public void SortByReleaseDate()
        { 
            NewPlaylist.Sort((x, y) => x.ReleaseDate.CompareTo(y.ReleaseDate));

            SaveNeeded = true;
        }

        /// <summary>
        /// allows the user to load a pre-existing playlist from a file
        /// </summary>
        /// <param name="filePath">path to the file the user wishes to write to</param>
        public void FillFromFile(string filePath)
        {
            try
            {
                StreamReader sr = new StreamReader(filePath);

                try
                {
                    //reads the first line of the user's file to get the name of the playlist, the user's name, and the playlist's creation date
                    string firstLine = sr.ReadLine();
                    string[] playlistInfo = firstLine.Split("|");

                    PlaylistName = playlistInfo[0];
                    PlaylistCreator = playlistInfo[1];
                    CreationDate = DateOnly.Parse(playlistInfo[2]);

                    sr.ReadLine();

                    //reads each line, creates a new MP3, and fills it with the appropriate information for the rest of the file until there is nothing left to read
                    while (sr.Peek() != -1)
                    {
                        string line = sr.ReadLine();
                        string[] songInfo = line.Split("|");

                        //tries to parse songInfo[4] to see if it can be parsed as type genre
                        Genre genre;
                        if (Enum.TryParse(songInfo[4].ToUpper(), out genre))
                        {
                            MP3 song = new MP3(songInfo[0], songInfo[1], DateOnly.Parse(songInfo[2]), Int32.Parse(songInfo[3]),
                                        (Genre)Enum.Parse(typeof(Genre), songInfo[4].ToUpper()), decimal.Parse(songInfo[5]), double.Parse(songInfo[6]), songInfo[7]);

                            NewPlaylist.Add(song);
                        }
                        //if songInfo[4] cant be parsed, the information gets replaced with the string OTHER, which will automatically 
                        //set it to OTHER when parsing as type genre
                        else
                        {
                            songInfo[4] = "OTHER";

                            MP3 song = new MP3(songInfo[0], songInfo[1], DateOnly.Parse(songInfo[2]), Int32.Parse(songInfo[3]),
                                        (Genre)Enum.Parse(typeof(Genre), songInfo[4].ToUpper()), decimal.Parse(songInfo[5]), double.Parse(songInfo[6]), songInfo[7]);

                            NewPlaylist.Add(song);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (sr != null)
                    {
                        sr.Close();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"\nThe file at file path {filePath} does not exist");
            }
        }

        /// <summary>
        /// allows the user to save their current playlist to a txt file
        /// </summary>
        /// <param name="filePath">path to the file the user wishes to write to</param>
        public void SaveToFile(string filePath)
        {
            //creates a new StreamWriter to write to the file the user specifies with
            StreamWriter sw = new StreamWriter(filePath);

            try
            {
                //writes down the name of the playlist, the name of the playlist's creator, and when it was created
                sw.WriteLine(PlaylistName + "|" + PlaylistCreator + "|" + CreationDate);
                sw.WriteLine();

                //writes down each song in a specific format
                for (int i = 0; i < NewPlaylist.Count; i++)
                {
                    sw.WriteLine(NewPlaylist.ElementAt(i).Title + "|" + NewPlaylist.ElementAt(i).Artist + "|" + NewPlaylist.ElementAt(i).ReleaseDate
                         + "|" + NewPlaylist.ElementAt(i).PlaybackTime + "|" + NewPlaylist.ElementAt(i).Genre + "|" + NewPlaylist.ElementAt(i).DownloadCost
                         + "|" + NewPlaylist.ElementAt(i).FileSizeMB + "|" + NewPlaylist.ElementAt(i).FilePath);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"\nThe file at file path {filePath} does not exist");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //when there are no more lines for the StreamWriter to write, closes the file
                //if SaveNeeded is currently true, set it to false
                if (sw != null)
                {
                    sw.Close();
                    SaveNeeded = false;
                }
            }
        }

        /// <summary>
        /// ToString method converts everything into an easy to read string
        /// </summary>
        /// <returns>all given information as a string</returns>
        public override string ToString()
        {
            string info = "";

            info += $"\n-----------------------------------------";
            info += $"\n\n{PlaylistName} by {PlaylistCreator}";
            info += $"\n\ncreated on {CreationDate}";
            info += $"\n\n-----------------------------------------";

            if (NewPlaylist.Count > 0)
            {
                for (int i = 0; i < NewPlaylist.Count; i++)
                {
                    info += $"\n\nSong #{i + 1}\n";
                    info += NewPlaylist[i];
                    info += $"\n";
                }
            }
            else
            {
                info += "\nThere are no songs currently in your playlist";
            }

            return info;
        }
    }
}
