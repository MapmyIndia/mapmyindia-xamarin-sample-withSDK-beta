using System;
using Android.Animation;
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
using Java.Lang;
using Xam.MapMyIndia.Droid.Controls;
using Xam.MapMyIndia.Droid.Extensions;

namespace Sample.Droid
{
	[Activity(Label = "Animate Marker On Points", ScreenOrientation = ScreenOrientation.Portrait)]
	public class AnimateMarkerPointsActivity : Activity, IOnMapReadyCallback
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
			new AnimateMarkerPoints().Start(mapboxMap, _map);
		}
	}

	public class AnimateMarkerPoints
	{
		MapboxMap mapboxMap = null;
		Marker _marker = null;

		public void Start(MapboxMap mapView, MapView map)
		{
			mapboxMap = mapView;

			var latlng = new LatLng() { Latitude = 25.321684, Longitude = 82.987289 };

			var marker = new MarkerOptions();
			marker.SetPosition(latlng);
			marker.SetTitle("Test Info Window");
			_marker = mapView.AddMarker(marker);

			//// set camera pos
			var camPos = new CameraPosition.Builder().Target(latlng).Zoom(5).Tilt(0).Build();
			mapView.CameraPosition = camPos;

			new Handler().PostDelayed(StartAnim, 5000);
		}

		void StartAnim()
		{
			var moveTo = new LatLng() { Latitude = 28.321684, Longitude = 85.987289 };

			var markerAnimator = ObjectAnimator.OfObject(_marker, "position", new MMILatLngEvaluator(), _marker.Position, moveTo);
			markerAnimator.SetDuration(5000);
			markerAnimator.Start();
		}
	}
}

