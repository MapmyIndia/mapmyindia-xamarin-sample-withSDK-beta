using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Sample.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController(IntPtr handle) : base(handle)
		{
			
		}

		public event Action<int> OptionClick;

		List<string> itemData;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Title = "Xam.MapMyIndia";

			var back = new UIBarButtonItem("", UIBarButtonItemStyle.Plain, null);
			NavigationItem.BackBarButtonItem = back;

			var height = NavigationController.NavigationBar.Frame.Height;
			var frame = new CoreGraphics.CGRect(0, 70, View.Frame.Width, View.Frame.Height);
			var tableView = new UITableView(frame);
			this.View.AddSubview(tableView);

			itemData = new List<string>()
	{
		"Multiple Markers",
		"Circle",
		"Polygon",
		"Polyline",
		"Drawing",
		"Info Window",
				"KML",
				"Animate Marker",
				"Animate Marker Between Points"
	};
			tableView.Source = new TableViewSource(itemData, this);
			tableView.TableFooterView = new UIView(CoreGraphics.CGRect.Empty);  

			OptionClick += ViewController_OptionClick;
		}

		private void ViewController_OptionClick(int option)
		{
			var title = itemData[option];
			if(option == 0) {
				NavigationController.PushViewController(new MarkersController(title), true);
			}
			else if(option == 1) {
				NavigationController.PushViewController(new CircleController(title), true);
			}
			else if (option == 2)
			{
				NavigationController.PushViewController(new PolygonController(title), true);
			}
			else if (option == 3)
			{
				NavigationController.PushViewController(new PolylineController(title), true);
			}
			else if (option == 4)
			{
				NavigationController.PushViewController(new DrawController(title), true);
			}
			else if (option == 5)
			{
				NavigationController.PushViewController(new InfoWindowController(title), true);
			}
			else if (option == 6)
			{
				NavigationController.PushViewController(new KmlController(title), true);
			}
			else if (option == 7)
			{
				NavigationController.PushViewController(new AnimateMarkerController(title), true);
			}
			else if (option == 8)
			{
				NavigationController.PushViewController(new AnimateMarkerPointsController(title), true);
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public class TableViewSource: UITableViewSource 
		{  
        	List<string> tabledata;
			private ViewController _controller = null;
			public TableViewSource(List<string> items, ViewController controller) {  
            	tabledata = items;
				_controller = controller;
        	}  

        	public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {  
            	UITableViewCell cell = tableView.DequeueReusableCell("cell");  
            	if (cell == null) {  
                	cell = new UITableViewCell(UITableViewCellStyle.Default, "cell");  
            	}  
            	cell.TextLabel.Text = tabledata[indexPath.Row];  
            	return cell;  
        	}  
        	public override nint RowsInSection(UITableView tableview, nint section) {  
            	return tabledata.Count;  
        	}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				_controller.OptionClick?.Invoke(indexPath.Row);
			}
    	} 
	}
}
