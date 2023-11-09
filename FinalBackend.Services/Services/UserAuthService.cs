using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBackend.Services.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IConfiguration _configuration;
        public UserAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool LogInUser()
        {
            throw new NotImplementedException();
        }
    }
}
