using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamSearchApi.Models.Responses
{
    public partial class HttpGetOwnedGamesResponse
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("game_count")]
        public long GameCount { get; set; }

    }
}