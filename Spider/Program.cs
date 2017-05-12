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

            Console.WriteLine("Press button to stop Spider...");
            spider.Stop();
        }

        private static Spider CreateSpider()
        {
            var repo = new Repository(new SpiderContext());
            var spider = new Spider(repo, new Browser(), new Checker("Grzesio Łysio"));
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