﻿using ALFCConnect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ALFCConnect.Views
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
            //this.BindingContext = new UrlsViewModel(url);
            InitializeComponent();
            if (searchString != string.Empty && !searchString.Contains("http"))
            {
                WebUrl = string.Concat(baseURL, searchTerm);
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
                HorizontalOptions = LayoutOptions.Fill
            };


            return webView;
        }
    }
}
