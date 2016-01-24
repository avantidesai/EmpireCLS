using System;

using System.Linq;
using System.Collections.Generic;
using System.Net;

namespace EmpireCLS.Mobile.ApiClient
{
	public class RatesClient : ApiClientBase
	{
		private static class UrlPaths
		{
			public const string Rates = "/api/Rates/GetRates";

		}

		public RatesClient ()
			: base (Settings.Current.EmpireCLSHost)
		{

			AddUrlPathModelTypeMapping (UrlPaths.Rates, typeof(Rates));
		}


		public Rates GetResult { get { return (this.Model as Rates); } }

		public RatesClient Get (TripDetail getInfo)
		{
			return InvokeStrategy<RatesClient> (() => {
				
				PostObject (
					UrlPaths.Rates, getInfo
				);
			
			});
			
		}

		public RatesClient GetAsync (BookingEntry getInfo)
		{

			return InvokeStrategy<RatesClient> (() => {				
				if (PostObjectAsync (UrlPaths.Rates, getInfo.GetTripDetail ()).HasErrors)
					return;
			});
		}
	
	}
}

