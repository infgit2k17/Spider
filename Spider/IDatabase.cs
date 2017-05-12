using System.Collections.Generic;

namespace Spider
{
    public interface IDatabase
    {
        void PushUrls(IEnumerable<string> urls);
        string PopUrl();
        void AddVisited(string url);
        void AddFound(string url);
        bool IsNew(string url);
        void Close();
    }
}