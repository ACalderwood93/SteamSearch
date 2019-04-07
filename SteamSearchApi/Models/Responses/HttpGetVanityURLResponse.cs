using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamSearchApi.Models.Responses
{
    public partial class HttpGetVanityUrlResponse
    {
        [JsonProperty("response")]
        public Response Response { get; set; }
    }

    public partial class Response
    {
        [JsonProperty("success")]
        public long Success { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("steamid")]
        public string SteamId { get; set; }
    }
}