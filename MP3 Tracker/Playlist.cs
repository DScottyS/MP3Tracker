﻿           /////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        private List<MP3> newPlaylist = new List<MP3>();

        public string playlistName { get; set; }

        public string playlistCreator { get; set; }

        public DateOnly creationDate { get; set; }

        public Playlist()
        {
            newPlaylist = null;
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



        public override string ToString()
        {
            string info = "";

            info += "\n-----------------------------------------\n";
            info += $"{playlistName} by {playlistCreator}";
            info += $"created on {creationDate}";
            info += "\n-----------------------------------------\n";

            for (int i = 0; i < newPlaylist.Count; i++)
            {
                info += newPlaylist[i];
            }

            info += "\n-----------------------------------------\n";

            return info;
        }
    }
}
