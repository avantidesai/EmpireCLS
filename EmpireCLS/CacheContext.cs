using System;
using System.Threading;
using System.Net;
using System.Linq;
using System.Collections.Generic;

using Xamarin;

namespace EmpireCLS
{
	/// <summary>
	/// Cache loader.
	/// </summary>
	public partial class CacheContext : IWebClient
	{
		private static class Constants
		{
			public const string StateFileName = "CacheState.xml";
			public const string AirportFileName = "CacheAirport.xml";
			public const string VehicleFileName = "CacheVehicle.xml";
			public const string AirlineFileName = "CacheAirline.xml";
			public const string ListFileName = "CacheList.xml";
			public const string TripTypeFileName = "Data/CacheTripType.xml";
			public const string CreditCardTypeFileName = "Data/CacheCreditCardType.xml";
			public const string TripNotificationTypeFileName = "Data/CacheTripNotificationType.xml";
		}

		#region Singleton pattern

		private static readonly CacheContext _cacheContext = new CacheContext ();

		public static CacheContext Current { get { return _cacheContext; } }

		#endregion // singleton pattern

		private Timer _startupTimer = null;

		private List<Tuple<CustomerProfile, LocationInfo>> _contacts = new List<Tuple<CustomerProfile, LocationInfo>> ();

		private readonly Dictionary<CacheId, string> _cacheErrors = new Dictionary<CacheId, string> ();

		private string _contactError = null;
		private string _startError = null;

		private readonly List<CacheId> _updatedCaches = new List<CacheId> ();

		/// <summary>
		/// 
		/// </summary>
		public CacheContext ()
		{
		}

		#region contacts

		public Func<Tuple<CustomerProfile, LocationInfo>[]> ContactsLoader { get; set; }

		public Action<CacheContext> CachesLoaded { get; set; }



		/// <summary>
		/// Gets or sets the contacts.
		/// </summary>
		/// <value>
		/// The contacts.
		/// </value>
		public IEnumerable<Tuple<CustomerProfile, LocationInfo>> Contacts {
			get {
				lock (this) {
					return _contacts;
				}
			}

			private set {
				lock (this) {
					_contacts = new List<Tuple<CustomerProfile, LocationInfo>> (value);
				}
			}
		}

		#endregion // contacts

		#region server caches

		public CacheList LocalCacheList { get; private set; }

		/// <summary>
		/// Gets or sets the vehicles.
		/// </summary>
		/// <value>
		/// The vehicles.
		/// </value>
		public IEnumerable<Vehicle> Vehicles {
			get {
				lock (this) {
					return _vehicles;
				}
			}
			private set {
				lock (this) {
					_vehicles = new List<Vehicle> (value);
				}
			}
		}

		private List<Vehicle> _vehicles = new List<Vehicle> ();

		/// <summary>
		/// Gets or sets the trip types.
		/// </summary>
		/// <value>
		/// The trip types.
		/// </value>
		// TODO: Remove Method, TripType no longer used
		/*	public IEnumerable<TripType> TripTypes
            {
                get
                {
                    lock(this)
                    {
                        return _tripTypes;
                    }
                }
                private set
                {
                    lock(this)
                    {
                        _tripTypes = new List<TripType>(value);
                    }
                }
            }

            private List<TripType> _tripTypes = new List<TripType>();
            */
		/// <summary>
		/// Gets or sets the credit card types.
		/// </summary>
		/// <value>
		/// The trip types.
		/// </value>
		public IEnumerable<CacheItemCreditCardType> CreditCardTypes {
			get {
				lock (this) {
					return _creditCardTypes;
				}
			}
			private set {
				lock (this) {

					_creditCardTypes = new List<CacheItemCreditCardType> (value ?? Enumerable.Empty<CacheItemCreditCardType> ());
				}
			}
		}

		private List<CacheItemCreditCardType> _creditCardTypes = new List<CacheItemCreditCardType> ();

		/// <summary>
		/// Gets or sets the credit card types.
		/// </summary>
		/// <value>
		/// The trip types.
		/// </value>
		public IEnumerable<CacheItemTripNotificationType> TripNotificationTypes {
			get {
				lock (this) {
					return _tripNotificationTypes;
				}
			}
			private set {
				lock (this) {

					_tripNotificationTypes = new List<CacheItemTripNotificationType> (value ?? Enumerable.Empty<CacheItemTripNotificationType> ());
				}
			}
		}

		private List<CacheItemTripNotificationType> _tripNotificationTypes = new List<CacheItemTripNotificationType> ();

