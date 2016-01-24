using System;
using System.Net;
using System.Json;

using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;


namespace EmpireCLS
{

	public class AccountClient : ApiClientBase
	{
		private static class UrlPaths
		{
			public const string RolesGet = "/api/Profile/Roles";
			public const string ProfileGet = "/api/Profile/Get";
			public const string ProfileUpdate = "/api/Profile/Update";
			public const string ProfileNew = "/api/Profile/New";
			public const string ProfileCreditCardsGet = "/api/ProfileCreditCard/Get";
			public const string ProfileCreditCardsDetailedGet = "/api/ProfileCreditCard/GetFull";
			public const string ProfileCreditCardsNew = "/api/ProfileCreditCard/New";
			public const string ProfileCreditCardsUpdate = "/api/ProfileCreditCard/Update";
			public const string ProfileCreditCardDelete = "/api/ProfileCreditCard/Delete";
			public const string PasswordUpdate = "/api/Credentials/ChangePassword";
			public const string AddressesGet = "/api/AddressBook/Addresses";
			public const string CorpAddressGet = "/api/Corporate/OfficeAddresses?CorpID={0}";
			public const string CorpRequirementsGet = "/api/Corporate/CorpRequirements?CorpID={0}";
			//   public const string CorpCreditCardsGet = "/api/Corporate/CorpCreditCards_GetForCurrentUser";
		}

		public AccountClient ()
			: base (Settings.Current.EmpireCLSHost)
		{
			//this.Token = ApplicationContext.Current.SessionToken;

			AddUrlPathModelTypeMapping (UrlPaths.RolesGet, typeof(UserRoles));			
			AddUrlPathModelTypeMapping (UrlPaths.ProfileGet, typeof(CustomerProfile));			
			AddUrlPathModelTypeMapping (UrlPaths.ProfileUpdate, typeof(CustomerProfile));			
			AddUrlPathModelTypeMapping (UrlPaths.ProfileNew, typeof(CustomerProfile));
			AddUrlPathModelTypeMapping (UrlPaths.ProfileCreditCardsGet, typeof(ProfileCreditCards));
			AddUrlPathModelTypeMapping (UrlPaths.ProfileCreditCardsDetailedGet, typeof(ProfileCreditCardsDetailed));
			AddUrlPathModelTypeMapping (UrlPaths.ProfileCreditCardsNew, typeof(CreditCard));
			AddUrlPathModelTypeMapping (UrlPaths.ProfileCreditCardsUpdate, typeof(CreditCard));
			AddUrlPathModelTypeMapping (UrlPaths.ProfileCreditCardDelete, typeof(string));
			AddUrlPathModelTypeMapping (UrlPaths.PasswordUpdate, typeof(CustomerPasswordChange));	
			AddUrlPathModelTypeMapping (UrlPaths.AddressesGet, typeof(AddressList));
			//  AddUrlPathModelTypeMapping(UrlPaths.CorpCreditCardsGet, typeof(CreditCard));		


		}

		private AccountLogonInfo _newLoginInfo;

		#region RolesGet

		public AccountClient RolesGet (bool async = false)
		{
			return InvokeStrategy<AccountClient> (() => {
				if (!async)
					Get (UrlPaths.RolesGet);
				else
					GetAsync (UrlPaths.RolesGet);
			});		
		}

		/*	public AccountClient RolesGet(bool async = false)
		{
			return InvokeStrategy<AccountClient>(()=>{
				if (async) {
					if (GetAsync(UrlPaths.RolesGet, RolesGetCompleted).HasErrors)
						return;
				} else {
					if (Get (UrlPaths.RolesGet).HasErrors)
						return;
					RolesGetCompleted();
				}
					
			});	
		}
		*/
		public UserRoles GetRoleseResult { get { return this.Model as UserRoles; } }

		public UserRoles RolesGetResult { get; private set; }


		#endregion

		#region ProfileNew

		public AccountClient ProfileNew (CustomerProfile profile, bool async = false)
		{
			/*profile.IsSurveyOptOut = !profile.IsSurveyOptOut;
			profile.Name = string.Format ("{0},{1}", profile.LastName, profile.FirstName);
			return InvokeStrategy<AccountClient> (() => {
				if (async) {
					if (base.PostObjectAsync (UrlPaths.ProfileNew, profile, this.ProfileNewCompleted).HasErrors)
						return;
				} else {
					if (base.PostObject (UrlPaths.ProfileNew, profile).HasErrors)
						return;
					ProfileNewCompleted ();
				}
			});*/
			return null;
		}

