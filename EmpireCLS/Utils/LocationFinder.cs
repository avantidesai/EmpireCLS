using System;

using System.Linq;
using System.Collections.Generic;

namespace EmpireCLS.Mobile
{
	/// <summary>
	/// Location finder item type.
	/// </summary>
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


	/// <summary>
	/// Location finder item.
	/// </summary>
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

	/// <summary>
	/// Location finder.
	/// </summary>
	public class LocationFinder
	{
		private System.Threading.Timer _searchTimer = null;

		//private readonly AddressLookupAggregator _addressLookupClient = new AddressLookupAggregator ();

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
		/*private void SearchTimerTicked (object state)
		{
			Stop ();

			int searchTimerRestartMs = 180;
			Func<string> lookupStringFunc = () => this.SearchBarTextLocked.ToUpper ().Trim ();

			//string lookupString = this.SearchString.ToUpper();
			string lookupString = lookupStringFunc ();
			if (lookupString != _lastSearchString) {			
				_searchResultItems.Clear ();

				if (lookupString.Length >= 3) {
					if (ApplicationContext.Current.IsLoggedIn && ApplicationContext.Current.CurrentUser != null && ApplicationContext.Current.CurrentUser.Person != null) {
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
						CacheContext.Current.LoadContacts ();
						_searchResultItems.AddRange (
							from c in CacheContext.Current.Contacts
							where (c.Item1.FirstName + c.Item1.LastName).ToUpper ().Contains (
								    lookupString.Replace (" ", "").ToUpper ()
							    )
							select new LocationFinderItem (c.Item1, c.Item2)
						);
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
		} */

		private void SearchTimerTicked (object state)
		{
		}
	}
}


