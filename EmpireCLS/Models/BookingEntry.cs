using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;


using System.Reflection;
using System.Text;

//using System.Reflection.Emit;

using System.Globalization;

namespace EmpireCLS
{
	public enum LocationFinderItemType
	{
		Unknown,
		Address,
		Airport,
		Train,
		Contact,
		Place,
		ProfileAddress,
		ProfileCorpAddress,
		Home,
		Establishment,
		LookupPlaceholder,
		LookupPlaceholderError
	}

	public class LocationFinderItem
	{
		public LocationFinderItem (Airport airport)
		{

			if (airport.IsTrainStation)
				this.Type = LocationFinderItemType.Train;
			else
				this.Type = LocationFinderItemType.Airport;
			this.DataItem = airport;

			this.Text = airport.AirportName;
			this.Detail = airport.AirportCode;

			//this.Lat = airport.Lat;
			//this.Lng = airport.Lng;
			this.FormattedAddress = null;

		}

		/// <summary>
		/// placeholder item
		/// </summary>
		/// <param name="text"></param>
		/// <param name="detail"></param>
		public LocationFinderItem (string text, string detail)
		{
			this.Type = LocationFinderItemType.LookupPlaceholder;

			this.Text = text;
			this.Detail = detail;
		}

		public LocationFinderItem (CustomerProfile person, LocationInfo address)
		{
			this.Type = LocationFinderItemType.Contact;
			this.DataItem = address;

			//this.Text = string.Format ("{0} {1}", person.FirstName, person.LastName);
			this.Detail = address.StreetAddress;			

			this.FormattedAddress = address.FormattedAddress;
			this.Lat = address.Lat;
			this.Lng = address.Lng;
		}

		public LocationFinderItem (Address profileAddress, LocationFinderItemType type)
		{
			if (profileAddress.IsPrimary)
				this.Type = LocationFinderItemType.Home;
			else
				this.Type = type;
			this.DataItem = profileAddress;

			this.Text = profileAddress.LocationDescription;
			this.Detail = string.Format ("{0}\n{1}, {2} {3}", profileAddress.LocationAddress, profileAddress.LocationCity, profileAddress.LocationState, profileAddress.LocationZip);
			this.FormattedAddress = profileAddress.FormattedAddress;
		}

		public LocationFinderItem (LocationInfo address)
		{
			this.Type = LocationFinderItemType.Address;

			if (address.ProviderType == LocationLookupProviderType.GooglePlace)
				this.Type = LocationFinderItemType.Place;
			else if (address.ProviderType == LocationLookupProviderType.Google && !string.IsNullOrWhiteSpace (address.Name))
				this.Type = LocationFinderItemType.Establishment; // treat a google establishment as a place

			if (address.ProviderType == LocationLookupProviderType.Bing) {
				address.StreetAddress = address.StreetAddress;
			}
			if (!string.IsNullOrEmpty (address.Landmark))
				address.StreetAddress = address.StreetAddress;

			this.DataItem = address;

			if (this.Type == LocationFinderItemType.Address || this.Type == LocationFinderItemType.Establishment) {
				if (!string.IsNullOrWhiteSpace (address.Name)) {
					this.Text = address.Name;
					this.Detail = string.Format ("{0}\n{1}, {2} {3}", address.StreetAddress, address.City, address.State, address.Zip);

				} else {
					this.Text = address.StreetAddress;
					this.Detail = string.Format ("{0}, {1} {2}", address.City, address.State, address.Zip);
				}

				this.FormattedAddress = address.FormattedAddress;
				this.Lat = address.Lat;
				this.Lng = address.Lng;

			} else {
				if (address.PlaceInfo != null) {
					var parts = address.PlaceInfo.Split (',');
					if (parts.Length == 0) {
						this.Text = address.PlaceInfo;

					} else {
						int firstHalf = (int)Math.Floor ((double)parts.Length / 2.0);

						this.Text = string.Join (", ", parts.Take (firstHalf).ToArray ());
						this.Detail = string.Join (", ", parts.Skip (firstHalf).ToArray ());
					}

					// prepend the name of the place if present, and it isn't already in the Text
					if (!string.IsNullOrWhiteSpace (address.Name) && this.Text.IndexOf (address.Name) == -1)
						this.Text = string.Format ("{0}\n{1}", address.Name, this.Text.Replace (address.Name, ""));
				}
			}
		}

		public string FormattedAddress { get; private set; }

		public LocationFinderItemType Type { get; private set; }

		public string Text { get; private set; }

		public string Detail { get; private set; }

		public object DataItem { get; private set; }

		public double Lat { get; private set; }

		public double Lng { get; private set; }

		public void UpdateTextAndDetails (string text, string detail, LocationFinderItemType itemType = LocationFinderItemType.LookupPlaceholder)
		{
			this.Text = text;
			this.Detail = detail;
			this.Type = itemType;
		}

		public Tuple<LocationInfo, Airport> LocationInfo {
			get {
				if (this.Type == LocationFinderItemType.Airport || this.Type == LocationFinderItemType.Train)
					return new Tuple<LocationInfo, Airport> (null, this.DataItem as Airport);
				else if (this.Type == LocationFinderItemType.Address || this.Type == LocationFinderItemType.Contact ||
				         this.Type == LocationFinderItemType.Establishment || this.Type == LocationFinderItemType.Home)
					return new Tuple<LocationInfo, Airport> (this.DataItem as LocationInfo, null);
				else
					return null;
			}
		}
	}

	public class LocationFinder
	{
		private System.Threading.Timer _searchTimer = null;

		private readonly AddressLookupAggregator _addressLookupClient = new AddressLookupAggregator ();

		private readonly List<LocationFinderItem> _searchResultItems = new List<LocationFinderItem> ();

		/// <summary>
		/// may need to be synchronized so use member and accessors to support locking later
		/// </summary>
		private string _searchString = "";

		private string _lastSearchString = "";

		/// <summary>
		/// 
		/// </summary>
		public LocationFinder ()
		{
			this.AllowAirports = true;
			this.AllowContacts = true;
			this.AllowAddresses = true;
			this.AllowProfileAddresses = true;
			this.AllowProfileCorporateAddresses = true;
		}

		public bool AllowAirports { get; set; }

		public bool AllowContacts { get; set; }

		public bool AllowAddresses { get; set; }

		public bool AllowProfileAddresses { get; set; }

		public bool AllowProfileCorporateAddresses { get; set; }

		public int Count {
			get {
				return _searchResultItems.Count;
			}
		}

		public LocationFinderItem ItemAt (int ordinal)
		{


			return _searchResultItems [ordinal];
		}

		public string SearchString {
			set {
				_searchString = value;
			}

			private get { return _searchString; }
		}

		public IEnumerable<LocationFinderItem> Items { get { return _searchResultItems.ToArray (); } }

		private Action FoundItemsStrategy { get; set; }

		public void Stop ()
		{
			if (_searchTimer != null) {
				_searchTimer.Dispose ();
				_searchTimer = null;
			}
		}

		private string _searchBarText = null;