		public CustomerProfile ProfileNewResult { get; private set; }

		private void ProfileNewCompleted ()
		{
			if (this.HasErrors)
				return;
			this.ProfileNewResult = this.Model as CustomerProfile;
			ApplicationContext.Current.CurrentUser.Person = this.ProfileNewResult;
			LogContext.Current.Log<AccountClient> ("ProfileNewCompleted");
			NotifyCompleted ();
		}

		#endregion

		#region ProfileGet

		public AccountClient ProfileGet (bool async = false)
		{
			return InvokeStrategy<AccountClient> (() => {

				if (!async)
					Get (UrlPaths.ProfileGet);
				else
					GetAsync (UrlPaths.ProfileGet);
			});			

		}

		public CustomerProfile GetProfileResult { 
			get {
				CustomerProfile result = this.Model as CustomerProfile;
				/*if (result.Name.Contains (",")) {
					int firstCommaPosition = result.Name.IndexOf (",");
					result.LastName = result.Name.Substring (0, firstCommaPosition).Trim ();
                   
					result.FirstName = result.Name.Substring (firstCommaPosition + 1).Trim ();
                   
					// Atlas stores names in all uppercase, proper case it for nicer reading
					result.FirstName = result.FirstName.ToProperCase ();

				} else {
					result.FirstName = result.Name.ToProperCase ();
				}*/
				return result;     
			}
		}

		//	public CustomerProfile ProfileGetResult { get; private set; }

		private void ProfileGetCompleted ()
		{
			if (this.HasErrors)
				return;

			LogContext.Current.Log<AccountClient> ("ProfileGetCompleted");

		}


		private void NotifyCompleted ()
		{
			Utilities.TryMobileAction<AccountClient> ("NotifyCompleted", () => {
				if (this.ClientCompleted != null)
					this.ClientCompleted (this);
			});
					
		}

		#endregion

		#region "Addresses"

		public AccountClient GetAddresses (bool async = false)
		{
			return InvokeStrategy<AccountClient> (() => {
				if (!async)
					Get (UrlPaths.AddressesGet);
				else
					GetAsync (UrlPaths.AddressesGet);
			});		
		}


		public AddressList GetAddressesResult { get { return this.Model as AddressList; } }
		//public AddressList AddressesGetResult { get; private set; }

		public AccountClient GetCorpAddresses (string corpNumber, bool async = false)
		{
			if (corpNumber.Trim () == "")
				return null;

			string urlPath = string.Format (UrlPaths.CorpAddressGet, corpNumber);
			AddUrlPathModelTypeMapping (urlPath, typeof(AddressList));

			return InvokeStrategy<AccountClient> (() => {
				if (!async)
					Get (urlPath);
				else
					GetAsync (urlPath); //, ()=> GetTripCompleted());
			});		
			//return InvokeStrategy<TripClient>(()=> {
			//if (base.PostObjectAsync(urlPath, BookingEntry.GetTripDetail(), GetTripCompleted).HasErrors)
			//	return;
			//});			
		}

		public AddressList GetCorpAddressesResult { get { return this.Model as AddressList; } }

		#endregion "Addresses"

		#region "CorpRequirements"

		public AccountClient GetCorpRequirements (string corpNumber, bool async = false)
		{
			if (corpNumber.Trim () == "")
				return null;

			string urlPath = string.Format (UrlPaths.CorpRequirementsGet, corpNumber);
			AddUrlPathModelTypeMapping (urlPath, typeof(CorporateRequirements));

			return InvokeStrategy<AccountClient> (() => {
				if (!async) {
					Get (urlPath);
					GetCorpRequirementsCompleted ();
				} else {
					GetAsync (urlPath, GetCorpRequirementsCompleted); //, ()=> GetTripCompleted());
				}
			});		
					
		}

		public CorporateRequirements GetCorpRequirementsResult {
			get {
				return this.Model as CorporateRequirements;
			}
		}

		private void GetCorpRequirementsCompleted ()
		{
        
			AssignSpecialBillingRequirements ();
		}

		List<SpecialBillingRequirement> _specialBillingRequirements = new List<SpecialBillingRequirement> ();

