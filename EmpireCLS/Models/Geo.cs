
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EmpireCLS
{
	public partial class PlacesSearchOptions
	{
		[XmlAttribute ()]
		public string Input { get; set; }
	}

	public partial class GooglePlacesSearchResults : ApiBaseModel
	{
		[XmlArrayItem ("prediction", typeof(GooglePlacePrediction))]
		public List<GooglePlacePrediction> predictions { get; set; }
	}

	public partial class GooglePlacePrediction
	{
		[XmlAttribute ()]
		public string description { get; set; }

		[XmlAttribute ()]
		public string id { get; set; }

		[XmlAttribute ()]
		public string reference { get; set; }
	}

	public partial class GooglePlaceLocation
	{
		[XmlAttribute ()]
		public string Country { get; set; }

		[XmlAttribute ()]
		public string Address1 { get; set; }

		[XmlAttribute ()]
		public string Locality { get; set; }

		[XmlAttribute ()]
		public string Region { get; set; }

		[XmlAttribute ()]
		public string PostalCode { get; set; }

		[XmlAttribute ()]
		public string Name { get; set; }

		[XmlAttribute ()]
		public string Type { get; set; }

		[XmlElement (IsNullable = true)]
		public decimal? Lat { get; set; }

		[XmlElement (IsNullable = true)]
		public decimal? Lng { get; set; }
	}

	public partial class GooglePlaceDetailsResponse : ApiBaseModel
	{
		public GooglePlaceLocation Location { get; set; }
	}
}
