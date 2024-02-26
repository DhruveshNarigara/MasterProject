using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MasterProject.Repositories;
using MasterProject.Models;

namespace MasterProject.Controllers
{
    //[Route("[controller]")]
    public class UserAjaxController : Controller
    {
        private readonly ILogger<UserAjaxController> _logger;

        private readonly IUserRepositories _userRepositories;

        public UserAjaxController(ILogger<UserAjaxController> logger, IUserRepositories userRepositories)
        {
            _logger = logger;
            _userRepositories = userRepositories;
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



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}