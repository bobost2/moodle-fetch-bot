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
    [Route("[controller]")]
    public class HomeController : Controller
    {
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
    }
}
