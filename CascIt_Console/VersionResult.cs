namespace CascIt_Console
{
    #region Imports

    using System.Collections.Generic;
    using System.Linq;

    #endregion
    public class VersionResult
    {
        public VersionResult(string line, CDNResults cdnResults)
        {
            CDNUrls = new Dictionary<string, IEnumerable<string>>();
            BuildUrls = new Dictionary<string, IEnumerable<string>>();

            Results = line.Split('|');

            if (Results.Length > 0)
                Region = Results[0];

            if (Results.Length > 1)
                BuildConfigHash = Results[1];

            if (Results.Length > 2)
                CDNConfigHash = Results[2];

            if (Results.Length > 3)
                BuildID = Results[3];

            if (Results.Length > 4)
                VersionName = Results[4];

            foreach (var cdnUrl in cdnResults.CDNUrls)
            {
                BuildUrls.Add(cdnUrl.Key,
                    cdnUrl.Value.Select(
                        s => string.Format(s, CASCEngine.PathType.Config, BuildConfigFirstTwo, BuildConfigSecondTwo, BuildConfigHash).ToLower()));
                CDNUrls.Add(cdnUrl.Key,
                    cdnUrl.Value.Select(
                        s => string.Format(s, CASCEngine.PathType.Config, CDNConfigFirstTwo, CDNConfigSecondTwo, CDNConfigHash).ToLower()));
            }
        }

        public string[] Results { get; set; }
        public string Region { get; set; }
        public string BuildConfigHash { get; set; }

        public string BuildConfigFirstTwo => !string.IsNullOrWhiteSpace(BuildConfigHash) && BuildConfigHash.Length >= 2
                                            ? BuildConfigHash.Substring(0, 2)
                                            : "XX";

        public string BuildConfigSecondTwo => !string.IsNullOrWhiteSpace(BuildConfigHash) && BuildConfigHash.Length >= 4
                                            ? BuildConfigHash.Substring(2, 2)
                                            : "XX";

        public string CDNConfigHash { get; set; }

        public string CDNConfigFirstTwo => !string.IsNullOrWhiteSpace(CDNConfigHash) && CDNConfigHash.Length >= 2
                                            ? CDNConfigHash.Substring(0, 2)
                                            : "XX";

        public string CDNConfigSecondTwo => !string.IsNullOrWhiteSpace(CDNConfigHash) && CDNConfigHash.Length >= 4
                                            ? CDNConfigHash.Substring(2, 2)
                                            : "XX";

        public string BuildID { get; set; }
        public string VersionName { get; set; }
        public Dictionary<string, IEnumerable<string>> BuildUrls { get; set; }
        public Dictionary<string, IEnumerable<string>> CDNUrls { get; set; }
    }
}