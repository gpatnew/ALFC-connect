using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALFCConnect.Models
{
    public class RssPost
    {
        public DateTime PubDate { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Guid { get; set; }
        public string Description { get; set; }
    }
}
