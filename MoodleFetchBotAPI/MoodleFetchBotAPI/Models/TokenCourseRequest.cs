﻿namespace MoodleFetchBotAPI.Models
{
    public class TokenCourseRequest
    {
        public string userToken { get; set; }
        public string guildId { get; set; }
        public int[] courses { get; set; }
    }
}
