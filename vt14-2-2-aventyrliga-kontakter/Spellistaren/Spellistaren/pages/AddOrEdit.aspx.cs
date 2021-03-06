﻿using Spellistaren.model;
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
                Sendbutton.Visible = true;
                EraseButton.Visible = true;
            }
            if (Request.QueryString["GameID"] == "0")
            {
                EraseButton.Visible = false;
            }

            if (Request.QueryString["amp;GameID"] != null && Request.QueryString["List"] != null) // om både gameID och List finns så är ett spel markerat, då ska man få möjligheten att ta bort det.
            {
                DeleteButton.Visible = true;                
            }
            if (Request.QueryString["List"] != null)
            {
                gameToAddListHeader.Visible = true;
                gameToAddList.Visible = true;
                gameToAddListRepeater.Visible = true;
                removeListbutton.Visible = true;
                
            }

            if (Request.QueryString["amp;GameToAddID"] != null && Request.QueryString["List"] != null)
            {
                addToListButton.Visible = true;
            }
            if(Session["Message"] != null)
            {
                succsestext.Text = Session["Message"].ToString();
                Session["Message"] = null;
                succsestext.Visible = true;
            }
            

        }

        public IEnumerable<Spellistaren.model.Game> GamelistRepeater_GetData()
        {
            return Service.GetAllGamesByUserID(); //CHECK
        }

        public Game GameDetailRepeater_GetData()//CHECK
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
                    if (ModelState.IsValid) // kontrollerar så att fälten som måste vara i fyllda verkligen är det. (spelets namn..)
                    {                       
                        if(IsValid)
                        {
                            var GameName = GameDetailRepeater.FindControl("GameName") as TextBox;
                            var CompanyName = GameDetailRepeater.FindControl("CompanyName") as TextBox;
                            var ReleaseDate = GameDetailRepeater.FindControl("ReleaseDate") as TextBox;
                            var PlayersOffline = GameDetailRepeater.FindControl("PlayersOffline") as TextBox;
                            var PlayersOnline = GameDetailRepeater.FindControl("PlayersOnline") as TextBox;
                            var Story = GameDetailRepeater.FindControl("Story") as TextBox;
                            var CustomNote = GameDetailRepeater.FindControl("CustomNote") as TextBox;
                            string problem; // skapar denna så att jag kan få en out-variabel att testa senare..

                            Service.AddGame(
                                CompanyName.Text.ToString() == "" ? null : CompanyName.Text.ToString(),  // om det står något annat än "" så ska det istället stå null, om det står något så ska det användas..
                                GameName.Text.ToString() == "" ? null : GameName.Text.ToString(),
                                PlayersOffline.Text.ToString() == "" ? null : PlayersOffline.Text.ToString(),
                                PlayersOnline.Text.ToString() == "" ? null : PlayersOnline.Text.ToString(),
                                ReleaseDate.Text.ToString() == "" ? null : ReleaseDate.Text.ToString(),
                                Story.Text.ToString() == "" ? null : Story.Text.ToString(),
                                CustomNote.Text.ToString() == "" ? null : CustomNote.Text.ToString(),
                                out problem
                                );
                            if (problem != null) // om problem inte är null, då är allt som det ska. Annars så är det fel i klassen..
                            {
                                var validator = new CustomValidator { };
                                validator.IsValid = false;
                                validator.ErrorMessage = problem; //Skriv ut vad problem är för något.. (kan bara vara en sak..)
                                Page.Validators.Add(validator);
                                //ModelState.AddModelError(string.Empty, problem); 
                            }
                            else
                            {
                                Session["Message"] = "Spelet lyckades läggas in!";
                                Response.Redirect(GetRouteUrl("AddOrEdit", null)); //Gör en PRG
                                Context.ApplicationInstance.CompleteRequest();
                            }
                        }
                    }
                
                }
                else // Allt annat är spel som ska uppdateras..
                {
                    try { 
                        //hör behöver jag inte kolla om något är Valid eller inte, just för att man inte kan skicka med ogiltiga värden.. 
                        //(tex: du kan ej skicka med null om du inte anger något, då skickas placeholder värdet med istället..
                        if(IsValid){
                            TextBox[] textboxArr = new TextBox[7];
                            textboxArr[0] = GameDetailRepeater.FindControl("GameName") as TextBox;
                            textboxArr[1] = GameDetailRepeater.FindControl("CompanyName") as TextBox;
                            textboxArr[2] = GameDetailRepeater.FindControl("ReleaseDate") as TextBox;
                            textboxArr[3] = GameDetailRepeater.FindControl("PlayersOffline") as TextBox;
                            textboxArr[4] = GameDetailRepeater.FindControl("PlayersOnline") as TextBox;
                            textboxArr[5] = GameDetailRepeater.FindControl("Story") as TextBox;
                            textboxArr[6] = GameDetailRepeater.FindControl("CustomNote") as TextBox;
                     
                   
                            Service.EditGame(textboxArr, gameid);
                            Session["Message"] = "Spelet lyckades redigeras!";
                            Response.Redirect(GetRouteUrl("AddOrEdit", null)); //Gör en PRG
                            Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            throw new ArgumentException("Datan var ej giltig");
                        }

                    }
                    catch(Exception ex){ 
                    Page.Validators.Add(new CustomValidator{
                        ErrorMessage = "Något är fel med den inmatade datan.." + ex,
                        IsValid = false
                        });
                    }
                }
            
        }
        //public string TextBoxValue(RepeaterItemCollection itm, string controlID ) // denna metod är till för att hämta ut värden ur textboxar på en Repeater..
        //{               //Metoden tar emot två parametrar: itm = Den repeater.Item List som controllen ska letas i. controlID = ID't på kontrollen vi letar efter..
        //    for (var i = 0; i < itm.Count; i++ )
        //    {
                
        //    }
        //    return null;
        //} 

        protected void addnewgame_Click(object sender, EventArgs e)//Check
        {
            Response.Redirect(GetRouteUrl("AddOrEdit", null) + "?GameID=0");
            Context.ApplicationInstance.CompleteRequest();
        }

        public IEnumerable<Spellistaren.model.List> ListRepeater_GetData() //Check
        {
            return Service.GetLists();
        }
        public IEnumerable<Spellistaren.model.Game> ListContentRepeater_GetData()//Check
        {
            return Service.GetListContent(Convert.ToInt32(Request.QueryString["List"]));
        }

        protected void DeleteButton_Click(object sender, EventArgs e)//Check
        {
            try { 
                Service.RemoveGameFromList(Convert.ToInt32(Request.QueryString[("List")]), Convert.ToInt32(Request.QueryString["amp;GameID"]));
                Session["Message"] = "Spelet togs bort från listan!";
                Response.Redirect(GetRouteUrl("AddOrEdit", null)+"?List="+Convert.ToInt32(Request.QueryString[("List")]));
                Context.ApplicationInstance.CompleteRequest();
                
            }
            catch(Exception ex)
            {
                Page.Validators.Add(new CustomValidator
                {
                    ErrorMessage = "Något är fel med den inmatade datan.." + ex,
                    IsValid = false
                });
            }
        }

        protected void addToListButton_Click(object sender, EventArgs e)//Check
        {
            if (Service.existonlistBool(Convert.ToInt32(Request.QueryString["List"]), Convert.ToInt32(Request.QueryString["amp;GameToAddID"])))
            {
                try
                {
                    Service.AddGameToList(Convert.ToInt32(Request.QueryString["amp;GameToAddID"]), Convert.ToInt32(Request.QueryString["List"]));

                    Session["Message"] = "Spelet lades till i listan!";
                    Response.Redirect(GetRouteUrl("AddOrEdit", null) + "?List=" + Convert.ToInt32(Request.QueryString[("List")]));
                    Context.ApplicationInstance.CompleteRequest();

                }
                catch(Exception ex)
                {
                    Page.Validators.Add(new CustomValidator
                    {
                        IsValid = false,
                        ErrorMessage = "Något gick fel när spelet skulle läggas in i listan"+ex
                    });
                }
                

            }
            else
            {
                Page.Validators.Add(new CustomValidator
                {
                    IsValid = false,
                    ErrorMessage = "Spelet finns redan i listan"
                });
            }
        }

        protected void EraseButton_Click(object sender, EventArgs e)//Check
        {
            try
            {
                Service.DeleteGame(Convert.ToInt32(Request.QueryString["GameID"]));

                Session["Message"] = "Spelet  är nu borta..!";
                Response.Redirect(GetRouteUrl("AddOrEdit", null));
                Context.ApplicationInstance.CompleteRequest();
            }catch(Exception ex)
            {
                Page.Validators.Add(new CustomValidator
                {
                    IsValid = false,
                    ErrorMessage = "Spelet lyckades inte tas bort.."+ex
                });
            }
            
            
            
        }

        protected void Close_Click(object sender, EventArgs e)//Check
        {
            Response.Redirect(GetRouteUrl("AddOrEdit", null));
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void removeListbutton_Click(object sender, EventArgs e)//Check
        {
            if (Service.doesListExistBool(Convert.ToInt32(Request.QueryString["List"]))) //kotnrollerar om det finns en lista med ett sånt id som man vill ta bort..
            {//kontrollerar om det finns en lista med det ID't..
                try
                {
                    Service.RemoveList(Convert.ToInt32(Request.QueryString["List"]));

                    Session["Message"] = "Listan är nu borttagen!";
                    Response.Redirect(GetRouteUrl("AddOrEdit", null));
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch(Exception ex)
                {
                    Page.Validators.Add(new CustomValidator
                    {
                        IsValid = false,
                        ErrorMessage = "Något gick fel när listan skulle tas bort.."+ex
                    });
                }
                
            }
            else
            {
                Page.Validators.Add(new CustomValidator
                {
                    IsValid = false,
                    ErrorMessage = "Finns ingen lista med ett sådant id.."
                });
            }
        }

        protected void AddListButton_Click(object sender, EventArgs e)
        {
            try
            {
                Service.CreateList(NewListBox.Text);

                Session["Message"] = "Listan är nu tillagd!";
                Response.Redirect(GetRouteUrl("AddOrEdit", null));
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Page.Validators.Add(new CustomValidator
                {
                    IsValid = false,
                    ErrorMessage = "Något gick fel när listan skulle läggas till..."+ex
                });

            }

        }

        protected void travelbutton_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetRouteUrl("Default", null));
        }

    }
}