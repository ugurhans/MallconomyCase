using System;
using Core.DataAccess.MongoDb;
using Core.Entities.Concrate;
using DataAccess.Abstract;
using Entitiy.Concrate;
using Microsoft.Extensions.Options;

namespace DataAccess.Concrate.MongoDb
{
    public class MongoPointDal : MongoRepositoryBase<Points>, IPointDao
    {
        public MongoPointDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}

