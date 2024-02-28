using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MasterProject.Models;
using MasterProject.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MasterProject.Controllers
{
    //[Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IEmployeeRepositories _employeerepo;
        private readonly IDepartmentRepositories _deptrepo;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public AdminController(ILogger<AdminController> logger, IEmployeeRepositories employeerepo, IDepartmentRepositories deptrepo, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
             _employeerepo = employeerepo;
            _deptrepo = deptrepo;
            _webHostEnvironment = hostingEnvironment;
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
                emp.c_empimagePath = UploadFile(emp.c_empimageFile, "images");
            }

            _employeerepo.AddEmployee(emp);
            return RedirectToAction("Index", "Admin");
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

            var d = _employeerepo.GetAllEmployees();

            return View(d);
        }


        // public IActionResult Details(int id)
        // {
        //     // Add your session check logic here if needed
        //     var employee = _employeerepo.GetEmployeeById(id);
        //     return View(employee);
        // }

        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            var departments = _deptrepo.GetAlldept();
            var employee = _employeerepo.GetEmployeeById(id);
            ViewBag.Departments = new SelectList(departments, "c_deptid", "c_deptname", employee.c_empdept);
            return View(employee);
        }

        [HttpPost]

        public IActionResult EditEmployee(EmployeeModel emp)
        {
            _employeerepo.UpdateEmployee(emp);
            return RedirectToAction("Index", "Admin");
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
            return RedirectToAction("Index","Admin");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = _employeerepo.GetEmployeeById(id);
            return View(employee);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}