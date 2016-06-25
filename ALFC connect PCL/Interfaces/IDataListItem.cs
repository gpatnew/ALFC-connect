using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALFCconnect.Interfaces
{
    public interface IDataListItem
    {
        int Id { get; set; }
        string Name { get; set; }
        string Value { get; set; }
    }
}
