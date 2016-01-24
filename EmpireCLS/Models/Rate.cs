using System;
using System.Collections.Generic;
using System.Linq;


namespace EmpireCLS
{
	/// <summary>
	/// Definition of trip to base Rates on
	/// </summary>
	[Serializable]
	public class RateDefinition
	{
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

		private string _PickupZip;

		/// <summary>
		/// 
		/// </summary>
		public string PickupZip {
			get { return _PickupZip; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PickupZip = value;
				} else {
					_PickupZip = value.Trim ();
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

		private string _DropOffZip;

		/// <summary>
		/// 
		/// </summary>
		public string DropOffZip {
			get { return _DropOffZip; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_DropOffZip = value;
				} else {
					_DropOffZip = value.Trim ();
				}
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

		private string _RequestedHours;

		/// <summary>
		/// When hourlyTrip flag is true, this will have a string with the number of hours e.g. 3, 3.5, 4, etc.
		/// </summary>
		public string RequestedHours {
			get { return _RequestedHours; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_RequestedHours = value;
				} else {
					_RequestedHours = value.Trim ();
				}
			}
		}
	}

	public class Rates : ApiBaseModel
	{
		public Rates ()
		{
			this.Results = new List<Rate> ();
		}

 
		public List<Rate> Results { get; set; }
	}

	/// <summary>
	/// Detailed Rate Information
	/// </summary>
	[Serializable]
	public partial class Rate
	{
		public string RateDescription { get; set; }

		private string _MasterVehicleID;

		/// <summary>
		/// Populated by the web in order to add a master fleet vehicle key
		/// </summary>
		public string MasterVehicleID {
			get { return _MasterVehicleID; }
			set {
				_MasterVehicleID = value;
			}
		}

		public string PreferedVehicleName { get; set; }


		private string _Description;

		public string Description {
			get { return _Description; }
			set {
				_Description = value;
			}
		}

		private string _VehicleName;

		public string VehicleName {
			get { return _VehicleName; }
			set {
				_VehicleName = value;
			}
		}

		private string _ImageName;

		public string ImageName {
			get { return _ImageName; }
			set {
				_ImageName = value;
			}
		}

		private string _ImagePath;

		public string ImagePath {
			get { return _ImagePath; }
			set {
				_ImagePath = value;
			}
		}

		private string _ThumbNailImagePath;

		public string ThumbNailImagePath {
			get { return _ThumbNailImagePath; }
			set {
				_ThumbNailImagePath = value;
			}
		}

		private string _MaxPassengerDescription;

		public string MaxPassengerDescription {
			get { return _MaxPassengerDescription; }
			set {
				_MaxPassengerDescription = value;
			}
		}

		private int _MaxPassengers;

		public int MaxPassengers {
			get { return _MaxPassengers; }
			set {
				_MaxPassengers = value;
			}
		}

		private string _LuggageCapacity;

		public string LuggageCapacity {
			get { return _LuggageCapacity; }
			set {
				_LuggageCapacity = value;
			}
		}

		private bool _IsCarTypeLinkedToMultipleVehicles;

		/// <summary>
		/// True if the car was linked to multiple vehicles in the master fleet for the quoted department
		/// </summary>
		public bool IsCarTypeLinkedToMultipleVehicles {
			get { return _IsCarTypeLinkedToMultipleVehicles; }
			set {
				_IsCarTypeLinkedToMultipleVehicles = value;
			}
		}

		private string _TripDate;

		/// <summary>
		/// 
		/// </summary>
		public string TripDate {
			get { return _TripDate; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_TripDate = value;
				} else {
					_TripDate = value.Trim ();
				}
			}
		}

		private string _TripTime;

		/// <summary>
		/// 
		/// </summary>
		public string TripTime {
			get { return _TripTime; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_TripTime = value;
				} else {
					_TripTime = value.Trim ();
				}
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

		private string _From;

		/// <summary>
		/// 
		/// </summary>
		public string From {
			get { return _From; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_From = value;
				} else {
					_From = value.Trim ();
				}
			}
		}

		private string _To;

		/// <summary>
		/// 
		/// </summary>
		public string To {
			get { return _To; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_To = value;
				} else {
					_To = value.Trim ();
				}
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

		private bool _CanBeBookdOnline;

		/// <summary>
		/// Flag to tell whether this trip/car combination can be booked online
		/// </summary>
		public bool CanBeBookdOnline {
			get { return _CanBeBookdOnline; }
			set {
				_CanBeBookdOnline = value;
			}
		}

		private bool _IsRatedHourly;

		/// <summary>
		/// True if the system rated the trip hourly, false if it is a fixed rate.  If true, the minimum hours and hourly rate will also be returned.
		/// </summary>
		public bool IsRatedHourly {
			get { return _IsRatedHourly; }
			set {
				_IsRatedHourly = value;
			}
		}

		private string _BilledHours;

		/// <summary>
		/// If the quote is hourly rated this will be populated with the amount of hours the were used to rate the trip.  This includes garage time and passenger travel time (sometimes min hours, sometimes explicit request for hourly trip num hours).
		/// </summary>
		public string BilledHours {
			get { return _BilledHours; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_BilledHours = value;
				} else {
					_BilledHours = value.Trim ();
				}
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
    
	}
}