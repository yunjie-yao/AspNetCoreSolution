using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangXuASPNETCORE3._0.Models;

namespace YangXuASPNETCORE3._0.Services
{
    public interface IEmployeeService
    {
        Task Add(Employee employee);
        Task<IEnumerable<Employee>> GetByDepartmentId(int id);
        Task<Employee> Fire(int id);
    }
}
