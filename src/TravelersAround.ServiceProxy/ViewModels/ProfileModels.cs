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

        public ProfileUpdateView MapToProfileUpdateView()
        {
            //TODO: Fix mapping. Success is always false
            ProfileUpdateView destObj = Mapper.Map<ProfileDisplayView, ProfileUpdateView>(this);
            destObj = Mapper.Map<TravelerView, ProfileUpdateView>(this.Profile);
            return destObj;
        }
    }

    public class ProfileUpdateView : BaseView
    {
        public string TravelerID { get; internal set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public string Birthdate { get; set; }

        [Required]
        [DisplayName("Staus message")]
        public string StatusMessage { get; set; }

        [Required]
        [RegularExpression("[mfMF]")]
        public string Gender { get; set; }

        [Required]
        [DisplayName("Available?")]
        public bool IsAvailable { get; set; }

        public ProfileDisplayView MapToProfileDisplayView()
        {
            ProfileDisplayView destObj = Mapper.Map<ProfileUpdateView, ProfileDisplayView>(this);
            return destObj;
        }
    }
   
}
