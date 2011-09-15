using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web;

namespace TravelersAround.Model
{
    public class HMACSHA512APIKeyGenerator : IAPIKeyGenerator
    {
        public string Generate(string password)
        {
            byte[] msgByte = ASCIIEncoding.ASCII.GetBytes(password);

            HMACSHA512 hmac = new HMACSHA512();
            byte[] hashMsg = hmac.ComputeHash(msgByte);

            return HttpServerUtility.UrlTokenEncode(hashMsg);
        }
    }
}
