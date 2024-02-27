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
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepositories _employeerepo;
        private readonly IDepartmentRepositories _deptrepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepositories employeerepo, IDepartmentRepositories deptrepo, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _employeerepo = employeerepo;
            _deptrepo = deptrepo;
            _webHostEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            // Add your session check logic here if needed
            var employees = _employeerepo.GetAllEmployees();
            return View(employees);
        }

        public IActionResult Create()
        {
           var dept = _deptrepo.GetAlldept();
            ViewBag.Departments = new SelectList(dept, "c_deptid", "c_deptname");
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeModel employee)
        {
            if (employee.c_empimageFile != null && employee.c_empimageFile.Length > 0)
            {
                employee.c_empimage = UploadFile(employee.c_empimageFile, "images");
            }

            _employeerepo.AddEmployee(employee);
            return RedirectToAction("Index");
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

       

        // public IActionResult GetImage(int id)
        // {
        //     var employee = _employeerepo.GetEmployeeById(id);
        //     var imagePath = employee.c_empimage;
        //     var imageBytes = System.IO.File.ReadAllBytes(imagePath);
        //     return File(imageBytes, "image/jpeg");
        // }

        public IActionResult Details(int id)
        {
            // Add your session check logic here if needed
            var employee = _employeerepo.GetEmployeeById(id);
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _employeerepo.GetEmployeeById(id);
            ViewBag.Departments = new List<string> { "IT", "HR", "Finance" }; // Example departments
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeModel employee)
        {
            if (employee.c_empimage != null)
            {
                employee.c_empimage = UploadFile(employee.c_empimageFile, "images");
            }

            _employeerepo.UpdateEmployee(employee);
            return RedirectToAction("Index");
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
    }
}
