using FinalBackend.Services.Models;
using FinalBackend.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FinalBackend.Controllers
{


    [ApiController]
    [Route("[controller]/")]
    public class UserAuthController : ControllerBase
    {
        private IUserService _userService;
        public UserAuthController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("")]
        public bool UserValidationController([FromBody][Required] UserModel model)
        {
            try
            {
                return _userService.UserValidation(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
