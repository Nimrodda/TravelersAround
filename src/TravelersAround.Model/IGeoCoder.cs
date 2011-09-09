using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TravelersAround.Model
{
    public interface IGeoCoder
    {
        GeoCoordinates ConvertIPAddressToGeoCoordinates(string ipAddress);
        GeoCoordinates ConvertStreetAddressToGeoCoordinates(string streetAddress);
    }
}
