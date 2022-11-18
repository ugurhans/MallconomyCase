using System;
using Core.DataAccess;
using Entitiy.Concrate;
using Entitiy.Dtos;
using MongoDB.Bson;

namespace DataAccess.Abstract
{
    public interface ILeaderBoardDao : IMongoDbRepository<LeaderBoards>
    {
        List<UserAwardDto> GetUserAwarDto(ObjectId userId);
    }
}