		/// <summary>
		/// Gets or sets the airline types.
		/// </summary>
		/// <value>
		/// The trip types.
		/// </value>
		public IEnumerable<Airline> Airlines {
			get {
				lock (this) {
					return _airlines;
				}
			}
			private set {
				lock (this) {
					_airlines = new List<Airline> {
						new Airline {
							Name = "Private/Other",
							Code = "PVT"
						}
					};
					_airlines.AddRange (value.OrderBy (x => x.Name));
				}
			}
		}

		private List<Airline> _airlines = new List<Airline> ();

		/// <summary>
		/// Gets or sets the airports.
		/// </summary>
		/// <value>
		/// The airports.
		/// </value>
		public IEnumerable<Airport> Airports {
			get {
				lock (this) {
					return _airports;
				}
			}
			private set {
				lock (this) {
					_airports = new List<Airport> (value);
				}
			}
		}

		private List<Airport> _airports = new List<Airport> ();

		private List<State> _states;

		public List<State> States {
			get {
				lock (this)
					return _states;
			}

			private set {
				lock (this)
					_states = new List<State> (value);
			}
		}

		#endregion // server caches

		/// <summary>
		/// Saves the caches.
		/// </summary>
		public void SaveCaches ()
		{
			Utilities.TryMobileAction<CacheContext> ("SaveCaches", () => {
				//ApplicationContext.Current.XmlEntitySave<CacheList>(Constants.ListFileName, CacheContext.Current.LocalCacheList);
				ApplicationContext.Current.XmlEntitySave<List<State>> (Constants.StateFileName, CacheContext.Current.States.ToList ());
				ApplicationContext.Current.XmlEntitySave<List<Airport>> (Constants.AirportFileName, CacheContext.Current.Airports.ToList ());
				ApplicationContext.Current.XmlEntitySave<List<Airline>> (Constants.AirlineFileName, CacheContext.Current.Airlines.ToList ());
				ApplicationContext.Current.XmlEntitySave<List<Vehicle>> (Constants.VehicleFileName, CacheContext.Current.Vehicles.ToList ());

				LogContext.Current.Log<CacheContext> ("SaveCaches", "Saved");
			});

		}

		/// <summary>
		/// Loads the caches.
		/// </summary>
		public void LoadCaches ()
		{
			Utilities.TryMobileAction<CacheContext> ("LoadCaches", () => {
				//TODO: we currently reload airports, airlines, vehicles from the API every time.  As a result, these lines aren't needed. Once
				//      caching is in place, reinstate these lines to loadcache from the device
				//	CacheContext.Current.LocalCacheList = ApplicationContext.Current.XmlEntityLoad<CacheList>(Constants.ListFileName);
				//	CacheContext.Current.Airports = ApplicationContext.Current.XmlEntityLoad<List<Airport>>(Constants.AirportFileName);
				//	CacheContext.Current.Airlines = ApplicationContext.Current.XmlEntityLoad<List<Airline>>(Constants.AirlineFileName);
				//	CacheContext.Current.Vehicles = ApplicationContext.Current.XmlEntityLoad<List<Vehicle>>(Constants.VehicleFileName);

				//NOTE: TripTypes nor CreditCardTypes are stored at the server.  The data is contained in CacheTripType.xml and CreditCardType.xml found in this project under Data\
				//CacheContext.Current.TripTypes = ApplicationContext.Current.XmlEntityLoad<TripTypeCollection>(Constants.TripTypeFileName).TripTypes;
				CacheContext.Current.States = ApplicationContext.Current.XmlEntityLoad<List<State>> (Constants.StateFileName);
				CacheContext.Current.CreditCardTypes = ApplicationContext.Current.XmlEntityLoad<CreditCardTypeCollection> (Constants.CreditCardTypeFileName).CreditCardTypes;
				CacheContext.Current.TripNotificationTypes = ApplicationContext.Current.XmlEntityLoad<TripNotificationTypeCollection> (Constants.TripNotificationTypeFileName).TripNotificationTypes;
				LogContext.Current.Log<CacheContext> ("LoadCaches", "Loaded");
			});

		}

		/// <summary>
		/// determine if cache should be downloaded
		/// </summary>
		/// <returns>
		/// The load.
		/// </returns>
		/// <param name='cacheId'>
		/// If set to <c>true</c> cache identifier.
		/// </param>
		/// <param name='serverList'>
		/// If set to <c>true</c> server list.
		/// </param>
		//private bool ShouldDownload(CacheId cacheId, CacheList serverList)
		//{
		//    // TODO: always return true for now since we don't have a cache/version at the API level; once that is in place, remove this line 
		//    return true;

