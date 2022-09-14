           /////////////////////////////////////////////////////////////////////////////////////////////////////////
          //                                                                                                     //
         //                                                                                                     //
        // Project: MP3 Tracker                                                                                //
       // File Name: Project1MP3                                                                              //
      // Description:                                                                                        //
     // Course: CSCI 1260 – Introduction to Computer Science II                                             //
    // Author: Scotty Snyder, snyderds@etsu.edu, Department of Computing, East Tennessee State University  //
   // Created: Thursday, September 7, 2022                                                                //
  // Copyright: Scotty Snyder, 2022                                                                      //
 //                                                                                                     //
/////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MP3_Tracker
{
    public class MP3
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string ReleaseDate { get; set; }
        public double PlaybackTime { get; set; }
        public Genre Genre { get; set; }
        public decimal DownloadCost { get; set; }
        public double FileSizeMB { get; set; }

        public string Path = "C:/Users/Scotty/Downloads/Funny_Monkey.jpg";


        public override string ToString()
        {
            string info = "";

            info += $"MP3 Title: {Title}";
            info += $"Artist: {Artist}";
            info += $"Release Date: {ReleaseDate}           Genre:{Genre}";
            info += $"Download Cost: {DownloadCost}         File Size: {FileSizeMB}";
            info += $"Playback Time: {PlaybackTime}         Album Photo: {Path}";

            return info;
        }
    }
}