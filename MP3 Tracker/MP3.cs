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
        public string Artist { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public double PlaybackTime { get; set; }
        public Genre Genre { get; set; }
        public decimal DownloadCost { get; set; }
        public double FileSizeMB { get; set; }
        public string FilePath { get; set; }

        /// <summary>
        /// default constructor initializes the MP3 so that the main method can tell if an MP3 has been made or not
        /// </summary>
        public MP3()
        {
            Title = "";
            Artist = "";
            ReleaseDate = new DateOnly (0001, 1, 1);
            PlaybackTime = 0;
            Genre = Genre.NONE;
            DownloadCost = 0;
            FileSizeMB = 0;
            FilePath = "";
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
            this.Artist = artist;
            this.ReleaseDate = releaseDate;
            this.PlaybackTime = playbackTime;
            this.Genre = genre;
            this.DownloadCost = downloadCost;
            this.FileSizeMB = fileSizeMB;
            this.FilePath = filePath;
        }

        /// <summary>
        /// ToString method converts everything into a string so it can be called in the main method
        /// </summary>
        /// <returns>string displaying the information the user input</returns>
        public override string ToString()
        {
            string info = "";

            info += $"\nMP3 Title: {Title}";
            info += $"\nArtist: {Artist}";
            info += $"\nRelease Date: {ReleaseDate}       \t\tGenre: {Genre}";
            info += $"\nDownload Cost: ${DownloadCost}       \t\tFile Size: {FileSizeMB}MB";
            info += $"\nPlayback Time: {Math.Round(PlaybackTime/60, 2)} Mins       \t\tAlbum Photo: {FilePath}";
            
            return info;
        }
    }
}