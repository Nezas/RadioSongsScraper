using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace RadioSongsScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb web = new();
            HtmlDocument doc = web.Load("https://powerhitradio.tv3.lt/top15");
            List<string> songs = new();

            ShowSongList(web, doc, songs);

            while(true)
            {
                int userChoice = GetUserChoice();
                if(userChoice == 0)
                {
                    break;
                }
                string searchText = GetSearchText(songs, userChoice);
                OpenSong(searchText);
            }
            Console.WriteLine("Program exited");
        }

        static void ShowSongList(HtmlWeb web, HtmlDocument doc, List<string> songs)
        {
            Console.WriteLine("Power Hit Radio TOP 15\n");

            for(int i = 1; i <= 15; i++)
            {
                var songName = doc.DocumentNode.SelectSingleNode($"//*[@id='topb1605831227']/div[2]/div/div/div[{i}]/div[4]/a/p");
                if(songName == null)
                {
                    songName = doc.DocumentNode.SelectSingleNode($"//*[@id='topb1605831227']/div[2]/div/div/div[{i}]/div[3]/a/p");
                }
                Console.WriteLine(i + ". " + songName.InnerText);
                songs.Add(songName.InnerText);
            }
        }

        static int GetUserChoice()
        {
            while(true)
            {
                Console.Write("\nEnter the number of the song you want to listen (0 - exit): ");
                try
                {
                    int userChoice = Convert.ToInt32(Console.ReadLine());
                    if(userChoice < 0)
                    {
                        Console.WriteLine("Choice cannot be negative.");
                    }
                    else if(userChoice > 15)
                    {
                        Console.WriteLine("Choice cannot be greater than 15.");
                    }
                    else return userChoice;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static string GetSearchText(List<string> songs, int userChoice)
        {
            return songs[userChoice - 1];
        }

        static void OpenSong(string searchText)
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