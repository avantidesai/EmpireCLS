using System;
using System.Linq;
using System.Collections.Generic;

using System.Json;

namespace EmpireCLS
{
	public sealed class JsonUtil
	{
		private static bool ContainsKey (JsonValue jsonValue, string key)
		{
			if (!jsonValue.ContainsKey (key)) {
				LogContext.Current.Log<JsonUtil> ("JsonValueString: Missing key", key);
				return false;
			} else
				return true;
		}

		public static string ToString (JsonValue jsonValue, string key, string defaultValue = "")
		{
			string returnValue = JsonUtil.ContainsKey (jsonValue, key)
				? jsonValue [key] as JsonPrimitive
				: defaultValue;		
			return returnValue;
		}

		public static double ToDouble (JsonValue jsonValue, string key, double defaultValue = 0.0)
		{
			double returnValue = JsonUtil.ContainsKey (jsonValue, key)
				? jsonValue [key] as JsonPrimitive
				: defaultValue;			
			return returnValue;
		}

		public static int ToInt (JsonValue jsonValue, string key, int defaultValue = 0)
		{
			int returnValue = JsonUtil.ContainsKey (jsonValue, key)
				? jsonValue [key] as JsonPrimitive
				: defaultValue;			
			return returnValue;
		}
		
	}
}

