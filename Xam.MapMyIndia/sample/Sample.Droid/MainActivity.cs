using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Mapbox.Mapboxsdk.Maps;
using Xam.MapMyIndia.Droid.Controls;

namespace Sample.Droid
{
	[Activity(Label = "Xam.MapMyIndia", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		string[] items;  
    	ListView mainList;  

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.listhome);


			items = new string[]
	{
		"Multiple Markers",
		"Circle",
		"Polygon",
		"Polyline",
		"Drawing",
		"Info Window",
				"KML",
				"Animate Marker",
				"Animate Marker Betweeen Points"
	};

			mainList = (ListView)FindViewById<ListView>(Resource.Id.mainlistview);
			mainList.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, items);

			mainList.ItemClick += (s, e) => {  
				var option = e.Position;

				Intent intent = null;
				if (option == 0)
				{
					intent = new Intent(this, typeof(MarkersActivity));
				}
				else if (option == 1)
				{
					intent = new Intent(this, typeof(CircleActivity));
				}
				else if (option == 2)
				{
					intent = new Intent(this, typeof(PolygonActivity));
				}
				else if (option == 3)
				{
					intent = new Intent(this, typeof(PolylineActivity));
				}
				else if (option == 4)
				{
					intent = new Intent(this, typeof(DrawActivity));
				}
				else if (option == 5)
				{
					intent = new Intent(this, typeof(InfoWindowActivity));
				}
				else if (option == 6)
				{
					intent = new Intent(this, typeof(KmlActivity));
				}
				else if (option == 7)
				{
					intent = new Intent(this, typeof(AnimateMarkerActivity));
				}
				else if (option == 8)
				{
					intent = new Intent(this, typeof(AnimateMarkerPointsActivity));
				}

				StartActivity(intent);
		};  
		}
	}
}

