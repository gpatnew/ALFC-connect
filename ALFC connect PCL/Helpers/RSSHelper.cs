using ALFCConnect.Common;
using ALFCConnect.Interfaces;
using ALFCConnect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;

namespace ALFCConnect.Helpers
{
    public class RssHelper
    {
        HttpClient client;

        public List<RssPost> EventItems { get; private set; }
        public List<RssSlide> SlideItems { get; private set; }
        public List<RssPost> PostItems { get; private set; }


        public RssHelper()
		{
			client = new HttpClient ();
		}

        public async Task<List<RssPost>> RefreshEventsAsync()
        {
            EventItems = new List<RssPost>();
            DataContractSerializer dcs = new DataContractSerializer(typeof(List<RssPost>));
            
            var uri = new Uri(string.Concat(Constants.BaseUrl, Constants.EventsPath));

            try
            {
               StringContent reqContent = new StringContent("");
                var response = await client.PostAsync(uri, reqContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    XmlReader reader = XmlReader.Create(uri.ToString());

                    EventItems = (List<RssPost>)dcs.ReadObject(reader);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return EventItems;
        }

        public async Task<List<RssSlide>> RefreshSlidesAsync()
        {
            SlideItems = new List<RssSlide>();

            var uri = new Uri(string.Concat(Constants.BaseUrl, Constants.SlidesPath));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    SlideItems = JsonConvert.DeserializeObject<List<RssSlide>>(content);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return SlideItems;
        }
    }
}
