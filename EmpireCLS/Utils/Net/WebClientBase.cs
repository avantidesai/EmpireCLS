using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Xml.Linq;


namespace EmpireCLS
{
	public delegate void TokenRenewalHandler (object source, TokenRenewalEventArgs e);
	public class TokenRenewalEventArgs : EventArgs
	{
		public TokenRenewalEventArgs ()
		{

		}

		public string TokenPropertyName { get; set; }

		public string TokenExpirationPropertyName { get; set; }
	}

	public class WebClientBase : WebClient, IWebClient
	{
		private class AsyncUserState
		{
			public string UrlPath { get; set; }

			public Action AsyncCompletedStrategy { get; set; }
		}

		private bool _asyncProcessing = false;
		private ManualResetEvent _asyncCompleteEvent = new ManualResetEvent (false);
		
		private string _hostName = null;

		public string Token { get; set; }

		public DateTime TokenExpiration { get; set; }

		public event TokenRenewalHandler OnTokenExpired;

		public static string HttpProtocol { get; set; }

		public static Func<IDisposable> AutoReleasePoolFactory { get; set; }

		public static Func<string, string> ClientInfoHeaderValueStrategy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="hostName"></param>
		public WebClientBase (string hostName)
		{
			_hostName = hostName;
			
			SetClientInfoHeader (hostName);				
		}

		/// <summary>
		/// Sets the client info header.
		/// </summary>
		private void SetClientInfoHeader (string hostName)
		{
			try {
				if (WebClientBase.ClientInfoHeaderValueStrategy != null)
					base.Headers.Add ("ClientInfo", WebClientBase.ClientInfoHeaderValueStrategy (hostName));
			} catch (Exception ex) {
				LogContext.Current.Log<WebClientBase> ("SetClientInfoHeader.Error", ex);
			}
		}

		protected void CheckToken ()
		{
	
			if (TokenExpiration <= DateTime.UtcNow) {
				this.
				OnTokenExpired (this, new TokenRenewalEventArgs {
					TokenPropertyName = "Token",
					TokenExpirationPropertyName = "TokenExpiration"
				});

			}
		}

		protected typeOfClient InvokeStrategy<typeOfClient> (Action strategy)
			where typeOfClient : WebClientBase
		{
			try {
				strategy ();
			} catch (Exception ex) {
				this.Error = ex;
			}
			return this as typeOfClient;
		}

		protected virtual void SetupHeaders ()
		{
		}

		#region IWebclient

		public Action<IWebClient> ClientCompleted { get; set; }

		public virtual bool HasErrors {
			get { 
				return this.Error != null;
			} 
		}

		public virtual string ErrorMessage {
			get {
				if (this.Error != null)
					return this.Error.Message;
				else
					return "";
			}
		}

		public virtual Exception LastError { get { return this.Error; } }

		#endregion // IWebClient

		public byte[] ResponseData { get; private set; }

		public string ResponseString { get { return this.ResponseData != null ? System.Text.UTF8Encoding.UTF8.GetString (this.ResponseData) : ""; } }

		public Exception Error { get; protected set; }

		public string GetUrl { get; private set; }

		#region auto release pool control ** bug in framework

		protected override void OnDownloadStringCompleted (DownloadStringCompletedEventArgs args)
		{
			if (WebClientBase.AutoReleasePoolFactory != null) {
				using (var a = WebClientBase.AutoReleasePoolFactory ()) {
					base.OnDownloadStringCompleted (args);
				}
			} else {
				base.OnDownloadStringCompleted (args);
			}
		}

		protected override void OnDownloadDataCompleted (DownloadDataCompletedEventArgs args)
		{
			if (WebClientBase.AutoReleasePoolFactory != null) {
				using (var a = WebClientBase.AutoReleasePoolFactory ()) {
					base.OnDownloadDataCompleted (args);
				}
			} else {
				base.OnDownloadDataCompleted (args);
			}
		}

		protected override void OnUploadDataCompleted (UploadDataCompletedEventArgs args)
		{
			if (WebClientBase.AutoReleasePoolFactory != null) {
				using (var a = WebClientBase.AutoReleasePoolFactory ()) {
					base.OnUploadDataCompleted (args);
				}
			} else {
				base.OnUploadDataCompleted (args);
			}
		}

		#endregion

		#region get / post

		public virtual WebClientBase Get (string urlPath, Action<WebHeaderCollection> customHeadersFunc = null)
		{
			try {
				this.GetUrl = urlPath;	

				SetupHeaders ();

				if (customHeadersFunc != null)
					customHeadersFunc (this.Headers);
							
				this.ResponseData = DownloadData (
					string.Format ("{0}://{1}{2}", WebClientBase.HttpProtocol, _hostName, urlPath)
				);
			
			
				ParseResponse (
					urlPath
				);
			} catch (Exception ex) {
				this.Error = ex;
	
						
			}
			return this;
		}

		public virtual WebClientBase Post (string urlPath, string postString)
		{
			try {
				SetupHeaders ();

				this.ResponseData = null;
				this.ResponseData = UploadData (
					string.Format ("{0}://{1}{2}", "HTTPS", _hostName, urlPath),
					"POST", System.Text.UTF8Encoding.UTF8.GetBytes (postString)
				);

				ParseResponse (
					urlPath
				);
			} catch (Exception ex) {
				this.Error = ex;
			}
			return this;
		}

