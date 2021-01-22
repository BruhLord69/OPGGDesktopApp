using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLib
{
    public class MatchProcessor
    {
        public static async Task<MatchModel> GetMatchAsync(long matchId)
        {
            StringBuilder url = new StringBuilder();
            url.Append("match/v4/matches/");
            // lol/match/v4/matches/{matchId}
            url.Append(matchId.ToString());
            Console.WriteLine(url.ToString());
            using (HttpResponseMessage response = await ApiHelper.client.GetAsync(url.ToString()))
            {
                if (response.IsSuccessStatusCode)
                {
                    MatchModel match = await response.Content.ReadAsAsync<MatchModel>();
                    return match;
                }
                else
                {
                    Console.WriteLine(response.RequestMessage);
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task<ChampionAveragesModel> GetStatAverages(List<long> gameIDs, int playerChampion)
        {
            ChampionAveragesModel averages = new ChampionAveragesModel();
            //Get the stat sums of all games
            double minutesPlayed = 0;
            foreach(var id in gameIDs)
            {
                MatchModel match = await GetMatchAsync(id);
                for(int i=0; i<match.participants.Length;i++)
                {
                    if(match.participants[i].championId == playerChampion)
                    {
                        if (match.participants[i].stats.win == true) averages.Wins += 1;
                        else averages.Losses += 1;
                        averages.KillAverage += match.participants[i].stats.kills;
                        averages.DeathAverage += match.participants[i].stats.deaths;
                        averages.AssistAverage += match.participants[i].stats.assists;
                        averages.CreepScoreAverage += match.participants[i].stats.totalMinionsKilled;
                        minutesPlayed += match.gameDuration;
                    }
                }    
            }
            //divide by games played to get the average
            averages.ChampionId = playerChampion;
            averages.Winrate = Math.Round(averages.Wins / (double)gameIDs.Count() * (double)100, 2);
            averages.KillAverage = Math.Round(averages.KillAverage / (double)gameIDs.Count(),2);
            averages.DeathAverage = Math.Round(averages.DeathAverage / (double)gameIDs.Count(),2);
            averages.AssistAverage = Math.Round(averages.AssistAverage / (double)gameIDs.Count());
            averages.CreepScoreAverage = Math.Round(averages.CreepScoreAverage / (double)gameIDs.Count(),2);
            averages.CreepScoreAveragePerMinute = Math.Round(averages.CreepScoreAverage / (minutesPlayed/60),2);

            return averages;
        }
    }
}
