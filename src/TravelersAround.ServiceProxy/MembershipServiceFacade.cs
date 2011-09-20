using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;

namespace TravelersAround.ServiceProxy
{
    public class MembershipServiceFacade
    {
        private IMembershipService _membershipService;

        public MembershipServiceFacade(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }


    }
}
