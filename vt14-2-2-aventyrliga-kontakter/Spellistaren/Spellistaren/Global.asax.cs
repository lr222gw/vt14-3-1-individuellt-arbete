﻿using Spellistaren.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace Spellistaren
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var jQuery = new ScriptResourceDefinition
            {
                Path = "~/scripts/jquery-2.1.0.min.js",
                DebugPath = "~/scripts/jquery-2.1.0.js",
                CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-2.1.0.min.js",
                CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-2.1.0.js",
            };
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", jQuery);

            RouteConfig.RegisterRoutes(RouteTable.Routes); //RouteTable innehåller alla Routes... vi skickar alltså in den som en parameter som alla routes läggs in i.

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}