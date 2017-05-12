using System.Net;

namespace Spider
{
    public class Browser : IBrowser
    {
        public string Browse(string url)
        {
            using (var client = new WebClient())
            {
                try
                {
                    return client.DownloadString(url);
                }
                catch (WebException)
                {
                    return null;
                }
            }
        }
    }
}