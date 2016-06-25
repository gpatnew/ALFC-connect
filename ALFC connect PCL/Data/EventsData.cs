using ALFCconnect.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ALFCconnect.Common;
using ALFCconnect.Helpers;
using SQLite.Net;
using HtmlAgilityPack;
using System;
using System.Linq;

namespace ALFCconnect.Data
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
        }

        public async Task<string> LoadAsync()
        {
            var message = "load events";

            try
            {
                HtmlParser parser = new HtmlParser();
                var nodesTask = await parser.ParsingFeatured(Constants.BaseUrl);
                if (nodesTask.Count > 0) ClearData();
                foreach (HtmlNode nodeEvents in nodesTask)
                {
                    foreach (HtmlNode node in nodeEvents.ChildNodes)
                    {

                        if(node.Name == "p")
                        {
                            try
                            {
                                var linkNode = node.ChildNodes.FindFirst("a");
                                var link = linkNode != null ? linkNode.Attributes["href"].Value : Constants.BaseUrl;
                                var title = linkNode != null ? linkNode.Attributes["title"].Value : "ALFC Event";
                                
                                var eventdate = node.InnerText.Replace("\n", "|");
                                string[] dateTimeparts = eventdate.Split('|');
                                string  dateparts = dateTimeparts[1].Replace("at", "");
                                
                                var desc = string.Empty;
                                //Put this feature into DB
                                AddFeatureEvent(title, link, dateTimeparts[1], desc);
                            }
                            catch (Exception e)
                            { }
                        
                        

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

        private void AddFeatureEvent(string title, string link, string eventDate, string desc)
        {
            var item = new FeatureEvent();
            item.Id = 0;
            item.Title = title;
            item.EventDate = eventDate;
            item.Link = link;
            item.Description = desc;
            database.Insert(item);
        }

        private void ClearData()
        {
            lock (locker)
            {
                database.DeleteAll<FeatureEvent>();
            }
        }

        public IEnumerable<FeatureEvent> GetItems()
        {
            lock (locker)
            {
                var listFeatures = (from i in database.Table<FeatureEvent>() select i).ToList();
                if( listFeatures.Count == 0)
                {
                    listFeatures = new List<FeatureEvent>();
                    listFeatures.Add(new FeatureEvent { Title = "Welcome", Id = 0, Link = "http://www.alfc.us", EventDate=DateTime.Now.ToString() });
                    return listFeatures as IEnumerable<FeatureEvent>;
                }
                return listFeatures;
            }
        }

    }
}
