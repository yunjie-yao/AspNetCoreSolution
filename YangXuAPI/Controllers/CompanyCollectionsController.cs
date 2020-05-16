using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YangXuAPI.Entities;
using YangXuAPI.Helpers;
using YangXuAPI.Models;
using YangXuAPI.Services;

namespace YangXuAPI.Controllers
{
    [ApiController]
    [Route("api/companycollections")]
    public class CompanyCollectionsController:ControllerBase

    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        

        public CompanyCollectionsController(ICompanyRepository companyRepository,IMapper mapper)
        {
            _companyRepository = companyRepository ??
                throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        //1,2,3,4
        //key1=value1,key2=value2
        [HttpGet("({ids})",Name = nameof(GetCompanyCollection))]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanyCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<int> ids)
        {
            if (ids==null)
            {
                return BadRequest();
            }

            var companyIds = ids as int[] ?? ids.ToArray();
            var entities = await _companyRepository.GetCompaniesAsync(companyIds);
            if (entities.Count() != companyIds.Count())
            {
                return NotFound();
            }

            var dtoToReturn = _mapper.Map<IEnumerable<CompanyDto>>(entities);
            return Ok(dtoToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollections(
            IEnumerable<CompanyAddDto> companyCollections)
        {
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollections);
            foreach (var company in companyEntities)
            {
                _companyRepository.AddCompany(company);
            }

            await _companyRepository.SaveAsync();

            var dtoToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            var ids = string.Join(",", dtoToReturn.Select(x => x.Id));

            return CreatedAtRoute(nameof(GetCompanyCollection), new {ids}, dtoToReturn);
        }
    }
}
