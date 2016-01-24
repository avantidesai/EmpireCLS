using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using System.Reflection;


using EmpireCLS.Mobile.ApiClient;


using System.Threading.Tasks;
using Xamarin;

namespace EmpireCLS
{
	public class UserContext : IWebClient
	{
		#region singleton

		private static readonly UserContext _userContext = new UserContext ();

		/// <summary>
		/// support singleton
		/// </summary>
		/*private UserContext ()
        {
            this.User = new AccountUserInfo ();
            this.Person = new CustomerProfile ();
            this.Address = new LocationInfo ();
        }
        */
		public static UserContext Current { get { return _userContext; } }

		#endregion

		/// <summary>
		/// 
		/// </summary>
		public UserContext ()
		{
		}

		private Timer _startupTimer = null;
		private string _startError = null;

		private List<ProfileCreditCard> _profileCreditCards = new List<ProfileCreditCard> ();

		private AccountUserInfo _user;

		public AccountUserInfo User {
			get
            { return _user; }
			internal set { _user = value; }
		}

		public UserRoles UserRoles { get; internal set; }

		public CustomerProfile Person { get; set; }

		public LocationInfo Address { get; internal set; }

		public AddressList ProfileAddresses { get; internal set; }

		public AddressList ProfileCorpAddresses { get; internal set; }


		private List<SpecialBillingRequirement> _specialBillingRequirements = new List<SpecialBillingRequirement> ();

		public CorporateRequirements ProfileCorpRequirements { get; set; }

		//TODO: Reactivate this?
		/*public void UpdateProfileInfo(PersonInfo person, AddressInfo address)
        {
            this.Person = person;
            this.Address = address;
        }
        */

		public IEnumerable<ProfileCreditCard> ProfileCreditCards {
			get {
				lock (this) {
					return _profileCreditCards;
				}
			}
			set {
				lock (this) {
					_profileCreditCards = new List<ProfileCreditCard> (value);
				}
			}
		}

		public IEnumerable<SpecialBillingRequirement> SpecialBillingRequirementQuestions {
			get {
				lock (this) {
					return _specialBillingRequirements;
				}
			}
			set {
				lock (this) {
					_specialBillingRequirements = new List<SpecialBillingRequirement> (value);
				}
			}
		}

		public IEnumerable<SpecialBillingRequirement> SpecialBillingRequirements {
			get {
				lock (this) {
					return _specialBillingRequirements;
				}
			}
			set {
				lock (this) {
					_specialBillingRequirements = new List<SpecialBillingRequirement> (value);
				}
			}
		}
		//public  IEnumerable<ProfileCreditCard> ProfileCreditCards { get; internal set;}
		/*public IEnumerable<ProfileCreditCard> ProfileCreditCards 
        { 
            get
            {
                return _profileCreditCards;
            }
			
            set
            {
                _profileCreditCards.Clear();
                _profileCreditCards.AddRange(value);
            }
        }
*/
		/// <summary>
		/// Loads data needed from the server, asynchronously.  
		/// </summary>
		/// <returns>
		/// The async.
		/// </returns>
		public UserContext LoadAsync ()
		{

			_startupTimer = new Timer (TimerCallback, this, 20, 0);

			return this;

		}

		public void UserLoaded ()
		{
			Utilities.TryMobileAction<UserContext> ("UserLoaded", () => {
				LogContext.Current.Log<UserContext> ("SaveCaches", "Saved");
			});


		}

		public async void UpdateProfileCreditCardsDetailedAsync ()
		{
			using (AccountClient ac = new AccountClient ()) {
				await Task.Factory.StartNew (() => GetProfileCreditCardsDetailed (ac));
			}
		}

		public CreditCard[] GetProfileCreditCardsDetailed (AccountClient ac = null)
		{
			if (!ApplicationContext.Current.IsLoggedIn)
				return new CreditCard[0];

			if (ac == null)
				using (var ac2 = new AccountClient ())
					return GetProfileCreditCardsDetailed (ac2);
			else {

				if (!ac.ProfileCreditCardsDetailedGet ().HasErrors) {
					// default credit cards to display in descending order, theorizing that the last one added is the preferred card to use
					var detailedCards = ac.GetProfileCreditCardsDetailedResult.Results.OrderBy (cc => cc.UniqueID).ToArray ();
					return detailedCards;
				}
			}

			return new CreditCard[0];
		}

		public async void UpdateProfileCreditCardsAsync ()
		{
			using (AccountClient ac = new AccountClient ()) {
				await Task.Factory.StartNew (() => UpdateProfileCreditCards (ac));
			}
		}

		public void UpdateProfileCreditCards (AccountClient ac = null)
		{
			if (!ApplicationContext.Current.IsLoggedIn)
				return;

			if (ac == null)
				using (var ac2 = new AccountClient ())
					UpdateProfileCreditCards (ac2);
			else {

				if (!ac.ProfileCreditCardsGet ().HasErrors) {
					// default credit cards to display in descending order, theorizing that the last one added is the preferred card to use
					var profileCards = ac.GetProfileCreditCardsResult.Results.OrderBy (cc => cc.CreditCardID).ToArray ();
					UserContext.Current.ProfileCreditCards = profileCards;
					LogContext.Current.Log<CacheContext> ("UserContext.TimerCallback.ProfileCreditCardsGet");
				}
			}
		}

		public void ClearUserContext ()
		{
			UserContext.Current.ProfileCorpRequirements = null;
			UserContext.Current.ProfileCreditCards = new List<ProfileCreditCard> ();
			UserContext.Current.SpecialBillingRequirements = new List<SpecialBillingRequirement> ();
			;
			UserContext.Current.ProfileAddresses = null;
		}

