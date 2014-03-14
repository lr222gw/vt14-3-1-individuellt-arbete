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

        public List<ListRow> GetListContent(int ListID, int UserID)
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
                            //if (reader.Read()) { //Vi kontrollerar först att det faktiskt finns poster att hämta ut, annars måste 0:värderna användas (se 4 och 6 rader längre ner) och dem ger error..
                                while(reader.Read()){
                                    ListOfGames.Add(new ListRow
                                    {
                                        GameID = reader.IsDBNull(idx_GameID) ?  0 : reader.GetInt32(idx_GameID), //om listan ej har rader, så blir gameID 0...
                                        ListID = ListID,
                                        ListRowID = reader.IsDBNull(idx_ListRowID) ? 0 : reader.GetInt32(idx_ListRowID)
                                    });

                                    // om listan ej har rader så är den inte funktionell, vi vill itne skicka tillbaka en icke funktionell lista, därför tar vi bort den posten
                                    // om så är fallet.
                                    if (ListOfGames[ListOfGames.Count -1].GameID == 0 || ListOfGames[ListOfGames.Count -1].ListRowID == 0) // man tar minus 1 för att hamna på rätt index...
                                    {
                                        ListOfGames.RemoveAt(ListOfGames.Count - 1); //tar b ort senaste tillagda värdet..
                                    }
                                }
                            //}
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