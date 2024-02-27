using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Repositories
{
    public interface IUserRepositories
    {
        void UserRegister(UserModel reg);

        bool UserLogin(UserModel login);

        // UserModel GetLoginMasterByEmail(string email);
    }
}