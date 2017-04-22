using ALConnect.Data;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ALConnect
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
        
        public int SelectedPageIndex { get; set; }
        public App ()
		{
            MainPage = new ALConnect.MainPage();
            
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
            try
            {
                EventsMessage = "Loading...";
                BuildCaches();
            }
            finally { EventsMessage = "Load error"; }
            
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
            // Handle when your app resumes
            EventsMessage = string.Empty;
        }

        private async void BuildCaches()
        {
            try
            { 
                var dataStore = new DataStoreHelper();
                EventsMessage = await dataStore.LoadEventsAsync();
                EventsMessage = await dataStore.LoadSermonsAsync();
            }
            catch(Exception e)
            {
                var strMSG = e.Message;
            }
        }

        private async Task ClearEventMessage()
        {
            await Task.Delay(4000);
            EventsMessage = string.Empty;
        }

        //public static SermonsData Database
        //{
        //    get
        //    {
        //        if (database == null)
        //        {
        //            database = new SermonsData();
        //        }
        //        return database;

        //    }
        //}
    }
}

