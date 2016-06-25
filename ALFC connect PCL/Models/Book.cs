
using ALFCconnect.Common;
using ALFCconnect.Interfaces;
using SQLite.Net.Attributes;

namespace ALFCconnect.Model
{
    public class Book : IDataListItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name
        {
            get;
            set;
        }

        public string Value { get; set; }

        public int ChapterCount
        {
            get;
            set;
        }

        public string OriginalLanguage
        { get; set; }

        public Book(int id, string name, string value, string original, int count)
        {
            
            this.Id = id;
            this.Name = name;
            this.Value = value;
            this.ChapterCount = count;
            this.OriginalLanguage = original;
        }

        public string Url()
        {

            return string.Format("{0}/search?{1}", Constants.SearchURLbase, Name);
        }

        public bool IsSelected
        {
            get { return false; }
        }
    }
}
