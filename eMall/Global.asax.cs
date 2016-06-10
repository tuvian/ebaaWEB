using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Diagnostics;
using BusinussLayer;

namespace eMall
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

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
            //get reference to the source of the exception chain
            Exception ex = Server.GetLastError().GetBaseException();
            eMallBL.logErrors(ex.Source, ex.GetType().ToString(), ex.Message);

            //log the details of the exception and page state to the
            //Windows 2000 Event Log
            //EventLog.WriteEntry("Test Web",
            //  "MESSAGE: " + ex.Message +
            //  "\nSOURCE: " + ex.Source +
            //  "\nFORM: " + Request.Form.ToString() +
            //  "\nQUERYSTRING: " + Request.QueryString.ToString() +
            //  "\nTARGETSITE: " + ex.TargetSite +
            //  "\nSTACKTRACE: " + ex.StackTrace,
            //  EventLogEntryType.Error);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}