using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;
using TravelersAround.DataContracts;
using System.ServiceModel.Activation;
using System.ServiceModel;
using TravelersAround.Model;
using TravelersAround.Model.Entities;

namespace TravelersAround.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MembershipService : IMembershipService
    {
        private IRepository _repository;
        private IMembership _membership;

        public RegisterResponse Register(string email, string password, string confirmPassword, string firstname, string lastname, string birthdate, string gender)
        {
            RegisterResponse response = new RegisterResponse { Success = true, ErrorMessage = "" };

            try
            {
                Guid newTravelerID = _membership.CreateUser(email, password, email);
                Traveler newTraveler = new Traveler
                {
                    TravelerID = newTravelerID,
                    Firstname = firstname,
                    Lastname = lastname,
                    Gender = gender,
                    Birthdate = DateTime.Parse(birthdate)
                };

                _repository.Add<Traveler>(newTraveler);
                _repository.Commit();
            }
            catch (Exception ex)
            {
                response.Success = false;

            }

            return response;

        }

        public LoginResponse Login(string email, string password)
        {
            LoginResponse response = new LoginResponse { Success = true, ErrorMessage = "" };
            
            return response;
        }
    }
}
