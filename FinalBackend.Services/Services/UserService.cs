using FinalBackend.Services.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinalBackend.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CreateUser(UserModel user)
        {
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_post_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar);

            cmd.Parameters["@UserID"].Value = user.UserId;
            cmd.Parameters["@PasswordHash"].Value = user.PasswordHash;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() == 2)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            } finally { conn.Close(); }

        }

        public bool UserValidation(UserModel user)
        {

            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_validate_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar);

            cmd.Parameters["@UserID"].Value = user.UserId;
            cmd.Parameters["@PasswordHash"].Value = user.PasswordHash;

            try
            {
                conn.Open();

                var exec = cmd.ExecuteScalar();
                if (exec.Equals(1))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            } finally { conn.Close(); }
        }

        public List<string> GetUserRoles(string userId)
        {
            string str = "";
            List<string> list = new List<string>();
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_get_userroles", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar);

            cmd.Parameters["@UserID"].Value = userId;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                         
                    }
                }
            } catch(Exception)
            {

            } finally { conn.Close(); }


            return list;
        }
    }
}
