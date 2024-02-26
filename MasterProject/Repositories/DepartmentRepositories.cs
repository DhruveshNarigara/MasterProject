using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Repositories
{
    public class DepartmentRepositories : CommanRepositories, IDepartmentRepositories
    {
        public DepartmentModel AddDepartments(DepartmentModel dept)
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("Insert Into t_deptmaster(c_deptname) values (@deptname)", conn))
            {
                cmd.Parameters.AddWithValue("@deptname", dept.c_deptname);

                cmd.ExecuteNonQuery();
            }
            conn.Close();
            return dept;
        }

        public IEnumerable<DepartmentModel> GetAlldept()
        {
            List<DepartmentModel> deptList = new List<DepartmentModel>();
            conn.Open();
            using (var cmd = new NpgsqlCommand("Select c_deptid, c_deptname from t_deptmaster", conn))
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DepartmentModel task = new DepartmentModel();
                    task.c_deptid = Convert.ToInt32(reader["c_deptid"]);
                    task.c_deptname = reader["c_deptname"].ToString();
                    deptList.Add(task);
                }
            }
            conn.Close();
            return deptList;
        }

        public void UpdateDepartments(DepartmentModel department)
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

        public void DeleteDepartments(int deptId)
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