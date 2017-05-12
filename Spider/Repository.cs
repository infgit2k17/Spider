using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Spider
{
    public class Repository : IRepository
    {
        private readonly IDatabase _db;
        private List<string> _localNewUrls, _newUrls;

        public Repository(IDatabase db)
        {
            _db = db;
        }

        public string GetNextUrl()
        {
            return _db.PopUrl();
        }

        public void AddVisited(string url)
        {
            _db.AddVisited(url);
        }

        public void AddFound(string url)
        {
            _db.AddFound(url);
        }

        public void Enqueue(IEnumerable<string> urls)
        {
            var urlsList = urls.ToList();
            var findNewInDbThread = new Thread(() => FindNewInDb(urlsList));
            findNewInDbThread.Start();

            var findNewThread = new Thread(() => FindNewInDb(urlsList));
            findNewThread.Start();

            findNewThread.Join();
            findNewInDbThread.Join();

            _db.PushUrls(_localNewUrls.Union(_newUrls));
        }

        public void Close()
        {
            _db.Close();

            // wysłanie wyników do innych węzłów w celu ich przechowania
        }

        private void FindNewInDb(List<string> urls)
        {
            _localNewUrls = new List<string>();

            for (int i = 0; i < urls.Count; i++)
            {
                if (_db.IsNew(urls[i]))
                    _localNewUrls.Add(urls[i]);
            }
        }

        private void FindNew(List<string> urls)
        {
            _newUrls = new List<string>();

            for (int i = 0; i < urls.Count; i++)
            {
                // należy odpytać inne węzły które url nie zostały jeszcze sprawdzone albo nie są w kolejkach
            }
        }
    }
}