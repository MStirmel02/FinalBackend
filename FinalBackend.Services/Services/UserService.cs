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

        public bool CreateUser()
        {
            throw new NotImplementedException();
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
                
            }
            catch (Exception)
            {

                return false;
            }


            return true;
        }
    }
}
