using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MoodleFetchBotAPI.Models.Collections
{
    public partial class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("discordId")]
        public string discordId { get; set; } = null!;

        [BsonElement("moodleDomain")]
        public string moodleDomain { get; set; } = null!;

        [BsonElement("moodleToken")]
        public string moodleToken { get; set; } = null!;
    }
}
