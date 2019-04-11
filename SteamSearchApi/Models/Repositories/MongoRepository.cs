using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB;
using MongoDB.Driver;

using System.Configuration;
using SteamSearchApi.Helpers;
using SteamSearchApi.Models.Attributes;
using System.Linq.Expressions;
using SteamSearchApi.Models.Interfaces;

namespace SteamSearchApi.Models.Repositories {
    public class MongoRepository<T> where T : IMongoObject {
        private IMongoDatabase _database { get; set; }
        private IMongoClient _client { get; set; }
        private IMongoCollection<T> _collection { get; set; }

        private string dbName { get; set; }
        private string collectionName { get; set; }

        public MongoRepository(string connectionString) {
            _client = new MongoClient(connectionString);
            dbName = (Util.GetAttribute<UserQuery>(typeof(MongoDBDatabaseName)) as MongoDBDatabaseName).Name;
            collectionName = (Util.GetAttribute<UserQuery>(typeof(MongoDBCollectionName)) as MongoDBCollectionName).Name;
            _database = _client.GetDatabase(dbName);
            _collection = _database.GetCollection<T>(collectionName);
        }
        public MongoRepository() : this(ConfigurationManager.AppSettings["MongoDbConnectionString"]) {

        }


        public List<T> GetAll() {
            return _collection.AsQueryable<T>().ToList();
        }
        public List<U> GetAll<U>() {
            var collection = _GetCollection<U>();
            return collection.AsQueryable().ToList();
        }


        public U Update<U>(U obj) where U : IMongoObject {

            var collection = _GetCollection<U>();
            collection.ReplaceOne(x => x._id == obj._id, obj);
            return obj;
        }
        public T Update(T obj) {


            _collection.ReplaceOne(x => x._id == obj._id, obj);
            return obj;
        }

        public bool Insert(T obj) {
            try {
                _collection.InsertOne(obj);
                return true;
            } catch {
                return false;
            }

        }
        public bool Insert<U>(U obj) {
            try {

                var collection = _GetCollection<U>();
                collection.InsertOne(obj);
                return true;
            } catch {
                return false;
            }
        }

        public List<T> Get(Func<T, bool> selector) {
            var items = _collection.AsQueryable<T>().Where(selector).ToList();
            return items;
        }
        public List<U> Get<U>(Func<U, bool> selector) {

            var collection = _GetCollection<U>();
            var result = collection.AsQueryable().Where(selector).ToList();
            return result;
        }

        public bool Exists(Func<T, bool> selector) {

            return _collection.AsQueryable().Any(selector);
        }
        public bool Exists<U>(Func<U, bool> selector) {
            var collection = _GetCollection<U>();
            return collection.AsQueryable().Any(selector);

        }

        public void RemoveAll(Expression<Func<T, bool>> selector) {
            _collection.DeleteMany(selector);
        }
        public void RemoveAll<U>(Expression<Func<U, bool>> selector) {

            var collection = _GetCollection<U>();
            collection.DeleteMany(selector);
        }

        public void Remove<U>(U obj) where U : IMongoObject {

            var collection = _GetCollection<U>();
            collection.DeleteOne<U>(x => x._id == obj._id);
        }
        public void Remove(T obj) {
            _collection.DeleteOne<T>(x => x._id == obj._id);
        }

        private IMongoCollection<U> _GetCollection<U>() {
            var dbName = (Util.GetAttribute<U>(typeof(MongoDBDatabaseName)) as MongoDBDatabaseName).Name;
            var cName = (Util.GetAttribute<U>(typeof(MongoDBCollectionName)) as MongoDBCollectionName).Name;
            var db = _client.GetDatabase(dbName);
            var collection = db.GetCollection<U>(cName);
            return collection;
        }
    }
}
