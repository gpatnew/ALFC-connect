using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALConnect.Data
{
    public class DataStoreHelper
    {
        public Task<string> LoadEventsAsync()
        {
            EventsData ed = new EventsData();
            return ed.LoadAsync();
        }

        

        public Task<string> LoadSermonsAsync()
        {
            SermonsData sd = new SermonsData();
            return sd.LoadAsync();
        }

    }
}
