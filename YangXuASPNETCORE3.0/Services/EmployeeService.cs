using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangXuASPNETCORE3._0.Models;

namespace YangXuASPNETCORE3._0.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly List<Employee> _employees=new List<Employee>();

        public EmployeeService()
        {
            _employees.Add(new Employee
            {
                Id = 1,
                DepartmentId = 1,
                FirstName = "小红",
                LastName = "李",
                Gender = Gender.女
            });
            _employees.Add(new Employee
            {
                Id = 2,
                DepartmentId = 1,
                FirstName = "小明",
                LastName = "张",
                Gender = Gender.男
            });
            _employees.Add(new Employee
            {
                Id = 3,
                DepartmentId = 1,
                FirstName = "小强",
                LastName = "王",
                Gender = Gender.男
            });
            _employees.Add(new Employee
            {
                Id = 4,
                DepartmentId = 2,
                FirstName = "小美",
                LastName = "陈",
                Gender = Gender.女
            });
            _employees.Add(new Employee
            {
                Id = 5,
                DepartmentId = 2,
                FirstName = "小希",
                LastName = "赵",
                Gender = Gender.女
            });
            _employees.Add(new Employee
            {
                Id = 6,
                DepartmentId = 2,
                FirstName = "小伟",
                LastName = "钱",
                Gender = Gender.男
            });
            _employees.Add(new Employee
            {
                Id = 7,
                DepartmentId = 3,
                FirstName = "小云",
                LastName = "马",
                Gender = Gender.女
            });
            _employees.Add(new Employee
            {
                Id = 8,
                DepartmentId = 3,
                FirstName = "小东",
                LastName = "刘",
                Gender = Gender.男
            });
            _employees.Add(new Employee
            {
                Id = 9,
                DepartmentId = 3,
                FirstName = "小华",
                LastName = "李",
                Gender = Gender.男
            });
        }

        public Task Add(Employee employee)
        {
            employee.Id = _employees.Max(x => x.Id) + 1;
            _employees.Add(employee);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Employee>> GetByDepartmentId(int id)
        {
            return Task.Run(function: () => _employees.Where(x => x.DepartmentId == id));
        }

        public Task<Employee> Fire(int id)
        {
            return Task.Run(function: () =>
            {
                var employee = _employees.FirstOrDefault(x => x.Id == id);
                if (employee == null) return null;
                employee.Fired = true;
                return employee;

            });
        }
    }
}
