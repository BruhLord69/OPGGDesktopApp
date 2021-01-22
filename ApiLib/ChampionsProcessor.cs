using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLib
{
    public class ChampionsProcessor
    {
        public static List<ChampionsModel> GetChampions()
        {
            //string filePath
            using (StreamReader r = new StreamReader(@"champions.json"))
            {
                string json = r.ReadToEnd();
                List<ChampionsModel> items = JsonConvert.DeserializeObject<List<ChampionsModel>>(json);
                return items;
            }
        }
    }
}
