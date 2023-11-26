using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using FinalBackend.Services.Models;
using FinalBackend.Services.Services;
using Microsoft.Extensions.Configuration;
namespace FinalBackend.Services
{
    public class ObjectService : IObjectService
    {
        private readonly IConfiguration _configuration;
        public ObjectService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public int PostObject(FullObjectModel obj)
        {
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_post_object", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ObjectID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ObjectTypeID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@RightAscension", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Declination", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Redshift", SqlDbType.Float);
            cmd.Parameters.Add("@ApparentMagnitude", SqlDbType.Float);
            cmd.Parameters.Add("@AbsoluteMagnitude", SqlDbType.Float);
            cmd.Parameters.Add("@Mass", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.Text);
            cmd.Parameters.Add("@DateSubmitted", SqlDbType.DateTime);
            cmd.Parameters.Add("@SubmitUser", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Image", SqlDbType.NVarChar);

            cmd.Parameters["@ObjectID"].Value = obj.ObjectID;
            cmd.Parameters["@ObjectTypeID"].Value = obj.ObjectTypeID;
            cmd.Parameters["@RightAscension"].Value = obj.RightAscension;
            cmd.Parameters["@Declination"].Value = obj.Declination;
            cmd.Parameters["@Redshift"].Value = obj.Redshift;
            cmd.Parameters["@ApparentMagnitude"].Value = obj.ApparentMagnitude;
            cmd.Parameters["@AbsoluteMagnitude"].Value = obj.AbsoluteMagnitude;
            cmd.Parameters["@Mass"].Value = obj.Mass;
            cmd.Parameters["@Description"].Value = obj.Description;
            cmd.Parameters["@DateSubmitted"].Value = obj.DateSubmitted;
            cmd.Parameters["@SubmitUser"].Value = obj.SubmitUser;
            cmd.Parameters["@Image"].Value = obj.Image;

            try
            {
                conn.Open();

                if(cmd.ExecuteNonQuery() == 1)
                {
                    return 1;
                } else
                {
                    throw new Exception();
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }

        }

        public int PutObject(FullObjectModel obj, string userId, string action)
        {

            string oldObj = JsonSerializer.Serialize(GetObjectByID(obj.ObjectID));
            string newObj = JsonSerializer.Serialize(obj);

            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_put_object", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ObjectID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ObjectTypeID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@RightAscension", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Declination", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Redshift", SqlDbType.Float);
            cmd.Parameters.Add("@ApparentMagnitude", SqlDbType.Float);
            cmd.Parameters.Add("@AbsoluteMagnitude", SqlDbType.Float);
            cmd.Parameters.Add("@Mass", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.Text);
            cmd.Parameters.Add("@DateSubmitted", SqlDbType.DateTime);
            cmd.Parameters.Add("@SubmitUser", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Image", SqlDbType.NVarChar);

            cmd.Parameters["@ObjectID"].Value = obj.ObjectID;
            cmd.Parameters["@ObjectTypeID"].Value = obj.ObjectTypeID;
            cmd.Parameters["@RightAscension"].Value = obj.RightAscension;
            cmd.Parameters["@Declination"].Value = obj.Declination;
            cmd.Parameters["@Redshift"].Value = obj.Redshift;
            cmd.Parameters["@ApparentMagnitude"].Value = obj.ApparentMagnitude;
            cmd.Parameters["@AbsoluteMagnitude"].Value = obj.AbsoluteMagnitude;
            cmd.Parameters["@Mass"].Value = obj.Mass;
            cmd.Parameters["@Description"].Value = obj.Description;
            cmd.Parameters["@DateSubmitted"].Value = obj.DateSubmitted;
            cmd.Parameters["@SubmitUser"].Value = obj.SubmitUser;
            cmd.Parameters["@Image"].Value = obj.Image;

            try
            {
                conn.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    if (PutObjectAudit(newObj, oldObj, userId, obj.ObjectID, action) == 1)
                    {
                        return 1;
                    }
                    return 0;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }

        public int PutObjectAudit(string newObj, string oldObj, string userId, string objectId, string action)
        {
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_audit_object", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ObjectID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Old", SqlDbType.Text);
            cmd.Parameters.Add("@New", SqlDbType.Text);
            cmd.Parameters.Add("@EditDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@EditUser", SqlDbType.NVarChar);
            cmd.Parameters.Add("@EditAction", SqlDbType.NVarChar);

            cmd.Parameters["@ObjectID"].Value = objectId;
            cmd.Parameters["@Old"].Value = oldObj;
            cmd.Parameters["@New"].Value = newObj;
            cmd.Parameters["@EditDate"].Value = DateTime.Now;
            cmd.Parameters["@EditUser"].Value = userId;
            cmd.Parameters["@EditAction"].Value = action;

            try
            {
                conn.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally {  conn.Close(); }


        }
        /*
         * 
         * 
	,	[ObjectID]			[NVARCHAR](255)						NOT NULL
	,	[Old]				[TEXT]								NOT NULL
	,	[New]				[TEXT]								NOT NULL
	,	[EditDate]			[DATETIME]							NOT NULL
	,	[EditUser]			[NVARCHAR](100)						NOT NULL
         * 
         */

        public List<ObjectModel> GetObjectList()
        {
            List<ObjectModel> objectList = new List<ObjectModel>();
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);  
            var cmd = new SqlCommand("sp_get_objects", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        ObjectModel objectModel = new ObjectModel();

                        objectModel.ObjectID = reader.GetString(0);
                        objectModel.DateSubmitted = reader.GetDateTime(1);
                        objectModel.UserSubmitted = reader.GetString(2);
                        objectModel.ObjectInfoID = reader.GetString(3);
                        objectModel.Image = reader.GetString(4);
                        
                        objectList.Add(objectModel);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }


            return objectList;
        }

        public List<ObjectModel> GetRequestList()
        {
            List<ObjectModel> objectList = new List<ObjectModel>();
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_get_requests", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ObjectModel objectModel = new ObjectModel();

                        objectModel.ObjectID = reader.GetString(0);
                        objectModel.DateSubmitted = reader.GetDateTime(1);
                        objectModel.UserSubmitted = reader.GetString(2);
                        objectModel.ObjectInfoID = reader.GetString(3);
                        objectModel.Image = reader.GetString(4);

                        objectList.Add(objectModel);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }


            return objectList;
        }

