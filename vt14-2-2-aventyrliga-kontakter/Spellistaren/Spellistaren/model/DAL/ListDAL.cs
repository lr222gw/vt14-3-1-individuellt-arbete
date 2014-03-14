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
                    cmd.Parameters.Add("@ListName", SqlDbType.VarChar, 30).Value = ListName;

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
        public void AddGameToList(int GameID, int ListID)
        {
            try
            {
                using(var conn = new SqlConnection(GetConnectionString()))
                {
                    var cmd = new SqlCommand("AppSchema.Usp_addGameToList", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@GameID", SqlDbType.Int).Value = GameID;
                    cmd.Parameters.Add("@ListID", SqlDbType.Int).Value = ListID;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public void EditListName(int ListID, string newName)
        {
            try
            {
                using(var conn = new SqlConnection(GetConnectionString())){
                    var cmd = new SqlCommand("AppSchema.Usp_EditListName", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ListID", SqlDbType.Int).Value = ListID;
                    cmd.Parameters.Add("@newName", SqlDbType.VarChar, 30).Value = newName;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public IEnumerable<ListRow> GetListContent(int ListID, int UserID)
        {
            try
            {
                List<ListRow> ListOfGames = new List<ListRow>();
                using(var conn = new SqlConnection(GetConnectionString()))
                {
                    var cmd = new SqlCommand("AppSchema.Usp_GetListContent", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ListID", SqlDbType.Int).Value = ListID;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                    conn.Open();
                        using(var reader = cmd.ExecuteReader()){

                            var idx_GameID = reader.GetOrdinal("GameID");
                            var idx_ListRowID = reader.GetOrdinal("ListRowID");

                            var listrowObj = new ListRow();                            
                            while(reader.Read()){
                                ListOfGames.Add(new ListRow
                                {
                                    GameID = reader.GetInt32(idx_GameID),
                                    ListID = ListID,
                                    ListRowID = reader.GetInt32(idx_ListRowID)
                                });
                            }
                        }


                    conn.Close();
                    return ListOfGames;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public IEnumerable<List> GetLists(int userId)
        {
            try
            {
                List<List> lists = new List<List>();
                using(var conn = new SqlConnection(GetConnectionString())){
                    var cmd = new SqlCommand("AppSchema.Usp_GetLists", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;

                    conn.Open();
                    using(var reader = cmd.ExecuteReader()){

                        var idx_ListID = reader.GetOrdinal("ListID");
                        var idx_UserID = reader.GetOrdinal("UserID");
                        var idx_ListName = reader.GetOrdinal("ListName");

                        while(reader.Read())
                        {
                            lists.Add(new List
                            {
                                ListID = reader.GetInt32(idx_ListID),
                                UserID = reader.GetInt32(idx_UserID),
                                ListName = reader.GetString(idx_ListName)
                            });
                        }
                    }
                }
                return lists;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public void RemoveGameFromList(int ListID, int GameID)
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    var cmd = new SqlCommand("AppSchema.Usp_RemoveGameFromList", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@GameID", SqlDbType.Int).Value = GameID;
                    cmd.Parameters.Add("@ListID", SqlDbType.Int).Value = ListID;

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
        public void RemoveList(int ListID, int UserID) 
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    var cmd = new SqlCommand("AppSchema.usp_RemoveList", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    cmd.Parameters.Add("@ListID", SqlDbType.Int).Value = ListID;

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