namespace MoodleFetchBotAPI.Models
{
    public class Assignment
    {
        public int id { get; set; }
        public int cmid { get; set; }
        public string name { get; set; }
        public int duedate { get; set; }
        public int allowsubmissionsfromdate { get; set; }
        public int timemodified { get; set; }
        public int cutoffdate { get; set; }
        public int maxattempts { get; set; }
        public string intro { get; set; }
        public int introformat { get; set; }
        public List<AssignmentFiles> introfiles { get; set; }
    }
}
