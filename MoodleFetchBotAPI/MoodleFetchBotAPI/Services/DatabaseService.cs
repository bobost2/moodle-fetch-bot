using MoodleFetchBotAPI.Models;
using MoodleFetchBotAPI.Models.Tables;

namespace MoodleFetchBotAPI.Services
{
    public class DatabaseService
    {
        public bool CheckIfUserExists(string discordId)
        {
            MoodleFetchBotDBContext context = new MoodleFetchBotDBContext();
            var user = context.UserTables.Where(x => x.DiscordId == discordId);

            if(user.Count() != 0)
                return true;

            return false;
        }

        public void LinkMoodleToDiscordId(string discordId, string moodleToken, string website)
        {
            if (!CheckIfUserExists(discordId))
            {
                MoodleFetchBotDBContext context = new MoodleFetchBotDBContext();
                UserTable user = new UserTable
                {
                    DiscordId = discordId,
                    MoodleDomain = website,
                    MoodleToken = moodleToken
                };
                context.Add(user);
                context.SaveChanges();
            }
        }

        public bool CheckIfServerRecordExists(string guildId)
        {
            MoodleFetchBotDBContext context = new MoodleFetchBotDBContext();
            var server = context.ServerLists.Where(x => x.GuildId == guildId);
            return server.Count() > 0;
        }

        public MoodleInfo FetchUserData(string discordId)
        {
            MoodleFetchBotDBContext context = new MoodleFetchBotDBContext();
            var userRecords = context.UserTables.Where(x => x.DiscordId == discordId);

            if (userRecords.Any())
            {
                var userRecord = userRecords.First();
                return new MoodleInfo
                {
                    token = userRecord.MoodleToken,
                    website = userRecord.MoodleDomain
                };
            }

            return null;
        }

        public void LinkGuildToCourse(string userId, string guildId, int[] courses)
        {
            MoodleFetchBotDBContext context = new MoodleFetchBotDBContext();
            var usersDB = context.UserTables.Where(x => x.DiscordId == userId);

            if (!usersDB.Any()) return;

            var user = usersDB.First();

            foreach (int course in courses)
            {
                if(!context.ServerLists.Where(x => x.CourseId == course && x.GuildId == guildId).Any())
                {
                    ServerList courseEntry = new ServerList
                    {
                        CourseId = course,
                        UserId = user.Id,
                        GuildId = guildId,
                    };
                    context.Add(courseEntry);
                }
            }

            if(courses.Length > 0)
                context.SaveChanges();
        }
    }
}
