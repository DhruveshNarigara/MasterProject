using System;
using System.Collections.Generic;
using Npgsql;
using MasterProject.Models;

namespace MasterProject.Repositories
{
    public class EmployeeRepositories : CommanRepositories, IEmployeeRepositories
    {
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            var employees = new List<EmployeeModel>();
            string sql = "SELECT c_empid, c_empname, c_empgender, c_empdob, c_empshift, c_empdept, c_empimage FROM t_employeemaster";

            using (var command = new NpgsqlCommand(sql, conn))
            {
                 conn.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var employee = new EmployeeModel
                    {
                        c_empid = reader.GetInt32(reader.GetOrdinal("c_empid")),
                       // c_uid = reader.GetInt32(reader.GetOrdinal("c_uid")),
                        c_empname = reader.GetString(reader.GetOrdinal("c_empname")),
                        c_empgender = reader.GetString(reader.GetOrdinal("c_empgender")),
                        c_empdob = reader.GetDateTime(reader.GetOrdinal("c_empdob")),
                        c_empshift = reader.GetString(reader.GetOrdinal("c_empshift")),
                        c_empdept = reader.GetInt32(reader.GetOrdinal("c_empdept")),
                        c_empimage = reader.GetString(reader.GetOrdinal("c_empimage"))
                    };

                    employees.Add(employee);
                }
                  conn.Close();
            }

            return employees;
        }

        public EmployeeModel GetEmployeeById(int id)
        {
            EmployeeModel employee = null;
            string sql = "SELECT c_empid, c_empname, c_empgender, c_empdob, c_empshift, c_empdept, c_empimage FROM t_employeemaster WHERE c_empid = @id";

            using (var command = new NpgsqlCommand(sql, conn))
            {
                 conn.Open();
                command.Parameters.AddWithValue("@id", id);

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    employee = new EmployeeModel
                    {
                        c_empid = reader.GetInt32(reader.GetOrdinal("c_empid")),
                       // c_uid = reader.GetInt32(reader.GetOrdinal("c_uid")),
                        c_empname = reader.GetString(reader.GetOrdinal("c_empname")),
                        c_empgender = reader.GetString(reader.GetOrdinal("c_empgender")),
                        c_empdob = reader.GetDateTime(reader.GetOrdinal("c_empdob")),
                        c_empshift = reader.GetString(reader.GetOrdinal("c_empshift")),
                        c_empdept = reader.GetInt32(reader.GetOrdinal("c_empdept")),
                        c_empimage = reader.GetString(reader.GetOrdinal("c_empimage"))
                    };
                }
                 conn.Close();
            }

            return employee;
        }

        public void AddEmployee(EmployeeModel employee)
        {
            conn.Open();
            string sql = "INSERT INTO t_employeemaster ( c_empname, c_empgender, c_empdob, c_empshift, c_empdept, c_empimage) VALUES ( @name, @gender, @dob, @shift, @dept, @image)";
                
            using (var command = new NpgsqlCommand(sql, conn))
            {
                //command.Parameters.AddWithValue("@uid", employee.c_uid);
                command.Parameters.AddWithValue("@name", employee.c_empname);
                command.Parameters.AddWithValue("@gender", employee.c_empgender);
                command.Parameters.AddWithValue("@dob", employee.c_empdob);
                command.Parameters.AddWithValue("@shift", employee.c_empshift);
                command.Parameters.AddWithValue("@dept", employee.c_empdept);
                command.Parameters.AddWithValue("@image", employee.c_empimage);

                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void UpdateEmployee(EmployeeModel employee)
        {
            string sql = "UPDATE t_employeemaster SET  c_empname = @name, c_empgender = @gender, c_empdob = @dob, c_empshift = @shift, c_empdept = @dept, c_empimage = @image WHERE c_empid = @id";
 conn.Open();
            using (var command = new NpgsqlCommand(sql, conn))
            {
                //command.Parameters.AddWithValue("@uid", employee.c_uid);
                command.Parameters.AddWithValue("@name", employee.c_empname);
                command.Parameters.AddWithValue("@gender", employee.c_empgender);
                command.Parameters.AddWithValue("@dob", employee.c_empdob);
                command.Parameters.AddWithValue("@shift", employee.c_empshift);
                command.Parameters.AddWithValue("@dept", employee.c_empdept);
                command.Parameters.AddWithValue("@image", employee.c_empimage);
                command.Parameters.AddWithValue("@id", employee.c_empid);

                command.ExecuteNonQuery();
                 conn.Close();
            }
        }

        public void DeleteEmployee(int id)
        {
            string sql = "DELETE FROM t_employeemaster WHERE c_empid = @id";
           conn.Open();
            using (var command = new NpgsqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                 conn.Close();
            }
        }
    }
}