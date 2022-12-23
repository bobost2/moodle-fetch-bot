namespace MoodleFetchBotAPI.Models.ReturnTypes.Moodle
{
    public class Discussions
    {
        public int id { get; set; }
        public string name { get; set; }
        public int timemodified { get; set; }
        public int usermodified { get; set; }
        public int discussion { get; set; }
        public int userid { get; set; }
        public int created { get; set; }
        public int modified { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string attachment { get; set; }
        public string userfullname { get; set; }
    }
}
