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
    }
}
