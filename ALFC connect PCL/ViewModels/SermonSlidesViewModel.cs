using ALFCConnect.Data;
using ALFCConnect.Models;
using System.Collections.Generic;

namespace ALFCConnect.ViewModels
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
            var ser = sd.GetItem(sermonId);
            if(ser != null)
                Slides = await sd.LoadSlidesAsync(sermonId, ser.SlideLink);
        }


    }
}
