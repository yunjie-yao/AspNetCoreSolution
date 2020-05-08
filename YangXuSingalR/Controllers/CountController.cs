using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace YangXuSignalR.Controllers
{
    [Route("api/count")]
    public class CountController:Controller
    {
        private readonly IHubContext<CountHub> _countHub;

        public CountController(IHubContext<CountHub> countHub)
        {
            _countHub = countHub;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _countHub.Clients.All.SendAsync("someFunc", new {random = "absd"});
            return Accepted(1);
        }
    }
}
