using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Controllers
{
    //[Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserRepositories _userRepositories;

        public UserController(ILogger<UserController> logger, IUserRepositories userRepositories)
        {
            _logger = logger;
            _userRepositories = userRepositories;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserRegister()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult UserRegister(UserModel reg)
        {
            _userRepositories.UserRegister(reg);
            return RedirectToAction("UserLogin");
        }

        public IActionResult UserLogin()
        {
            return View();
        }

       [HttpPost]
        public IActionResult UserLogin(UserModel Login)
        {
            bool isAuthenticated = _userRepositories.UserLogin(Login);
            
            if(isAuthenticated)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                TempData["Message"] = "User does not exist. Please register.";
                return RedirectToAction("UserLogin");
            }
        }

        public IActionResult Logout()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}