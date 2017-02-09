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
        private static List<FeatureEvent> eventList = new List<FeatureEvent>();

        public SlidesData()
        {
            database = DataConnection.Instance().DataBase;
            
        }
      
        public IEnumerable<FeatureEvent> GetItems()
        {
            lock (locker)
            {
                var slides = (from i in database.Table<FeatureEvent>() select i).ToList();

                if(slides.Count == 0)
                {
                    slides.Add(new FeatureEvent { Title = "Welcome", Id = 0,  IsVideo= false,   Url = "http://4.bp.blogspot.com/-nqlRlQkLpmA/T9tzmJCVMNI/AAAAAAAABlc/FyQMyDegG-k/s1600/Jesus+em+medita%C3%A7%C3%A3o.jpg", Link = "http://www.alfc.us" });
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
