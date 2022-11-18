using System;
using Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Entitiy.Concrate
{
    public class Points : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public bool approved { get; set; }
        public ObjectId user_id { get; set; }
        public int point { get; set; }
    }
}

