using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epidote.MongoDB
{
    public static class MongoDBSettings
    {
        private static readonly string ConnectionString = "mongodb+srv://snawalt:zxpTpVjP1Bx3KEAj@epidote.uagcn2j.mongodb.net/?retryWrites=true&w=majority";
        private static readonly string DatabaseName = "Epidote-Dataset";
        private static readonly string UsersCollection = "users";
        private static readonly string VersionCollection = "version";
        private static readonly string VersionField = "_version";
        public static DateTime _startTime = new DateTime();

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
                //
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
                return null;
            }

            return versionDocument[VersionField].AsString;
        }

        public static void UploadPlayerData()
        {
            Connect();
            if (_database == null) return;

            try
            {
                var existingPlayer = _usersCollection.Find(Builders<BsonDocument>.Filter.Eq("_users", Epidote.Program._username)).FirstOrDefault();
                if (existingPlayer != null)
                {
                    // Calculate the number of times the program has been started
                    int timesStarted = existingPlayer["timesStarted"].AsInt32 + 1;

                    // Calculate the amount of time spent in the program
                    TimeSpan timeSpent = TimeSpan.FromSeconds(existingPlayer["timeSpentInSeconds"].AsInt32 + (int)DateTime.Now.Subtract(_startTime).TotalSeconds);

                    // Update the existing document
                    _usersCollection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_users", Epidote.Program._username),
                        Builders<BsonDocument>.Update.Set("timesStarted", timesStarted).Set("timeSpentInSeconds", (int)timeSpent.TotalSeconds));
                    return;
                }

                // New player document
                var document = new BsonDocument
                {
                    { "_users", Epidote.Program._username },
                    { "_hwid", Epidote.Protection.HardwareInfo.GetHardwareInformationHash() },
                    { "timesStarted", 1 },
                    { "timeSpentInSeconds", 0 }
                };
                _usersCollection.InsertOne(document);
            }
            catch (Exception ex)
            {
                //
            }
        }


    }
}
