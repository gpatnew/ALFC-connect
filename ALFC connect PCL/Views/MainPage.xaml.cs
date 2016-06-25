using ALFCConnect.Views;
using ALFCConnect.Models;
using System;
using Xamarin.Forms;
using ALFCConnect.ViewModels;

namespace ALFCConnect
{
	public partial class MainPage : MasterDetailPage
	{
		public MainPage ()
		{
			InitializeComponent();
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
                else
                {
                    var detailPage = new NavigationPage((Page)Activator.CreateInstance(item.TargetType)); ;
                    
                    Detail = detailPage;
                }
                    masterPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
	}
}
