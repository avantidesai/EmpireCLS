using Foundation;
using UIKit;

namespace EmpireCLS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			return true;
		}

		public override void OnResignActivation (UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground (UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground (UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated (UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
			InvokeOnMainThread (delegate() {
				HandleStartup ();
			});
		}

		public override void WillTerminate (UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}

		private void HandleStartup ()
		{
			ECLSUIUtil.TryAction<AppDelegate> ("HandleStartup", () => {
				if (Reachability.InternetConnectionStatus () == Reachability.NetworkStatus.NotReachable) {
					//TODO:
					return;

				} else {
					RunStartupSequence ();
				}
			});
		}

		private void RunStartupSequence ()
		{
			ApplicationContext.Current.PerfStart ("RunStartupSequence");
			// TODO: DefaultSettingsCache.ApplicationVersionLocator = ECLSUIUtil.geta
			// TODO: LogContext.Device = new ec

			//SetupTheme ();
		}

		private void SetupTheme ()
		{
			ECLSUIUtil.TryAction<AppDelegate> ("SetupTheme", () => {
				var majorVersion = int.Parse (UIDevice.CurrentDevice.SystemVersion.Split ('.') [0]);
				UIBarButtonItem.Appearance.TintColor = UIColor.Green; // ThemeColors.NavButtonColor;
				UINavigationBar.Appearance.TintColor = UIColor.Green; // ThemeColors.NavButtonColor;
				UINavigationBar.Appearance.BackIndicatorImage = UIImage.FromBundle (@"NavBarBack.png");
				UINavigationBar.Appearance.BackIndicatorTransitionMaskImage = UIImage.FromBundle (@"NavBarBack.png");

				UITabBar.Appearance.TintColor = UIColor.Green; // ThemeColors.NavButtonColor;
				UITableView.Appearance.BackgroundColor = UIColor.Black;

				if (majorVersion < 7) {
					UIBarButtonItem.Appearance.TintColor = UIColor.Green; // ThemeColors.NavButtonColor;
					UINavigationBar.Appearance.TintColor = UIColor.Black;
					UINavigationBar.Appearance.BackgroundColor = UIColor.Black;
				}

				if (majorVersion >= 7) {


					UITabBar.Appearance.SelectedImageTintColor = UIColor.Green; // ThemeColors.NavButtonColor;
					//UITabBar.Appearance.col
					UITableView.Appearance.TintColor = UIColor.Black;
					UITableViewCell.Appearance.TintColor = UIColor.White;

					UITextField.AppearanceWhenContainedIn (typeof(UISearchBar)).TintColor = UIColor.Green; // ThemeColors.ActionItemColor;

					//TODO: this tint color doesn't change the textview font color.  The text color is currently set to white in the storyboard to accomodate
					//UITextView.AppearanceWhenContainedIn(typeof(AboutEmpireCLSController)).TintColor = UIColor.Green; // ThemeColors.ActionItemColor;
					UITableViewCell.Appearance.BackgroundColor = UIColor.Black;

					UIToolbar.Appearance.BarTintColor = UIColor.Green; // ThemeColors.BackgroundColor;
					UIToolbar.Appearance.TintColor = UIColor.Green; // ThemeColors.NavButtonColor;


					//setup color scheme
					UIApplication.SharedApplication.SetStatusBarStyle (UIStatusBarStyle.LightContent, false);
					UIButton.Appearance.SetTitleColor (UIColor.Green, UIControlState.Normal);

					UICollectionView.Appearance.BackgroundColor = UIColor.Black;

					UILabel.AppearanceWhenContainedIn (typeof(UIDatePicker)).TextColor = UIColor.Green; // ThemeColors.ActionItemColor;
					UILabel.AppearanceWhenContainedIn (typeof(UITableViewCell)).TextColor = UIColor.Green; // ThemeColors.ActionItemColor;
					// UILabel.AppearanceWhenContainedIn(typeof(UITableViewCell)).ShadowColor = UIColor.Clear;

					UILabel.AppearanceWhenContainedIn (typeof(UITextField)).TextColor = UIColor.Green; // ThemeColors.LabelColor;
					UILabel.AppearanceWhenContainedIn (typeof(UIButton)).BackgroundColor = UIColor.Clear;

					UINavigationBar.Appearance.SetBackgroundImage (UIImage.FromBundle (@"NavBar_Background.png"), UIBarMetrics.Default);
					UINavigationBar.Appearance.SetTitleTextAttributes (new UITextAttributes () {
						TextColor = UIColor.Green // ThemeColors.TitleColor//,   // Silver
						// TextShadowColor = UIColor.Clear

					});

					UINavigationBar.Appearance.BackgroundColor = UIColor.Black;
					UISearchBar.Appearance.BackgroundColor = UIColor.Black;
					UIToolbar.Appearance.BackgroundColor = UIColor.Green; // ThemeColors.BackgroundColor;

				}

			}
			
			);
		}
	}
}


