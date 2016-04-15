using ALFCConnect.Common;
using Plugin.Connectivity;
using ALFCConnect.Data;
using Xamarin.Forms;

namespace ALFCConnect
{
	public partial class EventsPage : ContentPage
	{
        private string pageTitle = "";
        private string featuredItem = "";
        public string PageTitle
        {
            get { return pageTitle; }
            set { pageTitle = value; OnPropertyChanged(); }
        }

        public string FeaturedItem
        {
            get { return featuredItem; }
            set { featuredItem = value; OnPropertyChanged(); }
        }
        public EventsPage()
        {
            PageTitle = "Yahoo";
            FeaturedItem = "This is what I like";
            var ED = new EventsData();
            //ItemsSource = ED.All;
            InitializeComponent();
           // this.Children.Add(new ContentPage { Content = new StackLayout { BackgroundColor = AppColors.AlfcOrange }});
		}

        private void CheckConnectivityStatus()
        {
            var connected = CrossConnectivity.Current.IsConnected;
        }
	}
}

