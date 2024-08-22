using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // GET api/<controller>
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var items = await Task.Run(() => _employeeService.GetAllEmployees());
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            var item = await Task.Run(() => _employeeService.GetEmployeeByCode(employeeCode));
            return _mapper.Map<EmployeeDto>(item);
        }

        // POST api/<controller>
        public async Task<bool> Post([FromBody] EmployeeDto employeeDto)
        {
            var employeeInfo = _mapper.Map<EmployeeInfo>(employeeDto);
            return await Task.Run(() => _employeeService.SaveEmployee(employeeInfo));
        }
    }
}