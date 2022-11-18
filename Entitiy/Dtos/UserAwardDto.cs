using System;
using Core.Entities;
using MongoDB.Bson;

namespace Entitiy.Dtos
{
    public class UserAwardDto : IDto
    {
        public ObjectId UserId { get; set; }
        public string Awards { get; set; }
    }
}

