using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLib
{
    public class ChampionAveragesModel
    {
            public int ChampionId { get; set; }
            public int Wins { get; set; }
            public int Losses { get; set; }
            public double Winrate { get; set; }
            public double KillAverage { get; set; }
            public double DeathAverage { get; set; }
            public double AssistAverage { get; set; }
            public double CreepScoreAverage { get; set; }
            public double CreepScoreAveragePerMinute { get; set; }
    }
}
