using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALFCConnect.Models
{
    public class SermonDisplay : Sermon
    {
        public List<SermonSlide> Slides { get; set; }

        public SermonDisplay(int id, string author, DateTime presentationDate, string sermonName, List<SermonSlide> slides, string audioUrl)
        {
            Id = id;
            Author = author;
            PresentationDate = presentationDate;
            SermonName = sermonName;
            Slides = slides;
            Done = 0;
            AudioUrl = audioUrl;
        }

        internal object GetSlide(object commandParameter)
        {
            return this.Slides.Find(x => x.Id == (int)commandParameter);
        }
    }
}
