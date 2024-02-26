using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Repositories
{
    public class UserRepositories : CommanRepositories , IUserRepositories
    {
        public void UserRegister(UserModel reg)
        {

        }

        public void UserLogin(UserModel login)
        {
            
        }
    }
}