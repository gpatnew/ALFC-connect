using ALFCConnect.Data;
using ALFCConnect.Models;
using System;
using System.Collections.Generic;

namespace ALFCConnect.ViewModels
{
    public class SermonsViewModel : BasePageViewModel
    {
        private List<Sermon> sermons;

        public List<Sermon> SermonsListItems
        {
            get { return sermons; }
            set
            {
                Set("SermonsListItems", ref sermons, value);
            }
        }

        public SermonsViewModel()
        {
            LoadSermons();
            PageTitle = "Current Sermons";
        }
         
        internal void LoadSermons()
        {
            SermonsListItems = new SermonsData().GetList(DateTime.Now.AddDays(-45));
        }

        public int AddSermon(int postId, string name)
        {
            var sermon = new Sermon { Id = 0, PostId = postId.ToString(), Author = "", SermonName = name, PresentationDate= DateTime.Now };
            var sd = new SermonsData();
           return  sd.Upsert(sermon);
        }
    }
}
