using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Xml.Serialization;

namespace EmpireCLS
{
	public partial class Airport
	{
		// NOTE: Lat and Lng are not provided at this time; included properties to be used in the LocationPicker for hope of future availability
		//	[XmlAttribute]
		//	public double Lat { get; set; }
		//	[XmlAttribute]
		//	public double Lng { get; set; }

		public string MultiLineTitle {
			get {
				List<string> parts = new List<string> () {
					this.AirportCode, this.AirportName
				};

				return string.Join (Environment.NewLine, parts.ToArray ());
			}
		}
	}

	public class DefaultSettings : ApiBaseModel
	{
		private bool _isUpdateRequired;
		private int _usesUntilRatingPrompt;
		private bool _isDriverInfoNotificationEligible = false;
		private bool _isDriverOnLocationNotificationsEligible = false;

		public bool IsDriverInfoNotificationsEligible {
			get { return _isDriverInfoNotificationEligible; }
			set { _isDriverInfoNotificationEligible = value; }
		}

		public bool IsDriverOnLocationNotificationsEligible {
			get { return _isDriverOnLocationNotificationsEligible; }
			set { _isDriverOnLocationNotificationsEligible = value; }
		}

		public bool IsUpdateRequired {
			get { return _isUpdateRequired; }
			set { _isUpdateRequired = value; }
		}

		public int UsesUntilRatingPrompt {
			get { return _usesUntilRatingPrompt; }
			set { _usesUntilRatingPrompt = value; }
		}

		public string InviteFriendEmailMessage {
			get;
			set;
		}
	}

	public class iOSVersions
	{
		public List<iOSVersion> Versions { get; set; }

		public iOSVersions ()
		{
			Versions = new List<iOSVersion> ();
		}

		public bool IsUpdateRequired (string version)
		{
			var versionEntry = Versions.SingleOrDefault (s => s.VersionNumber == version);
			return versionEntry == null ? false : versionEntry.IsUpdateRequired;
		}

		public void Add (string version, bool isUpdateRequired)
		{
			if (version == null)
				throw new ArgumentNullException ("version");

			version = version.Trim ().ToUpper ();
			Remove (version);
			var iosVersion = new iOSVersion () { VersionNumber = version, IsUpdateRequired = isUpdateRequired };
			Versions.Add (iosVersion);
		}

		public void Remove (string version)
		{
			version = version.Trim ().ToUpper ();
			Versions.RemoveAll (i => i.VersionNumber == version);
		}

		public void Save (string filePath)
		{
			// TODO: Delete / overwrite original
			var serializer = new XmlSerializer (typeof(iOSVersions));
			using (var sw = new StreamWriter (filePath))
				serializer.Serialize (sw, this);
		}

		public static iOSVersions Load (string filePath)
		{
			iOSVersions iosVersionTable = new iOSVersions ();

			if (File.Exists (filePath)) {
				var serializer = new XmlSerializer (typeof(iOSVersions));
				using (var sr = new StreamReader (filePath)) {
					iosVersionTable = (iOSVersions)serializer.Deserialize (sr);
				}
			}

			return iosVersionTable;
		}
	}

	public class iOSVersion
	{
		public string VersionNumber { get; set; }

		public bool IsUpdateRequired { get; set; }
	}

	public class DepartmentToVehicleAssociation
	{
		private string _CarType;

		/// <summary>
		/// The car identifier used in the system for that department
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

		private List<string> _AssociatedVehicleDetailIDs;

		/// <summary>
		/// 
		/// </summary>
		public List<string> AssociatedVehicleDetailIDs {
			get { return _AssociatedVehicleDetailIDs; }
			set {
				_AssociatedVehicleDetailIDs = value;
			}
		}

	}

	public class DepartmentVehicle
	{

		private string _CarType;

		/// <summary>
		/// The car identifier used in the system for that department
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

		private string _Name;

		/// <summary>
		/// Name of the Car
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

