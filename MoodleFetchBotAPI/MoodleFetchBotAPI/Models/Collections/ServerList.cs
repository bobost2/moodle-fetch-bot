using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace MoodleFetchBotAPI.Models.Collections
{
    public partial class ServerList
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userId")]
        public string? userId { get; set; }

        [BsonElement("guildId")]
        public string guildId { get; set; } = null!;

        [BsonElement("courseIds")]
        public int[] courseIds { get; set; }
    }
}
