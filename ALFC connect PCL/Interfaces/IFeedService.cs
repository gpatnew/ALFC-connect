using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALConnect.Models;

namespace ALConnect.Interfaces
{
    interface IFeedService
    {
        Rss Feed { get; set; }

        Task Update();
    }
}
