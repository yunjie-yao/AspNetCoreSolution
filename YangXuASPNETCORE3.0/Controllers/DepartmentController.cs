using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YangXuASPNETCORE3._0.Models;
using YangXuASPNETCORE3._0.Services;

namespace YangXuASPNETCORE3._0.Controllers
{
    public class DepartmentController:Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IOptions<ThreeOptions> _threeOptions;

        public DepartmentController(IDepartmentService departmentService,IOptions<ThreeOptions> threeOptions)
        {
            _departmentService = departmentService;
            _threeOptions = threeOptions;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Department Index";
            var departments = await _departmentService.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Title = "Add Department";
            return View(new Department());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department model)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.Add(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
