using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model.Exceptions
{
    public class MembershipCreationFailedException : ApplicationException
    {
        new public string Message { get; set; }
    }
}
