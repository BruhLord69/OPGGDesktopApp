using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLib
{
    public class MatchesProcessor
    {
        public static async Task<MatchesModel> GetMatchesAsync(string accountId, int weekNumber)
        {
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long seasonStartTime = 1610056800000;
            int msInAWeek = 604800000;
            StringBuilder url = new StringBuilder();
            url.Append("match/v4/matchlists/by-account/");
            url.Append(accountId);
            url.Append("?queue=420");
            long paginatedWeekStartTime = (seasonStartTime + (msInAWeek * weekNumber));

                url.Append("&endTime=");
                if(currentTime > paginatedWeekStartTime + msInAWeek)
                {
                    url.Append(seasonStartTime + msInAWeek);
                }
                else
                {
                    url.Append(currentTime);
                }
                url.Append("&beginTime=");
                url.Append(paginatedWeekStartTime);
            Console.WriteLine(url.ToString());
            using (HttpResponseMessage response = await ApiHelper.client.GetAsync(url.ToString()))
            {
                if (response.IsSuccessStatusCode)
                {
                    MatchesModel matches = await response.Content.ReadAsAsync<MatchesModel>();
                    return matches;
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine(response.RequestMessage);
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task<MatchesModel> GetLatestMatchesAsync(string accountId, int beginIndex, int endIndex)
        {
            StringBuilder url = new StringBuilder();
            url.Append("match/v4/matchlists/by-account/");
            url.Append(accountId);
            url.Append("?queue=420");
            url.Append("&endIndex=");
            url.Append(endIndex);
            url.Append("&beginIndex=");
            url.Append(beginIndex);
            
            Console.WriteLine(url.ToString());
            using (HttpResponseMessage response = await ApiHelper.client.GetAsync(url.ToString()))
            {
                if (response.IsSuccessStatusCode)
                {
                    MatchesModel matches = await response.Content.ReadAsAsync<MatchesModel>();
                    return matches;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine(response.RequestMessage);
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static int WeekCount()
        {
            long currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            long beginTime = 1610056800000;
            int msInAWeek = 604800000;
            double weeksFloatingPoint = ((double)currentTime - (double)beginTime) / (double)msInAWeek;
            int weeks = (int)Math.Ceiling(weeksFloatingPoint);
            return weeks;
        }
    }
}
