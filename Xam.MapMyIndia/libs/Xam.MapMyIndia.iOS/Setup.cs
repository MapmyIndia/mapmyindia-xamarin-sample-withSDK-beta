using System;
using Foundation;
using Xam.MapMyIndia.Models;
using Xam.MMI.iOS;

namespace Xam.MapMyIndia.iOS
{
	public class Setup
	{
		public static void Initialize(MMIAuthModel authModel)
		{
			MapmyIndiaAccountManager.MapSDKKey = new NSString(authModel.MapSDKKey);
			MapmyIndiaAccountManager.RestAPIKey = new NSString(authModel.RestAPIKey);
			MapmyIndiaAccountManager.AtlasClientId = new NSString(authModel.AtlasClientId);
			MapmyIndiaAccountManager.AtlasClientSecret = new NSString(authModel.AtlasClientSecret);
			MapmyIndiaAccountManager.AtlasGrantType = new NSString(authModel.AtlasGrantType);
			MapmyIndiaAccountManager.AtlasAPIVersion = new NSString(authModel.AtlasAPIVersion);
		}
	}
}
