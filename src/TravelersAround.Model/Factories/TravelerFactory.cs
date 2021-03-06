﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model.Entities;

namespace TravelersAround.Model.Factories
{
    public static class TravelerFactory
    {
        public static Traveler CreateTraveler(Guid id, 
                                            string firstname, 
                                            string lastname, 
                                            DateTime birthdate, 
                                            string gender,
                                            string apiKey)
        {
            return new Traveler
            {
                TravelerID = id,
                Firstname = firstname,
                Lastname = lastname,
                Gender = gender,
                Birthdate = birthdate,
                ApiKey = apiKey,
                StatusMessage = R.String.Default.StatusMessage,
                IsAvailable = true,
                IsOnline = true
            };
        }
    }
}
