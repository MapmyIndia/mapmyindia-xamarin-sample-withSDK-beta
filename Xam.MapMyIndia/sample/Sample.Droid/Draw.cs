using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Mapbox.Mapboxsdk.Annotations;
using Com.Mapbox.Mapboxsdk.Geometry;
using Com.Mapbox.Mapboxsdk.Maps;
using Java.Util;
using Xam.MapMyIndia.Droid.Controls;

namespace Sample.Droid
{
	[Activity(Label = "Drawing", ScreenOrientation = ScreenOrientation.Portrait)]
	public class DrawActivity : Activity, IOnMapReadyCallback
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
			new Draw(mapboxMap, _map);
		}
	}

	public class Draw : Java.Lang.Object, Android.Views.View.IOnTouchListener
	{
		private MapboxMap _map = null;
		private MapView _mapView = null;

		List<LatLng> list = new List<LatLng>();

		public Draw(MapboxMap map, MapView mapView)
		{
			_map = map;
			_mapView = mapView;

			_mapView.SetOnTouchListener(this);
		}

		public bool OnTouch(View v, MotionEvent e)
		{
			var action = e.ActionMasked;

			switch(action)
			{
				case MotionEventActions.Down:
					_map.Clear();
					list.Clear();
					break;

				case MotionEventActions.Move:
					var latlong = _map.Projection.FromScreenLocation(new PointF(x:e.GetX(), y:e.GetY()));
					list.Add(latlong);
					break;

				case MotionEventActions.Up :
					Add();
					break;
			}

			return true;
		}

		void Add()
		{
			var arrayList = new ArrayList(list);
			var polygon = new PolygonOptions().AddAll(arrayList);
			polygon.InvokeFillColor(Color.ParseColor("#753bb2d0").ToArgb());
			_map.AddPolygon(polygon);
		}
	}
}
