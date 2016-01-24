using System.Linq;
using Foundation;
using UIKit;

using MonoTouch.Dialog;
using System.Collections.Generic;
using EmpireCLS.Mobile.ApiClient;
using System.Diagnostics;
using System;
using System.ComponentModel;

using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;


namespace EmpireCLS
{
	partial class PickupDropController : UIViewController
	{
		public PickupDropController (IntPtr handle) : base (handle)
		{
		}

		public static PickupDropController GetInstance (string title)
		{
			Debug.Assert (ApplicationContext.Current.IsLoggedIn, "User must be logged in to load this controller");
			var controller = ECLSUIUtil.LoadViewContollerFromStoryBoard<PickupDropController> ("PickupDropController");
			controller.Title = title;
			return controller;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.btnPkApt.TouchUpInside += ((object sender, EventArgs e) => {
				HandlePickupAirportTapped ();
			});

			this.btnPkAddr.TouchUpInside += ((object sender, EventArgs e) => {
				HandlePickupAddressTapped ();
			});

			/*this.txtPkAirline.ValueChanged += ((object sender, EventArgs e) => {
				AirlinesTextChanged (sender, e);
			});*/

			txtPkAirline.EditingChanged += ((object sender, EventArgs e) => {
				AirlinesTextChanged (sender, e);
			});
		}

		private void HandlePickupAirportTapped ()
		{
			this.View.EndEditing (true);
			this.txtPkAirline.Hidden = false;
			new ECLSAsyncApiStatus<CacheContext> (ECLSPopoverStyle.Gradient) {
				CompletedStrategy = (cacheContext) => {
					cacheContext.SaveCaches ();
				}
			}.InvokeAsyncAction ("Loading airlines...", cacheContext => {
				cacheContext.LoadAsyncAirlines ();
			}, false);

		}

		private void AirlinesTextChanged (object sender, EventArgs e)
		{
			this.View.EndEditing (true);
			this.txtPkAirline.Hidden = false;
			new ECLSAsyncApiStatus<CacheContext> (ECLSPopoverStyle.Gradient) {
				CompletedStrategy = (cacheContext) => {
					cacheContext.SaveCaches ();
				}
			}.InvokeAsyncAction ("Loading airlines...", cacheContext => {
				cacheContext.LoadAsyncAirlines ();
			}, false);
		}

		private void HandlePickupAddressTapped ()
		{
			this.txtPkAirline.Hidden = true;
		}

			
		private void HandleAirlinesTextChanged (UITextField sender, EventArgs e)
		{
			throw new NotImplementedException ();
		}
	}

}
