namespace MoodleFetchBotAPI.Models.ReturnTypes.Moodle
{
    public class CourseModule
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int instance { get; set; }
        public int contextid { get; set; }
        public string modname { get; set; }
        public string modplural { get; set; }
        public string onclick { get; set; }
        public string afterlink { get; set; }
        public string customdata { get; set; }
        public List<ModuleDates> dates { get; set; }
        public List<ModuleContents> contents { get; set; }
        public ModuleContentsInfo contentsinfo { get; set; }
    }
}
