using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesFromCompany([FromRoute] int companyId)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employees = await _companyRepository.GetEmployeesAsync(companyId);
            var dtoToReturn = _autoMapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(dtoToReturn);
        }
        
    }
}