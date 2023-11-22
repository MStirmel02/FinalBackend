using FinalBackend.Services.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FinalBackend.Services.Services
{
    public class CommentService : ICommentService
    {
        public IConfiguration _configuration;

        public CommentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CreateComment(CommentModel comment)
        {
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_post_comment", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ObjectID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.Text);
            cmd.Parameters.Add("@TimePosted", SqlDbType.DateTime);

            cmd.Parameters["@UserID"].Value = comment.UserId;
            cmd.Parameters["@ObjectID"].Value = comment.ObjectId;
            cmd.Parameters["@Description"].Value = comment.Description;
            cmd.Parameters["@TimePosted"].Value = comment.TimePosted;

            try
            {
                conn.Open();

                if (cmd.ExecuteNonQuery() == 1) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } finally { conn.Close(); }
        }

        public bool DeactivateComment(int commentId)
        {
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_deactivate_comment", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CommentID", SqlDbType.Int);

            cmd.Parameters["@CommentID"].Value = commentId;

            try
            {
                conn.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditComment(CommentModel comment, string oldDescription)
        {
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_edit_comment", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CommentID", SqlDbType.Int);
            cmd.Parameters.Add("@NewDescription", SqlDbType.Text);
            cmd.Parameters.Add("@OldDescription", SqlDbType.Text);

            cmd.Parameters["@CommentID"].Value = comment.CommentID;
            cmd.Parameters["@NewDescription"].Value = comment.Description;
            cmd.Parameters["@OldDescription"].Value = oldDescription;

            try
            {
                conn.Open();

                if (cmd.ExecuteNonQuery() == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } finally { conn.Close(); }


        }

        public List<CommentModel> GetCommentsByObjectId(string ObjectId)
        {
            List<CommentModel> commentList = new List<CommentModel>();

            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_get_comments_by_object", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ObjectID", SqlDbType.NVarChar);

            cmd.Parameters["@ObjectID"].Value = ObjectId;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CommentModel comment = new CommentModel()
                        {
                            //SELECT [CommentID], [UserID], [ObjectID], [Description], [TimePosted]
                            CommentID = reader.GetInt32(0),
                            UserId = reader.GetString(1),
                            ObjectId = reader.GetString(2),
                            Description = reader.GetString(3),
                            TimePosted = reader.GetDateTime(4),
                            Active = true

                        };

                        commentList.Add(comment);
                    }
                }

                return commentList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
