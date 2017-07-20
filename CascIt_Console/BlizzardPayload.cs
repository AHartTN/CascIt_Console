namespace CascIt_Console
{
	public class BlizzardPayload
	{
		public const string AgentPayload =
			"<version program=\"Agnt\"><record program=\"Agnt\" component=\"cdn\" version=\"1\" /><record program=\"Agnt\" component=\"cfg\" version=\"1\" /><record program=\"Agnt\" component=\"Win\" version=\"1\" /><record program=\"Agnt\" component=\"blob\" version=\"1\" /></version>";

		public const string BattleNetPayload =
			"<version program=\"Bna\"><record program=\"Agnt\" component=\"cdn\" version=\"1\" /><record program=\"Agnt\" component=\"cfg\" version=\"1\" /><record program=\"Bna\" component=\"Win\" version=\"1\" /><record program=\"Bna\" component=\"blob\" version=\"1\" /></version>";

		public const string HearthPayload =
			"<version program=\"HSB\"><record program=\"Agnt\" component=\"cdn\" version=\"1\" /><record program=\"HSB\" component=\"enUS\" version=\"1\" /><record program=\"HSB\" component=\"blob\" version=\"1\" /></version>";

		public const string HearthVersionPayload =
			"<version program=\"HSB\"><record program=\"Agnt\" component=\"cdn\" version=\"1\" /><record program=\"HSB\" component=\"enUS\" version=\"1\" /><record program=\"HSB\" component=\"blob\" version=\"{0}\" /></version>";
	}
}