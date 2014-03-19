using Spellistaren.model;
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
        private Service _Service;
        private Service Service
        {
            get { return _Service ?? (_Service = new Service()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RawUrl == "/pages/Default.aspx") //om man försöker gå till sidan via "original"-vägen så blir det min Routade väg!
            {
                Response.Redirect(GetRouteUrl("Default", null));
                Context.ApplicationInstance.CompleteRequest();
            }
        }


        public IEnumerable<Spellistaren.model.List> ListRepeater_GetData()
        {
            return Service.GetLists();
        }

        public IEnumerable<Spellistaren.model.Game> ListContentRepeater_GetData()
        {
            return Service.GetListContent(Convert.ToInt32(Request.QueryString["List"]));
        }

        public Game GameDetailsRepeater_GetData()
        {
            
            return Service.GetGameDetails(Convert.ToInt32(Request.QueryString["amp;GameID"]));
        }
        protected void travelbutton_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetRouteUrl("AddOrEdit", null));
        }
    }
}