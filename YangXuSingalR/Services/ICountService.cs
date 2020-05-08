using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YangXuSignalR.Services
{
    public interface ICountService
    {
        int GetLatestCount();
    }
}
