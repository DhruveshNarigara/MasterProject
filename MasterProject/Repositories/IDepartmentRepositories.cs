using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Repositories
{
    public interface IDepartmentRepositories
    {
        void AddDepartments(DepartmentModel dept);
        public void UpdateDepartments(DepartmentModel department);
        public void DeleteDepartments(int deptId);
    }
}