using System;

using System.Json;

using EmpireCLS.Mobile.ApiClient;
using System.Reflection;
using System.Linq;
using Xamarin;

namespace EmpireCLS
{
	public class TokenClient : JsonWebClientBase
	{

		AccountLogonInfo _logonInfo;

		private static class UrlPaths
		{
			public const string Logon = "/api/token";
		}

		public TokenClient ()
			: base (Settings.Current.EmpireCLSHost)
		{
		}

		#region logon

		/// <summary>
		/// Validates users credentials for login, retrieves a token to 
		/// be used throughout the session
		/// </summary>
		/// <param name="logonInfo">Logon info. Contains username, password, rememberMe</param>
		public TokenClient Logon (AccountLogonInfo logonInfo, bool useAsync = false)
		{

			return InvokeStrategy<TokenClient> (() => {
				_logonInfo = logonInfo;
				string authInfo = Convert.ToBase64String (System.Text.Encoding.Default.GetBytes (
					                  logonInfo.UserName + ":" + logonInfo.Password
				                  ));

				if (useAsync) {
					var wc = base.GetAsync (UrlPaths.Logon, this.LogonCompleted, (whc) => {
						whc.Add ("Authorization", "Basic " + authInfo);
					});
					if (wc.HasErrors)
						return;				
				 
				} else {
					var wc = base.Get (UrlPaths.Logon, (whc) => {
						whc.Add ("Authorization", "Basic " + authInfo);
					});
					if (wc.HasErrors)
						return;		

					LogonCompleted ();
				}

			});					
		}

		private void LogonCompleted ()
		{
			string response = this.ResponseString;

			var json = JsonObject.Parse (response);
			var token = json ["access_token"].ToString ();

			int expiresIn = (int)double.Parse (json ["expires_in"].ToString ());
			var expiration = DateTime.UtcNow.AddSeconds (expiresIn);

			// store the token to Application 
			ApplicationContext.Current.SessionToken = token;		
			ApplicationContext.Current.SessionTokenExpiration = expiration;

			UserContext.Current.User = new AccountUserInfo (){ UserName = _logonInfo.UserName, IsGuest = _logonInfo.IsGuest };
            
			Insights.Identify (_logonInfo.UserName, "UserName", _logonInfo.UserName);
            
			LogContext.Current.Log<TokenClient> ("LogonCompleted:TokenRetrieved", token);

		}

	
		public static void GetTokenSilently (object sender, TokenRenewalEventArgs e)
		{
			TokenClient t = new TokenClient ();
			t.Logon (
				ApplicationContext.Current.WasUserCached
				? ApplicationContext.Current.RememberedUser 
				: AccountLogonInfo.Guest,
				false
			);		

			Type apiType = sender.GetType ();
			if (apiType.BaseType.ToString ().ToLower ().Contains ("apiclientbase")) {

			
				PropertyInfo tokenPropInfo = apiType.GetProperties ().Where (p => p.Name.ToLower () == e.TokenPropertyName.ToLower ()).First () as PropertyInfo;
				tokenPropInfo.SetValue (sender, ApplicationContext.Current.SessionToken);

				PropertyInfo tokenExpirationPropInfo = apiType.GetProperties ().Where (p => p.Name.ToLower () == e.TokenExpirationPropertyName.ToLower ()).First () as PropertyInfo;
				tokenExpirationPropInfo.SetValue (sender, ApplicationContext.Current.SessionTokenExpiration);

			}

			LogContext.Current.Log<TokenClient> ("GetTokenSilently:Token Renewed", ApplicationContext.Current.SessionToken);

		}

		#endregion
	}
}

