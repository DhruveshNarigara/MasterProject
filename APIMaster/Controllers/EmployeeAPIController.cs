using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MasterProject.Models;
using MasterProject.Repositories;

namespace APIMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeAPIController : ControllerBase
    {
         private readonly ILogger<EmployeeAPIController> _logger;
        private readonly IEmployeeRepositories _employeerepo;
       
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeAPIController(ILogger<EmployeeAPIController> logger, IEmployeeRepositories employeerepo, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _employeerepo = employeerepo;
            
            _webHostEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("CreateEmployee")]
        public IActionResult CreateEmployee([FromBody]EmployeeModel emp)
        {
            _employeerepo.AddEmployee(emp);
            return Ok();
        }

        [HttpGet]
        [Route("GetAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            var emp = _employeerepo.GetAllEmployees();
            return Ok(emp);
        }

        [HttpGet]
        [Route("GetEmployeeById")]
        public IActionResult GetEmployeeById(int id)
        {
            var emp = _employeerepo.GetEmployeeById(id);
            return Ok(emp);
        }

       
    }
}