		public string SearchBarTextLocked { 
			private get { 
				lock (this) { 
					return string.IsNullOrWhiteSpace (_searchBarText) ? "" : _searchBarText; 
				} 
			}
			set {
				lock (this) {
					_searchBarText = value;
				}
			}
		}

		#region proximity location

		public void ProximityLocationSet (double lat, double lng)
		{
			this.ProximityLocation = new Tuple<double, double> (lat, lng);
		}

		private Tuple<double, double> ProximityLocation {
			get {
				lock (this) {
					return _proximityLocation;
				}
			}
			set {
				lock (this) {
					_proximityLocation = value;
				}
			}
		}

		private Tuple<double, double> _proximityLocation = new Tuple<double,double> (0, 0);

		#endregion

		public void SearchTimeStart (int milliseconds, Action foundItemsStrategy)
		{
			this.FoundItemsStrategy = foundItemsStrategy;
			//this.SearchString = "";
			//_lastSearchString = "";
			_searchTimer = new System.Threading.Timer (SearchTimerTicked, null, 180, 0);
		}

		/// <summary>
		/// clears the last search string, will result in a retry of the address lookup stoff
		/// </summary>
		/// <remarks>
		/// 2/14/14 JMO
		///     - allow to retry search
		/// </remarks>
		public void ClearLastSearchString ()
		{
			_lastSearchString = "";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="state"></param>
		/// <remarks>
		/// 2/13/14 JMO
		///     - don't search unless you have 3 characters or more
		///     - trim the search string before searching (8 alvis returns marlton, 8 alvis and a space returns evesham)
		///     - if the search string changed after the address lookup returns, don't update the caller and set timer to immediately lookup address again
		///     - use placeholder to output search, and timeouts.  timeouts help when google or bing timeout and an expected address is not there
		///             - the placehoolder for "search" helps the user see that it's actually searching
		/// </remarks>
		private void SearchTimerTicked (object state)
		{
			Stop ();

			int searchTimerRestartMs = 180;
			Func<string> lookupStringFunc = () => this.SearchBarTextLocked.ToUpper ().Trim ();

			//string lookupString = this.SearchString.ToUpper();
			string lookupString = lookupStringFunc ();
			if (lookupString != _lastSearchString) {			
				_searchResultItems.Clear ();

				if (lookupString.Length >= 3) {
					/*	if (ApplicationContext.Current.IsLoggedIn && ApplicationContext.Current.CurrentUser != null && ApplicationContext.Current.CurrentUser.Person != null) {
                            CustomerProfile p = ApplicationContext.Current.CurrentUser.Person;

                                if (((p.Address + p.City + p.State + p.ZipCode).Replace(" ","").ToUpper ()).Contains (lookupString.Replace(" ","").ToUpper ())) {
                                Address homeAddress = new Address () { 
                                    LocationAddress = p.Address,
                                    LocationCity = p.City,
                                    LocationState = p.State,
                                    LocationZip = p.ZipCode
                                };
                                _searchResultItems.Add (new LocationFinderItem (homeAddress, LocationFinderItemType.Home));
                            }


                        }
                        */
					if (this.AllowAirports) {
						_searchResultItems.AddRange (
							from a in (
							        from a in CacheContext.Current.Airports
							        where a.AirportCode.ToUpper ().Contains (lookupString) || a.AirportName.ToUpper ().Contains (lookupString)
							        select new
								{
									SortOrder = a.AirportCode.ToUpper ().Contains (lookupString)
										? 1 : 2,
									Airport = a
								}
							    )
							orderby a.SortOrder ascending
							select new LocationFinderItem (a.Airport)
						);
					}

					if (this.AllowContacts) {
						/*CacheContext.Current.LoadContacts ();
						_searchResultItems.AddRange (
							from c in CacheContext.Current.Contacts
							where (c.Item1.FirstName + c.Item1.LastName).ToUpper ().Contains (
								    lookupString.Replace (" ", "").ToUpper ()
							    )
							select new LocationFinderItem (c.Item1, c.Item2)
						);*/
					}

					if (this.AllowProfileAddresses) {
						if (UserContext.Current.ProfileAddresses != null) {
							_searchResultItems.AddRange (
								from c in UserContext.Current.ProfileAddresses.Results
								where ((c.LocationDescription.ToUpper ()).Contains (
									    lookupString.ToUpper ()) ||
								    ((c.LocationAddress + c.LocationCity + c.LocationState + c.LocationZip).ToUpper ().Contains (
									    lookupString.Replace (" ", "").ToUpper ()
								    ))
								    )
								select new LocationFinderItem (c, LocationFinderItemType.ProfileAddress)

							);
						}
					}
					if (this.AllowProfileCorporateAddresses) {
						if (UserContext.Current.ProfileCorpAddresses != null) {
							_searchResultItems.AddRange (
								from c in UserContext.Current.ProfileCorpAddresses.Results
								where ((c.LocationDescription.ToUpper ()).Contains (
									    lookupString.ToUpper ()) ||
								    ((c.LocationAddress + c.LocationCity + c.LocationState + c.LocationZip).ToUpper ().Contains (
									    lookupString.Replace (" ", "").ToUpper ()
								    ))
								    )
								select new LocationFinderItem (c, LocationFinderItemType.ProfileCorpAddress)


							);
						}
					}

					if (this.AllowAddresses) {
						// use a placeholder item for when the address aggregator is running
						var placeHolderItem = new LocationFinderItem ("Searching for address...", lookupString);
						if (_searchResultItems.Count > 0)
							_searchResultItems.Insert (0, placeHolderItem);
						else
							_searchResultItems.Add (placeHolderItem);

						// notify what we  have so far
						if (this.FoundItemsStrategy != null)
							this.FoundItemsStrategy ();
						var lookups = _addressLookupClient.Lookup (lookupString).Lookups.ToList ();
						// just get the addresses
						var addresses = (
						                    from l in lookups
						                    from a in l.Addresses
						                    where a.IsValid
						                    select a
						                ).ToList ();

						// look for dupes, and build list of lists of items that can be removed
						var dupes = (
						                from i in (                            
						                        from a in addresses
						                        group a by a.MultiLineTitle into g
						                        select new {
									Key = g.Key,
									Items = g
								}
						                    )
						                where i.Items.Count () > 1
						                select i.Items.Skip (1).ToList ()
						            ).ToList ();

						// remove dupes
						dupes.ForEach (i => {
							i.ForEach (li => addresses.Remove (li));
						});

						// update placeholder accordingly
						if (lookups.Count (i => i.WasCancelled) == lookups.Count ())
							placeHolderItem.UpdateTextAndDetails ("Search had issues", "Touch to try again", LocationFinderItemType.LookupPlaceholderError);
						else if (addresses.Count > 0 && lookups.Exists (i => i.WasCancelled && (i.ProviderType == LocationLookupProviderType.Google || i.ProviderType == LocationLookupProviderType.GooglePlace)))
							placeHolderItem.UpdateTextAndDetails ("Search had issues, partial results shown", "Touch to try again", LocationFinderItemType.LookupPlaceholderError);
						else if (addresses.Count > 0 && lookups.Exists (i => i.WasCancelled && (i.ProviderType == LocationLookupProviderType.Bing)))
							placeHolderItem.UpdateTextAndDetails ("Search had issues, partial results shown", "Touch to try again", LocationFinderItemType.LookupPlaceholderError);
						else if (addresses.Count == 0 && _searchResultItems.Count (ri => ri.Type != LocationFinderItemType.LookupPlaceholder) == 0)
							placeHolderItem.UpdateTextAndDetails ("No results", lookupString);
						else
							_searchResultItems.Remove (placeHolderItem);

						// convert to locationFinder items
						// 6/5/14 JMO, updated with sort order logic to push specific location info object to the top
						var addressesSortedByProximity = (
						                                     from sa in (
						                                             from a in addresses
						                                             select new {
									Address = a,
									SortOrder = !string.IsNullOrWhiteSpace (a.Landmark) ? 0 : 1,
									Distance = this.ProximityLocation.Item1 != 0.0 && this.ProximityLocation.Item2 != 0.0 && a.Lng != 0.0 && a.Lng != 0.0
										? LocationUtil.Haversine_Distance (this.ProximityLocation.Item1, this.ProximityLocation.Item2, a.Lat, a.Lng, LocationUtil.LocationDistanceType.Miles)
										: 0.0
								}
						                                         )
						                                     orderby sa.SortOrder ascending, sa.Distance ascending
						                                     select sa
						                                 ).ToArray ();

						_searchResultItems.AddRange (
							from a in addressesSortedByProximity
							select new LocationFinderItem (a.Address)
						);
					}
				}

				_lastSearchString = lookupString;

				// 2/13/14 JMO, don't notify items found if the search string changed while finding the items, if changed behind our back rerun login in seperate loop
				if (lookupStringFunc () == _lastSearchString) {
					if (this.FoundItemsStrategy != null)
						this.FoundItemsStrategy ();
				} else {
					searchTimerRestartMs = 10;
				}

			}

			SearchTimeStart (searchTimerRestartMs, this.FoundItemsStrategy);
		}
	}



