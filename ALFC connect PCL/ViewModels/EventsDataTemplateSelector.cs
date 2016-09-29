using ALConnect.Models;
using Xamarin.Forms;

namespace ALConnect.ViewModels
{
    public class EventsDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ValidTemplate { get; set; }

        public DataTemplate CurrentTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            //return CurrentTemplate;
             return ((FeatureEvent)item).Id == 1 ? CurrentTemplate :  ValidTemplate;
        }
    }
}
