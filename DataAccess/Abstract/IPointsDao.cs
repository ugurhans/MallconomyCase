using System;
using Core.DataAccess;
using Core.Entities.Concrate;
using Entitiy.Concrate;

namespace DataAccess.Abstract
{
    public interface IPointDao : IMongoDbRepository<Points>
    {
    }
}

