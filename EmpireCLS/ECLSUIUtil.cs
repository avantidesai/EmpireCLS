using System;
using CoreGraphics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Collections.Generic;

using UIKit;
using Foundation;


using CoreAnimation;
using MonoTouch.Dialog;
using CoreText;
	
using System.Net;

using Xamarin;



namespace EmpireCLS
{

	public class ECLSUIUtil
	{

		/// <summary>
		/// old way to do static class so that log context works with type
		/// </summary>
		private ECLSUIUtil ()
		{
		}

		public static UIView OpaqueView {
			get {
				return new UIView () { Opaque = true };
			}
		}

		/*public static UIButton SectionHeaderViewButton (ECLSBindingContext bindingContext, string text, int tag, Action clickHandler, Func<bool> enabledHandler)
		{
			UIButton headerButton = ECLSUIUtil.StyleButton (new UIButton () {
				Frame = CGRect.FromLTRB (0, 0, 180, 20),
				Font = ECLSUIUtil.HelveticaNeueBold (10),
				Tag = tag

			}, UITextAlignment.Left);
			headerButton.SetTitle (text, UIControlState.Normal);
			headerButton.Enabled = enabledHandler ();
			bindingContext.Add (text, headerButton, clickHandler, enabledHandler);

			return headerButton;
		}
*/
		public static UIButton SectionHeaderViewButton (string text, int tag, Action clickHandler)
		{
			UIButton headerButton = ECLSUIUtil.StyleButton (new UIButton () {
				Frame = CGRect.FromLTRB (0, 0, 180, 20),
				Font = ECLSUIUtil.HelveticaNeueBold (10),
				Tag = tag
			}, UITextAlignment.Left);
			headerButton.SetTitle (text, UIControlState.Normal);
			headerButton.TouchUpInside += (sender, e) => {
				ECLSUIUtil.TryAction<ECLSUIUtil> ("TouchUpInside", clickHandler);
			};

			headerButton.Enabled = true;

			return headerButton;
		}

		private static UILabel SectionHeaderLabelStyled (UILabel label, string text, UIFont font)
		{
			label.Text = text;
			label.BackgroundColor = UIColor.Clear;
			label.Font = font;
			label.TextColor = UIColor.White;
			//label.ShadowColor = UIColor.Black;
			//label.ShadowOffset = new SizeF(0f, -1.0f);

			return label;
		}

		public static UIView SectionCellView (UITableViewCellStyle cellStyle, string text, string detailText)
		{
			UITableViewCell view = new UITableViewCell (cellStyle, "xx") {
				Frame = CGRect.FromLTRB (0, 0, 250, 45),
				BackgroundColor = UIColor.Clear
			};

			SectionHeaderLabelStyled (view.TextLabel, text, ECLSUIUtil.HelveticaNeueBold (16));

			SectionHeaderLabelStyled (view.DetailTextLabel, detailText, ECLSUIUtil.HelveticaNeueBoldItalic (10));

			return view;
		}

		public static UIView SectionCellView (string text, UIButton button = null)
		{
			UIView view = new UIView (CGRect.FromLTRB (0, 0, 0, 30)); // { BackgroundColor = UIColor.Clear };

			nfloat labelWidth = button == null ? UIScreen.MainScreen.Bounds.Width : 250;


			UILabel l = SectionHeaderLabelStyled (
				            new UILabel (CGRect.FromLTRB (15, 0, labelWidth, view.Bounds.Bottom)), text, ECLSUIUtil.HelveticaNeueBold (16)
			            );


			l.TextColor = UIColor.Blue;// .LabelColor;

			// 7/2/2014 TAS: Added logic to allow section label to autoshrinkg
			l.Lines = 0;
			l.AdjustsFontSizeToFitWidth = true;
			l.MinimumScaleFactor = .5f;

			CGSize size = LabelRequiredSize (l, l.Text);
			l.Frame = CGRect.FromLTRB (l.Frame.Left, l.Frame.Top, l.Frame.Left + size.Width, view.Bounds.Bottom);
			view.AddSubview (l);

			if (button != null) {

				button.Frame = CGRect.FromLTRB (
					l.Frame.Right + 10f, (l.Frame.Height / 2) - (button.Frame.Height / 2),
					l.Frame.Right + 10f + button.Frame.Width, (l.Frame.Height / 2) - (button.Frame.Height / 2) + button.Frame.Height
				);
				button.BackgroundColor = UIColor.Clear;

				view.AddSubview (button);
			}

			return view;
		}

