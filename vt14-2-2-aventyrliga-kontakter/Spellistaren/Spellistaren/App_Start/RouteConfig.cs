using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Spellistaren.App_Start
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //EDIT: 2014-03-22 Felet var att jag inte hade raden under... 
            routes.MapPageRoute("Default2", "", "~/pages/Default.aspx");

            routes.MapPageRoute("Default", "Listor", "~/pages/Default.aspx");
            routes.MapPageRoute("AddOrEdit", "Listor/Edit", "~/pages/AddOrEdit.aspx");
        }
    }
}