using MoodleFetchBotAPI.Models.ReturnTypes.Discord;
using RestSharp;
using System.Text.Json;

namespace MoodleFetchBotAPI.Services
{
    public class DiscordAPIService
    {
        private readonly IConfiguration Configuration;
        public DiscordAPIService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string AuthenticateUser(string code)
        {
            string clientId = Environment.GetEnvironmentVariable("DiscordClientId");
            string client_secret = Environment.GetEnvironmentVariable("DiscordClientSecret");
            string redirect_uri = Configuration["RedirectURL"];

            var client = new RestClient("https://discord.com/api/oauth2/token");
            var request = new RestRequest("",Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("code", code);
            request.AddParameter("client_id", clientId);
            request.AddParameter("client_secret", client_secret);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("redirect_uri", redirect_uri);
            RestResponse response = client.Execute(request);

            return JsonSerializer.Deserialize<DiscordToken>(response.Content).access_token;
        }

        public string GetDiscordUserId(string token)
        {
            var client = new RestClient("https://discord.com/api/users/@me");
            var request = new RestRequest("", Method.Get);
            request.AddHeader("Authorization", "Bearer " + token);
            RestResponse response = client.Execute(request);
            return JsonSerializer.Deserialize<DiscordUser>(response.Content).id;
        }
    }
}
