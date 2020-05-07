﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangXuASPNETCORE3._0.Models;

namespace YangXuASPNETCORE3._0.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department> GetById(int id);
        Task<CompanySummary> GetCompanySummary();
        Task Add(Department department);
    }
}
