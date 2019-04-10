using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteamSearchApi.Models.Attributes
{
    public class MongoDBDatabaseName : Attribute
    {
        public string Name { get; set; }

        public MongoDBDatabaseName(string name)
        {
            Name = name;
        }
    }
    public class MongoDBCollectionName : Attribute
    {
        public string Name { get; set; }

        public MongoDBCollectionName(string name)
        {
            Name = name;
        }
    }
}