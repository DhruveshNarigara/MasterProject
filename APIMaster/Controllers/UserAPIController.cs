using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MasterProject.Models;
using MasterProject.Repositories;

namespace APIMaster.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAPIController : ControllerBase
    {

        private readonly IUserRepositories _userRepositories;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAPIController(IUserRepositories userRepositories, IHttpContextAccessor httpContextAccessor)
        {
            _userRepositories = userRepositories;
            _httpContextAccessor = httpContextAccessor; 
        }
    

        [HttpPost]
        [Route("Register")]
       
        public IActionResult Register(UserModel reg)
        {
            _userRepositories.UserRegister(reg);
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserModel login)
        {
            
            bool isAuthenticated = _userRepositories.UserLogin(login);

            if (isAuthenticated)
            {
                return Ok("Login successful"); // Or return any additional information you need
            }
            else
            {
                return Unauthorized("Login failed"); // Or return any appropriate response
            }

        }
    }
}