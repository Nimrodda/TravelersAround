using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model
{
    public interface ICache
    {
        object GetValue(string key);
        IEnumerable<Guid> GetAll();
        string GetKey(object value);
        int Add(string key, object value);
        int Remove(string key);
        int SetOnline(string key);
        int RemoveExpired();
        int SetIdleUserOffline();

        int IdleTime { get; }
        int IdleUsersCleanUpTime { get; }
    }
}
