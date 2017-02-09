using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALConnect.Models
{
    public class AWSNotification
    {
        public string Author { get; set; }

        public string AudioUrl { get; set; }
        public string Bucket { get; set; }

        public DateTime? EndDate  { get; set; }

        public List<string> FileNames { get; set; }

        public bool IsVideo { get; set; }

        public string Message { get; set; }

        public DateTime StartDate { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public AWSNotification()
        {
            Type = "Feature";
            StartDate = DateTime.Now;
        }
        
    }
}