		private void TimerCallback (object state)
		{
			try {
				if (_startupTimer != null)
					_startupTimer.Dispose ();

				// all caches
				using (AccountClient ac = new AccountClient ()) {


					if (!ac.ProfileGet ().HasErrors) {
						UserContext.Current.Person = ac.GetProfileResult;
						LogContext.Current.Log<CacheContext> ("UserContext.TimerCallback.ProfileGet");

						if (!String.IsNullOrWhiteSpace (UserContext.Current.Person.CorporationNumber1)) {
							// get corporate offices for addresses
							if (!ac.GetCorpAddresses (UserContext.Current.Person.CorporationNumber1, false).HasErrors) {
								UserContext.Current.ProfileCorpAddresses = ac.GetCorpAddressesResult;
								LogContext.Current.Log<CacheContext> ("UserContext.TimerCallback.CORPAddressesGet");
							}

							// get corporate requirments
							if (!ac.GetCorpRequirements (UserContext.Current.Person.CorporationNumber1, false).HasErrors) {
								UserContext.Current.ProfileCorpRequirements = ac.GetCorpRequirementsResult;
								UserContext.Current.SpecialBillingRequirementQuestions = ac.GetCorpSpecialBillingResult;
								LogContext.Current.Log<CacheContext> ("UserContext.TimerCallback.CORPRequirementsGet");
							}

							// NOTE: Corp credit cards are retrieved during ProfileCreditCards get, so we don't need a separate call
							// get corporate credit cards 
							/* if (!ac.CorporateCreditCardsGet(false).HasErrors)
                             {
                                 UserContext.Current.ProfileCorporateCreditCards = ac.GetCorporateCreditCardsResult.Results;
                                 LogContext.Current.Log<CacheContext>("UserContext.TimerCallback.CORPCreditCardsGet");
                             }*/
						}
					}

					if (!ac.RolesGet ().HasErrors) {
						UserContext.Current.UserRoles = ac.GetRoleseResult;
						LogContext.Current.Log<CacheContext> ("UserContext.TimerCallback.RolesGet");
					}

					UpdateProfileCreditCards (ac);

					if (!ac.GetAddresses ().HasErrors) {
						UserContext.Current.ProfileAddresses = ac.GetAddressesResult;
						LogContext.Current.Log<CacheContext> ("UserContext.TimerCallback.AddressesGet");

					}

					/*	if (!ac.ProfileGet().HasErrors)
                        {
                            _updatedCaches.Add(CacheId.Airport);
                            CacheContext.Current.Airports = c.GetAirportsResult.Results;
                            LogContext.Current.Log<CacheContext>("TimerCallback.Airports", c.GetAirportsResult.Results.Count());
                        }
                        else
                            _cacheErrors.Add(CacheId.Airport, c.ErrorMessage);

                        //}

                        // vehicles
                        //	if (ShouldDownload(CacheId.Vehicle, serverCacheList))
                        //	{
                        if (!c.GetVehicles().HasErrors)
                        {
                            _updatedCaches.Add(CacheId.Vehicle);
                            CacheContext.Current.Vehicles = c.GetVehiclesResult.Results;
                            LogContext.Current.Log<CacheContext>("TimerCallback.Vehicles", c.GetVehiclesResult.Results.Count());
                        }					
                        else
                            _cacheErrors.Add(CacheId.Vehicle, c.ErrorMessage);
                        //	}



                        // airlines
                        //	if (ShouldDownload(CacheId.Airline, serverCacheList))
                        //	{
                        if (!c.GetAirlines().HasErrors)
                        {
                            _updatedCaches.Add(CacheId.Airline);							
                            CacheContext.Current.Airlines = c.GetAirlineResult.Results;
                            LogContext.Current.Log<CacheContext>("TimerCallback.Airlines", c.GetAirlineResult.Results.Count());
                        }					
                        else
                            _cacheErrors.Add(CacheId.Airline, c.ErrorMessage);
                        //	}

                        //	CacheContext.Current.LocalCacheList = serverCacheList;

                        if (this.CachesLoaded != null)
                            this.CachesLoaded(this);
    */
				}

			} catch (Exception ex) {
				LogContext.Current.Log<UserContext> ("UserContext.TimerCallback.Error", ex);
				if (LogContext.CrashReportEnabled && Settings.Current.EnableCrashReportExecution) {
					Insights.Report (ex);
				}


				_startError = ex.Message;
			}


			NotifyCompleted ();

		}

		private void NotifyCompleted ()
		{
			Utilities.TryMobileAction<UserContext> ("NotifyCompleted", () => {
				if (this.ClientCompleted != null)
					this.ClientCompleted (this);
			});

		}

		#region IWebclient

		public bool HasErrors {
			get {
				lock (this) {
					return !string.IsNullOrWhiteSpace (_startError); // || !string.IsNullOrWhiteSpace(_contactError) || _cacheErrors.Count > 0;
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
					List<string> errors = new List<string> ();
					//	List<string> errors = new List<string>(
					//		from e in _cacheErrors select string.Format("{0}: {1}", e.Key, e.Value)	
					//		);

					if (!string.IsNullOrWhiteSpace (_startError))
						errors.Add ("Timer: " + _startError);

					//if (!string.IsNullOrWhiteSpace(_contactError))
					//	errors.Add("Contacts: " + _contactError);

					string errorMessage = string.Join (", ", errors.ToArray ());
					return errorMessage;
				}
			}
		}

		public Action<IWebClient> ClientCompleted { get; set; }

		#endregion
	}
}

