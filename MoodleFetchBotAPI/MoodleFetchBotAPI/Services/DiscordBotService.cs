using Discord;
using Discord.WebSocket;

namespace MoodleFetchBotAPI.Services
{
    public class DiscordBotService
    {
        private DiscordSocketClient _client;
        string token = Environment.GetEnvironmentVariable("DiscordBotToken");


        public Task RunBot() => MainAsync();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}