using System;
using Core.Utilities.Results;
using Entitiy.Concrate;
using Entitiy.Dtos;
using MongoDB.Bson;

namespace Business.Abstract
{
    public interface ILeaderBoardService
    {
        IResult CreateLeaderBoard();
        IDataResult<List<LeaderBoards>> GetAllWithMonthOrUserId(int? month, ObjectId? userId);
        IDataResult<List<UserAwardDto>> GetUsersAward(ObjectId userId);

    }
}
