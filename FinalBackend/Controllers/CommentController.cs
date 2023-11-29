using FinalBackend.Services.Models;
using FinalBackend.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace FinalBackend.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class CommentController
    {
        ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Route("")]
        public bool PostCommentController([FromBody][Required] CommentModel comment)
        {
            try
            {
                return _commentService.CreateComment(comment);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPatch]
        [Route("")]
        public bool PatchCommentController([FromBody][Required] CommentModel comment, [FromHeader][Required] string oldDescription)
        {
            try
            {
                return _commentService.EditComment(comment, oldDescription);
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpDelete]
        [Route("id")]
        public bool DeactivateCommentController([FromQuery][Required] int commentId)
        {
            try
            {
                return _commentService.DeactivateComment(commentId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("")]
        public List<CommentModel> GetCommentsByIdController([FromQuery][Required] string objectId)
        {
            try
            {   
                return _commentService.GetCommentsByObjectId(objectId);

            }
            catch (Exception)
            {
                return new List<CommentModel>();
            }
        }

    }
}
