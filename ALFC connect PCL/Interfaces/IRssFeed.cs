using ALFCConnect.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ALFCConnect.Interfaces
{
    public interface IRssFeed
    {
        Task<string> LoadAsync();
    }
}
