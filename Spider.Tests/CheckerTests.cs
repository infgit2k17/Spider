using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Spider.Tests
{
    [TestClass]
    public class CheckerTests
    {
        private Checker _checker;

        [TestInitialize]
        public void Init()
        {
            _checker = new Checker("PATTERN");
        }

        [TestMethod]
        public void IsSearched_PatternNotFound_False()
        {
            Assert.IsFalse(_checker.IsSearched(String.Empty, "TEST"));
        }

        [TestMethod]
        public void IsSearched_PatternFound_True()
        {
            Assert.IsTrue(_checker.IsSearched(String.Empty, "TEST PATTERN"));
        }
    }
}