using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Repositories
{
    public interface IEmployeeRepositories
    {
        IEnumerable<EmployeeModel> GetAllEmployees();
        EmployeeModel GetEmployeeById(int id);
        void AddEmployee(EmployeeModel employee,HttpContext httpContext);
        void UpdateEmployee(EmployeeModel employee);
        void DeleteEmployee(int id);
    }
}