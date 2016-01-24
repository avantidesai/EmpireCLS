using System;

using System.Linq;
using System.Collections;
using System.Collections.Generic;

using System.Json;


namespace EmpireCLS.Mobile.AddressLookup
{
	public class AddressLookupGooglePlaceDetail : AddressLookupClient
	{
		public AddressLookupGooglePlaceDetail (Action<IAddressLookupClient> lookupCompletedHandler)
			: base (Settings.Current.GoogleMapApiHost, LocationLookupProviderType.GooglePlaceDetail, lookupCompletedHandler)
		{
		}

		public AddressLookupGooglePlaceDetail ()
			: base (Settings.Current.GoogleMapApiHost, LocationLookupProviderType.GooglePlaceDetail)
		{
		}

		protected override string OnGetUrlPath (string addressToLookup)
		{
			string urlPath = string.Format (
				                          "{0}?reference={1}&sensor={2}&key={3}",
				                          "/maps/api/place/details/json", addressToLookup, "true", Settings.Current.GooglePlacesApiKey
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

			this.Results = jsonData ["result"];
		}

		protected override LocationInfo[] ParseLookupResults ()
		{
			List<LocationInfo> addresses = new List<LocationInfo> ();

			LocationInfo info = new LocationInfo () { ProviderType = LocationLookupProviderType.Google };

			AddressLookupGoogle.PopulateInfo (info, this.Results);

			addresses.Add (info);

			return addresses.ToArray ();
		}
	}
}