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
	[Activity(Label = "Polygon", ScreenOrientation = ScreenOrientation.Portrait)]
	public class PolygonActivity : Activity, IOnMapReadyCallback
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
			new Polygon().Draw(mapboxMap);
		}
	}

	public class Polygon
	{
		public Polygon()
		{
			
		}

		public void Draw(MapboxMap mapbox)
		{
			var list = new List<LatLng>();
			list.Add(new LatLng() { Latitude = 28.703900, Longitude = 77.101318 });
			list.Add(new LatLng() { Latitude = 28.703331, Longitude = 77.10215 });
			list.Add(new LatLng() { Latitude = 28.703905, Longitude = 77.102761 });
			list.Add(new LatLng() { Latitude = 28.704248, Longitude = 77.102370 });

			var arrayList = new ArrayList(list);
			var polygon = new PolygonOptions().AddAll(arrayList);
			polygon.InvokeFillColor(Color.ParseColor("#753bb2d0").ToArgb());
			mapbox.AddPolygon(polygon);

			LatLngBounds latLngBounds = new LatLngBounds.Builder().Includes(list).Build();
			mapbox.AnimateCamera(CameraUpdateFactory.NewLatLngBounds(latLngBounds, 70));
		}
	}
}
