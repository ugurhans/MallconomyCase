using System;
using Core.DataAccess.MongoDb;
using Core.Entities.Concrate;
using DataAccess.Abstract;
using Entitiy.Concrate;
using Entitiy.Dtos;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace DataAccess.Concrate.MongoDb
{
    public class MongoLeaderBoardDal : MongoRepositoryBase<LeaderBoards>, ILeaderBoardDao
    {
        public MongoLeaderBoardDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }

        public List<UserAwardDto> GetUserAwarDto(ObjectId userId)
        {
            var result = GetAll(x => x.user_id == userId).Select(x => new UserAwardDto()
            {
                UserId = x.user_id,
                Awards = x.Award
            }).ToList();

            return result;
        }
    }
}

