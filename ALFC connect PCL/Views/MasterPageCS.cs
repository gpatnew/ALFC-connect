using ALFCConnect.Models;
using ALFCConnect.Views;
using ALFCConnect.Common;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ALFCConnect
{
	public class MasterPageCS : ContentPage
	{
		public ListView ListView { get { return listView; } }

		ListView listView;

		public MasterPageCS ()
		{
            //var ministriesPage = new WebPage("men");
			var masterPageItems = new List<MasterPageItem> ();
			masterPageItems.Add (new MasterPageItem {
				Title = "Events",
				IconSource = "Events.png",
				TargetType = typeof(EventsPageCS)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Giving",
				IconSource = "giving.png",
				TargetType = typeof(SermonsPageCS)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Reminders",
				IconSource = "reminders.png",
				TargetType = typeof(ReminderPageCS)
			});
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Sermons",
                IconSource = "sermons.png",
                TargetType = typeof(SermonsPageCS)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Ministries",
                IconSource = "ministries.png",
                TargetType = typeof(WebPage),
                CommandParameter = string.Concat(Constants.BaseUrl, "men")
            });
			listView = new ListView {
				ItemsSource = masterPageItems,
				ItemTemplate = new DataTemplate (() => {
					var imageCell = new ImageCell ();
					imageCell.SetBinding (TextCell.TextProperty, "Title");
					imageCell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");
					return imageCell;
				}),
				VerticalOptions = LayoutOptions.FillAndExpand,
				SeparatorVisibility = SeparatorVisibility.None
			};

			Padding = new Thickness (0, 40, 0, 0);
			Icon = "hamburger.png";
			Title = "ALFC Connect";
			Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					listView
				}	
			};
		}
	}
}
