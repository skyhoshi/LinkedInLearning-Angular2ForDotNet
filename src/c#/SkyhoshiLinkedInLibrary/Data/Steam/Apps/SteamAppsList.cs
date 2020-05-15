using System;
using System.Text;

namespace SkyhoshiLinkedInLibrary.Data.Steam.Apps
{
    public class SteamAppsList
    {

        public SteamAppsList(string strJson)
        {
            if (!string.IsNullOrWhiteSpace(strJson))
            {
                SteamAppsList steamAppsList = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamAppsList>(strJson);
                this.SteamAppList = steamAppsList.SteamAppList;
            }
        }

        [Newtonsoft.Json.JsonProperty(propertyName:"applist")]
        public SteamAppList SteamAppList { get; set; }
    }
}
