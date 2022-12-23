namespace MoodleFetchBotAPI.Models.ReturnTypes.Moodle
{
    public class ModuleContentsInfo
    {
        public int filecount { get; set; }
        public int filesize { get; set; }
        public int lastmodified { get; set; }
        public string[] mimetypes { get; set; }
        public string repositorytype { get; set; }
    }
}
