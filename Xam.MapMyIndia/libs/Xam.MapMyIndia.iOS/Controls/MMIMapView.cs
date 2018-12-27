using System;
using CoreLocation;
using Foundation;
using Mapbox;
using UIKit;
using Xam.MapMyIndia.Interfaces;

namespace Xam.MapMyIndia.iOS.Controls
{
	public class MMIMapView : MapmyIndiaMapView, IMMIMapView
	{
		public MMIMapView(CoreGraphics.CGRect frame) : base()
		{
			this.Frame = frame;

			ShowsUserLocation = true;
			UserTrackingMode = MGLUserTrackingMode.FollowWithCourse;
		}
	}
}
