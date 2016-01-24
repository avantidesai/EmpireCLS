using System;
using System.Linq;
using System.Collections.Generic;

using UIKit;
using Foundation;
using EmpireCLS;

namespace EmpireCLS
{
	public enum ECLSAlertViewType
	{
		Unknown,
		Ok,
		Cancel,
		Error
	}

	public class ECLSAlertViewButton
	{
		public string Title { get; set; }

		public Action Clicked { get; set; }
	}

	public class ECLSAlertView : UIAlertView
	{
		/// <summary>
		/// Show the specified title, message and type.
		/// </summary>
		/// <param name='title'>
		/// Title.
		/// </param>
		/// <param name='message'>
		/// Message.
		/// </param>
		/// <param name='type'>
		/// Type.
		/// </param>
		public static void Show (string title, string message, ECLSAlertViewType type = ECLSAlertViewType.Ok)
		{
			string buttonText = type == ECLSAlertViewType.Ok
				? "Ok"
				: type == ECLSAlertViewType.Cancel
					? "Cancel"
					: type == ECLSAlertViewType.Error
						? "Ok"
						: "Hit Me!";
			
			string titleToShow = type == ECLSAlertViewType.Error
				? string.Format ("{0}", title)
				: title;
			
			using (UIAlertView a = new UIAlertView (titleToShow, message, null, buttonText)) {
				a.Show ();
			}
			
		}

		private readonly List<ECLSAlertViewButton> _buttons = new List<ECLSAlertViewButton> ();

		public ECLSAlertView (string title, string message, params ECLSAlertViewButton[] buttons)
		{
			_buttons.AddRange (buttons);
			
			this.Title = title;
			this.Message = message;
			this.WeakDelegate = this;
			
			_buttons.ForEach (
				b => this.AddButton (b.Title)
			);
		}

		[Export ("alertView:didDismissWithButtonIndex:")]
		private void AlertViewDidDismiss (UIAlertView alertView, int buttonIndex)
		{
			ECLSUIUtil.TryAction<ECLSAlertView> ("AlertViewDidDismiss", () => {
				ECLSAlertViewButton button = _buttons [buttonIndex];
				if (button.Clicked != null)
					button.Clicked ();	
				
			});
			
		}
	}
}

