using SQLite.Net.Attributes;
using System;


namespace ALConnect.Models
{
    public class FeatureEvent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string DisplayDate { get; set;}
        public DateTime EndDate { get; set; }
        public int IsFeatured { get; set; }
        public bool IsVideo { get; set; }
        public string Link { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
