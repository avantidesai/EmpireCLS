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
	partial class NewViewConytoller : UIViewController
	{
		public NewViewConytoller (IntPtr handle) : base (handle)
		{
		}


		public static NewViewConytoller GetInstance (string title)
		{
			Debug.Assert (ApplicationContext.Current.IsLoggedIn, "User must be logged in to load this controller");
			var controller = ECLSUIUtil.LoadViewContollerFromStoryBoard<NewViewConytoller> ("NewViewConytoller");
			controller.Title = title;
			return controller;
		}
	}
}
