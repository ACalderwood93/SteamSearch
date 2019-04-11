using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamSearchApi.Models {
    public partial class Game {
        [JsonProperty("appid")]
        public long Appid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("playtime_2weeks")]
        public long Playtime2Weeks { get; set; }

        [JsonProperty("playtime_forever")]
        public long PlaytimeForever { get; set; }

        [JsonProperty("img_icon_url")]
        public string ImgIconUrl { get; set; }

        [JsonProperty("img_logo_url")]
        public string ImgLogoUrl { get; set; }
    }
}