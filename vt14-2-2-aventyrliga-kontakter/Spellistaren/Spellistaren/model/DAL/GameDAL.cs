using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Spellistaren.model.DAL
{
    public class GameDAL : DALBase
    {
        public Game GetGameDetails(int UserID, int GameID)
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {

                    var cmd = new SqlCommand("AppSchema.Usp_GetGameDetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    cmd.Parameters.Add("@GameID", SqlDbType.Int).Value = GameID;

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {

                        var idx_GameID = reader.GetOrdinal("GameID");
                        var idx_ScoreID = reader.GetOrdinal("ScoreID");
                        var idx_UserID = reader.GetOrdinal("UserID");
                        var idx_GameName = reader.GetOrdinal("GameName");
                        var idx_CompanyName = reader.GetOrdinal("CompanyName");
                        var idx_Story = reader.GetOrdinal("Story");
                        var idx_PlayersOnline = reader.GetOrdinal("PlayersOnline");
                        var idx_PlayersOffline = reader.GetOrdinal("PlayersOffline");
                        var idx_ReleaseDate = reader.GetOrdinal("ReleaseDate");
                        var idx_CustomNote = reader.GetOrdinal("CustomNote");

                        if (reader.Read())
                        {

                            Game aGame = new Game
                            {
                                CompanyName = reader.IsDBNull(idx_CompanyName) ? null : reader.GetString(idx_CompanyName),
                                CustomNote = reader.IsDBNull(idx_CustomNote) ? null : reader.GetString(idx_CustomNote),
                                GameID =  reader.GetInt32(idx_GameID),
                                GameName = reader.GetString(idx_GameName),
                                PlayersOffline = reader.IsDBNull(idx_PlayersOffline) ? (short?)null : reader.GetInt16(idx_PlayersOffline), //Tydligen ska int16 vara samma som smallint, vet inte om detta är sant, får bli upp till bevis..
                                PlayersOnline = reader.IsDBNull(idx_PlayersOnline) ? (int?)null : reader.GetInt32(idx_PlayersOnline),
                                ReleaseDate = reader.IsDBNull(idx_ReleaseDate) ? (DateTime?) null : reader.GetDateTime(idx_ReleaseDate),
                                ScoreID = reader.IsDBNull(idx_ScoreID) ? (int?) null : reader.GetInt32(idx_ScoreID),
                                Story = reader.IsDBNull(idx_Story) ? null : reader.GetString(idx_Story),
                                UserID = reader.GetInt32(idx_UserID)
                            };
                            return aGame;
                        }
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            
        }

        public void AddGame(Game gameToAdd)
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    var cmd = new SqlCommand("AppSchema.Usp_addGame", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = gameToAdd.UserID;
                    cmd.Parameters.Add("@GameName", SqlDbType.VarChar, 65).Value = gameToAdd.GameName;
                    cmd.Parameters.Add("@ReleaseDate", SqlDbType.Date).Value = gameToAdd.ReleaseDate;
                    cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 35).Value = gameToAdd.CompanyName;
                    cmd.Parameters.Add("@Story", SqlDbType.VarChar, 1000).Value = gameToAdd.Story;
                    cmd.Parameters.Add("@PlayersOnline", SqlDbType.Int).Value = gameToAdd.PlayersOnline;
                    cmd.Parameters.Add("@PlayersOffline", SqlDbType.SmallInt).Value = gameToAdd.PlayersOffline;
                    cmd.Parameters.Add("@CustomNote", SqlDbType.VarChar, 2000).Value = gameToAdd.CustomNote;


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
        public void EditGame(Game gameToEdit) // Spelets attribut sätts ihop till ett Game-objekt i Service klassen, där den sedan skickas in här.
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    var cmd = new SqlCommand("AppSchema.Usp_EditGame", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@GameID", SqlDbType.Int).Value = gameToEdit.GameID;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = gameToEdit.UserID;
                    cmd.Parameters.Add("@ScoreID", SqlDbType.Int).Value = gameToEdit.ScoreID;
                    cmd.Parameters.Add("@GameName", SqlDbType.VarChar, 65).Value = gameToEdit.GameName;
                    cmd.Parameters.Add("@ReleaseDate", SqlDbType.Date).Value = gameToEdit.ReleaseDate;
                    cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 35).Value = gameToEdit.CompanyName;
                    cmd.Parameters.Add("@Story", SqlDbType.VarChar, 1000).Value = gameToEdit.Story;
                    cmd.Parameters.Add("@PlayersOnline", SqlDbType.Int).Value = gameToEdit.PlayersOnline;
                    cmd.Parameters.Add("@PlayersOffline", SqlDbType.SmallInt).Value = gameToEdit.PlayersOffline;
                    cmd.Parameters.Add("@CustomNote", SqlDbType.VarChar, 2000).Value = gameToEdit.CustomNote;


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
        public void DeleteGame(int GameID, int UserID)
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    var cmd = new SqlCommand("AppSchema.Usp_RemoveGame", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@GameID", SqlDbType.Int).Value = GameID;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
            }catch(Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }
        public List<Game> GetAllGamesByUserID(int UserID)
        {
            List<Game> allGamesByUserID = new List<Game>();
            using(var conn = new SqlConnection(GetConnectionString())){
                var cmd = new SqlCommand("AppSchema.Usp_GetAllGamesByUserID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                conn.Open();
                using(var reader = cmd.ExecuteReader()){
                        var idx_GameID = reader.GetOrdinal("GameID");
                        var idx_ScoreID = reader.GetOrdinal("ScoreID");
                        var idx_UserID = reader.GetOrdinal("UserID");
                        var idx_GameName = reader.GetOrdinal("GameName");
                        var idx_CompanyName = reader.GetOrdinal("CompanyName");
                        var idx_Story = reader.GetOrdinal("Story");
                        var idx_PlayersOnline = reader.GetOrdinal("PlayersOnline");
                        var idx_PlayersOffline = reader.GetOrdinal("PlayersOffline");
                        var idx_ReleaseDate = reader.GetOrdinal("ReleaseDate");
                        var idx_CustomNote = reader.GetOrdinal("CustomNote");

                        while(reader.Read())
                        {

                            allGamesByUserID.Add(new Game
                            {
                                CompanyName = reader.IsDBNull(idx_CompanyName) ? null : reader.GetString(idx_CompanyName),
                                CustomNote = reader.IsDBNull(idx_CustomNote) ? null : reader.GetString(idx_CustomNote),
                                GameID =  reader.GetInt32(idx_GameID),
                                GameName = reader.GetString(idx_GameName),
                                PlayersOffline = reader.IsDBNull(idx_PlayersOffline) ? (short?)null : reader.GetInt16(idx_PlayersOffline), //Tydligen ska int16 vara samma som smallint, vet inte om detta är sant, får bli upp till bevis..
                                PlayersOnline = reader.IsDBNull(idx_PlayersOnline) ? (int?)null : reader.GetInt32(idx_PlayersOnline),
                                ReleaseDate = reader.IsDBNull(idx_ReleaseDate) ? (DateTime?) null : reader.GetDateTime(idx_ReleaseDate),
                                ScoreID = reader.IsDBNull(idx_ScoreID) ? (int?) null : reader.GetInt32(idx_ScoreID),
                                Story = reader.IsDBNull(idx_Story) ? null : reader.GetString(idx_Story),
                                UserID = reader.GetInt32(idx_UserID)
                            });
                            
                        }

                        return allGamesByUserID;
                    
                }
                
            }
             
        }
    }
}

                    //cmd.Parameters.Add("@ReleaseDate", SqlDbType.Date).Value = (object)gameToAdd.ReleaseDate ?? DBNull.Value;
                    //cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar, 35).Value = (object)gameToAdd.CompanyName ?? DBNull.Value;
                    //cmd.Parameters.Add("@Story", SqlDbType.VarChar, 1000).Value = (object)gameToAdd.Story ?? DBNull.Value;
                    //cmd.Parameters.Add("@PlayersOnline", SqlDbType.Int).Value = (object)gameToAdd.PlayersOnline ?? DBNull.Value;
                    //cmd.Parameters.Add("@PlayersOffline", SqlDbType.SmallInt).Value = (object)gameToAdd.PlayersOffline ?? DBNull.Value;
                    //cmd.Parameters.Add("@CustomNote", SqlDbType.VarChar, 2000).Value = (object)gameToAdd.CustomNote ?? DBNull.Value;


                                //CompanyName = reader.GetString(idx_CompanyName),
                                //CustomNote = reader.GetString(idx_CustomNote),
                                //GameID = reader.GetInt32(idx_GameID),
                                //GameName = reader.GetString(idx_GameName),
                                //PlayersOffline = reader.GetInt16(idx_PlayersOffline), //Tydligen ska int16 vara samma som smallint, vet inte om detta är sant, får bli upp till bevis..
                                //PlayersOnline = reader.GetInt32(idx_PlayersOnline),
                                //ReleaseDate = reader.GetDateTime(idx_ReleaseDate),
                                //ScoreID = reader.GetInt32(idx_ScoreID),
                                //Story = reader.GetString(idx_Story),
                                //UserID = reader.GetInt32(idx_UserID)