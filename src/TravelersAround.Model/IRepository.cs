using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace TravelersAround.Model
{
    public interface IRepository
    {
        TEntity FindBy<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        IQueryable<TEntity> FindAllBy<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        IQueryable<TEntity> FindAllBy<TEntity, TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, int index, int count) where TEntity : class;

        IQueryable<TEntity> FindAll<TEntity>() where TEntity : class;
        IQueryable<TEntity> FindAll<TEntity, TKey>(Expression<Func<TEntity, TKey>> orderBy, int index, int count) where TEntity : class;

        void Save<TEntity>(TEntity entity) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;

        int Commit();
    }
}
