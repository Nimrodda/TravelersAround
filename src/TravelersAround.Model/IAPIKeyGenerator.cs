using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model
{
    public interface IAPIKeyGenerator
    {
        string Generate(string password);
    }
}
