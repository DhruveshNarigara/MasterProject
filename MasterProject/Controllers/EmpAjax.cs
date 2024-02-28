using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MasterProject.Repositories;
using MasterProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MasterProject.Controllers
{
    //[Route("[controller]")]
    public class EmpAjax : Controller
    {
        private readonly ILogger<EmpAjax> _logger;
        private readonly IUserRepositories _userRepositories;
        private readonly IEmployeeRepositories _employeeRepositories;
        private readonly IDepartmentRepositories _departmentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmpAjax(ILogger<EmpAjax> logger, IUserRepositories userRepositories, IEmployeeRepositories employeeRepositories, IDepartmentRepositories departmentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _userRepositories = userRepositories;
            _employeeRepositories = employeeRepositories;
            _departmentRepository = departmentRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        // public IActionResult Index()
        // {
        //     var employees = _employeeRepositories.GetAllEmployees().ToList();
        //     ViewBag.DepartmentModel = _departmentRepository.GetAlldept()
        //                                                     .Select(d => new SelectListItem { Value = d.c_deptid.ToString(), Text = d.c_deptname })
        //                                                     .ToList();
        //     return View(employees);
        // }
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            return _employeeRepositories.GetAllEmployees();
        }
        // public IActionResult Index()
        // {
        //     var employees = _employeeRepositories.GetAllEmployees().ToList();
        //     ViewBag.DepartmentModel = _departmentRepository.GetAlldept()
        //     .Select(d => new SelectListItem { Value = d.c_deptid.ToString(), Text = d.c_deptname })
        //      .ToList();
        //     return View(employees);
        // }

         public IActionResult create()
        {
            var dept = _departmentRepository.GetAlldept();
            ViewBag.Departments = new SelectList(dept, "c_deptid", "c_deptname");
            return View();
        }

        public IActionResult getalldept()
        {
            var dept = _departmentRepository.GetAlldept();
            return Json(dept);
        }

        [HttpPost]
        public IActionResult create(EmployeeModel emp)
        {
            if (emp.c_empimageFile != null && emp.c_empimageFile.Length > 0)
            {
                emp.c_empimagePath = UploadFile(emp.c_empimageFile, "images");
            }

            _employeeRepositories.AddEmployee(emp);
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

        // public IActionResult Edit(int id)
        // {
        //     var emp = _employeeRepositories.GetAllEmployees(id);
        //     var dept = _departmentRepository.GetAlldept();
        //     ViewBag.Departments = new SelectList(dept, "c_deptid", "c_deptname");
        //     return View(emp);
        // }

        // [HttpPost]  
        // public IActionResult Edit(EmployeeModel emp)  
        // {  
        //     if (emp.c_empimageFile != null && emp.c_empimageFile.Length > 0)
        //     {
        //         emp.c_empimagePath = UploadFile(emp.c_empimageFile, "images");
        //     }
        //     _employeeRepositories.UpdateEmployee(emp);  
        //     return RedirectToAction("Index", "Employee");  
        // }

        public IActionResult Delete(int id)
        {
            _employeeRepositories.DeleteEmployee(id);
            return RedirectToAction("Index", "Employee");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}