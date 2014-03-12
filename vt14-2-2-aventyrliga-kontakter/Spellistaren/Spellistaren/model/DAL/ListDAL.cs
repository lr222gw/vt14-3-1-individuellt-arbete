using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Spellistaren.model.DAL
{
    public class ListDAL : DALBase
    {
        public void CreateList(int UserID, string ListName)
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    var cmd = new SqlCommand("AppSchema.usp_CreateList", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    cmd.Parameters.Add("@ListName", SqlDbType.VarChar).Value = ListName;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            
        }
    }
}