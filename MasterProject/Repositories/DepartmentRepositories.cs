using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Repositories
{
    public class DepartmentRepositories : CommanRepositories , IDepartmentRepositories
    {
        public void AddDepartments(DepartmentModel dept)
        {
            
        }
    }
}