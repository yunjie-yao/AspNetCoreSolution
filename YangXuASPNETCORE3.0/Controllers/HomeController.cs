using Microsoft.AspNetCore.Mvc;
using YangXuASPNETCORE3._0.Services;

namespace YangXuASPNETCORE3._0.Controllers
{
    public class HomeController:Controller
    {
        public HomeController(IClock clock)
        {

        }
    }
}
