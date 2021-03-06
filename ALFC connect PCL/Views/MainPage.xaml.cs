﻿using ALConnect.Views;
using ALConnect.Models;
using System;
using Xamarin.Forms;
using ALConnect.ViewModels;

namespace ALConnect
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
           // SelectedPageIndex = 
            var item = e.SelectedItem as MasterPageItem;
			if (item != null)
            {
                if (item.TargetType == typeof(WebPage))
                {
                    if (Device.OS == TargetPlatform.iOS && item.Title == "Giving")
                    {
                        Device.OpenUri(new Uri(item.CommandParameter));
                    }
                    else
                    { 
                        Page page = new WebPage(item.CommandParameter);
                        Detail = new NavigationPage(page);
                    }
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
