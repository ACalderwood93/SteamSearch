using Newtonsoft.Json;
using SteamSearchApi.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using SteamSearchApi.Models.Interfaces;

namespace SteamSearchApi.Models
{
    [MongoDBCollectionName("Players")]
    [MongoDBDatabaseName("SteamSearch")]
    public partial class Player : IMongoObject
    {
        public ObjectId _id { get; set; }


        [JsonProperty("steamid")]
        public string Steamid { get; set; }

        [JsonProperty("communityvisibilitystate")]
        public long Communityvisibilitystate { get; set; }

        [JsonProperty("profilestate")]
        public long Profilestate { get; set; }

        [JsonProperty("personaname")]
        public string Personaname { get; set; }

        [JsonProperty("lastlogoff")]
        public long Lastlogoff { get; set; }

        [JsonProperty("commentpermission")]
        public long Commentpermission { get; set; }

        [JsonProperty("profileurl")]
        public Uri Profileurl { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("avatarmedium")]
        public Uri Avatarmedium { get; set; }

        [JsonProperty("avatarfull")]
        public Uri Avatarfull { get; set; }

        [JsonProperty("personastate")]
        public long Personastate { get; set; }

        [JsonProperty("realname")]
        public string Realname { get; set; }

        [JsonProperty("primaryclanid")]
        public string Primaryclanid { get; set; }

        [JsonProperty("timecreated")]
        public long Timecreated { get; set; }

        [JsonProperty("personastateflags")]
        public long Personastateflags { get; set; }

        [JsonProperty("loccountrycode")]
        public string Loccountrycode { get; set; }

        [JsonProperty("locstatecode")]
        public string Locstatecode { get; set; }

        [JsonProperty("loccityid")]
        public long Loccityid { get; set; }

        
        public List<Game> OwnedGames { get; set; }
    }
}