		public virtual WebClientBase Put (string urlPath, string putString)
		{
			try {
				SetupHeaders ();

				this.ResponseData = null;
				this.ResponseData = UploadData (
					string.Format ("{0}://{1}{2}", WebClientBase.HttpProtocol, _hostName, urlPath),
					"PUT", System.Text.UTF8Encoding.UTF8.GetBytes (putString)
				);

/*                ParseResponse(
                    urlPath
                );*/
			} catch (Exception ex) {
				this.Error = ex;
			}
			return this;
		}

		#endregion

		#region async get / post

		private void GetAsyncCompleted (object sender, DownloadDataCompletedEventArgs e)
		{
			this.DownloadDataCompleted -= GetAsyncCompleted;
			
			try {
				if (e.Error != null && e.Error.InnerException is ThreadInterruptedException) {
					this.AsyncProcessing = false;
				} else if (e.Error != null || e.Cancelled) {
					AsyncComplete (e.Cancelled, e.Error, null, e.UserState);
				} else {
					AsyncComplete (e.Cancelled, e.Error, e.Result, e.UserState);
				}
			} catch (Exception ex) {
				LogContext.Current.Log<WebClientBase> ("GetAsyncCompleted.Error", ex);
			}
		}

		public virtual WebClientBase GetAsync (string urlPath, Action asyncCompletedStrategy = null, Action<WebHeaderCollection> customeHeadersFunc = null)
		{
			try {
				if (this.AsyncProcessing) {
					this.Error = new ApplicationException ("Cannot start async operation while one is pending");
					return this;
				}
				
				this.DownloadDataCompleted += GetAsyncCompleted;

				SetupHeaders ();

				if (customeHeadersFunc != null)
					customeHeadersFunc (this.Headers);

				string url = string.Format ("{0}://{1}{2}", "HTTPS", _hostName, urlPath);
				
				this.GetUrl = urlPath;
				this.ResponseData = null;				
				DownloadDataAsync (
					new Uri (url),
					new AsyncUserState () { UrlPath = urlPath, AsyncCompletedStrategy = asyncCompletedStrategy }
				);
				
				AsyncStarted ();
			} catch (Exception ex) {
				this.Error = ex;
			}
			return this;				
		}

		private void PostAsyncCompleted (object sender, UploadDataCompletedEventArgs e)
		{
			this.UploadDataCompleted -= PostAsyncCompleted;			
			
			if (e.Error != null)
				AsyncComplete (e.Cancelled, e.Error, null, e.UserState);
			else
				AsyncComplete (e.Cancelled, e.Error, e.Result, e.UserState);			
		}

		public virtual WebClientBase PostAsync (string urlPath, string postData, Action asyncCompletedStrategy = null)
		{
			try {
				if (this.AsyncProcessing) {
					this.Error = new ApplicationException ("Cannot start async operation while one is pending");
					return this;
				}

				SetupHeaders ();
				
				this.UploadDataCompleted += PostAsyncCompleted;
				
				this.ResponseData = null;				
				UploadDataAsync (
					new Uri (string.Format ("{0}://{1}{2}", WebClientBase.HttpProtocol, _hostName, urlPath)),
					"POST", System.Text.UTF8Encoding.UTF8.GetBytes (postData), 
					new AsyncUserState () { UrlPath = urlPath, AsyncCompletedStrategy = asyncCompletedStrategy }
				);
		
				AsyncStarted ();
			} catch (Exception ex) {
				this.Error = ex;
			}
			return this;			
		}

		public bool AsyncProcessing {
			get {
				lock (this) {
					return _asyncProcessing;
				}
			}
			set {
				lock (this) { 
					_asyncProcessing = value; 					
				}
			}
		}

		public bool AsyncWait (int millisecondsTimeout)
		{
			return _asyncCompleteEvent.WaitOne (millisecondsTimeout);
		}

		private void AsyncStarted ()
		{
			_asyncCompleteEvent.Reset ();
			this.AsyncProcessing = true;
		}

		private void AsyncComplete (bool cancelled, Exception error, byte[] responseData, object userState)
		{
			if (cancelled || (error != null && error.InnerException is ThreadInterruptedException)) {
				this.AsyncProcessing = false;
				return;
			}
			
			this.Error = error;
			if (this.Error == null) {
				try {
					AsyncUserState asyncState = userState as AsyncUserState;
					
					this.ResponseData = responseData;
					ParseResponse (
						asyncState.UrlPath
					);
					
					if (asyncState.AsyncCompletedStrategy is Action)
						(asyncState.AsyncCompletedStrategy as Action) ();
				} catch (Exception ex) {
					this.Error = ex;
					LogContext.Current.Log<WebClientBase> ("AsyncComplete.TryBlock", ex);
				}
			} else {
				LogContext.Current.Log<WebClientBase> ("AsyncComplete.Error", this.Error);
			}
			
			this.AsyncProcessing = false;
			
			if (this.ClientCompleted != null)
				this.ClientCompleted (this);

			_asyncCompleteEvent.Set ();					
		}

		#endregion

		protected virtual void ParseResponse (string urlPath)
		{
		}

		protected virtual CookieContainer CookieContainer { get { return null; } }

		protected override WebRequest GetWebRequest (Uri address)
		{
			WebRequest request = base.GetWebRequest (address);

			HttpWebRequest httpRequest = request as HttpWebRequest;
			if (httpRequest == null)
				return request;
			
			httpRequest.CookieContainer = this.CookieContainer;			
			return request;
		}
	}
}

