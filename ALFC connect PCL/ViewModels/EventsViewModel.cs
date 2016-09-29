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
        public string TextPrimaryColor
        {
            get
            {
                return AppColors.TextPrimaryColor;
            }
        }

        private List<EventSlide> slidesList;
        public List<EventSlide> Slides
        {
            get { return slidesList; }
            set { Set<List<EventSlide>>("EventsListItems", ref slidesList, value); }
        }

        private EventSlide currentSlide;
        public EventSlide CurrentSlide
        {
            get
            { return currentSlide; }

            set
            {
                Set<EventSlide>("CurrentSlide", ref currentSlide, value);
                CurrentSlideImageUrl = currentSlide.ImageUrl;
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

        public HtmlWebViewSource FeatureSource
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
            get { return featuredItem; }
            set { Set("FeaturedShare", ref featuredShare, value); }
        }
        public EventsViewModel()
        {
            PageTitle = "Upcoming Events";
            currentSlideId = 0;
            var slidesData = new SlidesData();
            Slides = (List<EventSlide>)slidesData.GetItems();
            CurrentSlide = Slides[currentSlideId];

             SetEventsFeaturedItem();


        }

        private void SetEventsFeaturedItem()
        {
            var eventsData = new EventsData();
            EventsListItems = eventsData.GetItems() as List<FeatureEvent>;

            var featuredEvent = eventsData.GetFeaturedItem();
            FeatureSource = BuildHtmlSource(featuredEvent);
        }

        private HtmlWebViewSource BuildHtmlSource(FeatureEvent featuredEvent)
        {
            var source = new HtmlWebViewSource();
            var innerHtml = "";
            if (featuredEvent.Id != 0)
            {
                if (featuredEvent.Link.Contains("youtube.com"))
                    innerHtml = CreateYouTubeFrame(featuredEvent.Link);
                else
                    innerHtml = CreateLabel(featuredEvent);
                
            }
            else if ((DateTime.Now.Minute % 2) == 0)
            {
                innerHtml = CreateYouTubeFrame("https://www.youtube.com/watch?v=kCHEXXLeVCE");

                FeaturedItem = "Men's Advance";
                FeaturedShare = "SEAL Training find out more @ www.alfc.us/ministry/men/";
            }
            else
            {

                FeaturedImageUrl = "Featured01.png";
                FeaturedItem = " contact Pat Newsome gpatnew@hotmail.com";
                FeaturedShare = "Join Essential Truths this Wed 6:30pm find out more @ www.alfc.us";
            }

            source.Html = string.Format("<html><body><div>{0}</div></body></html>", innerHtml);
            return source;
        }

        private string CreateLabel(FeatureEvent featuredItem)
        {
            var label = "";
            if (featuredItem.Link.Contains("http") && IsImageLink(featuredItem.Link))
            {
                label = "<img src='{0}' />";
            }
            else
            {
                label = string.Format("<a href='{0}'>{1}</a> <span class='smallText>{2}</span><div>{3}</div>'", featuredItem.Link, featuredItem.Title, featuredItem.Time, featuredItem.Description);
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
