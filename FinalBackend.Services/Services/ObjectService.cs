using System.Data;
using System.Data.SqlClient;
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


            /*
             * {
  "objectID": "string",
  "objectTypeID": "string",
  "rightAscension": "string",
  "declination": "string",
  "redshift": 0,
  "apparentMagnitude": 0,
  "absoluteMagnitude": 0,
  "mass": "string",
  "description": "string",
  "dateSubmitted": "2023-11-10T01:50:24.531Z",
  "dateAccepted": "2023-11-10T01:50:24.531Z",
  "submitUser": "string",
  "acceptUser": "string",
  "image": "string"
}
             * 
             */
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

        }

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
            return objectModel; 

        }



        /*
* 
* using (var conn = SqlConnectionProvider.GetConnection())
   {
       var cmdText = "sp_select_roles_by_employeeID";
       var cmd = new SqlCommand(cmdText, conn);
       cmd.CommandType = CommandType.StoredProcedure;
       cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
       cmd.Parameters["@EmployeeID"].Value = employeeID;

       try
       {
           conn.Open();
           var reader = cmd.ExecuteReader();
           if (reader.HasRows)
           {
               while (reader.Read())
               {
                   roles.Add(reader.GetString(0));
               }
           }
       }
       catch (Exception ex)
       {
           throw ex;
       }
   }

   return roles;
* 
* 
* 
*/

    }
}
