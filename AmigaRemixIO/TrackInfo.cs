using System;

namespace AmigaRemixIO
{
    public class TrackInfo
    {
        public DateTime DateAdded { get; internal set; }
        public string Arranger { get; internal set; }
        public string Composer { get; internal set; }
        public string Title { get; internal set; }
        public string Size { get; internal set; }
        public TimeSpan Duration { get; internal set; }
        public string DownloadURL { get; internal set; }
    }
}
