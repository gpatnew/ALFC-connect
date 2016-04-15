using System;
using Xamarin.Forms;

namespace ALFCConnect.Common
{
    public class AppColors
    {

        public static Color AlfcBlue
        {
            get { return Color.FromHex("059BD2"); }
        }
        public static Color Black
        {
            get
            {
                return Color.FromHex("010101");
            }
        }
        public static Color AlfcOrange
        {
            get { return Color.FromHex("FF6600"); }
        }
        public static Color AlfcOrangeLight
        {
            get { return Color.FromHex("FF8800"); }
        }

        public static Color AlfcGray
        {
            get { return Color.FromRgb(104, 104, 104); }
        }
        public static Color AlfcTextGray
        {
            get { return Color.FromHex("5F6062"); }
        }

        public static Color AlfcBgGray
        {
            get { return Color.FromHex("EAEAEA"); }
        }


        public static Color BlueBarBG
        {
            get { return Color.FromRgba(5, 155, 210, 182); }
        }

        public static Color White
        {
            get { return Color.FromRgb(255, 255, 255); }
        }

        public static Color Transparent
        {
            get { return Color.Transparent; }
        }
    }
}
