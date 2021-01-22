using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ApiLib;

namespace VisualApp
{
    /// <summary>
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        public ProfileWindow(SummonerModel summoner, LeagueModel league, List<MatchesModel> matches, List<ChampionAveragesModel> championAverages, List<MatchModel> latestThreeMatchesData, int[] championIDs)
        {
            InitializeComponent();
            SetName(summoner.name);
            SetRank(league.rank, league.leaguePoints, league.tier);
            SetLevel(summoner.summonerLevel);
            SetIcon(summoner.profileIconId);
            SetMostPlayedSectionValues(championAverages);
            SetMatchHistory(latestThreeMatchesData, championIDs);
        }

        private void SetMatchHistory(List<MatchModel> latestThreeMatches, int[] championIDs)
        {
            /* time0
             * outcome0
             * length0
             * summonerSpellFirst0
             * summonerSpellSecond0
             * keystoneFirst0
             * keystoneSecond0
             * championIcon0
             * championName0
             * matchKDAFull0
             * matchKDA0
             * matchCS0
             * level0
             */

                string[] championNamesArray = ChampionNames(championIDs);
                //0
                if(latestThreeMatches.Count >= 1)
            {
                time0.Content = DateTimeOffset.FromUnixTimeMilliseconds(latestThreeMatches[0].gameCreation).ToString("d");
                foreach (var participant in latestThreeMatches[0].participants)
                {
                    if (participant.championId == championIDs[0])
                    {
                        if (participant.stats.win == true)
                        {
                            outcome0.Content = "Victory";
                        }
                        else
                        {
                            outcome0.Content = "Defeat";
                        }
                        championName0.Content = championNamesArray[0];
                        matchKDAFull0.Content = participant.stats.kills.ToString() + " / " + participant.stats.deaths.ToString() + " / " + participant.stats.assists.ToString();
                        if (participant.stats.deaths != 0)
                        {
                            matchKDA0.Content = Math.Round((double)((participant.stats.kills + participant.stats.assists) / participant.stats.deaths), 1).ToString() + " KDA";
                        }
                        else matchKDA0.Content = (participant.stats.kills + participant.stats.assists).ToString();
                        matchCS0.Content = participant.stats.totalMinionsKilled.ToString() + " CS";
                        level0.Content = participant.stats.champLevel.ToString() + " Level";
                    }
                }
                length0.Content = (latestThreeMatches[0].gameDuration / 60).ToString() + "m " + ((double)latestThreeMatches[0].gameDuration % (double)60).ToString() + "s";
                championIcon0.Source = new BitmapImage(new Uri("http://ddragon.leagueoflegends.com/cdn/11.1.1/img/champion/" + championNamesArray[0] + ".png"));

            }
                else
            {
                time0.Visibility = Visibility.Hidden;
                outcome0.Visibility = Visibility.Hidden;
                championName0.Visibility = Visibility.Hidden;
                matchKDAFull0.Visibility = Visibility.Hidden;
                matchKDA0.Visibility = Visibility.Hidden;
                matchCS0.Visibility = Visibility.Hidden;
                level0.Visibility = Visibility.Hidden;
                length0.Visibility = Visibility.Hidden;
                championIcon0.Visibility = Visibility.Hidden;
                matchHistoryBorder0.Visibility = Visibility.Hidden;
                gameType0.Visibility = Visibility.Hidden;
            }


            //1
            if(latestThreeMatches.Count >= 2)
            {
                time1.Content = DateTimeOffset.FromUnixTimeMilliseconds(latestThreeMatches[1].gameCreation).ToString("d");
                foreach (var participant in latestThreeMatches[1].participants)
                {
                    if (participant.championId == championIDs[1])
                    {
                        if (participant.stats.win == true)
                        {
                            outcome1.Content = "Victory";
                        }
                        else
                        {
                            outcome1.Content = "Defeat";
                        }
                        championName1.Content = championNamesArray[1];
                        matchKDAFull1.Content = participant.stats.kills.ToString() + " / " + participant.stats.deaths.ToString() + " / " + participant.stats.assists.ToString();
                        if (participant.stats.deaths != 0)
                        {
                            matchKDA1.Content = Math.Round((double)((participant.stats.kills + participant.stats.assists) / participant.stats.deaths), 1).ToString() + " KDA";
                        }
                        else matchKDA1.Content = (participant.stats.kills + participant.stats.assists).ToString();
                        matchCS1.Content = participant.stats.totalMinionsKilled.ToString() + " CS";
                        level1.Content = participant.stats.champLevel.ToString() + " Level";
                    }
                }
                length1.Content = (latestThreeMatches[1].gameDuration / 60).ToString() + "m " + ((double)latestThreeMatches[1].gameDuration % (double)60).ToString() + "s";
                championIcon1.Source = new BitmapImage(new Uri("http://ddragon.leagueoflegends.com/cdn/11.1.1/img/champion/" + championNamesArray[1] + ".png"));
            }
            
            else
            {
                time1.Visibility = Visibility.Hidden;
                outcome1.Visibility = Visibility.Hidden;
                championName1.Visibility = Visibility.Hidden;
                matchKDAFull1.Visibility = Visibility.Hidden;
                matchKDA1.Visibility = Visibility.Hidden;
                matchCS1.Visibility = Visibility.Hidden;
                level1.Visibility = Visibility.Hidden;
                length1.Visibility = Visibility.Hidden;
                championIcon1.Visibility = Visibility.Hidden;
                matchHistoryBorder1.Visibility = Visibility.Hidden;
                gameType1.Visibility = Visibility.Hidden;
            }
                //2
                if(latestThreeMatches.Count >=3 )
            {
                time2.Content = DateTimeOffset.FromUnixTimeMilliseconds(latestThreeMatches[2].gameCreation).ToString("d");
                foreach (var participant in latestThreeMatches[2].participants)
                {
                    if (participant.championId == championIDs[2])
                    {
                        if (participant.stats.win == true)
                        {
                            outcome2.Content = "Victory";
                        }
                        else
                        {
                            outcome2.Content = "Defeat";
                        }
                        championName2.Content = championNamesArray[2];
                        matchKDAFull2.Content = participant.stats.kills.ToString() + " / " + participant.stats.deaths.ToString() + " / " + participant.stats.assists.ToString();
                        if (participant.stats.deaths != 0)
                        {
                            matchKDA2.Content = Math.Round((double)((participant.stats.kills + participant.stats.assists) / participant.stats.deaths), 1).ToString() + " KDA";
                        }
                        else matchKDA2.Content = (participant.stats.kills + participant.stats.assists).ToString();
                        matchCS2.Content = participant.stats.totalMinionsKilled.ToString() + " CS";
                        level2.Content = participant.stats.champLevel.ToString() + " Level";
                    }
                }
                length2.Content = (latestThreeMatches[2].gameDuration / 60).ToString() + "m " + ((double)latestThreeMatches[2].gameDuration % (double)60).ToString() + "s";
                championIcon2.Source = new BitmapImage(new Uri("http://ddragon.leagueoflegends.com/cdn/11.1.1/img/champion/" + championNamesArray[2] + ".png"));
            }
                else
            {
                time2.Visibility = Visibility.Hidden;
                outcome2.Visibility = Visibility.Hidden;
                championName2.Visibility = Visibility.Hidden;
                matchKDAFull2.Visibility = Visibility.Hidden;
                matchKDA2.Visibility = Visibility.Hidden;
                matchCS2.Visibility = Visibility.Hidden;
                level2.Visibility = Visibility.Hidden;
                length2.Visibility = Visibility.Hidden;
                championIcon2.Visibility = Visibility.Hidden;
                matchHistoryBorder2.Visibility = Visibility.Hidden;
                gameType2.Visibility = Visibility.Hidden;
            }
            
        }

