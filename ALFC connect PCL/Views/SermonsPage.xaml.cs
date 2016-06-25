using Xamarin.Forms;
using ALFCconnect.ViewModels;
using ALFCconnect.Models;
using System;

namespace ALFCconnect
{
	public partial class SermonsPage : ContentPage
	{
		public SermonsPage ()
		{
			InitializeComponent ();
        }
        
        public void OnRefreshSermons(object sender, EventArgs e)
        {
            var list = (ListView)sender;
            if(list.IsRefreshing)
            { 
                var svm = (SermonsViewModel)this.BindingContext;
                svm.LoadSermons();
                list.IsRefreshing = false;
            }
        }
        

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (SermonsList.SelectedItem != null)
            {
                var currentSermon = (Sermon)e.SelectedItem;
                LoadPage(currentSermon.Id, currentSermon.SermonName);
            }
        }

        private async void LoadPage(int id, string sermonName)
        {
            var slidePage = new SermonSlidePage(id, sermonName);
            var mv = new SermonSlidesViewModel(id, sermonName);
            slidePage.BindingContext = mv;
            SermonsList.SelectedItem = null;
            await Navigation.PushModalAsync(slidePage);
        }

        private void Add_Clicked(object sender, EventArgs e)
        {
            var postid = DateTime.Now.Year + DateTime.Now.DayOfYear;
            var name = string.Format("My Notes for {0}-{1}-{2}", DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Year);
            SermonsViewModel svm = (SermonsViewModel)this.BindingContext;
            LoadPage(svm.AddSermon(postid, name), name);   
        }

    }
}

