using System;
using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Mapbox.Mapboxsdk.Annotations;
using Com.Mapbox.Mapboxsdk.Camera;
using Com.Mapbox.Mapboxsdk.Geometry;
using Com.Mapbox.Mapboxsdk.Maps;
using Xam.MapMyIndia.Droid.Controls;

namespace Sample.Droid
{
	[Activity(Label = "Multiple Markers", ScreenOrientation = ScreenOrientation.Portrait)]
	public class MarkersActivity : Activity, IOnMapReadyCallback
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
			new Markers(this).Add(mapboxMap);
		}
	}

	public class Markers : Java.Lang.Object, MapboxMap.IOnMarkerClickListener
	{
		private MapboxMap _mapbox = null;
		private Activity _activity = null;

		public Markers(Activity activity)
		{
			_activity = activity;
		}

		public void Add(MapboxMap mapbox)
		{
			_mapbox = mapbox;

			_mapbox.SetOnMarkerClickListener(this);

			var list = new List<LatLng>();
			list.Add(new LatLng() { Latitude = 25.321684, Longitude = 82.987289 });
			list.Add(new LatLng() { Latitude = 26.321684, Longitude = 83.987289 });
			list.Add(new LatLng() { Latitude = 27.321684, Longitude = 84.987289 });
			list.Add(new LatLng() { Latitude = 28.321684, Longitude = 85.987289 });

			foreach (var pos in list)
			{
				var latlng = new LatLng() { Latitude = pos.Latitude, Longitude = pos.Longitude };

				var marker = new MarkerOptions();
				marker.SetPosition(latlng);
				mapbox.AddMarker(marker);
			}

			//set camera pos
			var camPos = new CameraPosition.Builder().Target(list[1]).Zoom(5).Tilt(0).Build();
			mapbox.CameraPosition = camPos;

			// remove all markers after 5 seconds.
			//new Handler().PostDelayed(Remove, 5000);
		}

		private void Remove()
		{
			_mapbox.Clear();
			System.Diagnostics.Debug.WriteLine($"Remove");
		}

		public bool OnMarkerClick(Marker p0)
		{
			var msg = $"you clicked marker {p0.Title}, {p0.Position.Latitude}, {p0.Position.Longitude}";

			Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(_activity);
			AlertDialog alert = dialog.Create();
			alert.SetTitle("MapMyIndia");
			alert.SetMessage(msg);
			alert.SetButton("OK", (c, ev) =>
			{
				// Ok button click task  
			});
			alert.Show();

			return false;
		}
	}
}
