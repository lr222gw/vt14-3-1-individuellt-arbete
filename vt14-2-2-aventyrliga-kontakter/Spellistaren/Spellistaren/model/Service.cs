using Spellistaren.model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace Spellistaren.model
{
    public class Service
    {
        private ListDAL _ListDAL;
        private GameDAL _GameDAL;

        private ListDAL ListDAL
        {
            get { return _ListDAL ??  (_ListDAL = new ListDAL()); } //om _ListDAL är Null, sätt ListDAL till ny ListDAL...
        }
        private GameDAL GameDAL
        {
            get { return _GameDAL ?? (_GameDAL = new GameDAL()); } //om _GameDAL är Null, sätt ListDAL till ny GameDAL...
        }

        public IEnumerable<Game> GetListContent(int ListID) 
        {
            var Listcontent =  ListDAL.GetListContent(ListID, 1); //hårdkodar in värdet 1 då programmet är begränsat till 1 användare..
            var ListcontentAndName = new List<Game>();
            Game GameDet;
            for (var i = 0; i < Listcontent.Count; i++ )
            {
                GameDet = GameDAL.GetGameDetails(1, Listcontent[i].GameID); //hämtar ut spelet som finns på listan.
                ListcontentAndName.Add(GameDet); //lägger in spelet i listan..
            }

            return ListcontentAndName; //returnerar alla spelen i listan...
        }

        public IEnumerable<List> GetLists()
        {
            return ListDAL.GetLists(1); //Hårdkodar in värdet 1 här, då programmet blir begränsatt till 1 användare...
        }

        public Game GetGameDetails(int GameID)
        {
            return GameDAL.GetGameDetails(1, GameID); //Hårdkodar in värdet 1 här...
        }
        public List<Game> GetAllGamesByUserID()
        {
            return GameDAL.GetAllGamesByUserID(1); //Hårdkodar in värdet 1 här också.. lite synd men visst!
        }

        public static Game getEmptyGame() //Denna var förut Internal static, allt fungerade då, ändrade till public, vet ej vad internal är..
        {
            return new Game { 
            CompanyName = "",
            GameName = "",
            PlayersOffline = null,
            PlayersOnline = null,
            ReleaseDate = null,
            Story = "",
            CustomNote = "",
            UserID = 1
            };
        }
        public static void AddGame(string? CompanyName, string GameName, string? PlayersOffline, string? PlayersOnline, string? ReleaseDate, string? Story, string? customNote) // tar ej emot UserID, då den alltid är 1..
        {

            try
            {
                short? newPlayerOffline;
                int? newPlayersOffline;
                DateTime? newReleaseDate;
                if (PlayersOffline == null)
                {
                    newPlayerOffline = null;
                }
                else {newPlayerOffline = PlayersOffline}

                if (PlayersOffline == null)
                {
                    newPlayersOffline == null;
                }
                else {newPlayersOffline PlayersOffline}

                var gameToAdd = new Game
                {
                    CompanyName =  CompanyName,
                    CustomNote = customNote,
                    GameName = GameName,
                    PlayersOffline = short.Parse(newPlayerOffline),
                    PlayersOnline = int.Parse(PlayersOffline),
                    ReleaseDate = DateTime.Parse(ReleaseDate),
                    Story = Story
                };

                var validationContext = new ValidationContext(gameToAdd); // skapar ett validationcontext objekt för att validera datan i gameToAdd..
                var validationErrorList = new List<ValidationResult>();
                if(!Validator.TryValidateObject(gameToAdd, validationContext, validationErrorList, true)){
                    //om validering (^) misslyckas så ska ett undantag kastas..
                    throw new ValidationException("Spelet innehöll ogiltig data..");

                }
                else
                {

                }

            }
            catch
            {

            }

        }

    }
}