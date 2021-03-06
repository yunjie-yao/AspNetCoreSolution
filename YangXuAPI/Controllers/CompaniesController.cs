﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using YangXuAPI.DtoParameters;
using YangXuAPI.Entities;
using YangXuAPI.Helpers;
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
        private readonly IPropertyMappingService _propertyMappingService;

        public CompaniesController(
            ICompanyRepository companyRepository
            ,IMapper mapper
            ,IPropertyMappingService propertyMappingService)
        {
            _companyRepository = companyRepository ??
                                 throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
                                      throw new ArgumentNullException(nameof(propertyMappingService));
        }

        // GET: api/Companies
        [HttpGet(Name = nameof(GetCompanies))]
        // Head:apo/companies
        [HttpHead]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies(
            [FromQuery]CompanyDtoParameters parameters)
        {
            if (!_propertyMappingService.ValidMappingExistFor<CompanyDto,Company>(parameters.OrderBy))
            {
                return BadRequest();
            }
            var companies = await _companyRepository.GetCompaniesAsync(parameters);

            var previousPageLink = companies.HasPrevious
                ? CreateCompanyResourceUri(parameters, ResourceUriType.PreviousPage)
                : null;
            var nextPageLink = companies.HasNext
                ? CreateCompanyResourceUri(parameters, ResourceUriType.NextPage)
                : null;

            var paginationMetedata = new
            {
                totalCount = companies.TotalCount,
                totalPages = companies.TotalPages,
                currentPage = companies.CurrentPage,
                pageSize = companies.PageSize,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(paginationMetedata,new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping//为了避免转义
            }));

            //404 NotFound();
            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companyDtos);
            //return new JsonResult(companies);
        }

        // GET: api/Companies/5
        [HttpGet("{companyId}",Name = nameof(GetCompany))]
        public async Task<ActionResult<CompanyDto>> GetCompany(
            int companyId,[FromHeader(Name = "Accept")] string mediaType)
        {
            //先判断是否存在，存在再从数据库里查出来
            //这种方法在并发量大的情况下可能会出问题，exist的时候存在，但是再执行下面查的时候恰好被其他请求删掉了，就可能会出错
            //if (!await _companyRepository.CompanyExistsAsync(companyId))
            //{
            //    return NotFound();
            //}
            if (MediaTypeHeaderValue.TryParse(mediaType,out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company==null)
            {
                //比上面那种方法稍微好点，还有更好的吗？？
                return NotFound();
            }

            return Ok(_mapper.Map<CompanyDto>(company));
        }

        // Post api/companies
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody]CompanyAddDto company)
        {
            if (company==null)
            {
                return BadRequest();
            }

            var companyEntity = _mapper.Map<Company>(company);
            _companyRepository.AddCompany(companyEntity);
            await _companyRepository.SaveAsync();

            var dto = _mapper.Map<CompanyDto>(companyEntity);

            return CreatedAtRoute(nameof(GetCompany), new {CompanyId = dto.Id}, dto);
        }

        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow","GET,POST,OPTIONS,DELETE");
            return Ok();
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany(int companyId)
        {
            var companyEntity = await _companyRepository.GetCompanyAsync(companyId);
            if (companyEntity==null)
            {
                return NotFound();
            }

            _companyRepository.DeleteCompany(companyEntity);
            await _companyRepository.SaveAsync();

            return NoContent();
        }

        private string CreateCompanyResourceUri(CompanyDtoParameters parameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        orderBy = parameters.OrderBy,
                        pageNumber = parameters.PageNumber - 1,
                        pageSize = parameters.PageSize,
                        companyName = parameters.CompanyName,
                        searchTerms = parameters.SearchTerms
                        
                    });
                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        orderBy = parameters.OrderBy,
                        pageNumber = parameters.PageNumber + 1,
                        pageSize = parameters.PageSize,
                        companyName = parameters.CompanyName,
                        searchTerms = parameters.SearchTerms
                    });
                default:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        orderBy = parameters.OrderBy,
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize,
                        companyName = parameters.CompanyName,
                        searchTerms = parameters.SearchTerms
                    });
            }
        }
    }
}
