using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Spider.DAL
{
    public class SpiderContext : DbContext, IDatabase
    {
        public DbSet<UrlEntity> Queue { get; set; }
        public DbSet<VisitedEntity> Visited { get; set; }
        public DbSet<FoundEntity> Found { get; set; }

        private string _buffer;
        private readonly MD5 _md5 = MD5.Create();

        public SpiderContext() : base(BuildDbContextOptions())
        { }

        public SpiderContext(DbContextOptions<SpiderContext> options) : base(options)
        { }

        private static DbContextOptions<SpiderContext> BuildDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<SpiderContext>();
            options.UseSqlServer(@"Data Source=.\SQLEXPRESS;Database=Spider;Integrated Security=True;");

            return options.Options;
        }

        public void PushUrls(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                if (_buffer == null)
                    _buffer = url;
                else
                    Queue.Add(new UrlEntity { Value = url });
            }

            SaveChanges();
        }

        public string PopUrl()
        {
            if (_buffer != null)
            {
                string buff = _buffer;
                _buffer = null;
                return buff;
            }

            UrlEntity url = Queue.FirstOrDefault();

            if (url == null)
                return null;

            Queue.Remove(url);
            SaveChanges();

            return url.Value;
        }

        public void AddVisited(string url)
        {
            Visited.Add(new VisitedEntity { Value = GetMd5Hash(url) });
            SaveChanges();
        }

        public void AddFound(string url)
        {
            Found.Add(new FoundEntity { Value = url });
            SaveChanges();
        }

        public bool IsNew(string url)
        {
            if (Queue.Any(i => i.Value == url))
                return false;

            byte[] hash = GetMd5Hash(url);
            if (Visited.Any(i => i.Value.SequenceEqual(hash)))
                return false;

            if (Found.Any(i => i.Value == url))
                return false;

            return true;
        }

        public void Close()
        {
            // ignored
        }

        private byte[] GetMd5Hash(string s)
        {
            return _md5.ComputeHash(Encoding.UTF8.GetBytes(s));
        }
    }
}