using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using MasterProject.Models;
using MasterProject.Repositories;

namespace MasterProject.Repositories
{
    public class UserRepositories : CommanRepositories, IUserRepositories
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepositories(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void UserRegister(UserModel reg)
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand("insert into t_loginmaster(c_username,c_email,c_password)values(@username,@email,@password)", conn))
            {
                cmd.Parameters.AddWithValue("@username", reg.c_username);
                cmd.Parameters.AddWithValue("@email", reg.c_email);
                cmd.Parameters.AddWithValue("@password", reg.c_password);
                cmd.ExecuteNonQuery();
            }
            // var session = _httpContextAccessor.HttpContext?.Session;
            // if(session!=null)
            // {
            //     session.SetString("email",reg.c_email);
            //     session.SetInt32("id",reg.c_id);

            //     Console.WriteLine("Session id " + reg.c_id);
            //     Console.WriteLine("Session id " + reg.c_email);
            // }

            conn.Close();
        }

        public bool UserLogin(UserModel login)
        {
          bool isAuthenticated = false;
            conn.Open();
            using(var cmd = new NpgsqlCommand("select * from t_loginmaster where c_email=@email and c_password=@password",conn))
            {   
                cmd.Parameters.AddWithValue("@email",login.c_email);
                cmd.Parameters.AddWithValue("@password",login.c_password);
                var reader = cmd.ExecuteReader();
                // if (reader.Read())
                // {
                //     isAuthenticated=true;
                //     var session = _httpContextAccessor.HttpContext?.Session;
                //     if (session != null)
                //     {
                //         session.SetString("email", login.c_email);
                //         session.SetInt32("id", login.c_id);

                //         Console.WriteLine("Session Email is  : " + login.c_email);
                        
                //     }
                //     conn.Close();
                    
                //     return isAuthenticated;
                // }
            
            }
            conn.Close();
            return false;
          
        }
    }
}