using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YangXuAPI.Data;
using YangXuAPI.DtoParameters;
using YangXuAPI.Entities;
using YangXuAPI.Helpers;
using YangXuAPI.Models;

namespace YangXuAPI.Services
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly RoutineDbContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public CompanyRepository(RoutineDbContext context,IPropertyMappingService propertyMappingService)
        {
            _context = context ?? 
                       throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ??
                              throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public async Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameters parameters)
        {
            if (parameters==null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryExpression = _context.Companies as IQueryable<Company>;

            if (!string.IsNullOrWhiteSpace(parameters.CompanyName))
            {
                queryExpression = queryExpression.Where(x => x.Name == parameters.CompanyName.Trim());
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerms))
            {
                var searchTerms = parameters.SearchTerms.Trim();
                queryExpression = queryExpression.Where(x => x.Name.Contains(searchTerms)
                                                             || x.Introduction.Contains(searchTerms));
            }

            var mappingDictionary = _propertyMappingService.GetPropertyMapping<CompanyDto, Company>();
            queryExpression = queryExpression.ApplySort(parameters.OrderBy, mappingDictionary);

            //return await queryExpression.ToListAsync();//此时才会查询数据库
            return await PagedList<Company>.CreateAsync(queryExpression, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<Company> GetCompanyAsync(int companyId)
        {
            //if (companyId==Guid.Empty)
            //{
            //    throw new ArgumentNullException(nameof(companyId));
            //}

            return await _context.Companies
                .FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<int> companyIds)
        {
            if (companyIds==null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }

            return await _context.Companies
                .Where(x => companyIds.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public void AddCompany(Company company)
        {
            if (company==null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            //company.Id=Guid.NewGuid();

            _context.Companies.Add(company);
        }

        public void UpdateCompany(Company company)
        {
            //_context.Entry(company).State = EntityState.Modified;
        }

        public void DeleteCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            _context.Companies.Remove(company);
        }

        public async Task<bool> CompanyExistsAsync(int companyId)
        {
            //if (companyId==Guid.Empty)
            //{
            //    throw new ArgumentNullException(nameof(companyId));
            //}

            return await _context.Companies.AnyAsync(x => x.Id == companyId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(int companyId,EmployeeDtoParameters parameters)
        {
            var items = _context.Employees.Where(x => x.CompanyId == companyId);//走到这其实并没有操作Db相当于在拼接sql语句

            if (!string.IsNullOrWhiteSpace(parameters.Gender))
            {
                var genderStr = parameters.Gender.Trim();
                var gender = Enum.Parse<Gender>(genderStr);

                items = items.Where(x => x.Gender == gender);
            }

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerms))
            {
                var search = parameters.SearchTerms.Trim();
                items = items.Where(x => x.LastName.Contains(search)
                                         || x.FirstName.Contains(search)
                                         || x.EmployeeNo.Contains(search));
            }

            //if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            //{
            //    //简单粗暴的排序
            //    if (parameters.OrderBy.ToLowerInvariant()=="name")
            //    {
            //        items = items.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
            //    }

            //}

            // 排序应该在分页之前
            var mappingDictionary = _propertyMappingService.GetPropertyMapping<EmployeeDto, Employee>();
            items = items.ApplySort(parameters.OrderBy, mappingDictionary);

            return await items.ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(int companyId, int employeeId)
        {
            //if (companyId==Guid.Empty)
            //{
            //    throw new ArgumentNullException(nameof(companyId));
            //}
            //if (employeeId == Guid.Empty)
            //{
            //    throw new ArgumentNullException(nameof(employeeId));
            //}

            return await _context.Employees
                .Where(x => x.CompanyId == companyId && x.Id == employeeId)
                .FirstOrDefaultAsync();
        }

        public void AddEmployee(int companyId, Employee employee)
        {
            //if (companyId == Guid.Empty)
            //{
            //    throw new ArgumentNullException(nameof(companyId));
            //}
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            employee.CompanyId = companyId;
            _context.Employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {

        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
