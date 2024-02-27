using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Controllers
{
    // [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepositories _employeerepo;
        private readonly IDepartmentRepositories _deptrepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepositories employeerepo, IDepartmentRepositories deptrepo, IWebHostEnvironment hostingEnvironment,IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _employeerepo = employeerepo;
            _deptrepo = deptrepo;
            _webHostEnvironment = hostingEnvironment;
             _httpContextAccessor = httpContextAccessor;
        }

        // public IActionResult Index()
        // {
        //     return View();
        // }
        public IActionResult create()
        {
            var dept = _deptrepo.GetAlldept();
            ViewBag.Departments = new SelectList(dept, "c_deptid", "c_deptname");
            return View();
        }

        [HttpPost]
        public IActionResult create(EmployeeModel emp)
        {
            if (emp.c_empimageFile != null && emp.c_empimageFile.Length > 0)
            {
                emp.c_empimagePath = UploadFile(emp.c_empimageFile, "Images");
            }

            _employeerepo.AddEmployee(emp,HttpContext);
            return RedirectToAction("Index", "Employee");
        }

        private string UploadFile(IFormFile file, string folderName)
        {
            string uniqueFileName = null;

            if (file != null && file.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        public IActionResult Index()
        {
            int id = (int)_httpContextAccessor.HttpContext.Session.GetInt32("id");
            var d = _employeerepo.GetEmployeeById(id);

            return View(d);
        }


        // public IActionResult Details(int id)
        // {
        //     // Add your session check logic here if needed
        //     var employee = _employeerepo.GetEmployeeById(id);
        //     return View(employee);
        // }

        [HttpGet]
        public IActionResult EditEmployee()
        {
             int id = (int)_httpContextAccessor.HttpContext.Session.GetInt32("id");
            var departments = _deptrepo.GetAlldept();
            var employee = _employeerepo.GetEmployeeById(id);
            ViewBag.Departments = new SelectList(departments, "c_deptid", "c_deptname", employee.c_empdept);
            return View(employee);
        }

        [HttpPost]

        public IActionResult EditEmployee(EmployeeModel emp)
        {
            _employeerepo.UpdateEmployee(emp);
            return RedirectToAction("Index", "Employee");
        }

        public IActionResult Delete(int id)
        {
            // Add your session check logic here if needed
            var employee = _employeerepo.GetEmployeeById(id);
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Add your session check logic here if needed
            _employeerepo.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Details()
        {
             int id = (int)_httpContextAccessor.HttpContext.Session.GetInt32("id");

            var employee = _employeerepo.GetEmployeeById(id);
            return View(employee);
        }

        

    }
}
