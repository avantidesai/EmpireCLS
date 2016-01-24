using System;
using CoreGraphics;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;
using CoreAnimation;
using ObjCRuntime;

namespace EmpireCLS
{
	public enum ECLSPopoverStyle
	{
		Unknown,
		OkCancel,
		Activity,
		Gradient,
		Plain

	}

	public class ECLSPopover : UIViewController
	{
		private UIBarButtonItem _leftBarButtonItem;
		private UIBarButtonItem _rightBarButtonItem;

		/// <summary>
		/// ECLS popover button handler.
		/// </summary>
		private class ECLSPopoverButtonItemHandler : NSObject
		{
			public ECLSPopoverButtonItemHandler (UIBarButtonItem leftBarButtonItem, UIBarButtonItem rightBarButtonItem)
			{
				leftBarButtonItem.Target = this;
				leftBarButtonItem.Action = new ObjCRuntime.Selector ("leftButtonClicked:");

				rightBarButtonItem.Target = this;
				rightBarButtonItem.Action = new ObjCRuntime.Selector ("rightButtonClicked:");
			}

			public Action<bool> NotifyComplete { get; set; }

			[Action ("leftButtonClicked:")]
			private void LeftButtonClicked (NSObject sender)
			{
				ECLSUIUtil.TryAction<ECLSPopover> ("LeftButtonClicked", () => {
					if (this.NotifyComplete != null)
						this.NotifyComplete (true);
				});
							
			}

			[Action ("rightButtonClicked:")]
			private void RightButtonClicked (NSObject sender)
			{
				ECLSUIUtil.TryAction<ECLSPopover> ("RightButtonClicked", () => {
					if (this.NotifyComplete != null)
						this.NotifyComplete (false);
				});
				
			}
		}

		
		private ECLSPopoverButtonItemHandler _buttonItemHandler = null;

		private UIView _container = null;
						
		private readonly nfloat _frameHeight;
		private readonly ECLSPopoverStyle _style;
		
		private UINavigationBar _navigationBar;
		
		private UILabel _activityLabel;
		private UIActivityIndicatorView _activityIndicator;
		
		// Shows a new loading modal with the supplied text.  Must be explicitly closed.
		public static ECLSPopover ShowLoadingModal (string title)
		{
			ECLSPopoverStyle style = ECLSPopoverStyle.Activity;
			var popover = new ECLSPopover (UIApplication.SharedApplication.Delegate.GetWindow ().Frame.Height * .40f, style);
			popover.PresentPopover (title, true, true);
			return popover;
		}

		public ECLSPopover (nfloat frameHeight, ECLSPopoverStyle style = ECLSPopoverStyle.OkCancel)
		{
			_style = style;		
			_frameHeight = frameHeight;
		}

		public bool Cancelled { get; private set; }

		public Action<ECLSPopover> Completed { get; set; }

		protected void NotifyComplete (bool cancelled)
		{
			this.Cancelled = cancelled;
			
			if (this.Completed != null)
				this.Completed (this);
		}

		/// <summary>
		/// Shows the popup.
		/// </summary>
		/// <param name='showInView'>
		/// Show in view.
		/// </param>
		public void PresentPopover (string title, bool animated = true, bool applyBackgroundColor = false)
		{
			this.Title = title;

			UIView viewForShowing = ECLSUIUtil.LeafViewController (UIApplication.SharedApplication.Delegate.GetWindow ().RootViewController).View;
           
			_container = new UIView (UIApplication.SharedApplication.Delegate.GetWindow ().Frame);

			if (applyBackgroundColor)
				_container.BackgroundColor = UIColor.Black;


			// explicitly load the view
			if (!this.IsViewLoaded) {
				this.LoadView ();
				this.ViewDidLoad ();
			}

			// loaded during the ViewDidLoad handler
			if (_navigationBar != null)
				_navigationBar.TopItem.Title = this.Title;
			else if (_activityLabel != null)
				_activityLabel.Text = this.Title;
			
			_container.Alpha = .0f;

			viewForShowing.AddSubview (_container);

			CGRect endingFrame = OnGetFrame ();
			CGRect startingFrame = endingFrame;
			startingFrame.Offset (0, endingFrame.Height);

			this.View.Frame = startingFrame;
		
			viewForShowing.AddSubview (this.View);

			UIView.Animate (.15, delegate() {
				this.View.Frame = endingFrame;
				_container.Alpha = .75f;

			}, delegate() {

			});

		}

		public void DismissPopup (bool animated = true)
		{
			if (_container == null)
				return;

			CGRect endingFrame = this.View.Frame;
			endingFrame.Offset (0, endingFrame.Height);

			UIView.Animate (.5, delegate() {
				this.View.Frame = endingFrame;
				_container.Alpha = .0f;
			}, delegate() {
				this.View.RemoveFromSuperview ();
				_container.RemoveFromSuperview ();
			});

		}

