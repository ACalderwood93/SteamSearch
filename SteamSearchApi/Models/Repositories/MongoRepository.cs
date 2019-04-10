using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Driver;
using System.Configuration;
using SteamSearchApi.Helpers;
using SteamSearchApi.Models.Attributes;

namespace SteamSearchApi.Models.Repositories
{
    public class MongoRepository<T>
    {
        private IMongoDatabase _database { get; set; }
        private IMongoClient _client { get; set; }
        private IMongoCollection<T> _collection { get; set; }

        private string dbName { get; set; }
        private string collectionName { get; set; }

        public MongoRepository(string connectionString)
        {
            _client = new MongoClient(connectionString);
            dbName = (Util.GetAttribute<Query>(typeof(MongoDBDatabaseName)) as MongoDBDatabaseName).Name;
            collectionName = (Util.GetAttribute<Query>(typeof(MongoDBCollectionName)) as MongoDBCollectionName).Name;
            _database = _client.GetDatabase(dbName);
            _collection = _database.GetCollection<T>(collectionName);
        }
        public MongoRepository() : this(ConfigurationManager.AppSettings["MongoDbConnectionString"])
        {

        }


        public List<T> GetAll()
        {
            return _collection.AsQueryable<T>().ToList();
        }
        public List<U> GetAll<U>()
        {
            var collection = _GetCollection<U>();
            return collection.AsQueryable().ToList();
        }


        public bool Insert(T obj)
        {
            try
            {
                _collection.InsertOne(obj);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Insert<U>(U obj)
        {
            try
            {

                var collection = _GetCollection<U>();
                collection.InsertOne(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<T> Get(Func<T, bool> selector)
        {
            var items = _collection.AsQueryable<T>().Where(selector).ToList();
            return items;
        }
        public List<U> Get<U>(Func<U, bool> selector)
        {

            var collection = _GetCollection<U>();
            var result = collection.AsQueryable().Where(selector).ToList();
            return result;
        }

        public bool Exists(Func<T, bool> selector)
        {

            return _collection.AsQueryable().Any(selector);
        }
        public bool Exists<U>(Func<U, bool> selector)
        {
            var collection = _GetCollection<U>();
            return collection.AsQueryable().Any(selector);

        }

        private IMongoCollection<U> _GetCollection<U>()
        {
            var dbName = (Util.GetAttribute<U>(typeof(MongoDBDatabaseName)) as MongoDBDatabaseName).Name;
            var cName = (Util.GetAttribute<U>(typeof(MongoDBCollectionName)) as MongoDBCollectionName).Name;
            var db = _client.GetDatabase(dbName);
            var collection = db.GetCollection<U>(cName);
            return collection;
        }
    }
}
