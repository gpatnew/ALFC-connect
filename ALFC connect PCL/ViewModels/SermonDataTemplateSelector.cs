using ALConnect.Models;
using System;
using Xamarin.Forms;

namespace ALConnect.ViewModels
{
    public class SermonDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RegularTemplate { get; set; }

        public DataTemplate CurrentTemplate { get; set; }

        public DataTemplate DoneTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var sermon = (Sermon)item;
            DataTemplate useTemplate = RegularTemplate;

            if (sermon.PresentationDate >= DateTime.Now.AddDays(-7))
            {
                useTemplate = CurrentTemplate;
            }
            if(sermon.Done != 0)
            {
                useTemplate = DoneTemplate;
            }
            return useTemplate;
           
        }
    }
}
