using Spellistaren.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spellistaren.pages
{
    public partial class AddOrEdit : System.Web.UI.Page
    {
        private Service _Service;
        private Service Service
        {
            get { return _Service ?? (_Service = new Service()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RawUrl == "/pages/AddOrEdit.aspx") //om man försöker gå till sidan via "original"-vägen så blir det min Routade väg!
            {
                Response.Redirect(GetRouteUrl("AddOrEdit", null));
                Context.ApplicationInstance.CompleteRequest();
            }
            if (Request.QueryString["GameID"] != null) // om GameID inte är null så finns det ett gameId att hämta..
            { //om det finns ett GameID att hämta så ska man kunna göra något med det, alltså ska Repeater2 bli visible..
                GameDetailRepeater.Visible = true;
            }
        }

        public IEnumerable<Spellistaren.model.Game> GamelistRepeater_GetData()
        {
            return Service.GetAllGamesByUserID(); 
        }

        public Game GameDetailRepeater_GetData()
        {
            if(Convert.ToInt32(Request.QueryString["GameID"]) != 0){ // Om GameID inte är 0 då ska ett spel hämtas
                return Service.GetGameDetails(Convert.ToInt32(Request.QueryString["GameID"]));
            }
            else// om GameID är noll, då ska fälten läggas ut med tom data som sedan kan schysteras till ett nytt spel.
            {
                
                return Service.getEmptyGame();
            }
        }

        protected void Sendbutton_Click(object sender, EventArgs e)
        {
            
            var gameid = Convert.ToInt32(Request.QueryString["GameID"]);
            if (gameid == 0) //om gameid == 0 så är det inte ett existerande spel, utan ett spel som ska läggas till
            {

                GameDetailRepeater.FindControl("GameName");
                if (ModelState.IsValid)
                {
                    var GameName = GameDetailRepeater.FindControl("GameName") as TextBox;
                    var CompanyName = GameDetailRepeater.FindControl("CompanyName") as TextBox;
                    var ReleaseDate = GameDetailRepeater.FindControl("ReleaseDate") as TextBox;
                    var PlayersOffline = GameDetailRepeater.FindControl("PlayersOffline") as TextBox;
                    var PlayersOnline = GameDetailRepeater.FindControl("PlayersOnline") as TextBox;
                    var Story = GameDetailRepeater.FindControl("Story") as TextBox;
                    var CustomNote = GameDetailRepeater.FindControl("CustomNote") as TextBox;

                    Service.AddGame(
                        CompanyName.Text.ToString() == "" ? null : CompanyName.Text.ToString(), // om det står något annat än "" så ska det istället stå null, om det står något så ska det användas..
                        GameName.Text.ToString() == "" ? null : GameName.Text.ToString(),
                        PlayersOffline.Text.ToString() == "" ? null : PlayersOffline.Text.ToString(),
                        PlayersOnline.Text.ToString() == "" ? null : PlayersOnline.Text.ToString(),
                        ReleaseDate.Text.ToString() == "" ? null : ReleaseDate.Text.ToString(),
                        Story.Text.ToString() == "" ? null : Story.Text.ToString(),
                        CustomNote.Text.ToString() == "" ? null : CustomNote.Text.ToString()
                        );                                            
                }
                
            }
            else // Allt annat är spel som ska uppdateras..
            {

            }
        }
        public string TextBoxValue(RepeaterItemCollection itm, string controlID ) // denna metod är till för att hämta ut värden ur textboxar på en Repeater..
        {               //Metoden tar emot två parametrar: itm = Den repeater.Item List som controllen ska letas i. controlID = ID't på kontrollen vi letar efter..
            for (var i = 0; i < itm.Count; i++ )
            {
                
            }
            return null;
        } 

        protected void addnewgame_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetRouteUrl("AddOrEdit", null) + "?GameID=0");
            Context.ApplicationInstance.CompleteRequest();
        }


    }
}