namespace MoodleFetchBotAPI.Models.ReturnTypes.Moodle
{
    public class ModuleContents
    {
        public string type { get; set; }
        public string filename { get; set; }
        public string filepath { get; set; }
        public int? filesize { get; set; }
        public string fileurl { get; set; }
        public int? timecreated { get; set; }
        public int? timemodified { get; set; }
        public int? sortorder { get; set; }
        public string mimetype { get; set; }
        public bool? isexternalfile { get; set; }
        public int? userid { get; set; }
        public string author { get; set; }
    }
}
