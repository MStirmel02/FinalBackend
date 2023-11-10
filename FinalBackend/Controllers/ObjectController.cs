using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FinalBackend.Services.Models;
using FinalBackend.Services.Services;
using System.Text.Json;

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
        public string GetObjectListController()
        {
            try
            {
                List<ObjectModel> response = _objectService.GetObjectList();

                string jsonString = JsonSerializer.Serialize(response);

                return jsonString;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Route("id")]
        public string GetObjectController([FromQuery][Required] string id, CancellationToken ct = default)
        {
            try
            {
                return JsonSerializer.Serialize(_objectService.GetObjectByID(id));
            }
            catch (Exception e)
            {

                throw e;
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
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