	public enum BookingEntryStatus
	{

		/// Code based on TripStatusCode constant: Unknown = 0,Open = 1,Completed = 4,NoShowed = 6,Canceled = 7
		Unknown = 0,
		Open = 1,
		Completed = 4,
		NoShowed = 6,
		Cancelled = 7
	}

	[XmlRoot ("TripDetail")]
	public partial class BookingEntry : TripDetail
	{

		#region Properties


		public bool IsDriverInfoNotificationsEnabled { get; set; }

		public bool IsDriverOnLocationNotificationsEnabled { get; set; }

		public List<TripDetailAdditionalStop> AdditionalStops { get; set; }

		public List<SpecialBillingRequirement> SpecialBillingRequirements { get; set; }
		/*public enum BookingEntryStatus
        {
            InProcess, Completed, Cancelled, NoShowed
        }  
        */
		public string PromotionCode { get; set; }

		public bool IsCreateProfile { get; set; }

		public bool IsCarSeatNeeded { get; set; }

		public bool IsChauffeurCommentsRequested { get; set; }

		public bool IsPickupSignRequested { get; set; }

		public bool IsPayingByCreditCard { get; set; }

		public string ValidationErrorMsg { get; set; }

		public TripLocation PickupLocation { get; set; }

		public TripLocation DropLocation { get; set; }

		public DateTime? TripDateTime { get; set; }

		public DateTime? DepartureDateTime { get; set; }

		public RecommendedPickupTime SuggestedPickupTime { get; set; }

		public bool RequiresHourEntry { get; set; }

		public Airline TripAirline { get; set; }

		public double? EstimatedNumberOfHours { get; set; }

		private Rate _selectedRate;

		public BookingEntryCreditCardInfo CreditCard { get; set; }

		public bool IsAddingNewCreditCard { get; set; }

		public Promotion BookingEntryPromotion { get; set; }

		public SpecialBillingRequirement SpecialBilling { get; set; }

		public TripUpdateConfirmation TripConfirmation { get; set; }

		public Rate SelectedRate {
			get { return _selectedRate; }
			set {
				_selectedRate = value;
				this.CarType = value.CarType;
				this.PreferredCarName = value.VehicleName;
			}
		}

		[XmlIgnore]
		public bool CreditCardAllowAddNew {
			get {
				return ApplicationContext.Current.IsLoggedIn &&
				this.CanEdit;

			}
		}

		public new string BookingDisclaimer {

			get {
				return @"* DISCLAIMER: All prices quoted on EmpireCLS' website are provided as an estimate ONLY. Any deviation from the requested service will result in appropriate additional charges.

Additional charges may also be incurred for areas outside of our corporate owned locations; as well as extra terminal, extra stops and/or waiting time, surcharges etc. incurred during the trip. All estimates include gratuity, applicable tax, STF; parking and tolls when applicable. Preferred vehicle types are based on availability.

Your chauffeur information will be available 30 minutes prior to the scheduled pickup time. Due to variables out of our control such as traffic and weather conditions your chauffeur information is subject to change without notice and may not be displayed in advance. 

Please call EmpireCLS at (800) 451-5466 or 
(201) 784-1200 for calls outside the U.S. and Canada should you have any questions or concerns.";
			}
		}

		public DateTime? CreditCardExpirationDate {
			get {
				DateTime result;
				if (DateTime.TryParse (this.CreditCardExpMonth + "/01/" + this.CreditCardExpYear, out result))
					return result;
				else
					return null;

			}

			set {
				if (value.HasValue) {
					this.CreditCardExpMonth = value.Value.ToString ("MM");
					this.CreditCardExpYear = value.Value.ToString ("yy");
				}
			}
		}

		[XmlIgnore]
		public bool IsCreditCardPersonal {
			get {

				// need to set the creditCardSubType needed by Atlas
				if (this.CreditCardSubType == "") {

					this.CreditCardSubType = "P"; // default to personal
				}
				return this.CreditCardSubType == "P";
			}
			set {

				if (value)
					this.CreditCardSubType = "P";
				else
					this.CreditCardSubType = "B";

			}
		}

		[XmlIgnore]
		public string CreditCardExpirationDateText {
			get {
				return this.CreditCardExpirationDate.HasValue ? string.Format ("Expires on {0:MM}/{0:yy}", this.CreditCardExpirationDate.Value) : "";
			}
		}

		[XmlIgnore]
		public IEnumerable<ProfileCreditCard> CreditCardProfileCardsToShow {
			get {

				// editing a new card so return none
				//if (this.CreditCardAllowEditBookingCard)
				//	return new ProfileCreditCard[] { };

				// not booking and editing so return all cards
				//else 
				if (ApplicationContext.Current.IsLoggedIn && this.CanAddNew || this.CanUpdate)
					return UserContext.Current.ProfileCreditCards;

                // booked and updating so return the existing card to use
                else if (ApplicationContext.Current.IsLoggedIn && !this.CanEdit && this.CreditCard.CardToUse != null)
					return new ProfileCreditCard[] { this.CreditCard.CardToUse };
				else
					return new ProfileCreditCard[] { };
			}
		}

