using System;
using System.Collections.Generic;
using System.Linq;

namespace CascIt_Console
{
    public class CDNResults
    {
        public CDNResults(string url)
        {
            string response = Traffic.HTTPGET(url);

            if (string.IsNullOrWhiteSpace(response))
            {
                Results = null;
                return;
            }

            List<string> lines = response.Replace("\r", "")
                .Split('\n')
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToList();
            lines.RemoveAt(0);
            Results = lines.Select(s => new CDNResult(s));
            Console.WriteLine("{0} CDN results retrieved!", Results.Count());
        }

        public IEnumerable<CDNResult> Results { get; set; }

        public Dictionary<string, IEnumerable<string>> CDNUrls
        {
            get {
                return Results == null || !Results.Any()
                    ? new Dictionary<string, IEnumerable<string>>()
                    : Results.ToDictionary(k => k.Region, v => v.Urls);
            }
        }
    }
}