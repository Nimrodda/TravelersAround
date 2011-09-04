using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model
{
    public interface IUnitOfWorkRepository
    {
        void PersistCreationOf<TEntity>(TEntity entity) where TEntity : class;
        void PersistUpdateOf<TEntity>(TEntity entity) where TEntity : class;
        void PersistDeletionOf<TEntity>(TEntity entity) where TEntity : class;
    }
}