		[XmlIgnore]
		public string PickupYear { get { return this.TripDateTime.HasValue ? this.TripDateTime.Value.Year.ToString () : ""; } }

		[XmlIgnore]
		public string PickupWeekDay { get { return this.TripDateTime.HasValue ? this.TripDateTime.Value.ToString ("ddd") : ""; } }


		[XmlIgnore]
		public string PickupMonth { get { return this.TripDateTime.HasValue ? this.TripDateTime.Value.ToString ("MMM") : ""; } }


		[XmlIgnore]
		public string PickupDay { get { return this.TripDateTime.HasValue ? this.TripDateTime.Value.Day.ToString () : ""; } }


		[XmlIgnore]
		public string MultiLineTripDateTime {
			get {
				return this.TripDateTime.HasValue
                    ? this.TripDateTime.Value.ToLongDateString () + Environment.NewLine + this.TripDateTime.Value.ToShortTimeString () : "";
			}
		}


		[XmlIgnore]
		public string SingleLineTripDateTime {
			get {
				return this.TripDateTime.HasValue
                    ? this.TripDateTime.Value.ToShortDateString () + " " + this.TripDateTime.Value.ToShortTimeString () : "";
			}
		}

		[XmlIgnore]
		public string MultiLineDepartureDateTime {
			get {
				return this.DepartureDateTime.HasValue
                    ? this.DepartureDateTime.Value.ToLongDateString () + Environment.NewLine + this.DepartureDateTime.Value.ToShortTimeString () : "";
			}
		}

		[XmlIgnore]
		public string SingleLineDepartureDateTime {
			get {
				return this.DepartureDateTime.HasValue
                    ? this.DepartureDateTime.Value.ToShortDateString () + " " + this.DepartureDateTime.Value.ToShortTimeString () : "";
			}
		}


		[XmlIgnore]
		public bool IsBooked { get { return !string.IsNullOrWhiteSpace (this.ConfirmationNumber); } }

		[XmlIgnore]
		public bool IsStep1Valid {
			get {
				bool isTripDateValid = false;
				bool isHourlyValid = true;


				if (TripDateTime == null)
					return false;

				DateTime tempDate;
				if (DateTime.TryParse (TripDateTime.Value.ToString (), out tempDate))
					isTripDateValid = true;
				else
					isTripDateValid = false;

				if (this.HourlyTripFlag) {



					if (this.EstimatedNumberOfHours <= 0)
						isHourlyValid = false;
				}

				return (this.PickupLocation != null && (this.PickupLocation.Address != null || this.PickupLocation.Airport != null))
				&& (this.DropLocation != null && (this.DropLocation.Address != null || this.DropLocation.Airport != null))

				&& isTripDateValid
				&& isHourlyValid;

				//&& ((this.TripType.ID == (int)TripTypes.Hourly && !string.IsNullOrWhiteSpace(this.SpecialInstructions)) || this.TripType.ID != (int)TripTypes.Hourly);
			}
		}

		[XmlIgnore]
		public bool IsValid {
			get {

				//bool isPickupLocationValid = true;
				//bool isDropLocationValid = true;
				//bool isCreditCardValid = false;
				//bool isGuestEmailValid = true;
				StringBuilder validatonMsg = new StringBuilder ();

				if (TripDateTime == null)
					return false;

				DateTime tempDate;
				if (!DateTime.TryParse (TripDateTime.Value.ToString (), out tempDate)) {

					validatonMsg.Append ("Invalid Trip Date\n");
				}
				if (this.HourlyTripFlag) {

					if (string.IsNullOrWhiteSpace (this.SpecialInstructions)) {

						validatonMsg.Append ("Missing Ride Details\n");
					}

					if (this.EstimatedNumberOfHours <= 0) {

						validatonMsg.Append ("Number of Hours Missing\n");
					}
				}
				if (this.PickupLocation != null) {
					if (this.PickupLocation.IsAirport || this.PickupLocation.IsTrain) {
						if (TripAirline == null) {
							//isPickupLocationValid = false;
							validatonMsg.Append ("Missing Airline\n");
						}
						if (string.IsNullOrWhiteSpace (PickupFlightNumber)) {
							//isPickupLocationValid = false;
							validatonMsg.Append ("Missing Flight Number\n");
						}

					}
				}

				if (this.DropLocation != null) {
					if (this.DropLocation.IsAirport || this.DropLocation.IsTrain) {
						if (this.TripAirline == null) {
							//isDropLocationValid = false;
							validatonMsg.Append ("Missing Airline\n");

						}
						if (string.IsNullOrWhiteSpace (DropOffFlightNumber)) {
							//isDropLocationValid = false;
							validatonMsg.Append ("Missing Flight Number\n");
						}

						if (this.DepartureDateTime == null) {
							//isPickupLocationValid = false;
							validatonMsg.Append ("Missing Departure Time\n");
						} else {

							if (!DateTime.TryParse (DepartureDateTime.Value.ToString (), out tempDate)) {

								validatonMsg.Append ("Invalid Departure Time\n");
							}
						}


					}
				}

				// if the logged in user is not entering an individual cc, they must select a profile cc; 
				// validation for the individual cc items is handled in another method
				if (this.CreditCard.CardToUse == null && !this.IsAddingNewCreditCard && ApplicationContext.Current.IsLoggedIn && this.IsPayingByCreditCard) {
					//isCreditCardValid = false;
					validatonMsg.Append ("Missing Credit Card\n");
				} else
                    //isCreditCardValid = true;

                    if (!ApplicationContext.Current.IsLoggedIn && !this.IsCreateProfile) {
					if (this.GuestEmail.IsEmpty ()) {
						//isGuestEmailValid = false;
						validatonMsg.Append ("Missing E-mail Address\n");
					} else {
						if (!Utilities.IsEmailAddressValid (this.GuestEmail, true)) {
							// isGuestEmailValid = false;
							validatonMsg.Append ("Invalid E-mail Address\n");
						}
					}

				}


				// validate special billing
				foreach (SpecialBillingRequirement sbr in this.SpecialBillingRequirements.Where(s => s.IsRequired == true)) {
					if (sbr.Value.IsEmpty ())
						validatonMsg.Append ("\r\nMissing " + sbr.Prompt);
				}

				this.ValidationErrorMsg = validatonMsg.ToString ();
				return string.IsNullOrEmpty (this.ValidationErrorMsg);

			}
		}

		[XmlIgnore]
		public bool IsNew { get; set; }

		[XmlIgnore ()]
		public bool CanAddNew {
			get {
				return !this.IsBooked;
			}
		}

		[XmlIgnore ()]
		public bool CanCancel {
			get {
				return this.TripStatus < 2 && this.TripDateTime >= System.DateTime.Now;
				;
			}
		}

		[XmlIgnore ()]
		public bool CanUpdate {
			get {

				return this.IsBooked && this.TripStatus < 2 && this.TripDateTime >= System.DateTime.Now;
			}
		}

		[XmlIgnore ()]
		public bool CanEdit { get { return this.CanAddNew || this.CanUpdate; } }

