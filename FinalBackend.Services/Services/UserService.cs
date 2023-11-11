using FinalBackend.Services.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
