using System;
using System.Collections.Generic;
using CoreLocation;
using Foundation;
using Mapbox;
using UIKit;
using Xam.MapMyIndia.iOS.Controls;

namespace Sample.iOS
{
	public class PolylineController : UIViewController
	{
		public PolylineController(string title)
		{
			Title = title;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var mapView = new Xam.MapMyIndia.iOS.Controls.MMIMapView(View.Bounds);
			this.View.AddSubview(mapView);

			new Polyline().Draw(mapView);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}

	public class Polyline : MGLMapViewDelegate
	{
		public Polyline()
		{
			
		}

		public void Draw(MMIMapView map)
		{
			map.Delegate = this;

			var positions = new List<CLLocationCoordinate2D>();
			positions.Add(new CLLocationCoordinate2D(28.705436, 77.100462));
			positions.Add(new CLLocationCoordinate2D(28.705191, 77.100784));
			positions.Add(new CLLocationCoordinate2D(28.704646, 77.101514));
			positions.Add(new CLLocationCoordinate2D(28.704194, 77.101171));
			positions.Add(new CLLocationCoordinate2D(28.704083, 77.101066));
			positions.Add(new CLLocationCoordinate2D(28.703900, 77.101318));

			var polygun = MGLPolyline.PolylineWithCoordinates(ref positions.ToArray()[0], (uint)positions.Count);

			// add all points
			map.AddAnnotation(polygun);

			//set camera pos
			map.SetCenterCoordinate(new CLLocationCoordinate2D(positions.ToArray()[0].Latitude, positions.ToArray()[0].Longitude), false);
			map.SetZoomLevel(20, true);
			map.ShowsUserLocation = false;
		}

		[Export("mapView:alphaForShapeAnnotation:")]
		public override nfloat MapView_AlphaForShapeAnnotation(MGLMapView mapView, MGLShape annotation)
		{
			return 0.5f;
		}

		[Export("mapView:strokeColorForShapeAnnotation:")]
		public override UIColor MapView_StrokeColorForShapeAnnotation(MGLMapView mapView, MGLShape annotation)
		{
			return UIColor.Red;
		}

		[Export("mapView:fillColorForPolygonAnnotation:")]
		public override UIColor MapView_FillColorForPolygonAnnotation(MGLMapView mapView, MGLPolygon annotation)
		{
			return UIColor.Brown;
		}
	}
}