using System.Collections.Generic;

namespace Spider
{
    public interface IRepository
    {
        string GetNextUrl();
        void AddVisited(string url);
        void AddFound(string url);
        void Enqueue(IEnumerable<string> urls);
        void Close();
    }
}