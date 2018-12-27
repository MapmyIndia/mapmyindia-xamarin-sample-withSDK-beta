using System;
using CoreLocation;
using Foundation;
using Mapbox;
using UIKit;
using Xam.MapMyIndia.iOS.Controls;

namespace Sample.iOS
{
	public class AnimateMarkerController : UIViewController
	{
		public AnimateMarkerController(string title)
		{
			Title = title;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var mapView = new Xam.MapMyIndia.iOS.Controls.MMIMapView(View.Bounds);
			this.View.AddSubview(mapView);

			new MoveMarker().Start(mapView);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}

	public class MoveMarker : MGLMapViewDelegate
	{
		private MMIMapView _map = null;
		private MGLPointAnnotation _marker = null;

		public void Start(MMIMapView mqp)
		{
			_map = mqp;

			_map.Delegate = this;

			// add pan gestures
			var pan = new UIPanGestureRecognizer(PanGesture);
			_map.AddGestureRecognizer(pan);

			_marker = new MGLPointAnnotation()
			{
				Title = "Temple of literature",
				Coordinate = new CLLocationCoordinate2D(28.550834, 77.268918)
			};
			_map.AddAnnotation(_marker);

			//set camera
			_map.SetCenterCoordinate(new CLLocationCoordinate2D(_marker.Coordinate.Latitude, _marker.Coordinate.Longitude), false);
			_map.SetZoomLevel(10, true);
			_map.ShowsUserLocation = false;
		}

		public void PanGesture(UIPanGestureRecognizer uitgr)
		{
			var loc = uitgr.LocationInView(_map);
			var coord = _map.ConvertPoint(loc, _map);
			_marker.Coordinate = coord;
		}
	}
}
