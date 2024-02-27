using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterProject.Models;

namespace MasterProject.Repositories
{
    public interface IAdminRepositories
    {
        IEnumerable<EmployeeModel> GetAllEmployees();
        EmployeeModel GetEmployeeById(int id);
        void AddEmployee(EmployeeModel employee);
        void UpdateEmployee(EmployeeModel employee);
        void DeleteEmployee(int id);
    }
}