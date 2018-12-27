using System;
using CoreGraphics;
using CoreLocation;
using Foundation;
using Mapbox;
using UIKit;
using Xam.MapMyIndia.iOS.Controls;

namespace Sample.iOS
{
	public class InfoWindowController : UIViewController
	{
		public InfoWindowController(string title)
		{
			Title = title;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var mapView = new Xam.MapMyIndia.iOS.Controls.MMIMapView(View.Bounds);
			this.View.AddSubview(mapView);

			new InfoWindow().Draw(mapView);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}
	}

	public class InfoWindow : MGLMapViewDelegate
	{
		public InfoWindow()
		{
			
		}

		public void Draw(MMIMapView mapview)
		{
			mapview.WeakDelegate = this;

			var temple = new MGLPointAnnotation()
			{
				Title = "Temple of literature",
				Coordinate = new CLLocationCoordinate2D(28.550834, 77.268918)
			};
			mapview.AddAnnotation(temple);

			//set camera pos
			mapview.SetCenterCoordinate(temple.Coordinate, false);
			mapview.SetZoomLevel(7, true);
			mapview.ShowsUserLocation = false;
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
			var callOutView = new MarkerView();
			callOutView.RepresentedObject = annotation;
			return callOutView;
		}

		[Export("mapView:tapOnCalloutForAnnotation:")]
		void MapView_TapOnCalloutForAnnotation(MGLMapView mapView, MGLAnnotation annotation)
		{
			mapView.DeselectAnnotation(annotation, true);
		}

		[Export("mapView:imageForAnnotation:")]
		public MGLAnnotationImage MapView_ImageForAnnotation(MGLMapView mapView, MGLAnnotation annotation)
		{
			var annotationImage = mapView.DequeueReusableAnnotationImageWithIdentifier("temple");
			if (annotationImage == null)
			{
				var image = UIImage.FromFile("temple.png");
				image = image.ImageWithAlignmentRectInsets(new UIEdgeInsets(0, 0, image.Size.Height / 2, 0));
				annotationImage = MGLAnnotationImage.AnnotationImageWithImage(image, "temple");
			}
			return annotationImage;
		}

		public class MarkerView : UIView, IMGLCalloutView
		{
			UILabel _label = null;

			public MarkerView()
			{
				this.WeakDelegate = this;
			}

			public MGLAnnotation RepresentedObject { get; set; }

			public UIView LeftAccessoryView { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
			public UIView RightAccessoryView { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

			public NSObject WeakDelegate { get; set; }

			public void DismissCalloutAnimated(bool animated)
			{
				Hide();
			}

			public void PresentCalloutFromRect(CGRect rect, UIView view, CGRect constrainedRect, bool animated)
			{
				AddShow(rect, view, true);
			}

			void Hide()
			{
				if (_label != null)
				{
					_label.Hidden = true;
				}
			}

			void AddShow(CGRect rect, UIView parent, bool vlaue)
			{
				if(_label != null)
				{
					_label.Hidden = false;
					return;
				}

				float height = 50;
				var centerMarker = new CGPoint(rect.X + rect.Width/2, rect.Y + rect.Height/ 2);
				var centerY = centerMarker.Y - (rect.Height / 2) - (height / 2);

				_label = new UILabel();
				_label.BackgroundColor = UIColor.Red;
				_label.Frame = new CGRect(centerMarker.X, centerY, 100, height);
				_label.Text = "Test Pin";
				parent.AddSubview(_label);
				_label.Center = new CGPoint(centerMarker.X, centerY);
			}
		}
	}
}
