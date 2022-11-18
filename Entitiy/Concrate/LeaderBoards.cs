using System;
using Core.Entities;
using DocumentFormat.OpenXml.Vml;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entitiy.Concrate
{
    public class LeaderBoards : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId user_id { get; set; }
        public int Rank { get; set; }
        public int TotalPoints { get; set; }
        public DateTime CreateDate { get; set; }
        public string Award { get; set; }
    }
}

