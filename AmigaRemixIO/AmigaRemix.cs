using System;
using System.Threading;

namespace AmigaRemixIO
{
    public class AmigaRemix
    {
        public SiteInfo SiteInfo { get; private set; }
        public event EventHandler SiteInfoLoaded;

        public AmigaRemix()
        {
            SiteInfo = new SiteInfo();
        }

        // non-blocking
        public void LoadSiteInfo()
        {
            new Thread(() =>
            {
                SiteInfo.Initialize();
                SiteInfoLoaded?.Invoke(this, EventArgs.Empty);
            }).Start();
        }
    }
}
