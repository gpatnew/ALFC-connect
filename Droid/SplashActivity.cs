
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace ALConnect.Droid
{
    [Activity(Theme = "@style/Theme.Splash", NoHistory = true, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            StartActivity(typeof(MainActivity));

            
        }

        
    }
}