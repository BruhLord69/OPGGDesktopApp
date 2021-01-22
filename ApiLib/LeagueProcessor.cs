using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace ApiLib
{
    //Only returns solo queue data
    public class LeagueProcessor
    {
        public static async Task<LeagueModel> GetLeagueAsync(string id)
        {
            StringBuilder url = new StringBuilder();
            url.Append("league/v4/entries/by-summoner/");
            url.Append(id);
            Console.WriteLine(url.ToString());
            using (HttpResponseMessage response = await ApiHelper.client.GetAsync(url.ToString()))
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<LeagueModel> league = JsonConvert.DeserializeObject<List<LeagueModel>>(json);
                    if (league.Count > 0)
                    {
                        return league.First();
                    }
                    else
                    {
                        league.Add(new LeagueModel() { tier = "Unranked"});
                        return league.First();
                    }
                }
                else
                {
                    Console.WriteLine(response.RequestMessage);
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
