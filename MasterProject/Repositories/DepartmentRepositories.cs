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

        public void UpdateEmployees(DepartmentModel department)
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("UPDATE t_deptmaster SET c_deptname = @deptname WHERE c_deptid = @deptId", conn))
            {
                cmd.Parameters.AddWithValue("@deptId", department.c_deptid);
                cmd.Parameters.AddWithValue("@deptname", department.c_deptname);

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void DeleteEmployees(int deptId)
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("DELETE FROM t_city WHERE c_cityid = @cityId", conn))
            {
                cmd.Parameters.AddWithValue("@cityId", deptId);
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine($"{rowsAffected} row(s) deleted successfully.");
                }
                else
                {
                    Console.WriteLine("No rows deleted.");
                }
            }
            conn.Close();
        }


    }
}