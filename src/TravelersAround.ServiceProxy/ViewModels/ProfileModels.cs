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
             ProfileUpdateView destObj = Mapper.Map<TravelerView, ProfileUpdateView>(this.Profile);
             destObj.Success = this.Success;
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

        public BirthdayPicker Birthdate { get; set; }

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
            return new ProfileDisplayView
            {
                Profile = Mapper.Map<ProfileUpdateView, TravelerView>(this),
                Success = this.Success
            };
        }
    }
   
}
