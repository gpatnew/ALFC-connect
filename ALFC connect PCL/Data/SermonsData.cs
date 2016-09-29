using ALConnect.Common;
using ALConnect.Helpers;
using ALConnect.Models;
using HtmlAgilityPack;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
//using Windows.Web.Syndication;

namespace ALConnect.Data
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

            //var aws = new AWSHelper();
            //var list = aws.SQSReader();
            var processor = new MessageProcessor();
            var token = new CancellationToken();
            var tl = processor.StartListener(token);
            try
            {
                string url = string.Concat(Constants.DevBaseUrl, Constants.SermonsPath);
                RssHelper helper = new RssHelper(url);
                await helper.Update();

                var sermon = new Sermon();
                //reader.Close();
                foreach (RssItem item in helper.Feed.Channel.Items)
                {
                    sermon.SermonName = item.Title;
                    var permalink = item.Guid.Split('&');
                    sermon.PostId = permalink.Length > 0 ? permalink[1].Replace("p=", "") : string.Empty;
                    sermon.SlideLink = item.Link;
                    sermon.PresentationDate = item.PubDate;
                    sermon.AudioUrl = item.Enclosure.Url;
                    var r = item.Enclosure.Type;


                var sermonExist = GetByPostId(sermon.PostId);
                    sermon.Id = sermonExist.Id;

                HtmlParser parser = new HtmlParser();
                var nodesTask = await parser.ParsingSermons(sermon.SlideLink);

                foreach (HtmlNode node in nodesTask)
                {
                    if (node.HasChildNodes)
                    {
                        var st = node.InnerHtml;

                        var articlesNodes = node.Descendants().Where(x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("entry-content"))).ToList();
                        if (articlesNodes.Count > 0)
                        {

                            for (int i = 0; i < articlesNodes.Count; i++)
                            {
                                                var article = articlesNodes[i];
                                //                var postId = article.Attributes["id"].Value;
                                //                var h1Node = article.ChildNodes.FindFirst("h1");
                                var passageNode = article.Descendants().Where(s => (s.Name == "span" && s.Attributes["class"] != null && s.Attributes["class"].Value == "bible_passage")).ToList();
                                var biblePassage = passageNode[0].InnerText.Replace("Bible Text:", "");
                                    //                var titleNode = h1Node.ChildNodes.FindFirst("a");
                                    //                var slideLink = titleNode != null ? titleNode.Attributes["href"].Value : "";
                                    //                var title = titleNode != null ? titleNode.Attributes["title"].Value : "ALFC Info Sermon";
                                    //                var dateNode = article.ChildNodes.FindFirst("span");
                                    //                var sermonDate = dateNode != null ? dateNode.InnerText : "1/1/2016";
                                var authorNodes = article.Descendants().Where(s => (s.Name == "span" && s.Attributes["class"] != null && s.Attributes["class"].Value == "preacher_name")).ToList();
                                var authorNode = authorNodes[0].ChildNodes.FindFirst("a");
                                var author = authorNode != null ? authorNode.InnerText : "pastor";
                                    //                var listenNode = article.Descendants().Where(li => (li.Name == "li" && li.Attributes["class"] != null && li.Attributes["class"].Value == "listen")).ToList();
                                    //                var audioUrl = listenNode != null && listenNode.Count > 0 ? listenNode[0].ChildNodes.FindFirst("a").Attributes["href"].Value : "";
                                    //                var passageNode = article.Descendants().Where(sp => (sp.Name == "span" && sp.Attributes["class"] != null && sp.Attributes["class"].Value == "bible_passage")).ToList();
                                    //                var passage = passageNode != null && passageNode.Count > 0 ? passageNode[0].InnerText.Remove(0, 12) : "";

              

                                }
                        }
                            //Put this Sermon into DB
                            var sermonId = Upsert(sermon);
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

        public async Task<List<SermonSlide>> LoadSlidesAsync(int sermonId, string link)
        {

            try
            {
                var webUrl = link;
                HtmlParser parser = new HtmlParser();
                var nodesTask = await parser.ParsingSermonSlides(webUrl.ToLower());
                if(nodesTask.Count > 0)
                    slides = new List<SermonSlide>();
                var n = 0;
                foreach (HtmlNode node in nodesTask)
                {
                    var s = GetSlide(sermonId, n);
                    s.SermonId = sermonId;
                    s.ImageUrl = string.IsNullOrEmpty(node.Attributes["data-app_slide"].Value) ? "" : node.Attributes["data-app_slide"].Value;
                    var titleNode = node.ChildNodes.FindFirst("h3");
                    s.Title = titleNode != null ? titleNode.InnerText : "Slide";
                    s.Message = node.InnerText;
                    s.Id = SaveSlideItem(s);
                    slides.Add(s);
                    n++;
                }

                if (slides.Count <= 0)
                {
                    FetchPackageSlides(sermonId);
                }
                return slides;
                
            }
            catch (Exception e)
            {
                FetchPackageSlides(sermonId);
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

        public SermonSlide GetSlide(int sermonId, int index)
        {
            var sermonSlide = new SermonSlide();
            lock (locker)
            {
                sermonSlide = database.FindWithQuery<SermonSlide>(string.Format("SELECT * FROM [SermonSlide] WHERE [SermonId] = {0} and SlideIndex = {1}", sermonId, index));
            }
            return sermonSlide != null ? sermonSlide : new SermonSlide();
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
            /// Check if slide exists
            /// 
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

        private void FetchPackageSlides(int sermonId)
        {
            slides = new List<SermonSlide>();
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 0, Message = "", Title = "Slide 1", ImageUrl = "slide01.png" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 1, Message = "", Title = "Slide 2", ImageUrl = "slide02.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 2, Message = "", Title = "Slide 3", ImageUrl = "slide03.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 3, Message = "", Title = "Slide 4", ImageUrl = "slide04.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 4, Message = "", Title = "Slide 5", ImageUrl = "slide05.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 5, Message = "", Title = "Slide 6", ImageUrl = "slide06.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 6, Message = "", Title = "Slide 7", ImageUrl = "slide07.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 7, Message = "", Title = "Slide 8", ImageUrl = "slide08.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 8, Message = "", Title = "Slide 9", ImageUrl = "slide09.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 9, Message = "", Title = "Slide 10", ImageUrl = "slide10.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 10, Message = "", Title = "Slide 11", ImageUrl = "slide11.jpg" });
            slides.Add(new SermonSlide { Id = 0, SermonId = sermonId, SlideIndex = 11, Message = "", Title = "Slide 12", ImageUrl = "slide12.jpg" });

        }
        
    }
}
