using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model
{
    public interface IUnitOfWork
    {
        void RegisterAmended(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository);
        void RegisterNew(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository);
        void RegisterRemoved(IAggregateRoot entity, IUnitOfWorkRepository unitOfWorkRepository);
        void Commit();
    }
}
