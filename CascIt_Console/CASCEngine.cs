namespace CascIt_Console
{
    #region Imports

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion
    public class CASCEngine
    {
        public static List<string> DetectedRegions = new List<string>();

        public enum RegionCode
        {
            cn,
            eu,
            kr,
            sg,
            tw,
            us,
        }

        public enum PathType
        {
            Config,
            Data,
            Patch
        }

        public enum ProgramCode
        {
            agent,
            //agnt,
            bna,
            bnt,
            catalogs,
            //client,
            clnt,
            d3,
            d3cn,
            d3t,
            //dst2a,
            //demo,
            hero,
            herot,
            heroc,
            hsb,
            //hsbt,
            //hst,
            pro,
            prot,
            proc,
            prodev,
            s1,
            s1a,
            s1t,
            //sc2,
            s2,
            s2t,
            s2b,
            //test,
            storm,
            //war3,
            w3,
            wow,
            wowt,
            wow_beta
        }

        #region Program Codes

        public static Dictionary<ProgramCode, string> Programs = new Dictionary<ProgramCode, string>
        {
            {ProgramCode.agent, "Battle.Net Agent"},
            //{ProgramCode.agnt, "Battle.Net Agent?"},
            {ProgramCode.bna, "Battle.Net Application"},
            {ProgramCode.bnt, "Heroes of the Storm Alpha (Deprecated)"},
            {ProgramCode.catalogs, "Catalog" },
            //{ProgramCode.client, "Client (Unknown)"},
            {ProgramCode.clnt, "Client (Deprecated)"},
            {ProgramCode.d3, "Diablo III"},
            {ProgramCode.d3cn, "Diablo III (China)"},
            {ProgramCode.d3t, "Diablo III Test"},
            //{ProgramCode.demo, "{Demo} (Partial?)"},
            //{ProgramCode.dst2a, "Destiny 2 Alpha (Coming Soon)"},
            {ProgramCode.hero, "Heroes of the Storm"},
            {ProgramCode.herot, "Heroes of the Storm Test"},
            {ProgramCode.heroc, "Heroes of the Storm Tournament"},
            {ProgramCode.hsb, "Hearthstone"},
            //{ProgramCode.hsbt, "Hearthstone Test (Unknown)"},
            //{ProgramCode.hst, "Hearthstone Test (Partial)"},
            {ProgramCode.pro, "Overwatch"},
            {ProgramCode.prot, "Overwatch Test"},
            {ProgramCode.proc, "Overwatch Tournament"},
            {ProgramCode.prodev, "Overwatch Development (Encrypted)"},
            {ProgramCode.s1, "Starcraft I"},
            {ProgramCode.s1a, "Starcraft I Alpha (Encrypted)"},
            {ProgramCode.s1t, "Starcraft I Beta"},
            //{ProgramCode.sc2, "Starcraft II (Deprecated)"},
            {ProgramCode.s2, "Starcraft II Retail"},
            {ProgramCode.s2t, "Starcraft II Test (Deprecated)"},
            {ProgramCode.s2b, "Starcraft II Beta (Deprecated)"},
            //{ProgramCode.test, "Test (Unknown\\Deprecated)"},
            {ProgramCode.storm, "Heroes of the Storm (Deprecated)"},
            //{ProgramCode.war3, "Warcraft III (Old)"},
            {ProgramCode.w3, "Warcraft III"},
            {ProgramCode.wow, "World of Warcraft"},
            {ProgramCode.wowt, "World of Warcraft Test"},
            {ProgramCode.wow_beta, "World of Warcraft Beta"}
        };

        #endregion Program Codes

        #region Region Codes
        public Dictionary<RegionCode, string> Regions = new Dictionary<RegionCode, string>
        {
            {RegionCode.cn, "China"},
            {RegionCode.eu, "Europe"},
            {RegionCode.kr, "Korea"},
            {RegionCode.sg, "Singapore"},
            {RegionCode.tw, "Taiwan" },
            {RegionCode.us, "United States"},
        };
        #endregion Region Codes

        public static string GetHashUrlSegment(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash)
                || !(hash.Length >= 4))
            {
                throw new ArgumentNullException(nameof(hash), "Hash string must not be null and must contain a valid value");
            }

            return $@"{hash.Substring(0, 2)}/{hash.Substring(2, 2)}/{hash}";
        }

        public static void ProcessProgram(ProgramCode programCode)
        {
            string cdnUrl = string.Format(BlizzardUrl.CDNUrl, programCode).ToLower();
            string versionUrl = string.Format(BlizzardUrl.VersionsUrl, programCode).ToLower();

            Console.WriteLine($"Retrieving CDN information for {programCode} from\r\n{cdnUrl}");
            CDNResults cdnResults = new CDNResults(cdnUrl);

            Console.WriteLine($"Retrieving Version information for {programCode} from\r\n{versionUrl}");
            VersionResults versionResults = new VersionResults(versionUrl, cdnResults);

            if (cdnResults.Results == null || cdnResults.CDNUrls == null ||
                !cdnResults.Results.Any() || !cdnResults.CDNUrls.Any())
            {
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\r\nAN ERROR HAS OCCURRED! No CDN Results were returned for {programCode} from {cdnUrl}!");
                Console.ForegroundColor = originalColor;
            }
            else
            {
                foreach (var result in cdnResults.Results)
                {
                    DetectedRegions.Add(result.Region);
                }
            }

            if (versionResults.Results == null || versionResults.BuildConfigUrls == null ||
                versionResults.CDNConfigUrls == null ||
                !versionResults.Results.Any() || !versionResults.BuildConfigUrls.Any() ||
                versionResults.CDNConfigUrls.Any())
            {
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(
                    $"\r\nAN ERROR HAS OCCURRED! No Version Results were returned for {programCode} from {versionUrl}!");
                Console.ForegroundColor = originalColor;
            }
            else
            {
                foreach (var result in versionResults.Results)
                {
                    DetectedRegions.Add(result.Region);
                }
            }

            Console.Clear();

            foreach (var region in DetectedRegions.Distinct().OrderBy(o => o))
                Console.WriteLine(region);

            //if (versionResults.Results != null
            //    && versionResults.Results.Any())
            //{
            //    foreach (VersionResult versionResult in versionResults.Results)
            //    {
            //        string region = versionResult.Region;
            //        string buildID = versionResult.BuildID;
            //        string versionName = versionResult.VersionName;
            //        int totalBuildUrlCount = versionResult.BuildUrls.Count;
            //        int totalCDNUrlCount = versionResult.CDNUrls.Count;

            //        foreach (KeyValuePair<string, IEnumerable<string>> urls in versionResult.CDNUrls)
            //        {
            //            foreach (string url in urls.Value)
            //            {
            //                string result = Traffic.HTTPGET(url);

            //                if (!string.IsNullOrWhiteSpace(result))
            //                {
            //                    Console.WriteLine("\r*****************************************************");
            //                    Console.WriteLine("*****                 CDN Config                *****");
            //                    Console.WriteLine("*****************************************************");
            //                    Console.WriteLine("***** Realm Region: {0} | File Region {1}\r\n***** Build: {1} | Version: {2}\r\n***** {3}",
            //                        region,
            //                        buildID,
            //                        versionName,
            //                        url);
            //                    Console.WriteLine(result);
            //                    Console.WriteLine("*****************************************************");
            //                    //Console.WriteLine("\n{0} | {1} | B? {2} | C? {3} | Data Length: {4}\r\n{5}", region, buildID, isBuildConfig, isCDNConfig, result.Length, versionUrl);
            //                }
            //            }
            //        }
            //        foreach (KeyValuePair<string, IEnumerable<string>> urls in versionResult.BuildUrls)
            //        {
            //            foreach (string url in urls.Value)
            //            {
            //                string result = Traffic.HTTPGET(url);

            //                if (!string.IsNullOrWhiteSpace(result))
            //                {
            //                    Console.WriteLine("\r*****************************************************");
            //                    Console.WriteLine("*****                Build Config               *****");
            //                    Console.WriteLine("*****************************************************");
            //                    Console.WriteLine("***** Realm Region: {0} | File Region {1}\r\n***** Build: {1} | Version: {2}\r\n***** {3}",
            //                        region,
            //                        buildID,
            //                        versionName,
            //                        url);
            //                    Console.WriteLine(result);
            //                    Console.WriteLine("*****************************************************");
            //                    //Console.WriteLine("\n{0} | {1} | B? {2} | C? {3} | Data Length: {4}\r\n{5}", region, buildID, isBuildConfig, isCDNConfig, result.Length, versionUrl);
            //                }
            //            }
            //        }
            //    }
            //}

            //Console.WriteLine($"Application has finished parsing {Programs[programCode]}.\r\nPress enter to continue.");
            //Console.ReadLine();
        }

        public static void ProcessEverything()
        {
            foreach (ProgramCode programCode in Enum.GetValues(typeof(ProgramCode)))
            {
                ProcessProgram(programCode);
            }
        }
    }
}