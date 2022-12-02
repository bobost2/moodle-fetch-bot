using Discord.Net.Queue;
using Discord.Net.Rest;
using Microsoft.AspNetCore.Mvc;
using MoodleFetchBotAPI.Models;
using MoodleFetchBotAPI.Services;
using Newtonsoft.Json;
using RestSharp;
using System.ComponentModel;

namespace MoodleFetchBotAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IConfiguration Configuration;
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        [Route("TestRequest")]
        public IActionResult TestRequest(string username, string password)
        {
            MoodleService moodleService = new MoodleService();

            string website = null;
            string token = null;


            //return Json(new { output = moodleService.FetchCourses(website, token, Course.Classification.inprogress) });
            //return Json(new { output = moodleService.FetchCourseData(website, token, 0) });
            //return Json(new { output = moodleService.FetchAssignments(website, token, 0) });
            //return Json(new { output = moodleService.FetchForum(website, token, 0) });
            return Json(new { token = moodleService.GetToken(website, username, password) });

        }

        [HttpPost]
        [Route("AuthenticateDiscord")]
        public IActionResult AuthenticateDiscord(DiscordCode discordCode)
        {
            DiscordAPIService discordAPI = new DiscordAPIService(Configuration);
            var token = discordAPI.AuthenticateUser(discordCode.code);

            if (token != null)
                return Ok(new { userToken = token });
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("CheckUserMoodleToken")]
        public IActionResult CheckUserMoodleToken(DiscordSingleToken discordSingleToken)
        {
            var discordAPI = new DiscordAPIService(Configuration);
            var database = new DatabaseService();
            string userId = discordAPI.GetDiscordUserId(discordSingleToken.userToken);
            bool status = database.CheckIfUserExists(userId);

            return Ok(new { hasBeenRegistered = status });
        }

        [HttpPost]
        [Route("LinkMoodleAccount")]
        public IActionResult LinkMoodleAccount(MoodleCredentials moodleCredentials)
        {
            var moodleAPI = new MoodleService();
            var moodleToken = moodleAPI.GetToken(moodleCredentials.website, moodleCredentials.username, moodleCredentials.password);

            if (moodleToken == null)
                return Ok(new { verificationPassed = false });

            var discordAPI = new DiscordAPIService(Configuration);
            var database = new DatabaseService();

            string userId = discordAPI.GetDiscordUserId(moodleCredentials.userToken);
            database.LinkMoodleToDiscordId(userId, moodleToken, moodleCredentials.website);

            return Ok(new { verificationPassed = true });
        }
    }
}
