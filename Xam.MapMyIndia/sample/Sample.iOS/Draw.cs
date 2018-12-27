using System;
using System.Collections.Generic;
using CoreLocation;
using Foundation;
using Mapbox;
using UIKit;
using Xam.MapMyIndia.iOS.Controls;

namespace Sample.iOS
{
	public class DrawController : UIViewController
	{
		public DrawController(string title)
		{
			Title = title;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var mapView = new Xam.MapMyIndia.iOS.Controls.MMIMapView(View.Bounds);
			this.View.AddSubview(mapView);

			new Draw(mapView);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}

	public class Draw : MGLMapViewDelegate
	{
		private MMIMapView _map = null;

		private List<CLLocationCoordinate2D> locations = new List<CLLocationCoordinate2D>();
		private MGLPolygon _polygun = null;

		public Draw(MMIMapView mqp)
		{
			_map = mqp;

			_map.Delegate = this;

			//set camera pos
			_map.SetZoomLevel(7, true);

			// add pan gestures
			var pan = new UIPanGestureRecognizer(PanGesture);
			_map.AddGestureRecognizer(pan);
		}

		public void PanGesture(UIPanGestureRecognizer uitgr)
		{
			var loc = uitgr.LocationInView(_map);
			var coord = _map.ConvertPoint(loc, _map);
			locations.Add(coord);

			switch(uitgr.State)
			{
				case UIGestureRecognizerState.Began :
					if(_polygun != null) {
						_map.RemoveAnnotation(_polygun);
					}
					locations.Clear();
					break;
				case UIGestureRecognizerState.Ended :
					Add();
					break;
			}
		}

		void Add()
		{
			_polygun = MGLPolygon.PolygonWithCoordinates(ref locations.ToArray()[0], (uint)locations.Count);

			// add all points
			_map.AddAnnotation(_polygun);
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
