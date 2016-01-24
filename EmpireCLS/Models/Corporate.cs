using System;
using System.Collections.Generic;
using System.Linq;


namespace EmpireCLS
{
	public class CreditCard : ApiBaseModel
	{
        
		private string _UniqueID;

		/// <summary>
		/// Used for identifying the credit card when updating
		/// </summary>
		public string UniqueID {
			get { return _UniqueID; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_UniqueID = value;
				} else {
					_UniqueID = value.Trim ();
				}
			}
		}

		private string _CustomerNumber;

		/// <summary>
		/// Populated if this card is from a customer profile
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

		private string _CorporationNumber;

		/// <summary>
		/// Populated if this card is from a corporate profile
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

		private string _CardType;

		/// <summary>
		/// 
		/// </summary>
		public string CardType {
			get { return _CardType; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CardType = value;
				} else {
					_CardType = value.Trim ();
				}
			}
		}

		private string _CardSubType;

		/// <summary>
		/// 
		/// </summary>
		public string CardSubType {
			get { return _CardSubType; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CardSubType = value;
				} else {
					_CardSubType = value.Trim ();
				}
			}
		}

		private string _CardNumber;

		/// <summary>
		/// 
		/// </summary>
		public string CardNumber {
			get { return _CardNumber; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_CardNumber = value;
				} else {
					_CardNumber = value.Trim ();
				}
			}
		}

		private string _ExpMonth;

		/// <summary>
		/// 
		/// </summary>
		public string ExpMonth {
			get { return _ExpMonth; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_ExpMonth = value;
				} else {
					_ExpMonth = value.Trim ();
				}
			}
		}

		private string _ExpYear;

		/// <summary>
		/// 
		/// </summary>
		public string ExpYear {
			get { return _ExpYear; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_ExpYear = value;
				} else {
					_ExpYear = value.Trim ();
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

		private bool _HasBillingInfo;

		/// <summary>
		/// true if there is a billing address and security code on file, false if the billing address record does not exist
		/// </summary>
		public bool HasBillingInfo {
			get { return _HasBillingInfo; }
			set {
				_HasBillingInfo = value;
			}
		}

		private string _BillingName;

		/// <summary>
		/// 
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
	}

	public partial class CorporateCreditCards : ApiBaseModel
	{
		public CorporateCreditCards ()
		{
			this.Results = new List<CreditCard> ();
		}

		public List<CreditCard> Results { get; set; }
	}

	public class CorporateRequirements : ApiBaseModel
	{
		private bool _CostCenterDisplay;

		/// <summary>
		/// 
		/// </summary>
		public bool CostCenterDisplay {
			get { return _CostCenterDisplay; }
			set {
				_CostCenterDisplay = value;
			}
		}

		private bool _SpecialBillingDisplay;

		/// <summary>
		/// 
		/// </summary>
		public bool SpecialBillingDisplay {
			get { return _SpecialBillingDisplay; }
			set {
				_SpecialBillingDisplay = value;
			}
		}

		private bool _OpenForDirectBill;

		/// <summary>
		/// 
		/// </summary>
		public bool OpenForDirectBill {
			get { return _OpenForDirectBill; }
			set {
				_OpenForDirectBill = value;
			}
		}

		private bool _RequeredEnterPassword;

		/// <summary>
		/// 
		/// </summary>
		public bool RequeredEnterPassword {
			get { return _RequeredEnterPassword; }
			set {
				_RequeredEnterPassword = value;
			}
		}

		private bool _CostCenterRequired;

		/// <summary>
		/// 
		/// </summary>
		public bool CostCenterRequired {
			get { return _CostCenterRequired; }
			set {
				_CostCenterRequired = value;
			}
		}

		private bool _CostCenterSelect;

		/// <summary>
		/// 
		/// </summary>
		public bool CostCenterSelect {
			get { return _CostCenterSelect; }
			set {
				_CostCenterSelect = value;
			}
		}

		private List<string> _CostCentersList;

		/// <summary>
		/// 
		/// </summary>
		public List<string> CostCentersList {
			get { return _CostCentersList; }
			set {
				_CostCentersList = value;
			}
		}

		private string _SpecialBillingTitle;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingTitle {
			get { return _SpecialBillingTitle; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingTitle = value;
				} else {
					_SpecialBillingTitle = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt1;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt1 {
			get { return _SpecialBillingPrompt1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt1 = value;
				} else {
					_SpecialBillingPrompt1 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize1;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize1 {
			get { return _SpecialBillingMaxSize1; }
			set {
				_SpecialBillingMaxSize1 = value;
			}
		}

		private int _SpecialBillingMinSize1;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize1 {
			get { return _SpecialBillingMinSize1; }
			set {
				_SpecialBillingMinSize1 = value;
			}
		}

		private List<string> _SpecialBillingList1;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList1 {
			get { return _SpecialBillingList1; }
			set {
				_SpecialBillingList1 = value;
			}
		}

		private string _SpecialBillingDefaultValue1;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue1 {
			get { return _SpecialBillingDefaultValue1; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue1 = value;
				} else {
					_SpecialBillingDefaultValue1 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt2;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt2 {
			get { return _SpecialBillingPrompt2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt2 = value;
				} else {
					_SpecialBillingPrompt2 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize2;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize2 {
			get { return _SpecialBillingMaxSize2; }
			set {
				_SpecialBillingMaxSize2 = value;
			}
		}

		private int _SpecialBillingMinSize2;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize2 {
			get { return _SpecialBillingMinSize2; }
			set {
				_SpecialBillingMinSize2 = value;
			}
		}

		private List<string> _SpecialBillingList2;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList2 {
			get { return _SpecialBillingList2; }
			set {
				_SpecialBillingList2 = value;
			}
		}

		private string _SpecialBillingDefaultValue2;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue2 {
			get { return _SpecialBillingDefaultValue2; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue2 = value;
				} else {
					_SpecialBillingDefaultValue2 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt3;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt3 {
			get { return _SpecialBillingPrompt3; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt3 = value;
				} else {
					_SpecialBillingPrompt3 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize3;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize3 {
			get { return _SpecialBillingMaxSize3; }
			set {
				_SpecialBillingMaxSize3 = value;
			}
		}

		private int _SpecialBillingMinSize3;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize3 {
			get { return _SpecialBillingMinSize3; }
			set {
				_SpecialBillingMinSize3 = value;
			}
		}

		private List<string> _SpecialBillingList3;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList3 {
			get { return _SpecialBillingList3; }
			set {
				_SpecialBillingList3 = value;
			}
		}

		private string _SpecialBillingDefaultValue3;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue3 {
			get { return _SpecialBillingDefaultValue3; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue3 = value;
				} else {
					_SpecialBillingDefaultValue3 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt4;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt4 {
			get { return _SpecialBillingPrompt4; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt4 = value;
				} else {
					_SpecialBillingPrompt4 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize4;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize4 {
			get { return _SpecialBillingMaxSize4; }
			set {
				_SpecialBillingMaxSize4 = value;
			}
		}

		private int _SpecialBillingMinSize4;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize4 {
			get { return _SpecialBillingMinSize4; }
			set {
				_SpecialBillingMinSize4 = value;
			}
		}

		private List<string> _SpecialBillingList4;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList4 {
			get { return _SpecialBillingList4; }
			set {
				_SpecialBillingList4 = value;
			}
		}

		private string _SpecialBillingDefaultValue4;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue4 {
			get { return _SpecialBillingDefaultValue4; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue4 = value;
				} else {
					_SpecialBillingDefaultValue4 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt5;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt5 {
			get { return _SpecialBillingPrompt5; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt5 = value;
				} else {
					_SpecialBillingPrompt5 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize5;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize5 {
			get { return _SpecialBillingMaxSize5; }
			set {
				_SpecialBillingMaxSize5 = value;
			}
		}

		private int _SpecialBillingMinSize5;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize5 {
			get { return _SpecialBillingMinSize5; }
			set {
				_SpecialBillingMinSize5 = value;
			}
		}

		private List<string> _SpecialBillingList5;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList5 {
			get { return _SpecialBillingList5; }
			set {
				_SpecialBillingList5 = value;
			}
		}

		private string _SpecialBillingDefaultValue5;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue5 {
			get { return _SpecialBillingDefaultValue5; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue5 = value;
				} else {
					_SpecialBillingDefaultValue5 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt6;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt6 {
			get { return _SpecialBillingPrompt6; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt6 = value;
				} else {
					_SpecialBillingPrompt6 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize6;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize6 {
			get { return _SpecialBillingMaxSize6; }
			set {
				_SpecialBillingMaxSize6 = value;
			}
		}

		private int _SpecialBillingMinSize6;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize6 {
			get { return _SpecialBillingMinSize6; }
			set {
				_SpecialBillingMinSize6 = value;
			}
		}

		private List<string> _SpecialBillingList6;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList6 {
			get { return _SpecialBillingList6; }
			set {
				_SpecialBillingList6 = value;
			}
		}

		private string _SpecialBillingDefaultValue6;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue6 {
			get { return _SpecialBillingDefaultValue6; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue6 = value;
				} else {
					_SpecialBillingDefaultValue6 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt7;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt7 {
			get { return _SpecialBillingPrompt7; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt7 = value;
				} else {
					_SpecialBillingPrompt7 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize7;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize7 {
			get { return _SpecialBillingMaxSize7; }
			set {
				_SpecialBillingMaxSize7 = value;
			}
		}

		private int _SpecialBillingMinSize7;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize7 {
			get { return _SpecialBillingMinSize7; }
			set {
				_SpecialBillingMinSize7 = value;
			}
		}

		private List<string> _SpecialBillingList7;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList7 {
			get { return _SpecialBillingList7; }
			set {
				_SpecialBillingList7 = value;
			}
		}

		private string _SpecialBillingDefaultValue7;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue7 {
			get { return _SpecialBillingDefaultValue7; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue7 = value;
				} else {
					_SpecialBillingDefaultValue7 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt8;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt8 {
			get { return _SpecialBillingPrompt8; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt8 = value;
				} else {
					_SpecialBillingPrompt8 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize8;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize8 {
			get { return _SpecialBillingMaxSize8; }
			set {
				_SpecialBillingMaxSize8 = value;
			}
		}

		private int _SpecialBillingMinSize8;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize8 {
			get { return _SpecialBillingMinSize8; }
			set {
				_SpecialBillingMinSize8 = value;
			}
		}

		private List<string> _SpecialBillingList8;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList8 {
			get { return _SpecialBillingList8; }
			set {
				_SpecialBillingList8 = value;
			}
		}

		private string _SpecialBillingDefaultValue8;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue8 {
			get { return _SpecialBillingDefaultValue8; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue8 = value;
				} else {
					_SpecialBillingDefaultValue8 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt9;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt9 {
			get { return _SpecialBillingPrompt9; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt9 = value;
				} else {
					_SpecialBillingPrompt9 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize9;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize9 {
			get { return _SpecialBillingMaxSize9; }
			set {
				_SpecialBillingMaxSize9 = value;
			}
		}

		private int _SpecialBillingMinSize9;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize9 {
			get { return _SpecialBillingMinSize9; }
			set {
				_SpecialBillingMinSize9 = value;
			}
		}

		private List<string> _SpecialBillingList9;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList9 {
			get { return _SpecialBillingList9; }
			set {
				_SpecialBillingList9 = value;
			}
		}

		private string _SpecialBillingDefaultValue9;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue9 {
			get { return _SpecialBillingDefaultValue9; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue9 = value;
				} else {
					_SpecialBillingDefaultValue9 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt10;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt10 {
			get { return _SpecialBillingPrompt10; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt10 = value;
				} else {
					_SpecialBillingPrompt10 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize10;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize10 {
			get { return _SpecialBillingMaxSize10; }
			set {
				_SpecialBillingMaxSize10 = value;
			}
		}

		private int _SpecialBillingMinSize10;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize10 {
			get { return _SpecialBillingMinSize10; }
			set {
				_SpecialBillingMinSize10 = value;
			}
		}

		private List<string> _SpecialBillingList10;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList10 {
			get { return _SpecialBillingList10; }
			set {
				_SpecialBillingList10 = value;
			}
		}

		private string _SpecialBillingDefaultValue10;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue10 {
			get { return _SpecialBillingDefaultValue10; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue10 = value;
				} else {
					_SpecialBillingDefaultValue10 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt11;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt11 {
			get { return _SpecialBillingPrompt11; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt11 = value;
				} else {
					_SpecialBillingPrompt11 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize11;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize11 {
			get { return _SpecialBillingMaxSize11; }
			set {
				_SpecialBillingMaxSize11 = value;
			}
		}

		private int _SpecialBillingMinSize11;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize11 {
			get { return _SpecialBillingMinSize11; }
			set {
				_SpecialBillingMinSize11 = value;
			}
		}

		private List<string> _SpecialBillingList11;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList11 {
			get { return _SpecialBillingList11; }
			set {
				_SpecialBillingList11 = value;
			}
		}

		private string _SpecialBillingDefaultValue11;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue11 {
			get { return _SpecialBillingDefaultValue11; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue11 = value;
				} else {
					_SpecialBillingDefaultValue11 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt12;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt12 {
			get { return _SpecialBillingPrompt12; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt12 = value;
				} else {
					_SpecialBillingPrompt12 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize12;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize12 {
			get { return _SpecialBillingMaxSize12; }
			set {
				_SpecialBillingMaxSize12 = value;
			}
		}

		private int _SpecialBillingMinSize12;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize12 {
			get { return _SpecialBillingMinSize12; }
			set {
				_SpecialBillingMinSize12 = value;
			}
		}

		private List<string> _SpecialBillingList12;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList12 {
			get { return _SpecialBillingList12; }
			set {
				_SpecialBillingList12 = value;
			}
		}

		private string _SpecialBillingDefaultValue12;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue12 {
			get { return _SpecialBillingDefaultValue12; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue12 = value;
				} else {
					_SpecialBillingDefaultValue12 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt13;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt13 {
			get { return _SpecialBillingPrompt13; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt13 = value;
				} else {
					_SpecialBillingPrompt13 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize13;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize13 {
			get { return _SpecialBillingMaxSize13; }
			set {
				_SpecialBillingMaxSize13 = value;
			}
		}

		private int _SpecialBillingMinSize13;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize13 {
			get { return _SpecialBillingMinSize13; }
			set {
				_SpecialBillingMinSize13 = value;
			}
		}

		private List<string> _SpecialBillingList13;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList13 {
			get { return _SpecialBillingList13; }
			set {
				_SpecialBillingList13 = value;
			}
		}

		private string _SpecialBillingDefaultValue13;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue13 {
			get { return _SpecialBillingDefaultValue13; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue13 = value;
				} else {
					_SpecialBillingDefaultValue13 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt14;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt14 {
			get { return _SpecialBillingPrompt14; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt14 = value;
				} else {
					_SpecialBillingPrompt14 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize14;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize14 {
			get { return _SpecialBillingMaxSize14; }
			set {
				_SpecialBillingMaxSize14 = value;
			}
		}

		private int _SpecialBillingMinSize14;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize14 {
			get { return _SpecialBillingMinSize14; }
			set {
				_SpecialBillingMinSize14 = value;
			}
		}

		private List<string> _SpecialBillingList14;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList14 {
			get { return _SpecialBillingList14; }
			set {
				_SpecialBillingList14 = value;
			}
		}

		private string _SpecialBillingDefaultValue14;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue14 {
			get { return _SpecialBillingDefaultValue14; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue14 = value;
				} else {
					_SpecialBillingDefaultValue14 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt15;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt15 {
			get { return _SpecialBillingPrompt15; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt15 = value;
				} else {
					_SpecialBillingPrompt15 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize15;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize15 {
			get { return _SpecialBillingMaxSize15; }
			set {
				_SpecialBillingMaxSize15 = value;
			}
		}

		private int _SpecialBillingMinSize15;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize15 {
			get { return _SpecialBillingMinSize15; }
			set {
				_SpecialBillingMinSize15 = value;
			}
		}

		private List<string> _SpecialBillingList15;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList15 {
			get { return _SpecialBillingList15; }
			set {
				_SpecialBillingList15 = value;
			}
		}

		private string _SpecialBillingDefaultValue15;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue15 {
			get { return _SpecialBillingDefaultValue15; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue15 = value;
				} else {
					_SpecialBillingDefaultValue15 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt16;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt16 {
			get { return _SpecialBillingPrompt16; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt16 = value;
				} else {
					_SpecialBillingPrompt16 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize16;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize16 {
			get { return _SpecialBillingMaxSize16; }
			set {
				_SpecialBillingMaxSize16 = value;
			}
		}

		private int _SpecialBillingMinSize16;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize16 {
			get { return _SpecialBillingMinSize16; }
			set {
				_SpecialBillingMinSize16 = value;
			}
		}

		private List<string> _SpecialBillingList16;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList16 {
			get { return _SpecialBillingList16; }
			set {
				_SpecialBillingList16 = value;
			}
		}

		private string _SpecialBillingDefaultValue16;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue16 {
			get { return _SpecialBillingDefaultValue16; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue16 = value;
				} else {
					_SpecialBillingDefaultValue16 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt17;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt17 {
			get { return _SpecialBillingPrompt17; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt17 = value;
				} else {
					_SpecialBillingPrompt17 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize17;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize17 {
			get { return _SpecialBillingMaxSize17; }
			set {
				_SpecialBillingMaxSize17 = value;
			}
		}

		private int _SpecialBillingMinSize17;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize17 {
			get { return _SpecialBillingMinSize17; }
			set {
				_SpecialBillingMinSize17 = value;
			}
		}

		private List<string> _SpecialBillingList17;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList17 {
			get { return _SpecialBillingList17; }
			set {
				_SpecialBillingList17 = value;
			}
		}

		private string _SpecialBillingDefaultValue17;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue17 {
			get { return _SpecialBillingDefaultValue17; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue17 = value;
				} else {
					_SpecialBillingDefaultValue17 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt18;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt18 {
			get { return _SpecialBillingPrompt18; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt18 = value;
				} else {
					_SpecialBillingPrompt18 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize18;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize18 {
			get { return _SpecialBillingMaxSize18; }
			set {
				_SpecialBillingMaxSize18 = value;
			}
		}

		private int _SpecialBillingMinSize18;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize18 {
			get { return _SpecialBillingMinSize18; }
			set {
				_SpecialBillingMinSize18 = value;
			}
		}

		private List<string> _SpecialBillingList18;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList18 {
			get { return _SpecialBillingList18; }
			set {
				_SpecialBillingList18 = value;
			}
		}

		private string _SpecialBillingDefaultValue18;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue18 {
			get { return _SpecialBillingDefaultValue18; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue18 = value;
				} else {
					_SpecialBillingDefaultValue18 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt19;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt19 {
			get { return _SpecialBillingPrompt19; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt19 = value;
				} else {
					_SpecialBillingPrompt19 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize19;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize19 {
			get { return _SpecialBillingMaxSize19; }
			set {
				_SpecialBillingMaxSize19 = value;
			}
		}

		private int _SpecialBillingMinSize19;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize19 {
			get { return _SpecialBillingMinSize19; }
			set {
				_SpecialBillingMinSize19 = value;
			}
		}

		private List<string> _SpecialBillingList19;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList19 {
			get { return _SpecialBillingList19; }
			set {
				_SpecialBillingList19 = value;
			}
		}

		private string _SpecialBillingDefaultValue19;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue19 {
			get { return _SpecialBillingDefaultValue19; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue19 = value;
				} else {
					_SpecialBillingDefaultValue19 = value.Trim ();
				}
			}
		}

		private string _SpecialBillingPrompt20;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingPrompt20 {
			get { return _SpecialBillingPrompt20; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingPrompt20 = value;
				} else {
					_SpecialBillingPrompt20 = value.Trim ();
				}
			}
		}

		private int _SpecialBillingMaxSize20;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMaxSize20 {
			get { return _SpecialBillingMaxSize20; }
			set {
				_SpecialBillingMaxSize20 = value;
			}
		}

		private int _SpecialBillingMinSize20;

		/// <summary>
		/// 
		/// </summary>
		public int SpecialBillingMinSize20 {
			get { return _SpecialBillingMinSize20; }
			set {
				_SpecialBillingMinSize20 = value;
			}
		}

		private List<string> _SpecialBillingList20;

		/// <summary>
		/// 
		/// </summary>
		public List<string> SpecialBillingList20 {
			get { return _SpecialBillingList20; }
			set {
				_SpecialBillingList20 = value;
			}
		}

		private string _SpecialBillingDefaultValue20;

		/// <summary>
		/// 
		/// </summary>
		public string SpecialBillingDefaultValue20 {
			get { return _SpecialBillingDefaultValue20; }
			set {
				if (string.IsNullOrEmpty (value)) {
					_SpecialBillingDefaultValue20 = value;
				} else {
					_SpecialBillingDefaultValue20 = value.Trim ();
				}
			}
		}

		private bool _CanBypassRealTimeVerification;

		/// <summary>
		/// If true this corporation does not require real time verification - defaults to false which will require real time verification and billing info
		/// </summary>
		public bool CanBypassRealTimeVerification {
			get { return _CanBypassRealTimeVerification; }
			set {
				_CanBypassRealTimeVerification = value;
			}
		}
	}
}