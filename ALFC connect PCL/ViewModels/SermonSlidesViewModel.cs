using ALConnect.Data;
using ALConnect.Models;
using System.Collections.Generic;

namespace ALConnect.ViewModels
{
    public class SermonSlidesViewModel : BasePageViewModel
    {
        List<SermonSlide> slides;
        public List<SermonSlide> Slides
        {
            get { return slides; }
            set { Set("Slides", ref slides, value); }
        }

        public string SermonName { get; private set; }

        public int SermonId { get; private set; }

        public void Save()
        {
            var sermonData = new SermonsData();
            sermonData.SetSermonDone(SermonId);
            foreach (SermonSlide slide in Slides)
            {
                sermonData.SaveSlideItem(slide);
            }
        }

        public SermonSlidesViewModel(int sermonId, string sermonName)
        {
            FetchSlides(sermonId, sermonName);
            SermonName = sermonName;
            SermonId = sermonId;
        }

        private async void  FetchSlides(int sermonId, string sermonName)
        {
            slides = new List<SermonSlide>();
            slides.Add(new SermonSlide { Id = 1, SermonId = sermonId, Message = sermonName, Title = "Loading...", ImageUrl = "loading.gif" });

            SermonsData sd = new SermonsData();
            var sermon = sd.GetItem(sermonId);
            var sermonSlides = (List<SermonSlide>)sd.GetSlides(sermonId);
            if (sermonSlides.Count <= 0)
                Slides = await sd.LoadSlidesAsync(sermonId, sermon.SlideLink);
            else
                slides = sermonSlides;
        }


    }
}
