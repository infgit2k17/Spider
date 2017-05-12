using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Spider.Tests
{
    [TestClass]
    public class BrowserTests
    {
        private Browser _browser;

        [TestInitialize]
        public void Init()
        {
            _browser = new Browser();
        }

        [TestMethod]
        public void FindUrls_EmptyString_NothingFound()
        {
            Assert.AreEqual(0, _browser.FindUrls(String.Empty).Count());
        }

        [TestMethod]
        public void FindUrls_NoLinks_NothingFound()
        {
            Assert.AreEqual(0, _browser.FindUrls("Test").Count());
        }

        [TestMethod]
        public void FindUrls_LinkExists_TwoFound()
        {
            var urls = _browser.FindUrls("<title>test</title>"
                                         + "<a href=\"http://domain.com\">domain</a>"
                                         + "Text"
                                         + "<a href=\"http://example.com\">example</a");

            Assert.AreEqual(2, urls.Count());
            Assert.AreEqual("http://domain.com", urls.ToArray()[0]);
            Assert.AreEqual("http://example.com", urls.ToArray()[1]);
        }
    }
}