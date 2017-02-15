using ALConnect.Common;
using ALConnect.Data;
using ALConnect.Models;
using Octane.Xam.VideoPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALConnect.ViewModels
{
    public class VideoListViewModel : BasePageViewModel
    {
        private List<FeatureEvent> eventsList ;
        public List<FeatureEvent> EventsListItems
        {
            get
            {
                return eventsList;
            }
            set
            {
                Set<List<FeatureEvent>>("EventsListItems", ref eventsList, value);
            }
        }

        private VideoSource featuredVideoSource;
        public VideoSource FeaturedVideoSource
        {
            get
            {
                return featuredVideoSource;
            }
            set { Set("FeaturedVideoSource", ref featuredVideoSource, value); }
        }


        public VideoListViewModel()
        {
            eventsList = new List<FeatureEvent>();
            var eventsData = new EventsData();
            EventsListItems = eventsData.GetEvents(true, true) as List<FeatureEvent>;
            FeaturedVideoSource = YouTubeVideoIdExtension.Convert(EventsListItems[0].Url);
        }
    }
}