		private void AssignSpecialBillingRequirements ()
		{
			CorporateRequirements corpReq = GetCorpRequirementsResult;
			Type corpReqType = corpReq.GetType ();
			var properties = corpReqType.GetProperties ().Where (p => p.DeclaringType == typeof(CorporateRequirements));
			int fieldIndex;
			int fieldIndexPosStart;
          
			foreach (PropertyInfo propertyInfo in properties.Where(pr => pr.Name.ToLower().StartsWith("specialbillingprompt"))) {
				if (propertyInfo.CanRead) {
					object specialBillingPromptValue = propertyInfo.GetValue (corpReq, null);
					/*if (specialBillingPromptValue != null && !specialBillingPromptValue.ToString ().IsEmpty ()) {
						// find the relative meta data that goes with this billing (could be up to 20 special billings)
						fieldIndexPosStart = propertyInfo.Name.NumericsBegin ();
						if (fieldIndexPosStart > 0) {
							if (int.TryParse (propertyInfo.Name.Substring (fieldIndexPosStart), out fieldIndex)) {

								// get the min value (note, if greater than 1, then it's a required field)
								string minValue = GetPropertyValue (corpReq, "specialbillingminsize" + fieldIndex);
								// get the max value
								string maxValue = GetPropertyValue (corpReq, "specialbillingmaxsize" + fieldIndex);
								// get the default value
								string defaultValue = GetPropertyValue (corpReq, "specialbillingdefaultvalue" + fieldIndex);

								_specialBillingRequirements.Add (new SpecialBillingRequirement {
									Prompt = specialBillingPromptValue.ToString (),
									DisplayIndex = fieldIndex,
									DefaultValue = defaultValue,
									MaxSize = maxValue,
									MinSize = minValue

								});
							}

						}

					}*/
				}
			}

          
		}

		private string GetPropertyValue (CorporateRequirements corpReq, string propertyName)
		{
			string result = "";

			Type corpReqType = corpReq.GetType ();
			var properties = corpReqType.GetProperties ().Where (p => p.DeclaringType == typeof(CorporateRequirements));
			PropertyInfo propertyInfo = properties.Where (pr => pr.Name.ToLower ().Equals (propertyName)).FirstOrDefault ();
			if (propertyInfo != null && propertyInfo.CanRead && propertyInfo != null) {
				object value = propertyInfo.GetValue (corpReq, null);
				if (value != null)
					result = value.ToString ();
			}
			return result;
		}

		public IEnumerable<SpecialBillingRequirement> GetCorpSpecialBillingResult {
			get {

				return _specialBillingRequirements;
			}
		}

		#endregion "CorpRequirements"

		#region Profile Update

		public AccountClient ProfileUpdate (CustomerProfile profile, bool async = false)
		{
			/*profile.Name = string.Format ("{0},{1}", profile.LastName, profile.FirstName);
			profile.IsSurveyOptOut = !profile.IsSurveyOptOut;

			// see if login cred's changed
			AccountLogonInfo existing = ApplicationContext.Current.RememberedUser;
			if (existing != null) {
				if (!existing.IsEmpty) {
					// since an existing user cannot change their userID, all we have to worry about is if their password changes
					if (existing.Password != profile.RegistrationPassword && profile.RegistrationPassword != null) {
						// need to update remembered user for persisting to cache
						this._newLoginInfo = new AccountLogonInfo () {
							UserName = existing.UserName,
							RememberMe = true,
							Password = profile.RegistrationPassword

						};
					}

				}
			}*/

			return InvokeStrategy<AccountClient> (() => {
				if (async) {
					if (base.PostObjectAsync (UrlPaths.ProfileUpdate, profile, this.ProfileUpdateCompleted).HasErrors)
						return;
				} else {
					if (base.PostObject (UrlPaths.ProfileUpdate, profile).HasErrors)
						return;

					ProfileUpdateCompleted ();
				}
			});
		}

		public CustomerProfile ProfileUpdateResult { get; private set; }

		private void UpdateCredentialCache (AccountLogonInfo accountLoginInfo)
		{

			UserContext.Current.User = UserContext.Current.User = new AccountUserInfo () {
				UserName = accountLoginInfo.UserName,
				IsGuest = accountLoginInfo.IsGuest
			};
			//  if (ApplicationContext.Current.WasUserCached)
			//  {
			//      accountLoginInfo.RememberMe = true;
              
			//  }
			ApplicationContext.Current.RememberedUser = accountLoginInfo;
		}

		private void ChangePassword (CustomerProfile profile, bool async = false)
		{
			/*if (profile.OldRegistrationPassword.IsEmpty () || profile.RegistrationPassword.IsEmpty ())
				return;

			ChangePassword (new CustomerPasswordChange {
				OldRegistrationPassword = profile.OldRegistrationPassword,
				NewRegistrationPassword = profile.RegistrationPassword 
			}, async
			);*/
		}

