using System;
using Core.Utilities.Results;
using Entitiy.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<UserAwardDto>> GetUsersAward(string userId);
    }
}

