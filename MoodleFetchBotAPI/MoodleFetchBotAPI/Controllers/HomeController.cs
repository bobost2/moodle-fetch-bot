using Microsoft.AspNetCore.Mvc;

namespace MoodleFetchBotAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("TestRequest")]
        public IActionResult TestRequest()
        {
            return Json(new {test = "yes"});
        }
    }
}
