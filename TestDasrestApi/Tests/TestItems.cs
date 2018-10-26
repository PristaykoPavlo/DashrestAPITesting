using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DasrestApi.Test
{
    [TestFixture]
    class TestItems
    {

        protected string token;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            Logger.InitializationLogging();
            token = Helper.getToken();
            Assert.AreEqual( 32, token.Length);
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            Logger.Dispose();
        }

        [Test]
        public void GetItems()
        {
            var parameters = new Dictionary<string, string>() { { "token", token } };

            string response = Helper.GetRequest("/items", parameters);
            //string actual = response.Content;
            Logger.WritingLogging($"Test Get All Items: content = {response}", null);
            Console.WriteLine("content -> " + response);
        }

        [Test]
        public void AddItem()
        {
            var parameters = new Dictionary<string, string>()
            {
                { "token",token },
                { "item","Hello"}
            };

            string response = Helper.PostRequest("/item/1", parameters);
            //string actual = response.Content;
            Logger.WritingLogging($"Test Get All Items: content = {response}", null);
            Console.WriteLine("content -> " + response);
        }

        [Test]
        public void ChangeItem()
        {
            var parameters = new Dictionary<string, string>()
            {
                {"token",token },
                {"item","World"}
            };

            string response = Helper.PutRequest("/item/1", parameters);
            Console.WriteLine("content ->"+ response);
            Assert.AreEqual("True", response);
        }

        //[Test]
        //public void ChangeItem()
        //{
        //    //Arrange

        //    //Act
        //    request = new RestRequest("/item/3", Method.PUT);
        //    request.AddParameter("token", token);
        //    request.AddParameter("item", "BBBB");
        //    IRestResponse response = client.Execute(request);
        //    var content = response.Content;

        //    //Assert
        //    Console.WriteLine("users -> " + content);
        //    Assert.AreEqual(token.Length, 32);
        //}

        ////[Test]
        //public void GetUsers()
        //{

        //    request = new RestRequest("/users", Method.GET);
        //    request.AddParameter("adminToken", token);
        //    IRestResponse response = client.Execute(request);
        //    var content = response.Content;
        //    Console.WriteLine("users -> " + content);
        //    Assert.AreEqual(token.Length, 32);
        //}

        //[Test]
        public void GetAdminToken()
        {
            var parameters = new Dictionary<string, string>()
            {
                {"name","admin" },
                {"password","qwerty" }
            };

            //IRestResponse response = Helper.GetResponse(Helper.Request(Method.POST, "/login", parameters));
            //token = Helper.getToken(response.Content);
            //Assert.AreEqual(token.Length, 32);
        }
    }
}
