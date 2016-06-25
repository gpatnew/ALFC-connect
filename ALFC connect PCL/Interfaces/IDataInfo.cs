using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALFCconnect.Interfaces
{
    public interface IDataInfo
    {
        List<IDataListItem> GetList();
    }
}
