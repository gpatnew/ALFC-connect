using ALFCconnect.ViewModels;
using ALFCconnect.Views;
using Plugin.Connectivity;
using System;
using System.Threading.Tasks;
using Plugin.Share;
using Xamarin.Forms;

namespace ALFCconnect
{
    public partial class EventsPage : ContentPage
    {
        
        private double xRotate;
        private uint yRotate;
        

        
        public EventsPage()
        {
            InitializeComponent();
            DisplayAlert();
            Timer();
        }

        private void CheckConnectivityStatus()
        {
            var connected = CrossConnectivity.Current.IsConnected;
        }

        async void DisplayAlert()
        {
            var result = await this.DisplayAlert("q", "move forward", "ok", "no");
        }
        public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            return;
            
            var img = sender as Image;
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    xRotate = 0;
                    yRotate = 20;
                    RotateImage(img, xRotate, yRotate);
                    break;
                case GestureStatus.Running:
                    yRotate += 20;
                    RotateImage(img, xRotate, yRotate);
                    break;

                case GestureStatus.Completed:
                    EventsViewModel evm = (EventsViewModel)this.BindingContext;
                    evm.ChangeSlide();
                    
                    break;
            }
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
            await CrossShare.Current.Share(evm.FeaturedShare, "ALFC Feature Event");
        }
    }
}

