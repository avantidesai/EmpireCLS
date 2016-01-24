using System;
using System.Collections.Generic;
using System.Linq;

using System.Xml.Serialization;


namespace  EmpireCLS
{
	public enum CacheId
	{
		[XmlEnum ()]
		Unknown,
		[XmlEnum ()]
		Airport,
		[XmlEnum ()]
		Vehicle,
		[XmlEnum ()]
		Airline,
		[XmlEnum ()]
		State
	}

	public partial class CacheList : ApiBaseModel
	{
		public CacheList ()
			: base ()
		{
			this.Results = new List<CacheListItem> ();
		}

		[XmlArrayItem ("Result", typeof(CacheListItem))]
		public List<CacheListItem> Results { get; set; }
	}

	public partial class CacheItemAirline
	{
		[XmlAttribute]
		public string Code { get; set; }

		[XmlAttribute]
		public string Name { get; set; }
	}

	public partial class CacheListItem : ApiBaseModel
	{
		[XmlAttribute]
		public CacheId CacheId { get; set; }

		[XmlAttribute]
		public int VersionNumber { get; set; }
	}




  


}