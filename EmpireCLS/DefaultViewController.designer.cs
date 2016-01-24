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
	[Register ("DefaultViewController")]
	partial class DefaultViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnForgot { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnGuest { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnLogin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSignUp { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtUsername { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnForgot != null) {
				btnForgot.Dispose ();
				btnForgot = null;
			}
			if (btnGuest != null) {
				btnGuest.Dispose ();
				btnGuest = null;
			}
			if (btnLogin != null) {
				btnLogin.Dispose ();
				btnLogin = null;
			}
			if (btnSignUp != null) {
				btnSignUp.Dispose ();
				btnSignUp = null;
			}
			if (txtPassword != null) {
				txtPassword.Dispose ();
				txtPassword = null;
			}
			if (txtUsername != null) {
				txtUsername.Dispose ();
				txtUsername = null;
			}
		}
	}
}
