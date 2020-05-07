using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using YangXuASPNETCORE3._0.Models;

namespace YangXuASPNETCORE3._0.Services
{
    public class DepartmentService:IDepartmentService
    {
        private readonly List<Department> _departments=new List<Department>();

        public DepartmentService()
        {
            _departments.Add(new Department()
            {
                Id = 1,
                Name = "HR",
                EmployeeCount = 16,
                Location = "Beijing"
            });
            _departments.Add(new Department()
            {
                Id = 2,
                Name = "R&D",
                EmployeeCount = 52,
                Location = "Shanghai"
            });
            _departments.Add(new Department()
            {
                Id = 3,
                Name = "Sales",
                EmployeeCount = 200,
                Location = "Hangzhou"
            });
        }

        public Task<IEnumerable<Department>> GetAll()
        {
            return Task.Run(function: () => _departments.AsEnumerable());
        }

        public Task<Department> GetById(int id)
        {
            return Task.Run(function: () => _departments.FirstOrDefault(x => x.Id == id));
        }

        public Task<CompanySummary> GetCompanySummary()
        {
            return Task.Run(function: () =>
            {
                return new CompanySummary()
                {
                    EmployeeCount = _departments.Sum(x => x.EmployeeCount),
                    AverageDapartmentEmployeeCount = (int) _departments.Average(x => x.EmployeeCount)
                };
            });
        }

        public Task Add(Department department)
        {
            department.Id = _departments.Max(x => x.Id) + 1;
            _departments.Add(department);
            return Task.CompletedTask;
        }
    }
}
