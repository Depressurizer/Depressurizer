using System.Collections.Generic;
using Depressurizer.Core.Enums;
using Newtonsoft.Json;

#pragma warning disable 1591

namespace Depressurizer.Core.Models
{
	// ReSharper disable once InconsistentNaming
	public sealed class HLTB_RawData
	{
		#region Public Properties

		[JsonProperty("ExcludedCount")]
		public long ExcludedCount { get; set; }

		[JsonProperty("Games")]
		public List<Game> Games { get; set; }

		[JsonProperty("PartialCache")]
		public bool PartialCache { get; set; }

		[JsonProperty("PersonaInfo")]
		public PersonaInfo PersonaInfo { get; set; }

		[JsonProperty("Totals")]
		public Totals Totals { get; set; }

		#endregion
	}

	public class Game
	{
		#region Public Properties

		[JsonProperty("Playtime")]
		public long Playtime { get; set; }

		[JsonProperty("SteamAppData")]
		public SteamAppData SteamAppData { get; set; }

		#endregion
	}

	public class SteamAppData
	{
		#region Public Properties

		[JsonProperty("AppType")]
		public AppType AppType { get; set; }

		[JsonProperty("Genres")]
		public List<string> Genres { get; set; }

		[JsonProperty("HltbInfo")]
		public HltbInfo HltbInfo { get; set; }

		[JsonProperty("MetacriticScore")]
		public long MetacriticScore { get; set; }

		[JsonProperty("ReleaseYear")]
		public long ReleaseYear { get; set; }

		[JsonProperty("SteamAppId")]
		public int SteamAppId { get; set; }

		[JsonProperty("SteamName")]
		public string SteamName { get; set; }

		[JsonProperty("VerifiedGame")]
		public bool VerifiedGame { get; set; }

		#endregion
	}

	public class HltbInfo
	{
		#region Public Properties

		[JsonProperty("CompletionistTtb")]
		public int CompletionistTtb { get; set; }

		[JsonProperty("CompletionistTtbImputed")]
		public bool CompletionistTtbImputed { get; set; }

		[JsonProperty("ExtrasTtb")]
		public int ExtrasTtb { get; set; }

		[JsonProperty("ExtrasTtbImputed")]
		public bool ExtrasTtbImputed { get; set; }

		[JsonProperty("Id")]
		public long Id { get; set; }

		[JsonProperty("MainTtb")]
		public int MainTtb { get; set; }

		[JsonProperty("MainTtbImputed")]
		public bool MainTtbImputed { get; set; }

		[JsonProperty("Name")]
		public string Name { get; set; }

		#endregion
	}

	public class PersonaInfo
	{
		#region Public Properties

		[JsonProperty("Avatar")]
		public string Avatar { get; set; }

		[JsonProperty("PersonaName")]
		public string PersonaName { get; set; }

		#endregion
	}

	public class Totals
	{
		#region Public Properties

		[JsonProperty("CompletionistCompleted")]
		public long CompletionistCompleted { get; set; }

		[JsonProperty("CompletionistRemaining")]
		public long CompletionistRemaining { get; set; }

		[JsonProperty("CompletionistTtb")]
		public long CompletionistTtb { get; set; }

		[JsonProperty("ExtrasCompleted")]
		public long ExtrasCompleted { get; set; }

		[JsonProperty("ExtrasRemaining")]
		public long ExtrasRemaining { get; set; }

		[JsonProperty("ExtrasTtb")]
		public long ExtrasTtb { get; set; }

		[JsonProperty("MainCompleted")]
		public long MainCompleted { get; set; }

		[JsonProperty("MainRemaining")]
		public long MainRemaining { get; set; }

		[JsonProperty("MainTtb")]
		public long MainTtb { get; set; }

		[JsonProperty("Playtime")]
		public long Playtime { get; set; }

		[JsonProperty("PlaytimesByAppType")]
		public PlaytimesByAppType PlaytimesByAppType { get; set; }

		[JsonProperty("PlaytimesByGenre")]
		public Dictionary<string, long> PlaytimesByGenre { get; set; }

		[JsonProperty("PlaytimesByMetacritic")]
		public Dictionary<string, long> PlaytimesByMetacritic { get; set; }

		[JsonProperty("PlaytimesByReleaseYear")]
		public Dictionary<string, long> PlaytimesByReleaseYear { get; set; }

		#endregion
	}

	public class PlaytimesByAppType
	{
		#region Public Properties

		[JsonProperty("dlc")]
		public long Dlc { get; set; }

		[JsonProperty("game")]
		public long Game { get; set; }

		[JsonProperty("mod")]
		public long Mod { get; set; }

		#endregion
	}
}
