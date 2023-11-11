﻿using Microsoft.AspNetCore.Mvc;
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
            catch (Exception)
            {
                return "SQLException";
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


        [HttpPut]
        [Route("")]
        public int PutObjectController([FromBody][Required] FullObjectModel obj, [FromHeader][Required] string userId, [FromHeader][Required] string action, CancellationToken ct = default)
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
    }
}
