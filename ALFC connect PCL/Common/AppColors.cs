using System;
using Xamarin.Forms;

namespace ALConnect.Common
{
    public class AppColors
    {

        public static string CurrentTextColor
        {
            get { return "#059BD2"; }
        }
        public static Color Black
        {
            get
            {
                return Color.FromHex("010101");
            }
        }
        
        public static string TextPrimaryColor
        {
            get { return "#CC2973BA"; }
        }

        public static string TextSecondaryColor
        {
            get { return "#CC7F6A00"; }
        }

        public static string SectionBGColor
        {
            get { return "#FBF9FF"; }
        }

        public static string BlueBarBG
        {
            get { return "#1F87D1"; }
        }

        public static Color White
        {
            get { return Color.FromRgb(255, 255, 255); }
        }

        public static Color Transparent
        {
            get { return Color.Transparent; }
        }

        public static Color ALFCBGBlue { get { return Color.FromRgba(5, 155, 210, 182); } }
        public static Color ALFCBGPurple { get { return Color.FromRgba(5, 155, 210, 182); } }

        public static Color AlfcTextRed { get { return Color.Red; } }
}
}
