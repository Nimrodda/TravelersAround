using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TravelersAround.Infrastructure
{
    [DataContract]
    public class PagedList<TSource>
    {
        [DataMember]
        public bool HasNext { get; set; }
        [DataMember]
        public bool HasPrevious { get; set; }
        [DataMember]
        public int TotalEntitiesCount { get; set; }
        [DataMember]
        public int TotalPageCount { get; set; }
        [DataMember]
        public IList<TSource> Entities { get; set; }
    }

    public static class PagedListExtensions
    {
        public static PagedList<TSource> ToPagedList<TSource>(this IEnumerable<TSource> source, int index, int count)
        {
            int totalEntities = source.Count();
            PagedList<TSource> t = 
             new PagedList<TSource>
            {
                HasNext = index + count < totalEntities,
                HasPrevious = index > 0,
                TotalEntitiesCount = totalEntities,
                TotalPageCount = totalEntities / count, 
                Entities = source.Skip(index).Take(count).ToList()
            };
            return t;
        }
    }
}
