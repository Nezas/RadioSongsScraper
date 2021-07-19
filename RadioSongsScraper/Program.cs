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

            SongList(web, doc, songs);
            OpenSongs(songs);
        }

        static void SongList(HtmlWeb web, HtmlDocument doc, List<string> songs)
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

        static void OpenSongs(List<string> songs)
        {
            int choice = 0;
            string searchText;
            string uri;
            var psi = new System.Diagnostics.ProcessStartInfo();
            var process = new System.Diagnostics.Process();
            psi.UseShellExecute = true;

            do
            {
                Console.Write("\nEnter the number of the song you want to listen (0 - exit): ");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                    if(choice == 0) break;
                    searchText = songs[choice - 1];
                }
                catch(ArgumentOutOfRangeException) when(choice < 0)
                {
                    Console.WriteLine("Choice cannot be negative.");
                    break;
                }
                catch(ArgumentOutOfRangeException) when(choice > 15)
                {
                    Console.WriteLine("Choice cannot be greater than 15.");
                    break;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                Console.Write("Song was successfully opened!");

                searchText = songs[choice - 1];
                uri = $"https://www.youtube.com/results?search_query={searchText}";
                psi.FileName = uri;
                process = System.Diagnostics.Process.Start(psi);
            } while(choice != 0);

            process.Close();
            Console.WriteLine("Program exited");
        }
    }
}
