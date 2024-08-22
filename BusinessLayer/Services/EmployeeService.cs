using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeInfo> GetAllEmployees()
        {
            var res = _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public EmployeeInfo GetEmployeeByCode(string employeeCode)
        {
            var result = _employeeRepository.GetByCode(employeeCode);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public bool SaveEmployee(EmployeeInfo employeeInfo)
        {
            var employee = _mapper.Map<Employee>(employeeInfo);
            return _employeeRepository.SaveEmployee(employee);
        }
    }
}
