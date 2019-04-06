using Newtonsoft.Json;
using SteamSearchApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamSearchApi.Models.Responses
{
    public partial class HttpFriendListResponse
    {
        [JsonProperty("friendslist")]
        public Friendslist Friendslist { get; set; }
    }

    public partial class Friendslist
    {
        [JsonProperty("friends")]
        public Friend[] Friends { get; set; }
    }

    public partial class Friend
    {
        [JsonProperty("steamid")]
        public string Steamid { get; set; }

        [JsonProperty("friend_since")]
        private long tsFriendSince { get; set; }


        [JsonProperty("friendSince")]
        public DateTime FriendSince { get
            {
                return Util.ConvertFromUnixTimeStamp(tsFriendSince);
            }
        }


    }
}