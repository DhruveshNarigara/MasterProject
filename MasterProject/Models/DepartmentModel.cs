using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Models
{
    public class DepartmentModel
    {
        public int c_deptid {get ; set ;}
        public string ?c_deptname { get ; set ;}

    }
}