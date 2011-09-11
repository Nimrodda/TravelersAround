using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;
using TravelersAround.DataContracts;

namespace TravelersAround.Service
{
    public class MembershipService : IMembershipService
    {
        public RegisterResponse Register(string email, string password, string confirmPassword, string firstname, string lastname, string birthdate, string gender)
        {
            throw new NotImplementedException();
        }

        public LoginResponse Login(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
