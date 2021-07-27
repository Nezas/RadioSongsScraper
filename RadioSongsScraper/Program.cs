using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace RadioSongsScraper
{
    public class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb web = new();
            HtmlDocument doc = web.Load("https://powerhitradio.tv3.lt/top15");
            List<string> songs = new();

            GetSongList(web, doc, songs);
            ShowSongList(songs);

            int userChoice = GetUserChoice();
            while(userChoice != 0)
            {
                if(ValidateUserChoice(userChoice))
                {
                    string searchText = GetSearchText(songs, userChoice);
                    OpenSong(searchText);
                }
                userChoice = GetUserChoice();
            }

            Console.WriteLine("Program exited.");
        }

        public static void GetSongList(HtmlWeb web, HtmlDocument doc, List<string> songs)
        {
            for(int i = 1; i <= 15; i++)
            {
                var songName = doc.DocumentNode.SelectSingleNode($"//*[@id='topb1605831227']/div[2]/div/div/div[{i}]/div[4]/a/p");
                if(songName == null)
                {
                    songName = doc.DocumentNode.SelectSingleNode($"//*[@id='topb1605831227']/div[2]/div/div/div[{i}]/div[3]/a/p");
                }
                songs.Add(songName.InnerText);
            }
        }

        public static void ShowSongList(List<string> songs)
        {
            Console.WriteLine("Power Hit Radio TOP 15\n");

            for(int i = 0; i < 15; i++)
            {
                Console.WriteLine(i + 1 + ". " + songs[i]);
            }
        }

        public static int GetUserChoice()
        {
            while(true)
            {
                Console.Write("\nEnter the number of the song you want to listen (0 - exit): ");
                try
                {
                    int userChoice = Convert.ToInt32(Console.ReadLine());
                    return userChoice;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static bool ValidateUserChoice(int userChoice)
        {
            if(userChoice < 0)
            {
                Console.WriteLine("Choice cannot be negative.");
                return false;
            }
            else if(userChoice > 15)
            {
                Console.WriteLine("Choice cannot be greater than 15.");
                return false;
            }
            return true;
        }

        public static string GetSearchText(List<string> songs, int userChoice)
        {
            return songs[userChoice - 1];
        }

        public static void OpenSong(string searchText)
        {
            string uri = $"https://www.youtube.com/results?search_query={searchText}";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            var process = System.Diagnostics.Process.Start(psi);
            Console.WriteLine("Song was successfully opened!");
        }
    }
}