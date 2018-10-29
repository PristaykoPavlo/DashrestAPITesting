using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDasrestApi.Tests;

namespace DasrestApi.Test
{
    [TestFixture]
    class ItemsTest:BaseTest
    {

        [TestCase("1","Hello")]
        public void GetItems(string index,string item)
        {
            string addedItems = Helper.AddTestItems(index, item);
            string response = Helper.PostRequest(Helper.item, index, item);
            Assert.AreEqual("True", response);

            response = Helper.GetRequest(Helper.items);
            Log($"Test Get All Items: content = {response}");
            Assert.AreEqual(addedItems, response);
            DeleteItem(index, item);
        }

        [TestCase("2","World")]
        public void AddItem(string index,string item)
        {

            string response = Helper.PostRequest(Helper.item, index, item);
            Log($"Test Add Item: content = {response}");

            Assert.AreEqual("True", response);
            DeleteItem(index, item);
        }

        [TestCase("2","World","TAQC")]
        public void ChangeItem(string index, string item,string second_item)
        {

            string response = Helper.PostRequest(Helper.item, index, item);
            Assert.AreEqual("True", response);

            response = Helper.PutRequest(Helper.item, index, item);
            Log($"Test Change Item: content = {response}");
            Assert.AreEqual("True", response);
            DeleteItem(index, item);
        }

        [TestCase("1","Hello")]
        public void DeleteItem(string index,string item)
        {

            string response = Helper.PostRequest(Helper.item, index, item);
            Assert.AreEqual("True", response);

            response = Helper.DeleteRequest(Helper.item, index);
            response = Helper.GetRequest(Helper.items);
            Console.WriteLine(response);
            StringAssert.DoesNotContain(item, response);
        }

        [TestCase("1","2","Hello")]
        public void GetIndexesTest(string index1,string index2, string item)
        {
            string response = Helper.PostRequest(Helper.item, index1, item);
            response = Helper.PostRequest(Helper.item, index2, item);
            Assert.AreEqual("True", response);

            response = Helper.GetRequest(Helper.itemIndexes,"");
            Log($"Test Get All Item Indexes: content = {response}");
            Assert.AreEqual("1 2 ", response);
        }

        [TestCase("1", "Hello")]
        public void GetItemByIndexTest(string index, string item)
        {
            string response = Helper.PostRequest(Helper.item, index, item);
            Assert.AreEqual("True", response);

            response = Helper.GetRequest(Helper.item,index);
            Log($"Test Get Item Name By Index: content = {response}");
            Assert.AreEqual(item, response);
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

    }
}
