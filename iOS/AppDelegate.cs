using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using ALConnect.Helpers;
using ALConnect;
using Octane.Xam.VideoPlayer.iOS;

namespace ALConnect.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
        

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
            global::Xamarin.Forms.Forms.Init ();
            FormsVideoPlayer.Init("E5BFE5EB794EE0126867E243632BFDB557AEF63C");
            //CrossPushNotification.Initialize<CrossPushNotificationListner>();
            LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

