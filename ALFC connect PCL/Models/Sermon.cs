using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;

namespace ALFCConnect.Models
{
    public class Sermon
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string PostId { get; set; }

        public string Author { get; set; }

        public DateTime PresentationDate { get; set; }

        public string SermonName { get; set; }

        public string SlideLink { get; set; }

        public string Passage { get; set; }

        public int Done { get; set; }

        public string AudioUrl { get; set; }

        
    }
}
