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
        public string Title { get; set; }
        public string artist { get; set; }
        public DateOnly releaseDate { get; set; }
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
            Title = "";
            artist = "";
            releaseDate = new DateOnly (0001, 1, 1);
            playbackTime = 0;
            genre = Genre.NONE;
            downloadCost = 0;
            fileSizeMB = 0;
            filePath = "";
        }

        /// <summary>
        /// Parameterized constructor for MP3 objects
        /// </summary>
        /// <param name="title"></param>
        /// <param name="artist"></param>
        /// <param name="releaseDate"></param>
        /// <param name="playbackTime"></param>
        /// <param name="genre"></param>
        /// <param name="downloadCost"></param>
        /// <param name="fileSizeMB"></param>
        /// <param name="filePath"></param>
        public MP3(string title, string artist, DateOnly releaseDate, double playbackTime, Genre genre, decimal downloadCost, double fileSizeMB, string filePath)
        {
            Title = title;
            this.artist = artist;
            this.releaseDate = releaseDate;
            this.playbackTime = playbackTime;
            this.genre = genre;
            this.downloadCost = downloadCost;
            this.fileSizeMB = fileSizeMB;
            this.filePath = filePath;
        }

        /// <summary>
        /// ToString method converts everything into a string so it can be called in the main method
        /// </summary>
        /// <returns>string displaying the information the user input</returns>
        public override string ToString()
        {
            string info = "";

            info += $"\nMP3 Title: {Title}";
            info += $"\nArtist: {artist}";
            info += $"\nRelease Date: {releaseDate}       \t\tGenre: {genre}";
            info += $"\nDownload Cost: ${downloadCost}       \t\tFile Size: {fileSizeMB}MB";
            info += $"\nPlayback Time: {Math.Round(playbackTime/60, 2)} Mins       \t\tAlbum Photo: {filePath}";
            
            return info;
        }
    }
}