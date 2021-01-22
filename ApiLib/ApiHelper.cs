using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiLib
{
    public static class ApiHelper
    {
        public static HttpClient client { get; set; }

        public static void IntializeClient()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://euw1.api.riotgames.com/lol/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("X-Riot-Token", "RGAPI-e3d72ca5-ab0d-4bd6-963e-6e813f4e4ac0");
        }
    }
}
