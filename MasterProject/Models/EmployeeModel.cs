using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Npgsql;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Models
{
    public class EmployeeModel
    {
        public int ?c_empid { get; set; }

        // public int c_uid { get; set; }

        public string c_empname { get; set; }
        public string c_empgender { get; set; }

        public DateTime c_empdob { get; set; }

        public string c_empshift { get; set; }

        public int c_empdept { get; set; }
        //public string c_deptname { get; set; }
        public string? c_empimagePath { get; set; }
        public IFormFile? c_empimageFile { get; set; }


    }
}