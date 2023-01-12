using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmigaRemixIO.Test
{
    class Program
    {
        private static AmigaRemix AmigaRemix { get; set; }

        static void Main(string[] args)
        {
            AmigaRemix = new AmigaRemix();
            AmigaRemix.SiteInfoLoaded += AmigaRemix_SiteInfoLoaded;
            AmigaRemix.LoadSiteInfo();
        }

        private static void AmigaRemix_SiteInfoLoaded(object sender, EventArgs e)
        {
            foreach(var trackInfo in AmigaRemix.SiteInfo.AvailableTracks)
            {
                Console.WriteLine(trackInfo.Title);
            }
            Console.ReadLine();
        }
    }
}
