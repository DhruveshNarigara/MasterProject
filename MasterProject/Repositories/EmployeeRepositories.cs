using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Repositories
{
    public class EmployeeRepositories : CommanRepositories , IEmployeeRepositories
    {
       public  void AddEmployees(EmployeeModel emp)
       {

       }
    }
}