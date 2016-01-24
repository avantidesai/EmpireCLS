using System;
using System.Xml.Serialization;
using System.ComponentModel;


namespace EmpireCLS
{

	public class TripNotificationTypeCollection
	{
		[XmlArrayItem ("TripNotificationType", typeof(CacheItemTripNotificationType))]
		public CacheItemTripNotificationType[] TripNotificationTypes { get; set; }
	}

	public class CacheItemTripNotificationType
	{
		[XmlAttribute ()]
		public int ID { get; set; }

		public string Code { get; set; }

		public string DisplayName { get; set; }

		public string Description { get; set; }

	}
}