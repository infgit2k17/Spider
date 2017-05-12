using System;

namespace Spider
{
    public delegate void FoundEventHandler(object sender, FoundEventArgs e);

    public class FoundEventArgs : EventArgs
    {
        public string Url { get; set; }

        public FoundEventArgs(string url)
        {
            Url = url;
        }
    }

    public delegate void StoppedEventHandler(object sender, EventArgs e);
}