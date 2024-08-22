using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET api/<controller>
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                Logger.Info("Fetching all employees.");
                var items = await Task.Run(() => _employeeService.GetAllEmployees());
                var result = _mapper.Map<IEnumerable<EmployeeDto>>(items);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while fetching all employees.");
                return InternalServerError(ex);
            }
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get(string employeeCode)
        {
            try
            {
                Logger.Info($"Fetching employee with code: {employeeCode}");
                var item = await Task.Run(() => _employeeService.GetEmployeeByCode(employeeCode));
                if (item == null)
                {
                    Logger.Warn($"Employee with code {employeeCode} not found.");
                    return NotFound();
                }
                var result = _mapper.Map<EmployeeDto>(item);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error occurred while fetching employee with code: {employeeCode}");
                return InternalServerError(ex);
            }
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                Logger.Warn("Received null employee data.");
                return BadRequest("Employee data is null.");
            }

            try
            {
                Logger.Info("Saving employee data.");
                var employeeInfo = _mapper.Map<EmployeeInfo>(employeeDto);
                var isSaved = await Task.Run(() => _employeeService.SaveEmployee(employeeInfo));
                if (isSaved)
                {
                    Logger.Info("Employee data saved successfully.");
                    return Ok(true);
                }
                else
                {
                    Logger.Warn("Failed to save employee data.");
                    return BadRequest("Failed to save the employee data.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occurred while saving employee data.");
                return InternalServerError(ex);
            }
        }
    }
}
