using FinalBackend.Services.Models;
using FinalBackend.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

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
                return false;
            }
        }

        [HttpPost]
        [Route("User")]
        public bool UserPostController([FromBody][Required] UserModel model)
        {
            try
            {
                return _userService.CreateUser(model);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("Roles/")]
        public List<string> UserGetRolesController([FromQuery][Required] string userId)
        {
            try
            {
                return _userService.GetUserRoles(userId);
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
    }
}
