namespace CascIt_Console
{
	public class BlizzardUrl
    {

        //a list domains available with game data per region
        //public const string CDNUrl = @"http://enus.patch.battle.net:1119/{0}/cdns";
	    public const string CDNUrl = @"http://us.patch.battle.net:1119/{0}/cdns";
	    
        //a list of the current game version, build config, and cdn config per region
	    public const string VersionsUrl = @"http://us.patch.battle.net:1119/{0}/versions";
	    
        //similar to versions, but tailored for use by the Battle.net App background downloader process
	    public const string BGDLVersionsUrl = @"http://us.patch.battle.net:1119/{0}/bgdl";

        //a blob file that regulates game functionality for the Battle.net App
        public const string BlobUrl = @"http://us.patch.battle.net:1119/{0}/blobs";

        //a blob file that regulates game functionality for the Battle.net App
        public const string GameUrl = @"http://us.patch.battle.net:1119/{0}/blob/game";

        //a blob file that regulates installer functionality for the game in the Battle.net App
        public const string InstallUrl = @"http://us.patch.battle.net:1119/{0}/blob/install";

	    public const string BuildConfigUrl = @"http://{0}/{1}/{2}/{3}/{4}/{5}/{6}";
	    
  //      // A string template for building the tools config URL
  //      // IE: @"http://dist.blizzard.com.edgesuite.net/tools-pod/bna/cache/89/13/8913a4977117e47431af1433138ae354";
	 //   public const string ToolsTemplateUrl = @"http://{0}/tools-pod/bna/cache/{1}";

  //      public const string test0 =
  //          @"http://dist.blizzard.com.edgesuite.net/tools-pod/bna/cache/20/BC/20BCA048990C8797A1A2FA280DF36F53";

  //      public const string test1 =
  //          @"http://dist.blizzard.com.edgesuite.net/tools-pod/bna/cache/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";

  //      public const string test2 =
  //          @"http://dist.blizzard.com.edgesuite.net/hs-pod/bna/cache/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";

  //      public const string test3 =
  //          @"http://dist.blizzard.com.edgesuite.net/hsb-pod/bna/cache/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";

  //      public const string test4 =
  //          @"http://dist.blizzard.com.edgesuite.net/tools-pod/bna/blobs/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";

  //      public const string test5 =
  //          @"http://dist.blizzard.com.edgesuite.net/hs-pod/bna/blobs/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";

  //      public const string test6 =
  //          @"http://dist.blizzard.com.edgesuite.net/hsb-pod/bna/blobs/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";

  //      public const string test7 =
  //          @"http://dist.blizzard.com.edgesuite.net/tools-pod/bna/blobs/CC/8E/CC8E779FF81A4C14E600A1CFFD984911";

  //      public const string PTRBattleNetPatchUrl = @"http://public-test.patch.battle.net:1119/patch";
		//public const string BattleNetPatchUrl = @"http://us.patch.battle.net:1119/patch";
		//public const string HearthConfigUrl = @"http://dist.blizzard.com.edgesuite.net/hs-pod/beta/{0}/config_hsb_na_{1}.xml";
		//public const string HearthBaseCDN = @"http://dist.blizzard.com.edgesuite.net/hs-pod/beta/{0}/{1}.direct/";

		//public const string HearthCDNUrl =
		//	@"http://dist.blizzard.com.edgesuite.net/hs-pod/beta/{0}/{1}.direct/hsb-{2}-{3}.mfil";
    }
}