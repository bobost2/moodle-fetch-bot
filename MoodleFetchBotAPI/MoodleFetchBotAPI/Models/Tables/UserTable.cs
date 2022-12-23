using System;
using System.Collections.Generic;

namespace MoodleFetchBotAPI.Models.Tables
{
    public partial class UserTable
    {
        public UserTable()
        {
            ServerLists = new HashSet<ServerList>();
        }

        public int Id { get; set; }
        public string DiscordId { get; set; } = null!;
        public string MoodleDomain { get; set; } = null!;
        public string MoodleToken { get; set; } = null!;

        public virtual ICollection<ServerList> ServerLists { get; set; }
    }
}
