using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using TravelersAround.ServiceProxy;
using TravelersAround.DataContracts.Views;
using AutoMapper;

namespace TravelersAround.ServiceProxy.ViewModels
{
    public class ChangePasswordView
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        //[ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        //[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginView : BaseView
    {
        [Required]
        [Display(Name = "User name")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string ApiKey { get; internal set; }
        public TravelerView Profile { get; internal set; }

        public int NewMessagesCount { get; internal set; }

        public ProfileDisplayView MapToProfileDisplayView()
        {
            return Mapper.Map<LoginView, ProfileDisplayView>(this);
        }
    }


    public class RegisterView : BaseView
    {
        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", 
            ErrorMessageResourceType = typeof(R.String.ErrorMessages),
            ErrorMessageResourceName = "InvalidEmail")]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression("[mfMF]",
            ErrorMessageResourceType = typeof(R.String.ErrorMessages),
            ErrorMessageResourceName = "InvalidGender")]
        public string Gender { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public BirthdayPicker Birthdate { get; set; }

        public string ApiKey { get; internal set; }
    }

    public class LogoutView : BaseView
    {
    }
}