		protected virtual CGRect OnGetFrame ()
		{
			return CGRect.FromLTRB (0, 
				UIApplication.SharedApplication.Delegate.GetWindow ().Frame.Height - _frameHeight, 
				UIApplication.SharedApplication.Delegate.GetWindow ().Frame.Width, 
				UIApplication.SharedApplication.Delegate.GetWindow ().Frame.Bottom
			);			
		}

	
		/// <summary>
		/// Views the did load.
		/// </summary>
		/// 
		public override void ViewDidLoad ()
		{
			ECLSUIUtil.TryAction<ECLSPopover> ("ViewDidLoad", () => {
				base.ViewDidLoad ();

				// default frame size
				this.View.Frame = OnGetFrame ();

				if (_style == ECLSPopoverStyle.OkCancel) {
					_navigationBar = new UINavigationBar (
						CGRect.FromLTRB (0, 0, UIApplication.SharedApplication.Delegate.GetWindow ().Frame.Width, 44)

					);
					_navigationBar.BackgroundColor = UIColor.DarkGray; //UIColor.FromRGB (175,1,83); //magenta
					//	_navigationBar.TintColor = UIColor.FromRGB (175,1,83); // UIColor.White; 
					
					_leftBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Cancel);                    
					_rightBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Done);
					_leftBarButtonItem.SetTitlePositionAdjustment (new UIOffset (15f, 0), UIBarMetrics.Default);
					_rightBarButtonItem.SetTitlePositionAdjustment (new UIOffset (-15f, 0), UIBarMetrics.Default);
					UINavigationItem navItem = new UINavigationItem () {
						LeftBarButtonItem = _leftBarButtonItem,
						RightBarButtonItem = _rightBarButtonItem,
					};

                    
					_navigationBar.SetItems (
						new UINavigationItem[] { navItem }, false
					);
					_navigationBar.ClipsToBounds = false;
                    
					_buttonItemHandler = new ECLSPopoverButtonItemHandler (
						navItem.LeftBarButtonItem, navItem.RightBarButtonItem
					);
					_buttonItemHandler.NotifyComplete = this.NotifyComplete;

					this.View.AddSubview (_navigationBar);

					this.View.AddSubview (
						new UIView (CGRect.FromLTRB (0, _navigationBar.Frame.Height, this.View.Bounds.Right, this.View.Bounds.Bottom)) {

							BackgroundColor = UIColor.FromRGB (240, 232, 236),
							Alpha = .95f
						}
					);	
					/*this.View.AddSubview(
						new UIImageView(UIImage.FromFile(@"Images/Background_Calculating_Full.png").StretchableImage(0, 5)) {
							Frame = RectangleF.FromLTRB(0, _navigationBar.Frame.Height, this.View.Bounds.Right, this.View.Bounds.Bottom)
						}
					);	*/										
				} else if (_style == ECLSPopoverStyle.Activity) {
						
					/*this.View.AddSubview(
						new UIImageView(UIImage.FromFile(@"Images/Background_Calculating_Full.png").StretchableImage(0, 5)) {
							Frame = this.View.Bounds
						}
					);
					
*/
					this.View.AddSubview (new UIView (this.View.Bounds) {
						BackgroundColor = UIColor.Black, 
						//BackgroundColor =  UIColor.FromRGB (175,1,83),  //magenta
						Alpha = .75f
					});
					
					_activityLabel = new UILabel () {
						Frame = CGRect.FromLTRB (this.View.Frame.Left, (_frameHeight / 2), this.View.Frame.Width, (_frameHeight / 2) + 31 /* adjusted below */),
						TextAlignment = UITextAlignment.Center,
						Lines = 3,
						LineBreakMode = UILineBreakMode.WordWrap,
						BackgroundColor = UIColor.Clear,
						Opaque = true,
						TextColor = UIColor.White,
						Text = this.Title,
						ShadowColor = UIColor.Black,
						ShadowOffset = new CGSize (0f, -1.0f),
						Font = ECLSUIUtil.HelveticaNeueBold (24.0f)
					};					
					CGSize labelSize = ECLSUIUtil.LabelRequiredSize (_activityLabel, this.Title);
					_activityLabel.Frame = CGRect.FromLTRB (
						_activityLabel.Frame.Left, _activityLabel.Frame.Top, _activityLabel.Frame.Right, _activityLabel.Frame.Top + labelSize.Height
					);
					this.View.AddSubview (_activityLabel);
			
					_activityIndicator = new UIActivityIndicatorView () {
						HidesWhenStopped = true,
						Center = new CGPoint (
							this.View.Frame.Width / 2, (_frameHeight / 2) - 20
						)
					};
					this.View.AddSubview (_activityIndicator);

					_activityIndicator.StartAnimating ();
				} else if (_style == ECLSPopoverStyle.Plain) {
								
					_activityLabel = new UILabel () {
						Frame = CGRect.FromLTRB (this.View.Frame.Left, (_frameHeight / 2), this.View.Frame.Width, (_frameHeight / 2) + 31 /* adjusted below */),
						TextAlignment = UITextAlignment.Center,
						Lines = 3,
						LineBreakMode = UILineBreakMode.WordWrap,
						BackgroundColor = UIColor.Clear,
						Opaque = true,
						TextColor = UIColor.White,
						Text = this.Title,
						ShadowColor = UIColor.Black,
						ShadowOffset = new CGSize (0f, -1.0f),
						Font = ECLSUIUtil.HelveticaNeueBold (18.0f)
					};					
					CGSize labelSize = ECLSUIUtil.LabelRequiredSize (_activityLabel, this.Title);
					_activityLabel.Frame = CGRect.FromLTRB (
						_activityLabel.Frame.Left, _activityLabel.Frame.Top, _activityLabel.Frame.Right, _activityLabel.Frame.Top + labelSize.Height
					);
					this.View.AddSubview (_activityLabel);

					_activityIndicator = new UIActivityIndicatorView () {
						HidesWhenStopped = true,
						Center = new CGPoint (
							this.View.Frame.Width / 2, (_frameHeight / 2) - 20
						)
					};
					this.View.AddSubview (_activityIndicator);

					_activityIndicator.StartAnimating ();
				}
			});
		
		}

	}
}

