using System;
using System.Collections.Generic;
using System.Linq;


namespace EmpireCLS
{
	public class CreditCardVerfication
	{

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
	}
}