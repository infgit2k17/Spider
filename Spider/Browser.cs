using HtmlAgilityPack;
using System.Collections.Generic;
using System.Net;

namespace Spider
{
    public class Browser : IBrowser
    {
        public string Browse(string url)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        public IEnumerable<string> FindUrls(string html)
        {
            var hrefTags = new List<string>();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var nodes = doc.DocumentNode.SelectNodes("//a[@href]");

            if (nodes == null)
                return hrefTags;

            foreach (HtmlNode link in nodes)
            {
                HtmlAttribute att = link.Attributes["href"];
                hrefTags.Add(att.Value);
            }

            return hrefTags;
        }
    }
}