using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YangXuAPI.Entities;
using YangXuAPI.Models;
using YangXuAPI.Services;

namespace YangXuAPI.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly ICompanyRepository _companyRepository;

        public EmployeesController(IMapper autoMapper,ICompanyRepository companyRepository)
        {
            _autoMapper = autoMapper;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesFromCompany(
            [FromRoute] int companyId
            ,[FromQuery(Name = "gender")]string genderDisplay
            ,string search)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employees = await _companyRepository.GetEmployeesAsync(companyId, genderDisplay, search);
            var employeeDtos = _autoMapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }

        [HttpGet("{employeeId}",Name = nameof(GetEmployeeFromCompany))]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeFromCompany(int companyId,int employeeId)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            
            var employee = await _companyRepository.GetEmployeeAsync(companyId,employeeId);
            if (employee==null)
            {
                return NotFound();
            }
            var employeeDto = _autoMapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany(int companyId, EmployeeAddDto employee)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            //框架可以保证，如果走到这那么employee肯定不为空
            //if (employee==null)
            //{
            //    return BadRequest();
            //}

            var entity = _autoMapper.Map<Employee>(employee);
            _companyRepository.AddEmployee(companyId,entity);
            await _companyRepository.SaveAsync();

            var returnDto = _autoMapper.Map<EmployeeDto>(entity);

            return CreatedAtRoute(nameof(GetEmployeeFromCompany),
                new {CompanyId = companyId, EmployeeId = returnDto.Id}, returnDto);

        }

    }
}