		//    if (CacheContext.Current.LocalCacheList == null || serverList == null)
		//        return true;

		//    CacheListItem localCacheListItem = CacheContext.Current.LocalCacheList.Results.FirstOrDefault(i => i.CacheId == cacheId);
		//    if (localCacheListItem == null)
		//        return true;

		//    CacheListItem serverCacheListItem = serverList.Results.FirstOrDefault(i => i.CacheId == cacheId);
		//    if (serverCacheListItem == null)
		//        return true;

		//    if (localCacheListItem.VersionNumber != serverCacheListItem.VersionNumber)
		//        return true;

		//    // don't load
		//    return false;
		//}

		/// <summary>
		/// Caches the load timer callback.
		/// </summary>
		/// <param name='state'>
		/// State.
		/// </param>
		private void TimerCallback (object state)
		{
			try {

				if (_startupTimer != null)
					_startupTimer.Dispose ();

				// all caches
				using (CachesClient c = new CachesClient ()) {
					// get the current cache list from the server
					// TODO: Short Term: check date on files on device, if less than a week old, don't download data
					//		 Long Term:Caches and versioning not stored on the server at this point.  At some point, add cache/version
					// 			logic on the server API so we don't have to pull all of the data all of the time
					//	CacheList serverCacheList = null;
					/*if (!c.GetCacheList().HasErrors)
                        serverCacheList = c.GetCacheListResult;					
                    */


					// airports
					//if (ShouldDownload(CacheId.Airport, serverCacheList))
					//{
					if (!c.GetAirports ().HasErrors) {
						_updatedCaches.Add (CacheId.Airport);
						CacheContext.Current.Airports = c.GetAirportsResult.Results;
						LogContext.Current.Log<CacheContext> ("TimerCallback.Airports", c.GetAirportsResult.Results.Count ());
					} else
						_cacheErrors.Add (CacheId.Airport, c.ErrorMessage);

					//}

					// vehicles
					//	if (ShouldDownload(CacheId.Vehicle, serverCacheList))
					//	{
					if (!c.GetVehicles ().HasErrors) {
						_updatedCaches.Add (CacheId.Vehicle);
						CacheContext.Current.Vehicles = c.GetVehiclesResult.Results;
						LogContext.Current.Log<CacheContext> ("TimerCallback.Vehicles", c.GetVehiclesResult.Results.Count ());
					} else
						_cacheErrors.Add (CacheId.Vehicle, c.ErrorMessage);
					//	}



					// airlines
					//	if (ShouldDownload(CacheId.Airline, serverCacheList))
					//	{
					if (!c.GetAirlines ().HasErrors) {
						_updatedCaches.Add (CacheId.Airline);
						CacheContext.Current.Airlines = c.GetAirlineResult.Results;
						LogContext.Current.Log<CacheContext> ("TimerCallback.Airlines", c.GetAirlineResult.Results.Count ());
					} else
						_cacheErrors.Add (CacheId.Airline, c.ErrorMessage);

					if (!c.GetStates ().HasErrors) {
						_updatedCaches.Add (CacheId.State);
						CacheContext.Current.States = c.GetStatesResult.Results;
						LogContext.Current.Log<CacheContext> ("TimerCallback.States", c.GetStatesResult.Results.Count ());
					} else
						_cacheErrors.Add (CacheId.State, c.ErrorMessage);
					//	}

					//	CacheContext.Current.LocalCacheList = serverCacheList;

					if (this.CachesLoaded != null)
						this.CachesLoaded (this);

				}

			} catch (Exception ex) {
				LogContext.Current.Log<CacheContext> ("TimerCallback.Error", ex);
				if (LogContext.CrashReportEnabled && Settings.Current.EnableCrashReportExecution) {
					Insights.Report (ex);
				}


				_startError = ex.Message;
			}

			LoadContacts ();

			NotifyCompleted ();

		}

