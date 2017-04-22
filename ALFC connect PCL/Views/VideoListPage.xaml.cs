using ALConnect.Common;
using ALConnect.Models;
using ALConnect.ViewModels;
using Octane.Xam.VideoPlayer.Constants;
using Octane.Xam.VideoPlayer.Events;

using Xamarin.Forms;

namespace ALConnect.Views
{
    public partial class VideoListPage : ContentPage
    {
        private int playVolume;
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoListPage" /> class.
        /// </summary>
        public VideoListPage()
        {
            InitializeComponent();
            Title = "Featured Videos";
            MuteButton.GestureRecognizers.Add(MuteCommand());
            UnMuteButton.GestureRecognizers.Add(UnMuteCommand());
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

        /// <summary>
        /// Detects taps on the playlist.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ItemTappedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void Playlist_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var vm = (VideoListViewModel)this.BindingContext;
            if (e.SelectedItem != null)
            {
                VideoPlayer.AutoPlay = true;
                var item = e.SelectedItem as FeatureEvent;

                if (item.Url.Contains("youtu"))
                    vm.FeaturedVideoSource = YouTubeVideoIdExtension.Convert(item.Url);
                
                else if (item.Url.Contains("vimeo"))
                    vm.FeaturedVideoSource = VimeoVideoIdExtension.Convert(item.Url);
                else
                    vm.FeaturedVideoSource = item.Url;

                VideoPlayer.Source = vm.FeaturedVideoSource;
                VideoPlayer.Play();
                ((ListView)sender).SelectedItem = null; // de-select the row
            }
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
