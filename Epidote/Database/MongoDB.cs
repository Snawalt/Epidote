using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Epidote.MongoDB
{
    public static class MongoDBSettings
    {
        private static readonly string ConnectionString = "mongodb+srv://snawalt:zxpTpVjP1Bx3KEAj@epidote.uagcn2j.mongodb.net/?retryWrites=true&w=majority";
        private static readonly string DatabaseName = "Epidote-Dataset";

        // Collections
        private static readonly string UsersCollection = "users";
        private static readonly string VersionCollection = "version";
        private static readonly string GlobalCounterCollection = "globalCounter";

        // Fields
        private static readonly string VersionField = "_version";
        private static readonly string CounterField = "_globalLaunches";
        private static readonly string TotalTimeSpentField = "_hoursPlayed";
        private static readonly string UserIdField = "_userId";
        private static readonly string BadgeField = "_badge";
        private static readonly string hwidField = "_hwid";

        static string userId = Guid.NewGuid().ToString();

        private static readonly Dictionary<int, string> Badges = new Dictionary<int, string>
        {
            { 0, "Test" },
            { 1, "Rookie" },
            { 2, "Dookie" },
            { 10, "Iron" },
            { 30, "Bronze" },
            { 50, "Silver" },
            { 75, "Gold" },
            { 100, "Platinum" },
            { 150, "Diamond" },
            { 250, "Conqueror" },
            { 350, "Immortal" },
            { 500, "Enigma" }
        };

        private static DateTime _startTime;
        private static MongoClient _client;
        private static IMongoDatabase _database;
        private static IMongoCollection<BsonDocument> _usersCollection;
        private static IMongoCollection<BsonDocument> _versionCollection;
        private static IMongoCollection<BsonDocument> _globalCounterCollection;

        public static void UploadPlayerData()
        {
            if (_database == null) return;

            try
            {
                var existingPlayer = _usersCollection.Find(Builders<BsonDocument>.Filter.Eq("_users", Epidote.Program._username)).FirstOrDefault();
                if (existingPlayer != null)
                {
                    // Calculate the number of times the program has been started
                    int timesStarted = existingPlayer["_timesStarted"].AsInt32 + 1;
                    // Calculate the total time spent in the program
                    double totalTimeSpent = Math.Round(existingPlayer[TotalTimeSpentField].AsDouble + (DateTime.Now - _startTime).TotalHours, 3);

                    // Update the existing document
                    _usersCollection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_users", Epidote.Program._username),
                        Builders<BsonDocument>.Update.Combine(
                            Builders<BsonDocument>.Update.Set("_timesStarted", timesStarted),
                            Builders<BsonDocument>.Update.Set(TotalTimeSpentField, totalTimeSpent)));

                    // Check if the player has earned a new badge
                    string badge = existingPlayer[BadgeField].AsString;
                    foreach (var entry in Badges)
                    {
                        if (totalTimeSpent >= entry.Key && badge != entry.Value)
                        {
                            badge = entry.Value;
                            _usersCollection.UpdateOne(Builders<BsonDocument>.Filter.Eq("_users", Epidote.Program._username),
                                Builders<BsonDocument>.Update.Set(BadgeField, badge));
                        }
                    }

                    return;
                }

                var document = new BsonDocument
                    {
                        { UserIdField, userId },
                        { "_users", Epidote.Program._username },
                        { "_hwid", Epidote.Protection.HardwareInfo.GetHardwareInformationHash() },
                        { "_timesStarted", 1 },
                        { TotalTimeSpentField, Math.Round((DateTime.Now - _startTime).TotalHours, 2) },
                        { BadgeField, "" }
                    };
                _usersCollection.InsertOne(document);
            }
            catch (Exception ex)
            {
                ExceptionLogger.Write(LogEvent.Error, $"error at: {ex.ToString()}", false);
            }
        }

        public static void IncrementCounter()
        {
            Connect();

            var filter = Builders<BsonDocument>.Filter.Empty;
            var update = Builders<BsonDocument>.Update.Inc(CounterField, 1);
            var options = new FindOneAndUpdateOptions<BsonDocument> { IsUpsert = true };
            _globalCounterCollection.FindOneAndUpdate(filter, update, options);
        }

        public static void Connect()
        {
            try
            {
                _client = new MongoClient(ConnectionString);
                _database = _client.GetDatabase(DatabaseName);
                _usersCollection = _database.GetCollection<BsonDocument>(UsersCollection);
                _versionCollection = _database.GetCollection<BsonDocument>(VersionCollection);
                _globalCounterCollection = _database.GetCollection<BsonDocument>(GlobalCounterCollection);
                _startTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                ExceptionLogger.Write(LogEvent.Error, $"error at: {ex.ToString()}", false);
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
    }
}
