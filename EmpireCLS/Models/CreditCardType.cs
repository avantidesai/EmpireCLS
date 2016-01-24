using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace EmpireCLS
{

	public class CreditCardTypeCollection
	{
		[XmlArrayItem ("CreditCardType", typeof(CacheItemCreditCardType))]
		public CacheItemCreditCardType[] CreditCardTypes { get; set; }
	}

	public class CacheItemCreditCardType
	{
		[XmlAttribute ()]
		public int ID { get; set; }

		[XmlAttribute ()]
		public string Code { get; set; }


		public string DisplayName { get; set; }

		public string IconName { get; set; }
	}

	public static class CreditCardUtilities
	{
		public static string GetCreditCardCodeByNumber (string ccnumber)
		{
			if (!String.IsNullOrWhiteSpace (ccnumber)) {
				Regex regVisa = new Regex ("^4[0-9]{12}(?:[0-9]{3})?$");
				Regex regMaster = new Regex ("^5[1-5][0-9]{14}$");
				Regex regExpress = new Regex ("^3[47][0-9]{13}$");
				Regex regDiners = new Regex ("^3(?:0[0-5]|[68][0-9])[0-9]{11}$");
				Regex regDiscover = new Regex ("^6(?:011|5[0-9]{2})[0-9]{12}$");
				//   Regex regJSB = new Regex("^(?:2131|1800|35\\d{3})\\d{11}$");

				if (regVisa.IsMatch (ccnumber))
					return "V";
				if (regMaster.IsMatch (ccnumber))
					return "M";
				if (regExpress.IsMatch (ccnumber))
					return "A";
				if (regDiners.IsMatch (ccnumber))
					return "C";
				if (regDiscover.IsMatch (ccnumber))
					return "D";
			}

			return String.Empty;
		}

		public static string GetCreditCardSubType (bool isPersonal)
		{
			return isPersonal ? "P" : "B";
		}
	}
}