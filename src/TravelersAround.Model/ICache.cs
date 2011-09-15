using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model
{
    public interface ICache
    {
        object GetValue(string key);
        string GetKey(object value);
        int Add(string key, object value);
        int Remove(string key);
    }
}
