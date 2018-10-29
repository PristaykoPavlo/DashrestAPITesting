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
        #region Fields and Const
        const string URL = "http://localhost:8080/";
        private static string token;
        public static string addedItems;

        public const string login = "/login";
        public const string logout = "/logout";
        public const string user = "/user";
        public const string cooldowntime = "/cooldowntime";
        public const string tokenlifetime = "/tokenlifetime";
        public const string admins = "/admins";
        public const string users = "/users";
        public const string tockens = "/tockens";
        public const string locked = "/locked";
        public const string item = "/item";
        public const string items = "/items";
        public const string itemIndexes = "/itemindexes";
        public const string adminLogin = "admin";
        public const string adminPassword = "qwerty";
        #endregion

        #region Token Methods
        public static string getToken()
        {
            var parameters = new Dictionary<string, string>
            {
                { "name",adminLogin },
                { "password",adminPassword}
            };
        
            string response = TokenRequest("/login", parameters);
            return response;
        }
        public static void AddToken(RestRequest request)
        {
            token = getToken();
            request.AddParameter("token",token);
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
        #endregion

        #region GET,POST,PUT,DELETE-Requests

        public static string GetRequest(string route)
        {
            RestRequest request = new RestRequest(route, Method.GET);
            AddToken(request);
            RestClient client = new RestClient(URL);
            return ResponseToString(client.Execute(request).Content);

        }

        public static string GetRequest(string route, string index)
        {
            RestRequest request = new RestRequest(route + "/" + index, Method.GET);
            AddToken(request);
            RestClient client = new RestClient(URL);
            return ResponseToString(client.Execute(request).Content);

        }

        public static string PostRequest(string route, string index, string item)
        {
            RestRequest request = new RestRequest(route + "/" + index, Method.POST);
            AddToken(request);
            request.AddParameter("item", item);
            RestClient client = new RestClient(URL);

            return ResponseToString(client.Execute(request).Content);
        }

        public static string PostRequest(string route, Dictionary<string, string> parameters)
        {
            RestRequest request = new RestRequest(route, Method.POST);
            AddToken(request);
            foreach (var item in parameters)
            {
                request.AddParameter(item.Key, item.Value);
            }
            RestClient client = new RestClient(URL);

            return ResponseToString(client.Execute(request).Content);
        }

        public static string PutRequest(string route, string index, string item)
        {
            RestRequest request = new RestRequest(route+"/"+index, Method.PUT);
            AddToken(request);
            request.AddParameter("item", item);
            RestClient client = new RestClient(URL);
           
            return ResponseToString(client.Execute(request).Content);
        }

        public static string DeleteRequest(string route, string index)
        {
            RestRequest request = new RestRequest(route + "/" + index, Method.DELETE);
            AddToken(request);
            RestClient client = new RestClient(URL);

            return ResponseToString(client.Execute(request).Content);
        }
        #endregion

        #region String Methods
        public static string AddTestItems(string index, string item)
        {
            return string.Format(index + " " + "\t" + item + "\n");
        }

        public static string ResponseToString(string body)
        {
            return JsonConvert.DeserializeObject<ServiceResponse>(body).content;
        }
        #endregion
    }
    public struct ServiceResponse
    {
        public string content;
    }
}
