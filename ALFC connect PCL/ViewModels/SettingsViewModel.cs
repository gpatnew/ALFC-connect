using ALFCConnect.Common;
using GalaSoft.MvvmLight;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ALFCConnect.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {

        private static ISettings AppSettings
        {
            get { return CrossSettings.Current;  }
        }
       
        public string UserEmail
        {
            get { return AppSettings.GetValueOrDefault<string>(Constants.UserNameKey, Constants.UserEmail); }
            set { AppSettings.AddOrUpdateValue<string>(Constants.UserNameKey, value); }
        }

        public string BibleVersion
        {
            get { return AppSettings.GetValueOrDefault<string>(Constants.BibleVersionKey, Constants.BibleVersion); }
            set { AppSettings.AddOrUpdateValue<string>(Constants.BibleVersionKey, value); }
        }

        public string BibleVersionName
        {
            get { return AppSettings.GetValueOrDefault<string>(Constants.BibleVersionNameKey, Constants.BibleVersionName); }
            set { AppSettings.AddOrUpdateValue<string>(Constants.BibleVersionNameKey, value); }
        }

    
    }
}
