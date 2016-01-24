using System;
using CoreGraphics;
using System.Linq;
using System.Collections.Generic;

using UIKit;
using EmpireCLS.Mobile;

using System.Net;


namespace EmpireCLS
{

	public class ECLSAsyncApiStatus<apiClientType> : ECLSPopover
		where apiClientType : class, IWebClient, new()
	{
		private readonly apiClientType _apiClient;

	
		public ECLSAsyncApiStatus (ECLSPopoverStyle style = ECLSPopoverStyle.Activity)
			: base (UIApplication.SharedApplication.Delegate.GetWindow ().Frame.Height * .40f, style)
		{
			_apiClient = new apiClientType () {
				ClientCompleted = (c) => UIApplication.SharedApplication.InvokeOnMainThread (this.ClientCompleted)
			};			
		}

		public bool CheckConnectivity ()
		{
			if (Reachability.InternetConnectionStatus () == Reachability.NetworkStatus.NotReachable) {
				ECLSAlertView.Show ("Connection Error", "Internet connection not found.\nPlease try again later.", ECLSAlertViewType.Error);
				return false;				
			} 
			return true;
		}

		/// <summary>
		/// Invokes the async action.
		/// </summary>
		/// <param name='invokeStrategy'>
		/// Invoke strategy.
		/// </param>
		public ECLSAsyncApiStatus<apiClientType> InvokeAsyncAction (string title, Action<apiClientType> invokeStrategy, bool applyBackgroundColor = true, bool presentPopover = true)
		{				
			if (!this.CheckConnectivity ())
				return this;
		
			if (this.StartingStrategy != null)
				this.StartingStrategy (_apiClient);
			
			if (presentPopover)
				this.PresentPopover (title, true, applyBackgroundColor);

			invokeStrategy (_apiClient);
			
			if (_apiClient.HasErrors) {
				ClientCompleted ();
				return this;
			}
				
			if (this.StartedStrategy != null)
				this.StartedStrategy (_apiClient);
			
			return this;
		}

		/// <summary>
		/// Clients the completed.
		/// </summary>
		/// <param name='webClient'>
		/// Web client.
		/// </param>
		private void ClientCompleted ()
		{
			ECLSUIUtil.TryAction<ECLSAsyncApiStatus<apiClientType>> ("ClientCompleted", () => {
				this.DismissPopup ();
				
				// errors
				/*if (_apiClient.HasErrors) {
					string errorMessage = _apiClient.ErrorMessage;

					// Check for login errors and show custom message.
					var webException = _apiClient.LastError as WebException;
					if (webException != null) {
						var webResponse = webException.Response as HttpWebResponse;
						if (webResponse != null) {
							if (webResponse.StatusCode == HttpStatusCode.Unauthorized)
								errorMessage = "Invalid Username and/or Password";
						}
					}

					ECLSAlertView.Show ("Error", errorMessage, ECLSAlertViewType.Error);
                    
					ECLSUIUtil.TryAction<ECLSAsyncApiStatus<apiClientType>> ("ClientCompletedErr", () => {
						if (this.ErrorStrategy != null)
							this.ErrorStrategy (_apiClient);
					});
					
					
					return;
				}*/
				
				if (this.CompletedStrategy != null)
					this.CompletedStrategy (_apiClient);				
			});
			
		}

		/// <summary>
		/// Gets or sets the error strategy.
		/// </summary>
		/// <value>
		/// The error strategy.
		/// </value>
		public Action<apiClientType> ErrorStrategy { get; set; }

		/// <summary>
		/// Gets or sets the completed strategy.
		/// </summary>
		/// <value>
		/// The completed strategy.
		/// </value>
		public Action<apiClientType> CompletedStrategy { get; set; }

		/// <summary>
		/// Gets or sets the starting strategy.
		/// </summary>
		/// <value>
		/// The starting strategy.
		/// </value>
		public Action<apiClientType> StartingStrategy { get; set; }

		/// <summary>
		/// Gets or sets the started strategy.
		/// </summary>
		/// <value>
		/// The started strategy.
		/// </value>
		public Action<apiClientType> StartedStrategy { get; set; }
		
	}
}

