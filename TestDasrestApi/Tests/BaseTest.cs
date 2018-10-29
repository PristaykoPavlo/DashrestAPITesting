using System;
using System.Collections.Generic;
using DasrestApi.Test;
using NUnit.Framework;

namespace TestDasrestApi.Tests
{
    class BaseTest
    {

        [OneTimeSetUp]
        public void BeforeAll()
        {
            Logger.InitializationLogging();

        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            Logger.Dispose();
        }

        public void Log(string message)
        {
            Logger.WritingLogging( message,null);
        }

    }
}
