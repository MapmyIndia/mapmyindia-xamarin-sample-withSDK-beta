using System;
using System.Collections.Generic;
using CoreLocation;
using Foundation;
using Mapbox;
using UIKit;
using Xam.MapMyIndia.Extensions;
using Xam.MapMyIndia.iOS.Controls;
using Xam.MapMyIndia.Models;

namespace Sample.iOS
{
	public class CircleController : UIViewController
	{
		public CircleController(string title)
		{
			Title = title;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var mapView = new Xam.MapMyIndia.iOS.Controls.MMIMapView(View.Bounds);
			this.View.AddSubview(mapView);

			new Circle().Draw(mapView);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}

	public class Circle : MGLMapViewDelegate
	{
		MGLPointAnnotation mGLPoint = null;
		double radiusInMtrs;

		public Circle()
		{
			mGLPoint = new MGLPointAnnotation();
			mGLPoint.Coordinate = new CLLocationCoordinate2D(25.321684, 82.987289);

			radiusInMtrs = 500000;
		}

		public void Draw(MMIMapView map)
		{
			map.Delegate = this;

			//set camera pos
			map.SetCenterCoordinate(new CLLocationCoordinate2D(mGLPoint.Coordinate.Latitude, mGLPoint.Coordinate.Longitude), false);
			map.SetZoomLevel(7, true);
			map.ShowsUserLocation = false;
		}

		[Export("mapView:didFinishLoadingStyle:")]
		public override void MapViewDidFinishLoadingStyle(MGLMapView mapView, MGLStyle style)
		{
			var souce = new MGLShapeSource(identifier: "circle", shape: mGLPoint, options: null);
			style.AddSource(souce);

			var layer = new MGLCircleStyleLayer(identifier: "circle", source: souce);

			var meterPerPoint = mapView.MetersPerPointAtLatitude(mGLPoint.Coordinate.Latitude);
			var radius = radiusInMtrs / meterPerPoint;
			layer.CircleRadius = NSExpression.FromConstant(new NSNumber(radius));
			layer.CircleOpacity = NSExpression.FromConstant(new NSNumber(0.5));
			style.AddLayer(layer);
		}

		[Export("mapViewRegionIsChanging:")]
		public override void MapViewRegionIsChanging(MGLMapView mapView) 
		{
			var style = mapView.Style;
			if(style == null) {
				return;
			}
			var layer = style.LayerWithIdentifier("circle") as MGLCircleStyleLayer;
			var meterPerPoint = mapView.MetersPerPointAtLatitude(mGLPoint.Coordinate.Latitude);
			var radius = radiusInMtrs / meterPerPoint;
			layer.CircleRadius = NSExpression.FromConstant(new NSNumber(radius));
		}
	}
}
