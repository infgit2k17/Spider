using System.Collections.Generic;

namespace Spider
{
    public interface IBrowser
    {
        string Browse(string url);
        IEnumerable<string> FindUrls(string html);
    }
}