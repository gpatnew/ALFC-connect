using System;
using Xamarin.Forms;
using ALFCConnect.Common;
namespace ALFCConnect.Helpers
{
    public class BoolColorConvertor : IValueConverter
    {
        private Color color1 = AppColors.ALFCBGBlue;
        private Color color2 = AppColors.ALFCBGPurple;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value is bool)
            {
                return (bool)value ? color1 : color2;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value is Color)
            {
                return (Color)value == color1 ? true: false;
            }
            return false;
        }


        public BoolColorConvertor(Color baseColor, Color selectedColor)
        {
            color1 = baseColor;
            color2 = selectedColor;
        }
    }
}
