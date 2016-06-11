using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALFCConnect.Models
{
    public class SermonSlide
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int SermonId { get; set; }

        public string Title { get; set; }

        public string Scripture { get; set; }
        

        public string Message { get; set; }

        public string Note { get; set; }

        public string ImageUrl { get; set; }

        public bool ImageIsVisible
        {
            get { return !string.IsNullOrEmpty(ImageUrl); }

          
        }

        //    public bool ScriptureIsVisible
        //    {
        //        get { return !string.IsNullOrEmpty(Scripture); }
        //    }
    }
}
