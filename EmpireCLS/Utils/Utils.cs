using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;
using Xamarin;

namespace EmpireCLS
{
	public static class Utilities
	{
		public static string StripCreditCardNumber (string creditCardNumber)
		{
			var strippedCC = Regex.Replace (creditCardNumber, @"[^\d]", "");
			return strippedCC;

		}


		/// <summary>
		/// Returns whether or not an email address is formatted correctly.
		/// </summary>
		/// <param name="emailAddress"></param>
		/// <param name="isRequired"></param>
		/// <returns></returns>
		public static bool IsEmailAddressValid (string emailAddress, bool isRequired = false)
		{
			if (isRequired && String.IsNullOrEmpty (emailAddress))
				return false;

			Regex regex = new Regex (@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
			Match match = regex.Match (emailAddress);
			if (!match.Success)
				return false;


			return true;
		}

		public static void TryMobileAction<typeOfCaller> (string context, Action action, params object[] contextParams)
		{
			//TODO: Include a finally action
			Exception exception = null;
			try {
				action ();
			} catch (Exception ex) {
				exception = ex;

				LogContext.Current.Log<typeOfCaller> (context, ex, contextParams);
				if (LogContext.CrashReportEnabled && Settings.Current.EnableCrashReportExecution) {
					Insights.Report (ex);
				}

			}


		}


	}
}