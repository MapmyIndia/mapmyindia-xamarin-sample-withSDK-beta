using System;
namespace Xam.MapMyIndia.Models
{
	public class MMIAuthModel
	{
		public string MapSDKKey { get; set; } = "";
		public string RestAPIKey { get; set; } = "";
		public string AtlasClientId { get; set; } = "";
		public string AtlasClientSecret { get; set; } = "";
		public string AtlasGrantType { get; set; } = "client_credentials";
		public string AtlasAPIVersion { get; set; } = "";
	}
}
