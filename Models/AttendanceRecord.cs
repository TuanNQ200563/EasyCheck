using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EasyCheck.Models
{
    public class AttendanceRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("CheckInTime")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CheckInTime { get; set; }

        [BsonElement("StudentCode")]
        public required string StudentCode { get; set; }
    }
}