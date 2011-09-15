using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using TravelersAround.Contracts;
using TravelersAround.Model;
using TravelersAround.Repository;
using TravelersAround.GeoCoding;

namespace TravelersAround.Service
{
    public class CustomModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMembershipService>().To<MembershipService>();
            Bind<ITravelersAroundService>().To<TravelersAroundService>();
            Bind<IRepository>().To<EFRepository>();
            Bind<ILocationDeterminator>().To<EFLocationDeterminator>();
            Bind<IGeoCoder>().To<GeoCoder>();
            Bind<IMembership>().To<MembershipAccount>();
            Bind<IAPIKeyGenerator>().To<HMACSHA512APIKeyGenerator>();
            Bind<ICache>().To<SQLiteCache>();
        }
    }
}
