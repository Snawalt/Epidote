using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mongodb_connect
{
    public static class MongoDBSettings
    {
        private static readonly string ConnectionString = "mongodb+srv://snawalt:zxpTpVjP1Bx3KEAj@epidote.uagcn2j.mongodb.net/?retryWrites=true&w=majority";
        private static readonly string DatabaseName = "Epidote-Dataset";
        private static readonly string UsersCollection = "users";
        private static readonly string VersionCollection = "version";
        private static readonly string VersionField = "_version";

        private static MongoClient _client;
        private static IMongoDatabase _database;
        private static IMongoCollection<BsonDocument> _usersCollection;
        private static IMongoCollection<BsonDocument> _versionCollection;

        public static void Connect()
        {
            try
            {
                _client = new MongoClient(ConnectionString);
                _database = _client.GetDatabase(DatabaseName);
                _usersCollection = _database.GetCollection<BsonDocument>(UsersCollection);
                _versionCollection = _database.GetCollection<BsonDocument>(VersionCollection);
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
            return _usersCollection.Find(Builders<BsonDocument>.Filter.Empty).ToList();
        }

        public static string GetVersion()
        {
            Connect();
            if (_database == null) return null;

            var versionDocument = _versionCollection.Find(Builders<BsonDocument>.Filter.Empty).FirstOrDefault();
            if (versionDocument == null)
            {
                Console.WriteLine("No version found in the 'version' collection.");
                return null;
            }

            return versionDocument[VersionField].AsString;
        }
    }
}
