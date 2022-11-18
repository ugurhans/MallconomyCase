using System;
using Core.DataAccess;
using Core.Entities.Concrate;

namespace DataAccess.Abstract
{
    public interface IUserDao : IMongoDbRepository<User>
    {
    }
}

