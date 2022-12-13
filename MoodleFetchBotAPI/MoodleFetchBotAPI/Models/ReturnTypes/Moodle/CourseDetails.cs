namespace MoodleFetchBotAPI.Models.ReturnTypes.Moodle
{
    public class CourseDetails
    {
        public int id { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
        public int section { get; set; }
        public List<CourseModule> modules { get; set; }
    }
}
