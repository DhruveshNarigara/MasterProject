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
    }
}