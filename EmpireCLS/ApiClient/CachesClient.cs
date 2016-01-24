using System;

using System.Linq;
using System.Collections.Generic;



using EmpireCLS;



namespace EmpireCLS
{
	public class CachesClient : ApiClientBase
	{
		private static class UrlPaths
		{

			//	public const string CacheList = "/api/xml/caches";
			public const string StatesCache = "/api/Application/GetStates";
			public const string AirportCache = "/api/Application/Airports";
			public const string VehicleCache = "/api/Application/Vehicles";
			public const string AirlineCache = "/api/Application/Airlines";
			public const string GetDefaultSettings = "/api/Application/GetDefaultSettings";

			// TODO: Do we need AirportCacheFiltered? - Troy
			//public const string AirportCacheFiltered = "/api/xml/caches/cache?id=Airport&searchQuery={0}&maxResults={1}&includeInternational={2}";
		}

		public CachesClient ()
			: base (Settings.Current.EmpireCLSHost)
		{
			//this.Token = ApplicationContext.Current.SessionToken;

			AddUrlPathModelTypeMapping (UrlPaths.StatesCache, typeof(CacheState));
			AddUrlPathModelTypeMapping (UrlPaths.AirportCache, typeof(CacheAirport));
			AddUrlPathModelTypeMapping (UrlPaths.VehicleCache, typeof(CacheVehicle));
			AddUrlPathModelTypeMapping (UrlPaths.AirlineCache, typeof(CacheAirline));
			AddUrlPathModelTypeMapping (UrlPaths.GetDefaultSettings, typeof(DefaultSettings));

			//AddUrlPathModelTypeMapping(UrlPaths.CacheList, typeof(CacheList));
		}

		public CacheState GetStatesResult { get { return this.Model as CacheState; } }

		public CacheAirport GetAirportsResult { get { return this.Model as CacheAirport; } }

		public CacheVehicle GetVehiclesResult { get { return this.Model as CacheVehicle; } }

		public CacheAirline GetAirlineResult { get { return this.Model as CacheAirline; } }

		public DefaultSettings GetDefaultSettingsResult { get { return this.Model as DefaultSettings; } }

		public static State GetState (string stateAbbr)
		{
			return CacheContext.Current.States.SingleOrDefault (s => s.StateShortName.Equals (stateAbbr, StringComparison.OrdinalIgnoreCase));
		}

		public static Airline GetAirline (string airlineCode, string fbo)
		{
			if (!string.IsNullOrEmpty (fbo)) {
				return CacheContext.Current.Airlines.FirstOrDefault (x => x.Code == "PVT");
			}
			Airline result = null;

			if (airlineCode == null)
				return null;

			if (CacheContext.Current.Airlines != null) {
				result = (from a in CacheContext.Current.Airlines
				          where a.Code.ToUpper () == airlineCode.ToUpper ()
				          select a).FirstOrDefault ();

			}

			return result;
		}
		//public CacheList GetCacheListResult { get { return this.Model as CacheList; } }

		/*public CachesClient GetCacheList(bool async = false)
        {
            return InvokeStrategy<CachesClient>(()=>{
                if (!async)
                    Get (UrlPaths.CacheList);
                else
                    GetAsync(UrlPaths.CacheList);
            });			
        }
        */

		public static Vehicle GetVehicle (string carType, string carName)
		{
			Vehicle result = null;

			if (carType == null || carName == null)
				return null;

			if (CacheContext.Current.Vehicles != null) {
				result = (from v in CacheContext.Current.Vehicles
				          where v.PreferredCarType.ToUpper () == carType.ToUpper () && v.Name.ToUpper () == carName.ToUpper ()
				          select v).FirstOrDefault ();

			}

			return result;
		}

		public CachesClient GetVehicles (bool async = false)
		{
			return InvokeStrategy<CachesClient> (() => {

				if (!async)
					Get (UrlPaths.VehicleCache);
				else
					GetAsync (UrlPaths.VehicleCache);
			});
		}

		public CachesClient GetDefaultSettings (string appVersion, bool async = false)
		{
			return InvokeStrategy<CachesClient> (() => {
				var url = String.Format ("{0}/?appVersion={1}", UrlPaths.GetDefaultSettings, appVersion.Trim ());
				AddUrlPathModelTypeMapping (url, typeof(DefaultSettings));
				if (!async)
					Get (url);
				else
					GetAsync (url);
			});
		}


		public CachesClient GetAirports (bool async = false)
		{
			return InvokeStrategy<CachesClient> (() => {

				if (!async) {
					Get (UrlPaths.AirportCache);
				} else
					GetAsync (UrlPaths.AirportCache);
			});
		}

		/// <summary>
		/// Determines whether the airport code represents a train station for the specified airportCode.
		/// </summary>
		/// <returns><c>true</c> if this instance is aiport train the specified airportCode; otherwise, <c>false</c>.</returns>
		/// <param name="airportCode">Airport code.</param>
		public static bool IsAiportTrain (string airportCode)
		{
			bool result = false;

			if (CacheContext.Current.Airports != null) {
				result = (from a in CacheContext.Current.Airports
				          where a.AirportCode.ToUpper () == airportCode.ToUpper ()
				          select a.IsTrainStation).FirstOrDefault ();

			}

			return result;
		}

		public CachesClient GetAirlines (bool async = false)
		{
			return InvokeStrategy<CachesClient> (() => {

				if (!async)
					Get (UrlPaths.AirlineCache);
				else
					GetAsync (UrlPaths.AirlineCache);
			});
		}

		public CachesClient GetStates (bool async = false)
		{
			return InvokeStrategy<CachesClient> (() => {
				if (!async)
					Get (UrlPaths.StatesCache);
				else
					GetAsync (UrlPaths.StatesCache);
			});
		}
	}
}

