using ALFCConnect.Common;
using Xamarin.Forms;

namespace ALFCConnect
{
	public class SermonsPageCS : ContentPage
	{
		public SermonsPageCS ()
		{
            this.BackgroundColor = AppColors.White;
			Title = "Sermons Page";
			Content = new StackLayout { 
				Children = {
					new Label {
						Text = "Todo list data goes here",
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};
		}
	}
}
