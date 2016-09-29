using ALConnect.Common;
using ALConnect.Models;
using ALConnect.Views;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ALConnect
{
	public partial class MasterPage : ContentPage
	{
        
        public ListView ListView { get { return listView; } }

		public MasterPage ()
		{
            InitializeComponent ();
            this.BackgroundColor = AppColors.White;
			var masterPageItems = new List<MasterPageItem> ();
            
			masterPageItems.Add (new MasterPageItem {
				Title = "ALFC Connect",
				IconSource = "events.png",
				TargetType = typeof(EventsPage)
                
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
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Settings",
                IconSource = "settings.png",
                TargetType = typeof(SettingsPage),
                CommandParameter = "settingsdata",
                
            });
            listView.ItemsSource = masterPageItems;
            listView.BackgroundColor = AppColors.White;
		}
	}
}
