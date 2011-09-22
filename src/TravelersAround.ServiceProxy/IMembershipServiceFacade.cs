using System;
using TravelersAround.ServiceProxy.ViewModels;

namespace TravelersAround.ServiceProxy
{
    public interface IMembershipServiceFacade
    {
        LoginView Login(LoginView view);
        RegisterView Register(RegisterView view);
    }
}
