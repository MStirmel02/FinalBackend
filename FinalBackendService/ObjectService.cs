using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FinalBackendService
{
    public class ObjectService
    {
        private readonly string _connectionDB;

        public ObjectService(IConfiguration configuration) {
            _connectionDB = configuration.GetConnectionString("Database");

        }

        public async Task<int> PostObject() 
        {



            return 0;
        }

        public async Task<List<Models.ObjectModel>> GetObjectList()
        {
            List<Models.ObjectModel> objectList = new List<Models.ObjectModel>();
            SqlConnection conn = new SqlConnection(_connectionDB);  
            var cmd = new SqlCommand("sp_get_objects", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                await conn.OpenAsync();

                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        Models.ObjectModel objectModel = new Models.ObjectModel();

                        objectModel.ObjectID = reader.GetString(0);
                        objectModel.ObjectInfoID = reader.GetString(1);
                        objectModel.DateSubmitted = reader.GetString(2);
                        
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
