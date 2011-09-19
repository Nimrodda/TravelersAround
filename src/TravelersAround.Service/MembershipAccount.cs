using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using TravelersAround.Model;
using TravelersAround.Model.Exceptions;

namespace TravelersAround.Service
{
    public class MembershipAccount : IMembership
    {
        public MembershipUser User { get; private set; }

        private readonly MembershipProvider _provider;

        public MembershipAccount()
            : this(null)
        {
        }

        public MembershipAccount(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            return _provider.ValidateUser(userName, password);
        }

        public Guid CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

            MembershipCreateStatus status;
            User = _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            if (User == null)
            {
                MembershipCreationFailedException exception = new MembershipCreationFailedException();
                exception.Message = Enum.GetName(status.GetType(), status);
                throw exception;
            }
            return (Guid)User.ProviderUserKey;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

        public Guid GetUserTravelerID(string userName)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            User = _provider.GetUser(userName, true);
            return (Guid)User.ProviderUserKey;
        }
    }
}
