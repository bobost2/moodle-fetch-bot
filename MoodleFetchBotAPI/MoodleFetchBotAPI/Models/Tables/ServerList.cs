using System;
using System.Collections.Generic;

namespace MoodleFetchBotAPI.Models.Tables
{
    public partial class ServerList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GuildId { get; set; } = null!;
        public int CourseId { get; set; }

        public virtual UserTable User { get; set; } = null!;
    }
}
