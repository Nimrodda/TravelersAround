using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelersAround.Model;
using System.IO;
using MaxMindGeocoder;
using System.Reflection;

namespace TravelersAround.GeoCoding
{
    public class GeoCoder : IGeoCoder
    {
        public GeoCoordinates ConvertIPAddressToGeoCoordinates(string ipAddress)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetAssembly(typeof(LookupService)).Location), @"\GeoLiteCity.dat");
            LookupService ls = new LookupService(path, LookupService.GEOIP_STANDARD);
            Location loc = ls.getLocation(ipAddress);
            GeoCoordinates geoCoords = new GeoCoordinates();
            geoCoords.Latitude = loc.latitude;
            geoCoords.Longtitude = loc.longitude;
            geoCoords.City = loc.city;
            geoCoords.Country = loc.countryName;
            return geoCoords;
        }

        public GeoCoordinates ConvertStreetAddressToGeoCoordinates(string streetAddress)
        {
            throw new NotImplementedException();
        }
    }
}
