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

        public void UpdateProfile(Traveler traveler)
        {
            _repository.Save<Traveler>(traveler);
            _repository.Commit();
        }
    }
}