        public FullObjectModel GetObjectByID(string id)
        {
            FullObjectModel objectModel = new FullObjectModel();
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_select_object", conn);
            cmd.Parameters.Add("@id", SqlDbType.NVarChar);
            cmd.Parameters["@id"].Value = id;
            cmd.CommandType = CommandType.StoredProcedure;

            /*
             * 
             * AS BEGIN
	SELECT [Object].[ObjectID], [Object].[DateSubmitted], [Object].[SubmitUser], [Object].[Image]
			[ObjectInfo].[ObjectTypeID], [ObjectInfo].[RightAscension], [ObjectInfo].[Declination],
			[ObjectInfo].[Redshift], [ObjectInfo].[ApparentMagnitude], [ObjectInfo].[AbsoluteMagnitude],
			[ObjectInfo].[Mass], [ObjectInfo].[Description]
             * 
             * 
             */

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objectModel.ObjectID = reader.GetString(0);
                        objectModel.DateSubmitted = reader.GetDateTime(1);
                        objectModel.SubmitUser = reader.GetString(2);
                        objectModel.Image = reader.GetString(3);
                        objectModel.ObjectTypeID = reader.GetString(4);
                        objectModel.RightAscension = reader.GetString(5);
                        objectModel.Declination = reader.GetString(6);
                        objectModel.Redshift = reader.GetDouble(7);
                        objectModel.ApparentMagnitude = reader.GetDouble(8);
                        objectModel.AbsoluteMagnitude = reader.GetDouble(9);
                        objectModel.Mass = reader.GetString(10);
                        objectModel.Description = reader.GetString(11);


                    }
                }


            } 
            catch (Exception e)
            {
                throw e;
            }
            finally { conn.Close(); }
            return objectModel; 

        }


    }
}