		public static UIViewController LeafViewController (UIViewController root = null)
		{
			UIViewController displayController = root != null ? root
                : (from w in UIApplication.SharedApplication.Windows
			                where w.RootViewController != null
			                select w.RootViewController).FirstOrDefault ();

			if (displayController.PresentedViewController != null)
				return LeafViewController (displayController.PresentedViewController);
			else if (displayController.NavigationController != null)
				return LeafViewController (displayController.NavigationController.TopViewController);
			else
				return displayController;
		}

		/// <summary>
		/// Checks the color of the and set tint, verifying selector existing (IOS 6+)
		/// </summary>
		/// <param name='stepper'>
		/// Stepper.
		/// </param>
		/// <param name='color'>
		/// Color.
		/// </param>
		public static void CheckAndSetTintColor (UIStepper stepper, UIColor color)
		{
			if (stepper.RespondsToSelector (new ObjCRuntime.Selector ("setTintColor:"))) {
				//stepper.TintColor = color;
			}
		}

		/// <summary>
		/// Checks the and set attributed text, verifying selector existing (IOS 6+)
		/// </summary>
		/// <param name='label'>
		/// Label.
		/// </param>
		/// <param name='text'>
		/// Text.
		/// </param>
		/// <param name='stringAttributes'>
		/// String attributes.
		/// </param>
		public static void CheckAndSetAttributedText (UILabel label, string text, CTStringAttributes stringAttributes)
		{
			// IOS 6 check
			if (label.RespondsToSelector (new ObjCRuntime.Selector ("setAttributedText:"))) {
				label.AttributedText = new NSAttributedString (text, stringAttributes);
			} else {
				label.Text = text;
			}
		}

		public static UIButton StyleButton (UIButton buttonToStyle, UITextAlignment alignment = UITextAlignment.Center)
		{
			//buttonToStyle.TitleLabel.ShadowColor = UIColor.Black;
			//	buttonToStyle.TitleLabel.ShadowOffset = new SizeF(-0.0f, -1.0f);
			buttonToStyle.TintColor = UIColor.FromRGB (175, 1, 83);
			/*(buttonToStyle.SetBackgroundImage(
                UIImage.FromFile(@"Images/Orange_Button_Large.png").StretchableImage(6, 6), 
                UIControlState.Normal
            );*/
			buttonToStyle.SetTitleColor (UIColor.Blue, UIControlState.Normal);//ThemeColors.NavButtonColor, UIControlState.Normal);
			buttonToStyle.BackgroundColor = (UIColor.Blue); // ThemeColors.TitleColor;
			/*
            buttonToStyle.SetBackgroundImage(
                UIImage.FromFile(@"Images/Orange_Button_Large_Disabled.png").StretchableImage(6, 6), 
                UIControlState.Disabled
                );*/
			buttonToStyle.SetTitleColor (UIColor.LightGray, UIControlState.Disabled);
			buttonToStyle.TitleLabel.TextAlignment = alignment;

			return buttonToStyle;
		}


		public static UIButton StyledDetailDisclosure {
			get {
				UIButton btn = new UIButton (CGRect.FromLTRB (0, 0, 23, 24));
				btn.SetBackgroundImage (
					UIImage.FromFile (@"Images/Button_RoundArrow.png"),
					UIControlState.Normal
				);
				return btn;
			}
		}

		public static UIFont HelveticaNeueBold (float pointSize)
		{
			UIFont f = UIFont.FromName ("HelveticaNeue-Bold", pointSize);
			return f;
		}

		public static UIFont HelveticaNeueBoldItalic (float pointSize)
		{
			UIFont f = UIFont.FromName ("HelveticaNeue-BoldItalic", pointSize);
			return f;
		}

