using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Spider
{
    public class UrlExtractor : IUrlExtractor
    {
        public string AllowPattern { get; set; }
        public string DisallowPattern { get; set; }

        public IEnumerable<string> ExtractUrls(string html, string currentUrl)
        {
            string domain = ExtractDomain(currentUrl);
            var links = new List<string>();
            var doc = new HtmlDocument();

            doc.LoadHtml(html);

            var nodes = doc.DocumentNode.SelectNodes("//a[@href]");
            if (nodes == null)
                return links;

            foreach (HtmlNode node in nodes)
            {
                string link = node.HrefAttribute();
                if (!IsAbsoluteUrl(link))
                    link = domain + link;

                if (IsAllowed(link) && !IsDisallowed(link))
                    links.Add(link);
            }

            return links;
        }

        private string ExtractDomain(string url)
        {
            string prefix = url.Substring(0, url.IndexOf("//") + 2);

            return prefix + new Uri(url).Host;
        }

        private bool IsAbsoluteUrl(string url)
        {
            return url.StartsWith("http");
        }

        private bool IsAllowed(string url)
        {
            if (AllowPattern == null)
                return true;

            return Regex.IsMatch(url, AllowPattern);
        }

        private bool IsDisallowed(string url)
        {
            if (DisallowPattern == null)
                return false;

            return Regex.IsMatch(url, DisallowPattern);
        }
    }

    public static class HtmlNodeExtensions
    {
        public static string HrefAttribute(this HtmlNode node)
        {
            return node.Attributes["href"].Value.ToLower();
        }
    }
}