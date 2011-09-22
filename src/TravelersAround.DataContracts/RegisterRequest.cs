using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace TravelersAround.DataContracts
{
    [DataContract]
    public class RegisterRequest
    {
        [DataMember]
        [RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", 
            MessageTemplateResourceType = typeof(R.String.ErrorMessages),
            MessageTemplateResourceName = "InvalidEmail")]
        public string Email { get; set; }
        
        [DataMember]
        [StringLengthValidator(6, 12, 
            MessageTemplateResourceType = typeof(R.String.ErrorMessages), 
            MessageTemplateResourceName = "InvalidPassword")]
        public string Password { get; set; }

        [DataMember]
        [StringLengthValidator(6, 12,
            MessageTemplateResourceType = typeof(R.String.ErrorMessages),
            MessageTemplateResourceName = "InvalidConfirmPassword")]
        [PropertyComparisonValidator("Password", ComparisonOperator.Equal,
            MessageTemplateResourceType = typeof(R.String.ErrorMessages),
            MessageTemplateResourceName = "InvalidPasswordMatch")]
        public string ConfirmPassword { get; set; }

        [DataMember]
        [StringLengthValidator(20,
            MessageTemplateResourceType = typeof(R.String.ErrorMessages),
            MessageTemplateResourceName = "InvalidFirstname")]
        public string Firstname { get; set; }
        
        [DataMember]
        [StringLengthValidator(20,
            MessageTemplateResourceType = typeof(R.String.ErrorMessages),
            MessageTemplateResourceName = "InvalidLastname")]
        public string Lastname { get; set; }
        
        [DataMember]
        [RegexValidator(@"^(?=\d)(?:(?!(?:(?:0?[5-9]|1[0-4])(?:\.|-|\/)10(?:\.|-|\/)(?:1582))|(?:(?:0?[3-9]|1[0-3])(?:\.|-|\/)0?9(?:\.|-|\/)(?:1752)))(31(?!(?:\.|-|\/)(?:0?[2469]|11))|30(?!(?:\.|-|\/)0?2)|(?:29(?:(?!(?:\.|-|\/)0?2(?:\.|-|\/))|(?=\D0?2\D(?:(?!000[04]|(?:(?:1[^0-6]|[2468][^048]|[3579][^26])00))(?:(?:(?:\d\d)(?:[02468][048]|[13579][26])(?!\x20BC))|(?:00(?:42|3[0369]|2[147]|1[258]|09)\x20BC))))))|2[0-8]|1\d|0?[1-9])([-.\/])(1[012]|(?:0?[1-9]))\2((?=(?:00(?:4[0-5]|[0-3]?\d)\x20BC)|(?:\d{4}(?:$|(?=\x20\d)\x20)))\d{4}(?:\x20BC)?)(?:$|(?=\x20\d)\x20))?((?:(?:0?[1-9]|1[012])(?::[0-5]\d){0,2}(?:\x20[aApP][mM]))|(?:[01]\d|2[0-3])(?::[0-5]\d){1,2})?$",
            MessageTemplateResourceType = typeof(R.String.ErrorMessages),
            MessageTemplateResourceName = "InvalidBirthdate")]
        public string Birthdate { get; set; }
        
        [DataMember]
        [RegexValidator("[fmFM]",
            MessageTemplateResourceType = typeof(R.String.ErrorMessages),
            MessageTemplateResourceName = "InvalidGender")]
        public string Gender { get; set; }
    }
}
