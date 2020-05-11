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
        Task<Company> GetCompanyAsync(Guid companyId);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);
        void AddComapny(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
        Task<bool> CompanyExistsAsync(Guid companyId);

        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);
        void AddEmployeey(Guid companyId, Employee employee);
        void UpdateEmployeey(Employee employee);
        void DeleteEmployeey(Employee employee);

        Task<bool> SaveAsync();
    }
}
