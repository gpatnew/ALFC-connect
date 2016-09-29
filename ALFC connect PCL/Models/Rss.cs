using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ALConnect.Models
{
    [XmlRoot("rss")]
    public class Rss
    {
        [XmlElement("channel")]
        public RssChannel Channel { get; set; }

        public Rss()
        {
            this.Channel = new RssChannel();
        }
    }

    public class RssChannel
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlIgnore]
        public DateTime LastBuildDate { get; set; }

        [XmlElement("lastBuildDate")]
        public string LastBuildDateString {
            get
            { return LastBuildDate.ToString(); }
            set
            {
                DateTime date;
                if(DateTime.TryParse(value, out date))
                {
                    this.LastBuildDate = date;
                }
            }
        }

        [XmlElement("item")]
        public List<RssItem> Items { get; set; }

        /// <summary>
        /// Channel Info
        /// </summary>
        public RssChannel()
        {
            this.Items = new List<RssItem>();
            this.LastBuildDate = DateTime.MinValue;
        }
    }

    public class RssItem
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("guid")]
        public string Guid { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlIgnore]
        public DateTime PubDate { get; set; }

        [XmlElement("pubDate")]
        public string PubDateString
        {
            get
            { return PubDate.ToString(); }
            set
            {
                DateTime date;
                if (DateTime.TryParse(value, out date))
                {
                    this.PubDate = date;
                }
            }
        }

        [XmlElement("enclosure")]
        public RssEnclosure Enclosure { get; set; }

        [XmlElement("category")]
        public List<string> Categories { get; set; }

    }

    public class RssEnclosure 
    {
        [XmlAttribute("url")]
        public string Url { get; set; }
        [XmlAttribute("length")]
        public int Length { get; set; }
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}
