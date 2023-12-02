using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FinalBackend.Services.Models;
using FinalBackend.Services.Services;
using System.Text.Json;
using RestSharp;

namespace FinalBackend.Controllers
{

    [ApiController]
    [Route("[controller]/")]
    public class ObjectController : ControllerBase
    {
        private IObjectService _objectService;
        public ObjectController(IObjectService objectService)
        {
            _objectService = objectService;
        }


        [HttpGet]
        [Route("")]
        public List<ObjectModel> GetObjectListController()
        {
            try
            {
                List<ObjectModel> response = _objectService.GetObjectList();

                string jsonString = JsonSerializer.Serialize(response);

                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Route("Requests")]
        public List<ObjectModel> GetRequestsController()
        {
            try
            {
                List<ObjectModel> response = _objectService.GetRequestList();

                string jsonString = JsonSerializer.Serialize(response);

                return response;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet]
        [Route("id")]
        public FullObjectModel GetObjectController([FromQuery][Required] string id, CancellationToken ct = default)
        {
            try
            {
                return _objectService.GetObjectByID(id);
            }
            catch (Exception)
            {
                return new FullObjectModel();
            }
            
        }

        [HttpPost]
        [Route("")]
        public int PostObjectController([FromBody][Required] FullObjectModel obj, CancellationToken ct = default)
        {
            try
            {
                return _objectService.PostObject(obj);
            }
            catch (Exception)
            {
                return 0;
            }

        }


        [HttpPatch]
        [Route("")]
        public int PutObjectController([FromBody] FullObjectModel obj, [FromHeader][Required] string userId, [FromHeader][Required] string action, CancellationToken ct = default)
        {
            try
            {
                return _objectService.PutObject(obj, userId, action);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        [HttpGet]
        [Route("ObjectTypes")]
        public List<string> GetObjectTypesController()
        {
            try
            {
                return _objectService.GetObjectTypes();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        [HttpGet]
        [Route("FilePath")]
        public string GetFilePathController()
        {
            try
            {
                return _objectService.GetPath();
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }
    }
}
