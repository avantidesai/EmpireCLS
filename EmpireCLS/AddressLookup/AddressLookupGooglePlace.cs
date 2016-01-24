using System;

using System.Linq;
using System.Collections;
using System.Collections.Generic;

using System.Json;


namespace EmpireCLS.Mobile.AddressLookup
{
	public class AddressLookupGooglePlace : AddressLookupClient
	{
		public AddressLookupGooglePlace (Action<IAddressLookupClient> lookupCompletedHandler)
			: base (Settings.Current.GoogleMapApiHost, LocationLookupProviderType.GooglePlace, lookupCompletedHandler)
		{
		}

		public AddressLookupGooglePlace ()
			: base (Settings.Current.GoogleMapApiHost, LocationLookupProviderType.GooglePlace)
		{
		}

		protected override string OnGetUrlPath (string addressToLookup)
		{
			string urlPath = string.Format (
				                          "{0}?query={1}&sensor={2}&key={3}",
				                          "/maps/api/place/textsearch/json", addressToLookup, "true", Settings.Current.GooglePlacesApiKey
			                          );
			return urlPath;
		}

		protected override string OnGetUrlPath (double lat, double lng)
		{
			throw new NotImplementedException ();
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

		protected override LocationInfo[] ParseLookupResults ()
		{
			List<LocationInfo> addresses = new List<LocationInfo> ();
			foreach (JsonValue result in (this.Results as JsonArray)) {
				var types = (result ["types"] as JsonArray).ToList ();

				// no airports
				if (types.Exists (jv => (jv as JsonPrimitive) == "airport"))
					continue;

				LocationInfo info = new LocationInfo () { ProviderType = this.ProviderType };

				info.FormattedAddress = result ["formatted_address"] as JsonPrimitive;
				info.Name = result ["name"] as JsonPrimitive;
				info.PlaceReference = result ["reference"] as JsonPrimitive;
				info.PlaceInfo = result ["formatted_address"] as JsonPrimitive;

				// 6/5/14 JMO, landmark integration
				info.Landmark = AddressLookupUtils.CheckForLandmark (result, info.Name);
              
				/*   info.Landmark = (
                    result.ContainsKey("types") ? (result["types"] as JsonArray).ToList() : new List<JsonValue>()
                ).Exists(v =>
                    v.ToString().ToUpper() == "STADIUM" // build this out as necessary with valid types from here https://developers.google.com/places/documentation/supported_types
                ) ? info.Name : null;
                */
				JsonValue location = result ["geometry"] ["location"];
				info.Lat = location ["lat"] as JsonPrimitive;
				info.Lng = location ["lng"] as JsonPrimitive;
               
				// airports and train stations are handled separately using data from the api/linux, don't add via google
				if (info.IsValid && !info.Name.ToLower ().Contains ("airport"))
					addresses.Add (info);

			}

			return addresses.ToArray ();
		}
	}
}