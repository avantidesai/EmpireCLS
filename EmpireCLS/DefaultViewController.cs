using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace EmpireCLS
{
	partial class DefaultViewController : UIViewController
	{
		public DefaultViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.btnLogin.TouchUpInside += (object sender, EventArgs e) => {
				HandleLogonTapped ();
			};

			this.btnSignUp.TouchUpInside += (object sender, EventArgs e) => {
				HandleSignUpTapped ();
			};


		}

		private readonly Action<UIViewController, bool> _completeAction;

		private void HandleLogonTapped ()
		{
			this.View.EndEditing (true);

			DefaultViewController.Logon (new AccountLogonInfo () {
				UserName = "ECLSIOS", // this.txtUsername.Text,
				Password = "ECLSIOS25", // this.txtPassword.Text, //
				RememberMe = true
			}, () => {
				ECLSUIUtil.TryAction<DefaultViewController> ("LoginChanged", () => {
					HandleLoginLogoutChanged ();
					ECLSUIUtil.TryAction<DefaultViewController> ("CompletedAction", () => _completeAction (this, true));
				});
			}
			);
		}

		public static void Logon (AccountLogonInfo logonInfo, Action loggedOn)
		{
			ECLSUIUtil.TryAction<DefaultViewController> ("Login", () => {
				new ECLSAsyncApiStatus<TokenClient> () {
					CompletedStrategy = (tokenClient) => {
						new ECLSAsyncApiStatus<UserContext> () {

							CompletedStrategy = (uc) => {
								uc.UserLoaded ();
								SyncDefaultSettings ();
								loggedOn ();
							}
						}.InvokeAsyncAction ("Loading Profile...", uc => {
							uc.LoadAsync ();
						});
						ApplicationContext.Current.RememberedUser = logonInfo;
					}
					
				}.InvokeAsyncAction ("Logging on...", (tokenclient) => {
					tokenclient.Logon (logonInfo, true);
				});
			});
		}

		private void HandleLoginLogoutChanged ()
		{
			if (this.NavigationItem.RightBarButtonItem != null)
				this.NavigationItem.RightBarButtonItem.Title = "test";
			if (ApplicationContext.Current.IsLoggedIn) {
				PopulateFromLoggedOnUser ();
				ReloadRoot ();
			}
			//	_bindingContext.Refresh();
			//	ReloadRoot();

		}

		public void ReloadRoot ()
		{
			//var controller = NewViewConytoller.GetInstance ("NewViewConytoller");
			// var controller = PickupDropController.GetInstance ("NewViewConytoller");
			//this.NavigationController.PushViewController ();
			//base.PrepareForSegue ();
			//this.NavigationController.PerformSegue (login,this);   //.NavigationController.PushViewController (controller, true);
			PerformSegue ("pickupsegue", this);
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			PickupDropController c = segue.DestinationViewController as PickupDropController;
			var navController = segue.DestinationViewController as UINavigationController;
			var detailController = navController.TopViewController as UIViewController;
		}

		private void PopulateFromLoggedOnUser ()
		{
			//TODO: pending
		}

		private void HandleSignUpTapped ()
		{
			
		}

		private static void SyncDefaultSettings ()
		{
			// Sync the default settings from the server
			// and perform appropriate actions on those variables post-sync.
			//ApplicationContext.Current.DefaultSettingsCache.Sync (iRateHelper.OnRateSettingsSynced);
		}
	}
}
