using ALConnect.Common;
using ALConnect.Interfaces;
using ALConnect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace ALConnect.Helpers
{
    public class RssHelper : IFeedService
    {
        HttpClient client;

        public string EndPoint { get; set; }
        public Rss Feed { get; set; }
        public RssHelper(string url)
		{
			client = new HttpClient ();
            this.Feed = new Rss();
            this.EndPoint = url;
        }
        
        public async Task Update()
        {
            Rss currentFeed;

            using (var reader = new StringReader(await client.GetStringAsync(EndPoint)))
            {
                
                var serializer = new XmlSerializer(typeof(Rss));
                currentFeed = serializer.Deserialize(reader) as Rss;
            }
            if (currentFeed != null) Feed = currentFeed;
        }
    }
}
