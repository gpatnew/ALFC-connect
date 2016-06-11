using ALFCConnect.Data;
using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace ALFCConnect
{
    
	public class App : Application
	{
        private string eventsMessage = "";
        static SermonsData database;
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
            BuildCaches();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        private async void BuildCaches()
        {
            var dataStore = new DataStoreHelper();
            SlidesMessage = await dataStore.LoadSlidesAsync();
            SlidesMessage = await dataStore.LoadEventsAsync();
            EventsMessage = await dataStore.LoadSermonsAsync();
            
        }

        public static SermonsData Database
        {
            get
            {
                if (database == null)
                {
                    database = new SermonsData();
                }
                return database;



            }
        }
    }
}