        private void SetMostPlayedSectionValues(List<ChampionAveragesModel> championAverages)
        {
            int[] championIDs = new int[championAverages.Count];
            for (int i = 0; i<championAverages.Count;i++)
            {
                championIDs[i] = championAverages[i].ChampionId;

            }
            string[] championNamesArray = ChampionNames(championIDs);

            //0
            if (championAverages[0].ChampionId != 0)
            {
                championImg0.Source = new BitmapImage(new Uri("http://ddragon.leagueoflegends.com/cdn/11.1.1/img/champion/" + championNamesArray[0] + ".png"));
                champion0.Text = championNamesArray[0];
                creepScore0.Text = "CS " + championAverages[0].CreepScoreAverage.ToString();
                kda0.Text = Math.Round((championAverages[0].KillAverage + championAverages[0].AssistAverage) / championAverages[0].DeathAverage, 1).ToString() + " KDA";
                fullKD0.Text = championAverages[0].KillAverage.ToString() + " / " + championAverages[0].DeathAverage.ToString() + " / " + championAverages[0].AssistAverage.ToString();
                wr0.Text = championAverages[0].Winrate.ToString() + " %";
                gamesPlayed0.Text = (championAverages[0].Wins + championAverages[0].Losses).ToString() + " games";
            }
            else
            {
                mostPlayedBorder0.Visibility = Visibility.Hidden;
                championImg0.Visibility = Visibility.Hidden;
                champion0.Visibility = Visibility.Hidden;
                creepScore0.Visibility = Visibility.Hidden;
                kda0.Visibility = Visibility.Hidden;
                fullKD0.Visibility = Visibility.Hidden;
                wr0.Visibility = Visibility.Hidden;
                gamesPlayed0.Visibility = Visibility.Hidden;
            }
            //1
            if (championAverages[1].ChampionId != 0)
            {
                championImg1.Source = new BitmapImage(new Uri("http://ddragon.leagueoflegends.com/cdn/11.1.1/img/champion/" + championNamesArray[1] + ".png"));
                champion1.Text = championNamesArray[1];
                creepScore1.Text = "CS " + championAverages[1].CreepScoreAverage.ToString();
                kda1.Text = Math.Round((championAverages[1].KillAverage + championAverages[1].AssistAverage) / championAverages[1].DeathAverage, 1).ToString() + " KDA";
                fullKD1.Text = championAverages[1].KillAverage.ToString() + " / " + championAverages[1].DeathAverage.ToString() + " / " + championAverages[1].AssistAverage.ToString();
                wr1.Text = championAverages[1].Winrate.ToString() + " %";
                gamesPlayed1.Text = (championAverages[1].Wins + championAverages[1].Losses).ToString() + " games";
            }
            else
            {
                mostPlayedBorder1.Visibility = Visibility.Hidden;
                championImg1.Visibility = Visibility.Hidden;
                champion1.Visibility = Visibility.Hidden;
                creepScore1.Visibility = Visibility.Hidden;
                kda1.Visibility = Visibility.Hidden;
                fullKD1.Visibility = Visibility.Hidden;
                wr1.Visibility = Visibility.Hidden;
                gamesPlayed1.Visibility = Visibility.Hidden;
            }
            //2
            if (championAverages[2].ChampionId != 0)
            {
                championImg2.Source = new BitmapImage(new Uri("http://ddragon.leagueoflegends.com/cdn/11.1.1/img/champion/" + championNamesArray[2] + ".png"));
                champion2.Text = championNamesArray[2];
                creepScore2.Text = "CS " + championAverages[2].CreepScoreAverage.ToString();
                kda2.Text = Math.Round((championAverages[2].KillAverage + championAverages[2].AssistAverage) / championAverages[2].DeathAverage, 1).ToString() + " KDA";
                fullKD2.Text = championAverages[2].KillAverage.ToString() + " / " + championAverages[2].DeathAverage.ToString() + " / " + championAverages[2].AssistAverage.ToString();
                wr2.Text = championAverages[2].Winrate.ToString() + " %";
                gamesPlayed2.Text = (championAverages[2].Wins + championAverages[2].Losses).ToString() + " games";
            }
            else
            {
                mostPlayedBorder2.Visibility = Visibility.Hidden;
                championImg2.Visibility = Visibility.Hidden;
                champion2.Visibility = Visibility.Hidden;
                creepScore2.Visibility = Visibility.Hidden;
                kda2.Visibility = Visibility.Hidden;
                fullKD2.Visibility = Visibility.Hidden;
                wr2.Visibility = Visibility.Hidden;
                gamesPlayed2.Visibility = Visibility.Hidden;
            }
            //3
            if (championAverages[3].ChampionId != 0)
            {
                championImg3.Source = new BitmapImage(new Uri("http://ddragon.leagueoflegends.com/cdn/11.1.1/img/champion/" + championNamesArray[3] + ".png"));
                champion3.Text = championNamesArray[3];
                creepScore3.Text = "CS " + championAverages[3].CreepScoreAverage.ToString();
                kda3.Text = Math.Round((championAverages[3].KillAverage + championAverages[3].AssistAverage) / championAverages[3].DeathAverage, 1).ToString() + " KDA";
                fullKD3.Text = championAverages[3].KillAverage.ToString() + " / " + championAverages[3].DeathAverage.ToString() + " / " + championAverages[3].AssistAverage.ToString();
                wr3.Text = championAverages[3].Winrate.ToString() + " %";
                gamesPlayed3.Text = (championAverages[3].Wins + championAverages[3].Losses).ToString() + " games";
            }
            else
            {
                mostPlayedBorder3.Visibility = Visibility.Hidden;
                championImg3.Visibility = Visibility.Hidden;
                champion3.Visibility = Visibility.Hidden;
                creepScore3.Visibility = Visibility.Hidden;
                kda3.Visibility = Visibility.Hidden;
                fullKD3.Visibility = Visibility.Hidden;
                wr3.Visibility = Visibility.Hidden;
                gamesPlayed3.Visibility = Visibility.Hidden;
            }
            //4
            if (championAverages[4].ChampionId != 0)
            {
                championImg4.Source = new BitmapImage(new Uri("http://ddragon.leagueoflegends.com/cdn/11.1.1/img/champion/" + championNamesArray[4] + ".png"));
                champion4.Text = championNamesArray[4];
                creepScore4.Text = "CS " + championAverages[4].CreepScoreAverage.ToString();
                kda4.Text = Math.Round((championAverages[4].KillAverage + championAverages[4].AssistAverage) / championAverages[4].DeathAverage, 1).ToString() + " KDA";
                fullKD4.Text = championAverages[4].KillAverage.ToString() + " / " + championAverages[4].DeathAverage.ToString() + " / " + championAverages[4].AssistAverage.ToString();
                wr4.Text = championAverages[4].Winrate.ToString() + " %";
                gamesPlayed4.Text = (championAverages[4].Wins + championAverages[4].Losses).ToString() + " games";
            }
            else
            {
                mostPlayedBorder4.Visibility = Visibility.Hidden;
                championImg4.Visibility = Visibility.Hidden;
                champion4.Visibility = Visibility.Hidden;
                creepScore4.Visibility = Visibility.Hidden;
                kda4.Visibility = Visibility.Hidden;
                fullKD4.Visibility = Visibility.Hidden;
                wr4.Visibility = Visibility.Hidden;
                gamesPlayed4.Visibility = Visibility.Hidden;
            }
                

            /* championImg0
             * champion0
             * creepScore0
             * kda0
             * fullKD0
             * wr0
             * gamesPlayed0
             */
        }

