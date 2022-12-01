namespace MoodleFetchBotAPI.Models
{
    public class Course
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string shortname { get; set; }
        public string idnumber { get; set; }
        public string summary { get; set; }
        public string fullnamedisplay { get; set; }
        public string viewurl { get; set; }
        public string coursecategory { get; set; }

        public enum Classification
        {
            past=0,
            inprogress=1,
            future=2
        }
    }
}
