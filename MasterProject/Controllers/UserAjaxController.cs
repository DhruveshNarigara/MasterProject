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
    public class UserAjaxController : Controller
    {
        private readonly ILogger<UserAjaxController> _logger;

        private readonly IUserRepositories _userRepositories;
        private readonly IEmployeeRepositories _employeeRepositories;
        private readonly IDepartmentRepositories _departmentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserAjaxController(ILogger<UserAjaxController> logger, IUserRepositories userRepositories, IEmployeeRepositories employeeRepositories, IDepartmentRepositories departmentRepository, IWebHostEnvironment webHostEnvironment)
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserModel user)
        {
            if (!ModelState.IsValid)
            {

                return Json(user);
            }

            bool isAuthenticated = _userRepositories.UserLogin(user);

            if (isAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                TempData["Message"] = "User does not exist. Please register.";
                return Json(new { Success = false, Message = "User does not exists" });

            }

        }
        [HttpPost]
        public IActionResult Registration([FromBody] UserModel reg)
        {
            if (!ModelState.IsValid)
            {
                // The model is not valid, return an error response or handle it appropriately
                Json(new { Success = false, Message = "Invalid data" });
            }

            _userRepositories.UserRegister(reg);
            return Json(new { Success = true, Message = "Register successfully" });
        }
        //        [HttpGet]
        //         [ActionName("Index")]
        //         public IActionResult Index1()
        //         {
        //             var employees = _employeeRepositories.GetAllEmployees().ToList(); // Fetch employees from the database
        //             return View(employees); // Return employees as JSON
        //         }
        [HttpGet]
        [ActionName("Index")]
        public IActionResult Index1()
        {
            var employees = _employeeRepositories.GetAllEmployees().ToList();
            ViewBag.DepartmentModel = _departmentRepository.GetAlldept()
                                                            .Select(d => new SelectListItem { Value = d.c_deptid.ToString(), Text = d.c_deptname })
                                                            .ToList();
            return View(employees); 
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeModel employee)
        {
 if (employee.c_empimageFile != null && employee.c_empimageFile.Length > 0)
            {
                employee.c_empimagePath = UploadFile(employee.c_empimageFile, "images");
            }

            // For demonstration purposes, let's just return a success message
            var response = new { message = "Employee added successfully" };

            // You can return JSON or any other type of response as needed
            return View(response);
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




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}