		[XmlIgnore ()]
		public string Summary {
			get {
				string format = "Picking up '{0}' {1} on '{2}' at '{3}' and dropping off at '{4}' in a '{5}'";

				string summary = string.Format (format,
					                 this.NumberOfPassengers,
					                 this.NumberOfPassengers == 1 ? "person" : "people",
					                 this.SingleLineTripDateTime,
					                 this.PickupLocation.SingleLineTitle,
					                 this.DropLocation.SingleLineTitle,
					                 this.PreferredCarName

				                 );

				return summary;
			}
		}

		// TODO: Remove TripType , no longer needed
		//public TripType TripType { get; set; }


		#endregion Properties


		public BookingEntry ()
		{
			IsCreditCardPersonal = true;

			// default to using credit card; we cannot rely on PaidByCreditCard as during the getratedtrip, the value for PaidByCreditCard is set to false by Atlas
			IsPayingByCreditCard = true;
			PaidByCreditCard = true;
			this.NumberOfPassengers = 1;
			this.CreditCard = new BookingEntryCreditCardInfo ();
			if (this.AdditionalStops == null)
				this.AdditionalStops = new List<TripDetailAdditionalStop> ();

			this.SpecialBillingRequirements = new List<SpecialBillingRequirement> ();

		}

		[XmlIgnore ()]
		public bool CreditCardAllowEditBookingCard {
			get {

				return this.CanEdit && this.CreditCard.CardToUse != null;
			}
		}

		public void IndexAdditionalStops ()
		{
			// reset display indexes
			int displayIndex = 1;
			this.AdditionalStops.ForEach (a => a.DisplayIndex = displayIndex++);
		}


		/// <summary>
		/// Merges from API. Sets up data used for trip details that doesn't exist in API.
		/// </summary>
		/// <returns>The from API.</returns>
		/// <param name="api">API.</param>
		/// <param name="mobile">Mobile.</param>
		public void MergeFromApi ()
		{
			this.TripDateTime = DateTime.Parse (String.Format ("{0} {1}:{2} {3}", this.PickupDate, this.PickupHour, this.PickupMinutes, this.PickupAmPm));

			Vehicle preferredVehicle = CachesClient.GetVehicle (this.CarType, this.PreferredCarName);
			if (preferredVehicle != null) {
				Rate selectedRate = new Rate () {
					CarType = this.CarType,
					PreferedVehicleName = this.PreferredCarName,
					VehicleName = preferredVehicle.Name,
					LuggageCapacity = preferredVehicle.LuggageCapacity,
					MaxPassengerDescription = preferredVehicle.PassengerCapacity,
					ImagePath = preferredVehicle.RateQuoteImageNameFullPath

				};
				this.SelectedRate = selectedRate;

			}

			this.IsCarSeatNeeded = this.BoosterSeats + this.ToddlerSeats + this.InfantSeats > 0;
			this.IsPickupSignRequested = !this.PickupSign.IsEmpty ();
			this.IsChauffeurCommentsRequested = !this.AdditionalComments1.IsEmpty ();

			this.IsPayingByCreditCard = this.PaidByCreditCard;
			if (!this.ProfileCreditCardKey.IsEmpty ()) {
				if (this.CreditCard.CardToUse == null)
					this.CreditCard.CardToUse = new ProfileCreditCard ();

				this.CreditCard.CardToUse.CreditCardID = this.ProfileCreditCardKey;
			}


			if (this.HourlyTripFlag) {
				DateTime estimatedDropTime = CalculateDropoffDateTime (
					                             new ApiDateTimeHelper () {
						Date = this.PickupDate,  // Note: Atlas does not store a date for drop off time; use the pickup date for our calculations
						Hour = this.DropOffHour,
						Minutes = this.DropOffMinutes,
						AmPm = this.DropOffAmPm,

					}.Merge ()
				                             );

				DateTime pickupTime = new ApiDateTimeHelper () {
					Date = this.PickupDate,
					Hour = this.PickupHour,
					Minutes = this.PickupMinutes,
					AmPm = this.PickupAmPm,

				}.Merge ();



				//Timespan span = estimatedDropTime.Subtract (pickupTime);
				TimeSpan span = estimatedDropTime - pickupTime;
				this.EstimatedNumberOfHours = span.TotalHours;
			}
			if (this.UseDropOffAirport) {
				this.DropLocation = new TripLocation () {
					Airport = CacheContext.Current.Airports.FirstOrDefault (x => x.AirportCode == this.DropOffAirport),
					LocationItemType = GetAirportLocationType (this.DropOffAirport)
				};

				this.TripAirline = CachesClient.GetAirline (DropOffAirline, this.FBO);
				this.DepartureDateTime = CalculateDropoffDateTime (
					new ApiDateTimeHelper () {
						Date = this.PickupDate,  // Note: Atlas does not store a date for drop off time; use the pickup date for our calculations
						Hour = this.DropOffFlightHour,
						Minutes = this.DropOffFlightMinutes,
						AmPm = this.DropOffFlightAmPm,

					}.Merge ()
				);

			} else {
				this.DropLocation = new TripLocation () {
					Address = new LocationInfo () {

						StreetAddress = this.DropOffAddress,
						City = this.DropOffCity,
						State = this.DropOffState,
						Zip = this.DropOffZipCode
					}
				};
			}

			if (this.UsePickupAirport) {
				this.PickupLocation = new TripLocation () {
					Airport = CacheContext.Current.Airports.FirstOrDefault (x => x.AirportCode == this.PickupAirport),
					LocationItemType = GetAirportLocationType (this.DropOffAirport)
				};
				this.TripAirline = CachesClient.GetAirline (PickupAirline, this.FBO);


			} else {
				this.PickupLocation = new TripLocation () {
					Address = new LocationInfo () {

						StreetAddress = this.PickupAddress,
						City = this.PickupCity,
						State = this.PickupState,
						Zip = this.PickupZipCode
					}
				};
			}

			if (AdditionalStops.Count > 0)
				AdditionalStops.RemoveRange (0, AdditionalStops.Count - 1);  // in case user went back, clear the previous listk

			this.AdditionalStops.Add (new TripDetailAdditionalStop () {
				DisplayIndex = 1,
				PassengerName = this.AdditionalStopPassengerName1,
				Address = new LocationInfo () {
					StreetAddress = this.AdditionalStopAddress1,
					City = this.AdditionalStopCity1,
					State = this.AdditionalStopState1,
					Zip = this.AdditionalStopZip1
				}

			});

			this.AdditionalStops.Add (new TripDetailAdditionalStop () {
				DisplayIndex = 2,
				PassengerName = this.AdditionalStopPassengerName2,
				Address = new LocationInfo () {
					StreetAddress = this.AdditionalStopAddress2,
					City = this.AdditionalStopCity2,
					State = this.AdditionalStopState2,
					Zip = this.AdditionalStopZip2
				}

			});

			this.AdditionalStops.Add (new TripDetailAdditionalStop () {
				DisplayIndex = 3,
				PassengerName = this.AdditionalStopPassengerName3,
				Address = new LocationInfo () {
					StreetAddress = this.AdditionalStopAddress3,
					City = this.AdditionalStopCity3,
					State = this.AdditionalStopState3,
					Zip = this.AdditionalStopZip3
				}

			});

			foreach (TripDetailAdditionalStop tdap in this.AdditionalStops) {
				if (!tdap.PassengerName.IsEmpty ()) {
					this.HasAdditionalPassengers = true;
					break;
				}
			}
			#region "Special Billing Requirements"
			foreach (SpecialBillingRequirement sbr in UserContext.Current.SpecialBillingRequirements) {

				string tripPropertyName = "specialbillingvalue" + sbr.DisplayIndex.ToString ();

				// use reflection to add data to actual property
				Type corpReqType = this.GetType ();
				var properties = corpReqType.GetProperties (); //.Where(p => p.DeclaringType == typeof(BookingEntry));
				PropertyInfo propertyInfo = properties.Where (pr => pr.Name.ToLower ().Equals (tripPropertyName)).FirstOrDefault ();
				if (propertyInfo != null && propertyInfo.CanRead) {
					this.SpecialBillingRequirements.Add (new SpecialBillingRequirement () {
						DisplayIndex = sbr.DisplayIndex,
						Prompt = sbr.Prompt,
						MinSize = sbr.MinSize,
						MaxSize = sbr.MaxSize,
						Value = (string)propertyInfo.GetValue (this, null)
					}
					);

				}

			}
			#endregion "Special Billing Requirements"

		}

