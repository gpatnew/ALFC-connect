using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALFCConnect.Interfaces
{
    public interface IDataInfo
    {
        List<IDataListItem> GetList();
    }
}
