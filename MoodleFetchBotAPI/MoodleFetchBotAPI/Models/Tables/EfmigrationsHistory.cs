﻿using System;
using System.Collections.Generic;

namespace MoodleFetchBotAPI.Models.Tables
{
    public partial class EfmigrationsHistory
    {
        public string MigrationId { get; set; } = null!;
        public string ProductVersion { get; set; } = null!;
    }
}
