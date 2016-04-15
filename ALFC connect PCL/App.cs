using ALFCConnect.Data;
using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace ALFCConnect
{
    
	public class App : Application
	{
        private string eventsMessage = "";

        public string EventsMessage
        {
            get { return eventsMessage; }
            set { eventsMessage = value; OnPropertyChanged(); }
        }
        public string SlidesMessage { get; set; }

        public App ()
		{
            MainPage = new ALFCConnect.MainPage();
		}

		protected override void OnStart ()
		{
            // Handle when your app starts

            EventsMessage = "Loading...";
            BuildCachedSlides();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        private async void BuildCachedSlides()
        {
            var dataStore = new DataStoreHelper();
            EventsMessage = await dataStore.LoadEventsAsync();
        }

    }
}

