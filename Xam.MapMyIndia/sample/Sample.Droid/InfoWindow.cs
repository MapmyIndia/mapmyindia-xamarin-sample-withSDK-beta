using System;
using System.Collections.Generic;
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
using static Com.Mapbox.Mapboxsdk.Maps.MapboxMap;

namespace Sample.Droid
{
	[Activity(Label = "Info Window", ScreenOrientation = ScreenOrientation.Portrait)]
	public class InfoWindowActivity : Activity, IOnMapReadyCallback
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
			new InfoWindow(this).Set(mapboxMap);
		}
	}

	public class InfoWindow
	{
		private Activity _activity = null;

		public InfoWindow(Activity a)
		{
			_activity = a;
		}

		public void Set(MapboxMap mapView)
		{
			var list = new List<LatLng>();
			list.Add(new LatLng() { Latitude = 25.321684, Longitude = 82.987289 });

			foreach (var pos in list)
			{
				var latlng = new LatLng() { Latitude = pos.Latitude, Longitude = pos.Longitude };

				var marker = new MarkerOptions();
				marker.SetPosition(latlng);
				marker.SetTitle("Test Info Window");

				IconFactory iconFactory = IconFactory.GetInstance(_activity);
				Icon icon = iconFactory.FromResource(Resource.Drawable.temple);
				marker.SetIcon(icon);

				mapView.AddMarker(marker);
			}

			//// set camera pos
			var camPos = new CameraPosition.Builder().Target(list[0]).Zoom(10).Tilt(0).Build();
			mapView.CameraPosition = camPos;

			mapView.InfoWindowAdapter = new Info(_activity);
		}

		public class Info : Java.Lang.Object, IInfoWindowAdapter
		{
			private Activity _activity = null;

			public Info(Activity activity)
			{
				_activity = activity;
			}

			public View GetInfoWindow(Marker p0)
			{
				var view = _activity.LayoutInflater.Inflate(Resource.Layout.infowindow, null);

				var txtTitle = view.FindViewById<TextView>(Resource.Id.tooltip_title);
				txtTitle.Text = p0.Title;

				var txtDesc = view.FindViewById<TextView>(Resource.Id.tooltip_description);
				txtDesc.Text = $"{p0.Position.Latitude}, {p0.Position.Longitude}";

				var txtSubDesc = view.FindViewById<TextView>(Resource.Id.tooltip_sub_description);
				txtSubDesc.Text = $"this is test description";

				return view;
			}
		}
	}
}
