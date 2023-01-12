using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AmigaRemixIO
{
    public class SiteInfo
    {
        private List<HtmlDocument> PageContents { get; set; }
        private HtmlWeb HtmlWeb { get; set; }

        public int PageCount { get; private set; }
        public List<TrackInfo> AvailableTracks { get; private set; }
        
        public void Initialize()
        {
            PageContents = new List<HtmlDocument>();
            HtmlWeb = new HtmlWeb();

            RetrievePageCount();

            for (var i = 1; i <= PageCount; i++)
            {
                PageContents.Add(DownloadPageContents(i));
            }

            AvailableTracks = new List<TrackInfo>();
            foreach (var html in PageContents)
            {
                AvailableTracks.AddRange(GetTrackInformationForPage(html));
            }

            PageContents.Clear();
        }

        private void RetrievePageCount()
        {
            var doc = HtmlWeb.Load(Common.ServiceURL);

            var targetElement = doc.DocumentNode.SelectSingleNode("//span[@class='numbers']");
            PageCount = int.Parse(targetElement.InnerText.Split('/')[1].Split('&')[0].Trim());
        }

        private HtmlDocument DownloadPageContents(int page)
        {
            HtmlWeb.OverrideEncoding = Encoding.UTF8;
            return HtmlWeb.Load(Common.RemixPageURL(page));
        }

        private List<TrackInfo> GetTrackInformationForPage(HtmlDocument htmlDoc)
        {
            var trackInfoList = new List<TrackInfo>();

            var trackInformationTable = htmlDoc.DocumentNode.SelectNodes("//table[@id='remixtable']/tr[@class]");
            foreach (var trackInformation in trackInformationTable)
            {
                trackInfoList.Add(GetSpecificTrackInformation(trackInformation));
            }

            return trackInfoList;
        }

        private TrackInfo GetSpecificTrackInformation(HtmlNode node)
        {
            var dateElement = node.SelectSingleNode("td[@class='c0']");
            var downloadLinkElement = node.SelectSingleNode("td[@class='c1']/a");
            var arrangerLinkElement = node.SelectSingleNode("td[@class='c2']/a[@class='remix']");
            var composerElement = node.SelectSingleNode("td[@class='c3']");
            var metaDataElements = node.SelectNodes("td[@class='c4']");

            var dateString = dateElement.InnerText;
            var downloadLinkString = Common.DownloadURL(downloadLinkElement.Attributes["href"].Value);
            var titleString = HtmlEntity.DeEntitize(downloadLinkElement.InnerText);
            var arrangerString = HtmlEntity.DeEntitize(arrangerLinkElement.InnerText);
            var composerString = HtmlEntity.DeEntitize(composerElement.InnerText);
            var sizeString = metaDataElements[0].InnerText;
            var durationString = metaDataElements[1].InnerText;

            var trackInfo = new TrackInfo();
            trackInfo.DateAdded = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            trackInfo.DownloadURL = downloadLinkString;
            trackInfo.Title = titleString;
            trackInfo.Arranger = arrangerString;
            trackInfo.Composer = composerString;
            trackInfo.Size = sizeString;
            trackInfo.Duration = TimeSpan.ParseExact(durationString, "mm\\:ss", CultureInfo.InvariantCulture, TimeSpanStyles.None);

            return trackInfo;
        }
    }
}
