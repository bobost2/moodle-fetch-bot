using Discord;
using Discord.WebSocket;

namespace MoodleFetchBotAPI.Services
{
    public static class DiscordBotService
    {
        private static DiscordSocketClient _client;
        private static readonly string token = Environment.GetEnvironmentVariable("DiscordBotToken");


        public static Task RunBot() => MainAsync();

        public static async Task MainAsync()
        {
            _client = new DiscordSocketClient( new DiscordSocketConfig { AlwaysDownloadUsers = true });

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        public static async Task<List<SocketGuild>> GetGulds()
        {
            return _client.Guilds.ToList();
        }
    }
}