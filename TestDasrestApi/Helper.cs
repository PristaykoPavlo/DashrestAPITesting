using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DasrestApi.Test
{
    public class Helper
    {

        const string URL = "http://localhost:8080/";
        //const string Parametr1 = "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW";
        //const string Parametr2 = "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"name\"\r\n\r\nadmin\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"password\"\r\n\r\nqwerty\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--";
        


        public static string getToken()
        {
            var parameters = new Dictionary<string, string>
            {
                { "name","admin" },
                { "password","qwerty"}
            };
        
            string response = TokenRequest("/login", parameters);
            return response;
        }

        protected static string TokenToString(string response)
        {
            string token="";
            try
            {
                token = response.Substring(response.IndexOf(":\"") + 2);
                token = token.Substring(0, token.Length - 2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return token;
        }
        /// <summary>
        /// Logging with parameters and return token
        /// </summary>
        /// <param name="route"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string TokenRequest(string route, Dictionary<string, string> parameters)
        {
            RestRequest request = new RestRequest(route, Method.POST);
            foreach (var item in parameters)
            {
                request.AddParameter(item.Key, item.Value);
            }
            RestClient client = new RestClient(URL);
            return TokenToString(client.Execute(request).Content);

        }

        public static string GetRequest(string route,Dictionary<string,string> parameters)
        {
            RestRequest request = new RestRequest(route, Method.GET);
            foreach (var item in parameters)
            {
                request.AddParameter(item.Key, item.Value);
            }
            RestClient client = new RestClient(URL);
            return ResponseToString(client.Execute(request).Content);

        }


        public static string PostRequest(string route, Dictionary<string, string> parameters)
        {
            RestRequest request = new RestRequest(route, Method.POST);
            foreach (var item in parameters)
            {
                request.AddParameter(item.Key, item.Value);
            }
            RestClient client = new RestClient(URL);
           
            //Logger.WritingLogging($"{System.Reflection.MethodBase.GetMethodFromHandle().Name}: content = ", null);
            return ResponseToString(client.Execute(request).Content);
        }

        public static string PutRequest(string route, Dictionary<string, string> parameters)
        {
            RestRequest request = new RestRequest(route, Method.PUT);
            foreach (var item in parameters)
            {
                request.AddParameter(item.Key, item.Value);
            }
            RestClient client = new RestClient(URL);
           
            return ResponseToString(client.Execute(request).Content);
        }

        public static string ResponseToString(string body)
        {
            return JsonConvert.DeserializeObject<ServiceResponse>(body).content;
        }


    }
    public struct ServiceResponse
    {
        public string content;
    }
}
