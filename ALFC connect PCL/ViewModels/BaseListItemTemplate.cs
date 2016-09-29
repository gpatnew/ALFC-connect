using ALConnect.Common;
using ALConnect.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ALConnect.ViewModels
{
    public class BaseListItemTemplate
    {

        public static DataTemplate Get(string currentValue)
        {
            return new DataTemplate(() =>
            {
                Button ListItem = new Button();
                ListItem.TextColor =  Color.FromHex(AppColors.TextPrimaryColor);
                ListItem.FontSize = 10;
                ListItem.SetBinding(Button.TextProperty, new Binding("Name", BindingMode.OneWay, null, null, "{0}"));
                ListItem.SetBinding(Button.CommandParameterProperty, new Binding("Value", BindingMode.OneWay, null, null, "{0}"));
                ListItem.HorizontalOptions = LayoutOptions.FillAndExpand;
                ListItem.VerticalOptions = LayoutOptions.FillAndExpand;
                ListItem.BackgroundColor = AppColors.White;

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Padding = new Thickness(5, 0, 10, 5),
                        Spacing = 0,
                        Children = { ListItem }
                    }
                };
            });
        }

        public static DataTemplate GetFullLabel(Color bgColor)
        {
            return new DataTemplate(() =>
            {
                Label ListItem = new Label();
                ListItem.TextColor = AppColors.White;
                ListItem.SetBinding(Label.TextProperty, new Binding("UrlSearch", BindingMode.OneWay, null, null, "{0}"));
                ListItem.FontSize = 10; ;
                ListItem.HorizontalOptions = LayoutOptions.FillAndExpand;
                ListItem.VerticalOptions = LayoutOptions.FillAndExpand;
                ListItem.BackgroundColor = bgColor;
                ListItem.MinimumHeightRequest = 120;
                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Padding = new Thickness(5, 0, 10, 5),
                        Spacing = 0,
                        Children = { ListItem }
                    }
                };
            });
        }

        public static DataTemplate GetSoap(Color bgColor)
        {
            return new DataTemplate(() =>
            {
                Label ListItem = new Label();
                ListItem.TextColor = AppColors.White;
                ListItem.SetBinding(Label.TextProperty, new Binding("Identifier", BindingMode.OneWay, null, null, "{0}"));
                ListItem.FontSize = 10; ;
                ListItem.HorizontalOptions = LayoutOptions.FillAndExpand;
                ListItem.VerticalOptions = LayoutOptions.FillAndExpand;
                ListItem.BackgroundColor = bgColor;
                ListItem.MinimumHeightRequest = 120;
                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Padding = new Thickness(5, 0, 10, 5),
                        Spacing = 0,
                        Children = { ListItem }
                    }
                };
            });
        }

     
        public static DataTemplate GetColorLabel(Color bgColor, Color selectedBGColor)
        {
            return new DataTemplate(() =>
            {
                Label ListItem = new Label();
                ListItem.TextColor = AppColors.White;
                ListItem.SetBinding(Label.TextProperty, new Binding("Name", BindingMode.OneWay, null, null, "{0}"));

                ListItem.FontSize = 12;
                ListItem.HorizontalOptions = LayoutOptions.FillAndExpand;
                ListItem.VerticalOptions = LayoutOptions.FillAndExpand;
                var bcConvertor = new BoolColorConvertor(selectedBGColor, bgColor);

                ListItem.SetBinding(Label.BackgroundColorProperty, new Binding("IsSelected", converter: bcConvertor));

                ListItem.MinimumHeightRequest = 120;
                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Padding = new Thickness(5, 0, 10, 5),
                        Spacing = 0,
                        Children = { ListItem }
                    }
                };
            });
        }

        public static DataTemplate GetUrl()
        {
            return new DataTemplate(() =>
            {
                Label ListItem = new Label();
                ListItem.TextColor = Color.FromHex(AppColors.TextPrimaryColor);
                ListItem.SetBinding(Label.TextProperty, new Binding("Name", BindingMode.OneWay, null, null, "{0}"));
                ListItem.BackgroundColor = AppColors.White;
                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Padding = new Thickness(5, 0, 10, 5),
                        Spacing = 0,
                        Children = { ListItem }
                    }
                };
            });
        }

        public static DataTemplate Error()
        {
            return new DataTemplate(() =>
            {
                Label ListItem = new Label();
                ListItem.TextColor = AppColors.AlfcTextRed;
                ListItem.SetBinding(Label.TextProperty, new Binding("Name", BindingMode.OneWay, null, null, "* {0}"));

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Padding = new Thickness(5, 0, 10, 5),
                        Spacing = 0,
                        Children = { ListItem }
                    }
                };
            });
        }

        public static DataTemplate TextCell()
        {
            return new DataTemplate(typeof(TextCell));
        }

        private TapGestureRecognizer ImageTap(Command navigateCommand)
        {
            var tapIt = new TapGestureRecognizer();
            tapIt.Command = navigateCommand;

            tapIt.NumberOfTapsRequired = 1;
            return tapIt;
        }


    }
}
