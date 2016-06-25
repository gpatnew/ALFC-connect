using ALFCconnect.ViewModels;
using ALFCconnect.Views;
using System;
using Xamarin.Forms;

namespace ALFCconnect
{
    public partial class SermonSlidePage : CarouselPage
    {
        public SermonSlidePage(int sermonId, string sermonName)
        {
            InitializeComponent();
            this.CurrentPageChanged += SermonSlidePage_CurrentPageChanged;
        }

        private void SermonSlidePage_CurrentPageChanged(object sender, EventArgs e)
        {
            SaveSlides();
        }
        
        private void CurrentPage_Disappearing(object sender, EventArgs e)
        {
            SaveSlides();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            SaveSlides();
            await Navigation.PopModalAsync();
        }
        
        async void OnShareButtonClicked(object sender, EventArgs e)
        {
            SaveSlides();
            var sermonSlides = (SermonSlidesViewModel)BindingContext;
            var sharePage = new SharePage();
            var shareButton = (Button)sender;
            var slideindex = sermonSlides.Slides.FindIndex(s => s.Id == (int)shareButton.CommandParameter);
            
            sharePage.BindingContext = sermonSlides.Slides[slideindex];
            await Navigation.PushModalAsync(sharePage);
        }

        private void SaveSlides()
        {
            var sermonSlides = (SermonSlidesViewModel)BindingContext;
            sermonSlides.Save();
        }
    }
}
