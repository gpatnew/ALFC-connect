using ALFCConnect.Interfaces;
using ALFCConnect.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using ALFCConnect.Common;
using Newtonsoft.Json;
using ALFCConnect.Helpers;

namespace ALFCConnect.Data
{
    public class EventsData : IRssFeed
    {

       
        public IList<RssPost> All { get; set; }

        public EventsData()
        {
            
            //GetEvents();
        }

        public async Task<string> LoadAsync()
        {
            var message = "loaded events";
            var client = new HttpClient();

            var result = await client.GetStringAsync(string.Concat(Constants.BaseUrl, Constants.EventsPath));
            var listEvents = new List<RssPost>();
            if(listEvents.Count <= 0)
            {
                message = "using cached events";
                All = DefaultEvents();
            }
            return message;;
        }

        public async void GetEvents()
        {
            var helper = new RssHelper();
            All = await helper.RefreshEventsAsync();

            if(All.Count<=0)
            {
                All = DefaultEvents();
            }
        }

        private IList<RssPost> DefaultEvents()
        {
            string[] title = { "Services", "Go Mission", "SEAL Training", "Small Groups" };
            var list =  new List<RssPost>();
            for (int i = 0; i < 4; i++)
            {
                var post = new RssPost();
                post.Title = title[i];
                post.Link = string.Concat(Constants.BaseUrl, title[1]);
                post.Description = "something goes here";
                list.Add(post);
            }

            return list;
        }
    }
}