		/*	public static CAGradientLayer GreyGradient
            {
                get
                {
                    UIColor colorOne = UIColor.FromWhiteAlpha(0.9f, 1.0f);
                    UIColor colorTwo = UIColor.FromHSBA(0.625f, 0.0f, 0.85f, 1.0f);
                    UIColor colorThree = UIColor.FromHSBA(0.625f, 0.0f, 0.7f, 1.0f);
                    UIColor colorFour = UIColor.FromHSBA(0.625f, 0.0f, 0.4f, 1.0f);
				
                    NSNumber stopOne = NSNumber.FromFloat(0.0f);
                    NSNumber stopTwo = NSNumber.FromFloat(0.02f);
                    NSNumber stopThree = NSNumber.FromFloat(0.99f);
                    NSNumber stopFour = NSNumber.FromFloat(1.0f);
				
                    CAGradientLayer headerLayer = new CAGradientLayer();
                    headerLayer.Colors = new MonoTouch.CoreGraphics.CGColor[] {
                        colorOne.CGColor, colorTwo.CGColor, colorThree.CGColor, colorFour.CGColor
                    };
                    headerLayer.Locations = new NSNumber[] {
                        stopOne, stopTwo, stopThree, stopFour
                    };
                    return headerLayer;
                }
            }
            */
		/*public static CAGradientLayer BlueGradient
        {
            get
            {
                UIColor colorOne = UIColor.FromRGBA(120f/255.0f, 135f/255.0f, 150f/255.0f, 1.0f);
                UIColor colorTwo = UIColor.FromRGBA(57f/255.0f, 79f/255.0f, 96f/255.0f, 1.0f);
				
                NSNumber stopOne = NSNumber.FromFloat(0.0f);
                NSNumber stopTwo = NSNumber.FromFloat(0.1f);
				
                CAGradientLayer headerLayer = new CAGradientLayer();
                headerLayer.Colors = new MonoTouch.CoreGraphics.CGColor[] {
                    colorOne.CGColor, colorTwo.CGColor
                };
                headerLayer.Locations = new NSNumber[] {
                    stopOne, stopTwo
                };
                return headerLayer;
            }
        }*/

		/// <summary>
		/// Tries the func value.
		/// </summary>
		/// <param name='context'>
		/// Context.
		/// </param>
		/// <param name='func'>
		/// Func.
		/// </param>
		/// <param name='contextParams'>
		/// Context parameters.
		/// </param>
		/// <typeparam name='typeOfCaller'>
		/// The 1st type parameter.
		/// </typeparam>
		/// <typeparam name='typeOfReturn'>
		/// The 2nd type parameter.
		/// </typeparam>
		public static typeOfReturn TryFuncValue<typeOfCaller, typeOfReturn> (string context, Func<typeOfReturn> func, Func<typeOfReturn> funcError, params object[] contextParams)
            where typeOfReturn : struct
		{
			try {
				return func ();
			} catch (Exception ex) {
				ECLSAlertView.Show (context, ex.Message, ECLSAlertViewType.Error);
				LogContext.Current.Log<typeOfCaller> (context, ex, contextParams);
				if (LogContext.CrashReportEnabled && Settings.Current.EnableCrashReportExecution) {
					Insights.Report (ex);
				}


				return funcError ();
			}
		}

		/// <summary>
		/// Tries the func object.
		/// </summary>
		/// <param name='context'>
		/// Context.
		/// </param>
		/// <param name='func'>
		/// Func.
		/// </param>
		/// <param name='contextParams'>
		/// Context parameters.
		/// </param>
		/// <typeparam name='typeOfCaller'>
		/// The 1st type parameter.
		/// </typeparam>
		/// <typeparam name='typeOfReturn'>
		/// The 2nd type parameter.
		/// </typeparam>
		public static typeOfReturn TryFuncObject<typeOfCaller, typeOfReturn> (string context, Func<typeOfReturn> func, Func<typeOfReturn> funcError = null, params object[] contextParams)
            where typeOfReturn : class
		{
			try {
				return func ();
			} catch (Exception ex) {
				ECLSAlertView.Show (context, ex.Message, ECLSAlertViewType.Error);
				LogContext.Current.Log<typeOfCaller> (context, ex, contextParams);
				if (LogContext.CrashReportEnabled && Settings.Current.EnableCrashReportExecution) {
					Insights.Report (ex);
				}


				return funcError == null ? null : funcError ();
			}
		}

		/// <summary>
		/// Invokes the action.
		/// </summary>
		/// <param name='context'>
		/// Context.
		/// </param>
		/// <param name='actionToInvoke'>
		/// Action to invoke.
		/// </param>
		/// <typeparam name='typeOfCaller'>
		/// The 1st type parameter.
		/// </typeparam>
		public async static void TryAction<typeOfCaller> (string context, Action action, params object[] contextParams)
		{
			Exception exception = null;
			try {
				action ();
			} catch (Exception ex) {
				exception = ex;
				ECLSAlertView.Show (context, ex.Message, ECLSAlertViewType.Error);
				LogContext.Current.Log<typeOfCaller> (context, ex, contextParams);
				if (LogContext.CrashReportEnabled && Settings.Current.EnableCrashReportExecution) {
					Insights.Report (ex);
				}

			}

			// Add some global custom extra data

			// Log the exception async
			if (exception != null) {
				if (LogContext.CrashReportEnabled && Settings.Current.EnableCrashReportExecution) {
					Insights.Report (exception);
				}



			}
			// Examine the raw ServerResponse
			// Debug.WriteLine("Response: {0}", result.ServerResponse);
		}


