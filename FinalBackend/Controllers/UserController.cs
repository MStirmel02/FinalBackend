using FinalBackend.Services;
using FinalBackend.Services.Models;
using FinalBackend.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace FinalBackend.Controllers
{


    [ApiController]
    [Route("[controller]/")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService) 
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
        [Route("CreateUser")]
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
        [Route("Roles")]
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

        [HttpPost]
        [Route("Roles/Add")]
        public bool UserAddRoleController([FromHeader][Required] string userId, [FromHeader][Required] string roleId,
            [FromHeader][Required] string editUser)
        {
            try
            {
                return _userService.AddRoleByUser(userId, roleId, editUser);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("Roles/Delete")]
        public bool UserRemoveRoleController([FromHeader][Required] string userId, [FromHeader][Required] string roleId,
            [FromHeader][Required] string editUser)
        {
            try
            {
                return _userService.RemoveRoleByUser(userId, roleId, editUser);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("")]
        public List<string> UserGetController()
        {
            try
            {
                return _userService.GetUsers();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        [HttpGet]
        [Route("Facts")]
        public List<string> GetFactsController()
        {
            try
            {
                return _userService.GetFacts();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
