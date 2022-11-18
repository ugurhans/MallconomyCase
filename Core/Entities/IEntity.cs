using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace Core.Entities
{
    public interface IEntity
    {
        public ObjectId Id { get; set; }
    }
}
