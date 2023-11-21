using FinalBackend.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public bool PostCommentController([FromBody][Required] string placeholder )
        {
            return true;
        }


    }
}
