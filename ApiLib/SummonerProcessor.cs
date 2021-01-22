using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLib
{
    public class SummonerProcessor
    {
        public async static Task<SummonerModel> GetSummonerAsync(string username)
        {
            StringBuilder url = new StringBuilder();
            url.Append("summoner/v4/summoners/by-name/");
            url.Append(username);
            Console.WriteLine(url.ToString());
            using (HttpResponseMessage response = await ApiHelper.client.GetAsync(url.ToString()))
            {
                if (response.IsSuccessStatusCode)
                {
                    SummonerModel summoner = await response.Content.ReadAsAsync<SummonerModel>();
                    return summoner;
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
