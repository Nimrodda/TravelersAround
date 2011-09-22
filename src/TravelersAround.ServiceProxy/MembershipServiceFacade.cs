using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;
using TravelersAround.ServiceProxy.ViewModels;
using TravelersAround.DataContracts;

namespace TravelersAround.ServiceProxy
{
    public class MembershipServiceFacade : IMembershipServiceFacade
    {
        private IMembershipService _membershipService;

        public MembershipServiceFacade(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public LoginView Login(LoginView view)
        {
            throw new NotImplementedException();
        }

        public RegisterView Register(RegisterView view)
        {
            throw new NotImplementedException();
        }
    }
}
