﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YangXuAPI.Data;
using YangXuAPI.Entities;

namespace YangXuAPI.Services
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly RoutineDbContext _context;
        public CompanyRepository(RoutineDbContext context)
        {
            _context = context ??
                       throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
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

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(int companyId)
        {
            return await _context.Employees.Where(x => x.CompanyId == companyId).ToListAsync();
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
