using Spider.DAL;
using System;

namespace Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            Spider spider = CreateSpider();

            string url = ReadInitialUrl();
            if (String.IsNullOrWhiteSpace(url))
                spider.Start();
            else 
                spider.Start(url);
        }

        private static Spider CreateSpider()
        {
            var repo = new Repository(new SpiderContext());
            var extractor = new UrlExtractor
            {
                AllowPattern = @"pl\.wikipedia\.org.+polska",
                DisallowPattern = @"(\.(jpg|png|svg|pdf|mp4)|\?.+=)" // disallow files and urls with parameters
            };
            var spider = new Spider(repo, new Browser(), new Checker("Grzesio Łysio"), extractor);
            spider.Found += OnFound;
            spider.Stopped += OnStopped;

            return spider;
        }

        private static string ReadInitialUrl()
        {
            Console.WriteLine("Provide initial url (optional): ");
            return Console.ReadLine();
        }

        private static void OnFound(object sender, FoundEventArgs args)
        {
            Console.WriteLine(args.Url);
        }

        private static void OnStopped(object sender, EventArgs args)
        {
            Console.WriteLine("Spider has stopped its work");
        }
    }
}