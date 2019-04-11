using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamSearchApi.Models.Responses {
    public partial class HttpRecentlyPlayedGames {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public partial class Response {
        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("games")]
        public List<Game> Games { get; set; }
    }


}