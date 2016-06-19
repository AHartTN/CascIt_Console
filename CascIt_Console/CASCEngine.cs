using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CascIt_Console
{
    public class CASCEngine
    {
        //a list domains available with game data per region
        public const string CDNUrl = @"http://enus.patch.battle.net:1119/{0}/cdns";
        //a list of the current game version, build config, and cdn config per region
        public const string VersionsUrl = @"http://us.patch.battle.net:1119/{0}/versions";
        //similar to versions, but tailored for use by the Battle.net App background downloader process
        public const string BGDLVersionsUrl = @"http://us.patch.battle.net:1119/{0}/bgdl";
        //a blob file that regulates game functionality for the Battle.net App
        public const string GameUrl = @"http://us.patch.battle.net:1119/{0}/blob/game";
        //a blob file that regulates installer functionality for the game in the Battle.net App
        public const string InstallUrl = @"http://us.patch.battle.net:1119/{0}/blob/install";
        // A string template for building the tools config URL
        // IE: @"http://dist.blizzard.com.edgesuite.net/tools-pod/bna/cache/89/13/8913a4977117e47431af1433138ae354";
        public const string ToolsTemplateUrl = @"http://{0}/tools-pod/bna/cache/{1}";
        public const string test0 = @"http://dist.blizzard.com.edgesuite.net/tools-pod/bna/cache/20/BC/20BCA048990C8797A1A2FA280DF36F53";
        public const string test1 = @"http://dist.blizzard.com.edgesuite.net/tools-pod/bna/cache/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";
        public const string test2 = @"http://dist.blizzard.com.edgesuite.net/hs-pod/bna/cache/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";
        public const string test3 = @"http://dist.blizzard.com.edgesuite.net/hsb-pod/bna/cache/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";
        public const string test4 = @"http://dist.blizzard.com.edgesuite.net/tools-pod/bna/blobs/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";
        public const string test5 = @"http://dist.blizzard.com.edgesuite.net/hs-pod/bna/blobs/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";
        public const string test6 = @"http://dist.blizzard.com.edgesuite.net/hsb-pod/bna/blobs/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";
        public const string test7 = @"http://dist.blizzard.com.edgesuite.net/tools-pod/bna/blobs/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";





        public enum ProgramCode
        {
            hsb,
            //hsbt,
            Hero,
            HeroT,
            Storm,
            WoW,
            WoWT,
            WoW_Beta
        }

        public enum PathType
        {
            Config,
            Data,
            Patch
        }
        public class CDNResult
        {
            public CDNResult(string line)
            {
                Results = line.Split('|');
                Region = Results[0];
                QueryPath = Results[1];
                RawHosts = Results[2];
                Hosts = RawHosts.Split(' ');
            }
            public string[] Results { get; set; }
            public string Region { get; set; }
            public string QueryPath { get; set; }
            public string RawHosts { get; set; }
            public IEnumerable<string> Hosts { get; set; }
            public IEnumerable<string> Urls
            {
                get
                {
                    return Hosts.Where(w => !string.IsNullOrWhiteSpace(w))
                                .Select(s => string.Format("http://{0}/{1}/", s, QueryPath) + "{0}/{1}/{2}/{3}")
                                .Distinct();
                }
            }
        }

        public class CDNResults
        {
            public CDNResults(string url)
            {
                string response = Traffic.HTTPGET(url);

                if (response == null)
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
                get
                {
                    return Results.ToDictionary(k => k.Region, v => v.Urls);
                }
            }
        }

        public class VersionResults
        {
            public class VersionResult
            {
                public VersionResult(string line, CDNResults cdnResults)
                {
                    CDNUrls = new Dictionary<string, IEnumerable<string>>();
                    BuildUrls = new Dictionary<string, IEnumerable<string>>();

                    Results = line.Split('|');
                    Region = Results[0];
                    BuildConfigHash = Results[1];
                    CDNConfigHash = Results[2];
                    BuildID = Results[3];
                    VersionName = Results[4];

                    foreach (var cdnUrl in cdnResults.CDNUrls)
                    {
                        BuildUrls.Add(cdnUrl.Key, cdnUrl.Value.Select(s => string.Format(s, PathType.Config, BuildConfigFirstTwo, BuildConfigSecondTwo, BuildConfigHash).ToLower()));
                        CDNUrls.Add(cdnUrl.Key, cdnUrl.Value.Select(s => string.Format(s, PathType.Config, CDNConfigFirstTwo, CDNConfigSecondTwo, CDNConfigHash).ToLower()));
                    }
                }

                public string[] Results { get; set; }
                public string Region { get; set; }
                public string BuildConfigHash { get; set; }
                public string BuildConfigFirstTwo { get { return !string.IsNullOrWhiteSpace(BuildConfigHash) && BuildConfigHash.Length >= 2 ? BuildConfigHash.Substring(0, 2) : "XX"; } }
                public string BuildConfigSecondTwo { get { return !string.IsNullOrWhiteSpace(BuildConfigHash) && BuildConfigHash.Length >= 4 ? BuildConfigHash.Substring(2, 2) : "XX"; } }
                public string CDNConfigHash { get; set; }
                public string CDNConfigFirstTwo { get { return !string.IsNullOrWhiteSpace(CDNConfigHash) && CDNConfigHash.Length >= 2 ? CDNConfigHash.Substring(0, 2) : "XX"; } }
                public string CDNConfigSecondTwo { get { return !string.IsNullOrWhiteSpace(CDNConfigHash) && CDNConfigHash.Length >= 4 ? CDNConfigHash.Substring(2, 2) : "XX"; } }
                public string BuildID { get; set; }
                public string VersionName { get; set; }
                public Dictionary<string, IEnumerable<string>> BuildUrls { get; set; }
                public Dictionary<string, IEnumerable<string>> CDNUrls { get; set; }
            }

            public VersionResults(string url, CDNResults cdnResults)
            {
                CDNResults = cdnResults;
                string response = Traffic.HTTPGET(url);

                if (response == null)
                {
                    Results = null;
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
                    return Results.SelectMany(s => s.BuildUrls).ToDictionary(k => k.Key, v => v.Value);
                }
            }
            public Dictionary<string, IEnumerable<string>> CDNConfigUrls
            {
                get
                {
                    return Results.SelectMany(s => s.CDNUrls).ToDictionary(k => k.Key, v => v.Value);
                }
            }
        }

        public static string GetHashUrlSegment(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash) || !(hash.Length >= 4))
            {
                throw new ArgumentNullException("Hash string must not be null and must contain a valid value");
            }

            return string.Format(@"{0}/{1}/{2}", hash.Substring(0, 2), hash.Substring(2, 2), hash);
        }

        public static void ProcessEverything()
        {
            List<string> lines = new List<string>();
            string getUrl = string.Empty;
            foreach (ProgramCode programCode in Enum.GetValues(typeof(ProgramCode)))
            {
                getUrl = string.Format(CDNUrl, programCode).ToLower();
                Console.WriteLine("Retrieving CDN information for {0} from\r\n{1}", programCode, getUrl);
                CDNResults cdnResults = new CDNResults(getUrl);

                getUrl = string.Format(VersionsUrl, programCode).ToLower();
                Console.WriteLine("Retrieving Version information for {0} from\r\n{1}", programCode, getUrl);
                VersionResults versionResults = new VersionResults(getUrl, cdnResults);

                //foreach (CASCEngine.ProgramCode programCode in Enum.GetValues(typeof(CASCEngine.ProgramCode)))
                //{
                //    try
                //    {
                //        getUrl = string.Format(CASCEngine.BGDLVersionsUrl, programCode).ToLower();
                //        Console.WriteLine("Retrieving information from\r\n{0}", getUrl);
                //        response = Traffic.HTTPGET(getUrl);
                //        lines = response.Split('\n').ToList();
                //        lines.RemoveAt(0);
                //        foreach (string line in lines)
                //        {
                //            Console.WriteLine(line);
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e);
                //    }
                //    finally
                //    {
                //    }
                //}
                //foreach (CASCEngine.ProgramCode programCode in Enum.GetValues(typeof(CASCEngine.ProgramCode)))
                //{
                //    try
                //    {
                //        getUrl = string.Format(CASCEngine.GameUrl, programCode).ToLower();
                //        Console.WriteLine("Retrieving information from\r\n{0}", getUrl);
                //        response = Traffic.HTTPGET(getUrl);
                //        lines = response.Split('\n').ToList();
                //        lines.RemoveAt(0);
                //        foreach (string line in lines)
                //        {
                //            Console.WriteLine(line);
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e);
                //    }
                //    finally
                //    {
                //    }
                //}
                //foreach (CASCEngine.ProgramCode programCode in Enum.GetValues(typeof(CASCEngine.ProgramCode)))
                //{
                //    try
                //    {
                //        getUrl = string.Format(CASCEngine.InstallUrl, programCode).ToLower();
                //        Console.WriteLine("Retrieving information from\r\n{0}", getUrl);
                //        response = Traffic.HTTPGET(getUrl);
                //        lines = response.Split('\n').ToList();
                //        lines.RemoveAt(0);
                //        foreach (string line in lines)
                //        {
                //            Console.WriteLine(line);
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        Console.WriteLine(e);
                //    }
                //    finally
                //    {
                //    }
                //}

                if (versionResults != null && versionResults.Results != null && versionResults.Results.Any())
                {
                    foreach(var versionResult in versionResults.Results)
                    {
                        var region = versionResult.Region;
                        var buildID = versionResult.BuildID;
                        var versionName = versionResult.VersionName;
                        int totalBuildUrlCount = versionResult.BuildUrls.Count();
                        int totalCDNUrlCount = versionResult.CDNUrls.Count();

                        foreach (var urls in versionResult.CDNUrls)
                        {
                            foreach(string url in urls.Value)
                            {
                                string result = Traffic.HTTPGET(url);

                                if (!string.IsNullOrWhiteSpace(result))
                                {
                                    Console.WriteLine("\r*****************************************************");
                                    Console.WriteLine("*****                 CDN Config                *****");
                                    Console.WriteLine("*****************************************************");
                                    Console.WriteLine("***** Realm Region: {0} | File Region {1}\r\n***** Build: {1} | Version: {2}\r\n***** {3}", region, buildID, versionName, url);
                                    Console.WriteLine(result);
                                    Console.WriteLine("*****************************************************");
                                    //Console.WriteLine("\n{0} | {1} | B? {2} | C? {3} | Data Length: {4}\r\n{5}", region, buildID, isBuildConfig, isCDNConfig, result.Length, versionUrl);
                                }
                            }
                        }
                        foreach (var urls in versionResult.BuildUrls)
                        {
                            foreach (string url in urls.Value)
                            {
                                string result = Traffic.HTTPGET(url);

                                if (!string.IsNullOrWhiteSpace(result))
                                {
                                    Console.WriteLine("\r*****************************************************");
                                    Console.WriteLine("*****                Build Config               *****");
                                    Console.WriteLine("*****************************************************");
                                    Console.WriteLine("***** Realm Region: {0} | File Region {1}\r\n***** Build: {1} | Version: {2}\r\n***** {3}", region, buildID, versionName, url);
                                    Console.WriteLine(result);
                                    Console.WriteLine("*****************************************************");
                                    //Console.WriteLine("\n{0} | {1} | B? {2} | C? {3} | Data Length: {4}\r\n{5}", region, buildID, isBuildConfig, isCDNConfig, result.Length, versionUrl);
                                }
                            }
                        }
                    }
                }
                Console.ReadLine();
            }
        }
    }
}