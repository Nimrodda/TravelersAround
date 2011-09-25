using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using TravelersAround.Contracts;
using TravelersAround.Model;
using TravelersAround.Repository;
using TravelersAround.GeoCoding;
using log4net;
using System.Configuration;

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
            Bind<ICache>().To<SQLiteCache>()
                .WithConstructorArgument("idleTime", Int32.Parse(ConfigurationManager.AppSettings["IdleTime"]))
                .WithConstructorArgument("idleUsersCleanUpTime", Int32.Parse(ConfigurationManager.AppSettings["IdleUsersCleanUpTime"]));
            Bind<ILog>().ToMethod(x => LogManager.GetLogger("TEST"));
        }
    }
}
