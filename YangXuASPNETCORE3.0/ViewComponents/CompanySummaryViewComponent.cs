﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YangXuASPNETCORE3._0.Services;

namespace YangXuASPNETCORE3._0.ViewComponents
{
    public class CompanySummaryViewComponent:ViewComponent
    {
        private readonly IDepartmentService _departmentService;

        public CompanySummaryViewComponent(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string title)
        {
            ViewBag.Title = title;
            var summary = await _departmentService.GetCompanySummary();
            return View(summary);
        }
    }
}
