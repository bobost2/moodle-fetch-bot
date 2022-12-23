namespace MoodleFetchBotAPI.Models.RequestTypes
{
    public class CourseModuleRequest
    {
        public string userToken { get; set; }
        public string guildId { get; set; }
        public int courseId { get; set; }
    }
}
