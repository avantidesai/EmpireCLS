using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;



namespace EmpireCLS
{
	class AddressLookupUtils
	{
		public static string CheckForLandmark (JsonValue searchResult, string locationName)
		{

			string result = "";
			result = (
			    searchResult.ContainsKey ("types") ? (searchResult ["types"] as JsonArray).ToList () : new List<JsonValue> ()
			).Exists (v =>
                v == "stadium" // build this out as necessary with valid types from here https://developers.google.com/places/documentation/supported_types
			) ? locationName : "";
			return result;
		}
	}
}