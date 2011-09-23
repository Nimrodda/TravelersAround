using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace TravelersAround.ServiceProxy
{
    public abstract class ServiceFacadeBase
    {
        protected object GetMappedObject(object toBeMapped, Type targetType)
        {
            Mapper.CreateMap(toBeMapped.GetType(), targetType);
            return Mapper.Map(toBeMapped, toBeMapped.GetType(), targetType);
        }
    }
}
