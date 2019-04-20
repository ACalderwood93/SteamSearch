using SteamSearchApi.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamSearchApi.Models.Interfaces
{
    [MongoDBCollectionName("Categories")]
    [MongoDBDatabaseName("SteamSearch")]
    interface ICatagory
    {
        //int Id { get; set; }
        string Description { get; set; }
    }
}
