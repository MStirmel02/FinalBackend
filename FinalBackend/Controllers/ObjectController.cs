using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Net;
using FinalBackend.Services;
using FinalBackend.Services.Models;
using System.Text.Json.Nodes;
using FinalBackend.Services.Models;
using FinalBackend.Services.Services;
using System.Text.Json;

namespace FinalBackend.Controllers
{

    [ApiController]
    [Route("Controller/[controller]/")]
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
        [Route("@{id}")]
        public int GetObjectController([FromQuery][Required] string id, CancellationToken ct = default)
        {
            try
            {
                return _objectService.PostObject();
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        [HttpPost]
        [Route("")]
        public List<ObjectModel> PostObjectController([FromBody][Required] FullObjectModel obj, CancellationToken ct = default)
        {
            throw new NotImplementedException();

        }
    }
}
