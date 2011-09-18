using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using System.Collections.Specialized;
using System.Reflection;

namespace TravelersAround.ServiceProxy
{
    public static class HttpRequestAdapter
    {
        public enum Method
        {
            POST,
            GET
        };

        /// <summary>
        /// Makes a JSON HTTP request to a WCF REST web service without the request body using only query string
        /// and returning the response type specified by the service contact operation
        /// </summary>
        /// <typeparam name="ResponseType">The expected response type defined by the service contract</typeparam>
        /// <param name="baseUrl">The base url</param>
        /// <param name="uri">The operation name</param>
        /// <param name="query">NameValueCollection of query string parameters and values</param>
        /// <param name="method">The method to use in the request</param>
        /// <returns>The response type defined in the method</returns>
        public static ResponseType WebHttpRequest<ResponseType>(string baseUrl, string uri, string queryString, Method method = Method.GET)
        {
            HttpWebRequest invokeRequest = WebRequest.Create(String.Concat(baseUrl, "/", uri, queryString)) as HttpWebRequest;
            invokeRequest.Method = Enum.GetName(typeof(Method), method);
            invokeRequest.ContentType = "application/json";

            invokeRequest.ContentLength = 0;

            WebResponse response = invokeRequest.GetResponse();

            return DeserializeFromJSON<ResponseType>(response.GetResponseStream());


        }