		public static void LogError<T> (Exception ex, string context, params object[] contextParams)
		{
			if (ex == null)
				throw new ArgumentNullException ("ex", String.Format ("Attempted to log null exception. Context: {0}", context));

			ECLSAlertView.Show (context, ex.Message, ECLSAlertViewType.Error);
			LogContext.Current.Log<T> (context, ex, contextParams);
		}

		public static typeOfViewController LoadViewContollerFromStoryBoard<typeOfViewController> (string identifier)
            where typeOfViewController : UIViewController
		{
			UIStoryboard board = UIStoryboard.FromName ("Main", null);
			typeOfViewController c = board.InstantiateViewController (identifier) as typeOfViewController;
			return c;
			//typeOfViewController c = UIApplication.SharedApplication.Delegate.Window.RootViewController.Storyboard.InstantiateViewController(identifier) as typeOfViewController;						
			//return c;
		}

		/// <summary>
		/// sets the label text, or placeholder text when text is null or whitespace
		/// </summary>
		/// <param name='label'>
		/// Label.
		/// </param>
		/// <param name='text'>
		/// Text.
		/// </param>
		/// <param name='placeHolderText'>
		/// Place holder text.
		/// </param>
		public static UILabel LabelTextPlaceHolder (UILabel label, string text, string placeHolderText, UIColor textColor = null)
		{
			if (textColor == null)
				textColor = UIColor.LightGray;

			if (string.IsNullOrWhiteSpace (text)) {
				label.TextColor = textColor;
				label.Text = string.IsNullOrWhiteSpace (placeHolderText) ? label.Text : placeHolderText;
			} else {
				//	label.TextColor = UIColor.DarkTextColor;
				label.Text = text;
			}

			return label;
		}

		public static UIButton ButtonAttachClick (UIButton button, Action clickHandler)
		{
			if (clickHandler != null) {
				button.TouchUpInside += (sender, e) => {
					ECLSUIUtil.TryAction<ECLSUIUtil> ("TouchUpInside", clickHandler, button.TitleLabel.Text);
				};
			}
			return button;
		}

		/// <summary>
		/// set the button text, placeholder text if text is null or whitespace
		/// </summary>
		/// <param name='button'>
		/// Button.
		/// </param>
		/// <param name='text'>
		/// Text.
		/// </param>
		/// <param name='placeHolderText'>
		/// Place holder text.
		/// </param>
		public static UIButton ButtonTextPlaceHolderClick (UIButton button, string text, string placeHolderText)
		{
			string textToSet = string.IsNullOrWhiteSpace (text)
                ? placeHolderText
                : text;
			button.SetTitle (textToSet, UIControlState.Normal);
			button.SetTitle (textToSet, UIControlState.Selected);

			UIColor color = string.IsNullOrWhiteSpace (text)
                ? UIColor.LightGray
                : UIColor.DarkTextColor;
			button.SetTitleColor (color, UIControlState.Normal);
			button.SetTitleColor (color, UIControlState.Selected);

			return button;
		}


		/// <summary>
		/// calculates the required size of the label, using the labels properties and the given text
		/// </summary>
		/// <returns>
		/// The required size.
		/// </returns>
		/// <param name='label'>
		/// Label.
		/// </param>
		/// <param name='text'>
		/// Text.
		/// </param>
		public static CGSize LabelRequiredSize (UILabel label, string text, Func<UILabel, CGSize, CGSize> adjustmentStrategy = null)
		{
			string item = string.IsNullOrEmpty (text) ? string.IsNullOrWhiteSpace (label.Text) ? "1" : label.Text : text;
			CGSize requiredSize = item.StringSize (label.Font, new CGSize (label.Frame.Width, 9999), label.LineBreakMode);


			if (adjustmentStrategy != null)
				requiredSize = adjustmentStrategy (label, requiredSize);

			return requiredSize;
		}

		public static void EnableControls (bool enabled, params UIView[] controls)
		{
			foreach (UIView control in controls) {
				if (control is UITextField)
					(control as UITextField).TextColor = enabled ? UIColor.DarkTextColor : UIColor.LightGray;
				else if (control is UIButton)
					(control as UIButton).TitleLabel.TextColor = enabled ? UIColor.DarkTextColor : UIColor.LightGray;
				else if (control is UILabel)
					(control as UILabel).TextColor = enabled ? UIColor.DarkTextColor : UIColor.LightGray;
				else if (control is UITableViewCell)
					(control as UITableViewCell).Accessory = enabled ? UITableViewCellAccessory.DisclosureIndicator : UITableViewCellAccessory.None;

				control.UserInteractionEnabled = enabled;
			}
		}

