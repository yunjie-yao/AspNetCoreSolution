using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YangXuAPI.DtoParameters;
using YangXuAPI.Entities;
using YangXuAPI.Models;
using YangXuAPI.Services;
using Microsoft.Extensions.Logging;

namespace YangXuAPI.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    //[ResponseCache(CacheProfileName = "120sCacheProfiles")]
    //[HttpCacheExpiration(CacheLocation = CacheLocation.Public)]
    //[HttpCacheValidation(MustRevalidate = true)]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _autoMapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IMapper autoMapper,ICompanyRepository companyRepository,ILogger<EmployeesController> logger)
        {
            _autoMapper = autoMapper;
            _companyRepository = companyRepository;
            _logger = logger;
        }

        [HttpGet]
        //[Authorize]
        [Authorize(Roles = "test,test1")]//多个角色用逗号分开
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesFromCompany(
            [FromRoute] int companyId
            ,[FromQuery] EmployeeDtoParameters parameters)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employees = await _companyRepository.GetEmployeesAsync(companyId, parameters);
            var employeeDtos = _autoMapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }

        [HttpGet("{employeeId}",Name = nameof(GetEmployeeFromCompany))]
        //[ResponseCache(Duration = 60)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public,MaxAge = 1800)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeFromCompany(int companyId,int employeeId)
        {
            _logger.LogInformation($"{nameof(GetEmployeeFromCompany)}/Input,companyId={companyId},employeeId={employeeId}");
            _logger.LogWarning($"{nameof(GetEmployeeFromCompany)}/Input,companyId={companyId},employeeId={employeeId}");
            _logger.LogTrace($"{nameof(GetEmployeeFromCompany)}/Input,companyId={companyId},employeeId={employeeId}");
            _logger.LogError($"{nameof(GetEmployeeFromCompany)}/Input,companyId={companyId},employeeId={employeeId}");
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
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany(
            int companyId, 
            EmployeeAddDto employee)
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

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployeeForCompany(
            int companyId, int employeeId,
            EmployeeUpdateDto employee)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity==null)
            {
                // 不存在进行创建
                var employeeToAddEntity = _autoMapper.Map<Employee>(employee);
                employeeToAddEntity.Id = employeeId;

                _companyRepository.AddEmployee(companyId,employeeToAddEntity);
                await _companyRepository.SaveAsync();

                var dtoToReturn = _autoMapper.Map<EmployeeDto>(employeeToAddEntity);

                return CreatedAtRoute(nameof(GetEmployeeFromCompany),
                    new { CompanyId = companyId, EmployeeId = dtoToReturn.Id }, dtoToReturn);
            }

            // PUT
            // entity->updateDto
            // employee->updateDto
            // updateDto->entity
            _autoMapper.Map(employee, employeeEntity);

            _companyRepository.UpdateEmployee(employeeEntity);
            await _companyRepository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{employeeId}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(
            int companyId, 
            int employeeId, 
            JsonPatchDocument<EmployeeUpdateDto> patchDocument)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);

            if (employeeEntity==null)
            {
                // 不存在，新增
                var employeeDto=new EmployeeUpdateDto();
                patchDocument.ApplyTo(employeeDto,ModelState);

                if (!TryValidateModel(employeeDto))
                {
                    return ValidationProblem(ModelState);
                }

                var employeeToAdd = _autoMapper.Map<Employee>(employeeDto);
                employeeToAdd.Id = employeeId;

                _companyRepository.AddEmployee(companyId, employeeToAdd);
                await _companyRepository.SaveAsync();

                var dtoToReturn = _autoMapper.Map<EmployeeDto>(employeeToAdd);
                return CreatedAtRoute(nameof(GetEmployeeFromCompany), new
                {
                    companyId,
                    employeeId = dtoToReturn.Id,
                }, dtoToReturn);
            }

            var dtoToPatch = _autoMapper.Map<EmployeeUpdateDto>(employeeEntity);

            // 需要验证
            patchDocument.ApplyTo(dtoToPatch,ModelState);

            if (!TryValidateModel(dtoToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _autoMapper.Map(dtoToPatch, employeeEntity);
            _companyRepository.UpdateEmployee(employeeEntity);
            await _companyRepository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(
            int companyId,
            int employeeId)
        {
            if (!await _companyRepository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }

            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);

            if (employeeEntity==null)
            {
                return NotFound();
            }

            _companyRepository.DeleteEmployee(employeeEntity);

            await _companyRepository.SaveAsync();

            return NoContent();
        }

        public override ActionResult ValidationProblem(
            ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.
                GetRequiredService<IOptions<ApiBehaviorOptions>>();

            return (ActionResult) options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}