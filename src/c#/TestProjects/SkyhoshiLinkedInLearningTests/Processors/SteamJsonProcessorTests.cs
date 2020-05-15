using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyhoshiLinkedInLibrary.Processors;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkyhoshiLinkedInLibrary.Processors.Tests
{
    [TestClass()]
    public class SteamJsonProcessorTests
    {
        [TestMethod()]
        public void RetrieveJsonDataTest()
        {
            var processor = new SteamJsonProcessor();
            processor.RetrieveJsonData();

        }

        [TestMethod()]
        public void GameListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ListTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ParseTest()
        {
            Assert.Fail();
        }
    }
}