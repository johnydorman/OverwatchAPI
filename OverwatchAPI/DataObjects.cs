using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverwatchStats
{
    class Profile
    {
        public string icon { get; set; }
        public string name { get; set; }
        public string level { get; set; }
        public string levelIcon { get; set; }
        public string prestige { get; set; }
        public string rating { get; set; }
        public string ratingIcon { get; set; }
        public string ratingName { get; set; }
        public string gamesWon { get; set; }

        public GameStats quickPlayStats { get; set; }
        public GameStats competitiveStats { get; set; }
    }

    class GameStats
    {
        public int eliminationsAvg { get; set; }
        public int damageDoneAvg { get; set; }
        public int deathsAvg { get; set; }
        public int finalBlowsAvg { get; set; }
        public int healingDoneAvg { get; set; }
        public int objectiveKillsAvg { get; set; }
        public string objectiveTimeAvg { get; set; }
        public int soloKillsAvg { get; set; }

        public Awards awards { get; set; }
        public Games games { get; set; }

    }

    class Awards
    {
        public int cards { get; set; }
        public int medals { get; set; }
        public int medalsBronze { get; set; }
        public int medalsSilver { get; set; }
        public int medalsGold { get; set; }
    }

    class Games
    {
        public int played { get; set; }
        public int won { get; set; }
    }
}
