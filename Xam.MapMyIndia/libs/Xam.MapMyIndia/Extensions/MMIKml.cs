using System;
using System.IO;
using System.Linq;
using System.Text;
using SharpKml.Dom;
using SharpKml.Engine;
using Xam.MapMyIndia.Models;

namespace Xam.MapMyIndia.Extensions
{
	public class MMIKml
	{
		public static MMIKmlModel Read(string Xml)
		{
			var model = new MMIKmlModel();

			KmlFile file = null;
			using (var stream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(Xml)))
			{
				file = KmlFile.Load(stream);
			}

			model = ParsePolygun(model, file);

			return model;
		}

		private static MMIKmlModel ParsePolygun(MMIKmlModel model, KmlFile kml)
		{
			var polygons = kml.Root.Flatten().OfType<Polygon>();

			var listPolyCoordinates = new System.Collections.Generic.List<MMILatLongModel>();

			foreach (var poly in polygons)
			{
				var listPoints = poly.OuterBoundary.LinearRing.Coordinates.ToList();
				foreach (var p in listPoints)
				{
					listPolyCoordinates.Add(new MMILatLongModel() { Latitude = p.Latitude, Longitude = p.Longitude });
				}
			}
			model.PolygonCoordinates = listPolyCoordinates;

			return model;
		}
	}
}
