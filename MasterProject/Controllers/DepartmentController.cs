using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MasterProject.Models;
using MasterProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MasterProject.Controllers
{
    //[Route("[controller]")]
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;

        private readonly IDepartmentRepositories _deptrepo;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentRepositories departmentRepositories)
        {
            _logger = logger;
            _deptrepo = departmentRepositories;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddDept()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDept(DepartmentModel dept){
            _deptrepo.AddDepartments(dept);
            return RedirectToAction("Index","Home");
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]
        public IActionResult AllDepartments(){
          var dept = _deptrepo.GetAlldept();
          return View(dept);
        }

        
    }
}