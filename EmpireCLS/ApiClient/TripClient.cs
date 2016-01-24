using System;

using System.Linq;
using System.Collections.Generic;


using System.Net;

namespace EmpireCLS
{
	public class TripClient : ApiClientBase
	{
		private static class UrlPaths
		{
			public const string Create = "/api/Trip/New";
			public const string GetTrip = "/api/Trip/GetTrip?TripNumber={0}";
			public const string GetTripAsGuest = "/api/Trip/GetTripAsGuest?ConfirmationNumber={0}&EmailAddress={1}";
			public const string GetTripListing = "/api/Trip/GetTripListing";
			public const string GetRated = "/api/Trip/GetRatedTrip";
			public const string Cancel = "/api/Trip/Cancel";
			public const string CancelAsGuest = "/api/Trip/CancelAsGuest";
			public const string GetTripReceipt = "/api/Trip/GetTripReceiptHTML?TripNumber={0}";
			public const string Update = "/api/Trip/Update";
			public const string GetRecommendedPickupTime = "/api/Trip/GetSuggestedPickUpTime";
		}

		public TripDetail RatedTripDetail { get; private set; }

		public RecommendedPickupTime RecommenedPickupDateTime { get; private set; }

		public TripReceipt UserTripReceipt { get; set; }

		public TripClient ()
			: base (Settings.Current.EmpireCLSHost)
		{
			AddUrlPathModelTypeMapping (UrlPaths.Create, typeof(TripUpdateConfirmation)); 
			AddUrlPathModelTypeMapping (UrlPaths.GetRated, typeof(TripDetail)); 
			AddUrlPathModelTypeMapping (UrlPaths.Cancel, typeof(TripCancelConfirmation)); 
			AddUrlPathModelTypeMapping (UrlPaths.CancelAsGuest, typeof(TripCancelConfirmation)); 
			AddUrlPathModelTypeMapping (UrlPaths.GetTripListing, typeof(TripList)); 
			AddUrlPathModelTypeMapping (UrlPaths.Update, typeof(TripUpdateConfirmation));
			AddUrlPathModelTypeMapping (UrlPaths.GetRecommendedPickupTime, typeof(RecommendedPickupTime)); 
		}

		public TripList TripListResult { get { return (this.Model as TripList); } }


		public TripClient GetTripListingAsync (TripListCriteria listCriteria, bool returnTripsWithReceiptsOnly = false)
		{
			listCriteria = new TripListCriteria ();
			listCriteria.PageSize = 250;
			listCriteria.ReturnCountOnly = false;
			listCriteria.ReturnTripsWithReceiptsOnly = returnTripsWithReceiptsOnly;
			listCriteria.StartDate = "*";
			listCriteria.StartTime = "*";
			listCriteria.EndDate = "*";
			listCriteria.EndTime = "*";
			listCriteria.CurrentPage = 0;
			listCriteria.TripStatusCodes = string.Format ("{0}, {1}, {2}, {3}", ((int)BookingEntryStatus.Open), ((int)BookingEntryStatus.Cancelled), ((int)BookingEntryStatus.Completed), ((int)BookingEntryStatus.NoShowed));
                        
			return InvokeStrategy<TripClient> (() => {
			
				if (base.PostObjectAsync (UrlPaths.GetTripListing, listCriteria, GetTripListingCompleted).HasErrors)
					return;
			});			

		}

		private void GetTripListingCompleted ()
		{
			if (this.HasErrors)
				return;		
			if (this.ClientCompleted != null)
				this.ClientCompleted (this);

			//this.TripListResult = this.Model as TripDetail;
		}

		public TripClient GetTripReceipt (string tripNumber)
		{
			string urlPath = string.Format (UrlPaths.GetTripReceipt, tripNumber);
			AddUrlPathModelTypeMapping (urlPath, typeof(TripReceipt));


			return InvokeStrategy<TripClient> (() => {
				GetAsync (urlPath, () => GetTripReceiptCompleted ());

			});
		}

		private void GetTripReceiptCompleted ()
		{
			if (this.HasErrors)
				return;		

			this.UserTripReceipt = this.Model as TripReceipt;
		}

		public TripClient GetTripAsync (string tripNumber)
		{
			if (tripNumber.Trim () == "")
				return null;

			string urlPath = string.Format (UrlPaths.GetTrip, tripNumber);
			AddUrlPathModelTypeMapping (urlPath, typeof(BookingEntry));

			return InvokeStrategy<TripClient> (() => {
					
				GetAsync (urlPath, () => GetTripCompleted ());
			});		
			//return InvokeStrategy<TripClient>(()=> {
			//if (base.PostObjectAsync(urlPath, BookingEntry.GetTripDetail(), GetTripCompleted).HasErrors)
			//	return;
			//});			
		}

