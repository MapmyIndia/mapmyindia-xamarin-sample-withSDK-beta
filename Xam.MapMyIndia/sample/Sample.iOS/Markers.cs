using System;
using System.Collections.Generic;
using CoreLocation;
using Foundation;
using Mapbox;
using UIKit;
using Xam.MapMyIndia.iOS.Controls;

namespace Sample.iOS
{
	public class MarkersController : UIViewController
	{
		public MarkersController(string title)
		{
			Title = title;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var mapView = new Xam.MapMyIndia.iOS.Controls.MMIMapView(View.Bounds);
			this.View.AddSubview(mapView);

			new Markers().AddMarkers(mapView);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}

	public class Markers : MGLMapViewDelegate
	{
		private MMIMapView _mapView;
		private List<MGLPointAnnotation> list = null;

		public Markers()
		{
			list = new List<MGLPointAnnotation>();
		}

		public void AddMarkers(MMIMapView mapview)
		{
			_mapView = mapview;
			_mapView.WeakDelegate = this;

			list.Add(new MGLPointAnnotation() { Title = "Test Point", Coordinate = new CLLocationCoordinate2D(25.321684, 82.987289) });
			list.Add(new MGLPointAnnotation() { Title = "Test Point", Coordinate = new CLLocationCoordinate2D(26.321684, 83.987289) });
			list.Add(new MGLPointAnnotation() { Title = "Test Point", Coordinate = new CLLocationCoordinate2D(27.321684, 84.987289) });
			list.Add(new MGLPointAnnotation() { Title = "Test Point", Coordinate = new CLLocationCoordinate2D(28.321684, 85.987289) });

			_mapView.AddAnnotations(list.ToArray());

			//set camera pos
			_mapView.SetCenterCoordinate(list.ToArray()[0].Coordinate, false);
			_mapView.SetZoomLevel(7, true);
			_mapView.ShowsUserLocation = false;

			// remove all markers after 5 seconds.
			//NSTimer.CreateScheduledTimer(new TimeSpan(0, 0, 0, 5, 0), delegate {
			//	Remove();
			//});
		}

		[Export("mapView:annotationCanShowCallout:")]
		public bool MapView_AnnotationCanShowCallout(MGLMapView mapView, MGLAnnotation annotation)
		{
			// one can use this event to get a call back for marker tap.
			return true;
		}

		[Export("mapView:calloutViewForAnnotation:")]
		public IMGLCalloutView MapView_CalloutViewForAnnotation(MGLMapView mapView, MGLAnnotation annotation)
		{
			//var callOutView = new MarkerView();
			//callOutView.RepresentedObject = annotation;
			return null;
		}

		[Export("mapView:tapOnCalloutForAnnotation:")]
		void MapView_TapOnCalloutForAnnotation(MGLMapView mapView, MGLAnnotation annotation)
		{
			mapView.DeselectAnnotation(annotation, true);
		}

		[Export("mapView:imageForAnnotation:")]
		public MGLAnnotationImage MapView_ImageForAnnotation(MGLMapView mapView, MGLAnnotation annotation)
		{
			//var annotationImage = mapView.DequeueReusableAnnotationImageWithIdentifier("temple");
			//if (annotationImage == null)
			//{
			//	var image = UIImage.FromFile("temple.png");
			//	image = image.ImageWithAlignmentRectInsets(new UIEdgeInsets(0, 0, image.Size.Height / 2, 0));
			//	annotationImage = MGLAnnotationImage.AnnotationImageWithImage(image, "temple");
			//}
			return null;
		}

		private void Remove()
		{
			_mapView.RemoveAnnotations(list.ToArray());
			System.Diagnostics.Debug.WriteLine($"Remove");
		}
	}
}
