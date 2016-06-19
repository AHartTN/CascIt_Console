using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CascIt_Console
{
    class Program
    {
        public class BlizzardUrl
        {
            public const string PTRBattleNetPatchUrl = @"http://public-test.patch.battle.net:1119/patch";
            public const string BattleNetPatchUrl = @"http://us.patch.battle.net:1119/patch";
            public const string HearthConfigUrl = @"http://dist.blizzard.com.edgesuite.net/hs-pod/beta/{0}/config_hsb_na_{1}.xml";
            public const string HearthBaseCDN = @"http://dist.blizzard.com.edgesuite.net/hs-pod/beta/{0}/{1}.direct/";
            public const string HearthCDNUrl = @"http://dist.blizzard.com.edgesuite.net/hs-pod/beta/{0}/{1}.direct/hsb-{2}-{3}.mfil";
        }

        public class BlizzardPayload
        {
            public const string AgentPayload = "<version program=\"Agnt\"><record program=\"Agnt\" component=\"cdn\" version=\"1\" /><record program=\"Agnt\" component=\"cfg\" version=\"1\" /><record program=\"Agnt\" component=\"Win\" version=\"1\" /><record program=\"Agnt\" component=\"blob\" version=\"1\" /></version>";
            public const string BattleNetPayload = "<version program=\"Bna\"><record program=\"Agnt\" component=\"cdn\" version=\"1\" /><record program=\"Agnt\" component=\"cfg\" version=\"1\" /><record program=\"Bna\" component=\"Win\" version=\"1\" /><record program=\"Bna\" component=\"blob\" version=\"1\" /></version>";
            public const string HearthPayload = "<version program=\"HSB\"><record program=\"Agnt\" component=\"cdn\" version=\"1\" /><record program=\"HSB\" component=\"enUS\" version=\"1\" /><record program=\"HSB\" component=\"blob\" version=\"1\" /></version>";
            public const string HearthVersionPayload = "<version program=\"HSB\"><record program=\"Agnt\" component=\"cdn\" version=\"1\" /><record program=\"HSB\" component=\"enUS\" version=\"1\" /><record program=\"HSB\" component=\"blob\" version=\"{0}\" /></version>";
        }

        public class BlizzardPTRPayload
        {
            public const string AgentPayload = "<version program=\"AgtB\"><record program=\"Agnt\" component=\"cdn\" version=\"1\" /><record program=\"Agnt\" component=\"cfg\" version=\"1\" /><record program=\"AgtB\" component=\"Win\" version=\"1\" /><record program=\"AgtB\" component=\"blob\" version=\"1\" /></version>";
        }

        static void Main(string[] args)
        {
            Console.SetBufferSize(Console.BufferWidth * 2, Console.BufferHeight * 20);
            //string[] urls = { CASCEngine.test0, CASCEngine.test1, CASCEngine.test2, CASCEngine.test3, CASCEngine.test4, CASCEngine.test5, CASCEngine.test6, CASCEngine.test7 };

            //foreach(string url in urls)
            //{
            //    string result = Traffic.HTTPGET(url);
            //    if (!result.IsNullOrWhiteSpace())
            //    {
            //        using (StreamWriter sw = new StreamWriter(File.OpenWrite(string.Format(@"C:\Users\Anthony\Desktop\UrlOutput\Output_{0}.txt", "XXXX"))))
            //        {
            //            sw.WriteLine(result);
            //        }
            //        Console.WriteLine(result);
            //    }
            //}

            CASCEngine.ProcessEverything();
            Console.ReadKey();
        }
    }
}