		public DateTime CalculateDropoffDateTime (DateTime dropDateTime)
		{

			DateTime pickupTime = new ApiDateTimeHelper () {
				Date = this.PickupDate,
				Hour = this.PickupHour,
				Minutes = this.PickupMinutes,
				AmPm = this.PickupAmPm,

			}.Merge ();

			// if dropoff time is early then pickuptime, then we have spanned to the next day.  Add 1 day to dropoff
			if (dropDateTime < pickupTime)
				dropDateTime = dropDateTime.AddDays (1);

			return dropDateTime;
		}

		public static LocationFinderItemType GetAirportLocationType (string airportCode)
		{
			if (CachesClient.IsAiportTrain (airportCode))
				return LocationFinderItemType.Train;
			else
				return LocationFinderItemType.Airport;

		}

		/// <summary>
		/// Merges the TripDetail returned from the API with the BookingEntry object used for the current booking.
		/// </summary>
		/// <returns>The atlas to mobile.</returns>
		public static BookingEntry MergeWithApi (BookingEntry mobile, TripDetail api)
		{
			//start with the api version since it has more properties
			Type apiType = api.GetType ();

			var properties = apiType.GetProperties ().Where (p => p.DeclaringType == typeof(TripDetail));
			foreach (PropertyInfo propertyInfo in properties) {

				if (propertyInfo.CanRead) {
					object apiValue = propertyInfo.GetValue (api, null);
					object bookingEntryValue = propertyInfo.GetValue (mobile, null);
					if (!object.Equals (apiValue, bookingEntryValue) && !apiValue.IsEmpty ()) {

						propertyInfo.SetValue (mobile, apiValue, null);
					}
				}

			}

			return mobile;

		}

		/// <summary>
		/// Pre-fill required trip data.  This is to trick out the backend api/atlas.  The dummy data being insesrted here should be replaced 
		/// with actual data prompted to the user prior to submitting the trip for reservation.  This is used to get the rates without the
		/// necessity of the user  
		/// to fill in all required data.
		/// </summary>
		public void PreLoadTrip ()
		{
			if (!ApplicationContext.Current.IsLoggedIn) {
				this.PassengerLastName = PassengerLastName.SetDummyData ();
				this.PassengerFirstName = PassengerFirstName.SetDummyData ();
				this.BillingName = BillingName.SetDummyData ();
				this.BookedBy = BookedBy.SetDummyData ();
				this.ContactPhoneNumber = ContactPhoneNumber.SetDummyData ();


			} else {
				PreLoadTrip (UserContext.Current.Person);
			}
			//	this.DropOffFlightNumber = DropOffFlightNumber.SetDummyData ();
			//	this.PickupFlightNumber = PickupFlightNumber.SetDummyData ();
			//	if (this.TripAirline == null || this.TripAirline.Code.IsEmptyOrDummy())
			//			this.TripAirline = CacheContext.Current.Airlines.First ();
			//	if ((this.DropLocation.IsAirport || this.DropLocation.IsTrain) && (this.DepartureDateTime.IsEmptyOrDummy() || this.DepartureDateTime < this.TripDateTime))
			//	this.DepartureDateTime = this.TripDateTime.Value.AddHours (2);
			//	else
			//	this.DepartureDateTime = DepartureDateTime.SetDummyData ();


		}


		public void PreLoadTrip (CustomerProfile person)
		{
			if (person == null)
				return;

			if (!person.Name.IsEmpty ()) {
				// format of name is last, first
				string parseName = person.Name.Replace (" ", "").Trim (); //remove all spaces, including between names
				int separatorLocation = parseName.IndexOf (",");

				if (this.PassengerLastName.IsEmpty ())
					this.PassengerLastName = parseName.Substring (0, separatorLocation);

				if (this.PassengerFirstName.IsEmpty ())
					this.PassengerFirstName = parseName.Substring (separatorLocation + 1);

				if (this.BillingName.IsEmpty ())
					this.BillingName = this.PassengerFirstName + " " + this.PassengerLastName;

				if (this.BookedBy.IsEmpty ())
					this.BookedBy = person.Name;


			}
			if (this.CustomerNumber.IsEmpty ())
				this.CustomerNumber = person.CustomerNumber;

			if (this.ContactPhoneNumber.IsEmpty ()) {
				//if (!person.WorkPhone.IsEmpty ())
				//		this.ContactPhoneNumber = person.WorkPhone;
				//	else 
				if (!person.CustomerNumber.IsEmpty ())
					this.ContactPhoneNumber = person.CustomerNumber;
			}
			if (!person.AdminEmail.IsEmpty () && this.GuestEmail.IsEmpty ())
				this.GuestEmail = person.AdminEmail;

			if (!person.Address.IsEmpty () && this.BillingAddress.IsEmpty ())
				this.BillingAddress = person.Address;

			if (!person.City.IsEmpty () && this.BillingTown.IsEmpty ())
				this.BillingTown = person.City;

			if (!person.State.IsEmpty () && this.BillingState.IsEmpty ())
				this.BillingState = person.State;

			if (!person.ZipCode.IsEmpty () && this.BillingZip.IsEmpty ())
				this.BillingZip = person.ZipCode;

			// For Notifications:
			// if the mobile app allows for it, and the profile allows for it, set the "enabled" setting to the value stored in the customer profile.  this is our default setting
			var defaultSettingsCache = ApplicationContext.Current.DefaultSettingsCache;
			if (defaultSettingsCache.Value.IsDriverInfoNotificationsEligible && person.IsDriverInfoNotificationsEligible)
				this.IsDriverInfoNotificationsEnabled = person.IsDriverInfoNotificationsEnabled;
			else
				this.IsDriverInfoNotificationsEnabled = false;

			if (defaultSettingsCache.Value.IsDriverOnLocationNotificationsEligible && person.IsDriverOnLocationNotificationsEligible)
				this.IsDriverOnLocationNotificationsEnabled = person.IsDriverOnLocationNotificationsEnabled;
			else
				this.IsDriverOnLocationNotificationsEnabled = false;

		}