        /// <summary>
        /// Makes a JSON HTTP request to a WCF REST web service without the request body using only query string
        /// and returning a stream (binary response) normally used for content files such as audio, video, documentation etc.
        /// </summary>
        /// <param name="baseUrl">The base url</param>
        /// <param name="uri">The operation name</param>
        /// <param name="query">NameValueCollection of query string parameters and values</param>
        /// <param name="method">The method to use in the request</param>
        /// <returns>MemoryStream containing the binary data</returns>
        public static MemoryStream WebHttpRequest(string baseUrl, string uri, string queryString, Method method = Method.GET)
        {
            HttpWebRequest invokeRequest = WebRequest.Create(String.Concat(baseUrl, "/", uri, queryString)) as HttpWebRequest;
            invokeRequest.Method = Enum.GetName(typeof(Method), method);
            invokeRequest.ContentType = "application/json";
            invokeRequest.ContentLength = 0;

            using (WebResponse response = invokeRequest.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    byte[] buffer = new byte[64 * 1024];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //copying the bytes array from one stream to another
                        //int read;
                        //while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        //{
                        //    ms.Write(buffer, 0, read);
                        //}
                        //It seems that CopyTo does the same thing as the loop above
                        responseStream.CopyTo(ms);
                        return ms;
                    }
                }
            }

        }

        /// <summary>
        /// Makes a JSON HTTP POST request to a WCF REST web service with the request body represented as an instance of the request type defined by the service contract
        /// </summary>
        /// <typeparam name="ResponseType">The expected response type</typeparam>
        /// <param name="requestObject">Represents the body of the request</param>
        /// <param name="url">The base url</param>
        /// <param name="uri">The operation name</param>
        /// <returns>The response type defined in the method</returns>
        public static ResponseType WebHttpPost<ResponseType>(string url, string uri, object requestObject)
        {
            HttpWebRequest invokeRequest = WebRequest.Create(String.Concat(url, uri)) as HttpWebRequest;
            
            invokeRequest.Method = "POST";
            invokeRequest.ContentType = "application/json";

            byte[] requestBodyBytes = SerializeToJSON(requestObject);
            invokeRequest.ContentLength = requestBodyBytes.Length;
            using (Stream postStream = invokeRequest.GetRequestStream())
                postStream.Write(requestBodyBytes, 0, requestBodyBytes.Length);

            WebResponse response = invokeRequest.GetResponse();

            return DeserializeFromJSON<ResponseType>(response.GetResponseStream());


        }

        /// <summary>
        /// Makes an Octet-Stream HTTP POST request, normally used for transferring files to WCF REST service
        /// </summary>
        /// <typeparam name="ResponseType">The response type specified by the service contract operation</typeparam>
        /// <param name="stream">The stream to send within the body of the request</param>
        /// <param name="streamContentLength">The length of the stream to be sent</param>
        /// <param name="baseUrl">The base URL</param>
        /// <param name="uri">The operation name</param>
        /// <param name="query">NameValueCollection of query string parameters and values</param>
        /// <returns></returns>
        public static ResponseType WebHttpPost<ResponseType>(string baseUrl, string uri, Stream stream, string queryString)
        {
            int streamContentLength = (int)stream.Length;
            byte[] requestBodyBytes = new byte[streamContentLength];
            using (stream)
                stream.Read(requestBodyBytes, 0, streamContentLength);

            HttpWebRequest invokeRequest = WebRequest.Create(String.Concat(baseUrl, "/", uri, queryString)) as HttpWebRequest;
            
            invokeRequest.Method = "POST";
            invokeRequest.ContentType = "application/octet-stream";
            invokeRequest.ContentLength = streamContentLength;

            using (Stream postStream = invokeRequest.GetRequestStream())
                postStream.Write(requestBodyBytes, 0, requestBodyBytes.Length);

            using (WebResponse response = invokeRequest.GetResponse())
            {
                return DeserializeFromJSON<ResponseType>(response.GetResponseStream());
            }


        }

        /// <summary>
        /// Serialzes any data type marked with DataContract to a byte array JSON representation
        /// </summary>
        /// <param name="obj">any object to serialize which is marked with DataContract attribute</param>
        /// <returns>Byte array representation of the object in json format</returns>
        private static byte[] SerializeToJSON(Object obj)
        {
            using (MemoryStream stream1 = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType());
                ser.WriteObject(stream1, obj);
                return stream1.ToArray();
            }
        }

        /// <summary>
        /// Deserializing json data objects in their binray format to a strongly typed data type
        /// </summary>
        /// <typeparam name="T">The data type to cast the object into, must be identical to the json object</typeparam>
        /// <param name="stream">The steam which contains the json object in its binary data form (byte array)</param>
        /// <returns>An instance of the specified type</returns>
        private static T DeserializeFromJSON<T>(Stream stream)
        {
            using (stream)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                return (T)ser.ReadObject(stream);
            }
        }

        private static byte[] SerializeToXML(Object obj)
        {
            using (MemoryStream stream1 = new MemoryStream())
            {
                XmlSerializer ser = new XmlSerializer(obj.GetType());
                ser.Serialize(stream1, obj);
                return stream1.ToArray();
            }
        }

        private static T DeserializeFromXML<T>(Stream stream)
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            return (T)ser.Deserialize(stream);
        }

        /// <summary>
        /// Constructs a HTTP request query string from a NameValueCollection
        /// </summary>
        /// <param name="nvc">NameValueCollection of string to string only</param>
        /// <returns>The query string</returns>
        private static string NameValueCollectionToQueryString(NameValueCollection nvc)
        {
            return String.Concat("?", String.Join("&", Array.ConvertAll(nvc.AllKeys, key => String.Format("{0}={1}", key, nvc[key]))));
        }

        /// <summary>
        /// Constructs a http request query string out of parent method's parameter names and input parameters
        /// </summary>
        /// <param name="parameterNames">The calling method ParameterInfos which will represent the key of the query</param>
        /// <param name="queryParameters">Must be the same parameters from the calling method and in the same order!</param>
        /// <returns>Query string representation</returns>
        public static string ConstructQueryString(ParameterInfo[] parameterNames, params object[] queryParameters)
        {
            string queryString = "?";
            Array.ForEach(parameterNames, x => queryString = String.Concat(queryString, String.Format("{0}={1}&", x.Name, queryParameters[x.Position].ToString())));
            return queryString.Remove(queryString.Length -1);
        }

    }
}
