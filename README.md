![MapmyIndia APIs](https://www.mapmyindia.com/api/img/mapmyindia-api.png)

# MapmyIndia Vector Maps Sample App (with SDK) for Xamarin - beta RC3

**Easy To Integrate Maps & Location APIs & SDKs For Web & Mobile Applications**

Powered with India's most comprehensive and robust mapping functionalities.
**Now Available**  for Srilanka, Nepal, Bhutan and Bangladesh

1. You can get your api key to be used in this document here: [https://www.mapmyindia.com/api/signup](https://www.mapmyindia.com/api/signup)

2. To make sure that the sample provided here works, MapmyIndia API keys,  (available here: http://www.mapmyindia.com/api/dashboard) need to be set in the "MMIAuthModel" class in the sample code.

3. The sample code is provided to help you understand the basic functionality of MapmyIndia maps & REST APIs working on Xamarin platform. 

## Version History

| Version | Last Updated | Author |
| ---- | ---- | ---- |
| 0.1.3 | 13 December 2018 | MapmyIndia API Team (RK) |

## Getting Started

This library provides the controls/renderers for using MapmyIndia SDKs inside your Xamarin application(Android or iOS). 
You can have a look at the map and features you will get in your own app by using the MapmyIndia [Move](https://itunes.apple.com/in/app/mapmyindia-move/id723492531?mt=8) app for iOS. 
The SDK handles map tiles download and their display along with a bunch of controls and gestures.

## API Usage
Your MapmyIndia Maps SDK usage needs a set of license keys (get them here) and is governed by the API [terms and conditions](https://www.mapmyindia.com/api/terms-&-conditions). 
As part of the terms and conditions, you cannot remove or hide the MapmyIndia logo and copyright information in your project. 
Please see [branding guidelines](https://www.mapmyindia.com/api/advanced-maps/API-Branding-Guidelines.pdf) on MapmyIndia [website](https://www.mapmyindia.com/api) for more details.
The allowed SDK hits are described on the plans page. Note that your usage is
shared between platforms, so the API hits you make from a web application, Android app or an iOS app all add up to your allowed daily limit.

## Setup your Project
To initialize SDK you have to set required keys.

### Xamarin-Android

1. Add reference `Xam.MMI.droid` in your Xamarin Android project.
2. Add using statements for `Com.Mmi.Services.Account` on `MainApplication `class of project.
3. In `OnCreate` method of `MainApplication` class use below code to set keys for SDK.

```csharp
using Com.Mmi.Services.Account;
using Com.Mapbox.Mapboxsdk;
public override void OnCreate()
	{
		base.OnCreate();
		RegisterActivityLifecycleCallbacks(this);
		MapmyIndiaAccountManager.Instance.RestAPIKey = “YOUR MapmyIndia REST KEY”;
		MapmyIndiaAccountManager.Instance.MapSDKKey = “YOUR MapmyIndia SDK KEY”;
		MapmyIndiaAccountManager.Instance.AtlasClientId = “YOUR MapmyIndia Atlas CLIENT ID”;
		MapmyIndiaAccountManager.Instance.AtlasClientSecret = “YOUR MapmyIndia Atlas CLIENT SECRET”;
		MapmyIndiaAccountManager.Instance.AtlasGrantType = “YOUR MapmyIndia Atlas Grant Type”; // always put "client_credentials"
		MapmyIndia.GetInstance(this);
	}
```

### Xamarin-iOS
1. Add references `Xam.MMI.iOS, Xam.MapMyIndia.iOS` in your Xamarin iOS
project.
2. Add using statement for `Xam.MMI.iOS` in AppDelegate class of project.
3. In `FinishedLoading` method of `AppDelegate` class use below code to set keys for SDK.

```csharp
using Xam.MMI.iOS;
public override bool FinishedLaunching(UIApplication application,
NSDictionary launchOptions)
	{
		MapmyIndiaAccountManager.MapSDKKey = new NSString(“YOUR 	MapmyIndia SDK KEY”);
		MapmyIndiaAccountManager.RestAPIKey = new NSString(“YOUR MapmyIndia REST KEY”);
		MapmyIndiaAccountManager.AtlasClientId = new NSString(“YOUR MapmyIndia Atlas CLIENT ID”);
		MapmyIndiaAccountManager.AtlasClientSecret = new NSString(“YOUR MapmyIndia Atlas CLIENT SECRET”);
		MapmyIndiaAccountManager.AtlasGrantType = new NSString(“YOUR MapmyIndia Atlas Grant Type”); // always "client_credentials"
		MapmyIndiaAccountManager.AtlasAPIVersion = new NSString(authModel.AtlasAPIVersion);
		return true;
	}
```
## Add a map view

### Xamarin-Android

1. Add using statement for `Com.Mapbox.Mapboxsdk.Maps` in activity class of
Xamarin Android project.
```csharp
using Com.Mapbox.Mapboxsdk.Maps;
```
2. Add map view on layout of activity.
```csharp
<com.mapbox.mapboxsdk.maps.MapView
android:id=“@+id/mapView"
android:layout_width=“fill_parent"
android:layout_height="fill_parent" />
```
3. Create a global object of MapView in activity class.
```csharp
private MapView mapView;
```
4. In `OnCreate` method of activity add code to initialize map view.
```csharp
mapView = FindViewById<MapView>(Resource.Id.mapView);
mapView.OnCreate(bundle);
mapView.GetMapAsync(this);
```

### Xamarin-iOS

1. Add using statement for `Xam.MapMyIndia.iOS` on `ViewController` class.
2. Create a global object of `MapmyIndiaMapView` on `ViewController` class.
```csharp
private MapmyIndiaMapView mapView;
```
3. In `ViewDidLoad` method of `ViewController` class add code to initialize map view.
```csharp
mapView = new MapmyIndiaMapView(View.Bounds);
this.View.AddSubview(mapView);
```
## Map Interactions

### Set Zoom Level

Set zoom to 4 for country level display and 18 for house number display.

#### Xamarin-Android

Use `CameraPosition` property of map to set its zoom level. For this create an
instance of `CameraPosition` class and set zoom using builder pattern.
eg.
```csharp
var camPos = new new CameraPosition.Builder().Zoom(4).Build();
mapView.CameraPosition = camPos;
```
#### Xamarin-iOS

```csharp
mapView.SetZoomLevel(4, true);
```

### Set Map Center

#### Xamarin-Android

Use `CameraPosition` property of map to set its center coordinates. For this create an instance of `CameraPosition` class and set Target using builder pattern.
eg.
```csharp
var camPos = new CameraPosition.Builder().Target(new LatLng() { Latitude = 25.321684, Longitude = 82.987289 });
mapView.CameraPosition = camPos;
```
#### Xamarin-iOS
Create an instance of `CLLocationCoordinate2D` and pass this to
`SetCenterCoordinate` method of map.
eg.
```csharp
mapView.SetCenterCoordinate(new CLLocationCoordinate2D(25.321684, 82.987289), false);
```

## Map Features

### User Location

Showing  user location

#### Xamarin-Android
Write your own logic by implementing some location service and,  
on location change update marker on map.

#### Xamarin-iOS
To show user location on map use property `showsUserLocation` and set its value to true.
```csharp
mapView.showsUserLocation = true
```

## Map Events

Different events can be used for map and markers and for other by implementing interfaces according to requirement and platform.

### Xamarin-Android

On Android, interfaces `MapboxMap.IOnMapClickListener`, `IOnMapReadyCallback`, `MapboxMap.IOnMarkerClickListener` etc. can be implemented on activity.
eg.

1. public void OnMapError(int p0, string p1)
2. public void OnMapReady(MapboxMap mapboxMap)
3. public void OnMapClick(LatLng p0)
4. public bool OnMarkerClick(Marker p0)

**Remember** to add listener to your map.
eg.
```csharp
mapView.SetOnMarkerClickListener(this);
mapView.SetOnMapClickListener(this);
```
### Xamarin-iOS

On iOS implement `MGLMapViewDelegate` which contains many interface methods which can be used accordingly.
eg.
1. `[Export("mapView:annotationCanShowCallout:")]`
`public bool MapView_AnnotationCanShowCallout(MGLMapView mapView,MGLAnnotation annotation)`
2. `[Export("mapView:calloutViewForAnnotation:")]`
`public IMGLCalloutView MapView_CalloutViewForAnnotation(MGLMapView mapView, MGLAnnotation annotation)`
4. `[Export("mapView:tapOnCalloutForAnnotation:")]`
`void MapView_TapOnCalloutForAnnotation(MGLMapView mapView, MGLAnnotation annotation)`
5. `[Export("mapView:imageForAnnotation:")]`
`public MGLAnnotationImage MapView_ImageForAnnotation(MGLMapView mapView, MGLAnnotation annotation)`

**Remember** to set reference to delegate as shown below:
```csharp
mapView.WeakDelegate = this;
```

## Map Overlays

### Add marker

#### Xamarin-Android

Create an instance of `MarkerOptions` add this on map using `AddMarker` method of map. `MarkerOptions` will require a object of `LatLng` which will take coordinate points on which marker will be plotted on map.
```csharp
var latlng = new LatLng() { Latitude = 25.321684, Longitude = 82.987289 };
var marker = new MarkerOptions();
marker.SetPosition(latlng);
mapbox.AddMarker(marker);
```
#### Xamarin-iOS

Create an instance of `MGLPointAnnotation` add this on map using  `AddAnnotation` method of map. `MGLPointAnnotation` will require a object of `CLLocationCoordinate2D` which will take coordinate points on which marker will be plotted on map. Also title and Subtitle can be set in instance of  `MGLPointAnnotation`.
```csharp
mapView.AddAnnotation(new MGLPointAnnotation() { Title = "New Test",
Subtitle = "New Test Sub", Coordinate = new
CLLocationCoordinate2D(25.321684, 82.987289) });
```
### Remove marker

#### Xamarin-Android

All markers can be remove using below code 
```csharp
mapView.Clear();
```
**OR**

Below code can be used to remove a particular marker.
```csharp
mapView.RemoveMarker(marker);
```

#### Xamarin-iOS

All markers can be remove using below code
```csharp
mapView.RemoveAnnotations(mapView.Annotations)
```
**OR**

Below code can be used to remove a particular marker.
```csharp
mapView.RemoveAnnotation(annotation);
```

### Custom Marker

#### Xamarin-Android
For custom marker create an icon using `IconFactory` and set this icon to marker using `SetIcon` method of marker.
```csharp
var latlng = new LatLng() { Latitude = 25.321684, Longitude = 82.987289 };
var marker = new MarkerOptions();
marker.SetPosition(latlng);
marker.SetTitle("Test Info Window");
IconFactory iconFactory = IconFactory.GetInstance(_activity);
Icon icon = iconFactory.FromResource(Resource.Drawable.temple);
marker.SetIcon(icon);
mapView.AddMarker(marker);
```

#### Xamarin-iOS
Use `MapView_ImageForAnnotation` method of `MGLMapViewDelegate` interface class to override image for marker. This method returns an object of
`MGLAnnotationImage` which can be overridden here.
```csharp
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
```

### Show Marker Info Window

#### Xamarin-Android

To enable info window on tap of marker set valid title for marker
```csharp
marker.SetTitle("Test Info Window”);
```

To disable, set null to its marker.
```csharp
marker.SetTitle(null);
```

#### Xamarin-iOS
Use `MapView_AnnotationCanShowCallout` method of interface `MGLMapViewDelegate` which returns a boolean value. To enable info window on tap of marker return true from below delegate method:
```csharp
[Export("mapView:annotationCanShowCallout:")]
public bool MapView_AnnotationCanShowCallout(MGLMapView mapView, MGLAnnotation annotation)
	{
		// one can use this event to get a call back for marker tap.
		return true;
	}
```

### Custom Info Window

#### Xamarin-Android

To set custom info window for marker implement `IInfoWindowAdapter` interface and use `GetInfoWindow` method of that interface which returns a view to display, as info window. Override this view using your custom layout and return from here.
eg.
```csharp
public View GetInfoWindow(Marker p0)
	{
		var view = _activity.LayoutInflater.Inflate(Resource.Layout.infowindow, null);
		var txtTitle = view.FindViewById<TextView>(Resource.Id.tooltip_title);
		txtTitle.Text = p0.Title;
		var txtDesc = view.FindViewById<TextView>(Resource.Id.tooltip_description);
		txtDesc.Text = $"{ p0.Position.Latitude}, { p0.Position.Longitude}";
		var txtSubDesc = view.FindViewById<TextView>(Resource.Id.tooltip_sub_description);
		txtSubDesc.Text = $"this is test description";
		return view;
	}
```

#### Xamarin-iOS

To show custom info window use `MapView_CalloutViewForAnnotation` method of `MGLMapViewDelegate` interface class which returns a view to display, as info window. A custom class is created, inherited from `UIView` and  `IMGLCalloutView` to return as custom info window whose code is shown as below.
```csharp
[Export("mapView:calloutViewForAnnotation:")]
public IMGLCalloutView MapView_CalloutViewForAnnotation(MGLMapView mapView, MGLAnnotation annotation)
	{
		var callOutView = new MarkerView();
		callOutView.RepresentedObject = annotation;
		return callOutView;
	}
```
eg.
```csharp
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
```

### Polylines

#### Add a Polyline

##### Xamarin-Android
To show a polyline on map create an instance of `PolylineOptions` and add that object to instance of `MapmyIndiaMapView` using method `AddPolyline`.
To create instance of `PolylineOptions` a list of `LatLng` will be required so first create a list of `LatLng`.
eg.
```csharp
var list = new List<LatLng>();
list.Add(new LatLng() { Latitude = 28.705436, Longitude = 77.100462 });
list.Add(new LatLng() { Latitude = 28.705191, Longitude = 77.100784 });
list.Add(new LatLng() { Latitude = 28.704646, Longitude = 77.101514 });
list.Add(new LatLng() { Latitude = 28.704194, Longitude = 77.101171 });
list.Add(new LatLng() { Latitude = 28.704083, Longitude = 77.101066 });
list.Add(new LatLng() { Latitude = 28.703900, Longitude = 77.101318 });
var arrayList = new ArrayList(list);
var polyline = new PolylineOptions().AddAll(arrayList);
polyline.InvokeColor(Color.ParseColor("#3bb2d0").ToArgb());
mapView.AddPolyline(polyline);
```

##### Xamarin-iOS

To show a polyline on map create an instance of `MGLPolyline` and add that object to instance of `MapmyIndiaMapView` using method `AddAnnotation`.
To create instance of `MGLPolyline` an array of `CLLocationCoordinate2D` will be required so first create an array of `CLLocationCoordinate2D`.
eg.
```csharp
var positions = new List<CLLocationCoordinate2D>();
positions.Add(new CLLocationCoordinate2D(28.705436, 77.100462));
positions.Add(new CLLocationCoordinate2D(28.705191, 77.100784));
positions.Add(new CLLocationCoordinate2D(28.704646, 77.101514));
positions.Add(new CLLocationCoordinate2D(28.704194, 77.101171));
positions.Add(new CLLocationCoordinate2D(28.704083, 77.101066));
positions.Add(new CLLocationCoordinate2D(28.703900, 77.101318));
var polyline = MGLPolyline.PolylineWithCoordinates(ref positions.ToArray() [0], (uint)positions.Count);
mapView.AddAnnotation(polyline);
```

#### Remove a Polyline

##### Xamarin-Android
```csharp
mapView.RemovePolyline(polyline);
```

##### Xamarin-iOS

```csharp
mapView.RemoveAnnotation(annotation);
```

### Polygons

#### Add a Polygon

##### Xamarin-Android
To show a polyline on map create an instance of `PolygonOptions` and add that object to instance of `MapmyIndiaMapView` using method `AddPolygon`.
To create instance of `PolylineOptions` a list of `LatLng` will be required so first create a list of `LatLng`.
eg.
```csharp
var list = new List<LatLng>();
list.Add(new LatLng() { Latitude = 28.703900, Longitude = 77.101318 });
list.Add(new LatLng() { Latitude = 28.703331, Longitude = 77.10215 });
list.Add(new LatLng() { Latitude = 28.703905, Longitude = 77.102761 });
list.Add(new LatLng() { Latitude = 28.704248, Longitude = 77.102370 });
var arrayList = new ArrayList(list);
var polygon = new PolygonOptions().AddAll(arrayList);
polygon.InvokeFillColor(Color.ParseColor("#753bb2d0").ToArgb());
mapView.AddPolygon(polygon);
```

##### Xamarin-iOS

To show a polygon on map create an instance of `MGLPolygon` and add that object to instance of `MGLMapView` using method `addAnnotation`.
To create instance of `MGLPolygon` an array of `CLLocationCoordinate2D` will be required so first create an array of `CLLocationCoordinate2D`.
eg.
```csharp
var positions = new List<CLLocationCoordinate2D>();
positions.Add(new CLLocationCoordinate2D(28.703900, 77.101318));
positions.Add(new CLLocationCoordinate2D(28.703331, 77.10215));
positions.Add(new CLLocationCoordinate2D(28.703905, 77.102761));
positions.Add(new CLLocationCoordinate2D(28.704248, 77.102370));
var polygon = MGLPolygon.PolygonWithCoordinates(ref positions.ToArray()[0], (uint)positions.Count);
// add all points
map.AddAnnotation(polygon);
```

#### Remove a Polygon

##### Xamarin-Android
```csharp
mapView.RemovePolygon(polygon);
```

###### Xamarin-iOS
```csharp
mapView.RemoveAnnotation(annotation);
```


For any queries and support, please contact: 

![Email](https://www.google.com/a/cpanel/mapmyindia.co.in/images/logo.gif?service=google_gsuite) 
Email us at [apisupport@mapmyindia.com](mailto:apisupport@mapmyindia.com)

![](https://www.mapmyindia.com/api/img/icons/stack-overflow.png)
[Stack Overflow](https://stackoverflow.com/questions/tagged/mapmyindia-api)
Ask a question under the mapmyindia-api

![](https://www.mapmyindia.com/api/img/icons/support.png)
[Support](https://www.mapmyindia.com/api/index.php#f_cont)
Need support? contact us!

![](https://www.mapmyindia.com/api/img/icons/blog.png)
[Blog](http://www.mapmyindia.com/blog/)
Read about the latest updates & customer stories


> © Copyright 2018. CE Info Systems Pvt. Ltd. All Rights Reserved. | [Terms & Conditions](http://www.mapmyindia.com/api/terms-&-conditions)
> mapbox-gl-native copyright (c) 2014-2018 Mapbox.
>  Written with [StackEdit](https://stackedit.io/) by MapmyIndia.