		public BookingEntry ConvertToAtlas ()
		{
			ApiDateTimeHelper dtHelper;
			UseDropOffAirport = this.DropLocation.IsAirport || this.DropLocation.IsTrain;
			UsePickupAirport = this.PickupLocation.IsAirport || this.PickupLocation.IsTrain;

			// Customer Number will be inferred from authenticated user
			//getInfo.CustomerNumber = "";

			dtHelper = new ApiDateTimeHelper (this.TripDateTime.GetValueOrDefault ());

			PickupDate = dtHelper.Date; // i.e. "08/31/2013" (use leading zeros)
			PickupHour = dtHelper.Hour; // i.e. "05" (use leading zeros)
			PickupMinutes = dtHelper.Minutes; // i.e. "00" (use leading zeros)
			PickupAmPm = dtHelper.AmPm; // i.e. "PM"
			RecomendPickupTime = false;

			//getInfo.HasAdditionalPassengers = false;

			//required if Outbound
			if (DropLocation.Airport != null) {
				dtHelper = new ApiDateTimeHelper (this.DepartureDateTime.GetValueOrDefault ());
				DropOffFlightHour = dtHelper.Hour;
				DropOffFlightMinutes = dtHelper.Minutes;
				DropOffFlightAmPm = dtHelper.AmPm;
				DropOffFlightDate = dtHelper.Date;
				DropOffAirline = TripAirline.Code;
				DropOffAirport = DropLocation.Airport.AirportCode;
                
			}

			// required if Inbound
			if (PickupLocation.Airport != null) {

				PickupAirport = PickupLocation.Airport.AirportCode;
				if (TripAirline != null) {
					PickupAirline = TripAirline.Code;
				} else {
					PickupAirline = "";
				}

			} else {
				PickupAirline = "";
				PickupFlightOriginCity = "";
			}

			// 2/18/14 JMO, updated to store pickupLocationDescription from name
			{
				//Required if Pickup is Address
				if (PickupLocation.Address != null) {
					PickupAddress = PickupLocation.Address.StreetAddress + " " + PickupLocation.Address.StreetAddress2; //  "USE O FOR OFFICE P/U'S";
					PickupCity = PickupLocation.Address.City;
					PickupState = PickupLocation.Address.State;
					PickupZipCode = PickupLocation.Address.Zip;
					PickupLocationDescription = PickupLocation.Address.Name;

					// Specify true below if Address specified should be saved to Profile 
					//getInfo.SavePickupLocationToAddressBook = false;
				}

				if (DropLocation.Address != null) {
					//Required if Drop is Address
					DropOffAddress = DropLocation.Address.StreetAddress + " " + DropLocation.Address.StreetAddress2;
					DropOffCity = DropLocation.Address.City;
					DropOffState = DropLocation.Address.State;
					DropOffZipCode = DropLocation.Address.Zip;
					DropoffLocationDescription = DropLocation.Address.Name;

				}
			}

			#region Additional Stop
			TripDetailAdditionalStop additionalStop;
			// Clear all properties needed by API (Atlas) as insurance in case user removed additional stops
			//	AdditionalStopFlag = false;
			AdditionalStopPassengerName1 = "";
			AdditionalStopAddress1 = "";
			AdditionalStopCity1 = "";
			AdditionalStopState1 = "";
			AdditionalStopZip1 = "";

			AdditionalStopPassengerName2 = "";
			AdditionalStopAddress2 = "";
			AdditionalStopCity2 = "";
			AdditionalStopState2 = "";
			AdditionalStopZip2 = "";

			AdditionalStopPassengerName3 = "";
			AdditionalStopAddress3 = "";
			AdditionalStopCity3 = "";
			AdditionalStopState3 = "";
			AdditionalStopZip3 = "";

			if (AdditionalStopFlag) {
				additionalStop = AdditionalStops [0];

				if (additionalStop != null) {


					AdditionalStopPassengerName1 = additionalStop.PassengerName;
					AdditionalStopAddress1 = additionalStop.Address.StreetAddress + " " + additionalStop.Address.StreetAddress2;
					AdditionalStopCity1 = additionalStop.Address.City;
					AdditionalStopState1 = additionalStop.Address.State;
					AdditionalStopZip1 = additionalStop.Address.Zip;

					if (AdditionalStops.Count > 1) {

						additionalStop = AdditionalStops [1];

						if (additionalStop != null) {

							AdditionalStopPassengerName2 = additionalStop.PassengerName;
							AdditionalStopAddress2 = additionalStop.Address.StreetAddress + " " + additionalStop.Address.StreetAddress2;
							AdditionalStopCity2 = additionalStop.Address.City;
							AdditionalStopState2 = additionalStop.Address.State;
							AdditionalStopZip2 = additionalStop.Address.Zip;

							if (AdditionalStops.Count > 2) {
								additionalStop = AdditionalStops [2];

								if (additionalStop != null) {
									AdditionalStopPassengerName3 = additionalStop.PassengerName;
									AdditionalStopAddress3 = additionalStop.Address.StreetAddress + " " + additionalStop.Address.StreetAddress2;
									AdditionalStopCity3 = additionalStop.Address.City;
									AdditionalStopState3 = additionalStop.Address.State;
									AdditionalStopZip3 = additionalStop.Address.Zip;

								}
							}

						}
					}

				}
			}

			#endregion Additional Stop

			#region "Special Billing Requirements"
			if (SpecialBillingRequirements != null) {
				foreach (SpecialBillingRequirement sbr in SpecialBillingRequirements) {
					if (!sbr.Value.IsEmpty ()) {
						string tripPropertyName = "specialbillingvalue" + sbr.DisplayIndex.ToString ();

						// use reflection to add data to actual property
						Type corpReqType = this.GetType ();
						var properties = corpReqType.GetProperties (); //.Where(p => p.DeclaringType == typeof(BookingEntry));
						PropertyInfo propertyInfo = properties.Where (pr => pr.Name.ToLower ().Equals (tripPropertyName)).FirstOrDefault ();
						if (propertyInfo != null && propertyInfo.CanWrite) {
							propertyInfo.SetValue (this, sbr.Value);
						}
					}
				}
			}
			#endregion "Special Billing Requirements"
			// No Trip Number for New Trips
			if (this.IsNew)
				TripNumber = "";
			// CorpCode Only getInfouired for Corporate Customers
			//getInfo.CorporationNumber = "";

			if (this.HourlyTripFlag && EstimatedNumberOfHours != null) {

				DateTime dropOffTime = TripDateTime.Value.AddHours (EstimatedNumberOfHours.GetValueOrDefault ());
				dtHelper = new ApiDateTimeHelper (dropOffTime);
				DropOffHour = dtHelper.Hour;
				DropOffMinutes = dtHelper.Minutes;
				DropOffAmPm = dtHelper.AmPm;
			}
			//getInfo.SpecialInstructions = "";


			//Based on Customer Profile settings
			//getInfo.AutoReceiptFlag = false;
			//getInfo.AutoReceiptAddToProfile = false;

			//Payment specified on Billing Step
			this.PaidByCreditCard = this.IsPayingByCreditCard;
			this.PaidByProfileCreditCard = false;
			if (this.PaidByCreditCard) {
				if (this.CreditCard.CardToUse != null) {
					this.PaidByProfileCreditCard = true;
					this.ProfileCreditCardKey = this.CreditCard.CardToUse.CreditCardID;
				}
				this.PaidByDirectBill = false;
			} else {
				this.PaidByDirectBill = true;
			}
			this.PaidByAdminDirectBill = false;

			//getInfo.PromotionID = 0;

			// After the credit card number is entered, we will be determining the type of card automatically.
			var ccType = CreditCardUtilities.GetCreditCardCodeByNumber (CreditCardNumber);
			if (!String.IsNullOrWhiteSpace (ccType))
				CreditCardType = ccType;


			return this;
		}


