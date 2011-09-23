using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TravelersAround.DataContracts.Views;
using System.IO;
using AutoMapper;

namespace TravelersAround.ServiceProxy.ViewModels
{
    public class ProfileDisplayView : BaseView
    {
        public TravelerView Profile { get; set; }

        public ProfileUpdateView ConvertToProfileUpdateView()
        {
            Mapper.CreateMap<ProfileDisplayView, ProfileUpdateView>();
            return Mapper.Map<ProfileDisplayView, ProfileUpdateView>(this);
        }
    }

    public class ProfileUpdateView : BaseView
    {
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
        
        [DataType(DataType.Date)]
        public string Birthdate { get; set; }
        
        public string StatusMessage { get; set; }
        
        [RegularExpression("[mfMF]")]
        public string Gender { get; set; }

        public bool IsAvailable { get; set; }
    }
   
}
