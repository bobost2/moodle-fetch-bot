namespace MoodleFetchBotAPI.Models.ReturnTypes.Discord
{
    public class SimpleDiscordGuild
    {
        public string id { get; set; }
        public string name { get; set; }
        public string iconUrl { get; set; }
        public bool configured { get; set; }
    }
}
