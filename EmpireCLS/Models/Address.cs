using System;
using System.Collections.Generic;
using System.Linq;

using System.Xml.Serialization;

namespace EmpireCLS
{
	// TODO: merge this with profile.address, and maybe the EmpireCLS.Web.Entities.addressInfo class.  This class was originally to be called AddressInfo, but changed to LocationInfo because of conflicting name in EmpireCLS.Web.Entities - Troy
	/// <summary>
	///
	/// </summary>

	public enum LocationLookupProviderType
	{
		Unknown,
		Bing,
		Google,
		AddressBook,
		GooglePlace,
		GooglePlaceDetail
	}

	public partial class LocationInfo
	{
		public LocationInfo ()
		{
		}

		[XmlAttribute ()]
		public LocationLookupProviderType ProviderType { get; set; }

		[XmlAttribute ()]
		public double Lat { get; set; }

		[XmlAttribute ()]
		public double Lng { get; set; }

		[XmlAttribute ()]
		public string Country { get; set; }

		[XmlAttribute ()]
		public string StreetAddress { get; set; }

		[XmlAttribute ()]
		public string StreetAddress2 { get; set; }

		[XmlAttribute ()]
		public string City { get; set; }

		[XmlAttribute ()]
		public string State { get; set; }

		[XmlAttribute ()]
		public string Zip { get; set; }

		[XmlAttribute ()]
		public string Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// 6/5/14 JMO, added to provide better feedback in location lookups further up the stack
		/// </remarks>
		[XmlAttribute ()]
		public string Landmark { get; set; }

		private string _formattedAddress;

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// 2/18/14 JMO
		///     - use both name and street address
		/// </remarks>
		[XmlIgnore ()]
		public string MultiLineTitle {
			get {
				List<string> parts = new List<string> ();

				if (!string.IsNullOrWhiteSpace (this.Name))
					parts.Add (this.Name);

				if (!string.IsNullOrWhiteSpace (this.StreetAddress) && this.StreetAddress != this.Name)  // remove duplicated text if it's in both the name and the street address (happens with Google places)
                        parts.Add (this.StreetAddress);
                    
				string cityStateZip = null;
				if (!string.IsNullOrWhiteSpace (this.City))
					cityStateZip = this.City;

				if (!string.IsNullOrWhiteSpace (this.State))
					cityStateZip = !string.IsNullOrWhiteSpace (cityStateZip) ? cityStateZip + ", " + this.State : this.State;

				if (!string.IsNullOrWhiteSpace (this.Zip))
					cityStateZip = !string.IsNullOrWhiteSpace (cityStateZip) ? cityStateZip + " " + this.Zip : this.Zip;

				parts.Add (cityStateZip);

				return string.Join (Environment.NewLine, parts.ToArray ());
			}
		}

		[XmlIgnore ()]
		public string PlaceInfo { get; set; }

		[XmlIgnore ()]
		public string PlaceReference { get; set; }

		[XmlIgnore ()]
		public string FormattedAddress {
			get {
				if (string.IsNullOrWhiteSpace (_formattedAddress))
					return string.Format ("{0}, {1}, {2} {3}", this.StreetAddress, this.City, this.State, this.Zip);
				else
					return _formattedAddress;
			}
			set {
				_formattedAddress = value;
			}
		}

		public static LocationInfo Empty { get { return new LocationInfo (); } }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cloneFunc"></param>
		/// <returns></returns>
		/// <remarks>
		/// 6/5/14 JMO, inverted cloning implementation to remove reference to empirecls assembly
		/// </remarks>
		public LocationInfo Clone (Func<LocationInfo, LocationInfo> cloneFunc)
		{
			LocationInfo clonedObject = cloneFunc (this);

			clonedObject.FormattedAddress = _formattedAddress == null ? null : _formattedAddress;
			return clonedObject;
		}

		[XmlIgnore ()]
		public bool IsValid {
			get {
				if (this.ProviderType == LocationLookupProviderType.GooglePlace)
					return !string.IsNullOrWhiteSpace (this.PlaceInfo) && !string.IsNullOrWhiteSpace (this.PlaceReference);

                    
				return this.ProviderType != LocationLookupProviderType.Unknown
				&& !string.IsNullOrWhiteSpace (this.FormattedAddress) && (
				    this.ProviderType == LocationLookupProviderType.AddressBook || (
				        this.Lat != 0.0 && this.Lng != 0.0
				    )
				)
				&& !string.IsNullOrWhiteSpace (this.Country)
				&& !string.IsNullOrWhiteSpace (this.StreetAddress)
				&& !string.IsNullOrWhiteSpace (this.City) && !string.IsNullOrWhiteSpace (this.State) && !string.IsNullOrWhiteSpace (this.Zip);
			}
		}
	}
        
}