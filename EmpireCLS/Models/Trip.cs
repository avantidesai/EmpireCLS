using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace EmpireCLS
{

	[Serializable]
	public class RecommendedPickupTime : ApiBaseModel
	{
		public string PickupAmPm { get; set; }

		public string PickupDate { get; set; }

		public string PickupHour { get; set; }

		public string PickupMinutes { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="prefix"></param>
		/// <returns></returns>
		public string Caption (string prefix = "")
		{
			return String.Format ("{0}{1} {2}:{3} {4}",
				prefix,
				this.PickupDate,
				this.PickupHour,
				this.PickupMinutes,
				this.PickupAmPm);
                  
		}

		public DateTime ToDateTime ()
		{
			return DateTime.Parse (string.Format ("{0} {1}:{2} {3}",
				this.PickupDate,
				this.PickupHour,
				this.PickupMinutes,
				this.PickupAmPm));
		}
    
	}


	[Serializable]
	public class GuestTripIdentifier
	{
		private string _TripNumber;

		/// <summary>
		/// 
		/// </summary>
		public string TripNumber {
			get { return _TripNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_TripNumber = value;
				} else {
					_TripNumber = value.Trim ();
				}
			}
		}

		private string _EmailAddress;

		/// <summary>
		/// 
		/// </summary>
		public string EmailAddress {
			get { return _EmailAddress; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_EmailAddress = value;
				} else {
					_EmailAddress = value.Trim ();
				}
			}
		}

		private string _ConfirmationNumber;

		/// <summary>
		/// 
		/// </summary>
		public string ConfirmationNumber {
			get { return _ConfirmationNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_ConfirmationNumber = value;
				} else {
					_ConfirmationNumber = value.Trim ();
				}
			}
		}
	}

	[Serializable]
	public class TripCancelConfirmation : ApiBaseModel
	{
		private string _TripNumber;

		/// <summary>
		/// 
		/// </summary>
		public string TripNumber {
			get { return _TripNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_TripNumber = value;
				} else {
					_TripNumber = value.Trim ();
				}
			}
		}

		private string _CancelationNumber;

		/// <summary>
		/// 
		/// </summary>
		public string CancelationNumber {
			get { return _CancelationNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CancelationNumber = value;
				} else {
					_CancelationNumber = value.Trim ();
				}
			}
		}
	}

	[Serializable]
	public class TripUpdateConfirmation : ApiBaseModel
	{
		private string _CustomerConfirmationNumber;

		/// <summary>
		/// 
		/// </summary>
		public string CustomerConfirmationNumber {
			get { return _CustomerConfirmationNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CustomerConfirmationNumber = value;
				} else {
					_CustomerConfirmationNumber = value.Trim ();
				}
			}
		}

        
		private string _TripNumber;

		/// <summary>
		/// 
		/// </summary>
		public string TripNumber {
			get { return _TripNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_TripNumber = value;
				} else {
					_TripNumber = value.Trim ();
				}
			}
		}

		private string _Itinerary;

		/// <summary>
		/// 
		/// </summary>
		public string Itinerary {
			get { return _Itinerary; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Itinerary = value;
				} else {
					_Itinerary = value.Trim ();
				}
			}
		}

		private string _CCAuthCode;

		/// <summary>
		/// 
		/// </summary>
		public string CCAuthCode {
			get { return _CCAuthCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CCAuthCode = value;
				} else {
					_CCAuthCode = value.Trim ();
				}
			}
		}

		private string _CCVerificationCode;

		/// <summary>
		/// 
		/// </summary>
		public string CCVerificationCode {
			get { return _CCVerificationCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CCVerificationCode = value;
				} else {
					_CCVerificationCode = value.Trim ();
				}
			}
		}

		private string _CCErrorCode;

		/// <summary>
		/// 
		/// </summary>
		public string CCErrorCode {
			get { return _CCErrorCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CCErrorCode = value;
				} else {
					_CCErrorCode = value.Trim ();
				}
			}
		}

		private string _CCErrorMessage;

		/// <summary>
		/// 
		/// </summary>
		public string CCErrorMessage {
			get { return _CCErrorMessage; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CCErrorMessage = value;
				} else {
					_CCErrorMessage = value.Trim ();
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string PickupProcedures {
			get { return _PickupProcedures; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupProcedures = value;
				} else {
					_PickupProcedures = value.Trim ();
				}
			}
		}

		private string _PickupProcedures;

	}


	/// <summary>
	/// Trip Detail
	/// </summary>
	//[Serializable]  JMO, removed so API unit tests don't include '_' characters
	public partial class TripDetail : ApiBaseModel
	{
		/// <summary>
		/// Trip Definition
		/// </summary>
		public TripDetail ()
		{
		}

		private string _GuestEmail;

		/// <summary>
		/// CPO Added 6/9/2014
		/// </summary>
		public int DefaultPaymentType { get; set; }

		/// <summary>
		/// CPO Added 7/30/2014
		/// </summary>
		public string BookingDisclaimer { get; set; }

		public string CancellationPolicy { get; set; }

        
		/// <summary>
		/// 
		/// </summary>
		public string PickupProcedures {
			get { return _PickupProcedures; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupProcedures = value;
				} else {
					_PickupProcedures = value.Trim ();
				}
			}
		}

		private string _PickupProcedures;

        
		/// <summary>
		/// 
		/// </summary>
		public string GuestEmail {
			get { return _GuestEmail; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_GuestEmail = value;
				} else {
					_GuestEmail = value.Trim ();
				}
			}
		}

		private string _CustomerConfirmationNumber;

		/// <summary>
		/// 
		/// </summary>
		public string CustomerConfirmationNumber {
			get { return _CustomerConfirmationNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CustomerConfirmationNumber = value;
				} else {
					_CustomerConfirmationNumber = value.Trim ();
				}
			}
		}

		private bool _UsePickupAirport;

		/// <summary>
		/// 
		/// </summary>
		public bool UsePickupAirport {
			get { return _UsePickupAirport; }
			set {
				_UsePickupAirport = value;
			}
		}

		private bool _UseDropOffAirport;

		/// <summary>
		/// 
		/// </summary>
		public bool UseDropOffAirport {
			get { return _UseDropOffAirport; }
			set {
				_UseDropOffAirport = value;
			}
		}

		private bool _HourlyTripFlag;

		/// <summary>
		/// 
		/// </summary>
		public bool HourlyTripFlag {
			get { return _HourlyTripFlag; }
			set {
				_HourlyTripFlag = value;
			}
		}

		private string _CustomerNumber;

		/// <summary>
		/// 
		/// </summary>
		public string CustomerNumber {
			get { return _CustomerNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CustomerNumber = value;
				} else {
					_CustomerNumber = value.Trim ();
				}
			}
		}

		private string _CustomerName;

		/// <summary>
		/// 
		/// </summary>
		public string CustomerName {
			get { return _CustomerName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CustomerName = value;
				} else {
					_CustomerName = value.Trim ();
				}
			}
		}

		private string _TripNumber;

		/// <summary>
		/// 
		/// </summary>
		public string TripNumber {
			get { return _TripNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_TripNumber = value;
				} else {
					_TripNumber = value.Trim ();
				}
			}
		}

		private string _CorporationNumber;

		/// <summary>
		/// 
		/// </summary>
		public string CorporationNumber {
			get { return _CorporationNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CorporationNumber = value;
				} else {
					_CorporationNumber = value.Trim ();
				}
			}
		}

		private string _PickupDate;

		/// <summary>
		/// 
		/// </summary>
		public string PickupDate {
			get { return _PickupDate; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupDate = value;
				} else {
					_PickupDate = value.Trim ();
				}
			}
		}

		private string _PickupHour;

		/// <summary>
		/// 
		/// </summary>
		public string PickupHour {
			get { return _PickupHour; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupHour = value;
				} else {
					_PickupHour = value.Trim ();
				}
			}
		}

		private string _PickupMinutes;

		/// <summary>
		/// 
		/// </summary>
		public string PickupMinutes {
			get { return _PickupMinutes; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupMinutes = value;
				} else {
					_PickupMinutes = value.Trim ();
				}
			}
		}

		private string _PickupAmPm;

		/// <summary>
		/// 
		/// </summary>
		public string PickupAmPm {
			get { return _PickupAmPm; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupAmPm = value;
				} else {
					_PickupAmPm = value.Trim ();
				}
			}
		}

		private bool _RecomendPickupTime;

		/// <summary>
		/// Let EmpireCLS suggest the time
		/// </summary>
		public bool RecomendPickupTime {
			get { return _RecomendPickupTime; }
			set {
				_RecomendPickupTime = value;
			}
		}

		private string _DropOffHour;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffHour {
			get { return _DropOffHour; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffHour = value;
				} else {
					_DropOffHour = value.Trim ();
				}
			}
		}

		private string _DropOffMinutes;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffMinutes {
			get { return _DropOffMinutes; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffMinutes = value;
				} else {
					_DropOffMinutes = value.Trim ();
				}
			}
		}

		private string _DropOffAmPm;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffAmPm {
			get { return _DropOffAmPm; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffAmPm = value;
				} else {
					_DropOffAmPm = value.Trim ();
				}
			}
		}

		private string _DropOffFlightHour;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffFlightHour {
			get { return _DropOffFlightHour; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffFlightHour = value;
				} else {
					_DropOffFlightHour = value.Trim ();
				}
			}
		}

		private string _DropOffFlightMinutes;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffFlightMinutes {
			get { return _DropOffFlightMinutes; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffFlightMinutes = value;
				} else {
					_DropOffFlightMinutes = value.Trim ();
				}
			}
		}

		private string _DropOffFlightAmPm;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffFlightAmPm {
			get { return _DropOffFlightAmPm; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffFlightAmPm = value;
				} else {
					_DropOffFlightAmPm = value.Trim ();
				}
			}
		}

		private string _DropOffFlightDate;

		/// <summary>
		/// Populated for outbound trips in order to calculate pickup date time
		/// </summary>
		public string DropOffFlightDate {
			get { return _DropOffFlightDate; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffFlightDate = value;
				} else {
					_DropOffFlightDate = value.Trim ();
				}
			}
		}

		private string _DropOffAirline;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffAirline {
			get { return _DropOffAirline; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffAirline = value;
				} else {
					_DropOffAirline = value.Trim ();
				}
			}
		}

		private string _DropOffFlightNumber;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffFlightNumber {
			get { return _DropOffFlightNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffFlightNumber = value;
				} else {
					_DropOffFlightNumber = value.Trim ();
				}
			}
		}

		private string _PassengerFirstName;

		/// <summary>
		/// 
		/// </summary>
		public string PassengerFirstName {
			get { return _PassengerFirstName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PassengerFirstName = value;
				} else {
					_PassengerFirstName = value.Trim ();
				}
			}
		}

		private string _PassengerLastName;

		/// <summary>
		/// 
		/// </summary>
		public string PassengerLastName {
			get { return _PassengerLastName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PassengerLastName = value;
				} else {
					_PassengerLastName = value.Trim ();
				}
			}
		}

		private int _NumberOfPassengers;

		/// <summary>
		/// 
		/// </summary>
		public int NumberOfPassengers {
			get { return _NumberOfPassengers; }
			set {
				_NumberOfPassengers = value;
			}
		}

		private string _CarType;

		/// <summary>
		/// 
		/// </summary>
		public string CarType {
			get { return _CarType; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CarType = value;
				} else {
					_CarType = value.Trim ();
				}
			}
		}

		private string _FBO;

		/// <summary>
		/// Fixed based operator - used for private jets
		/// </summary>
		public string FBO {
			get { return _FBO; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_FBO = value;
				} else {
					_FBO = value.Trim ();
				}
			}
		}

		private string _PickupAirport;

		/// <summary>
		/// 
		/// </summary>
		public string PickupAirport {
			get { return _PickupAirport; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupAirport = value;
				} else {
					_PickupAirport = value.Trim ();
				}
			}
		}

		private string _DropOffAirport;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffAirport {
			get { return _DropOffAirport; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffAirport = value;
				} else {
					_DropOffAirport = value.Trim ();
				}
			}
		}

		private string _PickupAirline;

		/// <summary>
		/// 
		/// </summary>
		public string PickupAirline {
			get { return _PickupAirline; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupAirline = value;
				} else {
					_PickupAirline = value.Trim ();
				}
			}
		}

		private string _PickupFlightNumber;

		/// <summary>
		/// 
		/// </summary>
		public string PickupFlightNumber {
			get { return _PickupFlightNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupFlightNumber = value;
				} else {
					_PickupFlightNumber = value.Trim ();
				}
			}
		}

		private string _PickupFlightOriginCity;

		/// <summary>
		/// 
		/// </summary>
		public string PickupFlightOriginCity {
			get { return _PickupFlightOriginCity; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupFlightOriginCity = value;
				} else {
					_PickupFlightOriginCity = value.Trim ();
				}
			}
		}

		private string _PickupLocationName;

		/// <summary>
		/// 
		/// </summary>
		public string PickupLocationName {
			get { return _PickupLocationName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupLocationName = value;
				} else {
					_PickupLocationName = value.Trim ();
				}
			}
		}

		private string _PickupAddress;

		/// <summary>
		/// 
		/// </summary>
		public string PickupAddress {
			get { return _PickupAddress; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupAddress = value;
				} else {
					_PickupAddress = value.Trim ();
				}
			}
		}

		private string _PickupCity;

		/// <summary>
		/// 
		/// </summary>
		public string PickupCity {
			get { return _PickupCity; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupCity = value;
				} else {
					_PickupCity = value.Trim ();
				}
			}
		}

		private string _PickupState;

		/// <summary>
		/// 
		/// </summary>
		public string PickupState {
			get { return _PickupState; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupState = value;
				} else {
					_PickupState = value.Trim ();
				}
			}
		}

		private string _PickupZipCode;

		/// <summary>
		/// 
		/// </summary>
		public string PickupZipCode {
			get { return _PickupZipCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupZipCode = value;
				} else {
					_PickupZipCode = value.Trim ();
				}
			}
		}

		private string _CrossStreet;

		/// <summary>
		/// 
		/// </summary>
		public string CrossStreet {
			get { return _CrossStreet; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CrossStreet = value;
				} else {
					_CrossStreet = value.Trim ();
				}
			}
		}

		private string _DropOffLocationName;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffLocationName {
			get { return _DropOffLocationName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffLocationName = value;
				} else {
					_DropOffLocationName = value.Trim ();
				}
			}
		}

		private string _DropOffAddress;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffAddress {
			get { return _DropOffAddress; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffAddress = value;
				} else {
					_DropOffAddress = value.Trim ();
				}
			}
		}

		private string _DropOffCity;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffCity {
			get { return _DropOffCity; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffCity = value;
				} else {
					_DropOffCity = value.Trim ();
				}
			}
		}

		private string _DropOffState;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffState {
			get { return _DropOffState; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffState = value;
				} else {
					_DropOffState = value.Trim ();
				}
			}
		}

		private string _DropOffZipCode;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffZipCode {
			get { return _DropOffZipCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffZipCode = value;
				} else {
					_DropOffZipCode = value.Trim ();
				}
			}
		}

		private string _SpecialInstructions;

		/// <summary>
		/// Details for Hourly Trips
		/// </summary>
		public string SpecialInstructions {
			get { return _SpecialInstructions; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialInstructions = value;
				} else {
					_SpecialInstructions = value.Trim ();
				}
			}
		}

		private string _AdditionalComments1;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalComments1 {
			get { return _AdditionalComments1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalComments1 = value;
				} else {
					_AdditionalComments1 = value.Trim ();
				}
			}
		}

		private string _BookedBy;

		/// <summary>
		/// 
		/// </summary>
		public string BookedBy {
			get { return _BookedBy; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_BookedBy = value;
				} else {
					_BookedBy = value.Trim ();
				}
			}
		}

		private string _ContactPhoneNumber;

		/// <summary>
		/// 
		/// </summary>
		public string ContactPhoneNumber {
			get { return _ContactPhoneNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_ContactPhoneNumber = value;
				} else {
					_ContactPhoneNumber = value.Trim ();
				}
			}
		}

		private string _PickupSign;

		/// <summary>
		/// 
		/// </summary>
		public string PickupSign {
			get { return _PickupSign; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupSign = value;
				} else {
					_PickupSign = value.Trim ();
				}
			}
		}

		private bool _IsReceiptReady;

		/// <summary>
		/// 
		/// </summary>
		public bool IsReceiptReady {
			get { return _IsReceiptReady; }
			set {
				_IsReceiptReady = value;
			}
		}

		private bool _IsTripComplete;

		/// <summary>
		/// 
		/// </summary>
		public bool IsTripComplete {
			get { return _IsTripComplete; }
			set {
				_IsTripComplete = value;
			}
		}

		private bool _IsTripCanBeModify;

		/// <summary>
		/// 
		/// </summary>
		public bool IsTripCanBeModify {
			get { return _IsTripCanBeModify; }
			set {
				_IsTripCanBeModify = value;
			}
		}

		private bool _IsTripCanBeCancel;

		/// <summary>
		/// 
		/// </summary>
		public bool IsTripCanBeCancel {
			get { return _IsTripCanBeCancel; }
			set {
				_IsTripCanBeCancel = value;
			}
		}

		private bool _HourlyRatedFlag;

		/// <summary>
		/// 
		/// </summary>
		public bool HourlyRatedFlag {
			get { return _HourlyRatedFlag; }
			set {
				_HourlyRatedFlag = value;
			}
		}

		private string _StartTime;

		/// <summary>
		/// Before a reservation has taken place, this is the estimated Garage Out Time based on what the system has.  If the sytem does not have this time it will not be included. When a reservation is in progress or has already completed, this is the actual Garage In Time.
		/// </summary>
		public string StartTime {
			get { return _StartTime; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_StartTime = value;
				} else {
					_StartTime = value.Trim ();
				}
			}
		}

		private string _PickupTime;

		/// <summary>
		/// Before a reservation has taken place, this is the time that the passenger requested to be picked up. When a reservation is in progress or has already completed, this is the actual time that the passenger was picked up.
		/// </summary>
		public string PickupTime {
			get { return _PickupTime; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupTime = value;
				} else {
					_PickupTime = value.Trim ();
				}
			}
		}

		private string _DropOffTime;

		/// <summary>
		/// Before a reservation has taken place, this is the time that the passenger requested to be dropped off. When a reservation is in progress or has already completed, this is the actual time that the passenger was dropped off.
		/// </summary>
		public string DropOffTime {
			get { return _DropOffTime; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffTime = value;
				} else {
					_DropOffTime = value.Trim ();
				}
			}
		}

		private string _EndTime;

		/// <summary>
		/// Before a reservation has taken place, this is the estimated Garage In Time based on what the system has.  If the sytem does not have this time it will not be included. When a reservation is in progress or has already completed, this is the actual Garage In Time.
		/// </summary>
		public string EndTime {
			get { return _EndTime; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_EndTime = value;
				} else {
					_EndTime = value.Trim ();
				}
			}
		}

		private string _TotalTime;

		/// <summary>
		/// Before a reservation has taken place, this is the estimated Total Time based on what the passenger has requested and estimated portal times if the system has them (StartTime and EndTime in the model). If the System does not have portal times, Start and End Time will not be included and the appropriate disclaimer should display that applicable portal times need to be added. When a reservation is in progress or has already completed, this is the actual Total Time, including Garage Out (StartTime) and Garage In (EndTime).
		/// </summary>
		public string TotalTime {
			get { return _TotalTime; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_TotalTime = value;
				} else {
					_TotalTime = value.Trim ();
				}
			}
		}

		private string _BaseRate;

		/// <summary>
		/// 
		/// </summary>
		public string BaseRate {
			get { return _BaseRate; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_BaseRate = value;
				} else {
					_BaseRate = value.Trim ();
				}
			}
		}

		private string _Tips;

		/// <summary>
		/// 
		/// </summary>
		public string Tips {
			get { return _Tips; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Tips = value;
				} else {
					_Tips = value.Trim ();
				}
			}
		}

		private string _STF;

		/// <summary>
		/// 
		/// </summary>
		public string STF {
			get { return _STF; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_STF = value;
				} else {
					_STF = value.Trim ();
				}
			}
		}

		private string _Taxes;

		/// <summary>
		/// 
		/// </summary>
		public string Taxes {
			get { return _Taxes; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Taxes = value;
				} else {
					_Taxes = value.Trim ();
				}
			}
		}

		private string _Parking;

		/// <summary>
		/// 
		/// </summary>
		public string Parking {
			get { return _Parking; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Parking = value;
				} else {
					_Parking = value.Trim ();
				}
			}
		}

		private string _Tolls;

		/// <summary>
		/// 
		/// </summary>
		public string Tolls {
			get { return _Tolls; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Tolls = value;
				} else {
					_Tolls = value.Trim ();
				}
			}
		}

		private string _FuelSurcharge;

		/// <summary>
		/// 
		/// </summary>
		public string FuelSurcharge {
			get { return _FuelSurcharge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_FuelSurcharge = value;
				} else {
					_FuelSurcharge = value.Trim ();
				}
			}
		}

		private string _Total;

		/// <summary>
		/// 
		/// </summary>
		public string Total {
			get { return _Total; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Total = value;
				} else {
					_Total = value.Trim ();
				}
			}
		}

		private bool _AutoReceiptFlag;

		/// <summary>
		/// 
		/// </summary>
		public bool AutoReceiptFlag {
			get { return _AutoReceiptFlag; }
			set {
				_AutoReceiptFlag = value;
			}
		}

		private string _AutoReceiptEmail;

		/// <summary>
		/// 
		/// </summary>
		public string AutoReceiptEmail {
			get { return _AutoReceiptEmail; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AutoReceiptEmail = value;
				} else {
					_AutoReceiptEmail = value.Trim ();
				}
			}
		}

		private bool _AutoReceiptAddToProfile;

		/// <summary>
		/// 
		/// </summary>
		public bool AutoReceiptAddToProfile {
			get { return _AutoReceiptAddToProfile; }
			set {
				_AutoReceiptAddToProfile = value;
			}
		}

		private bool _AdditionalStopFlag;

		/// <summary>
		/// 
		/// </summary>
		public bool AdditionalStopFlag {
			get { return _AdditionalStopFlag; }
			set {
				_AdditionalStopFlag = value;
			}
		}

		private string _AdditionalStopPassengerName1;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopPassengerName1 {
			get { return _AdditionalStopPassengerName1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopPassengerName1 = value;
				} else {
					_AdditionalStopPassengerName1 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopLocationName1;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopLocationName1 {
			get { return _AdditionalStopLocationName1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopLocationName1 = value;
				} else {
					_AdditionalStopLocationName1 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopAddress1;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopAddress1 {
			get { return _AdditionalStopAddress1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopAddress1 = value;
				} else {
					_AdditionalStopAddress1 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopCity1;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopCity1 {
			get { return _AdditionalStopCity1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopCity1 = value;
				} else {
					_AdditionalStopCity1 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopState1;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopState1 {
			get { return _AdditionalStopState1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopState1 = value;
				} else {
					_AdditionalStopState1 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopZip1;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopZip1 {
			get { return _AdditionalStopZip1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopZip1 = value;
				} else {
					_AdditionalStopZip1 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopPassengerName2;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopPassengerName2 {
			get { return _AdditionalStopPassengerName2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopPassengerName2 = value;
				} else {
					_AdditionalStopPassengerName2 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopLocationName2;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopLocationName2 {
			get { return _AdditionalStopLocationName2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopLocationName2 = value;
				} else {
					_AdditionalStopLocationName2 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopAddress2;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopAddress2 {
			get { return _AdditionalStopAddress2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopAddress2 = value;
				} else {
					_AdditionalStopAddress2 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopCity2;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopCity2 {
			get { return _AdditionalStopCity2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopCity2 = value;
				} else {
					_AdditionalStopCity2 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopState2;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopState2 {
			get { return _AdditionalStopState2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopState2 = value;
				} else {
					_AdditionalStopState2 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopZip2;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopZip2 {
			get { return _AdditionalStopZip2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopZip2 = value;
				} else {
					_AdditionalStopZip2 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopPassengerName3;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopPassengerName3 {
			get { return _AdditionalStopPassengerName3; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopPassengerName3 = value;
				} else {
					_AdditionalStopPassengerName3 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopLocationName3;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopLocationName3 {
			get { return _AdditionalStopLocationName3; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopLocationName3 = value;
				} else {
					_AdditionalStopLocationName3 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopAddress3;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopAddress3 {
			get { return _AdditionalStopAddress3; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopAddress3 = value;
				} else {
					_AdditionalStopAddress3 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopCity3;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopCity3 {
			get { return _AdditionalStopCity3; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopCity3 = value;
				} else {
					_AdditionalStopCity3 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopState3;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopState3 {
			get { return _AdditionalStopState3; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopState3 = value;
				} else {
					_AdditionalStopState3 = value.Trim ();
				}
			}
		}

		private string _AdditionalStopZip3;

		/// <summary>
		/// 
		/// </summary>
		public string AdditionalStopZip3 {
			get { return _AdditionalStopZip3; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdditionalStopZip3 = value;
				} else {
					_AdditionalStopZip3 = value.Trim ();
				}
			}
		}

		private bool _PaidByCreditCard;

		/// <summary>
		/// 
		/// </summary>
		public bool PaidByCreditCard {
			get { return _PaidByCreditCard; }
			set {
				_PaidByCreditCard = value;
			}
		}

		private bool _PaidByProfileCreditCard;

		/// <summary>
		/// 
		/// </summary>
		public bool PaidByProfileCreditCard {
			get { return _PaidByProfileCreditCard; }
			set {
				_PaidByProfileCreditCard = value;
			}
		}

		private bool _PaidByDirectBill;

		/// <summary>
		/// 
		/// </summary>
		public bool PaidByDirectBill {
			get { return _PaidByDirectBill; }
			set {
				_PaidByDirectBill = value;
			}
		}

		private bool _PaidByAdminDirectBill;

		/// <summary>
		/// 
		/// </summary>
		public bool PaidByAdminDirectBill {
			get { return _PaidByAdminDirectBill; }
			set {
				_PaidByAdminDirectBill = value;
			}
		}

		private string _CreditCardType;

		/// <summary>
		/// 
		/// </summary>
		public string CreditCardType {
			get { return _CreditCardType; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CreditCardType = value;
				} else {
					_CreditCardType = value.Trim ();
				}
			}
		}

		private string _CreditCardSubType;

		/// <summary>
		/// 
		/// </summary>
		public string CreditCardSubType {
			get { return _CreditCardSubType; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CreditCardSubType = value;
				} else {
					_CreditCardSubType = value.Trim ();
				}
			}
		}

		private string _CreditCardNumber;

		/// <summary>
		/// 
		/// </summary>   
       
		public string CreditCardNumber {
			get { return _CreditCardNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CreditCardNumber = value;
				} else {
					_CreditCardNumber = value.Trim ();
				}
			}
		}

		private string _CreditCardExpMonth;

		/// <summary>
		/// 
		/// </summary>
		public string CreditCardExpMonth {
			get { return _CreditCardExpMonth; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CreditCardExpMonth = value;
				} else {
					_CreditCardExpMonth = value.Trim ();
				}
			}
		}

		private string _CreditCardExpYear;

		/// <summary>
		/// 
		/// </summary>
		public string CreditCardExpYear {
			get { return _CreditCardExpYear; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CreditCardExpYear = value;
				} else {
					_CreditCardExpYear = value.Trim ();
				}
			}
		}

		private string _CreditCardSecurityCode;

		/// <summary>
		/// CID The Credit Card Validation Code is a 3 or 4 digit security code printed on the card.
		/// </summary>
		public string CreditCardSecurityCode {
			get { return _CreditCardSecurityCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CreditCardSecurityCode = value;
				} else {
					_CreditCardSecurityCode = value.Trim ();
				}
			}
		}

		private string _ProfileCreditCardKey;

		/// <summary>
		/// 
		/// </summary>
		public string ProfileCreditCardKey {
			get { return _ProfileCreditCardKey; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_ProfileCreditCardKey = value;
				} else {
					_ProfileCreditCardKey = value.Trim ();
				}
			}
		}

		private string _BillingName;

		/// <summary>
		/// Used for new cards, and for old cards that need to be updated -                   for old cards- this field should only be used if no billing address information is on file yet
		/// </summary>
		public string BillingName {
			get { return _BillingName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_BillingName = value;
				} else {
					_BillingName = value.Trim ();
				}
			}
		}

		private string _BillingAddress;

		/// <summary>
		/// 
		/// </summary>
		public string BillingAddress {
			get { return _BillingAddress; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_BillingAddress = value;
				} else {
					_BillingAddress = value.Trim ();
				}
			}
		}

		private string _BillingTown;

		/// <summary>
		/// 
		/// </summary>
		public string BillingTown {
			get { return _BillingTown; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_BillingTown = value;
				} else {
					_BillingTown = value.Trim ();
				}
			}
		}

		private string _BillingState;

		/// <summary>
		/// 
		/// </summary>
		public string BillingState {
			get { return _BillingState; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_BillingState = value;
				} else {
					_BillingState = value.Trim ();
				}
			}
		}

		private string _BillingCountry;

		/// <summary>
		/// 
		/// </summary>
		public string BillingCountry {
			get { return _BillingCountry; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_BillingCountry = value;
				} else {
					_BillingCountry = value.Trim ();
				}
			}
		}

		private string _BillingZip;

		/// <summary>
		/// 
		/// </summary>
		public string BillingZip {
			get { return _BillingZip; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_BillingZip = value;
				} else {
					_BillingZip = value.Trim ();
				}
			}
		}

		private bool _SaveCreditCardToProfile;

		/// <summary>
		/// Check this flag, if new card is being used
		/// </summary>
		public bool SaveCreditCardToProfile {
			get { return _SaveCreditCardToProfile; }
			set {
				_SaveCreditCardToProfile = value;
			}
		}

		private string _CostCenter;

		/// <summary>
		/// 
		/// </summary>
		public string CostCenter {
			get { return _CostCenter; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CostCenter = value;
				} else {
					_CostCenter = value.Trim ();
				}
			}
		}

		private string _AccountPassword;

		/// <summary>
		/// 
		/// </summary>
		public string AccountPassword {
			get { return _AccountPassword; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AccountPassword = value;
				} else {
					_AccountPassword = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue1;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue1 {
			get { return _SpecialBillingValue1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue1 = value;
				} else {
					_SpecialBillingValue1 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue2;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue2 {
			get { return _SpecialBillingValue2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue2 = value;
				} else {
					_SpecialBillingValue2 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue3;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue3 {
			get { return _SpecialBillingValue3; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue3 = value;
				} else {
					_SpecialBillingValue3 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue4;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue4 {
			get { return _SpecialBillingValue4; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue4 = value;
				} else {
					_SpecialBillingValue4 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue5;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue5 {
			get { return _SpecialBillingValue5; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue5 = value;
				} else {
					_SpecialBillingValue5 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue6;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue6 {
			get { return _SpecialBillingValue6; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue6 = value;
				} else {
					_SpecialBillingValue6 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue7;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue7 {
			get { return _SpecialBillingValue7; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue7 = value;
				} else {
					_SpecialBillingValue7 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue8;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue8 {
			get { return _SpecialBillingValue8; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue8 = value;
				} else {
					_SpecialBillingValue8 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue9;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue9 {
			get { return _SpecialBillingValue9; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue9 = value;
				} else {
					_SpecialBillingValue9 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue10;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue10 {
			get { return _SpecialBillingValue10; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue10 = value;
				} else {
					_SpecialBillingValue10 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue11;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue11 {
			get { return _SpecialBillingValue11; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue11 = value;
				} else {
					_SpecialBillingValue11 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue12;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue12 {
			get { return _SpecialBillingValue12; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue12 = value;
				} else {
					_SpecialBillingValue12 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue13;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue13 {
			get { return _SpecialBillingValue13; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue13 = value;
				} else {
					_SpecialBillingValue13 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue14;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue14 {
			get { return _SpecialBillingValue14; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue14 = value;
				} else {
					_SpecialBillingValue14 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue15;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue15 {
			get { return _SpecialBillingValue15; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue15 = value;
				} else {
					_SpecialBillingValue15 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue16;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue16 {
			get { return _SpecialBillingValue16; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue16 = value;
				} else {
					_SpecialBillingValue16 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue17;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue17 {
			get { return _SpecialBillingValue17; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue17 = value;
				} else {
					_SpecialBillingValue17 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue18;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue18 {
			get { return _SpecialBillingValue18; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue18 = value;
				} else {
					_SpecialBillingValue18 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue19;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue19 {
			get { return _SpecialBillingValue19; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue19 = value;
				} else {
					_SpecialBillingValue19 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingValue20;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingValue20 {
			get { return _SpecialBillingValue20; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingValue20 = value;
				} else {
					_SpecialBillingValue20 = value.Trim ();
				}
			}
		}

		private string _ChangedFields;

		/// <summary>
		/// 
		/// </summary>
		public string ChangedFields {
			get { return _ChangedFields; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_ChangedFields = value;
				} else {
					_ChangedFields = value.Trim ();
				}
			}
		}

		private bool _ShowRateToAffiliatesFarmout;

		/// <summary>
		/// Determines whether to display rates for Affilates/farmout trips
		/// </summary>
		public bool ShowRateToAffiliatesFarmout {
			get { return _ShowRateToAffiliatesFarmout; }
			set {
				_ShowRateToAffiliatesFarmout = value;
			}
		}

		private string _ConfirmationNumber;

		/// <summary>
		/// 
		/// </summary>
		public string ConfirmationNumber {
			get { return _ConfirmationNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_ConfirmationNumber = value;
				} else {
					_ConfirmationNumber = value.Trim ();
				}
			}
		}

		private string _AffiliateCancellationNumber;

		/// <summary>
		/// 
		/// </summary>
		public string AffiliateCancellationNumber {
			get { return _AffiliateCancellationNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AffiliateCancellationNumber = value;
				} else {
					_AffiliateCancellationNumber = value.Trim ();
				}
			}
		}

		private int _AffiliateFarmoutStatusStatus;

		/// <summary>
		/// Code based on AffiliateFarmoutStatusCode constant
		/// </summary>
		public int AffiliateFarmoutStatusStatus {
			get { return _AffiliateFarmoutStatusStatus; }
			set {
				_AffiliateFarmoutStatusStatus = value;
			}
		}

		private int _AffiliatePostChargeStatus;

		/// <summary>
		/// Enum of whether to include items accepted, denied or Pending Post charge.
		/// </summary>
		public int AffiliatePostChargeStatus {
			get { return _AffiliatePostChargeStatus; }
			set {
				_AffiliatePostChargeStatus = value;
			}
		}

		private string _DropoffLocationDescription;

		/// <summary>
		/// 
		/// </summary>
		public string DropoffLocationDescription {
			get { return _DropoffLocationDescription; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropoffLocationDescription = value;
				} else {
					_DropoffLocationDescription = value.Trim ();
				}
			}
		}

		private string _PickupLocationDescription;

		/// <summary>
		/// 
		/// </summary>
		public string PickupLocationDescription {
			get { return _PickupLocationDescription; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupLocationDescription = value;
				} else {
					_PickupLocationDescription = value.Trim ();
				}
			}
		}

		private bool _SavePickupLocationToAddressBook;

		/// <summary>
		/// 
		/// </summary>
		public bool SavePickupLocationToAddressBook {
			get { return _SavePickupLocationToAddressBook; }
			set {
				_SavePickupLocationToAddressBook = value;
			}
		}

		private int _InfantSeats;

		/// <summary>
		/// 
		/// </summary>
		public int InfantSeats {
			get { return _InfantSeats; }
			set {
				_InfantSeats = value;
			}
		}

		private int _ToddlerSeats;

		/// <summary>
		/// 
		/// </summary>
		public int ToddlerSeats {
			get { return _ToddlerSeats; }
			set {
				_ToddlerSeats = value;
			}
		}

		private int _BoosterSeats;

		/// <summary>
		/// 
		/// </summary>
		public int BoosterSeats {
			get { return _BoosterSeats; }
			set {
				_BoosterSeats = value;
			}
		}

		private int _TripStatus;

		/// <summary>
		/// Code based on TripStatusCode constant: Unknown = 0,Open = 1,Completed = 4,NoShowed = 6,Canceled = 7
		/// </summary>
		public int TripStatus {
			get { return _TripStatus; }
			set {
				_TripStatus = value;
			}
		}

		private int _ChauffeurStatus;

		/// <summary>
		/// Code based on ChauffeurStatusCode constant
		/// </summary>
		public int ChauffeurStatus {
			get { return _ChauffeurStatus; }
			set {
				_ChauffeurStatus = value;
			}
		}

		private string _LateNightCharge;

		/// <summary>
		/// 
		/// </summary>
		public string LateNightCharge {
			get { return _LateNightCharge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_LateNightCharge = value;
				} else {
					_LateNightCharge = value.Trim ();
				}
			}
		}

		private string _HolidayCharge;

		/// <summary>
		/// 
		/// </summary>
		public string HolidayCharge {
			get { return _HolidayCharge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_HolidayCharge = value;
				} else {
					_HolidayCharge = value.Trim ();
				}
			}
		}

		private string _MeetAndGreetCharge;

		/// <summary>
		/// 
		/// </summary>
		public string MeetAndGreetCharge {
			get { return _MeetAndGreetCharge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MeetAndGreetCharge = value;
				} else {
					_MeetAndGreetCharge = value.Trim ();
				}
			}
		}

		private string _ExtraStopCharge;

		/// <summary>
		/// 
		/// </summary>
		public string ExtraStopCharge {
			get { return _ExtraStopCharge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_ExtraStopCharge = value;
				} else {
					_ExtraStopCharge = value.Trim ();
				}
			}
		}

		private string _MiscCode1Charge;

		/// <summary>
		/// actual charge - baby seat charge, terminal charge, etc
		/// </summary>
		public string MiscCode1Charge {
			get { return _MiscCode1Charge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MiscCode1Charge = value;
				} else {
					_MiscCode1Charge = value.Trim ();
				}
			}
		}

		private string _MiscCode1ChargeDescription;

		/// <summary>
		/// description for charge
		/// </summary>
		public string MiscCode1ChargeDescription {
			get { return _MiscCode1ChargeDescription; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MiscCode1ChargeDescription = value;
				} else {
					_MiscCode1ChargeDescription = value.Trim ();
				}
			}
		}

		private string _MiscCode2Charge;

		/// <summary>
		/// actual charge- baby seat charge, terminal charge, etc
		/// </summary>
		public string MiscCode2Charge {
			get { return _MiscCode2Charge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MiscCode2Charge = value;
				} else {
					_MiscCode2Charge = value.Trim ();
				}
			}
		}

		private string _MiscCode2ChargeDescription;

		/// <summary>
		/// description for charge
		/// </summary>
		public string MiscCode2ChargeDescription {
			get { return _MiscCode2ChargeDescription; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MiscCode2ChargeDescription = value;
				} else {
					_MiscCode2ChargeDescription = value.Trim ();
				}
			}
		}

		private string _MiscCode3Charge;

		/// <summary>
		/// 
		/// </summary>
		public string MiscCode3Charge {
			get { return _MiscCode3Charge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MiscCode3Charge = value;
				} else {
					_MiscCode3Charge = value.Trim ();
				}
			}
		}

		private string _MiscCode3ChargeDescription;

		/// <summary>
		/// 
		/// </summary>
		public string MiscCode3ChargeDescription {
			get { return _MiscCode3ChargeDescription; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MiscCode3ChargeDescription = value;
				} else {
					_MiscCode3ChargeDescription = value.Trim ();
				}
			}
		}

		private string _DepartmentNumber;

		/// <summary>
		/// EmpireCLS Department Number
		/// </summary>
		public string DepartmentNumber {
			get { return _DepartmentNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DepartmentNumber = value;
				} else {
					_DepartmentNumber = value.Trim ();
				}
			}
		}

		private string _PreferredCarName;

		/// <summary>
		/// If multiple vehicles are available for a department car code the customer can select what they prefer if the vehicle is available.
		/// </summary>
		public string PreferredCarName {
			get { return _PreferredCarName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PreferredCarName = value;
				} else {
					_PreferredCarName = value.Trim ();
				}
			}
		}

		private List<string> _ChauffeurStatusHistory;

		/// <summary>
		/// History for the trip if it is available in the format: 05:00PM - EnRoute
		/// </summary>
		public List<string> ChauffeurStatusHistory {
			get { return _ChauffeurStatusHistory; }
			set {
				_ChauffeurStatusHistory = value;
			}
		}

		private string _HourlyRate;

		/// <summary>
		/// If the quote is hourly rated this will be populated with the hourly rate.  This is not yet used as the base rate icludes the total of this with other fees that need to be displayed but are not yet separate.
		/// </summary>
		public string HourlyRate {
			get { return _HourlyRate; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_HourlyRate = value;
				} else {
					_HourlyRate = value.Trim ();
				}
			}
		}

		private string _CancelationNumber;

		/// <summary>
		/// Applicable to cancelled trips
		/// </summary>
		public string CancelationNumber {
			get { return _CancelationNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CancelationNumber = value;
				} else {
					_CancelationNumber = value.Trim ();
				}
			}
		}

		private bool _ShouldShowDriverPicture;

		/// <summary>
		/// 
		/// </summary>
		public bool ShouldShowDriverPicture {
			get { return _ShouldShowDriverPicture; }
			set {
				_ShouldShowDriverPicture = value;
			}
		}

		private bool _ShouldShowDriverPhoneNumber;

		/// <summary>
		/// 
		/// </summary>
		public bool ShouldShowDriverPhoneNumber {
			get { return _ShouldShowDriverPhoneNumber; }
			set {
				_ShouldShowDriverPhoneNumber = value;
			}
		}

		private bool _ShouldShowDispatchPhoneNumber;

		/// <summary>
		/// 
		/// </summary>
		public bool ShouldShowDispatchPhoneNumber {
			get { return _ShouldShowDispatchPhoneNumber; }
			set {
				_ShouldShowDispatchPhoneNumber = value;
			}
		}

		private bool _ShouldShowQCPhoneNumber;

		/// <summary>
		/// 
		/// </summary>
		public bool ShouldShowQCPhoneNumber {
			get { return _ShouldShowQCPhoneNumber; }
			set {
				_ShouldShowQCPhoneNumber = value;
			}
		}

		private string _DriverHandle;

		/// <summary>
		/// 
		/// </summary>
		public string DriverHandle {
			get { return _DriverHandle; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DriverHandle = value;
				} else {
					_DriverHandle = value.Trim ();
				}
			}
		}

		private int _DriverNumber;

		/// <summary>
		/// 
		/// </summary>
		public int DriverNumber {
			get { return _DriverNumber; }
			set {
				_DriverNumber = value;
			}
		}

		private string _DriverEmpireCLSPhoneNumber;

		/// <summary>
		/// MotoQ Number if available
		/// </summary>
		public string DriverEmpireCLSPhoneNumber {
			get { return _DriverEmpireCLSPhoneNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DriverEmpireCLSPhoneNumber = value;
				} else {
					_DriverEmpireCLSPhoneNumber = value.Trim ();
				}
			}
		}

		private string _CorporationName;

		/// <summary>
		/// Account name - used to display to affiliate and farmouts in order to properly bill
		/// </summary>
		public string CorporationName {
			get { return _CorporationName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CorporationName = value;
				} else {
					_CorporationName = value.Trim ();
				}
			}
		}

		private string _VipStatus;

		/// <summary>
		/// Used for affiliate and farmouts
		/// </summary>
		public string VipStatus {
			get { return _VipStatus; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_VipStatus = value;
				} else {
					_VipStatus = value.Trim ();
				}
			}
		}

		private string _TimeStamp;

		/// <summary>
		/// Used to determine if trip changed since it was returned
		/// </summary>
		public string TimeStamp {
			get { return _TimeStamp; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_TimeStamp = value;
				} else {
					_TimeStamp = value.Trim ();
				}
			}
		}

		private string _NetJetGroupingNumber;

		/// <summary>
		/// ID of NetJet Trip
		/// </summary>
		public string NetJetGroupingNumber {
			get { return _NetJetGroupingNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_NetJetGroupingNumber = value;
				} else {
					_NetJetGroupingNumber = value.Trim ();
				}
			}
		}

		private bool _HasAdditionalPassengers;

		/// <summary>
		/// Flag specifying if trip has additional passengers
		/// </summary>
		public bool HasAdditionalPassengers {
			get { return _HasAdditionalPassengers; }
			set {
				_HasAdditionalPassengers = value;
			}
		}

		private int _PromotionID;

		/// <summary>
		/// 
		/// </summary>
		public int PromotionID {
			get { return _PromotionID; }
			set {
				_PromotionID = value;
			}
		}

		private string _PromotionDisclaimerText;

		/// <summary>
		/// 
		/// </summary>
		public string PromotionDisclaimerText {
			get { return _PromotionDisclaimerText; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PromotionDisclaimerText = value;
				} else {
					_PromotionDisclaimerText = value.Trim ();
				}
			}
		}

		private string _PromotionName;

		/// <summary>
		/// 
		/// </summary>
		public string PromotionName {
			get { return _PromotionName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PromotionName = value;
				} else {
					_PromotionName = value.Trim ();
				}
			}
		}

		private string _PromotionDiscountPercent;

		/// <summary>
		/// 
		/// </summary>
		public string PromotionDiscountPercent {
			get { return _PromotionDiscountPercent; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PromotionDiscountPercent = value;
				} else {
					_PromotionDiscountPercent = value.Trim ();
				}
			}
		}

		private string _PromotionFlatDiscount;

		/// <summary>
		/// 
		/// </summary>
		public string PromotionFlatDiscount {
			get { return _PromotionFlatDiscount; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PromotionFlatDiscount = value;
				} else {
					_PromotionFlatDiscount = value.Trim ();
				}
			}
		}

		private string _FullRate;

		/// <summary>
		/// 
		/// </summary>
		public string FullRate {
			get { return _FullRate; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_FullRate = value;
				} else {
					_FullRate = value.Trim ();
				}
			}
		}

		private string _MiscExp;

		/// <summary>
		/// 
		/// </summary>
		public string MiscExp {
			get { return _MiscExp; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MiscExp = value;
				} else {
					_MiscExp = value.Trim ();
				}
			}
		}

		private string _WaitingExp;

		/// <summary>
		/// 
		/// </summary>
		public string WaitingExp {
			get { return _WaitingExp; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_WaitingExp = value;
				} else {
					_WaitingExp = value.Trim ();
				}
			}
		}

		private string _PhoneExp;

		/// <summary>
		/// 
		/// </summary>
		public string PhoneExp {
			get { return _PhoneExp; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PhoneExp = value;
				} else {
					_PhoneExp = value.Trim ();
				}
			}
		}

		private string _HourlyFuelSurcharge;

		/// <summary>
		/// 
		/// </summary>
		public string HourlyFuelSurcharge {
			get { return _HourlyFuelSurcharge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_HourlyFuelSurcharge = value;
				} else {
					_HourlyFuelSurcharge = value.Trim ();
				}
			}
		}

		private string _QuoteAmount;

		/// <summary>
		/// 
		/// </summary>
		public string QuoteAmount {
			get { return _QuoteAmount; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_QuoteAmount = value;
				} else {
					_QuoteAmount = value.Trim ();
				}
			}
		}

		private string _BookedWithPayCode;

		/// <summary>
		/// 
		/// </summary>
		public string BookedWithPayCode {
			get { return _BookedWithPayCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_BookedWithPayCode = value;
				} else {
					_BookedWithPayCode = value.Trim ();
				}
			}
		}

		private string _MainPaymentPayCode;

		/// <summary>
		/// 
		/// </summary>
		public string MainPaymentPayCode {
			get { return _MainPaymentPayCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MainPaymentPayCode = value;
				} else {
					_MainPaymentPayCode = value.Trim ();
				}
			}
		}

		private string _MainPaymentCCSurcharge;

		/// <summary>
		/// 
		/// </summary>
		public string MainPaymentCCSurcharge {
			get { return _MainPaymentCCSurcharge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MainPaymentCCSurcharge = value;
				} else {
					_MainPaymentCCSurcharge = value.Trim ();
				}
			}
		}

		private string _MainPaymentTolls;

		/// <summary>
		/// 
		/// </summary>
		public string MainPaymentTolls {
			get { return _MainPaymentTolls; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MainPaymentTolls = value;
				} else {
					_MainPaymentTolls = value.Trim ();
				}
			}
		}

		private string _MainPaymentTips;

		/// <summary>
		/// 
		/// </summary>
		public string MainPaymentTips {
			get { return _MainPaymentTips; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MainPaymentTips = value;
				} else {
					_MainPaymentTips = value.Trim ();
				}
			}
		}

		private string _MainPaymentRate;

		/// <summary>
		/// 
		/// </summary>
		public string MainPaymentRate {
			get { return _MainPaymentRate; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MainPaymentRate = value;
				} else {
					_MainPaymentRate = value.Trim ();
				}
			}
		}

		private string _MainPaymentGross;

		/// <summary>
		/// 
		/// </summary>
		public string MainPaymentGross {
			get { return _MainPaymentGross; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_MainPaymentGross = value;
				} else {
					_MainPaymentGross = value.Trim ();
				}
			}
		}

		private string _SplitPayment1PayCode;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment1PayCode {
			get { return _SplitPayment1PayCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment1PayCode = value;
				} else {
					_SplitPayment1PayCode = value.Trim ();
				}
			}
		}

		private string _SplitPayment1CCSurcharge;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment1CCSurcharge {
			get { return _SplitPayment1CCSurcharge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment1CCSurcharge = value;
				} else {
					_SplitPayment1CCSurcharge = value.Trim ();
				}
			}
		}

		private string _SplitPayment1Tolls;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment1Tolls {
			get { return _SplitPayment1Tolls; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment1Tolls = value;
				} else {
					_SplitPayment1Tolls = value.Trim ();
				}
			}
		}

		private string _SplitPayment1Tips;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment1Tips {
			get { return _SplitPayment1Tips; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment1Tips = value;
				} else {
					_SplitPayment1Tips = value.Trim ();
				}
			}
		}

		private string _SplitPayment1Rate;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment1Rate {
			get { return _SplitPayment1Rate; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment1Rate = value;
				} else {
					_SplitPayment1Rate = value.Trim ();
				}
			}
		}

		private string _SplitPayment1Gross;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment1Gross {
			get { return _SplitPayment1Gross; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment1Gross = value;
				} else {
					_SplitPayment1Gross = value.Trim ();
				}
			}
		}

		private string _SplitPayment2PayCode;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment2PayCode {
			get { return _SplitPayment2PayCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment2PayCode = value;
				} else {
					_SplitPayment2PayCode = value.Trim ();
				}
			}
		}

		private string _SplitPayment2CCSurcharge;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment2CCSurcharge {
			get { return _SplitPayment2CCSurcharge; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment2CCSurcharge = value;
				} else {
					_SplitPayment2CCSurcharge = value.Trim ();
				}
			}
		}

		private string _SplitPayment2Tolls;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment2Tolls {
			get { return _SplitPayment2Tolls; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment2Tolls = value;
				} else {
					_SplitPayment2Tolls = value.Trim ();
				}
			}
		}

		private string _SplitPayment2Tips;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment2Tips {
			get { return _SplitPayment2Tips; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment2Tips = value;
				} else {
					_SplitPayment2Tips = value.Trim ();
				}
			}
		}

		private string _SplitPayment2Rate;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment2Rate {
			get { return _SplitPayment2Rate; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment2Rate = value;
				} else {
					_SplitPayment2Rate = value.Trim ();
				}
			}
		}

		private string _SplitPayment2Gross;

		/// <summary>
		/// 
		/// </summary>
		public string SplitPayment2Gross {
			get { return _SplitPayment2Gross; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SplitPayment2Gross = value;
				} else {
					_SplitPayment2Gross = value.Trim ();
				}
			}
		}

		private string _CancelledDateTime;

		/// <summary>
		/// 
		/// </summary>
		public string CancelledDateTime {
			get { return _CancelledDateTime; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CancelledDateTime = value;
				} else {
					_CancelledDateTime = value.Trim ();
				}
			}
		}

		/// <summary>
		/// Trip Notifications (SMS/Email/Fax)
		/// </summary>
		#region "Trip Notifications"
        public bool TripNotificationIsReadOnly1 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType1 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress1 { get; set; }

		public bool TripNotificationIsReadOnly2 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType2 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress2 { get; set; }

		public bool TripNotificationIsReadOnly3 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType3 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress3 { get; set; }

		public bool TripNotificationIsReadOnly4 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType4 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress4 { get; set; }

		public bool TripNotificationIsReadOnly5 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType5 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress5 { get; set; }

		public bool TripNotificationIsReadOnly6 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType6 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress6 { get; set; }

		public bool TripNotificationIsReadOnly7 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType7 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress7 { get; set; }

		public bool TripNotificationIsReadOnly8 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType8 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress8 { get; set; }

		public bool TripNotificationIsReadOnly9 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType9 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress9 { get; set; }

		public bool TripNotificationIsReadOnly10 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType10 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress10 { get; set; }

		public bool TripNotificationIsReadOnly11 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType11 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress11 { get; set; }

		public bool TripNotificationIsReadOnly12 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType12 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress12 { get; set; }

		public bool TripNotificationIsReadOnly13 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType13 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress13 { get; set; }

		public bool TripNotificationIsReadOnly14 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType14 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress14 { get; set; }

		public bool TripNotificationIsReadOnly15 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType15 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress15 { get; set; }

		public bool TripNotificationIsReadOnly16 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType16 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress16 { get; set; }

		public bool TripNotificationIsReadOnly17 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType17 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress17 { get; set; }

		public bool TripNotificationIsReadOnly18 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType18 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress18 { get; set; }

		public bool TripNotificationIsReadOnly19 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType19 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress19 { get; set; }

		public bool TripNotificationIsReadOnly20 { get; set; }

		/// <summary>
		/// DriverInfo (1), Onlocation (5)
		/// </summary>
		public int TripNotificationType20 { get; set; }

		/// <summary>
		/// Phone Number, Fax Number, Email Address
		/// </summary>
		public string TripNotificationAddress20 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile1 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile2 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile3 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile4 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile5 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile6 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile7 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile8 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile9 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile10 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile11 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile12 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile13 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile14 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile15 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile16 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile17 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile18 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile19 { get; set; }

		/// <summary>
		/// Address is from Profile
		/// </summary>
		public bool TripNotificationIsProfile20 { get; set; }



		public int TripNotificationID1 { get; set; }

		public int TripNotificationID2 { get; set; }

		public int TripNotificationID3 { get; set; }

		public int TripNotificationID4 { get; set; }

		public int TripNotificationID5 { get; set; }

		public int TripNotificationID6 { get; set; }

		public int TripNotificationID7 { get; set; }

		public int TripNotificationID8 { get; set; }

		public int TripNotificationID9 { get; set; }

		public int TripNotificationID10 { get; set; }

		public int TripNotificationID11 { get; set; }

		public int TripNotificationID12 { get; set; }

		public int TripNotificationID13 { get; set; }

		public int TripNotificationID14 { get; set; }

		public int TripNotificationID15 { get; set; }

		public int TripNotificationID16 { get; set; }

		public int TripNotificationID17 { get; set; }

		public int TripNotificationID18 { get; set; }

		public int TripNotificationID19 { get; set; }

		public int TripNotificationID20 { get; set; }

		public bool TripNotificationUpdateProfile1 { get; set; }

		public bool TripNotificationUpdateProfile2 { get; set; }

		public bool TripNotificationUpdateProfile3 { get; set; }

		public bool TripNotificationUpdateProfile4 { get; set; }

		public bool TripNotificationUpdateProfile5 { get; set; }

		public bool TripNotificationUpdateProfile6 { get; set; }

		public bool TripNotificationUpdateProfile7 { get; set; }

		public bool TripNotificationUpdateProfile8 { get; set; }

		public bool TripNotificationUpdateProfile9 { get; set; }

		public bool TripNotificationUpdateProfile10 { get; set; }

		public bool TripNotificationUpdateProfile11 { get; set; }

		public bool TripNotificationUpdateProfile12 { get; set; }

		public bool TripNotificationUpdateProfile13 { get; set; }

		public bool TripNotificationUpdateProfile14 { get; set; }

		public bool TripNotificationUpdateProfile15 { get; set; }

		public bool TripNotificationUpdateProfile16 { get; set; }

		public bool TripNotificationUpdateProfile17 { get; set; }

		public bool TripNotificationUpdateProfile18 { get; set; }

		public bool TripNotificationUpdateProfile19 { get; set; }

		public bool TripNotificationUpdateProfile20 { get; set; }

        
        
		#endregion "Trip Notifications"

		//public class SimplePropertyNames
		//{
		//    [XmlIgnore()]
		//    public const string UsePickupAirport = "UsePickupAirport";
		//    [XmlIgnore()]
		//    public const string UseDropOffAirport = "UseDropOffAirport";
		//    [XmlIgnore()]
		//    public const string HourlyTripFlag = "HourlyTripFlag";
		//    [XmlIgnore()]
		//    public const string CustomerNumber = "CustomerNumber";
		//    [XmlIgnore()]
		//    public const string TripNumber = "TripNumber";
		//    [XmlIgnore()]
		//    public const string CorporationNumber = "CorporationNumber";
		//    [XmlIgnore()]
		//    public const string PickupDate = "PickupDate";
		//    [XmlIgnore()]
		//    public const string PickupHour = "PickupHour";
		//    [XmlIgnore()]
		//    public const string PickupMinutes = "PickupMinutes";
		//    [XmlIgnore()]
		//    public const string PickupAmPm = "PickupAmPm";
		//    [XmlIgnore()]
		//    public const string RecomendPickupTime = "RecomendPickupTime";
		//    [XmlIgnore()]
		//    public const string DropOffHour = "DropOffHour";
		//    [XmlIgnore()]
		//    public const string DropOffMinutes = "DropOffMinutes";
		//    [XmlIgnore()]
		//    public const string DropOffAmPm = "DropOffAmPm";
		//    [XmlIgnore()]
		//    public const string DropOffFlightHour = "DropOffFlightHour";
		//    [XmlIgnore()]
		//    public const string DropOffFlightMinutes = "DropOffFlightMinutes";
		//    [XmlIgnore()]
		//    public const string DropOffFlightAmPm = "DropOffFlightAmPm";
		//    [XmlIgnore()]
		//    public const string DropOffFlightDate = "DropOffFlightDate";
		//    [XmlIgnore()]
		//    public const string DropOffAirline = "DropOffAirline";
		//    [XmlIgnore()]
		//    public const string DropOffFlightNumber = "DropOffFlightNumber";
		//    [XmlIgnore()]
		//    public const string PassengerFirstName = "PassengerFirstName";
		//    [XmlIgnore()]
		//    public const string PassengerLastName = "PassengerLastName";
		//    [XmlIgnore()]
		//    public const string NumberOfPassengers = "NumberOfPassengers";
		//    [XmlIgnore()]
		//    public const string CarType = "CarType";
		//    [XmlIgnore()]
		//    public const string FBO = "FBO";
		//    [XmlIgnore()]
		//    public const string PickupAirport = "PickupAirport";
		//    [XmlIgnore()]
		//    public const string DropOffAirport = "DropOffAirport";
		//    [XmlIgnore()]
		//    public const string PickupAirline = "PickupAirline";
		//    [XmlIgnore()]
		//    public const string PickupFlightNumber = "PickupFlightNumber";
		//    [XmlIgnore()]
		//    public const string PickupFlightOriginCity = "PickupFlightOriginCity";
		//    [XmlIgnore()]
		//    public const string PickupLocationName = "PickupLocationName";
		//    [XmlIgnore()]
		//    public const string PickupAddress = "PickupAddress";
		//    [XmlIgnore()]
		//    public const string PickupCity = "PickupCity";
		//    [XmlIgnore()]
		//    public const string PickupState = "PickupState";
		//    [XmlIgnore()]
		//    public const string PickupZipCode = "PickupZipCode";
		//    [XmlIgnore()]
		//    public const string CrossStreet = "CrossStreet";
		//    [XmlIgnore()]
		//    public const string DropOffLocationName = "DropOffLocationName";
		//    [XmlIgnore()]
		//    public const string DropOffAddress = "DropOffAddress";
		//    [XmlIgnore()]
		//    public const string DropOffCity = "DropOffCity";
		//    [XmlIgnore()]
		//    public const string DropOffState = "DropOffState";
		//    [XmlIgnore()]
		//    public const string DropOffZipCode = "DropOffZipCode";
		//    [XmlIgnore()]
		//    public const string SpecialInstructions = "SpecialInstructions";
		//    [XmlIgnore()]
		//    public const string AdditionalComments1 = "AdditionalComments1";
		//    [XmlIgnore()]
		//    public const string BookedBy = "BookedBy";
		//    [XmlIgnore()]
		//    public const string ContactPhoneNumber = "ContactPhoneNumber";
		//    [XmlIgnore()]
		//    public const string PickupSign = "PickupSign";
		//    [XmlIgnore()]
		//    public const string IsReceiptReady = "IsReceiptReady";
		//    [XmlIgnore()]
		//    public const string IsTripComplete = "IsTripComplete";
		//    [XmlIgnore()]
		//    public const string IsTripCanBeModify = "IsTripCanBeModify";
		//    [XmlIgnore()]
		//    public const string IsTripCanBeCancel = "IsTripCanBeCancel";
		//    [XmlIgnore()]
		//    public const string HourlyRatedFlag = "HourlyRatedFlag";
		//    [XmlIgnore()]
		//    public const string StartTime = "StartTime";
		//    [XmlIgnore()]
		//    public const string PickupTime = "PickupTime";
		//    [XmlIgnore()]
		//    public const string DropOffTime = "DropOffTime";
		//    [XmlIgnore()]
		//    public const string EndTime = "EndTime";
		//    [XmlIgnore()]
		//    public const string TotalTime = "TotalTime";
		//    [XmlIgnore()]
		//    public const string BaseRate = "BaseRate";
		//    [XmlIgnore()]
		//    public const string Tips = "Tips";
		//    [XmlIgnore()]
		//    public const string STF = "STF";
		//    [XmlIgnore()]
		//    public const string Taxes = "Taxes";
		//    [XmlIgnore()]
		//    public const string Parking = "Parking";
		//    [XmlIgnore()]
		//    public const string Tolls = "Tolls";
		//    [XmlIgnore()]
		//    public const string FuelSurcharge = "FuelSurcharge";
		//    [XmlIgnore()]
		//    public const string Total = "Total";
		//    [XmlIgnore()]
		//    public const string AutoReceiptFlag = "AutoReceiptFlag";
		//    [XmlIgnore()]
		//    public const string AutoReceiptEmail = "AutoReceiptEmail";
		//    [XmlIgnore()]
		//    public const string AutoReceiptAddToProfile = "AutoReceiptAddToProfile";
		//    [XmlIgnore()]
		//    public const string AdditionalStopFlag = "AdditionalStopFlag";
		//    [XmlIgnore()]
		//    public const string AdditionalStopPassengerName1 = "AdditionalStopPassengerName1";
		//    [XmlIgnore()]
		//    public const string AdditionalStopLocationName1 = "AdditionalStopLocationName1";
		//    [XmlIgnore()]
		//    public const string AdditionalStopAddress1 = "AdditionalStopAddress1";
		//    [XmlIgnore()]
		//    public const string AdditionalStopCity1 = "AdditionalStopCity1";
		//    [XmlIgnore()]
		//    public const string AdditionalStopState1 = "AdditionalStopState1";
		//    [XmlIgnore()]
		//    public const string AdditionalStopZip1 = "AdditionalStopZip1";
		//    [XmlIgnore()]
		//    public const string AdditionalStopPassengerName2 = "AdditionalStopPassengerName2";
		//    [XmlIgnore()]
		//    public const string AdditionalStopLocationName2 = "AdditionalStopLocationName2";
		//    [XmlIgnore()]
		//    public const string AdditionalStopAddress2 = "AdditionalStopAddress2";
		//    [XmlIgnore()]
		//    public const string AdditionalStopCity2 = "AdditionalStopCity2";
		//    [XmlIgnore()]
		//    public const string AdditionalStopState2 = "AdditionalStopState2";
		//    [XmlIgnore()]
		//    public const string AdditionalStopZip2 = "AdditionalStopZip2";
		//    [XmlIgnore()]
		//    public const string AdditionalStopPassengerName3 = "AdditionalStopPassengerName3";
		//    [XmlIgnore()]
		//    public const string AdditionalStopLocationName3 = "AdditionalStopLocationName3";
		//    [XmlIgnore()]
		//    public const string AdditionalStopAddress3 = "AdditionalStopAddress3";
		//    [XmlIgnore()]
		//    public const string AdditionalStopCity3 = "AdditionalStopCity3";
		//    [XmlIgnore()]
		//    public const string AdditionalStopState3 = "AdditionalStopState3";
		//    [XmlIgnore()]
		//    public const string AdditionalStopZip3 = "AdditionalStopZip3";
		//    [XmlIgnore()]
		//    public const string PaidByCreditCard = "PaidByCreditCard";
		//    [XmlIgnore()]
		//    public const string PaidByProfileCreditCard = "PaidByProfileCreditCard";
		//    [XmlIgnore()]
		//    public const string PaidByDirectBill = "PaidByDirectBill";
		//    [XmlIgnore()]
		//    public const string PaidByAdminDirectBill = "PaidByAdminDirectBill";
		//    [XmlIgnore()]
		//    public const string CreditCardType = "CreditCardType";
		//    [XmlIgnore()]
		//    public const string CreditCardSubType = "CreditCardSubType";
		//    [XmlIgnore()]
		//    public const string CreditCardNumber = "CreditCardNumber";
		//    [XmlIgnore()]
		//    public const string CreditCardExpMonth = "CreditCardExpMonth";
		//    [XmlIgnore()]
		//    public const string CreditCardExpYear = "CreditCardExpYear";
		//    [XmlIgnore()]
		//    public const string CreditCardSecurityCode = "CreditCardSecurityCode";
		//    [XmlIgnore()]
		//    public const string ProfileCreditCardKey = "ProfileCreditCardKey";
		//    [XmlIgnore()]
		//    public const string BillingName = "BillingName";
		//    [XmlIgnore()]
		//    public const string BillingAddress = "BillingAddress";
		//    [XmlIgnore()]
		//    public const string BillingTown = "BillingTown";
		//    [XmlIgnore()]
		//    public const string BillingState = "BillingState";
		//    [XmlIgnore()]
		//    public const string BillingCountry = "BillingCountry";
		//    [XmlIgnore()]
		//    public const string BillingZip = "BillingZip";
		//    [XmlIgnore()]
		//    public const string SaveCreditCardToProfile = "SaveCreditCardToProfile";
		//    [XmlIgnore()]
		//    public const string CostCenter = "CostCenter";
		//    [XmlIgnore()]
		//    public const string AccountPassword = "AccountPassword";
		//    [XmlIgnore()]
		//    public const string SpecialBillingValue1 = "SpecialBillingValue1";
		//    [XmlIgnore()]
		//    public const string SpecialBillingValue2 = "SpecialBillingValue2";
		//    [XmlIgnore()]
		//    public const string SpecialBillingValue3 = "SpecialBillingValue3";
		//    [XmlIgnore()]
		//    public const string SpecialBillingValue4 = "SpecialBillingValue4";
		//    [XmlIgnore()]
		//    public const string SpecialBillingValue5 = "SpecialBillingValue5";
		//    [XmlIgnore()]
		//    public const string ChangedFields = "ChangedFields";
		//    [XmlIgnore()]
		//    public const string ShowRateToAffiliatesFarmout = "ShowRateToAffiliatesFarmout";
		//    [XmlIgnore()]
		//    public const string ConfirmationNumber = "ConfirmationNumber";
		//    [XmlIgnore()]
		//    public const string AffiliateCancellationNumber = "AffiliateCancellationNumber";
		//    [XmlIgnore()]
		//    public const string AffiliateFarmoutStatusStatus = "AffiliateFarmoutStatusStatus";
		//    [XmlIgnore()]
		//    public const string AffiliatePostChargeStatus = "AffiliatePostChargeStatus";
		//    [XmlIgnore()]
		//    public const string DropoffLocationDescription = "DropoffLocationDescription";
		//    [XmlIgnore()]
		//    public const string PickupLocationDescription = "PickupLocationDescription";
		//    [XmlIgnore()]
		//    public const string SavePickupLocationToAddressBook = "SavePickupLocationToAddressBook";
		//    [XmlIgnore()]
		//    public const string InfantSeats = "InfantSeats";
		//    [XmlIgnore()]
		//    public const string ToddlerSeats = "ToddlerSeats";
		//    [XmlIgnore()]
		//    public const string BoosterSeats = "BoosterSeats";
		//    [XmlIgnore()]
		//    public const string TripStatus = "TripStatus";
		//    [XmlIgnore()]
		//    public const string ChauffeurStatus = "ChauffeurStatus";
		//    [XmlIgnore()]
		//    public const string LateNightCharge = "LateNightCharge";
		//    [XmlIgnore()]
		//    public const string HolidayCharge = "HolidayCharge";
		//    [XmlIgnore()]
		//    public const string MeetAndGreetCharge = "MeetAndGreetCharge";
		//    [XmlIgnore()]
		//    public const string ExtraStopCharge = "ExtraStopCharge";
		//    [XmlIgnore()]
		//    public const string MiscCode1Charge = "MiscCode1Charge";
		//    [XmlIgnore()]
		//    public const string MiscCode1ChargeDescription = "MiscCode1ChargeDescription";
		//    [XmlIgnore()]
		//    public const string MiscCode2Charge = "MiscCode2Charge";
		//    [XmlIgnore()]
		//    public const string MiscCode2ChargeDescription = "MiscCode2ChargeDescription";
		//    [XmlIgnore()]
		//    public const string MiscCode3Charge = "MiscCode3Charge";
		//    [XmlIgnore()]
		//    public const string MiscCode3ChargeDescription = "MiscCode3ChargeDescription";
		//    [XmlIgnore()]
		//    public const string DepartmentNumber = "DepartmentNumber";
		//    [XmlIgnore()]
		//    public const string PreferredCarName = "PreferredCarName";
		//    [XmlIgnore()]
		//    public const string ChauffeurStatusHistory = "ChauffeurStatusHistory";
		//    [XmlIgnore()]
		//    public const string HourlyRate = "HourlyRate";
		//    [XmlIgnore()]
		//    public const string CancelationNumber = "CancelationNumber";
		//    [XmlIgnore()]
		//    public const string ShouldShowDriverPicture = "ShouldShowDriverPicture";
		//    [XmlIgnore()]
		//    public const string ShouldShowDriverPhoneNumber = "ShouldShowDriverPhoneNumber";
		//    [XmlIgnore()]
		//    public const string ShouldShowDispatchPhoneNumber = "ShouldShowDispatchPhoneNumber";
		//    [XmlIgnore()]
		//    public const string ShouldShowQCPhoneNumber = "ShouldShowQCPhoneNumber";
		//    [XmlIgnore()]
		//    public const string DriverHandle = "DriverHandle";
		//    [XmlIgnore()]
		//    public const string DriverNumber = "DriverNumber";
		//    [XmlIgnore()]
		//    public const string DriverEmpireCLSPhoneNumber = "DriverEmpireCLSPhoneNumber";
		//    [XmlIgnore()]
		//    public const string CorporationName = "CorporationName";
		//    [XmlIgnore()]
		//    public const string VipStatus = "VipStatus";
		//    [XmlIgnore()]
		//    public const string TimeStamp = "TimeStamp";
		//    [XmlIgnore()]
		//    public const string NetJetGroupingNumber = "NetJetGroupingNumber";
		//    [XmlIgnore()]
		//    public const string HasAdditionalPassengers = "HasAdditionalPassengers";
		//    [XmlIgnore()]
		//    public const string PromotionID = "PromotionID";
		//    [XmlIgnore()]
		//    public const string PromotionDisclaimerText = "PromotionDisclaimerText";
		//    [XmlIgnore()]
		//    public const string PromotionName = "PromotionName";
		//    [XmlIgnore()]
		//    public const string PromotionDiscountPercent = "PromotionDiscountPercent";
		//    [XmlIgnore()]
		//    public const string PromotionFlatDiscount = "PromotionFlatDiscount";
		//    [XmlIgnore()]
		//    public const string FullRate = "FullRate";
		//    [XmlIgnore()]
		//    public const string MiscExp = "MiscExp";
		//    [XmlIgnore()]
		//    public const string WaitingExp = "WaitingExp";
		//    [XmlIgnore()]
		//    public const string PhoneExp = "PhoneExp";
		//    [XmlIgnore()]
		//    public const string HourlyFuelSurcharge = "HourlyFuelSurcharge";
		//    [XmlIgnore()]
		//    public const string QuoteAmount = "QuoteAmount";
		//    [XmlIgnore()]
		//    public const string BookedWithPayCode = "BookedWithPayCode";
		//    [XmlIgnore()]
		//    public const string MainPaymentPayCode = "MainPaymentPayCode";
		//    [XmlIgnore()]
		//    public const string MainPaymentCCSurcharge = "MainPaymentCCSurcharge";
		//    [XmlIgnore()]
		//    public const string MainPaymentTolls = "MainPaymentTolls";
		//    [XmlIgnore()]
		//    public const string MainPaymentTips = "MainPaymentTips";
		//    [XmlIgnore()]
		//    public const string MainPaymentRate = "MainPaymentRate";
		//    [XmlIgnore()]
		//    public const string MainPaymentGross = "MainPaymentGross";
		//    [XmlIgnore()]
		//    public const string SplitPayment1PayCode = "SplitPayment1PayCode";
		//    [XmlIgnore()]
		//    public const string SplitPayment1CCSurcharge = "SplitPayment1CCSurcharge";
		//    [XmlIgnore()]
		//    public const string SplitPayment1Tolls = "SplitPayment1Tolls";
		//    [XmlIgnore()]
		//    public const string SplitPayment1Tips = "SplitPayment1Tips";
		//    [XmlIgnore()]
		//    public const string SplitPayment1Rate = "SplitPayment1Rate";
		//    [XmlIgnore()]
		//    public const string SplitPayment1Gross = "SplitPayment1Gross";
		//    [XmlIgnore()]
		//    public const string SplitPayment2PayCode = "SplitPayment2PayCode";
		//    [XmlIgnore()]
		//    public const string SplitPayment2CCSurcharge = "SplitPayment2CCSurcharge";
		//    [XmlIgnore()]
		//    public const string SplitPayment2Tolls = "SplitPayment2Tolls";
		//    [XmlIgnore()]
		//    public const string SplitPayment2Tips = "SplitPayment2Tips";
		//    [XmlIgnore()]
		//    public const string SplitPayment2Rate = "SplitPayment2Rate";
		//    [XmlIgnore()]
		//    public const string SplitPayment2Gross = "SplitPayment2Gross";
		//    [XmlIgnore()]
		//    public const string CancelledDateTime = "CancelledDateTime";
		//}
	}

	/// <summary>
	/// List of Search criteria to be used to return a list of trip summaries
	/// </summary>
	[Serializable]
	public class TripListCriteria
	{

		private bool _ReturnCountOnly;

		/// <summary>
		/// Indicates record count should only be returned not data
		/// </summary>
		public bool ReturnCountOnly {
			get { return _ReturnCountOnly; }
			set {
				_ReturnCountOnly = value;
			}
		}

		public bool ReturnTripsWithReceiptsOnly { get; set; }

		private int _PageSize;

		/// <summary>
		/// Amount of records per page
		/// </summary>
		public int PageSize {
			get { return _PageSize; }
			set {
				_PageSize = value;
			}
		}

		private int _CurrentPage;

		/// <summary>
		/// Current Page to be displayed
		/// </summary>
		public int CurrentPage {
			get { return _CurrentPage; }
			set {
				_CurrentPage = value;
			}
		}

		private string _StartDate;

		/// <summary>
		/// 
		/// </summary>
		public string StartDate {
			get { return _StartDate; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_StartDate = value;
				} else {
					_StartDate = value.Trim ();
				}
			}
		}

		private string _EndDate;

		/// <summary>
		/// 
		/// </summary>
		public string EndDate {
			get { return _EndDate; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_EndDate = value;
				} else {
					_EndDate = value.Trim ();
				}
			}
		}

		private string _TripStatusCodes;

		/// <summary>
		/// A comma delimited list of TripStatusCode constant
		/// </summary>
		public string TripStatusCodes {
			get { return _TripStatusCodes; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_TripStatusCodes = value;
				} else {
					_TripStatusCodes = value.Trim ();
				}
			}
		}

        
		private string _TripNumber;

		/// <summary>
		/// 
		/// </summary>
		public string TripNumber {
			get { return _TripNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_TripNumber = value;
				} else {
					_TripNumber = value.Trim ();
				}
			}
		}

		private string _StartTime;

		/// <summary>
		/// 
		/// </summary>
		public string StartTime {
			get { return _StartTime; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_StartTime = value;
				} else {
					_StartTime = value.Trim ();
				}
			}
		}

		private string _EndTime;

		/// <summary>
		/// 
		/// </summary>
		public string EndTime {
			get { return _EndTime; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_EndTime = value;
				} else {
					_EndTime = value.Trim ();
				}
			}
		}

	}

    
	/// <summary>
	/// List of Trips for a customer
	/// </summary>
	public partial class TripList : ApiBaseModel
	{
		public TripList ()
		{
			this.Results = new List<TripListItem> ();
		}

		[XmlArrayItem ("Item", typeof(TripListItem))]
		public List<TripListItem> Results { get; set; }

		//  [XmlArrayItem("Item", typeof(TripListItem))]
		// public List<TripListItem> Items { get; set; }
	}

	/// <summary>
	/// Summary Information for a trip
	/// </summary>
	[Serializable]
	public class TripListItem
	{
		private string _Confirmation;

		/// <summary>
		/// 
		/// </summary>
		public string Confirmation {
			get { return _Confirmation; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Confirmation = value;
				} else {
					_Confirmation = value.Trim ();
				}
			}
		}

		private string _Time;

		/// <summary>
		/// 
		/// </summary>
		public string Time {
			get { return _Time; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Time = value;
				} else {
					_Time = value.Trim ();
				}
			}
		}

		private string _Date;

		/// <summary>
		/// 
		/// </summary>
		public string Date {
			get { return _Date; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Date = value;
				} else {
					_Date = value.Trim ();
				}
			}
		}

		public DateTime TripDateTime {
			get {
				return DateTime.Parse (String.Format ("{0} {1}", Date, Time));
			}
		}

		private string _PickupLocation;

		/// <summary>
		/// 
		/// </summary>
		public string PickupLocation {
			get { return _PickupLocation; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupLocation = value;
				} else {
					_PickupLocation = value.Trim ();
				}
			}
		}

		private string _DropOffLocation;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffLocation {
			get { return _DropOffLocation; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffLocation = value;
				} else {
					_DropOffLocation = value.Trim ();
				}
			}
		}

		private int _TripStatus;

		/// <summary>
		/// Code based on TripStatusCode constant: Unknown = 0,Open = 1,Completed = 4,NoShowed = 6,Canceled = 7
		/// </summary>
		public int TripStatus {
			get { return _TripStatus; }
			set {
				_TripStatus = value;
			}
		}

		private bool _IsReceiptReady;

		/// <summary>
		/// 
		/// </summary>
		public bool IsReceiptReady {
			get { return _IsReceiptReady; }
			set {
				_IsReceiptReady = value;
			}
		}

		private bool _IsTripComplete;

		/// <summary>
		/// 
		/// </summary>
		public bool IsTripComplete {
			get { return _IsTripComplete; }
			set {
				_IsTripComplete = value;
			}
		}

		private bool _IsTripCanBeModify;

		/// <summary>
		/// 
		/// </summary>
		public bool IsTripCanBeModify {
			get { return _IsTripCanBeModify; }
			set {
				_IsTripCanBeModify = value;
			}
		}

		private bool _IsTripCanBeCancel;

		/// <summary>
		/// 
		/// </summary>
		public bool IsTripCanBeCancel {
			get { return _IsTripCanBeCancel; }
			set {
				_IsTripCanBeCancel = value;
			}
		}
	}

	[Serializable]
	public partial class TripReceipt : ApiBaseModel
	{
		public TripReceipt ()
		{
		}

		public string HtmlReceipt { get; set; }

	}

	public partial class TripDetailAdditionalStop
	{
		public TripDetailAdditionalStop ()
		{
			//	this.Person = new PersonInfo();
			this.PassengerName = "";
			this.Address = new LocationInfo ();
		}

		public int DisplayIndex { get; set; }
		//public PersonInfo Person { get; set; }
		public string PassengerName { get; set; }

		public LocationInfo Address { get; set; }
	}

	[Serializable]
	public class SpecialBillingRequirement
	{
		public SpecialBillingRequirement ()
		{
		}

		public int DisplayIndex { get; set; }

		private int MinSizeInt {
			get {
				if (String.IsNullOrEmpty (MinSize))
					return 0;

				Int16 result;
				if (Int16.TryParse (MinSize, out result))
					return result;
				else
					return 0; 


			}
		}

		public string Prompt { get; set; }

		public string Value { get; set; }

		public string DefaultValue { get; set; }

		public string MinSize { get; set; }

		public string MaxSize { get; set; }

		public bool IsRequired {
			get {
				return MinSizeInt > 0;
			}
		}
	}

	/// <summary>
	/// Trip detail.
	/// This partial class provides expanded funtionality to the WebsiteAPI.TripDetail model.  This model
	/// allows use to use some more mobile friendly technology.
	/// </summary>
	public partial class TripDetail
	{


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

	public class TripPriceItems
	{
		public TripPriceItems ()
		{
			this.Items = new List<TripPriceItem> ();
		}

		public void Add (TripPriceItem tripPriceItem)
		{
			if (tripPriceItem.Amount != null)
				this.Items.Add (tripPriceItem);
		}

		public void Add (string priceDescription, string amount)
		{
			if (amount != null)
				this.Items.Add (new TripPriceItem (priceDescription, amount));
		}

		public List<TripPriceItem> Items { get; set; }
	}


}
