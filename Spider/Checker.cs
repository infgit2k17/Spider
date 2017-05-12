using System.Text.RegularExpressions;

namespace Spider
{
    public class Checker : IChecker
    {
        private readonly Regex _pattern;

        public Checker(string regexPattern)
        {
            _pattern = new Regex(regexPattern);
        }

        public bool IsSearched(string url, string content)
        {
            return _pattern.IsMatch(content);
        }
    }
}