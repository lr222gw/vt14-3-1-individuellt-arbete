﻿using Spellistaren.model;      //TA BORT MIG EFTER TESTER!
using Spellistaren.model.DAL; //TA BORT MIG EFTER TESTER!
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spellistaren.pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RawUrl == "/pages/Default.aspx") //om man försöker gå till sidan via "original"-vägen så blir det min Routade väg!
            {
                Response.Redirect(GetRouteUrl("Default", null));
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            var gg = new GameDAL();
            //gg.GetGameDetails(int.Parse(b1.Text), int.Parse(b2.Text));

            Game game = new Game
            {
                CompanyName = null,
                CustomNote = null,
                GameName = "Joni",
                PlayersOffline = null,
                PlayersOnline = null,
                ReleaseDate = null,
                Story = null,
            };
            var gameee = gg.GetGameDetails(1, 18);

            gameee.GameName = "pocahontas";

            gg.EditGame(gameee);

        }
    }
}