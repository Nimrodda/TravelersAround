﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;
using TravelersAround.Model.Factories;

namespace TravelersAround.Model.Services
{
    public class RelationshipService
    {
        private IRepository _repository;

        public enum Operation
        {
            RemoveFriend,
            AddFriend
        }

        public RelationshipService(IRepository repository)
        {
            _repository = repository;
        }

        public void ModifyFriendsList(Operation operation, Guid travelerID, Guid friendID)
        {
            
            Traveler traveler = _repository.FindBy<Traveler>(t => t.TravelerID == travelerID);
            Traveler friend = _repository.FindBy<Traveler>(f => f.TravelerID == friendID);

            switch (operation)
            {
                case Operation.RemoveFriend:
                    traveler.RemoveFriend(friend);
                    break;
                case Operation.AddFriend:
                    traveler.AddFriend(friend);
                    break;
            }
                
            _repository.Save<Traveler>(traveler);
            _repository.Commit();
        }
    }
}
