﻿using SQLite.Net.Attributes;
using System;


namespace ALConnect.Models
{
    public class FeatureEvent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string EventDate { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Time { get; set; }
        public string Description { get; set; }

        public int IsFeatured { get; set; }
    }
}
