using System;

namespace EmpireCLS
{
	public static class LocationUtil
	{
		public enum LocationDistanceType
		{
			Miles,
			Kilometers}

		;

		/// <summary>  
		/// Returns the distance in miles or kilometers of any two  
		/// latitude / longitude points.  
		/// </summary>  
		/// <param name=”pos1″></param>  
		/// <param name=”pos2″></param>  
		/// <param name=”type”></param>  
		/// <returns></returns>  
		/// <remarks>
		/// http://en.wikipedia.org/wiki/Haversine_formula
		/// </remarks>
		public static double Haversine_Distance (double lat1, double lng1, double lat2, double lng2, LocationDistanceType type)
		{
			double R = (type == LocationDistanceType.Miles) ? 3960 : 6371;
			double dLat = LocationUtil.toRadian (lat2 - lat1);
			double dLon = LocationUtil.toRadian (lng2 - lng1);  
			double a = Math.Sin (dLat / 2) * Math.Sin (dLat / 2) +
			                    Math.Cos (LocationUtil.toRadian (lat1)) * Math.Cos (LocationUtil.toRadian (lat2)) *
			                    Math.Sin (dLon / 2) * Math.Sin (dLon / 2);  
			double c = 2 * Math.Asin (Math.Min (1, Math.Sqrt (a)));  
			double d = R * c;  
			return d;  
		}

		/// <summary>  
		/// Convert to Radians.  
		/// </summary>  
		/// <param name="val"></param>  
		/// <returns></returns>  
		private static double toRadian (double val)
		{  
			return (Math.PI / 180) * val;  
		}

	}
}