		private void TimerCallbackAirlines (object state)
		{
			try {

				if (_startupTimer != null)
					_startupTimer.Dispose ();

				// all caches
				using (CachesClient c = new CachesClient ()) {
					// get the current cache list from the server
					// TODO: Short Term: check date on files on device, if less than a week old, don't download data
					//		 Long Term:Caches and versioning not stored on the server at this point.  At some point, add cache/version
					// 			logic on the server API so we don't have to pull all of the data all of the time
					//	CacheList serverCacheList = null;
					/*if (!c.GetCacheList().HasErrors)
                        serverCacheList = c.GetCacheListResult;					
                    */


					// airports
					//if (ShouldDownload(CacheId.Airport, serverCacheList))
					//{
					if (!c.GetAirports ().HasErrors) {
						_updatedCaches.Add (CacheId.Airport);
						CacheContext.Current.Airports = c.GetAirportsResult.Results;
						LogContext.Current.Log<CacheContext> ("TimerCallback.Airports", c.GetAirportsResult.Results.Count ());
					} else
						_cacheErrors.Add (CacheId.Airport, c.ErrorMessage);


					// airlines
					//	if (ShouldDownload(CacheId.Airline, serverCacheList))
					//	{
					if (!c.GetAirlines ().HasErrors) {
						_updatedCaches.Add (CacheId.Airline);
						CacheContext.Current.Airlines = c.GetAirlineResult.Results;
						LogContext.Current.Log<CacheContext> ("TimerCallback.Airlines", c.GetAirlineResult.Results.Count ());
					} else
						_cacheErrors.Add (CacheId.Airline, c.ErrorMessage);


					if (this.CachesLoaded != null)
						this.CachesLoaded (this);

				}

			} catch (Exception ex) {
				LogContext.Current.Log<CacheContext> ("TimerCallback.Error", ex);
				if (LogContext.CrashReportEnabled && Settings.Current.EnableCrashReportExecution) {
					Insights.Report (ex);
				}


				_startError = ex.Message;
			}

			//LoadContacts ();

			NotifyCompleted ();
		}

		/// <summary>
		/// Gets the loaded caches, will only contain IDs for caches that have been loaded from server
		/// </summary>
		/// <value>
		/// The loaded caches.
		/// </value>
		public IEnumerable<CacheId> UpdatedCaches { get { return _updatedCaches; } }


		/// <summary>
		/// Loads the contacts.
		/// </summary>
		public void LoadContacts ()
		{
			try {
				// contacts
				if (this.ContactsLoader != null)
					CacheContext.Current.Contacts = this.ContactsLoader ();

			} catch (Exception ex) {
				LogContext.Current.Log<CacheContext> ("LoadContacts.Error", ex);
				if (LogContext.CrashReportEnabled && Settings.Current.EnableCrashReportExecution) {
					Insights.Report (ex);
				}


				_contactError = ex.Message;
			}

		}

		/// <summary>
		/// Notifies the completed.
		/// </summary>
		private void NotifyCompleted ()
		{
			Utilities.TryMobileAction<CacheContext> ("CacheContext.NotifyCompleted", () => {

				if (this.ClientCompleted != null)
					this.ClientCompleted (this);
			});

		}

		/// <summary>
		/// Loads data needed from the server, asynchronously.  Checks local cache
		/// first to make sure local cache is outdated before bringing data down from 
		/// the server
		/// </summary>
		/// <returns>
		/// The async.
		/// </returns>
		public CacheContext LoadAsync ()
		{
			_startupTimer = new Timer (TimerCallback, this, 20, 0);
			return this;
		}

		public CacheContext LoadAsyncAirlines ()
		{
			_startupTimer = new Timer (TimerCallbackAirlines, this, 20, 0);
			return this;
		}

		#region IWebclient

		public bool HasErrors {
			get {
				lock (this) {
					return !string.IsNullOrWhiteSpace (_startError) || !string.IsNullOrWhiteSpace (_contactError) || _cacheErrors.Count > 0;
				}
			}
		}

		public Exception LastError {
			get {
				string errorMessage = this.ErrorMessage;
				return string.IsNullOrWhiteSpace (errorMessage) ? null : new ApplicationException (errorMessage);
			}
		}

		public string ErrorMessage {
			get {
				lock (this) {
					List<string> errors = new List<string> (
						                      from e in _cacheErrors
						                      select string.Format ("{0}: {1}", e.Key, e.Value)
					                      );

					if (!string.IsNullOrWhiteSpace (_startError))
						errors.Add ("Timer: " + _startError);

					if (!string.IsNullOrWhiteSpace (_contactError))
						errors.Add ("Contacts: " + _contactError);

					string errorMessage = string.Join (", ", errors.ToArray ());
					return errorMessage;
				}
			}
		}

		public Action<IWebClient> ClientCompleted { get; set; }

		#endregion // IWebClient
	}
}

