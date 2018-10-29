using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DasrestApi.Test;

namespace TestDasrestApi.Tests
{
    [TestFixture]
    class UsersTest:BaseTest
    {

        [TestCase("12345", "Bob", "true")]
        public void CreateUserTest(string password,string userName,string adminRights)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"newName", userName},
                {"newPassword",password },
                {"adminRights",adminRights }
            };

            string response = Helper.PostRequest(Helper.user, parameters);
            Log($"Test Add User: content = {response}");
            Assert.AreEqual("True", response);
        }

        [TestCase("12345","Bob")]
        public void LoginNewUserTest(string password,string userName)
        {
            var parameters = new Dictionary<string, string>() { };

            string response = Helper.PostRequest(Helper.login, parameters);
            Log($"Test Login As New User: token = {response}");
            Assert.AreEqual(32, response.Length);
        }

        [Test]
        public void LogoutTest()
        {
            var parameters = new Dictionary<string, string>() { };

            string response = Helper.PostRequest(Helper.logout, parameters);
            Log($"Test Logout: token = {response}");
            Assert.AreEqual("False", response);
        }
        //[Test]
        public void GetAllUsersTest()
        {

            string response = Helper.GetRequest(Helper.users);
            Log($"Test All Users: content = {response}");
            //Console.WriteLine(response);
            Assert.AreEqual(113, response.Length);
        }

        [TestCase("admin")]
        public void GetUserNameTest(string name)
        {

            string response = Helper.GetRequest(Helper.user);
            Log($"Test Get User Name: content = {response}");
            Assert.AreEqual(name, response);
        }
    }
}
