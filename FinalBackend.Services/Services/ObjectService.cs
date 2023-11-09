using System.Data;
using System.Data.SqlClient;
using FinalBackend.Services.Models;
using FinalBackend.Services.Services;
using Microsoft.Extensions.Configuration;
namespace FinalBackend.Services
{
    public class ObjectService : IObjectService
    {
        private readonly string _connectionDB = "";
        private readonly IConfiguration _configuration;
        public ObjectService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public int PostObject()
        {

            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_post_object", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            return 0;
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

        public FullObjectModel GetObjectByID(int id)
        {
            FullObjectModel objectModel = new FullObjectModel();
            SqlConnection conn = new SqlConnection(_configuration["ConnectionStrings:Database"]);
            var cmd = new SqlCommand("sp_get_objects", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        

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
