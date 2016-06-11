using ALFCConnect.Common;
using Plugin.Connectivity;
using ALFCConnect.ViewModels;
using Xamarin.Forms;
using System;
using ALFCConnect.Views;
using System.Threading.Tasks;

namespace ALFCConnect
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

        private void CheckConnectivityStatus()
        {
            var connected = CrossConnectivity.Current.IsConnected;
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


    }
}

