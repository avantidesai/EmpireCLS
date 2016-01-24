using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EmpireCLS
{
	public class Profile
	{
		public Profile ()
		{
		}
	}

	public class AccountLogonInfo
	{
		public static class Constants
		{
			public const string GuestUserName = "Guest";
			public const string GuestPassword = "ImaGuest";
		}

		[XmlAttribute]
		public string UserName { get; set; }

		[XmlAttribute]
		public string Password { get; set; }

		[XmlAttribute]
		public bool RememberMe { get; set; }

		[XmlIgnore]
		public bool IsGuest { get { return !this.IsEmpty && this.UserName == Constants.GuestUserName; } }

		[XmlIgnore]
		public bool IsEmpty { get { return string.IsNullOrWhiteSpace (this.UserName); } }

		public static AccountLogonInfo Empty { get { return new AccountLogonInfo (); } }

		public AccountLogonInfo Clone (Func<AccountLogonInfo, AccountLogonInfo> cloneFunc)
		{
			return cloneFunc (this);
		}

		public static AccountLogonInfo Guest { get { return new AccountLogonInfo () {
				UserName = Constants.GuestUserName,
				Password = Constants.GuestPassword,
				RememberMe = true
			}; } }
	}

	public partial class AccountUserInfo
	{
		[XmlAttribute ()]
		public string UserName { get; set; }

		[XmlAttribute ()]
		public bool IsGuest { get; set; }

		[XmlIgnore ()]        
		public string Password { get; set; }

		[XmlIgnore]
		public string PasswordConfirm { get; set; }



		public static AccountUserInfo Empty { get { return new AccountUserInfo (); } }

	}

	public partial class UserRoles : ApiBaseModel
	{
		private List<string> _UserRoles;

		/// <summary>
		/// 
		/// </summary>
		public List<string> Roles {
			get { return _UserRoles; }
			set {
				_UserRoles = value;
			}
		}

		private string _CorporationName;

		/// <summary>
		/// 
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
	}

	public partial class Promotion : ApiBaseModel
	{
		private int _ID;

		/// <summary>
		/// Promotion ID
		/// </summary>
		public int ID {
			get { return _ID; }
			set {
				_ID = value;
			}
		}

		private string _Code;

		/// <summary>
		/// Promotional/Coupon Code
		/// </summary>
		public string Code {
			get { return _Code; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Code = value;
				} else {
					_Code = value.Trim ();
				}
			}
		}

		private string _Name;

		/// <summary>
		/// Promotion Friendly Text to be displayed to user
		/// </summary>
		public string Name {
			get { return _Name; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Name = value;
				} else {
					_Name = value.Trim ();
				}
			}
		}


	}

	public partial class AddressList : ApiBaseModel
	{
		public AddressList ()
		{
			this.Results = new List<Address> ();
		}

		[XmlArrayItem ("Address", typeof(Address))]
		public List<Address> Results { get; set; }

	}

	public partial class Address
	{

		private string _formattedAddress;
		private string _LocationNumber;

		/// <summary>
		/// 
		/// </summary>
		public string LocationNumber {
			get { return _LocationNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_LocationNumber = value;
				} else {
					_LocationNumber = value.Trim ();
				}
			}
		}

		private string _LocationDescription;

		/// <summary>
		/// 
		/// </summary>
		public string LocationDescription {
			get { return _LocationDescription; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_LocationDescription = value;
				} else {
					_LocationDescription = value.Trim ();
				}
			}
		}

		private string _LocationAddress;

		/// <summary>
		/// 
		/// </summary>
		public string LocationAddress {
			get { return _LocationAddress; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_LocationAddress = value;
				} else {
					_LocationAddress = value.Trim ();
				}
			}
		}

		private string _LocationAddress2;

		/// <summary>
		/// 
		/// </summary>
		public string LocationAddress2 {
			get { return _LocationAddress2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_LocationAddress2 = value;
				} else {
					_LocationAddress2 = value.Trim ();
				}
			}
		}

		private string _LocationCity;

		/// <summary>
		/// 
		/// </summary>
		public string LocationCity {
			get { return _LocationCity; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_LocationCity = value;
				} else {
					_LocationCity = value.Trim ();
				}
			}
		}

		private string _LocationState;

		/// <summary>
		/// 
		/// </summary>
		public string LocationState {
			get { return _LocationState; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_LocationState = value;
				} else {
					_LocationState = value.Trim ();
				}
			}
		}

		private string _LocationZip;

		/// <summary>
		/// 
		/// </summary>
		public string LocationZip {
			get { return _LocationZip; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_LocationZip = value;
				} else {
					_LocationZip = value.Trim ();
				}
			}
		}

		private string _LocationCountry;

		/// <summary>
		/// 
		/// </summary>
		public string LocationCountry {
			get { return _LocationCountry; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_LocationCountry = value;
				} else {
					_LocationCountry = value.Trim ();
				}
			}
		}

		private string _LocationPhone;

		/// <summary>
		/// 
		/// </summary>
		public string LocationPhone {
			get { return _LocationPhone; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_LocationPhone = value;
				} else {
					_LocationPhone = value.Trim ();
				}
			}
		}

		private bool _IsPrimary;

		/// <summary>
		/// 
		/// </summary>
		public bool IsPrimary {
			get { return _IsPrimary; }
			set {
				_IsPrimary = value;
			}
		}

		private string _Directions;

		/// <summary>
		/// Special directions for the address
		/// </summary>
		public string Directions {
			get { return _Directions; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Directions = value;
				} else {
					_Directions = value.Trim ();
				}
			}
		}

		[XmlIgnore ()]
		public string FormattedAddress {
			get {
				if (string.IsNullOrWhiteSpace (_formattedAddress))
					return string.Format ("{0}, {1}, {2} {3}", this.LocationAddress, this.LocationCity, this.LocationState, this.LocationZip);
				else
					return _formattedAddress;
			}
			set {
				_formattedAddress = value;
			}
		}

	}

	public partial class ProfileCreditCards : ApiBaseModel
	{
		public ProfileCreditCards ()
		{
			this.Results = new List<ProfileCreditCard> ();
		}

		public List<ProfileCreditCard> Results { get; set; }
	}

	public partial class ProfileCreditCardsDetailed : ApiBaseModel
	{
		public ProfileCreditCardsDetailed ()
		{
			this.Results = new List<CreditCard> ();
		}

		public List<CreditCard> Results { get; set; }
	}

	public partial class ProfileCreditCard
	{
		public bool IsDefault { get; set; }

		private string _CreditCardID;

		/// <summary>
		/// 
		/// </summary>
		public string CreditCardID {
			get { return _CreditCardID; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CreditCardID = value;
				} else {
					_CreditCardID = value.Trim ();
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

		private bool _HasBillingInfo;

		/// <summary>
		/// true if billing info is on file, false if it is not on file
		/// </summary>
		public bool HasBillingInfo {
			get { return _HasBillingInfo; }
			set {
				_HasBillingInfo = value;
			}
		}

		private bool _IsCorporationCard;

		/// <summary>
		/// true if card is from corporate profile, false if it is from the customer profile
		/// </summary>
		public bool IsCorporationCard {
			get { return _IsCorporationCard; }
			set {
				_IsCorporationCard = value;
			}
		}

	}

	/// <summary>
	/// Customer Profile
	/// </summary>
	public partial class CustomerProfile : ApiBaseModel
	{

		private string _WebLoginID;

		/// <summary>
		/// Used for registering, and also returned when a user profile is looked up, if the system profile has web access.
		/// </summary>
		public string WebLoginID {
			get { return _WebLoginID; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_WebLoginID = value;
				} else {
					_WebLoginID = value.Trim ();
				}
			}
		}

		private string _CustomerNumber;

		/// <summary>
		/// Identifier currently in system
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

		private string _Name;

		/// <summary>
		/// 
		/// </summary>
		public string Name {
			get { return _Name; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Name = value;
				} else {
					_Name = value.Trim ();
				}
			}
		}

		private string _Address;

		/// <summary>
		/// 
		/// </summary>
		public string Address {
			get { return _Address; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Address = value;
				} else {
					_Address = value.Trim ();
				}
			}
		}

		private string _Address2;

		/// <summary>
		/// 
		/// </summary>
		public string Address2 {
			get { return _Address2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Address2 = value;
				} else {
					_Address2 = value.Trim ();
				}
			}
		}

		private string _City;

		/// <summary>
		/// 
		/// </summary>
		public string City {
			get { return _City; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_City = value;
				} else {
					_City = value.Trim ();
				}
			}
		}

		private string _State;

		/// <summary>
		/// 
		/// </summary>
		public string State {
			get { return _State; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_State = value;
				} else {
					_State = value.Trim ();
				}
			}
		}

		private string _ZipCode;

		/// <summary>
		/// 
		/// </summary>
		public string ZipCode {
			get { return _ZipCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_ZipCode = value;
				} else {
					_ZipCode = value.Trim ();
				}
			}
		}

		private string _Country;

		/// <summary>
		/// 
		/// </summary>
		public string Country {
			get { return _Country; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Country = value;
				} else {
					_Country = value.Trim ();
				}
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

		private string _CorporationNumber1;

		/// <summary>
		/// 
		/// </summary>
		public string CorporationNumber1 {
			get { return _CorporationNumber1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CorporationNumber1 = value;
				} else {
					_CorporationNumber1 = value.Trim ();
				}
			}
		}

		private string _CorporationNumber2;

		/// <summary>
		/// 
		/// </summary>
		public string CorporationNumber2 {
			get { return _CorporationNumber2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CorporationNumber2 = value;
				} else {
					_CorporationNumber2 = value.Trim ();
				}
			}
		}

		private string _Directions;

		/// <summary>
		/// Special directions for the primary address
		/// </summary>
		public string Directions {
			get { return _Directions; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Directions = value;
				} else {
					_Directions = value.Trim ();
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

		private string _CellPhone;

		/// <summary>
		/// 
		/// </summary>
		public string CellPhone {
			get { return _CellPhone; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CellPhone = value;
				} else {
					_CellPhone = value.Trim ();
				}
			}
		}

		private string _WorkPhone;

		/// <summary>
		/// 
		/// </summary>
		public string WorkPhone {
			get { return _WorkPhone; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_WorkPhone = value;
				} else {
					_WorkPhone = value.Trim ();
				}
			}
		}

		private string _CBRphone;

		/// <summary>
		/// 
		/// </summary>
		public string CBRphone {
			get { return _CBRphone; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CBRphone = value;
				} else {
					_CBRphone = value.Trim ();
				}
			}
		}

		private string _SpecialInstructions;

		/// <summary>
		/// 
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

		private bool _HasOnlineAccount;

		/// <summary>
		/// 
		/// </summary>
		public bool HasOnlineAccount {
			get { return _HasOnlineAccount; }
			set {
				_HasOnlineAccount = value;
			}
		}

		private bool _IsSurveyOptOut;

		/// <summary>
		/// Flag determing if user has Opted out of receiving Survey email requests
		/// </summary>
		public bool IsSurveyOptOut {
			get { return _IsSurveyOptOut; }
			set {
				_IsSurveyOptOut = value;
			}
		}

		private string _PrimaryEmail;

		/// <summary>
		/// 
		/// </summary>
		public string PrimaryEmail {
			get { return _PrimaryEmail; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PrimaryEmail = value;
				} else {
					_PrimaryEmail = value.Trim ();
				}
			}
		}

		private bool _PrimaryEmailAutoReceiptEnabled;

		/// <summary>
		/// 
		/// </summary>
		public bool PrimaryEmailAutoReceiptEnabled {
			get { return _PrimaryEmailAutoReceiptEnabled; }
			set {
				_PrimaryEmailAutoReceiptEnabled = value;
			}
		}

		private string _SecondaryEmail;

		/// <summary>
		/// 
		/// </summary>
		public string SecondaryEmail {
			get { return _SecondaryEmail; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SecondaryEmail = value;
				} else {
					_SecondaryEmail = value.Trim ();
				}
			}
		}

		private bool _SecondaryEmailAutoReceiptEnabled;

		/// <summary>
		/// 
		/// </summary>
		public bool SecondaryEmailAutoReceiptEnabled {
			get { return _SecondaryEmailAutoReceiptEnabled; }
			set {
				_SecondaryEmailAutoReceiptEnabled = value;
			}
		}

		private string _AdminEmail;

		/// <summary>
		/// 
		/// </summary>
		public string AdminEmail {
			get { return _AdminEmail; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AdminEmail = value;
				} else {
					_AdminEmail = value.Trim ();
				}
			}
		}

		private bool _AdminEmailAutoReceiptEnabled;

		/// <summary>
		/// 
		/// </summary>
		public bool AdminEmailAutoReceiptEnabled {
			get { return _AdminEmailAutoReceiptEnabled; }
			set {
				_AdminEmailAutoReceiptEnabled = value;
			}
		}

		private string _AuxiliaryEmail1;

		/// <summary>
		/// 
		/// </summary>
		public string AuxiliaryEmail1 {
			get { return _AuxiliaryEmail1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AuxiliaryEmail1 = value;
				} else {
					_AuxiliaryEmail1 = value.Trim ();
				}
			}
		}

		private bool _AuxiliaryEmail1AutoReceiptEnabled;

		/// <summary>
		/// 
		/// </summary>
		public bool AuxiliaryEmail1AutoReceiptEnabled {
			get { return _AuxiliaryEmail1AutoReceiptEnabled; }
			set {
				_AuxiliaryEmail1AutoReceiptEnabled = value;
			}
		}

		private string _AuxiliaryEmail2;

		/// <summary>
		/// 
		/// </summary>
		public string AuxiliaryEmail2 {
			get { return _AuxiliaryEmail2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AuxiliaryEmail2 = value;
				} else {
					_AuxiliaryEmail2 = value.Trim ();
				}
			}
		}

		private bool _AuxiliaryEmail2AutoReceiptEnabled;

		/// <summary>
		/// 
		/// </summary>
		public bool AuxiliaryEmail2AutoReceiptEnabled {
			get { return _AuxiliaryEmail2AutoReceiptEnabled; }
			set {
				_AuxiliaryEmail2AutoReceiptEnabled = value;
			}
		}

		private string _PrimaryFaxNumber;

		/// <summary>
		/// 
		/// </summary>
		public string PrimaryFaxNumber {
			get { return _PrimaryFaxNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PrimaryFaxNumber = value;
				} else {
					_PrimaryFaxNumber = value.Trim ();
				}
			}
		}

		private bool _PrimaryFaxNumberAutoReceiptEnabled;

		/// <summary>
		/// 
		/// </summary>
		public bool PrimaryFaxNumberAutoReceiptEnabled {
			get { return _PrimaryFaxNumberAutoReceiptEnabled; }
			set {
				_PrimaryFaxNumberAutoReceiptEnabled = value;
			}
		}

		private int _RegistrationPromoId;

		/// <summary>
		/// This is used to determine if the reg was prompted by a promotion
		/// </summary>
		public int RegistrationPromoId {
			get { return _RegistrationPromoId; }
			set {
				_RegistrationPromoId = value;
			}
		}

		private string _RegistrationPassword;

		/// <summary>
		/// 
		/// </summary>
		public string RegistrationPassword {
			get { return _RegistrationPassword; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_RegistrationPassword = value;
				} else {
					_RegistrationPassword = value.Trim ();
				}
			}
		}

		private bool _RegistrationIsCorporate;

		/// <summary>
		/// 
		/// </summary>
		public bool RegistrationIsCorporate {
			get { return _RegistrationIsCorporate; }
			set {
				_RegistrationIsCorporate = value;
			}
		}

		private bool _RequestDirectBill;

		/// <summary>
		/// 
		/// </summary>
		public bool RequestDirectBill {
			get { return _RequestDirectBill; }
			set {
				_RequestDirectBill = value;
			}
		}

		private string _RegistrationDefaultCorporation;

		/// <summary>
		/// 
		/// </summary>
		public string RegistrationDefaultCorporation {
			get { return _RegistrationDefaultCorporation; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_RegistrationDefaultCorporation = value;
				} else {
					_RegistrationDefaultCorporation = value.Trim ();
				}
			}
		}

		private bool _IsDirectBillEnabled;

		/// <summary>
		/// If true, direct billing is enabled for the profile
		/// </summary>
		public bool IsDirectBillEnabled {
			get { return _IsDirectBillEnabled; }
			set {
				_IsDirectBillEnabled = value;
			}
		}

		private bool _IsDirectBillPending;

		/// <summary>
		/// If true, a request has been sent to enable direct bill
		/// </summary>
		public bool IsDirectBillPending {
			get { return _IsDirectBillPending; }
			set {
				_IsDirectBillPending = value;
			}
		}

		private bool _IsDriverInfoNotificationsEligible;

		/// <summary>
		/// If True, customer is allowed to opt in to receive Notifications
		/// </summary>
		public bool IsDriverInfoNotificationsEligible {
			get { return _IsDriverInfoNotificationsEligible; }
			set {
				_IsDriverInfoNotificationsEligible = value;
			}
		}

		private bool _IsDriverOnLocationNotificationsEligible;

		/// <summary>
		/// If True, customer is allowed to opt in to receive Notifications
		/// </summary>
		public bool IsDriverOnLocationNotificationsEligible {
			get { return _IsDriverOnLocationNotificationsEligible; }
			set {
				_IsDriverOnLocationNotificationsEligible = value;
			}
		}

		private bool _IsDriverInfoNotificationsEnabled;

		/// <summary>
		/// if True, the Customer Opted in to receive Driver Info, If false either opted out or never opted in
		/// </summary>
		public bool IsDriverInfoNotificationsEnabled {
			get { return _IsDriverInfoNotificationsEnabled; }
			set {
				_IsDriverInfoNotificationsEnabled = value;
			}
		}

		private bool _IsDriverOnLocationNotificationsEnabled;

		/// <summary>
		/// if True, the Customer Opted in to receive OnLocation, If false either opted out or never opted in
		/// </summary>
		public bool IsDriverOnLocationNotificationsEnabled {
			get { return _IsDriverOnLocationNotificationsEnabled; }
			set {
				_IsDriverOnLocationNotificationsEnabled = value;
			}
		}

		private string _NotificationsCell1;

		/// <summary>
		/// Cell to Send Notifications to
		/// </summary>
		public string NotificationsCell1 {
			get { return _NotificationsCell1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_NotificationsCell1 = value;
				} else {
					_NotificationsCell1 = value.Trim ();
				}
			}
		}

		private string _NotificationsCell2;

		/// <summary>
		/// Cell to Send Notifications to
		/// </summary>
		public string NotificationsCell2 {
			get { return _NotificationsCell2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_NotificationsCell2 = value;
				} else {
					_NotificationsCell2 = value.Trim ();
				}
			}
		}

		private string _NotificationsCell3;

		/// <summary>
		/// Cell to Send Notifications to
		/// </summary>
		public string NotificationsCell3 {
			get { return _NotificationsCell3; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_NotificationsCell3 = value;
				} else {
					_NotificationsCell3 = value.Trim ();
				}
			}
		}

		private string _NotificationsCell4;

		/// <summary>
		/// Cell to Send Notifications to
		/// </summary>
		public string NotificationsCell4 {
			get { return _NotificationsCell4; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_NotificationsCell4 = value;
				} else {
					_NotificationsCell4 = value.Trim ();
				}
			}
		}

		private string _NotificationsCell5;

		/// <summary>
		/// Cell to Send Notifications to
		/// </summary>
		public string NotificationsCell5 {
			get { return _NotificationsCell5; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_NotificationsCell5 = value;
				} else {
					_NotificationsCell5 = value.Trim ();
				}
			}
		}

		private bool _IsDirectBillEligible;

		/// <summary>
		/// If true, the customers corporation is available for direct billing
		/// </summary>
		public bool IsDirectBillEligible {
			get { return _IsDirectBillEligible; }
			set {
				_IsDirectBillEligible = value;
			}
		}

		public int DefaultPaymentType { get; set; }

		public static CustomerProfile Empty { get { return new CustomerProfile (); } }
	}

	public partial class CustomerPasswordChange : ApiBaseModel
	{

		private string _OldRegistrationPassword;

		/// <summary>
		/// 
		/// </summary>
		public string OldRegistrationPassword {
			get { return _OldRegistrationPassword; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_OldRegistrationPassword = value;
				} else {
					_OldRegistrationPassword = value.Trim ();
				}
			}
		}


		private string _NewRegistrationPassword;

		/// <summary>
		/// 
		/// </summary>
		public string NewRegistrationPassword {
			get { return _NewRegistrationPassword; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_NewRegistrationPassword = value;
				} else {
					_NewRegistrationPassword = value.Trim ();
				}
			}
		}
	}
}



