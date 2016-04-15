using ALFCConnect.Data;
using Xamarin.Forms;

namespace ALFCConnect
{
	public class EventsPageCS : ContentPage
	{
        public EventsPageCS()
		{
			Title = "Events Page";
            var eventsData = new EventsData();
            //this.ItemsSource = eventsData.All;
            //var page = new ContentPage();
            //var box = new BoxView { Color = Color.Aqua };
            //page.Content = box;
            //this.Children.Add(page);
        }
	}
}
