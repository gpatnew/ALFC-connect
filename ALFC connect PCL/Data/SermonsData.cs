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
            var message = "Loading sermons complete";
            AWSHelper awshelper = new AWSHelper();

            var isBucket = await awshelper.BucketExist();
            if(isBucket)
            {
                var notices = await awshelper.LoadNotifications();
                if(notices.Count > 0)
                    message = await ParseNotifications(notices);
            }
             return message;
        }

        private async Task<string> ParseNotifications(List<AWSNotification> notifications)
        {
            var result = ""; 
            foreach (var note in notifications)
            {
                if(note.Type.ToLower() == Constants.Featured)
                {
                    SaveFeaturedItem(note);
                }
                if(note.Type.ToLower() == Constants.Message)
                {
                    SaveWeeklyMessage(note);
                }
            }

            return result;
        }


        private void SaveFeaturedItem(AWSNotification note)
        {
            var eventData = new EventsData();
            eventData.AddFeatureEvent(note);
        }


        private void SaveWeeklyMessage(AWSNotification note)
        {
            var sermon = GetItem(note.Title);

            sermon.AudioUrl = note.AudioUrl;
            sermon.Author = note.Author;
            sermon.Passage = note.Message;
            sermon.PresentationDate = note.StartDate;
            sermon.SermonName = note.Title;

            var id = Upsert(sermon);
            var slideIndex = 1;
            foreach (var item in note.FileNames)
            {
                var imageuri = string.Format("{0}{1}/{2}/{3}/{4}", Constants.S3Path, Constants.Bucket, Constants.MessagePath, sermon.SermonName, item);
                var slide = GetSlide(id, slideIndex);
                slide.SermonId = id;
                slide.ImageUrl = imageuri;
                slide.Scripture = sermon.Passage;
                slide.SlideIndex = slideIndex;
                slide.Title = string.Format("{0} slide {1}", sermon.SermonName, slideIndex);
                SaveSlideItem(slide);
                slideIndex++;
            }

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
            //LoadMessages();
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
               
                var query = string.Format("SELECT * FROM [SermonSlide] WHERE SermonId = ?", sermonId);
                List<SermonSlide> sermonSlides = database.Query<SermonSlide>(query, sermonId);

                if (sermonSlides.Count > 0)
                    sermonSlide = sermonSlides.Find(ss => ss.SlideIndex == index);
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

        public Sermon GetItem(string title)
        {
            var sermon = new Sermon();
            lock (locker)
            {
                sermon = database.Table<Sermon>().FirstOrDefault(x => x.SermonName == title);
            }

            return sermon != null ? sermon : new Sermon();
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
