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
    public class DepartmentAjaxController : Controller
    {
        private readonly ILogger<DepartmentAjaxController> _logger;

        private readonly IDepartmentRepositories _deptrepo;

        public DepartmentAjaxController(ILogger<DepartmentAjaxController> logger, IDepartmentRepositories departmentRepositories)
        {
            _logger = logger;
            _deptrepo = departmentRepositories;
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDept([FromBody] DepartmentModel dept)
        {
            _deptrepo.AddDepartments(dept);
            return Json(new { Success = true, Message = "Department Added" });
        }

        [HttpGet]
        public IActionResult AllDepartments()
        {
            var dept = _deptrepo.GetAlldept();
            return Json(dept);
        }

        [HttpPost]
        public IActionResult UpdateDept(DepartmentModel dept)
        {
            _deptrepo.UpdateDepartments(dept);
            return Json(new { Success = true, Message = "Department Updated" });
        }

        [HttpPost]
        public IActionResult DeleteDept(int deptId)
        {
            _deptrepo.DeleteDepartments(deptId);
            return Json(new { Success = true, Message = "Department deleted" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}