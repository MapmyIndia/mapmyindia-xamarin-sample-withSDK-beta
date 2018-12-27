using System;
using System.Collections.Generic;
using Xam.MapMyIndia.Models;

namespace Xam.MapMyIndia.Extensions
{
	public class MMICircle
	{
		public static List<MMILatLongModel> Draw(MMICircleModel circleModel)
		{
			double meterRadius = circleModel.Radius;
			double M_PI = 22 / 7;

			double degreesBetweenPoints = 6; //120 sides
			int numberOfPoints = (int)Math.Floor(360 / degreesBetweenPoints);
			double distRadians = meterRadius / 6371000.0; // earth radius in meters
			double centerLatRadians = circleModel.Center.Latitude * M_PI / 180;
			double centerLonRadians = circleModel.Center.Longitude * M_PI / 180;

			var list = new List<MMILatLongModel>();

			for (int index = 0; index < numberOfPoints; index++)
			{
				double degrees = index * degreesBetweenPoints;
				double degreeRadians = degrees * M_PI / 180;

				double pointLatRadians = Math.Asin(Math.Sin(centerLatRadians) * Math.Cos(distRadians) + Math.Cos(centerLatRadians) * Math.Sin(distRadians) * Math.Cos(degreeRadians));
				double pointLonRadians = centerLonRadians + Math.Atan2(Math.Sin(degreeRadians) * Math.Sin(distRadians) * Math.Cos(centerLatRadians),
																	   Math.Cos(distRadians) - Math.Sin(centerLatRadians) * Math.Sin(pointLatRadians));
				double pointLat = pointLatRadians * 180 / M_PI;
				double pointLon = pointLonRadians * 180 / M_PI;

				list.Add(new MMILatLongModel() { Latitude = pointLat, Longitude = pointLon });
			}

			return list;
		}
	}
}
