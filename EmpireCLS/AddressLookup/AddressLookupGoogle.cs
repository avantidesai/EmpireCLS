using System;

using System.Linq;
using System.Collections;
using System.Collections.Generic;

using System.Json;


namespace EmpireCLS.Mobile.AddressLookup
{
	/// <summary>
	/// Address lookup google.
	/// </summary>
	public class AddressLookupGoogle : AddressLookupClient
	{
		public AddressLookupGoogle (Action<IAddressLookupClient> lookupCompletedHandler)
			: base (Settings.Current.GoogleMapApiHost, LocationLookupProviderType.Google, lookupCompletedHandler)
		{
		}

		public AddressLookupGoogle ()
			: base (Settings.Current.GoogleMapApiHost, LocationLookupProviderType.Google)
		{
		}

		protected override string OnGetUrlPath (string addressToLookup)
		{
			/*	string urlPath = string.Format(
				"{0}?address={1}&sensor={2}",
				"/maps/api/geocode/json", addressToLookup, "true"
			);*/

			string urlPath = string.Format ("{0}&address={1}", Settings.Current.GoogleApiUrl, addressToLookup);
			return urlPath;
		}

		protected override string OnGetUrlPath (double lat, double lng)
		{
			string urlPath = string.Format (
				                 "{0}?latlng={1}&sensor={2}",
				                 "/maps/api/geocode/json", string.Format ("{0},{1}", lat, lng), "true"
			                 );
			return urlPath;
		}

		protected override void ParseResponse (string urlPath)
		{
			JsonValue jsonData = JsonObject.Parse (this.ResponseString);
			
			string status = jsonData ["status"] as JsonPrimitive;
			if (status != "OK") {
				this.Error = new ApplicationException (status);
				return;
			}
			
			this.Results = jsonData ["results"];
		}

		/// <summary>
		/// generic way to populate a LocationInfo from google API json address result
		/// </summary>
		/// <param name="info"></param>
		/// <param name="result"></param>
		/// <remarks>
		/// 2/13/14 JMO
		///     - facgtored for shared usage for placeDetail lookup
		/// </remarks>
		public static void PopulateInfo (LocationInfo info, JsonValue result)
		{
			info.FormattedAddress = result ["formatted_address"] as JsonPrimitive;

			JsonValue location = result ["geometry"] ["location"];
			info.Lat = location ["lat"] as JsonPrimitive;
			info.Lng = location ["lng"] as JsonPrimitive;
			info.Name = "";
			JsonArray components = result ["address_components"] as JsonArray;
			foreach (JsonValue c in components) {
				string shortName = c ["short_name"] as JsonPrimitive;

				foreach (JsonValue t in c["types"] as JsonArray) {
					string type = t as JsonPrimitive;

					if (type == "street_number")
						info.StreetAddress = shortName;
					else if (type == "route")
						info.StreetAddress = info.StreetAddress == null ? shortName : string.Format ("{0} {1}", info.StreetAddress, shortName);
					else if (type == "sublocality" || type == "locality" || type == "administrative_area_level_3")
						info.City = shortName;
					else if (type == "administrative_area_level_1")
						info.State = shortName;
					else if (type == "postal_code")
						info.Zip = shortName;
					else if (type == "country" && shortName == "US")
						info.Country = shortName;
					else if (type == "establishment") {
						info.Name = shortName;

					}
				}
			}

			// 6/5/14 JMO, landmark usage
			info.Landmark = AddressLookupUtils.CheckForLandmark (result, info.Name);
          
           
		}

		protected override LocationInfo[] ParseLookupResults ()
		{
			List<LocationInfo> addresses = new List<LocationInfo> ();
			
			var results = this.Results as JsonArray;
			// Ideally, reverse this logic and throw a custom exception.  InvalidContactAddressException

			if (results == null) {
				throw new InvalidAddressException ("No Lookup Results");
			}

			foreach (var result in results) {
				LocationInfo info = new LocationInfo () { ProviderType = this.ProviderType };

				AddressLookupGoogle.PopulateInfo (info, result);

				// airports and train stations are handled separately using data from the api/linux, don't add via google
				if (info.IsValid && !info.Name.ToLower ().Contains ("airport"))
					addresses.Add (info);
			}
			
			return addresses.ToArray ();
		}
				
	}
}

