using ALConnect.ViewModels;
using ALConnect.Views;

using System;
using System.Threading.Tasks;
using Plugin.Share;
using Xamarin.Forms;

namespace ALConnect
{
    public partial class EventsPage : ContentPage
    {
        
        private double xRotate;
        private uint yRotate;
        

        
        public EventsPage()
        {
            InitializeComponent();
            Timer();
        }

        

        async void DisplayAlert()
        {
            //var result = await this.DisplayAlert("q", "move forward", "ok", "no");
        }
        public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            return;
            
          
        }

        private async void Timer()
        {
            await Task.Delay(8000);
            EventsViewModel evm = (EventsViewModel)this.BindingContext;
            evm.ChangeSlide();
            Timer();
        }
        public  void OnImageTapped(object sender, EventArgs arg)
        {
                EventsViewModel evm = (EventsViewModel)this.BindingContext;
            
                var eventWebPage = new WebPage(evm.CurrentSlideLink);
                Navigation.PushModalAsync(eventWebPage);
        }

        public void OnFeaturedTapped(object sender, EventArgs arg)
        {
            EventsViewModel evm = (EventsViewModel)this.BindingContext;

            DisplayAlert("Featured", evm.FeaturedShare, "OK");

        }
        private void RotateImage(Image img, double xRotate, uint yRotate)
        {
            var ease = Easing.BounceOut;
            img.RotateTo(xRotate, yRotate, ease);
        }

        public async void EventClicked(object sender, EventArgs e)
        {
            var selectedButton = sender as Button;
            var webUrl = selectedButton.CommandParameter.ToString();
            var eventWebPage = new WebPage(webUrl);
            await Navigation.PushModalAsync(eventWebPage);
        }

        public async void shareButtonClicked(object sender, EventArgs e)
        {
            EventsViewModel evm = (EventsViewModel)this.BindingContext;
            await CrossShare.Current.Share(evm.FeaturedShare, "ALC Feature Event");
        }
    }
}

