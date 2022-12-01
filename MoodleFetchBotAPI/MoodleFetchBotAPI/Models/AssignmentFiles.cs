namespace MoodleFetchBotAPI.Models
{
    public class AssignmentFiles
    {
        public string filename { get; set; }
        public string filepath { get; set; }
        public int filesize { get; set; }
        public string fileurl { get; set; }
        public int timemodified { get; set; }
        public string mimetype { get; set; }
        public bool isexternalfile { get; set; }
    }
}
