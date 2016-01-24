using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;


namespace EmpireCLS
{
	public interface IAddressLookupClient
	{
		bool HasErrors { get; }

		string ErrorMessage { get; }

		LocationLookupProviderType ProviderType { get; }

		IEnumerable<LocationInfo> Addresses { get; }

		void Cancel ();

		bool WasCancelled { get; }

		bool IsProcessing { get; }

		IAddressLookupClient LookupAsync (string addressToLookup);

		IAddressLookupClient LookupAsync (double lat, double lng);

		IAddressLookupClient Lookup (string addressToLookup);
	}

	/// <summary>
	/// Address lookup base.
	/// </summary>
	public abstract class AddressLookupClient : JsonWebClientBase, IAddressLookupClient
	{
		private readonly List<LocationInfo> _addresses = new List<LocationInfo> ();

		private readonly LocationLookupProviderType _providerType;

		private bool _wasCancelled = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="EmpireCLS.Mobile.AddressLookupUtil"/> class.
		/// </summary>
		/// <param name='addressToLookup'>
		/// Address to lookup.
		/// </param>
		public AddressLookupClient (string hostName, LocationLookupProviderType providerType, Action<IAddressLookupClient> lookupCompletedHandler)
			: base (hostName)
		{
			_providerType = providerType;

			this.ClientCompleted = (c) => {
				lookupCompletedHandler (this);
			};
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EmpireCLS.Mobile.AddressLookupUtil"/> class.
		/// </summary>
		/// <param name='addressToLookup'>
		/// Address to lookup.
		/// </param>
		public AddressLookupClient (string hostName, LocationLookupProviderType providerType)
			: base (hostName)
		{
			_providerType = providerType;
		}

		public LocationLookupProviderType ProviderType { get { return _providerType; } }

		public IEnumerable<LocationInfo> Addresses { get { return _addresses; } }

		protected abstract LocationInfo[] ParseLookupResults ();

		protected abstract string OnGetUrlPath (string addressToLookup);

		protected abstract string OnGetUrlPath (double lat, double lng);

		#region IAddressLookupClient

		bool IAddressLookupClient.HasErrors { get { return base.HasErrors; } }

		string IAddressLookupClient.ErrorMessage { get { return base.ErrorMessage; } }

		LocationLookupProviderType IAddressLookupClient.ProviderType { get { return this.ProviderType; } }

		void IAddressLookupClient.Cancel ()
		{
			_wasCancelled = true;
			this.CancelAsync ();
		}

		bool IAddressLookupClient.WasCancelled { get { return _wasCancelled; } }

		bool IAddressLookupClient.IsProcessing { get { return base.AsyncProcessing; } }

		IEnumerable<LocationInfo> IAddressLookupClient.Addresses { get { return this.Addresses; } }

		IAddressLookupClient IAddressLookupClient.Lookup (string addressToLookup)
		{
			return InvokeStrategy<AddressLookupClient> (() => {

				_wasCancelled = false;
				_addresses.Clear ();

				if (Get (OnGetUrlPath (addressToLookup)).HasErrors)
					return;

				_addresses.AddRange (ParseLookupResults ());
			});
		}

		IAddressLookupClient IAddressLookupClient.LookupAsync (string addressToLookup)
		{
			return InvokeStrategy<AddressLookupClient> (() => {

				_wasCancelled = false;
				_addresses.Clear ();
				
				if (GetAsync (OnGetUrlPath (addressToLookup), () => _addresses.AddRange (ParseLookupResults ())).HasErrors)
					return;
			});
		}

		IAddressLookupClient IAddressLookupClient.LookupAsync (double lat, double lng)
		{
			return InvokeStrategy<AddressLookupClient> (() => {

				_wasCancelled = false;
				_addresses.Clear ();

				if (GetAsync (OnGetUrlPath (lat, lng), () => _addresses.AddRange (ParseLookupResults ())).HasErrors)
					return;
			});						
		}

		#endregion // IAddressLookupClient
	}
}

