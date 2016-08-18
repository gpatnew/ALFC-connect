using ALFCconnect.Views;
using ALFCconnect.Models;
using System;
using Xamarin.Forms;
using ALFCconnect.ViewModels;

namespace ALFCconnect
{ 
	public partial class MainPage : MasterDetailPage
	{
		public MainPage ()
		{
            InitializeComponent();
            var mp = this.FindByName<MasterPage>("masterPage");
			mp.ListView.ItemSelected += OnItemSelected;

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
