using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Mapbox.Mapboxsdk.Annotations;
using Com.Mapbox.Mapboxsdk.Camera;
using Com.Mapbox.Mapboxsdk.Geometry;
using Com.Mapbox.Mapboxsdk.Maps;
using Java.Util;
using Xam.MapMyIndia.Droid.Controls;

namespace Sample.Droid
{
	[Activity(Label = "Polyline", ScreenOrientation = ScreenOrientation.Portrait)]
	public class PolylineActivity : Activity, IOnMapReadyCallback
	{
		private MapView _map = null;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main);

			var layout = FindViewById<LinearLayout>(Resource.Id.layout);

			var mmimapview = new MMIMapView(this);
			var view = mmimapview.GetView(savedInstanceState);
			view.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
																  ViewGroup.LayoutParams.MatchParent);
			layout.AddView(view);
			_map = mmimapview.Map;
			_map.OnCreate(savedInstanceState);
			_map.GetMapAsync(this);
		}

		protected override void OnStart()
		{
			base.OnStart();
			_map.OnStart();
		}

		protected override void OnStop()
		{
			base.OnStop();
			_map.OnStop();
		}

		public void OnMapError(int p0, string p1)
		{
			System.Diagnostics.Debug.WriteLine($"OnMapError - {p0}, {p1}");
		}

		public void OnMapReady(MapboxMap mapboxMap)
		{
			new Polyline().Draw(mapboxMap);
		}
	}

	public class Polyline
	{
		public Polyline()
		{
			
		}

		public void Draw(MapboxMap mapbox)
		{
			var list = new List<LatLng>();
			list.Add(new LatLng() { Latitude = 28.705436, Longitude = 77.100462 });
			list.Add(new LatLng() { Latitude = 28.705191, Longitude = 77.100784 });
			list.Add(new LatLng() { Latitude = 28.704646, Longitude = 77.101514 });
			list.Add(new LatLng() { Latitude = 28.704194, Longitude = 77.101171 });
			list.Add(new LatLng() { Latitude = 28.704083, Longitude = 77.101066 });
			list.Add(new LatLng() { Latitude = 28.703900, Longitude = 77.101318 });

			var arrayList = new ArrayList(list);
			var polyline = new PolylineOptions().AddAll(arrayList);
			polyline.InvokeColor(Color.ParseColor("#3bb2d0").ToArgb());
			mapbox.AddPolyline(polyline);

			LatLngBounds latLngBounds = new LatLngBounds.Builder().Includes(list).Build();
			mapbox.AnimateCamera(CameraUpdateFactory.NewLatLngBounds(latLngBounds, 70));
		}
	}
}
