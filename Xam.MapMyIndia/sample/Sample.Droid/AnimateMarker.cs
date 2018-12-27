using System;
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
using Xam.MapMyIndia.Droid.Controls;

namespace Sample.Droid
{
	[Activity(Label = "Animate Marker", ScreenOrientation = ScreenOrientation.Portrait)]
	public class AnimateMarkerActivity : Activity, IOnMapReadyCallback
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
			new AnimateMarker().Start(mapboxMap, _map);
		}
	}


	public class AnimateMarker : Java.Lang.Object, Android.Views.View.IOnTouchListener
	{
		MapboxMap mapboxMap = null;
		Marker _marker = null;

		public AnimateMarker()
		{
			
		}

		public void Start(MapboxMap mapView, MapView map)
		{
			mapboxMap = mapView;

			var latlng = new LatLng() { Latitude = 25.321684, Longitude = 82.987289 };

			var marker = new MarkerOptions();
			marker.SetPosition(latlng);
			marker.SetTitle("Test Info Window");
			_marker = mapView.AddMarker(marker);

			//// set camera pos
			var camPos = new CameraPosition.Builder().Target(latlng).Zoom(10).Tilt(0).Build();
			mapView.CameraPosition = camPos;

			map.SetOnTouchListener(this);
		}

		public bool OnTouch(View v, MotionEvent e)
		{
			var latlong = mapboxMap.Projection.FromScreenLocation(new PointF(x: e.GetX(), y: e.GetY()));
			_marker.Position = latlong;  

			return true;
		}
	}
}
