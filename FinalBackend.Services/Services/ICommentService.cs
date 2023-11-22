using FinalBackend.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBackend.Services.Services
{
    public interface ICommentService
    {
        bool CreateComment(CommentModel comment);

        bool EditComment(CommentModel comment, string oldDescription);

        bool DeactivateComment(int commentId);

        List<CommentModel> GetCommentsByObjectId(string ObjectId);
    }
}
