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
    internal class Playlist
    {
        private List<MP3> newPlaylist;

        public string playlistName { get; set; }

        public string playlistCreator { get; set; }

        public DateOnly creationDate { get; set; }

        public Playlist()
        {
            newPlaylist = new List<MP3>();
            playlistName = "";
            playlistCreator = "";
            creationDate = new DateOnly(0001, 1, 1);
        }

        public Playlist(List<MP3> newPlaylist, string playlistName, string playlistCreator, DateOnly creationDate)
        {
            this.newPlaylist = newPlaylist;
            this.playlistName = playlistName;
            this.playlistCreator = playlistCreator;
            this.creationDate = creationDate;
        }

        /// <summary>
        /// adds a song to the user's playlist
        /// </summary>
        /// <param name="songToAdd">Song being added to the playlist</param>
        public void AddToPlaylist(MP3 songToAdd)
        {
            newPlaylist.Add(songToAdd);
        }

        public void ChooseSong(int songNum)
        {
            try
            {
                newPlaylist.ElementAt(songNum);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"The song at position {songNum + 1} does not exist");
            }
        }

        public void RemoveSong(int songNum)
        {
            newPlaylist.RemoveAt(songNum);
        }

        public void DisplaySongsByGenre()
        {
            
        }

        public void DisplaySongsByArtist(string artist)
        {
            
        }

        public void SortByTitle()
        {
            newPlaylist.Sort((x, y) => x.title.CompareTo(y.title));
        }

        public void SortByReleaseDate()
        {
            newPlaylist.Sort((x, y) => x.releaseDate.CompareTo(y.releaseDate));
        }

        public override string ToString()
        {
            string info = "";

            info += $"\n-----------------------------------------";
            info += $"\n\n{playlistName} by {playlistCreator}";
            info += $"\n\ncreated on {creationDate}";
            info += $"\n\n-----------------------------------------\n\n";

            for (int i = 0; i < newPlaylist.Count; i++)
            {
                info += $"Song #{i + 1}\n";
                info += newPlaylist[i];
                info += $"\n";
            }

            return info;
        }
    }
}
