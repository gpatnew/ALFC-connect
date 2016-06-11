using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ALFCConnect.Models;

using ALFCConnect;
using SQLite.Net;
using Xamarin.Forms;
using ALFCConnect.Helpers;
using ALFCConnect.Common;
using HtmlAgilityPack;

namespace ALFCConnect.Data
{
    public class SermonsData 
    {
        static object locker = new object();
        SQLiteConnection database;
        private static List<Sermon> sermonList = new List<Sermon>();
        private static List<SermonSlide> slides = new List<SermonSlide>();
        private string sortDir = "desc";
        public SermonsData()
        {
            database = DataConnection.Instance().DataBase;
            database.CreateTable<Sermon>();
            database.CreateTable<SermonSlide>();
        }

        internal void SetSermonDone(int sermonId)
        {
            var sermon = GetItem(sermonId);
            sermon.Done = 1;
            Upsert(sermon);
        }

        public async Task<string> LoadAsync()
        {
            var message = "Loading Sermons complete";

            try
            {
                HtmlParser parser = new HtmlParser();
                var nodesTask = await parser.ParsingSermons(Constants.BaseUrl  + Constants.SermonsPath);
                //if (nodesTask.Count > 0) ClearData();
                foreach (HtmlNode node in nodesTask)
                {
                    if (node.HasChildNodes)
                    {
                        var st = node.InnerHtml;
                        var articlesNodes = node.Descendants().Where(x => (x.Name == "article" && x.Attributes["id"] != null && x.Attributes["id"].Value.Contains("post"))).ToList();
                        if(articlesNodes.Count > 0)
                        {

                            for (int i = 0; i < articlesNodes.Count; i++)
                            {
                                var article = articlesNodes[i];
                                var postId = article.Attributes["id"].Value;
                                var h1Node = article.ChildNodes.FindFirst("h1");
                                var titleNode = h1Node.ChildNodes.FindFirst("a");
                                var title = titleNode != null ? titleNode.Attributes["title"].Value : "ALFC Info Sermon";
                                var dateNode = article.ChildNodes.FindFirst("span");
                                var sermonDate = dateNode != null ? dateNode.InnerText : "1/1/2016";
                                var authorNodes = article.Descendants().Where(s => (s.Name == "span" && s.Attributes["class"] != null && s.Attributes["class"].Value == "preacher_name")).ToList();
                                var authorNode = authorNodes[0].ChildNodes.FindFirst("a");
                                var author = authorNode != null ? authorNode.InnerText : "pastor";
                                var listenNode = article.Descendants().Where(li => (li.Name == "li" && li.Attributes["class"] != null && li.Attributes["class"].Value == "listen")).ToList();
                                var audioUrl = listenNode != null && listenNode.Count > 0 ? listenNode[0].ChildNodes.FindFirst("a").Attributes["href"].Value : "";
                                var passageNode = article.Descendants().Where(sp => (sp.Name == "span" && sp.Attributes["class"] != null && sp.Attributes["class"].Value == "bible_passage")).ToList();
                                var passage = passageNode != null && passageNode.Count > 0 ? passageNode[0].InnerText.Remove(0, 12) : "";

                                var sermon = GetByPostId(postId);


                                string[] dateparts = sermonDate.Split('/');
                                int dateYear = Convert.ToInt16(dateparts[2].Substring(0, 2));
                                int dateMonth = Convert.ToInt16(dateparts[0]);
                                int dateDay = Convert.ToInt16(dateparts[1]);
                                dateYear += 2000;

                                sermon.SermonName = title;
                                sermon.Author = author;
                                sermon.PostId = postId;
                                sermon.Passage = passage;
                                sermon.AudioUrl = audioUrl;
                                
                                try
                                {
                                    sermon.PresentationDate = new DateTime(dateYear, dateMonth, dateDay);
                                }
                                catch { }
                                //Put this Sermon into DB
                                var sermonId = Upsert(sermon);

                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                message = e.Message;
            }

            return message;
        }

        public async Task<List<SermonSlide>> LoadSlidesAsync(int sermonId, string sermonName)
        {

            slides = (List<SermonSlide>)GetSlides(sermonId);
            if(slides.Count > 0)
            {
                return slides;
            }
            try
            {
                var webUrl = string.Format("{0}/sermons/{1}/", Constants.BaseUrl, sermonName.Replace(' ', '-'));
                HtmlParser parser = new HtmlParser();
                var nodesTask = await parser.ParsingSermonSlides(webUrl.ToLower());
                FetchFalseSlides(sermonId); 
            }
            catch (Exception e)
            {
                FetchErrorSlides(sermonId);
            }
                return slides;
        }

        
        internal List<Sermon> GetList(DateTime dateTime)
        {
            sermonList =  (List<Sermon>)GetItems();

            return Sort(sermonList, s => s.PresentationDate, ref sortDir);
            
        }

        public  IEnumerable<Sermon> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<Sermon>() select i).OrderBy(x => x.PresentationDate).ToList();
            }
        }

        private List<T> Sort<T, TKey>(List<T> list, Func<T, TKey> sorter, ref string direction)
        {
            if(direction == "asc")
            {
                list = list.OrderBy(sorter).ToList();
                direction = "desc";
            }
            else
            {
                list = list.OrderByDescending(sorter).ToList();
                direction = "asc";
            }
            return list;


        }
        public Sermon GetByPostId(string postId)
        {
            var sermon = new Sermon();
            lock (locker)
            {
                sermon = database.FindWithQuery<Sermon>(string.Format("SELECT * FROM [Sermon] WHERE [PostId] = '{0}'", postId));    
            }
            return sermon != null ? sermon : new Sermon();
        }

        public IEnumerable<SermonSlide> GetSlides(int sermonId)
        {
            lock (locker)
            {
                return database.Query<SermonSlide>(string.Format("SELECT * FROM [SermonSlide] WHERE [SermonId] = {0}", sermonId));
            }
        }

        public Sermon GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<Sermon>().FirstOrDefault(x => x.Id == id);
            }
        }
        public int Upsert(Sermon item)
        {
            lock (locker)
            {
                var sermon = item;
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    var sermonId = database.Insert(item);
                    var id = item.Id;
                    return id;
                }
            }
        }

        public int SaveSlideItem(SermonSlide item)
        {
            
            if (item.Id != 0)
            {
                database.Update(item);
            }
            else
            {
                database.Insert(item);
                
            }
            return item.Id;
        }
        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<Sermon>(id);
            }
        }

        private void FetchFalseSlides(int sermonId)
        {
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 1", ImageUrl = "http://www.quotesforthemind.com/wp-content/uploads/2013/02/Inspirational-Daily-Quotes-Scriptures-Verses-and-passages-from-the-Holy-Bible-Online.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 2", ImageUrl = "http://freebibleverses.org/bible_verses/image/Romans_8.39_Bible_Verse.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 3", ImageUrl = "http://freebibleverses.org/bible_verses/image/Romans_8.34_Bible_Verse.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 4", ImageUrl = "http://freebibleverses.org/bible_verses/image/1_Peter_5.6-7_Bible_Verse.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 5", ImageUrl = "" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 6", ImageUrl = "http://freebibleverses.org/bible_verses/image/2_Timothy_4.8_Bible_Verse.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 7", ImageUrl = "" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 8", ImageUrl = "http://freebibleverses.org/bible_verses/image/Ephesians_17_Bible_Verse.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 9", ImageUrl = "" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 10", ImageUrl = "" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 11", ImageUrl = "" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 12", ImageUrl = "" });
        }

        private void FetchErrorSlides(int sermonId)
        {
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 1", ImageUrl = "http://www.turnbacktogod.com/wp-content/uploads/2008/09/cross-of-christ-0101.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 2", ImageUrl = "http://4.bp.blogspot.com/-nqlRlQkLpmA/T9tzmJCVMNI/AAAAAAAABlc/FyQMyDegG-k/s1600/Jesus+em+medita%C3%A7%C3%A3o.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 3", ImageUrl = "http://freebibleverses.org/bible_verses/image/Romans_8.34_Bible_Verse.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 4", ImageUrl = "http://freebibleverses.org/bible_verses/image/1_Peter_5.6-7_Bible_Verse.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 5", ImageUrl = "" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 6", ImageUrl = "http://freebibleverses.org/bible_verses/image/2_Timothy_4.8_Bible_Verse.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 7", ImageUrl = "" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 8", ImageUrl = "http://freebibleverses.org/bible_verses/image/Ephesians_17_Bible_Verse.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 9", ImageUrl = "" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 10", ImageUrl = "" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 11", ImageUrl = "" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, Message = "", Title = "Slide 12", ImageUrl = "" });

        }

    }
}
