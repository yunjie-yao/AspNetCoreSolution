using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using YangXuAPI.Entities;
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

            return Ok();
        }
    }
}
