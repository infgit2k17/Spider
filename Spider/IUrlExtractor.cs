using System.Collections.Generic;

namespace Spider
{
    public interface IUrlExtractor
    {
        IEnumerable<string> ExtractUrls(string html, string currentUrl);
    }
}