		public static void SetTextFieldValid (UITextField textField, bool isValid)
		{
			if (isValid) {
				textField.RightViewMode = UITextFieldViewMode.Never;
			} else {
				// if (textField.Placeholder.ToLower().IndexOf("(required)") <= 0)
				//  textField.Placeholder += " (Required)";



				if (textField.RightView == null) {
					textField.RightView = new UITextView () {
						Frame = CGRect.FromLTRB (0, 0, 20, 20),
						Text = "*",
						BackgroundColor = UIColor.Clear,
						TextColor = UIColor.Red,
						Font = UIFont.SystemFontOfSize (16),
						UserInteractionEnabled = false


					};
				}
				textField.RightViewMode = UITextFieldViewMode.Always;
			}
		}

		public static void SetTextValue (UIView view, string format, params object[] formatArgs)
		{
			string textValue = string.IsNullOrWhiteSpace (format)
                ? ""
                : string.Format (format, formatArgs);

			if (view is UITextField)
				(view as UITextField).Text = textValue;
			else if (view is UILabel)
				(view as UILabel).Text = textValue;

		}

		public static void SetImageLoadActivity (WebClientBase completedClient, UIActivityIndicatorView activity, UIImageView imageView)
		{
			if (!completedClient.HasErrors && completedClient.ResponseData != null) {
				if (activity.IsAnimating)
					activity.StopAnimating ();

				imageView.Image = UIImage.LoadFromData (
					NSData.FromArray (completedClient.ResponseData)
				);
				imageView.Hidden = false;
			}
		}

		/*	public static void ConfigureSearchBar (ECLSDialogViewController view, UISearchBar searchBar, string text, Action searchHandler, string placeHolderText)
		{
			//searchBar = new UISearchBar ();
			searchBar.Placeholder = placeHolderText;
			searchBar.SizeToFit ();
			searchBar.AutocorrectionType = UITextAutocorrectionType.No;
			searchBar.AutocapitalizationType = UITextAutocapitalizationType.None;
			searchBar.Translucent = false;
			// searchBar.BackgroundColor = ThemeColors.BackgroundColor;
			searchBar.BarStyle = UIBarStyle.Black;
			searchBar.TintColor = ThemeColors.NavButtonColor;
			searchBar.ShowsSearchResultsButton = false;

			searchBar.OnEditingStarted += (sender, e) => {
				searchBar.ShowsCancelButton = true;
			};

			searchBar.CancelButtonClicked += (sender, e) => {
				searchBar.Text = "";
				searchBar.ShowsCancelButton = false;
				searchBar.ResignFirstResponder ();
			};

			if (searchHandler != null) {
				searchBar.SearchButtonClicked += (sender, e) => {
					ECLSUIUtil.TryAction<ECLSUIUtil> ("SearchHandler", searchHandler);
				};
			}

			searchBar.CancelButtonClicked += (sender, e) => {
				searchBar.Text = "";
				searchBar.ShowsScopeBar = false;
			};

			searchBar.Text = text;

			searchBar.Find<UITextField> ().ToList ().ForEach (f => f.KeyboardAppearance = IosTheme.KeyBoardAppearance);

			view.TableView.TableHeaderView = searchBar;

		}

*/
		/*	public static UIImage GetLocationIcon (LocationFinderItemType locationType)
		{
			UIImage locationIcon = UIImage.FromBundle ("Map.png");

			switch (locationType) {
			case LocationFinderItemType.Address:
				locationIcon = UIImage.FromBundle ("Map.png");
				break;
			case LocationFinderItemType.Airport:
				locationIcon = UIImage.FromBundle ("Plane.png");
				break;
			case LocationFinderItemType.Contact:
				locationIcon = UIImage.FromBundle ("AddressBook.png");
				break;
			case LocationFinderItemType.Home:
				locationIcon = UIImage.FromBundle ("Home.png");
				break;
			case LocationFinderItemType.Place:
			case LocationFinderItemType.Establishment:
				locationIcon = UIImage.FromBundle ("Pin.png");

				break;
			case LocationFinderItemType.ProfileCorpAddress:
				locationIcon = UIImage.FromBundle ("BusinessCard.png"); //<
				break;

			case LocationFinderItemType.Train:
				locationIcon = UIImage.FromBundle ("Train.png");
				break;

			}

			return locationIcon;
		}

		*/
	}
}

