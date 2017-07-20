using System;
using System.Collections.Generic;
using System.Linq;

namespace CascIt_Console
{
    public class VersionResults
    {
        public VersionResults(string url, CDNResults cdnResults)
        {
            CDNResults = cdnResults;
            string response = Traffic.HTTPGET(url);

            if (response == null)
            {
                Results = null;
                Console.WriteLine("Zero (0) Version results retrieved! Probably a bad program code?");
                return;
            }

            List<string> lines = response.Replace("\r", "")
                .Split('\n')
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .ToList();

            lines.RemoveAt(0);

            Results = lines.Select(s => new VersionResult(s, cdnResults));
            Console.WriteLine("{0} Version results retrieved!", Results.Count());
        }

        public CDNResults CDNResults { get; set; }
        public IEnumerable<VersionResult> Results { get; set; }

        public Dictionary<string, IEnumerable<string>> BuildConfigUrls
        {
            get
            {
                Dictionary<string, IEnumerable<string>> result = new Dictionary<string, IEnumerable<string>>();

                IEnumerable<string> regions = Results.SelectMany(s => s.BuildUrls.Keys).Distinct().OrderBy(o => o);

                foreach (var region in regions)
                {
                    var results = Results.SelectMany(s => s.BuildUrls[region]);

                    if (result.ContainsKey(region))
                        result[region] = result[region].Union(results).Distinct();
                    else
                        result.Add(region, results.Distinct());
                }

                return result;
            }
        }

        public Dictionary<string, IEnumerable<string>> CDNConfigUrls
        {
            get
            {
                Dictionary<string, IEnumerable<string>> result = new Dictionary<string, IEnumerable<string>>();

                IEnumerable<string> regions = Results.SelectMany(s => s.CDNUrls.Keys).Distinct().OrderBy(o => o);

                foreach (var region in regions)
                {
                    var results = Results.SelectMany(s => s.CDNUrls[region]);

                    if (result.ContainsKey(region))
                        result[region] = result[region].Union(results).Distinct();
                    else
                        result.Add(region, results.Distinct());
                }

                return result;
            }
        }
    }
}