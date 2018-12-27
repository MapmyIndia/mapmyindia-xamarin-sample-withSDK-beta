using System;
using System.Collections.Generic;
using CoreLocation;
using Foundation;
using Mapbox;
using UIKit;
using Xam.MapMyIndia.iOS.Controls;

namespace Sample.iOS
{
	public class PolygonController : UIViewController
	{
		public PolygonController(string title)
		{
			Title = title;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var mapView = new Xam.MapMyIndia.iOS.Controls.MMIMapView(View.Bounds);
			this.View.AddSubview(mapView);

			new Polygon().Draw(mapView);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}

	public class Polygon : MGLMapViewDelegate
	{
		public Polygon()
		{
			
		}

		public void Draw(MMIMapView map)
		{
			map.Delegate = this;

			var positions = new List<CLLocationCoordinate2D>();
			positions.Add(new CLLocationCoordinate2D(28.703900, 77.101318));
			positions.Add(new CLLocationCoordinate2D(28.703331, 77.10215));
			positions.Add(new CLLocationCoordinate2D(28.703905, 77.102761));
			positions.Add(new CLLocationCoordinate2D(28.704248, 77.102370));

			var polygun = MGLPolygon.PolygonWithCoordinates(ref positions.ToArray()[0], (uint)positions.Count);

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
