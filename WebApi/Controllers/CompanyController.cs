using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public IEnumerable<CompanyDto> GetAll()
        {
            var items = _companyService.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public CompanyDto Get(string companyCode)
        {
            var item = _companyService.GetCompanyByCode(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public bool Post([FromBody] CompanyDto companyDto)
        {
            var companyInfo = _mapper.Map<CompanyInfo>(companyDto);
            return _companyService.SaveCompany(companyInfo);
        }

        // PUT api/<controller>/5
        public bool Put(string companyCode, [FromBody] CompanyDto companyDto)
        {
            var companyInfo = _mapper.Map<CompanyInfo>(companyDto);
            return _companyService.UpdateCompany(companyInfo, companyCode);
        }

        // DELETE api/<controller>/5
        public bool Delete(string companyCode)
        {
            return _companyService.DeleteCompany(companyCode);
        }
    }
}