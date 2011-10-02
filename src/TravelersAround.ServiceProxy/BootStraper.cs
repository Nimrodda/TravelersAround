using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using TravelersAround.DataContracts.Views;
using TravelersAround.ServiceProxy.ViewModels;

namespace TravelersAround.ServiceProxy
{
    public static partial class BootStraper
    {
        public static void Initialize()
        {
            Mapper.CreateMap<ProfileDisplayView, ProfileUpdateView>();
            Mapper.CreateMap<ProfileUpdateView, TravelerView>();
            Mapper.CreateMap<LoginView, ProfileDisplayView>();
            Mapper.CreateMap<TravelerView, ProfileUpdateView>();
        }
        
    }
}
