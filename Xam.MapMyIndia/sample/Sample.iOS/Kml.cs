using System;
using System.Collections.Generic;
using System.IO;
using CoreLocation;
using Foundation;
using Mapbox;
using UIKit;
using Xam.MapMyIndia.Extensions;
using Xam.MapMyIndia.iOS.Controls;
using Xam.MapMyIndia.Models;

namespace Sample.iOS
{
	public class KmlController : UIViewController
	{
		public KmlController(string title)
		{
			Title = title;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var mapView = new Xam.MapMyIndia.iOS.Controls.MMIMapView(View.Bounds);
			this.View.AddSubview(mapView);

			new Kml().Read(mapView);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}

	public class Kml : MGLMapViewDelegate
	{
		private MMIMapView _mapView = null;

		public Kml()
		{
			
		}

		public void Read(MMIMapView map)
		{
			_mapView = map;
			_mapView.Delegate = this;


			string Xml = File.ReadAllText("Polygon.kml");
			var model = MMIKml.Read(Xml);

			ShowPolygon(model);
		}

		void ShowPolygon(MMIKmlModel model)
		{
			var positions = new List<CLLocationCoordinate2D>();
			foreach(var p in model.PolygonCoordinates) {
				var clc = new CLLocationCoordinate2D(latitude: p.Latitude, longitude: p.Longitude);
				positions.Add(clc);
			}

			var polygun = MGLPolygon.PolygonWithCoordinates(ref positions.ToArray()[0], (uint)positions.Count);

			// add all points
			_mapView.AddAnnotation(polygun);

			//set camera pos
			_mapView.SetCenterCoordinate(new CLLocationCoordinate2D(positions.ToArray()[0].Latitude, positions.ToArray()[0].Longitude), false);
			_mapView.SetZoomLevel(10, true);
			_mapView.ShowsUserLocation = false;
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
