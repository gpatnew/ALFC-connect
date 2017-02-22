using ALConnect.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALConnect.Common;
using ALConnect.Helpers;
using SQLite.Net;
using HtmlAgilityPack;
using System;
using System.Linq;

namespace ALConnect.Data
{
    public class EventsData 
    {

        static object locker = new object();
        SQLiteConnection database;
        private static List<FeatureEvent> events = new List<FeatureEvent>();
        
        public EventsData()
        {
            database = DataConnection.Instance().DataBase;
            database.CreateTable<FeatureEvent>();
            //ClearData();
        }

        public async Task<string> LoadAsync()
        {
            var message = "load events";

            try
            {
                HtmlParser parser = new HtmlParser();
                var nodesTask = await parser.ParsingEvents(Constants.BaseUrl);
                if (nodesTask.Count > 0) ClearData();
                foreach (HtmlNode nodeEvents in nodesTask)
                {
                    foreach (HtmlNode node in nodeEvents.ChildNodes)
                    {
                        if(node.Name == "ul")
                        {
                            var listNode = node;
                            foreach (var cn in listNode.ChildNodes)
                            {
                                try
                                {
                                    var linkNode = cn.ChildNodes.FindFirst("a");
                                    var link = linkNode != null ? linkNode.Attributes["href"].Value : Constants.BaseUrl;
                                    var title = linkNode != null ? linkNode.InnerText : "ALFC Event";
                                
                                    var eventdateNode = cn.ChildNodes.FindFirst("div");
                                    var eventDate = eventdateNode.InnerText;
                                    var startDate = parser.ParseStartDate(eventDate);
                                    var desc = string.Empty;
                                    //Put this feature into DB
                                    AddFeatureEvent(title, link, eventDate,  desc, startDate, 0);
                                }
                                catch (Exception e)
                                {
                                        var msg = e.Message;
                                }
                            }
                        }
                    }
                }
                    message = "done";
            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return message;
        }

        internal void AddFeatureEvent(AWSNotification note)
        {
             var filename = string.Format("{0}{1}/{2}/{3}", Constants.S3Path,Constants.Bucket, Constants.FeaturePath, note.FileNames[0]);
            var item = GetEventByName(note.Title, note.StartDate);
            item.IsFeatured = 1;
            item.Description = note.Message;
            item.DisplayDate = note.EndDate != null ? note.EndDate.ToString() : note.StartDate.ToString();
            item.EndDate = note.EndDate != null ? (DateTime)note.EndDate : note.StartDate;
            item.IsFeatured = 1;
           
            item.IsVideo = note.IsVideo;
            item.Url = !note.IsVideo ? filename : note.FileNames[0];
            item.Link = note.AudioUrl;
            item.StartDate = note.StartDate;
            item.Title = note.Title;

            if (item.Id > 0)
                database.Update(item);
            else
                database.Insert(item);
        }

       
        public void AddFeatureEvent(string title, string link, string endDate, string desc, DateTime startDate, int isFeatured)
        {
            var item = GetEventByName(title, startDate);
            
            item.Title = title;
            item.DisplayDate = endDate;
            item.EndDate = startDate;
            item.Link = link;
            item.Description = desc;
            item.IsVideo = false;
            item.IsFeatured = isFeatured;
            item.StartDate = startDate;
            var id = database.Insert(item);
        }

        private void ClearData()
        {
            lock (locker)
            {
                database.Execute("DELETE FROM FeatureEvent WHERE IsFeatured = 0");
            }
        }

        public IEnumerable<FeatureEvent> GetEvents(bool featured)
        {
            lock (locker)
            {
                var fi = featured ?  1: 0;
                var currentDate = DateTime.Now;
                var listFeatures = new List<FeatureEvent>();
                var listFeatureResults = (from i in database.Table<FeatureEvent>() select i).ToList(); 
                
                foreach (var item in listFeatureResults)
                {

                    if (item.IsFeatured == fi  && item.StartDate >= currentDate)
                    {
                        listFeatures.Add(item);
                    }
                }
                if( listFeatures.Count == 0 && !featured)
                {
                    listFeatures.Add(new FeatureEvent { Title = "Welcome", Id = 0, Link = "http://www.alfc.us", EndDate=DateTime.Now, DisplayDate= string.Format("{0}", DateTime.Now.ToString("MMM dd")) });
                }
                if (listFeatures.Count == 0 && featured)
                {
                    listFeatures.Add(new FeatureEvent { Title = "Welcome", Id = 0, Link = "http://www.alfc.us", EndDate = DateTime.Now.AddMonths(1), DisplayDate = string.Format("{0}", DateTime.Now.ToString("MMM dd")), Url = "https://s3-us-west-2.amazonaws.com/alcmobileapp/featureitem/newlogopacGreen.png"   });   
                }
                return listFeatures;
            }
        }

        public IEnumerable<FeatureEvent> GetEvents(bool featured, bool isVideo)
        {
            lock (locker)
            {
                var fi = featured ? 1 : 0;
                var currentDate = DateTime.Now;
                var listFeatures = new List<FeatureEvent>();
                var listFeatureResults = (from i in database.Table<FeatureEvent>() select i).ToList();

                foreach (var item in listFeatureResults)
                {
                    if (item.IsFeatured == fi && item.IsVideo)
                    {
                        listFeatures.Add(item);
                    }
                }
                
                return listFeatures;
            }
        }

        private FeatureEvent GetEventByName(string title, DateTime startDate)
        {
            var fe = new FeatureEvent();
            lock (locker)
            {
                
                var dbList = database.Query<FeatureEvent>("SELECT * FROM [FeatureEvent] WHERE Title = ? AND StartDate = ?", title, startDate);
                var dbListCount = dbList.Count;

                if(dbListCount > 0)
                {
                    fe = dbList[0];
                }
                //fe = database.Table<FeatureEvent>().FirstOrDefault(x => (x.Title == title && x.StartDate == startDate));
                 //database.FindWithQuery<FeatureEvent>(string.Format("SELECT * FROM [FeatureEvent] WHERE Title = '{0}' AND StartDate ='{1}'", title, startDate));
            }
            
            return fe!= null ? fe : new FeatureEvent();
        }

        internal FeatureEvent GetFeaturedItem()
        {
            var feature = new FeatureEvent();
            lock (locker)
            {
                var fts = (from i in database.Table<FeatureEvent>() select i).OrderBy(x => x.DisplayDate).ToList();
                //database.Find<FeatureEvent>("SELECT * FROM [FeatureEvent] WHERE [isFeatured] = 1 ");
                feature = database.FindWithQuery<FeatureEvent>(string.Format("SELECT * FROM [FeatureEvent] WHERE [isFeatured] = 1 and StartDate <= '{0}' Order by EndDate Limit 1", DateTime.Now.AddDays(-1).ToString() ));
            }
            return feature != null ? feature : new FeatureEvent();
        }
    }
}
