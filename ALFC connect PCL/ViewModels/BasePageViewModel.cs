using ALFCconnect.Common;
using GalaSoft.MvvmLight;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ALFCconnect.ViewModels
{
    public class BasePageViewModel : ViewModelBase
    {
        private string pageTitle = "Page Name Missing";
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }
        
        public  string TextSecondaryColor
        {
            get
            {
                return AppColors.TextSecondaryColor;
            }
        }

        public static string CurrentTextColor
        {
            get
            { 
                return AppColors.CurrentTextColor;
            }
        }

        public static string SectionBGColor
        {
            get { return AppColors.SectionBGColor; }
        }
        public string PageBGColor
        {
            get
            {
                string colorHex = "White";
                return colorHex;
            }

        }

        public string PageTitle
        {
            get { return pageTitle; }
            set { pageTitle = value; }
        }
}
}
