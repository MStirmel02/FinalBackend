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
                if (cmd.ExecuteNonQuery() != 0)
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
                return list;
            } finally { conn.Close(); }


            return list;
        }

        public List<string> GetUsers()
        {
            List<string> list = new List<string>();
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_get_users", conn);
            cmd.CommandType = CommandType.StoredProcedure;

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
            }
            catch (Exception)
            {
                return list;
            } finally { conn.Close(); }


            return list;
        }

        public bool AddRoleByUser(string userId, string roleId, string editUser)
        {
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_add_role", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@EditUser", SqlDbType.NVarChar);
            cmd.Parameters.Add("@EditAction", SqlDbType.Text);
            cmd.Parameters.Add("@EditDate", SqlDbType.DateTime);

            cmd.Parameters["@UserID"].Value = userId;
            cmd.Parameters["@RoleID"].Value = roleId;
            cmd.Parameters["@EditUser"].Value = editUser;
            cmd.Parameters["@EditAction"].Value = roleId + " added from " + userId;
            cmd.Parameters["@EditDate"].Value = DateTime.Now;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
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

        public bool RemoveRoleByUser(string userId, string roleId, string editUser)
        {
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_remove_role", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@RoleID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@EditUser", SqlDbType.NVarChar);
            cmd.Parameters.Add("@EditAction", SqlDbType.Text);
            cmd.Parameters.Add("@EditDate", SqlDbType.DateTime);

            cmd.Parameters["@UserID"].Value = userId;
            cmd.Parameters["@RoleID"].Value = roleId;
            cmd.Parameters["@EditUser"].Value = editUser;
            cmd.Parameters["@EditAction"].Value = roleId + " removed from " + userId;
            cmd.Parameters["@EditDate"].Value = DateTime.Now;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally { conn.Close(); }
        }
    }
}
