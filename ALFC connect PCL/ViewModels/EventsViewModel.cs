using ALConnect.Common;
using ALConnect.Data;
using ALConnect.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ALConnect.ViewModels
{
    public class EventsViewModel : BasePageViewModel
    {
        private List<FeatureEvent> slidesList;
        public List<FeatureEvent> Slides
        {
            get { return slidesList; }
            set { Set<List<FeatureEvent>>("EventsListItems", ref slidesList, value); }
        }

        private FeatureEvent currentSlide;
        public FeatureEvent CurrentSlide
        {
            get
            { return currentSlide; }

            set
            {
                Set<FeatureEvent>("CurrentSlide", ref currentSlide, value);
                CurrentSlideImageUrl = currentSlide.Url;
                CurrentSlideTitle = currentSlide.Title;
                CurrentSlideLink = currentSlide.Link;
            }
        }

        private List<FeatureEvent> eventList;
        public List<FeatureEvent> EventsListItems
        {
            get
            {
                return eventList;
            }
            set
            {
                Set<List<FeatureEvent>>("EventsListItems", ref eventList, value);
            }
        }

        private string currentSlideImageUrl;
        public string CurrentSlideImageUrl
        {
            get { return currentSlideImageUrl; }
            set { Set("CurrentSlideImageUrl", ref currentSlideImageUrl, value); }
        }

        private string currentSlideLink;
        public string CurrentSlideLink
        {
            get { return currentSlideLink; }
            set { Set("CurrentSlideLink", ref currentSlideLink, value); }
        }

        private string currentSlideTitle;
        public string CurrentSlideTitle
        {
            get { return currentSlideTitle; }
            set { Set("CurrentSlideTitle", ref currentSlideTitle, value); }
        }

        private int currentSlideId;
        public int CurrentSlideId
        {
            get
            { return currentSlideId; }

            set
            {
                currentSlideId = value;
                CurrentSlide = Slides[CurrentSlideId];
            }
        }

        private string featuredImageUrl;
        public string FeaturedImageUrl
        {
            get { return featuredImageUrl; }
            set { Set("FeaturedImageUrl", ref featuredImageUrl, value); }
        }

        private HtmlWebViewSource featuredSource;

        public HtmlWebViewSource FeaturedSource
        {
            get { return featuredSource; }

            set { Set("FeatureSource", ref featuredSource, value); }
        }

        private string featuredItem;
        public string FeaturedItem
        {
            get { return featuredItem; }
            set { Set("FeaturedItem", ref featuredItem, value); }
        }

        private string featuredShare;
        public string FeaturedShare
        {
            get { return featuredShare; }
            set { Set("FeaturedShare", ref featuredShare, value); }
        }
        public EventsViewModel()
        {
            PageTitle = "Upcoming Events";
            
            SetEventsFeaturedItem();
        }

        private void SetEventsFeaturedItem()
        {
            var eventsData = new EventsData();
            EventsListItems = eventsData.GetEvents(false) as List<FeatureEvent>;

            currentSlideId = 0;
            Slides = (List<FeatureEvent>)eventsData.GetEvents(true);
            CurrentSlide = Slides[currentSlideId];
            var featuredEvent = eventsData.GetFeaturedItem();
            FeaturedSource = BuildHtmlSource(featuredEvent);
        }

        private HtmlWebViewSource BuildHtmlSource(FeatureEvent featuredEvent)
        {
            var source = new HtmlWebViewSource();
            var innerHtml = "";
            if (featuredEvent.Id != 0)
            {
                if (featuredEvent.Link != null && featuredEvent.Link.Contains("youtube.com"))
                    innerHtml = CreateYouTubeFrame(featuredEvent.Link);
                else
                { 
                    innerHtml = CreateLabel(featuredEvent);
                    FeaturedImageUrl = featuredEvent.Url;
                    FeaturedItem = featuredEvent.Title;
                    FeaturedShare = string.Format("{0} {1} {2}", featuredEvent.Title,featuredEvent.Description,featuredEvent.Url);

                }
            }
            else if ((DateTime.Now.Minute % 2) == 0)
            {
                FeaturedImageUrl = "https://s3-us-west-2.amazonaws.com/alcmobileapp/featureitem/emc2.jpg";
                FeaturedItem = "Join a community group now.";
                FeaturedShare = "Join us as we learn more about EMC2 find out more @ www.alfc.us";
                innerHtml = string.Format("<img src='{0}' ></img>", FeaturedImageUrl);
            }
            else
            {
                FeaturedImageUrl = "https://s3-us-west-2.amazonaws.com/alcmobileapp/featureitem/ElectionPrayer.jpg";
                FeaturedItem = "contact pastorkimo@alfc.com";
                FeaturedShare = "Join us as we pray for our Nation and the Elections this Wed 6:30pm find out more @ www.alfc.us";
                innerHtml = string.Format("<img src='{0}' width='100%' height='100%' ></img>", FeaturedImageUrl);
            }

            var wrapperHtml = string.Format(@"<html><body>
<img src='{0}' />
</body>
</html>", FeaturedImageUrl);
            source.Html = wrapperHtml;

            //source.Html = string.Format(@"<html><head><title>ALC Featured</title></head><body><div></div> {0} here ya go</body></html>", innerHtml);
            return source;
        }

        private string CreateLabel(FeatureEvent featuredItem)
        {
            var label = "";
            if (featuredItem.Url.Contains("http") && IsImageLink(featuredItem.Url))
            {
                label = string.Format(@"<img src='{0}'  width='100%' height='100%' title='{1}'></img>", featuredItem.Url, featuredItem.Title);
            }
            else
            {
                label = string.Format(@"<a href='{0}'>{1}</a> <span class='smallText>{2}</span><div>{3}</div>'", featuredItem.Url, featuredItem.Title, featuredItem.Time, featuredItem.Description);
             }
            return label;
        }

        private bool IsImageLink(string link)
        {
            bool result = link.Contains(".png");
            if (link.Contains(".gif")) result = true;
            if (link.Contains(".jpg")) result = true;
            if (link.Contains(".jpeg")) result = true;
            return result;

        }

        private string CreateYouTubeFrame(string link)
        {
            return string.Format("<iframe width=\"320\" height=\"600\" src=\"{0}\" frameborder=\"0\" allowfullscreen><iframe>", link);
        }

        public void ChangeSlide()
        {
            CurrentSlideId = CurrentSlideId == Slides.Count - 1 ? 0 : CurrentSlideId += 1;
        }



    }
}
