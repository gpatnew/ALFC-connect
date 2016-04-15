using ALFCConnect.Models;
using ALFCConnect.Views;
using System;
using Xamarin.Forms;

namespace ALFCConnect
{
	public class MainPageCS : MasterDetailPage
	{
		MasterPageCS masterPage;

		public MainPageCS ()
		{
			masterPage = new MasterPageCS ();
			Master = masterPage;
			Detail = new NavigationPage (new EventsPageCS ());

			masterPage.ListView.ItemSelected += OnItemSelected;

			if (Device.OS == TargetPlatform.Windows) {
				Master.Icon = "swap.png";
			}
		}

		void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                if (item.TargetType == typeof(WebPage))
                {
                    Page page = new WebPage(item.CommandParameter);
                    Detail = new NavigationPage(page);
                }
                else { 
                    Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                }
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
            
		}
	}
}
