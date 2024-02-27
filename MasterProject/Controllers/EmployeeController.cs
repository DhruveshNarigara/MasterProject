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

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeRepositories employeerepo, IDepartmentRepositories deptrepo,IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _employeerepo = employeerepo;
            _deptrepo= deptrepo;
            _webHostEnvironment=hostingEnvironment;
        }

        public IActionResult Index()
        {
            // Add your session check logic here if needed
            var employees = _employeerepo.GetAllEmployees();
            return View(employees);
        }

        public IActionResult Create()
        {
            // Add your session check logic here if needed
            ViewBag.Departments = new List<string> { "IT", "HR", "Finance" }; // Example departments
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeModel employee, IFormFile image)
        {
            // if (employee.c_empimageFile!= null)
            // {
            //     employee.c_empimage = UploadFile(employee.c_empimageFile, "images");
            // }

            // _employeerepo.AddEmployee(employee);
            // return RedirectToAction("Index");

            if (image!= null && image.Length > 0)
            {
                var filename = Path.GetFileName(image.FileName);
                Console.WriteLine("filename : " , filename);
                var filepath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot" , "images" ,filename );
                Console.WriteLine("filepath : " + filepath);
                using(var stream = new FileStream(filepath , FileMode.Create)){
                    image.CopyTo(stream);
                }
                string imagepath = "/images/" + filename;
                _employeerepo.AddEmployee(employee ,imagepath);
                return RedirectToAction("Index");
            }else{
            return RedirectToAction("Index", "Home");
            }
        }

        private string UploadFile(IFormFile file, string folderName)
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, folderName, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }
            return filePath;
        }

        public IActionResult GetImage(int id)
        {
            var employee = _employeerepo.GetEmployeeById(id);
            var imagePath = employee.c_empimage;
            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/jpeg");
        }

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
