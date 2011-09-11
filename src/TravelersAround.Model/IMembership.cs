using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model
{
    public interface IMembership
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        Guid CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
