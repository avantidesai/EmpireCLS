using System;
using System.Linq;
using System.Collections.Generic;

using System.Net;

namespace EmpireCLS
{
	/// <summary>
	/// Address lookup client.
	/// </summary>
	public class AddressLookupAggregator : IWebClient
	{
		private readonly List<IAddressLookupClient> _addressLookups = new List<IAddressLookupClient> ();
		
		private readonly List<IAddressLookupClient> _addressLookupsCompleted = new List<IAddressLookupClient> ();
		
		private readonly List<LocationInfo> _foundAddresses = new List<LocationInfo> ();

		
		private bool _canLookup = true;

		public AddressLookupAggregator ()
		{
		}

		public IEnumerable<IAddressLookupClient> Lookups { get { return _addressLookups; } }

		#region IWebclient

		public Action<IWebClient> ClientCompleted { get; set; }

		public bool HasErrors {
			get {
				lock (this) {
					return _addressLookupsCompleted.Exists (al => al.HasErrors);
				}
			}
		}

		public string ErrorMessage {
			get {
				lock (this) {
					string errorMessage = (from al in _addressLookupsCompleted
					                       where al.HasErrors
					                       select string.Format ("[{0}] - {1}", al.ProviderType, al.ErrorMessage)).FirstOrDefault ();
					return errorMessage;
				}
			}
		}

		public Exception LastError { get { return string.IsNullOrWhiteSpace (this.ErrorMessage) ? null : new ApplicationException (this.ErrorMessage); } }

		#endregion // IWebClient

		private void InitLookups ()
		{
			_addressLookups.Clear ();

			//_addressLookups.Add (new AddressLookupBing (AddressLookupClientCompleted));
			//_addressLookups.Add (new AddressLookupGoogle (AddressLookupClientCompleted));
			//_addressLookups.Add (new AddressLookupGooglePlace (AddressLookupClientCompleted));
		}

		/// <summary>
		/// Gets or sets the found addresses.
		/// </summary>
		/// <value>
		/// The found addresses.
		/// </value>
		public IEnumerable<LocationInfo> FoundAddresses {
			get {
				lock (this) {
					return new List<LocationInfo> (_foundAddresses);
				}
			}
			set {
				lock (this) {
					_foundAddresses.Clear ();
					_foundAddresses.AddRange (value);
				}
			}
		}

		public string SearchString { get; set; }

		public Tuple<double, double> LookedUpLatLng { get; set; }

		public void Cancel ()
		{
			_addressLookups.ForEach (a => a.Cancel ());
			_canLookup = true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchString"></param>
		/// <returns></returns>
		/// <remarks>
		/// 2/13/14 JMO
		///     - changed to wait for 2 second, was 5, and check complete every 50 ms, was 100 ms.
		/// </remarks>
		public AddressLookupAggregator Lookup (string searchString)
		{
			InitLookups ();

			_addressLookupsCompleted.Clear ();
			this.SearchString = SearchString;

			_addressLookups.ForEach (a => {
				a.LookupAsync (searchString);
			});

			DateTime waitTime = DateTime.Now.AddSeconds (2);
			while (_addressLookups.Exists (a => a.IsProcessing)) { 
				System.Threading.Thread.Sleep (50); 

				if (DateTime.Now > waitTime)
					break;
			}

			// cancel whatever didn't complete
			_addressLookups.ForEach (a => {
				if (a.IsProcessing) {
					System.Diagnostics.Debug.WriteLine (string.Format ("Address lookup cancelled - {0}", a.GetType ().Name));
					a.Cancel ();
				}
			});

			return this;
		}

		public void LookupAsync (string searchString)
		{
			if (!_canLookup)
				return;
			_canLookup = false;
			
			_addressLookupsCompleted.Clear ();
			this.SearchString = searchString;
			
			_addressLookups.ForEach (a => a.LookupAsync (searchString));
		}

		public void LookupAsync (double lat, double lng)
		{
			if (!_canLookup)
				return;
			_canLookup = false;

			_addressLookupsCompleted.Clear ();
			this.LookedUpLatLng = new Tuple<double, double> (lat, lng);
			
			_addressLookups.ForEach (a => a.LookupAsync (lat, lng));
		}


		private void AddressLookupClientCompleted (IAddressLookupClient client)
		{						
			lock (this) {
				_addressLookupsCompleted.Add (client);
				if (_addressLookupsCompleted.Count != _addressLookups.Count)
					return;

				List<LocationInfo> addresses = new List<LocationInfo> ();
				_addressLookups.ForEach (al => addresses.AddRange (al.Addresses));
				this.FoundAddresses = addresses;				
				_canLookup = true;
			}
			
			// needs to be outside of the lock statement otherwise the actualy handler may just lock up
			//   if invoking on the main thread.  A possible mono bug
			if (this.ClientCompleted != null)
				this.ClientCompleted (this);				
		}
	}
	

	

	
	
}

