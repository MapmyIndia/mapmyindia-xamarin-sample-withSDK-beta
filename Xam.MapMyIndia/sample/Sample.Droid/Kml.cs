using System;
using System.Collections.Generic;
using System.IO;
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
using Xam.MapMyIndia.Extensions;
using Xam.MapMyIndia.Models;

namespace Sample.Droid
{
	[Activity(Label = "KML", ScreenOrientation = ScreenOrientation.Portrait)]
	public class KmlActivity : Activity, IOnMapReadyCallback
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
			new Kml(this).Draw(mapboxMap);
		}
	}


	public class Kml
	{
		private Activity _activity = null;
		private MapboxMap _map = null;

		public Kml(Activity activity)
		{
			_activity = activity;
		}

		public void Draw(MapboxMap mapbox)
		{
			_map = mapbox;

			string Xml = string.Empty;
			using (StreamReader sr = new StreamReader(_activity.Assets.Open("Polygon.kml")))
			{
				Xml = sr.ReadToEnd();
			}

			var model = MMIKml.Read(Xml);

			ShowPolygon(model);
		}

		void ShowPolygon(MMIKmlModel model)
		{
			var list = new List<LatLng>();
			foreach (var p in model.PolygonCoordinates)
			{
				list.Add(new LatLng() { Latitude = p.Latitude, Longitude = p.Longitude });
			}

			var arrayList = new ArrayList(list);
			var polygon = new PolygonOptions().AddAll(arrayList);
			polygon.InvokeFillColor(Color.ParseColor("#753bb2d0").ToArgb());
			_map.AddPolygon(polygon);

			LatLngBounds latLngBounds = new LatLngBounds.Builder().Includes(list).Build();
			_map.AnimateCamera(CameraUpdateFactory.NewLatLngBounds(latLngBounds, 70));
		}
	}
}
