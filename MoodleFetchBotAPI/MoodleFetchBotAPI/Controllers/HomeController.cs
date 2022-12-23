using Discord.Net.Queue;
using Discord.Net.Rest;
using Microsoft.AspNetCore.Mvc;
using MoodleFetchBotAPI.Models.RequestTypes;
using MoodleFetchBotAPI.Models.ReturnTypes.Discord;
using MoodleFetchBotAPI.Models.ReturnTypes.Moodle;
using MoodleFetchBotAPI.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        /*
        [HttpGet]
        [Route("TestRequest")]
        public IActionResult TestRequest()
        {
            //MoodleService moodleService = new MoodleService();

            //string website = null;
            //string token = null;


            //return Json(new { output = moodleService.FetchCourses(website, token, Course.Classification.inprogress) });
            //return Json(new { output = moodleService.FetchCourseData(website, token, 0) });
            //return Json(new { output = moodleService.FetchAssignments(website, token, 0) });
            //return Json(new { output = moodleService.FetchForum(website, token, 0) });
            //return Json(new { token = moodleService.GetToken(website, username, password) });
            var guilds = DiscordBotService.GetGulds();
            return Ok(new { guilds = guilds });
        }*/

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

        [HttpPost]
        [Route("FetchBotServersForUser")]
        public async Task<IActionResult> FetchBotServersForUser(DiscordSingleToken discordSingleToken)
        {
            var discordAPI = new DiscordAPIService(Configuration);
            var databaseAPI = new DatabaseService();

            var guilds = await DiscordBotService.GetGulds();
            var userId = discordAPI.GetDiscordUserId(discordSingleToken.userToken);

            var returnedGuilds = new List<SimpleDiscordGuild>();

            foreach (var guild in guilds)
            {
                //If the bot is added by a non-owner, it will cause issues
                if (guild.OwnerId.ToString() == userId)
                {
                    returnedGuilds.Add(new SimpleDiscordGuild
                    {
                        id = guild.Id.ToString(),
                        name = guild.Name,
                        iconUrl = guild.IconUrl,
                        configured = databaseAPI.CheckIfServerRecordExists(guild.Id.ToString())
                    });
                }
                else
                {
                    var users = guild.Users.ToList();
                    //The list isn't always refreshed, sometimes returns 0 for some
                    //unknown reasons (I have "SERVER MEMBERS INTENT" on in dev portal)

                    foreach (var user in users)
                    {
                        //Console.WriteLine($"Found '{user.Username}' in '{guild.Name}'");
                        if (user.GuildPermissions.ManageGuild == true && user.Id.ToString() == userId)
                        {
                            returnedGuilds.Add(new SimpleDiscordGuild
                            {
                                id = guild.Id.ToString(),
                                name = guild.Name,
                                iconUrl = guild.IconUrl,
                                configured = databaseAPI.CheckIfServerRecordExists(guild.Id.ToString())
                            });
                        }
                    }
                }
            }

            return Ok(new { guilds = returnedGuilds });
        }

        [HttpPost]
        [Route("FetchMoodleCourses")]
        public IActionResult FetchMoodleCourses(DiscordSingleToken discordSingleToken)
        {
            MoodleService moodleService = new MoodleService();
            DiscordAPIService discordAPI = new DiscordAPIService(Configuration);
            var databaseService = new DatabaseService();

            List<Course> courses = new List<Course>();
            string userId = discordAPI.GetDiscordUserId(discordSingleToken.userToken);

            var userInfo = databaseService.FetchUserData(userId);

            string website = userInfo.website;
            string token = userInfo.token;

            courses.AddRange(moodleService.FetchCourses(website, token, Course.Classification.past));
            courses.AddRange(moodleService.FetchCourses(website, token, Course.Classification.inprogress));
            courses.AddRange(moodleService.FetchCourses(website, token, Course.Classification.future));

            return Ok(new { courses = courses });
        }

        [HttpPost]
        [Route("LinkMoodleCoursesToGuild")]
        public IActionResult LinkMoodleCoursesToGuild(TokenCourseRequest tokenCourseRequest)
        {
            DiscordAPIService discordAPI = new DiscordAPIService(Configuration);
            var databaseService = new DatabaseService();

            var userId = discordAPI.GetDiscordUserId(tokenCourseRequest.userToken);

            if (userId == null) return Unauthorized();

            databaseService.LinkGuildToCourse(userId, tokenCourseRequest.guildId, tokenCourseRequest.courses);

            return Ok(new { response = "You are not supposed to look here." });
        }

        [HttpPost]
        [Route("FetchCoursesByGuild")]
        public IActionResult FetchCoursesByGuild(TokenGuildRequest tokenGuildRequest)
        {
            DiscordAPIService discordAPI = new DiscordAPIService(Configuration);
            MoodleService moodleService = new MoodleService();
            var databaseService = new DatabaseService();

            var userId = discordAPI.GetDiscordUserId(tokenGuildRequest.userToken);

            if (userId == null) return Unauthorized();

            var courseIds = databaseService.ReturnLinkedCourses(userId, tokenGuildRequest.guildId);


            List<Course> courses = new List<Course>();
            var userInfo = databaseService.FetchUserData(userId);
            courses.AddRange(moodleService.FetchCourses(userInfo.website, userInfo.token, Course.Classification.past));
            courses.AddRange(moodleService.FetchCourses(userInfo.website, userInfo.token, Course.Classification.inprogress));
            courses.AddRange(moodleService.FetchCourses(userInfo.website, userInfo.token, Course.Classification.future));

            List<Course> coursesFilter = new List<Course>();
            foreach (var courseId in courseIds)
            {
                var result = courses.Where(x => x.id == courseId);
                if (result.Any())
                {
                    coursesFilter.Add(result.First());
                }
            }

            return Ok(new { courses = coursesFilter });
        }

        [HttpPost]
        [Route("FetchMoodleCourseModules")]
        public IActionResult FetchMoodleCourseModules(CourseModuleRequest courseModuleRequest)
        {
            DiscordAPIService discordAPI = new DiscordAPIService(Configuration);
            MoodleService moodleService = new MoodleService();
            var databaseService = new DatabaseService();

            var userId = discordAPI.GetDiscordUserId(courseModuleRequest.userToken);

            if (userId == null) return Unauthorized();

            var userInfo = databaseService.FetchUserData(userId);
            var courseDetails = moodleService.FetchCourseData(userInfo.website, userInfo.token, courseModuleRequest.courseId);

            List<ModuleReturn> moduleReturn = new List<ModuleReturn>();

            foreach(var course in courseDetails)
            {
                foreach(var module in course.modules)
                {
                    string mod = module.modname;
                    if (mod != "forum" && mod != "assign" && !moduleReturn.Where(x => x.modname == mod).Any())
                    {
                        moduleReturn.Add(new ModuleReturn
                        {
                            id = moduleReturn.Count,
                            modname = mod,
                            modplural = module.modplural,
                            selected = false
                        });
                    }
                }
            }

            return Ok(new {modules = moduleReturn});
        }
    }
}
