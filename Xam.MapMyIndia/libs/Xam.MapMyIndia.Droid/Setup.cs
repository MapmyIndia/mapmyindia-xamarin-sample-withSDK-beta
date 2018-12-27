using System;
using Android.App;
using Android.OS;
using Com.Mapbox.Mapboxsdk;
using Com.Mmi.Services.Account;
using Xam.MapMyIndia.Models;

namespace Xam.MapMyIndia.Droid
{
	public class Setup
	{
		public static void Initialize(Application app, MMIAuthModel authModel)
		{
			MapmyIndiaAccountManager.Instance.RestAPIKey = authModel.RestAPIKey;
			MapmyIndiaAccountManager.Instance.MapSDKKey = authModel.MapSDKKey;
			MapmyIndiaAccountManager.Instance.AtlasClientId = authModel.AtlasClientId;
			MapmyIndiaAccountManager.Instance.AtlasClientSecret = authModel.AtlasClientSecret;
			MapmyIndiaAccountManager.Instance.AtlasGrantType = authModel.AtlasGrantType;

			MapmyIndia.GetInstance(app);
		}
	}
}
