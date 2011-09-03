﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using TravelersAround.Model;

namespace TravelersAround.Repository
{
    public class EFRepository : IRepository
    {
        private TravelersAroundEntities _dataContext;

        public EFRepository()
        {
            _dataContext = new TravelersAroundEntities();
        }

        public TEntity FindBy<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            if (predicate != null)
            {
                return _dataContext.CreateObjectSet<TEntity>().FirstOrDefault(predicate);
            }
            else
            {
                throw new ApplicationException("Predicate value must be passed.");
            }
        }

        public IQueryable<TEntity> FindAllBy<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            if (predicate != null)
            {
                return _dataContext.CreateObjectSet<TEntity>().Where(predicate);
            }
            else
            {
                throw new ApplicationException("Predicate value must be passed.");
            }
        }

        public IQueryable<TEntity> FindAll<TEntity>() where TEntity : class
        {

            return _dataContext.CreateObjectSet<TEntity>();
        }

        public IQueryable<TEntity> FindAll<TEntity>(int index, int count) where TEntity : class
        {
            return _dataContext.CreateObjectSet<TEntity>().Skip(index).Take(count);
        }

        public void Save<TEntity>(TEntity entity) where TEntity : class
        {
            //Entity Framework tracks updates automatically
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is IAggregateRoot)
            {
                _dataContext.CreateObjectSet<TEntity>().AddObject(entity);
            }
            else
            {
                throw new ApplicationException("TEntity must be of type IAggregateRoot");
            }
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is IAggregateRoot)
            {
                _dataContext.CreateObjectSet<TEntity>().DeleteObject(entity);
            }
            else
            {
                throw new ApplicationException("TEntity must be of type IAggregateRoot");
            }
        }

        public OperationStatus Commit()
        {
            OperationStatus operStat;
            try
            {
                operStat = new OperationStatus { Succeeded = _dataContext.SaveChanges() > 0 };
            }
            catch (Exception ex)
            {
                operStat = OperationStatus.CreateFromException("Save changes failed", ex);
            }
            
            return operStat;
        }
        
    }
}
