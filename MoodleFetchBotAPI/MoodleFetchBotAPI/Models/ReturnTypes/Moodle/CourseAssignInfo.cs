namespace MoodleFetchBotAPI.Models.ReturnTypes.Moodle
{
    public class CourseAssignInfo
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string shortname { get; set; }
        public int timemodified { get; set; }
        public List<Assignment> assignments { get; set; }
    }
}
