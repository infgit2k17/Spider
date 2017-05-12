﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Spider
{
    public class SpiderContext : DbContext, IDatabase
    {
        public DbSet<UrlEntity> Queue { get; set; }
        public DbSet<VisitedEntity> Visited { get; set; }
        public DbSet<FoundEntity> Found { get; set; }

        private string _buffer;

        public SpiderContext() : base(BuildDbContextOptions())
        { }

        public SpiderContext(DbContextOptions<SpiderContext> options) : base(options)
        { }

        private static DbContextOptions<SpiderContext> BuildDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<SpiderContext>();
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=spider-database;Trusted_Connection=True;MultipleActiveResultSets=true");

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
            Visited.Add(new VisitedEntity { Value = url });
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

            if (Visited.Any(i => i.Value == url))
                return false;

            if (Found.Any(i => i.Value == url))
                return false;

            return true;
        }

        public void Close()
        {
            // ignored
        }
    }
}