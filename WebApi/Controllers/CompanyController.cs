using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using NLog;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        // GET api/<controller>
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                Logger.Info("Get all companies.");
                var items = await Task.Run(() => _companyService.GetAllCompanies());
                var result = _mapper.Map<IEnumerable<CompanyDto>>(items);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while getting all companies.");
                return InternalServerError(ex);
            }
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get(string companyCode)
        {
            try
            {
                Logger.Info($"Fetching company with code: {companyCode}");
                var item = await Task.Run(() => _companyService.GetCompanyByCode(companyCode));
                if (item == null)
                {
                    Logger.Warn($"Company with code {companyCode} not found.");
                    return NotFound();
                }
                var result = _mapper.Map<CompanyDto>(item);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error occurred while getting company with code: {companyCode}");
                return InternalServerError(ex);
            }
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody] CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                Logger.Warn("Received null company data.");
                return BadRequest("Company data is null.");
            }

            try
            {
                Logger.Info("Saving company data.");
                var companyInfo = _mapper.Map<CompanyInfo>(companyDto);
                var isSaved = await Task.Run(() => _companyService.SaveCompany(companyInfo));
                if (isSaved)
                {
                    Logger.Info("Company data saved successfully.");
                    return Ok(true);
                }
                else
                {
                    Logger.Warn("Failed to save company data.");
                    return BadRequest("Failed to save the company data.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while saving company data.");
                return InternalServerError(ex);
            }
        }

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put(string companyCode, [FromBody] CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                Logger.Warn("Received null company data for update.");
                return BadRequest("Company data is null.");
            }

            try
            {
                Logger.Info($"Updating company with code: {companyCode}");
                var companyInfo = _mapper.Map<CompanyInfo>(companyDto);
                var isUpdated = await Task.Run(() => _companyService.UpdateCompany(companyInfo, companyCode));
                if (isUpdated)
                {
                    Logger.Info("Company data updated successfully.");
                    return Ok(true);
                }
                else
                {
                    Logger.Warn($"Failed to update company with code {companyCode}.");
                    return BadRequest("Failed to update the company data.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error occurred while updating company with code: {companyCode}");
                return InternalServerError(ex);
            }
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(string companyCode)
        {
            try
            {
                Logger.Info($"Deleting company with code: {companyCode}");
                var isDeleted = await Task.Run(() => _companyService.DeleteCompany(companyCode));
                if (isDeleted)
                {
                    Logger.Info("Company data deleted successfully.");
                    return Ok(true);
                }
                else
                {
                    Logger.Warn($"Failed to delete company with code {companyCode}.");
                    return BadRequest("Failed to delete the company data.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error occurred while deleting company with code: {companyCode}");
                return InternalServerError(ex);
            }
        }
    }
}
