using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YangXuAPI.Entities;
using YangXuAPI.Models;
using YangXuAPI.Services;

namespace YangXuAPI.Controllers
{
    [ApiController]
    [Route("api/companies")]
    //[Route("api/controller")]//两种多可以
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyRepository companyRepository,IMapper mapper)
        {
            _companyRepository = companyRepository ??
                                 throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            var companies = await _companyRepository.GetCompaniesAsync();
            //404 NotFound();
            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companyDtos);
            //return new JsonResult(companies);
        }

        // GET: api/Companies/5
        [HttpGet("{companyId}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(int companyId)
        {
            //先判断是否存在，存在再从数据库里查出来
            //这种方法在并发量大的情况下可能会出问题，exist的时候存在，但是再执行下面查的时候恰好被其他请求删掉了，就可能会出错
            //if (!await _companyRepository.CompanyExistsAsync(companyId))
            //{
            //    return NotFound();
            //}

            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company==null)
            {
                //比上面那种方法稍微好点，还有更好的吗？？
                return NotFound();
            }

            return Ok(_mapper.Map<CompanyDto>(company));
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            
            return NoContent();
        }

        // POST: api/Companies
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }
    }
}
