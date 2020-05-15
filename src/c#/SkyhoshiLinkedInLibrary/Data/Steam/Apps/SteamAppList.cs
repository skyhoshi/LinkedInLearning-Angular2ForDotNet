using System.Collections.Generic;

namespace SkyhoshiLinkedInLibrary.Data.Steam.Apps
{
 
    //[Newtonsoft.Json.JsonObject(id: "applist")]
    public class SteamAppList
    {
        [Newtonsoft.Json.JsonProperty(propertyName: "apps")]
        public List<SteamApp> Apps { get; set; }
    }
}