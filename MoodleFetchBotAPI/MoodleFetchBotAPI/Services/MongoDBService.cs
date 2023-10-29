using MongoDB.Driver;
using MoodleFetchBotAPI.Models.Collections;
using MoodleFetchBotAPI.Models.ReturnTypes.Moodle;

namespace MoodleFetchBotAPI.Services
{
    // ReSharper disable once InconsistentNaming
    public class MongoDBService
    {
        private readonly IMongoCollection<Users> _usersCollection;
        private readonly IMongoCollection<ServerList> _serverListCollection;

        public MongoDBService()
        {
            MongoClient client = new MongoClient(Environment.GetEnvironmentVariable("FetchBotMongoConnectionString"));

            var database = client.GetDatabase("MoodleFetchBot");
            var userCollection = database.GetCollection<Users>("Users");
            var serverListCollection = database.GetCollection<ServerList>("ServerList");

            _usersCollection = userCollection;
            _serverListCollection = serverListCollection;
        }

        public async Task<bool> CheckIfUserExists(string discordId)
        {
            var user = await _usersCollection.Find(x => x.discordId == discordId).SingleOrDefaultAsync();

            if ( user != null )
                return true;

            return false;
        }

        public async Task LinkMoodleToDiscordId(string discordId, string moodleToken, string website)
        {
            var doesUserExist = await CheckIfUserExists(discordId);

            if (!doesUserExist)
            {
                var user = new Users
                {
                    discordId = discordId,
                    moodleDomain = website,
                    moodleToken = moodleToken
                };

                await _usersCollection.InsertOneAsync(user);
            }
        }

        public async Task<bool> CheckIfServerRecordExists(string guildId)
        {
            var serverCount = await _serverListCollection.Find(x => x.guildId == guildId).CountDocumentsAsync();
            return serverCount > 0;
        }

        public async Task<MoodleInfo?> FetchUserData(string discordId)
        {
            var userRecord = await _usersCollection.Find(x => x.discordId == discordId).SingleOrDefaultAsync();
            
            if (userRecord != null)
            {
                return new MoodleInfo
                {
                    token = userRecord.moodleToken,
                    website = userRecord.moodleDomain
                };
            }

            return null;
        }

        public async Task LinkGuildToCourse(string userId, string guildId, int[] courses)
        {
            var user = await _usersCollection.Find(x => x.discordId == userId).SingleOrDefaultAsync();

            if (user == null) return;

            var courseEntry = new ServerList
            {
                courseIds = courses,
                userId = user.Id,
                guildId = guildId,
            };

            await _serverListCollection.InsertOneAsync(courseEntry);
        }

        public async Task<List<int>?> ReturnLinkedCourses(string userId, string guildId)
        {
            List<int> courseIds = new List<int>();
            var user = await _usersCollection.Find(x => x.discordId == userId).SingleOrDefaultAsync();
            
            if (user == null) return null;

            var courses = await _serverListCollection.Find(x => x.userId == user.Id && x.guildId == guildId).SingleOrDefaultAsync();

            if (courses != null)
            {
                courseIds = courses.courseIds.ToList();
            }

            return courseIds;
        }
    }
}
