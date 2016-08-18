using System;
using Plugin.Share;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using ALFCconnect.Models;

namespace ALFCconnect.Views
{
    public partial class SharePage : ContentPage
    {
        public SharePage()
        {
            InitializeComponent();
            this.BindingContextChanged += SharePage_BindingContextChanged; 
        }

        private void SharePage_BindingContextChanged(object sender, EventArgs e)
        {
            MakeMessage(sender, e);
        }

        public void MakeMessage(object sender, EventArgs e)
        {
            SermonSlide slide = (SermonSlide)this.BindingContext;

            MessageTitle.Text = shareTitleSwitch.IsToggled ? slide.Title : "I want to share my ALFC note:";

            
            if (shareMessageSwitch.IsToggled && shareMyNoteSwitch.IsToggled)
            {
                MessageText.Text = string.Format("{0}  {1}", slide.Message, slide.Note);
            }
            else if (shareMessageSwitch.IsToggled)
            { MessageText.Text = slide.Message; }
            else if (shareMyNoteSwitch.IsToggled)
            {
                MessageText.Text = slide.Note;
            }
            else { MessageText.Text = "write a message"; }

            if (shareImageSwitch.IsToggled)
                MessageText.Text += slide.ImageUrl;
        }
        async void OnReturnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void OnShareButtonClicked(object sender, EventArgs e)
        {
            await CrossShare.Current.Share(MessageText.Text, MessageTitle.Text);
            await Navigation.PopModalAsync();
        }
   }
}
