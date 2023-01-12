namespace AmigaRemixIO
{
    internal class Common
    {
        public const string ServiceURL = "http://amigaremix.com";

        public static string RemixPageURL(int page) => $"{ServiceURL}/remixes/{page}";
        public static string SearchRemixesURL(string query) => $"{ServiceURL}/remixes/search/{query}";
        public static string DownloadURL(string subUrl) => $"{ServiceURL}{subUrl}";
    }
}
