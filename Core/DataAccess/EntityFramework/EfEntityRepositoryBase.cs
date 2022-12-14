using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
  public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
  where TEntity : class, IEntity, new()
  where TContext : DbContext, new()
  {
    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {
      using (TContext context = new TContext())
      {
        return filter == null
            ? context.Set<TEntity>().ToList()
            : context.Set<TEntity>().Where(filter).ToList();

      }
    }

    public TEntity Get(Expression<Func<TEntity, bool>> filter)
    {
      using (TContext context = new TContext())
      {
        return context.Set<TEntity>().SingleOrDefault(filter);
      }
    }

    public void Add(TEntity entity)
    {
      using (TContext context = new TContext())
      {
        var addedEntity = context.Entry(entity);
        addedEntity.State = EntityState.Added;
        context.SaveChanges();
      }
    }

    public void Update(TEntity entity)
    {
      using (TContext context = new TContext())
      {
        var uptatedEntity = context.Entry(entity);
        uptatedEntity.State = EntityState.Modified;
        context.SaveChanges();
      }
    }

    public void Delete(int id)
    {
      using (TContext context = new TContext())
      {
        context.Remove(context.Set<TEntity>().Find(id));
        context.SaveChanges();
      }
    }

    public void AddOrUpdate(TEntity entity)
    {
      using TContext context = new TContext();
      var entry = context.Entry(entity);
      switch (entry.State)
      {
        case EntityState.Detached:
          context.Update(entity);
          break;
        case EntityState.Modified:
          context.Update(entity);
          break;
        case EntityState.Added:
          context.Add(entity);
          break;
        case EntityState.Unchanged:
          //item already in db no need to do anything  
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      context.SaveChanges();
    }
  }
}
