using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using SteamSearchApi.Models.Attributes;
using MongoDB.Bson;

namespace SteamSearchApi.Models
{
    [MongoDBCollectionName("QueryHistory")]
    [MongoDBDatabaseName("SteamSearch")]

    public class Query
    {

        public ObjectId _id { get; set; }

        public DateTime DateCreated { get; set; }
        public string SteamId { get; set; }
        public string IpAddress { get; set; }
    }
}