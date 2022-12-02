using System;
using System.Collections.Generic;

namespace MoodleFetchBotAPI.Models.Tables
{
    public partial class UserTable
    {
        public int Id { get; set; }
        public string DiscordId { get; set; } = null!;
        public string MoodleDomain { get; set; } = null!;
        public string MoodleToken { get; set; } = null!;
    }
}
