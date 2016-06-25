using ALFCconnect.Common;
using ALFCconnect.Data;
using ALFCconnect.Models;
using System;
using System.Collections.Generic;

namespace ALFCconnect.ViewModels
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
        public EventSlide CurrentSlide {
            get
            { return currentSlide;}

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
            set { Set("CurrentSlideImageUrl", ref currentSlideImageUrl,  value); }
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
        private string featuredItem;
        public string FeaturedItem
        {
            get { return featuredItem; }
            set {  Set("FeaturedItem", ref featuredItem, value); }
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
            var eventsData = new EventsData();
            EventsListItems = eventsData.GetItems() as List<FeatureEvent>;
            CurrentSlide = Slides[currentSlideId];

            if((DateTime.Now.Minute % 2) == 0)
            {
                FeaturedImageUrl = "Featured02.png";
                FeaturedItem = "Operation GO contact Pastor Graig";
                FeaturedShare = "I'm helping with Operation GO! find out more @ www.alfc.us";
            }
            else
            {
                FeaturedImageUrl = "Featured01.png";
                FeaturedItem = " contact Pat Newsome gpatnew@hotmail.com";
                FeaturedShare = "Join Essential Truths this Wed 6:30pm find out more @ www.alfc.us";

            }
        }

        public void ChangeSlide()
        {
            CurrentSlideId = CurrentSlideId == Slides.Count-1 ?  0 : CurrentSlideId += 1;
        }

       
      
    }
}
