using System;
using ALConnect;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Octane.Xam.VideoPlayer.Android;

namespace ALConnect.Droid
{
	[Activity (Label = "ALConnect", Icon = "@drawable/icon")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            FormsVideoPlayer.Init("DE2DFE93F1197FA53842EC121B5171C754345C77");
            LoadApplication (new App ());
		}
	}
}

