// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace EmpireCLS
{
	[Register ("PickupDropController")]
	partial class PickupDropController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnPkAddr { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnPkApt { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnPkStn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnToAddr { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnToArpt { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnToStn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtDrop { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtPickup { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtPkAirline { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtToAirline { get; set; }

		[Action ("HandleAirlinesTextChanged:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void HandleAirlinesTextChanged (UITextField sender);

		void ReleaseDesignerOutlets ()
		{
			if (btnPkAddr != null) {
				btnPkAddr.Dispose ();
				btnPkAddr = null;
			}
			if (btnPkApt != null) {
				btnPkApt.Dispose ();
				btnPkApt = null;
			}
			if (btnPkStn != null) {
				btnPkStn.Dispose ();
				btnPkStn = null;
			}
			if (btnToAddr != null) {
				btnToAddr.Dispose ();
				btnToAddr = null;
			}
			if (btnToArpt != null) {
				btnToArpt.Dispose ();
				btnToArpt = null;
			}
			if (btnToStn != null) {
				btnToStn.Dispose ();
				btnToStn = null;
			}
			if (txtDrop != null) {
				txtDrop.Dispose ();
				txtDrop = null;
			}
			if (txtPickup != null) {
				txtPickup.Dispose ();
				txtPickup = null;
			}
			if (txtPkAirline != null) {
				txtPkAirline.Dispose ();
				txtPkAirline = null;
			}
			if (txtToAirline != null) {
				txtToAirline.Dispose ();
				txtToAirline = null;
			}
		}
	}
}
