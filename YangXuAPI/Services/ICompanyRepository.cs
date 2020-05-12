using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YangXuAPI.Entities;

namespace YangXuAPI.Services
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company> GetCompanyAsync(int companyId);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<int> companyIds);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
        Task<bool> CompanyExistsAsync(int companyId);

        Task<IEnumerable<Employee>> GetEmployeesAsync(int companyId);
        Task<Employee> GetEmployeeAsync(int companyId, int employeeId);
        void AddEmployee(int companyId, Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);

        Task<bool> SaveAsync();
    }
}
