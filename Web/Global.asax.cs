using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Web
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup


        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            //Exception exc = Server.GetLastError();

            //// Handle HTTP errors
            //if (exc.GetType() == typeof(HttpException))
            //{
            //    // The Complete Error Handling Example generates
            //    // some errors using URLs with "NoCatch" in them;
            //    // ignore these here to simulate what would happen
            //    // if a global.asax handler were not implemented.
            //    if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
            //        return;

            //    //Redirect HTTP errors to HttpError page
            //    Server.Transfer("HttpErrorPage.aspx");
            //}

            //// For other kinds of errors give the user some information
            //// but stay on the default page
            //Response.Write("<h2>Try again, and if the problem persists, see your system administrator.</h2>\n");
            //Response.Write(
            //    "<p>" + exc.Message + "</p>\n");
            //Response.Write("Return to the <a href='/Sales/Index.aspx'>" +
            //    "Default Page</a>\n");

            //// Log the exception and notify system operators
            ////LogException(exc, "DefaultPage");
            ////NotifySystemOps(exc);

            //// Clear the error from the server
            //Server.ClearError();
            // The code here is disabled now because I prefer using a distinct class that inherits from IHttpModule. This way I
            // have a reusable class that I can share across all of my applications.

            // Exception ex = HttpContext.Current.Server.GetLastError();
            // CrashReport report = CrashReporter.CreateReport(ex, null);
            // HttpContext.Current.Session[Settings.Names.CrashReport] = report;

            // If an unhandled exception is thrown here then this Application_Error event handler will re-catch it, 
            // wiping out the original exception, and it will not re-throw the exception -- so this line of code does
            // not create an infinite loop (although it might appear to do so).

            // throw new DemoException("What happens to an unhandled exception in Global.Application_Error?");
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
