using ALConnect.Helpers;
using ALConnect.Common;
using ALConnect.Models;

using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Xamarin.Forms;

namespace ALConnect.Data
{
    public class SlidesData
    {
        static object locker = new object();
        SQLiteConnection database;
        private static List<EventSlide> sermonList = new List<EventSlide>();

        public SlidesData()
        {
            database = DataConnection.Instance().DataBase;
            database.CreateTable<EventSlide>();
        }
        public async Task<string> LoadAsync()
        {
            var message = "loaded slides";

            try
            {
                HtmlParser parser = new HtmlParser();
                var nodesTask = await parser.ParsingSlides(Constants.BaseUrl);
                if (nodesTask.Count > 0) ClearData();
                foreach (HtmlNode node in nodesTask)
                {
                    if (node.HasChildNodes)
                    {
                        
                        var slideId = node.Attributes["id"].Value;
                        var linkNode = node.ChildNodes.FindFirst("a");
                        var link = linkNode != null ? linkNode.Attributes["href"].Value : Constants.BaseUrl;
                        var imageNode = node.ChildNodes.FindFirst("img");
                        var imageUrl = imageNode != null ? imageNode.Attributes["src"].Value : "";
                        var title = imageNode != null ? imageNode.Attributes["alt"].Value : "ALFC Info";
                        //Put this slide into DB
                        AddEventSlide(slideId, imageUrl, link, title);
                    }
                }

            }
            catch (Exception e)
            {
                message = e.Message;
            }

              return message;
        }

        public IEnumerable<EventSlide> GetItems()
        {
            lock (locker)
            {
                var slides = (from i in database.Table<EventSlide>() select i).ToList();

                if(slides.Count == 0)
                {
                    slides.Add(new EventSlide { Title = "Welcome", Id = 0, ImageUrl = "http://4.bp.blogspot.com/-nqlRlQkLpmA/T9tzmJCVMNI/AAAAAAAABlc/FyQMyDegG-k/s1600/Jesus+em+medita%C3%A7%C3%A3o.jpg", Link = "http://www.alfc.us" });
                    }
                return slides;
            }
        }

        private void ClearData()
        {
            lock (locker)
            {
                database.DeleteAll<EventSlide>();
            }
        }

        private void AddEventSlide(string slideId, string imageUrl, string link, string title)
        {
            var ms = new EventSlide { SlideId = slideId, ImageUrl = imageUrl, Link = link, Title = title };
            SaveItem(ms);
        }

        public int SaveItem(EventSlide item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item); 
                }
            }
        }
    }
}
