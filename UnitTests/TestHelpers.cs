using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TaskPlanner;

namespace UnitTests
{
    [TestClass]
    public class TestHelpers
    {

        [TestMethod]
        public void TestFileExtensions()
        {
            Assert.AreEqual("cs", Helper.GetFileExtension("somefile.cs"));
            Assert.AreEqual("tar.gz", Helper.GetFileExtension("uncommonext.tar.gz"));
            Assert.AreEqual("", Helper.GetFileExtension("filewithoutextension"));
            Assert.ThrowsException<ArgumentException>(() => Helper.GetFileExtension("invalid."));
        }
    }
}
