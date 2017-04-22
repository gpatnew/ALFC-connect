using ALConnect.Common;

using Xamarin.Forms;

namespace ALConnect.Views
{
    public partial class WebPage : ContentPage
    {
        private string baseURL = Constants.BaseUrl;
        private string searchTerm = "";
        private string webUrl = "";
        public string WebUrl
        {
            get { return webUrl; }
            private set { webUrl = value; }
        }
        public WebPage(string searchString)
        {
           
            InitializeComponent();
            if (searchString != string.Empty && !searchString.Contains("http"))
            {
                WebUrl = string.Concat(baseURL, searchString);
            }
            else if(searchString.Contains("http"))
            {
                WebUrl = searchString;
            }
            else
            { 
                WebUrl = Constants.BaseUrl;
            }

            this.searchTerm = searchString.Replace(" &", ",");
            BuildContent();
        }

        private void BuildContent()
        {
            this.Content = new StackLayout
            {
                Children =
                {
                    BuildWebView()
                }
            };
        }
        private WebView BuildWebView()
        {
            WebView webView = new WebView
            {
                Source = new UrlWebViewSource
                {
                    Url = WebUrl
                },
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
                
            };
            webView.IsVisible = true;

            return webView;
        }
    }
}
