using ALFCConnect.Common;
using ALFCConnect.Models;
using ALFCConnect.Views;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ALFCConnect
{
	public partial class MasterPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		public MasterPage ()
		{
            this.BackgroundColor = AppColors.White;
			InitializeComponent ();

			var masterPageItems = new List<MasterPageItem> ();
            
			masterPageItems.Add (new MasterPageItem {
				Title = "ALFC Connect",
				IconSource = "events.png",
				TargetType = typeof(EventsPage),
                
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Sermons",
				IconSource = "sermons.png",
				TargetType = typeof(SermonsPage)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Groups",
				IconSource = "groups.png",
				TargetType = typeof(WebPage),
                CommandParameter = Constants.GroupsUrl
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Ministries",
                IconSource = "ministries.png",
                TargetType = typeof(WebPage),
                CommandParameter = Constants.MinistriesUrl
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Prayer",
                IconSource = "prayers.png",
                TargetType = typeof(WebPage),
                CommandParameter = Constants.PrayersUrl
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Giving",
                IconSource = "giving.png",
                TargetType = typeof(WebPage),
                CommandParameter = Constants.DonateUrl
            });
			listView.ItemsSource = masterPageItems;
            listView.BackgroundColor = AppColors.White;
		}
	}
}
