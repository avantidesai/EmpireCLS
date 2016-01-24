using System;
using System.Collections.Generic;
using System.Linq;


namespace EmpireCLS
{
	public class RealTimeFleetItem
	{
		public int VehicleID { get; set; }

		public string VehicleType { get; set; }

		public RealTimeLocation CurrentLocation { get; set; }

		public RealTimeLocation PreviousLocation { get; set; }

		public RealTimeLocation StartingLocation { get; set; }

		public RealTimeLocation FinalLocation { get; set; }

		public bool IsEngaged { get; set; }

		public string ChauffeurName { get; set; }
	}

	public class RealTimeLocation
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public DateTime Time { get; set; }

		public string Longitude { get; set; }

		public string Latitude { get; set; }
	}
}