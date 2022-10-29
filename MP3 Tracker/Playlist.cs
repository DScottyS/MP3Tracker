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
        private List<MP3> newPlaylist;

        /// <summary>
        /// gets and sets the name of the playlist
        /// </summary>
        public string playlistName { get; set; }

        /// <summary>
        /// gets the name of the user who created the playlist
        /// </summary>
        public string playlistCreator { get; set; }

        /// <summary>
        /// gets the date of when the playlist was created
        /// </summary>
        public DateOnly creationDate { get; set; }

        /// <summary>
        /// default constructor sets all values to default values
        /// </summary>
        public Playlist()
        {
            newPlaylist = new List<MP3>();
            playlistName = "";
            playlistCreator = "";
            creationDate = new DateOnly(0001, 1, 1);
        }

        /// <summary>
        /// adds a song to the user's playlist
        /// </summary>
        /// <param name="songToAdd">Song being added to the playlist</param>
        public void AddToPlaylist(MP3 songToAdd)
        {
            newPlaylist.Add(songToAdd);
        }

        /// <summary>
        /// counts the length of the playlist and returns it as an integer
        /// </summary>
        /// <returns>the integer equivalent to the length of the playlist</returns>
        public int PlaylistLength()
        {
            return newPlaylist.Count();
        }

        /// <summary>
        /// takes the user's input of what song they would like to choose and takes the indexed position of that number - 1
        /// </summary>
        /// <param name="songNum">the MP3 of the song the user chose</param>
        public void EditSong(int songNum)
        {
            try
            {
                newPlaylist.Add(newPlaylist.ElementAt(songNum - 1));
                newPlaylist.RemoveAt(songNum - 1);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"The song at position {songNum} does not exist");
            }
        }

        public void RemoveSong(int songNum)
        {
            newPlaylist.RemoveAt(songNum);
        }

        public void DisplaySongsByGenre(Genre genre)
        {
            //https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-6.0
            IEnumerable<MP3> genrePlaylist = newPlaylist.Where(song => song.genre == genre);

            foreach (MP3 songGenre in genrePlaylist)
            {
                Console.WriteLine(songGenre);
            }
        }

        /// <summary>
        /// displays all songs of a given artist as determined by the user
        /// </summary>
        /// <param name="artist">the name of the artist the user wants to display the songs of</param>
        public void DisplaySongsByArtist(string artist)
        {
            //https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where?view=net-6.0
            IEnumerable<MP3> artistPlaylist = newPlaylist.Where(song => song.artist.ToLower() == artist.ToLower());

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
            newPlaylist.Sort((x, y) => x.title.CompareTo(y.title));
        }

        /// <summary>
        /// sorts all songs from most recent to oldest
        /// </summary>
        public void SortByReleaseDate()
        { 
            newPlaylist.Sort((x, y) => x.releaseDate.CompareTo(y.releaseDate));
        }

        /// <summary>
        /// ToString method converts everything into an easy to read string
        /// </summary>
        /// <returns>all given information as a string</returns>
        public override string ToString()
        {
            string info = "";

            info += $"\n-----------------------------------------";
            info += $"\n\n{playlistName} by {playlistCreator}";
            info += $"\n\ncreated on {creationDate}";
            info += $"\n\n-----------------------------------------";

            if (newPlaylist.Count > 0)
            {
                for (int i = 0; i < newPlaylist.Count; i++)
                {
                    info += $"\n\nSong #{i + 1}\n";
                    info += newPlaylist[i];
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
