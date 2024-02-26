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
        IEnumerable<DepartmentModel> GetAlldept();
        DepartmentModel AddDepartments(DepartmentModel dept);
        public DepartmentModel GetDeptById(int deptId);
        
        public void UpdateDepartments(DepartmentModel department);
        public void DeleteDepartments(int deptId);
    }
}