		private string _Description;

		/// <summary>
		/// Description of car for the department
		/// </summary>
		public string Description {
			get { return _Description; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Description = value;
				} else {
					_Description = value.Trim ();
				}
			}
		}

		private int _MaxPassengers;

		/// <summary>
		/// Maximum number of passengers that the car allows
		/// </summary>
		public int MaxPassengers {
			get { return _MaxPassengers; }
			set {
				_MaxPassengers = value;
			}
		}

		private bool _IsNotACar;

		/// <summary>
		/// true if the listing is not a vehicle
		/// </summary>
		public bool IsNotACar {
			get { return _IsNotACar; }
			set {
				_IsNotACar = value;
			}
		}


	}

	public class CorpSplashPage
	{


		public string CorpImageFullPath { get; set; }


		private string _CorporationName;

		/// <summary>
		/// Corporation name to display
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

		private string _CorporationSplashText;

		/// <summary>
		/// XHTML to display on the corporations splash page
		/// </summary>
		public string CorporationSplashText {
			get { return _CorporationSplashText; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CorporationSplashText = value;
				} else {
					_CorporationSplashText = value.Trim ();
				}
			}
		}

		private string _CorporationImageName;

		/// <summary>
		/// Name of the image to use for a corporation. Example KPMGLogo.jpg
		/// </summary>
		public string CorporationImageName {
			get { return _CorporationImageName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CorporationImageName = value;
				} else {
					_CorporationImageName = value.Trim ();
				}
			}
		}

		private string _CorporationDirectory;

		/// <summary>
		/// folder for the corporation
		/// </summary>
		public string CorporationDirectory {
			get { return _CorporationDirectory; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CorporationDirectory = value;
				} else {
					_CorporationDirectory = value.Trim ();
				}
			}
		}
	}

	public class CacheState : ApiBaseModel
	{
		public List<State> Results { get; set; }

		public CacheState ()
		{
			this.Results = new List<State> ();
		}
	}

	public partial class CacheAirline : ApiBaseModel
	{
		public CacheAirline ()
		{
			this.Results = new List<Airline> ();
		}

		//public CacheId CacheId { get; set; }


		public int VersionNumber { get; set; }

		// [XmlArrayItem("Result", typeof(Airport))]
		public List<Airline> Results { get; set; }
	}

	public partial class CacheVehicle : ApiBaseModel
	{
		public CacheVehicle ()
		{
			this.Results = new List<Vehicle> ();
		}

		//public CacheId CacheId { get; set; }


		public int VersionNumber { get; set; }

		// [XmlArrayItem("Result", typeof(Airport))]
		public List<Vehicle> Results { get; set; }
	}

	public class Vehicle
	{

		public string RateQuoteImageNameFullPath { get; set; }

		private string _VehicleID;

		/// <summary>
		/// Unique Identifier used for storage
		/// </summary>
		public string VehicleID {
			get { return _VehicleID; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_VehicleID = value;
				} else {
					_VehicleID = value.Trim ();
				}
			}
		}

		private int _OrderNumber;

		/// <summary>
		/// Relative order of vehicle
		/// </summary>
		public int OrderNumber {
			get { return _OrderNumber; }
			set {
				_OrderNumber = value;
			}
		}

		private string _FleetPageExteriorImage;

		/// <summary>
		/// Image name including extension to be shown on Fleet Page
		/// </summary>
		public string FleetPageExteriorImage {
			get { return _FleetPageExteriorImage; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_FleetPageExteriorImage = value;
				} else {
					_FleetPageExteriorImage = value.Trim ();
				}
			}
		}

		private string _FleetPageInteriorImage;

		/// <summary>
		/// Interior Imagename including extension  to be shown on Fleet Page
		/// </summary>
		public string FleetPageInteriorImage {
			get { return _FleetPageInteriorImage; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_FleetPageInteriorImage = value;
				} else {
					_FleetPageInteriorImage = value.Trim ();
				}
			}
		}

		private string _FleetPageLargeImage;

		/// <summary>
		/// Large Image name including extension to be shown on Fleet Page
		/// </summary>
		public string FleetPageLargeImage {
			get { return _FleetPageLargeImage; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_FleetPageLargeImage = value;
				} else {
					_FleetPageLargeImage = value.Trim ();
				}
			}
		}

		private string _LuggageCapacity;

		/// <summary>
		/// XHML Luggage Capacity
		/// </summary>
		public string LuggageCapacity {
			get { return _LuggageCapacity; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_LuggageCapacity = value;
				} else {
					_LuggageCapacity = value.Trim ();
				}
			}
		}

		private string _PassengerCapacity;

		/// <summary>
		/// Passenger Capacity
		/// </summary>
		public string PassengerCapacity {
			get { return _PassengerCapacity; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PassengerCapacity = value;
				} else {
					_PassengerCapacity = value.Trim ();
				}
			}
		}

		private string _PreferredCarType;

		/// <summary>
		/// The preferred vehicle identifier used in the system
		/// </summary>
		public string PreferredCarType {
			get { return _PreferredCarType; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_PreferredCarType = value;
				} else {
					_PreferredCarType = value.Trim ();
				}
			}
		}

		private string _Name;

		/// <summary>
		/// The Name of the Car
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

		private string _Description;

		/// <summary>
		/// XHTML Vehicle description
		/// </summary>
		public string Description {
			get { return _Description; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_Description = value;
				} else {
					_Description = value.Trim ();
				}
			}
		}

		private string _RateQuoteImageName;

		/// <summary>
		/// Image name including extension rate quote
		/// </summary>
		public string RateQuoteImageName {
			get { return _RateQuoteImageName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_RateQuoteImageName = value;
				} else {
					_RateQuoteImageName = value.Trim ();
				}
			}
		}
	}

	[Serializable]
	public class Airline
	{
		public Airline ()
		{
		}

		public Airline (string code, string name)
		{
			Code = code;
			Name = name;
		}

		public string Code { get; set; }

		public string Name { get; set; }
	}

	public class State
	{
		private string _StateShortName;

		/// <summary>
		/// 
		/// </summary>
		public string StateShortName {
			get { return _StateShortName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_StateShortName = value;
				} else {
					_StateShortName = value.Trim ();
				}
			}
		}

		private string _StateLongName;

		/// <summary>
		/// 
		/// </summary>
		public string StateLongName {
			get { return _StateLongName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_StateLongName = value;
				} else {
					_StateLongName = value.Trim ();
				}
			}
		}
	}

	public partial class CacheAirport : ApiBaseModel
	{
		public CacheAirport ()
		{
			this.Results = new List<Airport> ();
		}

		//public CacheId CacheId { get; set; }


		public int VersionNumber { get; set; }

		// [XmlArrayItem("Result", typeof(Airport))]
		public List<Airport> Results { get; set; }
	}

	public partial class Airport
	{

		private string _AirportCode;

		/// <summary>
		/// 
		/// </summary>
		public string AirportCode {
			get { return _AirportCode; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AirportCode = value;
				} else {
					_AirportCode = value.Trim ();
				}
			}
		}

		private string _AirportName;

		/// <summary>
		/// 
		/// </summary>
		public string AirportName {
			get { return _AirportName; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_AirportName = value;
				} else {
					_AirportName = value.Trim ();
				}
			}
		}

		private bool _IsTrainStation;

		/// <summary>
		/// 
		/// </summary>
		public bool IsTrainStation {
			get { return _IsTrainStation; }
			set {
				_IsTrainStation = value;
			}
		}

		private bool _SupportsFBO;

		/// <summary>
		/// 
		/// </summary>
		public bool SupportsFBO {
			get {
				return _SupportsFBO;
			}
			set {
				_SupportsFBO = value;
			}
		}
	}

}