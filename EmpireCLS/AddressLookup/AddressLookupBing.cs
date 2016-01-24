using System;

using System.Linq;
using System.Collections;
using System.Collections.Generic;

using System.Json;



namespace EmpireCLS.Mobile.AddressLookup
{
	/// <summary>
	/// Address lookup bing.
	/// </summary>
	public class AddressLookupBing : AddressLookupClient
	{
		public AddressLookupBing (Action<IAddressLookupClient> lookupCompletedHandler)
			: base (Settings.Current.BingMapApiHost, LocationLookupProviderType.Bing, lookupCompletedHandler)
		{
		}

		public AddressLookupBing ()
			: base (Settings.Current.BingMapApiHost, LocationLookupProviderType.Bing)
		{
		}

		protected override string OnGetUrlPath (string addressToLookup)
		{
			string urlPath = string.Format (
				                 "{0}/{2}?culture={1}&key={3}",
				                 "/REST/v1/Locations",
				                 "en-US",
				                 addressToLookup,
				                 "AnywxuVzEdE3WANPFCDH2UWbSIp1-rAT8iIGupMbGJej6FbmBIv9m7wlsWPvuKQc"
			                 );
			return urlPath;			
		}

		protected override string OnGetUrlPath (double lat, double lng)
		{
			string latLngToLookup = string.Format ("{0},{1}", lat, lng);		
			
			string urlPath = string.Format (
				                 "{0}/{2}?culture={1}&key={3}",
				                 "/REST/v1/Locations",
				                 "en-US",
				                 latLngToLookup,
				                 "AnywxuVzEdE3WANPFCDH2UWbSIp1-rAT8iIGupMbGJej6FbmBIv9m7wlsWPvuKQc"
			                 );
			return urlPath;			
		}

		protected override void ParseResponse (string urlPath)
		{
			JsonValue jsonData = JsonObject.Parse (this.ResponseString);
			
			string status = jsonData ["statusDescription"] as JsonPrimitive;
			if (status != "OK") {
				this.Error = new ApplicationException (status);
				return;
			}
			
			this.Results = jsonData ["resourceSets"];
		}

		protected override LocationInfo[] ParseLookupResults ()
		{
			List<LocationInfo> addresses = new List<LocationInfo> ();
			
			foreach (JsonValue resourceSet in (this.Results as JsonArray)) {			
				foreach (JsonValue resource in resourceSet["resources"] as JsonArray) {
					LocationInfo info = new LocationInfo () { ProviderType = this.ProviderType };
				
					JsonValue address = resource ["address"];
					{
						string country = JsonUtil.ToString (address, "countryRegion");
						if (country == "United States")
							info.Country = "US";
						info.State = JsonUtil.ToString (address, "adminDistrict");
						info.FormattedAddress = JsonUtil.ToString (address, "formattedAddress");														
						
						info.StreetAddress = JsonUtil.ToString (address, "addressLine");
						info.City = JsonUtil.ToString (address, "locality");
						info.Zip = JsonUtil.ToString (address, "postalCode");
						info.Landmark = JsonUtil.ToString (address, "landmark");
					}
					
					JsonValue point = (resource ["geocodePoints"] as JsonArray).FirstOrDefault (p => (p ["type"] as JsonPrimitive) == "Point");
					if (point != null) {
						info.Lat = point ["coordinates"] [0];
						info.Lng = point ["coordinates"] [1];
					}
					
					if (info.IsValid)
						addresses.Add (info);
				
				}
			}
			
			return addresses.ToArray ();
		}
	}
}

