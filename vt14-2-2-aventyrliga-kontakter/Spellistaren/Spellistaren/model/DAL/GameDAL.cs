using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Spellistaren.model.DAL
{
    public static class GameDAL : DALBase
    {
        public Game GetGameDetails(int UserID, int GameID)
        {
            using(var conn = new SqlConnection(GetConnectionString())){

                var cmd = new SqlCommand("AppSchema.Usp_GetGameDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

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

                    if(reader.Read()){

                        Game aGame = new Game { 
                        CompanyName = reader.GetString(idx_CompanyName),
                        CustomNote = reader.GetString(idx_CustomNote),
                        GameID = reader.GetInt32(idx_GameID),
                        GameName = reader.GetString(idx_GameName),
                        PlayersOffline = reader.GetInt16(idx_PlayersOffline), //Tydligen ska int16 vara samma som smallint, vet inte om detta är sant, får bli upp till bevis..
                        PlayersOnline = reader.GetInt32(idx_PlayersOnline),
                        ReleaseDate = reader.GetDateTime(idx_ReleaseDate),
                        ScoreID = reader.GetInt32(idx_ScoreID),
                        Story = reader.GetString(idx_Story),
                        UserID = reader.GetInt32(idx_UserID)
                        };
                        return aGame;
                    }                    
                }

                return null;
            }


        }
    }
}