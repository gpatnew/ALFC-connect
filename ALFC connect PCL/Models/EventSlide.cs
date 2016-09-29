using SQLite.Net.Attributes;


namespace ALConnect.Models
{
    public class EventSlide
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string SlideId { get; set; }

        public string ImageUrl { get; set; }

        public string Link { get; set; }

        public string Title { get; set; }
    }


}
