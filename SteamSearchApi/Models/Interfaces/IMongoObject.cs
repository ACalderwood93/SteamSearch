using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace SteamSearchApi.Models.Interfaces {
    public interface IMongoObject {

        ObjectId _id { get; set; }
    }
}
