           /////////////////////////////////////////////////////////////////////////////////////////////////////////
          //                                                                                                     //
         //                                                                                                     //
        // Project: MP3 Tracker                                                                                //
       // File Name: MP                                                                              //
      // Description: This is where all the information the user inputs into the MP3 Driver is stored        //
     // Course: CSCI 1260 – Introduction to Computer Science II                                             //
    // Author: Scotty Snyder, snyderds@etsu.edu, Department of Computing, East Tennessee State University  //
   // Created: Thursday, September 7, 2022                                                                //
  // Copyright: Scotty Snyder, 2022                                                                      //
 //                                                                                                     //
/////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MP3_Tracker
{
    /// <summary>
    /// A class that holds all the information related to the MP3 a user input
    /// </summary>
    public class MP3
    {
        /// <summary>
        /// properties get and set the information about a song the user input such as: Title, Artist, Release date, 
        /// Playback time, Genre, Download cost, File size, and the path for the album photo
        /// </summary>
        public string title { get; set; }
        public string artist { get; set; }
        public string releaseDate { get; set; }
        public double playbackTime { get; set; }
        public Genre genre { get; set; }
        public decimal downloadCost { get; set; }
        public double fileSizeMB { get; set; }
        public string filePath { get; set; }

        /// <summary>
        /// default constructor initializes the MP3 so that the main method can tell if an MP3 has been made or not
        /// </summary>
        public MP3()
        {
            title = "";
            artist = "";
            releaseDate = "";
            playbackTime = 0;
            genre = new Genre();
            downloadCost = 0;
            fileSizeMB = 0;
            filePath = "";
        }

        /// <summary>
        /// ToString method converts everything into a string so it can be called in the main method
        /// </summary>
        /// <returns>string displaying the information the user input</returns>
        public override string ToString()
        {
            string info = "";

            info += $"\nMP3 Title: {title}";
            info += $"\nArtist: {artist}";
            info += $"\nRelease Date: {releaseDate}         \tGenre:{genre}";
            info += $"\nDownload Cost: ${downloadCost}         \tFile Size: {fileSizeMB}MB";
            info += $"\nPlayback Time: {Math.Round(playbackTime/60, 2)} Mins         \tAlbum Photo: {filePath}";
            
            return info;
        }
    }
}