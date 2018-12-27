using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Mapbox.Mapboxsdk.Maps;
using Xam.MapMyIndia.Interfaces;

namespace Xam.MapMyIndia.Droid.Controls
{
	public class MMIMapView : Java.Lang.Object, IMMIMapView
	{
		private Activity _context = null;

		private MapView _map;
		public MapView Map { 
			get {
				return _map;	
			}
			private set {
				value = _map;
			}
		}

		public MMIMapView(Activity context)
		{
			_context = context;
		}

		public View GetView(Bundle bundle)
		{
			var view = _context.LayoutInflater.Inflate(Resource.Layout.MMIMapLayout, null);
			_map = view.FindViewById<MapView>(Resource.Id.mapView);
			return view;
		}
	}
}
