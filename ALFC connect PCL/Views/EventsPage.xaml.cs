using ALConnect.ViewModels;
using ALConnect.Views;

using System;
using System.Threading.Tasks;
using Plugin.Share;
using Xamarin.Forms;
using Octane.Xam.VideoPlayer;
using Octane.Xam.VideoPlayer.Constants;
using Octane.Xam.VideoPlayer.Events;

namespace ALConnect
{
    public partial class EventsPage : ContentPage
    {
        
        private double xRotate;
        private int playVolume;
        

        
        public EventsPage()
        {
            InitializeComponent();
            MuteButton.GestureRecognizers.Add(VolumeCommand());
            MuteButton.GestureRecognizers.Add(MuteCommand());
            
            UnMuteButton.GestureRecognizers.Add(UnMuteCommand());
            Timer();
        }

        /// <summary>
        /// Handles the OnPlayerStateChanged event of the VideoPlayer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="VideoPlayerStateChangedEventArgs"/> instance containing the event data.</param>
        private void VideoPlayer_OnPlayerStateChanged(object sender, VideoPlayerStateChangedEventArgs e)
        {
            switch (e.CurrentState)
            {
                case PlayerState.Paused:
                case PlayerState.Prepared:
                case PlayerState.Completed:
                case PlayerState.Initialized:
                    PauseButton.IsVisible = false;
                    PlayButton.IsVisible = true;
                    break;
                default:
                    PlayButton.IsVisible = false;
                    PauseButton.IsVisible = true;
                    break;
            }
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


        public TapGestureRecognizer UnMuteCommand()
        {
            var tapIt = new TapGestureRecognizer();
            
            tapIt.Tapped += ((sender, e) =>
            {
                try
                {
                    /// todo make this volume a setting
                    VideoPlayer.Volume = playVolume > 0 ? playVolume : 75;
                    MuteButton.IsVisible = true;
                    UnMuteButton.IsVisible = false;
                }
                catch { }
            });
            return tapIt;
        }
        public TapGestureRecognizer MuteCommand()
        {
            var tapIt = new TapGestureRecognizer();
            tapIt.NumberOfTapsRequired = 1;
            tapIt.Tapped += ((sender, e) =>
            {
                try
                {
                    playVolume = VideoPlayer.Volume;
                    VideoPlayer.Volume = 0;
                    MuteButton.IsVisible = false;
                    UnMuteButton.IsVisible = true;
                }
                catch { }
            });
            return tapIt;
        }
        public TapGestureRecognizer VolumeCommand()
        {
            var tapIt = new TapGestureRecognizer();
            tapIt.NumberOfTapsRequired = 2;
            tapIt.Tapped += ((sender, e) =>
            {
                try
                {
                    playVolume = VideoPlayer.Volume;
                    if (playVolume < 100)
                        playVolume += 5;

                    VideoPlayer.Volume = playVolume;
                }
                catch { }
            });
            return tapIt;
        }
        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VideoPlayer.Play();

            // We need to hide the main menu splash screen video when navigating to a new page
            // due to the way Xamarin Forms layers pages on Android.
            if (Device.OS == TargetPlatform.Android)
                VideoPlayer.IsVisible = true;
        }

        /// <summary>
        /// When overridden, allows the application developer to customize behavior as the <see cref="T:Xamarin.Forms.Page" /> disappears.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            VideoPlayer.Pause();

            // We need to hide the main menu splash screen video when navigating to a new page
            // due to the way Xamarin Forms layers pages on Android.
            if (Device.OS == TargetPlatform.Android)
                VideoPlayer.IsVisible = false;
        }
    }
}

