using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Entities.Concrate;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Core.DataAccess.MongoDb
{
    public class MongoRepositoryBase<TEntity> : IMongoDbRepository<TEntity>
      where TEntity : class, IEntity, new()
    {
        protected readonly IMongoCollection<TEntity> Collection;
        private readonly MongoDbSettings settings;

        protected MongoRepositoryBase(IOptions<MongoDbSettings> options)
        {
            this.settings = options.Value;
            var client = new MongoClient(this.settings.ConnectionString);
            var db = client.GetDatabase(this.settings.Database);
            this.Collection = db.GetCollection<TEntity>(typeof(TEntity).Name.ToLowerInvariant());
        }


        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return Collection.Find(filter).FirstOrDefault();
        }


        public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter)
        {
            return filter == null
                           ? Collection.AsQueryable().ToList()
                           : Collection.AsQueryable().Where(filter).ToList();
        }

        public void Add(TEntity entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            Collection.InsertOne(entity, options);
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var options = new InsertOneOptions { BypassDocumentValidation = false };
            await Collection.InsertOneAsync(entity, options);
            return entity;
        }
        public void AddOrUpdate(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ObjectId id)
        {
            Collection.FindOneAndDelete(x => x.Id == id);

        }

        public void Update(TEntity entity, Expression<Func<TEntity, bool>> filter)
        {
            Collection.ReplaceOne(filter, entity);
        }

        public virtual async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var options = new BulkWriteOptions { IsOrdered = false, BypassDocumentValidation = false };
            return (await Collection.BulkWriteAsync((IEnumerable<WriteModel<TEntity>>)entities, options)).IsAcknowledged;
        }
    }
}

