using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DeviceMonitoring.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }
        public DateTime CreatedDt { get; set; }
        public DateTime UpdatedDt { get; set; }
    }
}