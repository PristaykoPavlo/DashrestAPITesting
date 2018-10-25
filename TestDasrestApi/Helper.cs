using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace TestDasrestApi
{
    public class Helper
    {

        const string URL = "http://localhost:8080/";
        const string Parametr1 = "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW";
        const string Parametr2 = "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"name\"\r\n\r\nadmin\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"password\"\r\n\r\nqwerty\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--";



        public static string getToken()
        {
            string token="";

            IRestResponse response = GetResponse(Request(Method.POST,"/login", ParameterType.RequestBody, Parametr1,Parametr2 ));
            var content = response.Content;
            try
            {
                token = content.Substring(content.IndexOf(":\"") + 2);
                token = token.Substring(0, token.Length - 2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return token;
        }

        public static IRestResponse GetResponse(RestRequest request)
        {
            RestClient client = new RestClient(URL);
            return client.Execute(request);
        }

        public static RestRequest Request(Method method,string url, Dictionary<string,string> parametrs)
        {
            RestRequest request = new RestRequest(url, method);
            foreach(var item in parametrs)
            {
                request.AddParameter(item.Key, item.Value);
            }
            return request;
        }

        public static RestRequest Request(Method method, string url)
        {
            RestRequest request = new RestRequest(url, method);
            return request;
        }

        public static RestRequest Request(Method method, string url, ParameterType type, string par1,string par2)
        {
            RestRequest request = new RestRequest(url, method);
            request.AddParameter(par1,par2,type);
            return request;
        }

        public static ServiceResponse GetServiceResponse(string body)
        {

            return JsonConvert.DeserializeObject<ServiceResponse>(body);
        }

    }
    public struct ServiceResponse
    {
        public string content;
    }
}
