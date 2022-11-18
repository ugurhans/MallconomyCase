using System;
using System.Linq.Expressions;
using Core.Entities;
using MongoDB.Bson;

namespace Core.DataAccess
{
    public interface IMongoDbRepository<T>
  where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity, Expression<Func<T, bool>> filter);
        void Delete(ObjectId id);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
    }
}

