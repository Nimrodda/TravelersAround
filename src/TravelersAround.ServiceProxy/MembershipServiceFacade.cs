using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Contracts;
using TravelersAround.ServiceProxy.ViewModels;
using TravelersAround.DataContracts;

namespace TravelersAround.ServiceProxy
{
    public class MembershipServiceFacade : ServiceFacadeBase, IMembershipServiceFacade
    {
        private IMembershipService _membershipService;

        public MembershipServiceFacade(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public LoginView Login(LoginView view)
        {
            LoginRequest request = (LoginRequest)GetMappedObject(view, typeof(LoginRequest));
            return (LoginView)GetMappedObject(_membershipService.Login(request), typeof(LoginView));
        }

        public RegisterView Register(RegisterView view)
        {
            RegisterRequest request = (RegisterRequest)GetMappedObject(view, typeof(RegisterRequest));
            return (RegisterView)GetMappedObject(_membershipService.Register(request), typeof(RegisterView));
        }


        public LogoutView Logout(string apiKey)
        {
            return (LogoutView)GetMappedObject(_membershipService.Logout(apiKey), typeof(LogoutView));
        }
    }
}