		public TripClient GetTripAsGuestAsync (string confirmationNumber, string emailAddress)
		{
			if (confirmationNumber.Trim () == "" || emailAddress.Trim () == "")
				return null;

			string urlPath = string.Format (UrlPaths.GetTripAsGuest, confirmationNumber, emailAddress);
			AddUrlPathModelTypeMapping (urlPath, typeof(BookingEntry));

			return InvokeStrategy<TripClient> (() => {

				GetAsync (urlPath, () => GetTripCompleted ());
			});		
			//return InvokeStrategy<TripClient>(()=> {
			//if (base.PostObjectAsync(urlPath, BookingEntry.GetTripDetail(), GetTripCompleted).HasErrors)
			//	return;
			//});			
		}

		public TripDetail GetTripRated { get { return (this.Model as TripDetail); } }

		public TripClient GetRatedTrip (BookingEntry bookingEntry, bool async = false)
		{
		
			return InvokeStrategy<TripClient> (() => {
				if (async) {
					if (base.PostObjectAsync (UrlPaths.GetRated, bookingEntry.GetTripDetail (), GetRatedTripCompleted).HasErrors)
						return;
				} else {
					if (base.PostObject (UrlPaths.GetRated, bookingEntry.GetTripDetail ()).HasErrors)
						return;

					GetRatedTripCompleted ();
				}
			});			
		}

		public TripClient GetRecommendedPickupTime (TripDetail trip, bool async = false)
		{

			return InvokeStrategy<TripClient> (() => {
				// need to nullify pickup time (in case it has a value) for recommendation to work
				trip.PickupDate = "";
				trip.PickupHour = "";
				trip.PickupMinutes = "";
				trip.PickupAmPm = "";

				if (async) {
					if (base.PostObjectAsync (UrlPaths.GetRecommendedPickupTime, trip, GetRecommendedPickupTimeCompleted).HasErrors)
						return;
				} else {
					if (base.PostObject (UrlPaths.GetRecommendedPickupTime, trip).HasErrors)
						return;

					GetRecommendedPickupTimeCompleted ();
				}
			});
		}

		private void GetRecommendedPickupTimeCompleted ()
		{
			if (this.HasErrors)
				return;

			this.RecommenedPickupDateTime = this.Model as RecommendedPickupTime;
		}

		private void GetRatedTripCompleted ()
		{
			if (this.HasErrors)
				return;		

			this.RatedTripDetail = this.Model as TripDetail;
		}

		public TripClient CreateAsync (TripDetail tripDetail)
		{
			return InvokeStrategy<TripClient> (() => {
                
				if (base.PostObjectAsync (UrlPaths.Create, tripDetail, CreateTripCompleted).HasErrors)
					return;
			});			
		}

		private void CreateTripCompleted ()
		{
			if (this.HasErrors)
				return;
			ApplicationContext.Current.TripConfirmation = this.Model as TripUpdateConfirmation;

		}

		private void GetTripCompleted ()
		{
			if (this.HasErrors)
				return;


			ApplicationContext.Current.ActiveQuoteGetInfo = this.Model as BookingEntry;
			ApplicationContext.Current.ActiveQuoteGetInfo.MergeFromApi ();
		}

		public TripClient UpdateAsync (TripDetail tripDetail)
		{
			return InvokeStrategy<TripClient> (() => {

				if (base.PostObjectAsync (UrlPaths.Update, tripDetail, UpdateTripCompleted).HasErrors)
					return;
			});			
		}

		private void UpdateTripCompleted ()
		{
			if (this.HasErrors)
				return;
			ApplicationContext.Current.TripConfirmation = this.Model as TripUpdateConfirmation;

		}

		public TripClient CancelTrip (string tripNumber)
		{
			return InvokeStrategy<TripClient> (() => {

				if (base.PostObjectAsync (UrlPaths.Cancel, tripNumber, CancelTripCompleted).HasErrors)
					return;
			});		

		}

		public TripClient CancelTripAsGuest (string emailAddress, string confirmationNumber, string tripNumber)
		{
			GuestTripIdentifier GuestTripID = new GuestTripIdentifier {
				EmailAddress = emailAddress,
				ConfirmationNumber = confirmationNumber,
				TripNumber = tripNumber
			};   

			return InvokeStrategy<TripClient> (() => {

				if (base.PostObjectAsync (UrlPaths.CancelAsGuest, GuestTripID, CancelTripCompleted).HasErrors)
					return;
			});		

		}

		public void CancelTripCompleted ()
		{

			if (this.HasErrors)
				return;
			ApplicationContext.Current.TripCancelConfirmation = this.Model as TripCancelConfirmation;


		}

	
	}
}

