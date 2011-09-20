using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;

namespace TravelersAround.ServiceProxy
{
    public class TravelersAroundFacade
    {
        private ITravelersAroundService _travelersAroundService;

        public TravelersAroundFacade(ITravelersAroundService travelersAroundService)
        {
            _travelersAroundService = travelersAroundService;
        }


    }
}
