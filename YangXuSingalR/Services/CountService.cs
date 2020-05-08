using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YangXuSignalR.Services
{
    public class CountService:ICountService
    {
        private int _count;
        public int GetLatestCount()
        {
            //return Task.Run(function: () => _count++);
            return _count++;
        }
    }
}
