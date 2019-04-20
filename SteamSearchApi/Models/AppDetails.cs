using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using SteamSearchApi.Models.Interfaces;
using SteamSearchApi.Models.Attributes;
using Newtonsoft;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Attributes;

namespace SteamSearchApi.Models
{
    [MongoDBCollectionName("Games")]
    [MongoDBDatabaseName("SteamSearch")]
    public class AppDetails : IMongoObject
    {
        public ObjectId _id { get; set; }
        [JsonProperty("type")]
        public static string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("steam_appid")]
        public long AppId { get; set; }
       // [JsonProperty("categories")]
       // public List<Catagory> Categories { get; set; }
        [JsonProperty("detailed_description")]
        public string DetailedDescription { get; set; }
        [JsonProperty("short_description")]
        public string ShortDescription { get; set; }
        [JsonProperty("about_the_game")]
        public string AboutTheGame { get; set; }


    }



    [MongoDBCollectionName("Categories")]
    [MongoDBDatabaseName("SteamSearch")]
    public class Catagory : ICatagory,IMongoObject
    {

        [BsonId]
        public ObjectId _id { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

    }


}