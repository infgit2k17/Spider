using System;

namespace Spider
{
    public class Spider
    {
        private readonly IRepository _repo;
        private readonly IBrowser _browser;
        private readonly IChecker _checker;
        private readonly IUrlExtractor _extractor;
        private bool _status;

        public event FoundEventHandler Found;
        public event StoppedEventHandler Stopped;

        public Spider(IRepository repo, IBrowser browser, IChecker checker, IUrlExtractor extractor)
        {
            _repo = repo;
            _browser = browser;
            _checker = checker;
            _extractor = extractor;
            _status = true;
        }

        public void Start()
        {
            while (_status)
                ProccessPage();
        }

        public void Start(string initialUrl)
        {
            _repo.Enqueue(new[] { initialUrl });
            Start();
        }

        public void Stop()
        {
            _status = false;
            _repo.Close();
            OnStop(EventArgs.Empty);
        }

        private void OnFound(FoundEventArgs e)
        {
            Found?.Invoke(this, e);
        }

        private void OnStop(EventArgs e)
        {
            Stopped?.Invoke(this, e);
        }

        private void ProccessPage()
        {
            string url = _repo.GetNextUrl();
            if (url == null)
            {
                Stop();
                return;
            }

            string html = _browser.Browse(url);
            if (IsInvalidPage(html))
                return;

            if (_checker.IsSearched(url, html))
            {
                _repo.AddFound(url);
                OnFound(new FoundEventArgs(url));
            }
            else
                _repo.AddVisited(url);

            _repo.Enqueue(_extractor.ExtractUrls(html, url));
        }

        private bool IsInvalidPage(string html)
        {
            return html == null;
        }
    }
}