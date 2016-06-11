using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ALFCConnect.Views
{
    public partial class SharePage : ContentPage
    {
        public SharePage()
        {
            InitializeComponent();
        }

        async void OnReturnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void OnSendButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
   }
}
