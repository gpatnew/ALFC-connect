using ALConnect.Common;
using ALConnect.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ALConnect.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        void Done_Clicked(object sender, EventArgs e)
        {
            //Button btn = (Button)sender;
            var d = Parent;
            Navigation.PopModalAsync();
        }

        void ExtendedButton_Clicked(object sender, EventArgs e)
        {
            ExtendedButton btn = (ExtendedButton)sender;
            Navigation.PushModalAsync(new DropdownListPage(btn, btn.Value.ToString(), btn.CommandParameter.ToString()));
        }
    }
}
