using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ApiLib;

namespace VisualApp
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.IntializeClient();
        }
        private async Task<SummonerModel> LoadSummoner(string username)
        {
            return await SummonerProcessor.GetSummonerAsync(username);
        }
        private async Task<LeagueModel> LoadLeague(string id)
        {
            return await LeagueProcessor.GetLeagueAsync(id);
        }
        private async Task<MatchModel> LoadMatchData(long matchId)
        {
            return await MatchProcessor.GetMatchAsync(matchId);
        }
        private async Task<MatchesModel> LoadMatches(string accountId, int weekNumber)
        {
            return await MatchesProcessor.GetMatchesAsync(accountId, weekNumber);
        }
        private async Task<MatchesModel> LoadLatestMatches(string accoundId, int beginIndex, int endIndex)
        {
            return await MatchesProcessor.GetLatestMatchesAsync(accoundId, beginIndex, endIndex);
        }
        private void TextBox_Focus(object sender, RoutedEventArgs e)
        {
                firstNameText.Text = string.Empty;
        }

        private int[] CalculateMostPlayedChampions(List<MatchesModel> matches)
        {
            //Return champion averages list with all game statistics
            //1. send data to method in 2nd task
            //2. make a method in MatchProcessor to make requests from a list of MatchIDs to GetMatchAsync method
            //3. Store those values in a List
            //List<ChampionAveragesModel> mostPlayedChampionData = new List<ChampionAveragesModel>();

            List<int> championsPlayed = new List<int>();
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i] != null)
                {
                    for (int j = 0; j < matches[i].totalGames; j++)
                    {
                        championsPlayed.Add(matches[i].matches[j].champion);
                    }
                }
            }
            var championsGrouped = championsPlayed.GroupBy(c => c).OrderByDescending(c => c.Count());
            int[] mostPlayed = new int[5];
            int k = 0;
            foreach (var champion in championsGrouped.Take(5))
            {
                mostPlayed[k] = champion.Key;
                k++;
            }
            return mostPlayed;
        }

        private List<long> GetGameIDs(List<MatchesModel> matches, int champion)
        {
            List<long> gameIds = new List<long>();
            for (int j = 0; j < matches.Count(); j++)
            {
                if (matches[j] != null)
                {
                    IEnumerable<MatchesModel.Match> temp = matches[j].matches.Where(m => m.champion == champion);
                    for (int k = 0; k < temp.Count(); k++)
                    {
                        gameIds.Add(temp.ElementAt(k).gameId);
                    }

                }
            }
            return gameIds;
        }
        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            var SummonerData = await LoadSummoner(firstNameText.Text);
            var LeagueData = await LoadLeague(SummonerData.id);
            int weekCount = MatchesProcessor.WeekCount();
            List<MatchesModel> matchPaginatedList = new List<MatchesModel>();
            for (int i = 0; i < weekCount; i++)
            {
                matchPaginatedList.Add(await LoadMatches(SummonerData.accountId, i));
            }

            MatchesModel latestThreeMatchesID = await LoadLatestMatches(SummonerData.accountId, 0, 3);
            List<MatchModel> latestThreeMatchesData = new List<MatchModel>();
            int[] championsIDs = new int[3];
            if (latestThreeMatchesID != null)
            {
                for (int i = 0; i < latestThreeMatchesID.matches.Length; i++)
                {
                    championsIDs[i] = latestThreeMatchesID.matches[i].champion;
                }
                
                for (int i = 0; i < 3; i++)
                {
                    latestThreeMatchesData.Add(await LoadMatchData(latestThreeMatchesID.matches[i].gameId));
                }
            }         
            
            int[] mostPlayed = CalculateMostPlayedChampions(matchPaginatedList);
            List<ChampionAveragesModel> mostPlayedChampionData = new List<ChampionAveragesModel>();
            for (int i = 0; i < 5; i++)
            {
                mostPlayedChampionData.Add(await MatchProcessor.GetStatAverages(GetGameIDs(matchPaginatedList, mostPlayed[i]), mostPlayed[i]));
            }
            
            
            ProfileWindow profile = new ProfileWindow(SummonerData, LeagueData, matchPaginatedList, mostPlayedChampionData, latestThreeMatchesData, championsIDs);
            profile.Show();
            profile.Activate();
            Close();
        }
    }
}
