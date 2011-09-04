using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;
using TravelersAround.Model.Factories;

namespace TravelersAround.Model.Services
{
    public class ProfileService
    {
        private IRepository _repository;

        public ProfileService(IRepository repository)
        {
            _repository = repository;
        }

        public OperationStatus UpdateProfile(Traveler traveler)
        {
            OperationStatus operStat;

            try
            {
                _repository.Save<Traveler>(traveler);
                operStat = OperationStatusFactory.CreateFromBoolean(_repository.Commit() > 0);
            }
            catch (Exception ex)
            {
                operStat = OperationStatusFactory.CreateFromException(ex);
            }

            return operStat;
        }
    }
}
