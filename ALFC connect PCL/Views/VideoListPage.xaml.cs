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

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoListPage" /> class.
        /// </summary>
        
        public VideoListPage()
        {
            InitializeComponent();
            Title = "Featured Videos";
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

                if (item.Url.Contains("youtube"))
                    vm.FeaturedVideoSource = YouTubeVideoIdExtension.Convert(item.Url);
                
                else if (item.Url.Contains("vimeo"))
                    vm.FeaturedVideoSource = VimeoVideoIdExtension.Convert(item.Url);
                else
                    vm.FeaturedVideoSource = item.Url;

                VideoPlayer.Source = vm.FeaturedVideoSource;
                ((ListView)sender).SelectedItem = null; // de-select the row
            }
        }
    }
}
