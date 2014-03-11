using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Spellistaren.model.DAL
{
    public abstract class DALBase 
    {
        protected string GetConnectionString()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["userConn"].ConnectionString;
            return connectionString;
        }
    }
}