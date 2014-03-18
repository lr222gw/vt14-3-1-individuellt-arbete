using Spellistaren.model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        public void AddGame(string CompanyName, string GameName, string PlayersOffline, string PlayersOnline, string ReleaseDate, string Story, string customNote, out string problem) // tar ej emot UserID, då den alltid är 1..
        {
            problem = null; // om allt går rätt kommer problem vara null...
            try
            {
                short? newPlayerOffline;
                int? newPlayersOffline;
                DateTime? newReleaseDate;
                if (PlayersOffline == null)
                {
                    newPlayerOffline = null;
                }
                else {newPlayerOffline = short.Parse(PlayersOffline.ToString());}// om värdet ej är null så sska det sparas. värdet kan bara parsas från string, så först gör vi värdet till string sen parsar vi till short..

                if (PlayersOffline == null)
                {
                    newPlayersOffline = null;
                }
                else { newPlayersOffline = short.Parse(PlayersOffline.ToString()); }

                if (ReleaseDate == null)
                {
                    newReleaseDate = null;
                }
                else { newReleaseDate = DateTime.Parse(ReleaseDate.ToString()); }

                var gameToAdd = new Game
                {
                    CompanyName =  CompanyName,
                    CustomNote = customNote,
                    GameName = GameName,
                    PlayersOffline = newPlayerOffline,
                    PlayersOnline = newPlayersOffline,
                    ReleaseDate = newReleaseDate,
                    Story = Story,
                    ScoreID = 11 //hårdkådar denna eftersom jag inte har med betyg...
                };

                var validationContext = new ValidationContext(gameToAdd); // skapar ett validationcontext objekt för att validera datan i gameToAdd..
                var validationErrorList = new List<ValidationResult>();
                if(!Validator.TryValidateObject(gameToAdd, validationContext, validationErrorList, true)){
                    //om validering (^) misslyckas så ska ett undantag kastas..
                    problem = "Spelet innehöll ogiltig data..";
                    throw new ValidationException(problem);

                }
                else
                {
                    GameDAL.AddGame(gameToAdd);
                }

            }
            catch
            {
               // throw new ArgumentException("ett problem uppstod vid tilläggningen av spelet..");
            }

        }
        public void EditGame(TextBox[] tbArr, int GameID)
        {
            try
            {
                short? pOffline;
                int? pOnline;
                DateTime? dateAndTime;
                if(tbArr[3].Text == ""){
                    pOffline = null;
                }
                else
                {
                    string h = tbArr[3].Text;
                    pOffline = short.Parse(h);
                }
                if (tbArr[4].Text == "")
                {
                    pOnline = null;
                }
                else
                {
                    string h = tbArr[4].Text;
                    pOnline = int.Parse(h);
                }
                if (tbArr[2].Text == "")
                {
                    dateAndTime = null;
                }
                else
                {
                    var h = tbArr[2].Text;
                    dateAndTime = DateTime.Parse(h);
                }

                //först hämtar vi ner spelet vi ska redigera..
                Game gameToEdit = GameDAL.GetGameDetails(1, GameID);
                //Sen tar vi fram den nya datan vi vill byta ut i spelet..
                gameToEdit.PlayersOffline = pOffline;
                gameToEdit.PlayersOnline = pOnline;
                gameToEdit.Story = tbArr[5].Text;
                gameToEdit.ReleaseDate = dateAndTime;
                gameToEdit.CompanyName = tbArr[1].Text;
                gameToEdit.CustomNote = tbArr[6].Text;
                gameToEdit.GameName = tbArr[0].Text;

                GameDAL.EditGame(gameToEdit);
            }catch
            {
                throw new Exception("Något är fel på datan som försökte matas in.");      
            }
            
        }
        public void RemoveGameFromList(int ListID, int GameID)
        {
            ListDAL.RemoveGameFromList(ListID, GameID);
        }
        public void AddGameToList(int GameID, int ListID)
        {
            ListDAL.AddGameToList(GameID, ListID);
        }
        public void DeleteGame(int GameID)
        {
            GameDAL.DeleteGame(GameID, 1);
        }
        public bool existonlistBool(int List1, int GameID)
        {
            List<ListRow> list1 = ListDAL.GetListContent(List1, 1);// hämtar ner listraderna 
            for (var i = 0; i < list1.Count; i++)
            {
                if (list1[i].GameID == GameID) // kollar om det finns en listrad med samma id
                {
                    return false; // returnerar false om det gör det
                }
            }
            return true; //returnera true om det inte gör det..
        }
        public void RemoveList(int ListID)
        {
            _ListDAL.RemoveList(ListID, 1);
        }

    }
}