		private void ChangePassword (CustomerPasswordChange customerPasswordChange, bool async = false)
		{
			/*if (customerPasswordChange.OldRegistrationPassword.IsEmpty () || customerPasswordChange.NewRegistrationPassword.IsEmpty ())
				return;


			//TODO : implement async 
			if (base.PostObject (UrlPaths.PasswordUpdate, customerPasswordChange).HasErrors)
				return;*/
		}

		private void ProfileUpdateCompleted ()
		{
			if (this.HasErrors)
				return;

			this.ProfileUpdateResult = this.Model as CustomerProfile;
			if (_newLoginInfo != null)
				UpdateCredentialCache (_newLoginInfo);
               
			ApplicationContext.Current.CurrentUser.Person = this.ProfileUpdateResult;
			// ChangePassword(this.ProfileUpdateResult, true);  // the profile update will also update the password, don't need to call separately
			LogContext.Current.Log<AccountClient> ("ProfileUpdateCompleted");
			NotifyCompleted ();
		}

		#endregion

		#region Credit Cards

		public async Task<AccountClient> ProfileDeleteCreditCardAsync (string creditCardId)
		{
			return await Task.Factory.StartNew (() => {
				return ProfileDeleteCreditCard (creditCardId);
			});
		}

		public AccountClient ProfileDeleteCreditCard (string creditCardId)
		{
			return InvokeStrategy<AccountClient> (() => {
				base.PutObject (UrlPaths.ProfileCreditCardDelete, creditCardId);
			});
		}

		#region Add/Create new credit cards

		public async Task ProfileAddNewCreditCardAsync (CreditCard creditCard)
		{
			await Task.Factory.StartNew (() => {
				ProfileAddNewCreditCard (creditCard);
			});
		}

		public void ProfileAddNewCreditCard (CreditCard creditCard)
		{
			base.PostObject (UrlPaths.ProfileCreditCardsNew, creditCard);
		}

		#endregion

		#region Edit/update existing credit cards

		public async Task ProfileUpdateCreditCardAsync (CreditCard creditCard)
		{
			await Task.Factory.StartNew (() => {
				ProfileUpdateCreditCard (creditCard);
			});
		}

		public void ProfileUpdateCreditCard (CreditCard creditCard)
		{
			InvokeStrategy<AccountClient> (() => {
				base.PutObject (UrlPaths.ProfileCreditCardsUpdate, creditCard);
			});
		}

		#endregion

		public ProfileCreditCardsDetailed GetProfileCreditCardsDetailedResult { get { return this.Model as ProfileCreditCardsDetailed; } }

		public AccountClient ProfileCreditCardsDetailedGet (bool async = false)
		{
			return InvokeStrategy<AccountClient> (() => {

				if (!async)
					Get (UrlPaths.ProfileCreditCardsDetailedGet);
				else
					GetAsync (UrlPaths.ProfileCreditCardsDetailedGet);
			});
		}

		private void ProfileCreditCardsDetailedGetCompleted ()
		{
			if (this.HasErrors)
				return;
			//this.ProfileCreditCardsGetResult = this.Model as ProfileCreditCards;

			LogContext.Current.Log<AccountClient> ("ProfileCreditCardsDetailedGetCompleted");
		}



		public AccountClient ProfileCreditCardsGet (bool async = false)
		{
			return InvokeStrategy<AccountClient> (() => {

				if (!async)
					Get (UrlPaths.ProfileCreditCardsGet);
				else
					GetAsync (UrlPaths.ProfileCreditCardsGet);
			});	
		}

		public ProfileCreditCards GetProfileCreditCardsResult { get { return this.Model as ProfileCreditCards; } }


		private void ProfileCreditCardsGetCompleted ()
		{
			if (this.HasErrors)
				return;
			//this.ProfileCreditCardsGetResult = this.Model as ProfileCreditCards;

			LogContext.Current.Log<AccountClient> ("ProfileCreditCardsGetCompleted");
		}

     


		/*	private void ProfileCreditCardsGetCompleted()
		{
			if (this.HasErrors)
				return;
			//this.RolesGetResult = this.Model as UserRoles;

			LogContext.Current.Log<AccountClient> ("ProfileCreditCardsGetCompleted");
		}
*/

		#endregion Credit Cards

		#region status

		/*
		public AccountClient Status()
		{
			return InvokeStrategy<AccountClient>(()=> {

				if (base.Get(UrlPaths.Status).HasErrors)
					return;
				this.StatusResult = this.Model as AccountStatusResult;
			});
		}
*/

		//	public AccountStatusResult StatusResult { get; private set; }

		#endregion status

	}
}

