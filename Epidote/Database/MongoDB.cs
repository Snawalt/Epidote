using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epidote.Database
{
    public static class MongoDBSettings
    {
        //45tWlbaNS7jvJQMO
        private static readonly string Settings = "mongodb+srv://Snawalt:45tWlbaNS7jvJQMO@epidote.uagcn2j.mongodb.net/?retryWrites=true&w=majority";
        private static MongoClient _client;
        private static IMongoDatabase _database;
        private static IMongoCollection<BsonDocument> _collection;
        private static IMongoCollection<BsonDocument> _pCollection;
        private static List<BsonDocument> _result;
        private static List<BsonDocument> _pResult;

        public static void Connect()
        {
            try
            {
                _client = new MongoClient(Settings);
                _database = _client.GetDatabase("Epidote-Dataset");
                _collection = _database.GetCollection<BsonDocument>("users");
                _pCollection = _database.GetCollection<BsonDocument>("version");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while connecting to the database: " + e.Message);
            }
        }

        public static List<BsonDocument> GetUsers()
        {
            Connect();
            if (_database == null) return null;
            return _collection.Find(Builders<BsonDocument>.Filter.Empty).ToList();
        }

        public static List<BsonDocument> GetVersion()
        {
            Connect();
            if (_database == null) return null;
            return _pCollection.Find(Builders<BsonDocument>.Filter.Eq("_version", Epidote.Utils.VersionChecker.CurrentVersion)).ToList();
        }
    }
}
