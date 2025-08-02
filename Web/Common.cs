using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Web.Security;
using System.Data;
using Twitterizer;
using Facebook;
using System.Dynamic;
using System.Net;
using System.IO;


namespace Web
{
    public class Common
    {

        Database.ERPEntities DB = new Database.ERPEntities();
        public class FacebookVariable
        {
            public string AuthenticationUrl
            {
                get
                {
                    return string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}", FacebookContext.Current.AppId, HttpUtility.UrlEncode(this.RedirectUrl), this.Scope);
                }
            }
            public string RedirectUrl
            {
                get
                {
                    return ConfigurationManager.AppSettings["callbackUrl"].ToString();
                }
            }
            public string Scope
            {
                get
                {
                    return "offline_access,publish_stream,email,user_birthday,user_about_me,user_groups,user_location,user_likes,user_events,friends_interests,user_activities,user_hometown,user_interests";

                }
            }

        }

    }
}

