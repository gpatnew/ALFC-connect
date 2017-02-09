using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace ALConnect.Helpers
{
    public class HtmlParser
    {
        private List<HtmlNode> nodes;

        public HtmlParser()
        {
            nodes = new List<HtmlNode>();
        }

       /// <summary>
       /// parse the main page slide info
       /// </summary>
       /// <param name="websiteUrl"></param>
       /// <returns></returns>
        public async Task<List<HtmlNode>> ParsingSlides(string websiteUrl)
        {
            var page = await FetchPage(websiteUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            return doc.DocumentNode.Descendants().Where(x => (x.Name == "li" && x.Attributes["id"] != null && x.Attributes["id"].Value.Contains("slide"))).ToList();
        }
        /// <summary>
        /// Parse Sermon list from webpage
        /// </summary>
        /// <param name="websiteUrl"></param>
        /// <returns></returns>
        public async Task<List<HtmlNode>> ParsingSermons(string websiteUrl)
        {
            var page = await FetchPage(websiteUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            return doc.DocumentNode.Descendants().Where(x => (x.Name == "article" && x.Attributes["id"] != null && x.Attributes["id"].Value.Contains("post-"))).ToList();
        }

        /// <summary>
        /// Parse Sermon Slides from webpage
        /// </summary>
        /// <param name="websiteUrl"></param>
        /// <returns></returns>
        public async Task<List<HtmlNode>> ParsingSermonSlides(string websiteUrl)
        {
            var page = await FetchPage(websiteUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);
            return doc.DocumentNode.Descendants().Where(x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("app_slide"))).ToList();
        }

        internal DateTime ParseStartDate(string eventDate)
        {
            char tab = '\u0009';
            var cleanEventDate = eventDate.Replace(tab.ToString(), "").Replace(Environment.NewLine,"");
            var start = DateTime.Now;
            if (string.IsNullOrEmpty(eventDate))
                return start;

            var year = DateTime.Now.Year;
            var month = String.Format("{0:MMM}", DateTime.Now);
            var nextMonth = String.Format("{0:MMM}", DateTime.Now.AddMonths(1));
            var isCurrentMonth = cleanEventDate.Contains(month);
            var day = isCurrentMonth ? cleanEventDate.Replace(month, "") : nextMonth.Replace(month, "");

            if (month == "Dec" && !isCurrentMonth)
            {
                year += 1;
            }

            DateTime.TryParse(string.Format("{0} {1} {2}", day, month, year), out start);
            return start;
        }

        public async Task<List<HtmlNode>> ParsingEvents(string websiteUrl)
        {
            var page = await FetchPage(websiteUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);//churchpack-column churchpack-one-third churchpack-column-last   churchpack-all
            return doc.DocumentNode.Descendants().Where(x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("churchpack-column-last"))).ToList();
        }
        private async Task<string> FetchPage(string websiteUrl)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetByteArrayAsync(websiteUrl);
            string page = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            return WebUtility.HtmlDecode(page);
        }
    }
}
