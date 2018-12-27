using System;
using Android.Animation;
using Com.Mapbox.Mapboxsdk.Geometry;

namespace Xam.MapMyIndia.Droid.Extensions
{
	public class MMILatLngEvaluator : Java.Lang.Object, ITypeEvaluator
	{
		private LatLng latLng = new LatLng();

		public Java.Lang.Object Evaluate(float fraction, Java.Lang.Object startValue, Java.Lang.Object endValue)
		{
			var start = (LatLng)startValue;
			var end = (LatLng)endValue;

			latLng.Latitude = start.Latitude + ((end.Latitude - start.Latitude) * fraction);
			latLng.Longitude = start.Longitude + ((end.Longitude - start.Longitude) * fraction);

			return latLng;
		}
	}
}
