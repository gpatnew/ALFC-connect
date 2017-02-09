using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using ALConnect.Helpers;
using ALConnect;

namespace ALConnect.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
        

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
            global::Xamarin.Forms.Forms.Init ();
            //CrossPushNotification.Initialize<CrossPushNotificationListner>();
			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