        private string[] ChampionNames(int[] championIds)
        {
            List<ChampionsModel> championList = ChampionsProcessor.GetChampions();
            string[] namesArray = new string[championIds.Length];
            for(int i = 0; i<championIds.Length; i++)
            {
                for(int j=0; j< championList.Count; j++)
                {
                    if (championIds[i] == Int16.Parse(championList[j].key))
                    {
                        namesArray[i] = championList[j].id;
                        break;
                    }
                }
            }
            return namesArray;
        }
        private void SetName(string name)
        {
            profileName.Text = name;
        }
        private void SetRank(string rank, int leaguePoints, string tier)
        {
            if (rank != null)
            {
                StringBuilder fullRank = new StringBuilder();
                fullRank.Append(tier);
                fullRank.Append(" ");
                fullRank.Append(rank);
                fullRank.Append(" ");
                fullRank.Append(leaguePoints.ToString());
                fullRank.Append("LP");
                profileRank.Text = fullRank.ToString();
            }
            else profileRank.Text = tier;
        }
        private void SetLevel(string level)
        {
            profileLevel.Text = level;
        }
        private void SetIcon(string iconId)
        {
            StringBuilder uri = new StringBuilder("http://ddragon.leagueoflegends.com/cdn/11.1.1/img/profileicon/");
            uri.Append(iconId);
            uri.Append(".png");
            profileIcon.Source = new BitmapImage(new Uri(uri.ToString()));
        }
    }
}