		public void ClearCreditCardDetails ()
		{
			this.CreditCard = new BookingEntryCreditCardInfo ();
			this.CreditCardNumber = "";
			/*this.BillingAddress = "";
            this.BillingName = "";
            this.BillingState = "";
            this.BillingZip = "";
            this.BillingCountry = "";
            this.BillingTown = "";*/
			this.PaidByCreditCard = false;
			this.CreditCardSecurityCode = "";


		}

		/// <summary>
		/// Splits the date time.  Needed for Atlas mapping.
		/// </summary>
		//public  class SplitDateTimeForApi
		public class ApiDateTimeHelper
		{
			public DateTime DateToSplit { get; set; }

			public string Hour { get; set; }

			public string Minutes { get; set; }

			public string AmPm { get; set; }

			public string Date { get; set; }

			public ApiDateTimeHelper ()
			{
			}

			public ApiDateTimeHelper (DateTime dateToSplit)
			{

				DateToSplit = dateToSplit;
				Split ();

			}

			public void Split ()
			{
				// make sure 12 hour time
				this.Hour = DateToSplit.ToString ("hh", CultureInfo.InvariantCulture);
				this.Minutes = DateToSplit.ToString ("mm", CultureInfo.InvariantCulture);
				this.AmPm = DateToSplit.ToString ("tt", CultureInfo.InvariantCulture);
             
				this.Date = DateToSplit.Date.ToString ("MM/dd/yyyy");
				//string datetimeTemp = DateToSplit.ToString ("mm/dd/yyyy hh:mm tt");
				//this.Hour = (DateTime)datetimeTemp

			}

			public DateTime Merge ()
			{
				DateTime result;

				DateTime.TryParse (
					String.Format ("{0} {1}:{2} {3}",
						this.Date,
						this.Hour,
						this.Minutes,
						this.AmPm),
					out result);
				return result;
			}


		}

		public class TripPriceItem
		{

			public string PriceDescription { get; set; }

			public string Amount { get; set; }

			public TripPriceItem ()
			{
			}

			public TripPriceItem (string priceDescription, string amount)
			{
				this.Amount = amount;
				this.PriceDescription = priceDescription;
			}

		}

		/// <summary>
		/// Gets the trip detail. Excludes properties of BookingEntry; This is used to streamline the passing of data to the API
		/// </summary>
		/// <returns>The trip detail.</returns>
		//[XmlInclude(typeof(BookingEntry))]
		public TripDetail GetTripDetail ()
		{
			TripDetail result = new TripDetail ();

			Type bookingType = base.GetType ();

			var properties = bookingType.GetProperties ().Where (p => p.DeclaringType == typeof(TripDetail));
			foreach (PropertyInfo propertyInfo in properties) {
				if (propertyInfo.CanRead) {
					object bookingValue = propertyInfo.GetValue (this, null);
					object tripDetailValue = propertyInfo.GetValue (result, null);
					if (!object.Equals (bookingValue, tripDetailValue)) {
						propertyInfo.SetValue (result, bookingValue, null);
					}
				}
			}
			return result;
		}


	}

	public partial class BookingEntryCreditCardInfo
	{
		public BookingEntryCreditCardInfo ()
		{
		}

		public ProfileCreditCard CardToUse { get; set; }

		//public BookingEntryCreditCardItem BookingCard { get; set; }

	}
	/*public partial class BookingEntryCreditCardItem 
    {
        public BookingEntryCreditCardItem()
        {
            this.IsNew = true;
		
        }

        [XmlAttribute]
        public bool IsNew { get; set; }

        //[XmlElement(IsNullable=true)]
        //public DateTime? ExpirationDate { get; set; }



        //[XmlAttribute]
        //public string CardNumber { get; set; }
        //[XmlAttribute]
        //public string CardNumberObfuscated { get; set; }
    //	[XmlAttribute]
        //public string CCV { get; set; }

        //public CacheItemCreditCardType Type { get; set; }

    //	[XmlAttribute]
        //public bool AddCardToProfile { get; set; }
    }
*/
	public class TripLocation
	{
		public Airport Airport { get; set; }

		public LocationInfo Address { get; set; }

		public LocationFinderItemType ItemType { get; set; }

		public bool IsAirport {
			get { return this.Airport != null && this.Airport.IsTrainStation == false; }
		}

		public bool IsTrain {

			get { return this.Airport != null && this.Airport.IsTrainStation == true; }

		}

		public bool IsAddress {
			get {
				return this.Address != null && !this.IsAirport && !this.IsTrain;
			}
		}

		public override string ToString ()
		{
			return this.Airport != null
                ? this.Airport.ToString ()
                    : this.Address != null
                    ? this.Address.ToString ()
                    : "missing";
		}


		public string MultiLineTitle {
			get {
				if (this.Address != null)
					return this.Address.MultiLineTitle;
				else if (this.Airport != null)
					return this.Airport.MultiLineTitle;
				else
					return "";

			}
		}


		public string SingleLineTitle {
			get {
				if (this.Airport != null)
					return this.Airport.AirportCode;
				else if (this.Address != null)
					return (string.IsNullOrWhiteSpace (this.Address.Name) ? "" : this.Address.Name + ", ")
					+ this.Address.StreetAddress
					+ ", " + this.Address.City + ", " + this.Address.State + " " + this.Address.Zip;
				else
					return "";
			}
		}

		/// <summary>
		/// Property to use for comparing addresses, concatenate only items needed to see if 2 addresses are the same
		/// </summary>
		public string TextForCompare {
			get {
				if (this.Airport != null)
					return this.Airport.AirportCode;
				else if (this.Address != null)
					return this.Address.StreetAddress + this.Address.Zip;
				else
					return "";

			}
		}

		public LocationFinderItemType LocationItemType { get; set; }
	}

	public partial class BookingGetAnonymousInfo
	{
		[XmlAttribute]
		public string ConfirmationNumber { get; set; }

		[XmlAttribute]
		public string EmailAddress { get; set; }

		public static BookingGetAnonymousInfo Empty { get { return new BookingGetAnonymousInfo (); } }
	}




}

