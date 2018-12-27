using System;
using CoreAnimation;
using CoreLocation;
using Foundation;
using Mapbox;
using UIKit;
using Xam.MapMyIndia.iOS.Controls;

namespace Sample.iOS
{
	public class AnimateMarkerPointsController : UIViewController
	{
		public AnimateMarkerPointsController(string title)
		{
			Title = title;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var mapView = new Xam.MapMyIndia.iOS.Controls.MMIMapView(View.Bounds);
			this.View.AddSubview(mapView);

			new AnimateMarkerPoints().Start(mapView);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}

	public class AnimateMarkerPoints : MGLMapViewDelegate
	{
		private MMIMapView _map = null;
		private MGLPointAnnotation _marker = null;

		public void Start(MMIMapView mqp)
		{
			_map = mqp;
			_map.Delegate = this;

			_marker = new MGLPointAnnotation()
			{
				Title = "Temple of literature",
				Coordinate = new CLLocationCoordinate2D(28.550834, 77.268918)
			};
			_map.AddAnnotation(_marker);

			//set camera
			_map.SetCenterCoordinate(new CLLocationCoordinate2D(_marker.Coordinate.Latitude, _marker.Coordinate.Longitude), false);
			_map.SetZoomLevel(7, true);
			_map.ShowsUserLocation = false;

			NSTimer.CreateScheduledTimer(new TimeSpan(0, 0, 0, 5, 0), delegate {
				Animate();
			});
		}

		void Animate()
		{
			var start = _marker.Coordinate;
			var final = new CLLocationCoordinate2D(28.321684, 85.987289);

			int numberOfPoints = 100;

			var latModifier = (start.Latitude - final.Latitude) / numberOfPoints;
			var lngModifier = (start.Longitude - final.Longitude) / numberOfPoints;

			for (int counter = 0; counter < numberOfPoints; counter++)
			{
				var lat = start.Latitude + (latModifier * counter);
				var lng = start.Longitude + (lngModifier * counter);

				System.Diagnostics.Debug.WriteLine($"{counter} - {lat}, {lng}");

				CATransaction.Begin();
				CATransaction.AnimationDuration = 0.1;
				_marker.Coordinate = new CLLocationCoordinate2D(lat, lng);
				CATransaction.Commit();
			}

			//CLLocationCoordinate2D newPoint;   // A newly-created point along the line

			//double latitudeModifier;    // Distance to add/subtract to each latitude point
			//double longitudeModifier;   // Distance to add/subtract to each longitude point

			//int numberOfPoints = 100;   // The number of points you want between the two points

			//// Determine the distance between the lats and divide by numberOfPoints
			//latitudeModifier = (startPoint.latitude - endPoint.latitude) / numbernumberOfPoints;
			//// Same with lons
			//longitudeModifier = (startPoint.longitude - endPoint.longitude) / numbernumberOfPoints;

			//// Loop through the points
			//for (int i = 0; i < numberOfPoints; i++)
			//{
			//	newPoint.latitude = startPoint.latitude + (latitudeModifier * i);
			//	newPoint.longitude = startPoint.longitude + (longitudeModifier * i);

			//	// Do something with your newPoint here

			//}






		}
	}
}
