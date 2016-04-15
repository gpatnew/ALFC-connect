using ALFCConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALFCConnect.Interfaces
{
    public interface IRssHelper
    {
          List<RssPost> GetList(string url);
    }
}
