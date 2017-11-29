using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.IO;

namespace OverwatchStats
{
    class Program
    {
        static void Main(string[] args)
        {
            DataRequest data = new DataRequest(onData);
            string battletagEntry = "Johny-2380";

            while (battletagEntry.Length != 0)
            {
                Console.WriteLine("Enter a BattleTag:\n(Example-1234 for Example#1234) or nothing to close");
                battletagEntry = Console.ReadLine();

                Task.Run(async () =>
                {
                    await data.Lookup(battletagEntry);
                }).GetAwaiter().GetResult();
            }
        }

        public static void onData(DataRequest data)
        {
            DataSave.SaveProfile(data.profile);
            Window.display(data.profile);
        }
    }

    // An Example of output
    class Window
    {
        public static void display(Profile profile)
        {
            Console.WriteLine("\tOverwatch Stats");
            Console.WriteLine("\t-------------------------------------------------------------------------");
            Console.WriteLine("\tName: " + profile.name + "   Level: " + profile.prestige + "" +profile.level + "   Rating: " + profile.rating);
            Console.WriteLine();
            Console.WriteLine("\tQuickplay:");
            Console.WriteLine("\t-------------------------------------------------------------------------");
            Console.WriteLine("\tGames Won: " + profile.quickPlayStats.games.won);
            Console.WriteLine("\tMedals: " + profile.quickPlayStats.awards.medals);
            Console.WriteLine();
            Console.WriteLine("\tCompetitive:");
            Console.WriteLine("\t-------------------------------------------------------------------------");
            Console.WriteLine("\tGames played: " + profile.competitiveStats.games.played + "   Games Won: " + profile.competitiveStats.games.won);
            Console.WriteLine("\tMedals: " + profile.competitiveStats.awards.medals);
            Console.WriteLine();
        }
    }

    class Config
    {
        Config()
        {

        }
    }

    class DataSave
    {
        private static DateTime dateTime;

        // Saves the file, returns status
        public static bool SaveProfile(Profile profile) {
            // Updates the date
            dateTime = DateTime.UtcNow.Date;

            // trys to write the Json
            try
            {
                File.WriteAllText(@"e:\temp\" + profile.name + dateTime.ToString("dd_MM_yyyy") + ".Json", JsonConvert.SerializeObject(profile));
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
            return true;
        }

        // Reads a stored profile, returns profile or null
        public static Profile ReadProfile(string dir)
        {
            String file;

            // Trys to read a json
            try{
                file = File.ReadAllText(@dir);
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }

            return JsonConvert.DeserializeObject<Profile>(file); ;
        } 
    }
}
