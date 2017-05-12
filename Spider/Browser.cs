using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;

namespace Spider
{
    public class Browser : IBrowser
    {
        public string Browse(string url)
        {
            using (var client = new WebClient())
            {
                try
                {
                    return client.DownloadString(url);
                }
                catch (WebException)
                {
                    return null;
                }
            }
        }

        public IEnumerable<string> FindUrls(string html, string domain)
        {
            var links = new List<string>();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var nodes = doc.DocumentNode.SelectNodes("//a[@href]");

            if (nodes == null)
                return links;

            foreach (HtmlNode node in nodes)
            {
                string link = node.Attributes["href"].Value;
                links.Add(link);
            }

            return links;
        }

        public string ExtractDomain(string url)
        {
            return new Uri(url).Host;
        }
    }
}